import axios from "axios";
import authHeader from "./auth-header";

const API_URL = "http://localhost:5000/frogs/";

const getAll = () => {
    return axios.get(API_URL)
    .then((response: any) => {
      return { payload: response.data, hasErrors: false }
    }).catch((error: any) => {
      return { payload: error.response, hasErrors: true, errorMessage: error }
    });
};

const getByUserId = (userId: any) => {
    return axios.get(API_URL + `/${userId}`)
    .then((response: any) => {
      return { payload: response.data, hasErrors: false }
    }).catch((error: any) => {
      return { payload: error.response, hasErrors: true, errorMessage: error }
    });
};

const getById = (id: any) => {
    return axios.get(API_URL + `/${id}`)
    .then((response: any) => {
        return { payload: response.data, hasErrors: false }
      }).catch((error: any) => {
        return { payload: error.response, hasErrors: true, errorMessage: error }
      });
};

const create = (frog: any) => {
    return axios.post(API_URL , frog, { headers: authHeader() })
    .then((response: any) => {
        return { payload: response.data, hasErrors: false }
      }).catch((error: any) => {
        return { payload: error.response, hasErrors: true, errorMessage: error }
      });
};

const update = (frog: any) => {
    return axios.put(API_URL, frog, { headers: authHeader() })
    .then((response: any) => {
        return { payload: response.data, hasErrors: false }
      }).catch((error: any) => {
        return { payload: error.response, hasErrors: true, errorMessage: error }
      });
};

const remove = (id: any) => {
    return axios.delete(API_URL + `${id}`, { headers: authHeader() })
    .then((response: any) => {
        return { payload: response.data, hasErrors: false }
      }).catch((error: any) => {
        return { payload: error.response, hasErrors: true, errorMessage: error }
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