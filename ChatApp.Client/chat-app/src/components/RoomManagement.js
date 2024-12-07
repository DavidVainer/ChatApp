import React, { useState, useEffect } from "react";
import {
    Box,
    Button,
    TextField,
    Typography,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    IconButton,
    Alert,
} from "@mui/material";
import { Delete } from "@mui/icons-material";
import { getAllRooms, createRoom, deleteRoom } from "../api/room";

const RoomManagement = () => {
    const [rooms, setRooms] = useState([]);
    const [newRoom, setNewRoom] = useState({ name: "" });
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchRooms();
    }, []);

    const fetchRooms = async () => {
        try {
            const data = await getAllRooms();
            setRooms(data);
            setError(null);
        } catch {
            setError("Failed to fetch rooms.");
        }
    };

    const handleAddRoom = async () => {
        if (!newRoom.name) {
            setError("Please fill in all fields.");
            return;
        }

        try {
            await createRoom(newRoom);
            setNewRoom({ name: "" });
            fetchRooms();
        } catch {
            setError("Failed to add room.");
        }
    };

    const handleDeleteRoom = async (roomId) => {
        try {
            await deleteRoom(roomId);
            fetchRooms();
        } catch {
            setError("Failed to delete room.");
        }
    };

    return (
        <Box width={"75%"} margin={"0px auto"}>
            <Typography variant="h4" marginBottom={3}>
                Room Management
            </Typography>

            {error && (
                <Alert severity="error" sx={{ mb: 3 }}>
                    {error}
                </Alert>
            )}

            <Box autoComplete="off" sx={{ mb: 4, maxWidth: "50%" }}>
                <Typography variant="h6" marginBottom={2}>
                    Add New Room
                </Typography>

                <TextField
                    label="Room Name"
                    value={newRoom.name}
                    onChange={(e) =>
                        setNewRoom({ ...newRoom, name: e.target.value })
                    }
                    fullWidth
                    sx={{ marginBottom: 1 }}
                    required
                />

                <Button variant="contained" onClick={handleAddRoom} fullWidth>
                    Add New Room
                </Button>
            </Box>

            <Typography variant="h6" marginBottom={2}>
                Rooms List
            </Typography>

            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>Name</TableCell>
                        <TableCell>Created At</TableCell>
                        <TableCell>Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rooms?.map((room) => (
                        <TableRow key={room.id}>
                            <TableCell>{room.id}</TableCell>
                            <TableCell>{room.name}</TableCell>
                            <TableCell>{new Date(room.createdAt).toLocaleString()}</TableCell>
                            <TableCell>
                                <IconButton onClick={() => handleDeleteRoom(room.id)}>
                                    <Delete />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Box>
    );
};

export default RoomManagement;
