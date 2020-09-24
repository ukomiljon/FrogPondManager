﻿import React, { useState, useEffect } from "react";
import frogsService from '../services/frogs.service'
import ViewForm from "./templates/ViewForm";
 
const Frogs = () => {
    const fieldNames = [
        "Name",
        "Alife",
        "Age",
        "UserId"
    ]

    return (
        <div>
            <ViewForm
                fieldNames={fieldNames}
                remove={frogsService.remove}
                update={frogsService.update}
                get={frogsService.getById}
                getAll={frogsService.getAll}
            />

        </div>
    )
}

export default Frogs;
