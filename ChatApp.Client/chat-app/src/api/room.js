import axios from "axios";

const BASE_URL = "http://localhost:8080/api/rooms";

const api = axios.create({ baseURL: BASE_URL });

api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
}, (error) => Promise.reject(error));

export const getActiveRooms = async () => {
    const response = await api.get("/active");
    return response.data;
};

export const getRoomDetails = async (id) => {
    const response = await api.get(`/${id}/details`);
    return response.data;
};