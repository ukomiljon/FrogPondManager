import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import ViewForm from "./templates/ViewForm";
import { actionCreators } from "../store/frog.store";
import { role } from "../services/accounts.service";


const Frogs = () => {
    const dispatch = useDispatch()
    const { frog } = useSelector((state: any) => state.frog);
    const { account } = useSelector((state: any) => state.account);
    const history = useHistory();

    useEffect(() => {
        dispatch(actionCreators.getAll())
    }, [])

    const redirect = () => {
        history.push("/");
    }

    const fieldNames = [
        "name",
        "type",
        "color", 
    ]

    return (
        <div>
            <p>List of Frogs</p>
            <ViewForm
                fieldNames={fieldNames}
                remove={actionCreators.remove}
                update={actionCreators.update}
                getAll={actionCreators.getAll}
                data={frog?.frogs}
            />

        </div>
    )
}

export default Frogs;
