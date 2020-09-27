import axios from "axios";
import authHeader, { originHeader } from "./auth-header";

const API_URL = "http://localhost:5000/accounts/";

const getAll = () => {
  return axios.get(API_URL, { headers: authHeader() }).then((response: any) => {
    return { payload: response.data, hasErrors: false }
  }).catch((error: any) => {
    return { payload: error.response, hasErrors: true, errorMessage: error }
  });
};

const getById = (id: any) => {
  return axios.get(API_URL + `/${id}`).then((response: any) => {
    return { payload: response.data, hasErrors: false }
  }).catch((error: any) => {
    return { payload: error.response, hasErrors: true, errorMessage: error }
  });;
};

const register = (account: any) => {
  return axios
    .post(API_URL + "register", account)
    .then((response: any) => {
      return { payload: response.data.message, hasErrors: false }
    }).catch((error: any) => {
      return { payload: error.response.data.title, hasErrors: true, errorMessage: error }
    });
}

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
      return { payload: response.data, hasErrors: false }
    }).catch((error: any) => {
      console.log(error)
      return { payload: error.response, hasErrors: true, errorMessage: error }
    });
};

const create = (account: any) => {
  return axios
    .post(API_URL, account, { headers: authHeader() })
    .then((response: any) => {
      return { payload: response.data, hasErrors: false }
    }).catch((error: any) => {
      return { payload: error.response.data.title, hasErrors: true, errorMessage: error }
    });;
};

const update = (account: any, id: any) => {
  return axios
    .put(API_URL + `/${id}`, account, { headers: authHeader() })
    .then((response: any) => {
      return response.data;
    }).catch((error: any) => {
      return { payload: error.response.data.title, hasErrors: true, errorMessage: error }
    });;
};

const remove = (id: any) => {
  return axios
    .delete(API_URL + `/${id}`, { headers: authHeader() })
    .then((response: any) => {
      return response.data;
    }).catch((error: any) => {
      return { payload: error.response.data.title, hasErrors: true, errorMessage: error }
    });;
};



export default {
  getAll,
  register,
  authenticate,
  create,
  update,
  remove,
  getById
};
