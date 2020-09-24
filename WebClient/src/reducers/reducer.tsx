import { initFrogsPond } from "./collection"

export default function reducer(state = initFrogsPond, action: any) {
 
    return {
        ...state, 
    }
}