import { createSlice, PayloadAction, Dispatch } from '@reduxjs/toolkit';
import frogsService from '../services/frogs.service';
 
export interface IFrogStoreState {
    frog: {
        isFetching: boolean;
        frogs: any[];
    }
}

// Create the slice.
const slice = createSlice({
    name: "frog",
    initialState: {
        frog: {
            isFetching: false,
            frogs: []
        }
    } as IFrogStoreState,
    reducers: {
        setFetching: (state, action: PayloadAction<boolean>) => {
            state.frog.isFetching = action.payload;
        },
        setData: (state, action: PayloadAction<[]>) => {
            state.frog.frogs = action.payload;
        },
        addData: (state, action: PayloadAction<any>) => {
            state.frog.frogs = [...state.frog.frogs, action.payload];
        },

    }
});

// Export reducer from the slice.
export const { reducer } = slice;

// Define action creators.
export const actionCreators = {
    get: (id: string) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        const result = await frogsService.getById(id);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.payload));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    getAll: () => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        const result = await frogsService.getAll();

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.payload));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    getByUserId: (userId: any) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        const result = await frogsService.getByUserId(userId);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.payload));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    create: (model: any) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        await frogsService.create(model);
        var account = JSON.parse(localStorage.getItem('account')!)
        const result = await frogsService.getByUserId(account.id);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.payload));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    update: (model: any) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        await frogsService.update(model);
        var account = JSON.parse(localStorage.getItem('account')!)
        const result = await frogsService.getByUserId(account.id);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.payload));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    delete: (id: any) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));

        await frogsService.remove(id);
        var account = JSON.parse(localStorage.getItem('account')!)
        const result = await frogsService.getByUserId(account.id);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.payload));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    }
};

