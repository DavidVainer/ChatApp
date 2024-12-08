import React, { useEffect } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { jwtDecode } from "jwt-decode";

import LoginPage from "./components/LoginPage";
import PrivateRoute from "./routes/PrivateRoute";
import RoomsDashboard from "./components/RoomsDashboard";
import ChatRoom from "./components/ChatRoom";
import Layout from "./components/Layout";

const checkJwtExpiry = () => {
    const token = localStorage.getItem("token");

    if (token) {
        try {
            const decodedToken = jwtDecode(token);
            const isExpired = decodedToken.exp * 1000 < Date.now();

            if (isExpired) {
                localStorage.removeItem("token");
                localStorage.removeItem("userId");
                return true;
            }
        } catch (error) {
            console.error("Invalid token format:", error);
            localStorage.removeItem("token");
            localStorage.removeItem("userId");
        }
    }
    return false;
};

const App = () => {
    useEffect(() => {
        if (checkJwtExpiry()) {
            alert("Your session has expired, please log in again.");
        }
    }, []);

    return (
        <Router>
            <Routes>
                <Route path="/login" element={<LoginPage />} />

                <Route path="/" element={
                    <PrivateRoute>
                        <Layout />
                    </PrivateRoute>
                }>
                    <Route path="/rooms" element={
                        <PrivateRoute>
                            <RoomsDashboard />
                        </PrivateRoute>
                        }
                    />
                    <Route path="/rooms/:roomId" element={
                        <PrivateRoute>
                            <ChatRoom />
                        </PrivateRoute>
                        }
                    />
                />
                </Route>
            </Routes>
        </Router>
    );
};

export default App;
