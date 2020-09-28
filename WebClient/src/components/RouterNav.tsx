import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { BrowserRouter as Router, Switch, Route, Link, useHistory } from "react-router-dom";

import ".././App.css";
import Frogs from "./Frogs";
import Login from "./Login";
import Register from "./Register";
import Profile from "./Profile";
import Users from "./Users";
import { actionCreators } from "../store/account.store";
import { role } from "../services/accounts.service";
import CreateFrog from "./CreateFrog";

export default function RouterNav() {

    const dispatch = useDispatch()
    const { account } = useSelector((state: any) => state.account?.account);
    const history = useHistory();

    const logOut = (e: any) => {
        e.preventDefault();
        dispatch(actionCreators.logout())  
        //redirect()
    };

    const redirect = () => {
        history.push("/home");
    }

   

    return (
        <Router >
            <div>
                <nav className="navbar navbar-expand navbar-dark bg-dark">
                    <Link to={"/"} className="navbar-brand">FrogsPond</Link>
                    <div className="navbar-nav mr-auto">
                        <li className="nav-item">
                            <Link to={"/home"} className="nav-link">Home</Link>
                        </li>

                        {account && account.role === role.admin && (
                            <li className="nav-item">
                                <Link to={"/user"} className="nav-link">
                                    Users
                                 </Link>
                            </li>
                        )}
                    </div>

                    {account ? (
                        <div className="navbar-nav ml-auto">
                            <li className="nav-item">
                                <Link to={"/profile"} className="nav-link">
                                    {account.username}
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
                        <Route exact path={["/", "/frogs"]} component={CreateFrog} />
                        <Route path="/user" component={Users} />
                    </Switch>
                </div>
            </div>
        </Router>
    )
}
