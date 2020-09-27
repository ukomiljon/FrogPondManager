import { configureStore } from '@reduxjs/toolkit'
import { combineReducers } from 'redux'
 

import * as accountStore from "./account.store";
import * as frogStore from "./frog.store";

export interface IApplicationState {
    account: accountStore.IAccountStoreState;
    frog: frogStore.IFrogStoreState;
}

const reducer = combineReducers({
    account: accountStore.reducer,
    frog: frogStore.reducer
  }) 

const store = configureStore({
    reducer
})

export default store;