using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeetingRoomReserveationBack.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {         
            _reservationService = reservationService;
        }

        
        [HttpGet]
        public IActionResult GetAllReservations()
        {
            var reservation = _reservationService.GetAllReservations();
            if (reservation == null)
                return NotFound();
            else
                return Ok(reservation);
        }

        [HttpPost]
        public IActionResult AddNewReservations(Reservation reservation)
        {
            if (string.IsNullOrEmpty(reservation.ReservePerson)) return BadRequest();
            if (reservation == null || reservation.HourRange.Split("-").Length != 2 || !reservation.HourRange.Contains("-")) return BadRequest();
            if (reservation.RoomNumber < 0 || reservation.RoomNumber > 9) return BadRequest();
            if (DateTime.Parse(reservation.HourRange.Split("-")[0]) >= DateTime.Parse(reservation.HourRange.Split("-")[1])) return BadRequest();
       
             DateTime parseTime;
            if (!DateTime.TryParse(reservation.HourRange.Split("-")[0], out parseTime)  || !DateTime.TryParse(reservation.HourRange.Split("-")[1], out parseTime)) return BadRequest();

            bool reslut = _reservationService.AddNewReservation(reservation);
            if (reslut) 
                return Ok(); 
            else 
                return BadRequest();
        }
    }
}
