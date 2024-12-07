import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { loginUser } from "../api/auth";
import { Alert, Box, Button, TextField, Typography } from "@mui/material";

const LoginPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (token) {
            navigate("/rooms");
        }

    }, [navigate]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);

        try {
            const data = await loginUser(email, password);

            localStorage.setItem("userId", data.userId);
            localStorage.setItem("token", data.token);

            navigate("/rooms");
        } catch (error) {
            setError(error.response?.data || "Login failed. Please try again.");
        }
    };

    return (
        <Box
            display="flex"
            flexDirection="column"
            alignItems="center"
            mt={10}
        >
            <Typography variant="h3" marginBottom={5}>
                Login
            </Typography>

            <Box onSubmit={handleSubmit} component="form">
                <TextField
                    fullWidth
                    label="Email"
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                    sx={{ mb: 2 }}
                />

                <TextField
                    fullWidth
                    label="Password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />

                <Button
                    fullWidth
                    variant="contained"
                    color="primary"
                    type="submit"
                    sx={{ mt: 2 }}
                >
                    Login
                </Button>
            </Box>

            { error && (
                <Alert severity="error" sx={{ mt: 3 }}>
                    { error }
                </Alert>
            )}
        </Box>
    );
};

export default LoginPage;
