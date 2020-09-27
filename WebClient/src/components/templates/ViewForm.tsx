import React, { useEffect, useState } from 'react'
import { Table } from 'react-bootstrap'
import { useDispatch, useSelector } from 'react-redux'
 
 
export default function ViewForm(props: any) {

    const { fieldNames, remove, update, get, getAll } = props
    const dispatch = useDispatch()
    const [data, setData] = useState([])

    useEffect(() => {
        getAll().then(
            (data: any) => {
                setData(data);
            },
        ).catch(
            function (error: any) {
                console.log(error)
            }
        );

    }, [])


    const deleteRow = (id: any) => {
        remove(id)
    }

    const editHandler = (schemaId: any) => {
       
    }

    return (
        <div>
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
                            <tr key={schema._id}>
                                <td>{index + 1}</td>

                                {
                                    fieldNames?.map((fieldName: any) =>
                                        <td>{schema[fieldName]}</td>
                                    )

                                }

                                {
                                    <td>
                                        <button className="btn btn-secondary btn-sm" onClick={(id: any) => editHandler(schema._id)} >  Change </button> &nbsp;
                                     <button className="btn btn-danger btn-sm" type="submit" onClick={(id: any) => deleteRow(schema._id)}>  Delete </button>
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
