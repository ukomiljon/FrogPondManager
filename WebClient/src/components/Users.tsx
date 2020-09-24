import React from "react"; 
import ViewForm from "./templates/ViewForm";
import accountsService from "../services/accounts.service";
 

export default function Accounts(props: any) {

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
            <ViewForm
                fieldNames={fieldNames}
                remove={accountsService.remove}
                update={accountsService.update}
                get={accountsService.getById}
                getAll={accountsService.getAll}
            />

        </div>
    )
}
