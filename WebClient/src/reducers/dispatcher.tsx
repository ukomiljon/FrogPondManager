import { store } from "../store/store"
import { actionType } from "./actions"


export function cleanFrogs() {
    store.dispatch({ type: actionType.CLEAN_FROGS })
}

export function cleanUsers() {
    store.dispatch({ type: actionType.CLEAN_ACCOUNTS })
}

export default function saveToRedux(data: any, dispatch: any) {

    //data.map((item: any) =>null
    //    dispatch({ type: ActionType.ADD_REPORT, item: { ...item } })
    //) 
}

