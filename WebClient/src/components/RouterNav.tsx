import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

import "bootstrap/dist/css/bootstrap.min.css";
import ".././App.css";
import accountsService from "../services/accounts.service";
import Frogs from "./Frogs";
import Login from "./Login";
import Register from "./Register";
import Profile from "./Profile";
import Users from "./Users";




export default function RouterNav() {

    const [showFrogs] = useState(false);

    const { user: currentUser } = useSelector((state: any) => state.auth);
    const dispatch = useDispatch();


    useEffect(() => {

        // showFrogs(currentUser.roles.includes("ROLE_ADMIN"));

    }, [currentUser]);

    const logOut = () => {
        accountsService.logout(dispatch)
    };

    return (
        <Router >
            <div>
                <nav className="navbar navbar-expand navbar-dark bg-dark">
                    <Link to={"/"} className="navbar-brand">
                        FrogsPond
      </Link>
                    <div className="navbar-nav mr-auto">

                        <li className="nav-item">
                            <Link to={"/home"} className="nav-link">
                                Home
          </Link>
                        </li>

                        {currentUser && (
                            <li className="nav-item">
                                <Link to={"/user"} className="nav-link">
                                    Users
            </Link>
                            </li>
                        )}
                    </div>

                    {currentUser ? (
                        <div className="navbar-nav ml-auto">
                            <li className="nav-item">
                                <Link to={"/profile"} className="nav-link">
                                    {currentUser.username}
                                </Link>
                            </li>
                            <li className="nav-item">
                                <a href="/login" className="nav-link" onClick={logOut}>
                                    LogOut
            </a>
                            </li>
                        </div>
                    ) : (
                            <div className="navbar-nav ml-auto">
                                <li className="nav-item">
                                    <Link to={"/login"} className="nav-link">
                                        Login
            </Link>
                                </li>

                                <li className="nav-item">
                                    <Link to={"/register"} className="nav-link">
                                        Sign Up
            </Link>
                                </li>
                            </div>
                        )}
                </nav>

                <div className="container mt-3">
                    <Switch>
                        <Route exact path={["/", "/home"]} component={Frogs} />
                        <Route exact path="/login" component={Login} />
                        <Route exact path="/register" component={Register} />
                        <Route exact path="/profile" component={Profile} />
                        <Route path="/user" component={Users} /> 
                    </Switch>
                </div>
            </div>
        </Router>
    )
}
