import React, { useEffect } from "react"; 
import ViewForm from "./templates/ViewForm"; 
import {actionCreators} from "../store/account.store"; 
import { useDispatch, useSelector } from "react-redux";

export default function Accounts(props: any) {
    const dispatch = useDispatch()     
    const { account } = useSelector((state: any) => state.account); 
    
    useEffect(() => { 
         dispatch(actionCreators.getAll())
         console.log("account=",account)
    }, [])


    const fieldNames = [
        "title",
        "firstName",
        "lastName",
        "role",
        "email", 
    ]
 
    return (
        <div>
            <p>Users</p>
            <ViewForm
                fieldNames={fieldNames}
                remove={actionCreators.remove}
                update={actionCreators.update}
                get={actionCreators.getById}
                getAll={actionCreators.getAll} 
                data={account.users}
            />

        </div>
    )
}
