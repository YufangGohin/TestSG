import * as React from "react";
import { Button } from "react-bootstrap";

export default function CustomButton({ onClick, children }) {
  return (
    <Button variant="outline-primary" onClick={onClick} style={{ width: 80 }}>
      {children}
    </Button>
  );
}
