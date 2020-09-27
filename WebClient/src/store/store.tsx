import { createStore } from "redux"; 
import { createSlice } from '@reduxjs/toolkit'
import reducer from "../reducers/reducer"; 

export const store = createStore(reducer);
 