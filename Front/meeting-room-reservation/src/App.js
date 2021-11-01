import React, { useEffect, useState } from "react";
import axios from "axios";
import Button from "./components/Button";
import Select from "./components/Select";
import TimeSelector from "./components/TimeSelector";
import Alert from "./components/Alert";
import { Table } from "react-bootstrap";
import Text from "./components/Text";
import "./App.css";

const baseURL = "https://localhost:44311/Reservation";

const App = (props) => {
  const [reservations, setReservations] = useState([]);
  const [timeRange, setTimeRange] = useState("");
  const [roomNumber, setRoomNumber] = useState(-1);
  const [open, setOpen] = useState(false);
  const [alertType, setAlertType] = useState("");
  const [text, setText] = useState("");

  useEffect(() => {
    axios.get(baseURL).then((response) => {
      setReservations(response.data);
    });
  }, []);

  if (!reservations) return null;

  const handleSave = () => {
    var reservation = {
      reservePerson: text,
      roomNumber: parseInt(roomNumber),
      hourRange: timeRange,
    };
    debugger;
    axios
      .post(baseURL, reservation)
      .then(() => {
        axios.get(baseURL).then((response) => {
          setReservations(
            response.data.sort(
              (a, b) => parseInt(a.roomNumber) - parseInt(b.roomNumber)
            )
          );
        });
        setAlertType("success");
        setOpen(true);
        setTimeout(() => {
          setOpen(false);
        }, 2000);
      })
      .catch(() => {
        setAlertType("danger");
        setOpen(true);
        setTimeout(() => {
          setOpen(false);
        }, 2000);
      });
  };

  const roomArray = Array.from({ length: 10 }, (x, i) => i);

  return (
    <>
      <div className="container-md">
        <h1>All reservations</h1>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Room Number</th>
              <th>Meeting Range</th>
            </tr>
          </thead>
          <tbody>
            {reservations.map((reservation) => (
              <tr>
                <td>Room{reservation.roomNumber}</td>
                <td>{reservation.hourRange}</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>
      <div className="container-md">
        <h1>Create new reservation</h1>
        <div class="row row-cols-4">
          <Alert open={open} setOpen={setOpen} alertType={alertType}>
            {alertType === "danger"
              ? "Error occurred!"
              : "Reservation saved successfully!"}
          </Alert>
          <Select class="col" array={roomArray} handleChange={setRoomNumber}>
            Room Number
          </Select>
          <TimeSelector class="col" handleChange={setTimeRange} />
          <Text
            class="col"
            handleChange={setText}
            placeholder="Reserve person"
          />
          <Button
            class="col"
            onClick={() => {
              handleSave();
            }}
          >
            Save
          </Button>
        </div>
      </div>
    </>
  );
};

export default App;
