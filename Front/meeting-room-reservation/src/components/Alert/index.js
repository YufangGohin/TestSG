// import * as React from 'react';
import * as React from "react";
import { Alert } from "react-bootstrap";

export default function DescriptionAlerts({
  open,
  setOpen,
  alertType,
  children,
}) {
  if (open) {
    return (
      <Alert
        variant={alertType}
        onClose={() => setOpen(false)}
        dismissible
        className="Alert"
      >
        {children}
      </Alert>
    );
  }
  return <></>;
}
