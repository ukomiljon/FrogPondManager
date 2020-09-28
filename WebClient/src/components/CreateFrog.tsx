import React from 'react'
import InputForm from './templates/InputForm';
import frogsService from '../services/frogs.service'
import { useHistory } from 'react-router-dom';

export default function CreateFrog(props: any) {
    const history = useHistory();
    const { mode, id } = props
    const fieldNames = [
        "Name",
        "Alife",
        "Age",
        "UserId"
    ]

    const redirect = () => {
        history.push("/");
    }

    return (
        <div>
            <InputForm
                fieldNames={fieldNames}
                post={frogsService.create}
                update={frogsService.update}
                get={frogsService.getById}
                mode={mode}
                id={id}
                redirect={redirect}
            />
        </div>
    )
}
