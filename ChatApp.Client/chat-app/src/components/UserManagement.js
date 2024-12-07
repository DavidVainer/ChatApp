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
import { addUser, deleteUser, getAllUsers } from "../api/user";

const UserManagement = () => {
    const [users, setUsers] = useState([]);
    const [newUser, setNewUser] = useState({
        email: "",
        displayName: "",
        password: "",
    });
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            const data = await getAllUsers();
            setUsers(data);
            setError(null);
        } catch {
            setError("Failed to fetch users.");
        }
    };

    const handleAddUser = async () => {
        if (!newUser.email || !newUser.displayName || !newUser.password) {
            setError("Please fill in all fields.");
            return;
        }

        try {
            await addUser(newUser);
            setNewUser({ email: "", displayName: "", password: "" });
            fetchUsers();
        } catch {
            setError("Failed to add user.");
        }
    };

    const handleDeleteUser = async (userId) => {
        try {
            await deleteUser(userId);
            fetchUsers();
        } catch {
            setError("Failed to delete user.");
        }
    };

    return (
        <Box width={"75%"} margin={"0px auto"}>
            <Typography variant="h4" marginBottom={3}>
                User Management
            </Typography>

            {error && (
                <Alert severity="error" sx={{ mb: 3 }}>
                    {error}
                </Alert>
            )}

            <Box autoComplete="off" sx={{ mb: 4, maxWidth: "50%" }}>
                <Typography variant="h6" marginBottom={2}>
                    Add New User
                </Typography>

                <TextField
                    label="Email"
                    type="email"
                    value={newUser.email}
                    onChange={(e) =>
                        setNewUser({ ...newUser, email: e.target.value })
                    }
                    fullWidth
                    sx={{ marginBottom: 1 }}
                    required
                />

                <TextField
                    label="Display Name"
                    value={newUser.displayName}
                    onChange={(e) =>
                        setNewUser({ ...newUser, displayName: e.target.value })
                    }
                    fullWidth
                    sx={{ marginBottom: 1 }}
                    required
                />

                <TextField
                    label="Password"
                    type="password"
                    value={newUser.password}
                    onChange={(e) =>
                        setNewUser({ ...newUser, password: e.target.value })
                    }
                    fullWidth
                    sx={{ marginBottom: 1 }}
                    required
                />

                <Button variant="contained" onClick={handleAddUser} fullWidth>
                    Add New User
                </Button>
            </Box>

            <Typography variant="h6" marginBottom={1}>
                Users List
            </Typography>

            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>ID</TableCell>
                        <TableCell>Email</TableCell>
                        <TableCell>Display Name</TableCell>
                        <TableCell>Created At</TableCell>
                        <TableCell>Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {users?.map((user) => (
                        <TableRow key={user.id}>
                            <TableCell>{user.id}</TableCell>
                            <TableCell>{user.email}</TableCell>
                            <TableCell>{user.displayName}</TableCell>
                            <TableCell>{new Date(user.createdAt).toLocaleString()}</TableCell>
                            <TableCell>
                                <IconButton onClick={() => handleDeleteUser(user.id)}>
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

export default UserManagement;
