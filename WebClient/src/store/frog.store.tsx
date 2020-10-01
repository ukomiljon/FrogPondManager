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
        updateData: (state) => {
           //actionCreators.getAll()
        },
    }
});

// Export reducer from the slice.
export const { reducer } = slice;

// Define action creators.
export const actionCreators = { 
    getAll: () => async (dispatch: Dispatch) => {
        await dispatch(slice.actions.setFetching(true));

        const result = await frogsService.getAll(); 
        console.log("getAll=",result)
       
        if (!result.hasErrors) { 
            await dispatch(slice.actions.setData(result.payload));
        }

        await dispatch(slice.actions.setFetching(false));

        return result;
    },
    create: (model: any) => async (dispatch: Dispatch) => {
        await dispatch(slice.actions.setFetching(true));

        const result = await frogsService.create(model); 
        
        if (!result.hasErrors) {  
            await dispatch(slice.actions.updateData());
        }

        await dispatch(slice.actions.setFetching(false));

        return result;
    },
    update: (model: any) => async (dispatch: Dispatch) => {
        await dispatch(slice.actions.setFetching(true));

        const result = await frogsService.update(model);
        
        if (!result.hasErrors) {
            await dispatch(slice.actions.updateData());
        }

        await dispatch(slice.actions.setFetching(false));

        return result;
    },
    remove: (id: any) => async (dispatch: Dispatch) => {
        await dispatch(slice.actions.setFetching(true));

        const result = await frogsService.remove(id); 
        
        if (!result.hasErrors) {  
            console.log(result)
            await dispatch(slice.actions.updateData());
        }

        await dispatch(slice.actions.setFetching(false));

        return result;
    }, 
};

