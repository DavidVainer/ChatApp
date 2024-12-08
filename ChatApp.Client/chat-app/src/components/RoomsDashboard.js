import React, { useState, useEffect } from "react";
import {
    Box,
    Typography,
    Card,
    CardContent,
    CardActions,
    Button, Alert
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { getActiveRooms } from "../api/room";

const RoomsDashboard = () => {
    const [rooms, setRooms] = useState([]);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        fetchRooms();
    }, []);

    const fetchRooms = async () => {
        try {
            const data = await getActiveRooms();
            setRooms(data);
        } catch (err) {
            setError("Failed to fetch rooms.");
        }
    };

    return (
        <Box>
            <Typography variant="h4" marginBottom={3}>
                Rooms List
            </Typography>

            {error ? (
                <Alert severity="error" sx={{ mt: 3 }}>
                    { error }
                </Alert>
            ) : rooms.length === 0 ? (
                <Typography color="textSecondary">
                    No rooms available at the moment.
                </Typography>
            ) : (
                <Box display={"flex"} gap={3} justifyContent={"space-between"} flexWrap={"wrap"}>
                    {rooms.map((room) => (
                        <Card key={room.id} sx={{ width: "30%" }} elevation={3}>
                            <CardContent sx={{ display: "flex", justifyContent: "center" }}>
                                <Typography variant="h4" fontWeight={"bold"}>
                                    { room.name }
                                </Typography>
                            </CardContent>
                            <CardActions>
                                <Button variant="contained" color="primary" onClick={() => navigate(`/rooms/${room.id}`)} fullWidth>
                                    Enter Room
                                </Button>
                            </CardActions>
                        </Card>
                    ))}
                </Box>
            )}
        </Box>
    );
};

export default RoomsDashboard;
