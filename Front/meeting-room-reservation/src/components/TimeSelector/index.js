import React, { useState } from "react";
import Select from "../Select";
import Alert from "../Alert";

export default function TimeSelector({ handleChange }) {
  const [beginHour, setBeginHour] = useState("");
  const [endHour, setEndHour] = useState("");
  const [open, setOpen] = useState(false);

  const handleChangeBegin = (hour) => {
    setBeginHour(hour);
    handleChange(`${hour} - ${endHour}`);
    setOpen(hour >= endHour);
  };

  const handleChangeEnd = (hour) => {
    setEndHour(hour);
    handleChange(`${beginHour}-${hour}`);
    setOpen(beginHour >= hour);
  };

  const hourArray = Array.from({ length: 24 }, (x, i) => {
    return (i > 9 ? i : "0" + i) + ":00";
  });

  return (
    <div class="row row-cols-2">
      <Select
        class="col"
        array={hourArray}
        time
        handleChange={handleChangeBegin}
        initialItem="00:00"
      >
        Begin Time
      </Select>
      <Select
        class="col"
        array={hourArray}
        time
        handleChange={handleChangeEnd}
        initialItem="00:00"
      >
        End Time
      </Select>
      <Alert open={open} setOpen={setOpen} alertType="danger">
        End time should be greater than begin time !
      </Alert>
    </div>
  );
}
