import axios from "axios";
import authHeader from "./auth-header";

const API_URL = "http://localhost:5000/frogs/";

const getAll = () => {
    return axios.get(API_URL)
        .then((response:any) => {
            return response.data
        })
};

const getByUserId = (userId: any) => {
    return axios.get(API_URL + `/${userId}`)
        .then((response: any) => {
            return response.data
        });
};

const getById = (id: any) => {
    return axios.get(API_URL + `/${id}`)
        .then((response: any) => {
            return response.data
        });
};

const create = (frog: any) => {
    return axios.post(API_URL , frog, { headers: authHeader() })
        .then((response: any) => {
            return response.data
        });
};

const update = (frog: any) => {
    return axios.put(API_URL, frog, { headers: authHeader() })
        .then((response: any) => {
            return response.data
        });
};

const remove = (id: any) => {
    return axios.delete(API_URL + `/${id}`, { headers: authHeader() })
        .then((response: any) => {
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