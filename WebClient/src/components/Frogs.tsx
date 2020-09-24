import React, { useState, useEffect } from "react";
import frogsService from '../services/frogs.service'
 
const Frogs = () => {
    const [frogs, setFrogs] = useState("");
   
    useEffect(() => {
        frogsService.getAll().then(
            (data) => {
                setFrogs(data);
                console.log("frogs=", data)
            }, 
        );
    }, []);

    return (
        <div className="container">
            <header className="jumbotron">
                <h3>{frogs}</h3>
            </header>
        </div>
    );
};

export default Frogs;
