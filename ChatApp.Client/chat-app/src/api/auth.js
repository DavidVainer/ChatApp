import axios from 'axios';

const API_BASE_URL = "https://localhost:7159/api/auth";

export const loginUser = async (email, password) => {
    const response = await axios.post(`${API_BASE_URL}/login`, { email, password});
    return response.data;
};