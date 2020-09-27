import React, { useEffect } from "react"; 
import ViewForm from "./templates/ViewForm"; 
import {actionCreators} from "../store/account.store"; 
import { useDispatch, useSelector } from "react-redux";

export default function Accounts(props: any) {
    const dispatch = useDispatch()     
    const { account } = useSelector((state: any) => state.account); 
    
    useEffect(() => { 
         dispatch(actionCreators.getAll())
         console.log(account)
    }, [])


    const fieldNames = [
        "Title",
        "FirstName",
        "LastName",
        "Role",
        "Email",
        "Password",
        "ConfirmPassword"
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
