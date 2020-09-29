import React, { useEffect, useState } from "react";
import { Button, Col, Form, Row } from 'react-bootstrap'
import { useDispatch, useSelector } from "react-redux";
import "bootstrap/dist/css/bootstrap.min.css";
import { modeType } from "./TemplateMode";

export default function InputForm(props: any) {

  const { fieldNames, post, update, get, mode, id, redirect } = props;
  const [data, setData] = useState({});
  const dispatch = useDispatch();

  useEffect(() => {
    if (mode === modeType.edit) {
      get(id).then((responseData: any) => setData(responseData));
    }
  }, []);

  const submitData = async (e: any) => {
    e.preventDefault();

    if (mode === modeType.edit) {
      if (update) dispatch(update(data))
      return;
    }

    if (post) dispatch(post(data))
    if (redirect) redirect()
    setData({})
  };

  const onChangeInput = (e: any) => {
    let value = e.target.value;
    if (value === "true") value = true;
    setData({
      ...data,
      [e.target.id]: value
    });
  };

  const readFiledsByKey = (key: any, data: any) => {
    return data[key];
  };

  return (
    <div>
      <Form>
        {fieldNames.map((fieldName: any) => {
          let inputType = "text";
          if (fieldName === "password") inputType = "password";

          return (
            <Form.Group as={Row}>
              <Form.Label column sm="2">
                {fieldName}:
              </Form.Label>
              <Col sm="10">
                <Form.Control
                  type={inputType}
                  placeholder={"Enter " + fieldName}
                  onChange={onChangeInput}
                  id={fieldName}
                  value={data ? readFiledsByKey(fieldName, data) : ""}
                />
              </Col>
            </Form.Group>
          );
        })}

        <Button variant="primary" type="submit" onClick={submitData}>
          {" "}
          Submit{" "}
        </Button>
      </Form>
    </div>
  );
}
