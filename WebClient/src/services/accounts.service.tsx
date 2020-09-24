import axios from "axios";
import authHeader, { originHeader } from "./auth-header";

const API_URL = "http://localhost:5000/accounts/";

const getAll = () => {
    return axios.get(API_URL);
};

const getById = (id:any) => {
    return axios.get(API_URL + `/${id}`);
};

const register = (account: any) => {
    console.log("account=", account)
    return axios.post(API_URL + "register", account, { headers: originHeader })
        .then((response:any) => {
            return response.data
        });
};

const authenticate = (account: any) => {
    return axios.post(API_URL + "authenticate", account, { headers: originHeader })
        .then((response:any) => {
            if (response.data.accessToken) {
                localStorage.setItem("account", JSON.stringify(response.data));
            }

            return response.data;
        });
};
 
const create = (account: any) => {
    console.log(account)
    return axios.post(API_URL, account, { headers: authHeader() })
        .then((response: any) => {
            return response.data
        });
};

const update = (account: any, id:any) => {
    return axios.put(API_URL + `/${id}`, account, { headers: authHeader() })
        .then((response: any)  => {
            return response.data
        });
};

const remove = (id: any) => {
    return axios.delete(API_URL + `/${id}`, { headers: authHeader() })
        .then((response: any)  => {
            return response.data
        });
};

const logout = (dispatch: any) => {
    localStorage.removeItem("account");
    //dispatch({
    //    type: LOGOUT,
    //});
}

 

export default {
    getAll,
    register,
    authenticate,
    create,
    update,
    remove,
    logout,
    getById
};