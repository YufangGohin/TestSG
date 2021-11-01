import * as React from "react";
import { Form } from "react-bootstrap";

export default function TextInput({ handleChange, placeholder }) {
  const onChange = (event) => {
    handleChange(event.target.value);
  };
  return (
    <div>
      <Form.Control type="text" onChange={onChange} placeholder={placeholder} />
    </div>
  );
}
