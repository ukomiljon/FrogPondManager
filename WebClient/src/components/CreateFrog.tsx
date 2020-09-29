import React from 'react'
import InputForm from './templates/InputForm';
import { useHistory } from 'react-router-dom';
import { actionCreators } from "../store/frog.store";

export default function CreateFrog(props: any) {
    const history = useHistory();
    const { mode, id } = props
    const fieldNames = [
        "name",
        "type",
        "color", 
    ]

    const redirect = () => {
        history.push("/");
    }

    return (
        <div>
            <InputForm
                fieldNames={fieldNames}
                post={actionCreators.create}
                update={actionCreators.update}
                get={actionCreators.getAll}
                mode={mode}
                id={id}
                redirect={redirect}
            />
        </div>
    )
}
