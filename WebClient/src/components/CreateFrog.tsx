import React, { useEffect, useState } from 'react' 
import InputForm from './templates/InputForm';
import frogsService from '../services/frogs.service'

export default function CreateFrog(props: any) {
    const { mode, id } = props
    const fieldNames = [
        "Name",
        "Alife",
        "Age",
        "UserId"
    ]

    return (
        <div>
            <InputForm
                fieldNames={fieldNames}
                post={frogsService.create}
                update={frogsService.update}
                get={frogsService.getById}
                mode={mode}
                id={id} />
        </div>
    )
}
