import React, { useEffect, useState } from 'react'
//import { Button, Col, Form, Row } from 'react-bootstrap'
import Button from 'react-bootstrap/Button';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import { useDispatch, useSelector } from 'react-redux'
import { modeType } from '../../reducers/actions'
import 'bootstrap/dist/css/bootstrap.min.css'; 

export default function InputForm(props: any) {
    const editMode = useSelector((state: any) => state.editMode)
    const { fieldNames, post, update, get, mode, id } = props
    const [data, setData] = useState({})
    const dispatch = useDispatch()

    useEffect(() => {

        if (mode === modeType.edit) {
            get(id).then((data:any) => {
                setData(data) 
            })
        }

    }, [])

    const submitData = (e: any) => {
        e.preventDefault()

        if (mode === modeType.edit) {
            update(data)
            return
        }

        post(data)
    }

    const onChangeInput = (e: any) => {

        setData(
            {
                ...data,
                [e.target.id]: e.target.value
            })
    }

    const readFiledsByKey = (key: any, data: any) => {
        return data[key]
    }

    return (
        <div>

            <Form>
                {
                    fieldNames.map((fieldName: any) => {

                        let inputType = "text"
                        if (fieldName == "password") inputType = "password"

                        return <Form.Group as={Row}  >
                            <Form.Label column sm="2">
                                {fieldName}:
                        </Form.Label>
                            <Col sm="10">
                                <Form.Control type={inputType} placeholder={"Enter " + fieldName}
                                    onChange={onChangeInput}
                                    id={fieldName}
                                    value={data ? readFiledsByKey(fieldName, data) : ""}
                                />
                            </Col>
                        </Form.Group>
                    })
                }

                <Button variant="primary" type="submit" onClick={submitData}> Submit  </Button>
            </Form>
        </div>
    )
}
