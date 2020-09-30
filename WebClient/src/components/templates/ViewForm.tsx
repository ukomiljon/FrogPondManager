import React, { useEffect, useState } from 'react'
import { Table } from 'react-bootstrap'
import { useDispatch, useSelector } from 'react-redux'
import { useHistory } from 'react-router-dom'
import { BsPencil, BsTrash } from "react-icons/bs";
import { role } from '../../services/accounts.service';

export default function ViewForm(props: any) {

    const { fieldNames, remove, update, get, getAll, data, redirect } = props
    const dispatch = useDispatch()
    const { account } = useSelector((state: any) => state.account?.account);
    const history = useHistory();

    const deleteHandler = async (id: any) => {
        if (window.confirm("Are you sure to delete it?")) {
            if (remove) await dispatch(remove(id))
        }
    }

    const editHandler = (id: any) => {
        alert('Edit feature is not implemented yet!')
    }

    const createHandler = (id: any) => {
        history.push("/frogs");
    }

    return (
        <div>
            {account &&
                <>
                    <div> <button className="btn btn-secondary btn-sm" onClick={createHandler}  >  Create new </button></div>
                    <div> &nbsp;</div>
                </>
            }

            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#</th>

                        {
                            fieldNames?.map((fieldName: string[]) => <th>{fieldName}</th>)
                        }

                        {<td></td>}

                    </tr>
                </thead>
                <tbody>

                    {
                        data?.map((schema: any, index: number) =>
                            <tr key={schema.id}>
                                <td>{index + 1}</td>

                                {
                                    fieldNames?.map((fieldName: any) =>
                                        <td>{schema[fieldName]}</td>
                                    )
                                }

                                {
                                    <td align="center" width="10%">
                                        {(account?.role === role.admin || schema.userId === account?.id) && <>
                                            <BsPencil onClick={(id: any) => editHandler(schema.id)} /> <BsTrash onClick={(id: any) => deleteHandler(schema.id)} />
                                        </>
                                        }
                                    </td>
                                }

                            </tr>
                        )
                    }
                </tbody>
            </Table>
        </div>
    )
}
