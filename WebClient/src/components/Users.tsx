import React, { useEffect } from "react";
import ViewForm from "./templates/ViewForm";
import { actionCreators } from "../store/account.store";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from 'react-router-dom';
import { role } from "../services/accounts.service";

export default function Accounts(props: any) {
    const dispatch = useDispatch()
    const { account } = useSelector((state: any) => state.account);
    const history = useHistory();

    useEffect(() => {
        dispatch(actionCreators.getAll())
        if (!account?.account) redirect()
    }, [])

    const redirect = () => {
        history.push("/");
    }

    const fieldNames = [
        "title",
        "firstName",
        "lastName",
        "role",
        "email",
    ]

    return (
        <div>
            { account?.account &&
                <>
                    <p>Users</p>
                    <ViewForm
                        fieldNames={fieldNames}
                        remove={actionCreators.remove}
                        update={actionCreators.update}
                        get={actionCreators.getById}
                        getAll={actionCreators.getAll}
                        data={account.users}
                        redirect={redirect}
                    />
                </>
            }

        </div>
    )
}
