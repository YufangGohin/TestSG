import React from "react";
import { Form } from "react-bootstrap";

export default function NumberSelect({ children, handleChange, array }) {
  const onChange = (event) => {
    handleChange(event.target.value);
  };

  return (
    <div>
      <Form.Select aria-label="Default select example" onChange={onChange}>
        <option>{children}</option>
        {array.map((item) => (
          <option value={item}>{item}</option>
        ))}
      </Form.Select>
    </div>
  );
}
