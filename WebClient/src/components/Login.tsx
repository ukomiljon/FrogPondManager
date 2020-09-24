import React, { useEffect, useState } from 'react'  
import InputForm from './templates/InputForm'; 
import accountsService from '../services/accounts.service'
import 'bootstrap/dist/css/bootstrap.min.css'; 

export default function Login() {

    const fieldNames = [
        "Email",
        "Password"
    ]

    return (
        <div>
            <p><h2>Login</h2></p>
            <InputForm
                fieldNames={fieldNames}
                post={accountsService.authenticate}
            />
        </div>
    )
}
