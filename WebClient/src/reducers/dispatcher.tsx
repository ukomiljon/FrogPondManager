import { actionType } from "./actions"


export function cleanFrogs(dispatch: any) {
    dispatch({ type: actionType.CLEAN_FROGS })
}

export function cleanUsers(dispatch: any) {
    dispatch({ type: actionType.CLEAN_ACCOUNTS })
}

export default function saveToRedux(data: any, dispatch: any) {

    //data.map((item: any) =>null
    //    dispatch({ type: ActionType.ADD_REPORT, item: { ...item } })
    //) 
}

