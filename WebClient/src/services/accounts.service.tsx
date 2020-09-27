import axios from "axios";
import { store } from "../store/store";
import authHeader, { originHeader } from "./auth-header";

const API_URL = "http://localhost:5000/accounts/";

const getAll = () => {
  return axios.get(API_URL);
};

const getById = (id: any) => {
  return axios.get(API_URL + `/${id}`);
};

const register = (account: any) => {
  console.log("account=", account);
  return axios
    .post(API_URL + "register", account, { headers: originHeader })
    .then((response: any) => {
      return response.data;
    });
};


// {created:'2020-09-26T14:45:37.759Z'
// email:'kk1@gmail.com'
// firstName:'Komiljon'
// id:'5f6f5419ae40515ae8eee3b2'
// isVerified:true
// jwtToken:'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjVmNmY1NDE5YWU0MDUxNWFlOGVlZTNiMiIsIm5iZiI6MTYwMTEzNzI5NiwiZXhwIjoxNjAxMTM4MTk2LCJpYXQiOjE2MDExMzcyOTZ9.nA84NyuwVuKWZKEQmsDJduFD11foDP9RQs8hE2MTYNo'
// lastName:'Usarov'
// role:'User'
// title:'Mr.'}
const authenticate = (account: any) => {
  return axios
    .post(API_URL + "authenticate", account, { headers: originHeader })
    .then((response: any) => {
      
      store.dispatch({ type: 'INCREMENT' })

      return response.data;
    });
};

const create = (account: any) => {
  console.log(account);
  return axios
    .post(API_URL, account, { headers: authHeader() })
    .then((response: any) => {
      return response.data;
    });
};

const update = (account: any, id: any) => {
  return axios
    .put(API_URL + `/${id}`, account, { headers: authHeader() })
    .then((response: any) => {
      return response.data;
    });
};

const remove = (id: any) => {
  return axios
    .delete(API_URL + `/${id}`, { headers: authHeader() })
    .then((response: any) => {
      return response.data;
    });
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
  logout,
  getById
};
