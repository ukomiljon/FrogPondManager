import React from "react";
import InputForm from "./templates/InputForm"; 
import accountsService from '../services/accounts.service'
import 'bootstrap/dist/css/bootstrap.min.css'; 

export default function Register(props: any) {
    const { mode, id } = props
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
            <p><h2>Register</h2></p>
            <InputForm
                fieldNames={fieldNames}
                post={accountsService.register}
                update={accountsService.update}
                get={accountsService.getById}
                mode={mode}
                id={id} />

        </div>
    )
}
