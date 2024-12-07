import axios from "axios";

const BASE_URL = "https://localhost:8081/api/rooms";

const api = axios.create({ baseURL: BASE_URL });

api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
}, (error) => Promise.reject(error));

export const getAllRooms = async () => {
    const response = await api.get("/");
    return response.data;
};

export const getRoomDetails = async (id) => {
    const response = await api.get(`/${id}/details`);
    return response.data;
};

export const createRoom = async (room) => {
    const response = await api.post("/", room);
    return response.data;
};

export const deleteRoom = async (id) => {
    await api.delete(`/${id}`);
};
