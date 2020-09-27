import React from "react";
import InputForm from "./templates/InputForm";
import 'bootstrap/dist/css/bootstrap.min.css';
import { useSelector } from "react-redux";
import {actionCreators} from "../store/account.store";


export default function Register(props: any) {
    const { mode, id } = props
    const { account } = useSelector((state: any) => state.account);

    const fieldNames = [
        "Title",
        "FirstName",
        "LastName",
        "Email",
        "Password",
        "ConfirmPassword",
        "AcceptTerms true/false" 
    ]

    return (
        <div>
            <p><h2>Register</h2></p>
            <p>{account?.registerResponse}</p>
            <InputForm
                fieldNames={fieldNames}
                post={actionCreators.register}
                //update={accountsService.update}
                //get={accountsService.getById}
                mode={mode}
                id={id}
            />



        </div>
    )
}
