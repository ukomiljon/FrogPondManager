import { createSlice, PayloadAction, Dispatch } from '@reduxjs/toolkit';
import frogsService from '../services/frogs.service';

export interface IFrogStoreState {
    isFetching: boolean;
    frogs: any[];
} 

// Create the slice.
const slice = createSlice({
    name: "frog",
    initialState: {
        isFetching: false,
        frogs: []
    } as IFrogStoreState,
    reducers: {
        setFetching: (state, action: PayloadAction<boolean>) => {
            state.isFetching = action.payload;
        },
        setData: (state, action: PayloadAction<any[]>) => {
            state.frogs = action.payload;
        },
        addData: (state, action: PayloadAction<any>) => {
            state.frogs = [...state.frogs, action.payload];
        },
        updateData: (state, action: PayloadAction<any>) => {
            // We need to clone collection (Redux-way).
            var collection = [...state.frogs];
            var entry = collection.find(x => x.id === action.payload.id);
            entry.firstName = action.payload.firstName;
            entry.lastName = action.payload.lastName;
            state.frogs = [...state.frogs];
        },
        deleteData: (state, action: PayloadAction<{ id: number }>) => {
            state.frogs = state.frogs.filter(x => x.id !== action.payload.id);
        }
    }
});

// Export reducer from the slice.
export const { reducer } = slice;

// Define action creators.
export const actionCreators = {
    search: (term?: string) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));
         
        const result = await frogsService.getByUserId(term);

        if (!result.hasErrors) {
            dispatch(slice.actions.setData(result.value));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    add: (model: any) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true));
 
        const result = await frogsService.create(model);

        if (!result.hasErrors) {
            model.id = result.value;
            dispatch(slice.actions.addData(model));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    update: (model: any) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true)); 

        const result = await frogsService.update(model);

        if (!result.hasErrors) {
            dispatch(slice.actions.updateData(model));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    },
    delete: (id: any) => async (dispatch: Dispatch) => {
        dispatch(slice.actions.setFetching(true)); 
       
        const result = await frogsService.remove(id);

        if (!result.hasErrors) {
            dispatch(slice.actions.deleteData({ id }));
        }

        dispatch(slice.actions.setFetching(false));

        return result;
    }
};
