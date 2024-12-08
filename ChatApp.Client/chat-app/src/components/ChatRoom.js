import React, { useState, useEffect } from "react";
import {
    Box,
    Typography,
    TextField,
    Button,
    List,
    ListItem,
    ListItemText,
    Divider,
} from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { getRoomDetails } from "../api/room";
import UnseenIcon from "../assets/unseen-icon.png";
import SeenIcon from "../assets/seen-icon.png";

const ChatRoom = () => {
    const { roomId } = useParams();
    const navigate = useNavigate();
    const [roomDetails, setRoomDetails] = useState({
        roomName: "",
        messages: [],
        messageAuthors: [],
        participants: [],
    });
    const [newMessage, setNewMessage] = useState("");
    const [connection, setConnection] = useState(null);

    const userId = localStorage.getItem("userId");

    useEffect(() => {
        const initializeChatRoom = async () => {
            try {
                const details = await getRoomDetails(roomId);
                setRoomDetails({
                    roomName: details.roomName || "",
                    messages: details.messages || [],
                    messageAuthors: details.messageAuthors || [],
                    participants: details.participants || [],
                });

                const connect = new HubConnectionBuilder()
                    .withUrl(`http://localhost:8080/chatHub`)
                    .build();

                connect.on("ReceiveMessage", (message) => {
                    setRoomDetails((prev) => ({
                        ...prev,
                        messages: [...prev.messages, message],
                    }));

                    connect.invoke("MarkMessageAsSeen", roomId, userId, message.id);
                });

                connect.on("UserJoined", (participantDetails) => {
                    if (participantDetails.userId === userId) {
                        participantDetails.userDisplayName = "You";
                    }

                    setRoomDetails((prev) => {
                        const participantExists = prev.participants?.some(participant => participant.userId === participantDetails.userId);

                        if (participantExists) {
                            return prev;
                        }

                        return {
                            ...prev,
                            participants: [
                                ...(prev.participants || []),
                                participantDetails
                            ]
                        };
                    });
                });

                connect.on("UserLeft", (userId) => {
                    setRoomDetails((prev) => ({
                        ...prev,
                        participants: (prev.participants || []).filter((p) => p.userId !== userId),
                    }));
                });

                connect.on("MessageSeen", (messageId, userId) => {
                    setRoomDetails((prev) => ({
                        ...prev,
                        messages: prev.messages.map((msg) =>
                            msg.id === messageId && !msg.seenBy?.includes(userId)
                                ? { ...msg, seenBy: [...(msg.seenBy || []), userId] }
                                : msg
                        ),
                    }));
                });

                await connect.start();
                await connect.invoke("JoinRoom", roomId, userId);

                setConnection(connect);
            } catch (error) {
                console.error("Error initializing chat room:", error);
            }
        };

        initializeChatRoom();

        return () => {
            connection?.stop();
        };
    }, [roomId]);

    const handleSendMessage = async () => {
        if (newMessage.trim() && connection) {
            await connection.invoke("SendMessage", roomId, userId, newMessage);
            setNewMessage("");
        }
    };

    const handleLeaveRoom = async () => {
        if (connection) {
            try {
                await connection.invoke("LeaveRoom", roomId, userId);
                navigate("/rooms");
            } catch (error) {
                console.error("Error leaving room:", error);
            }
        }
    };

    return (
        <Box display="flex" flexDirection="column" height="calc(100vh - 150px)">
            <Box display="flex" gap={3} alignItems="center" px={0}>
                <Typography variant="h3" margin={1}>Room Name: {roomDetails.roomName}</Typography>
                <Button variant={"contained"} color="secondary" onClick={handleLeaveRoom}>
                    Leave Room
                </Button>
            </Box>

            <Box display="flex" flex="1" overflow="hidden">
                <Box flex="1" display="flex" flexDirection="column" overflow="hidden">
                    <Box flex="1" overflow="auto" padding={2}>
                        {roomDetails.messages?.sort((a, b) => new Date(b.sentAt) - new Date(a.sentAt))?.map((message, index) => (
                            <Box key={index} marginBottom={2}>
                                <Typography variant="subtitle2">
                                    <strong>{ roomDetails.messageAuthors.find(author => author.id === message.senderId)?.displayName }</strong>
                                </Typography>
                                <Typography variant="body1">{message.content}</Typography>
                                <Typography variant="caption">{new Date(message.sentAt).toLocaleTimeString()}</Typography>
                                { (message.senderId === userId) && (
                                    <Box display="flex" alignItems="center" mt={2} mb={0.5}>
                                        {message.seenBy?.some(id => id !== userId) ? (
                                            <Typography variant="caption" color="primary">
                                                <img src={SeenIcon} alt={"Seen"} /> Seen
                                            </Typography>
                                        ) : (
                                            <Typography variant="caption" color="textSecondary">
                                                <img src={UnseenIcon} alt={"Unseen"} /> Unseen
                                            </Typography>
                                        )}
                                    </Box>
                                )}
                                <Divider />
                            </Box>
                        ))}
                    </Box>
                    <Box padding={2} display="flex" borderTop="1px solid #ccc">
                        <TextField
                            fullWidth
                            placeholder="Type your message..."
                            value={newMessage}
                            onChange={(e) => setNewMessage(e.target.value)}
                            onKeyPress={(e) => e.key === "Enter" && handleSendMessage()}
                        />
                        <Button variant="contained" color="primary" onClick={handleSendMessage} style={{ marginLeft: 8 }}>
                            Send
                        </Button>
                    </Box>
                </Box>

                <Box width="15%" borderLeft="1px solid #ccc" padding={2} overflow="auto">
                    <Typography variant="h6">Participants</Typography>
                    <List>
                        {roomDetails.participants?.map((participant, index) => (
                            <ListItem key={index}>
                                <ListItemText
                                    primary={participant.userDisplayName || participant.userId}
                                    secondary={`Joined at: ${new Date(participant.joinedAt).toLocaleString()}`}
                                />
                            </ListItem>
                        ))}
                    </List>
                </Box>
            </Box>
        </Box>
    );
};

export default ChatRoom;
