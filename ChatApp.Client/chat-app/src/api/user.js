import axios from "axios";

const BASE_URL = "https://localhost:8081/api/users";

const api = axios.create({ baseURL: BASE_URL });

api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
}, (error) => Promise.reject(error));

export const getAllUsers = async () => {
    const response = await api.get("/");
    return response.data;
}

export const addUser = async (user) => {
    const response = await api.post("/", user);
    return response.data;
}

export const deleteUser = async (id) => {
    await api.delete(`/${id}`);
}