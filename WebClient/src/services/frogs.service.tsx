import axios from "axios";
import authHeader from "./auth-header";

const API_URL = "http://localhost:5000/frogs/";

const getAll = () => {
    return axios.get(API_URL + "all")
        .then(response => {
            return response.data
        })
};

const getByUserId = (userId: any) => {
    return axios.get(API_URL + "all")
        .then(response => {
            return response.data
        });
};

const getById = (userId: any) => {
    return axios.get(API_URL + "all")
        .then(response => {
            return response.data
        });
};

const create = (frog: any) => {
    return axios.post(API_URL + "user", frog, { headers: authHeader() })
        .then(response => {
            return response.data
        });
};

const update = (id: any, frog: any) => {
    return axios.put(API_URL + `/${id}`, frog, { headers: authHeader() })
        .then(response => {
            return response.data
        });
};

const remove = (id: any) => {
    return axios.delete(API_URL + `/${id}`, { headers: authHeader() })
        .then(response => {
            return response.data
        });
};

export default {
    getAll,
    getByUserId,
    getById,
    create,
    update,
    remove
};