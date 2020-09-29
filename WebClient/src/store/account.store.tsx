import { createSlice, Dispatch, PayloadAction } from '@reduxjs/toolkit'
import accountsService from '../services/accounts.service';

export interface IAccountStoreState {
    account: {
        isFetching: boolean;
        isLoginSuccess: boolean;
        account: any;
        registerResponse: string;
        users:[]; 
    };
}

// Create the slice.
const slice = createSlice({
    name: "account",
    initialState: {
        account: {
            isFetching: false,
            isLoginSuccess: false,
            account: JSON.parse(localStorage.getItem('account')!),
            registerResponse: "",
            users:[]
        }
    } as IAccountStoreState,
    reducers: {
        setFetching: (state, action: PayloadAction<boolean>) => {
            state.account.isFetching = action.payload;
        },
        setSuccess: (state, action: PayloadAction<boolean>) => {
            state.account.isLoginSuccess = action.payload;
        },
        setAccount: (state, action: PayloadAction<any>) => {
            state.account.account = action.payload;
        },
        setRegisterResponse: (state, action: PayloadAction<string>) => {
            state.account.registerResponse = action.payload;
        }, 
        setUsers: (state, action: PayloadAction<[]>) => {
            state.account.users = action.payload;
        }
    }
});



export const { reducer } = slice;

export const actionCreators = {

    login: (model: any) => async (dispatch: Dispatch) => {

        dispatch(slice.actions.setFetching(true));

        const result = await accountsService.authenticate(model);

        if (!result.hasErrors) {
            localStorage.setItem('account', JSON.stringify(result.payload))
            dispatch(slice.actions.setSuccess(true));
            dispatch(slice.actions.setAccount(result.payload));
        }

        dispatch(slice.actions.setFetching(false));
    },

    logout: () => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setSuccess(false));
        localStorage.removeItem('account')
        dispatch(slice.actions.setAccount(null));
    },

    register: (model: any) => async (dispatch: Dispatch) => {

        const result = await accountsService.register(model);
        if (!result.hasErrors) {
            dispatch(slice.actions.setRegisterResponse(result.payload));
            return
        }

        dispatch(slice.actions.setRegisterResponse(result.payload));
    },

    update: (model: any) => async (dispatch: Dispatch) => {


    },

    getById: (id: any) => async (dispatch: Dispatch) => {


    },

    getAll: () => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));
        const result = await accountsService.getAll();
        if (!result.hasErrors) {
            dispatch(slice.actions.setUsers(result.payload));
            dispatch(slice.actions.setFetching(false)); 
        }  
    },
    delete: (id: any) => async (dispatch: Dispatch) => {


    },
};




//  register 
//  update 
//  getById 