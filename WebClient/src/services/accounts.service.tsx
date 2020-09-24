import axios from "axios";
import authHeader, { originHeader } from "./auth-header";

const API_URL = "http://localhost:5000/accounts/";

const getAll = () => {
    return axios.get(API_URL);
};

const register = (account:any) => {
    return axios.post(API_URL + "register", account, { headers: originHeader  });
};

const authenticate = (account: any) => {
    return axios.post(API_URL + "authenticate", account, { headers: originHeader })
        .then((response) => {
            if (response.data.accessToken) {
                localStorage.setItem("account", JSON.stringify(response.data));
            }

            return response.data;
        });
};
 
const create = (account: any) => {
    return axios.post(API_URL, account, { headers: authHeader() });
};

const update = (account: any, id:any) => {
    return axios.put(API_URL + `/${id}`, account,{ headers: authHeader() });
};

const remove = (id: any) => {
    return axios.delete(API_URL + `/${id}`, { headers: authHeader() });
};

const logout = (dispatch: any) => {
    localStorage.removeItem("account");
    //dispatch({
    //    type: LOGOUT,
    //});
};

 

export default {
    getAll,
    register,
    authenticate,
    create,
    update,
    remove,
    logout
};