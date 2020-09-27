import React from 'react'
import InputForm from './templates/InputForm';
import 'bootstrap/dist/css/bootstrap.min.css';
import { actionCreators } from "../store/account.store";
import { useHistory } from 'react-router-dom';

export default function Login() {
    const history = useHistory();

    const fieldNames = [
        "Email",
        "Password"
    ]

    const redirection = () => {
        history.push("/");
    }

    return (
        <div>
            <p><h2>Login</h2></p>
            <InputForm
                fieldNames={fieldNames}
                post={actionCreators.login}
                redirection={redirection}
            />
        </div>
    )
}
