import React from "react";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import { Box, Button, Typography, AppBar, Toolbar } from "@mui/material";

const Layout = () => {
    const location = useLocation();
    const navigate = useNavigate();

    const showLogoutButton = location.pathname === "/rooms";

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("userId");

        navigate("/login");
    };

    return (
        <Box>
            <AppBar position="static" color="primary">
                <Toolbar sx={{
                        display: "flex",
                        justifyContent: "space-between",
                    }}
                >
                    <Typography variant="h4">Chat App</Typography>

                    {showLogoutButton && (
                        <Button variant="contained" color="secondary" onClick={handleLogout}>
                            Logout
                        </Button>
                    )}
                </Toolbar>
            </AppBar>
            <Box flex="1" p={5} margin={"auto"}>
                <Outlet />
            </Box>
        </Box>
    );
};

export default Layout;
