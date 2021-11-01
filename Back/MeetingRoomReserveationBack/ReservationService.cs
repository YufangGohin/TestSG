using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetingRoomReserveationBack
{
    public interface IReservationService
    {
        bool AddNewReservation(Reservation reservation);
        List<Reservation> GetAllReservations();
    }
    public class ReservationService: IReservationService
    {
        Dictionary<int, List<string>> _reservationDic;
        public ReservationService()
        {
            _reservationDic = new Dictionary<int, List<string>>();
        }

        public bool AddNewReservation(Reservation reservation)
        {
            if (!_reservationDic.ContainsKey(reservation.RoomNumber))
            {
                _reservationDic.Add(reservation.RoomNumber, new List<string> { reservation.HourRange + "(" + reservation.ReservePerson + ")" });            
                return true;
            }

            var reservedInfos = _reservationDic[reservation.RoomNumber];

            foreach (var reserve in reservedInfos)
            {
                var reservationRange = reserve.Split("(")[0];
                var reservedBeginTime = DateTime.Parse(reservationRange.Split("-")[0]);
                var reservedEndTime = DateTime.Parse(reservationRange.Split("-")[1]);
                var newReserveBeginTime = DateTime.Parse(reservation.HourRange.Split("-")[0]);
                var newReserveEndTime = DateTime.Parse(reservation.HourRange.Split("-")[1]);

                if ( newReserveBeginTime >= reservedBeginTime && newReserveBeginTime < reservedEndTime || newReserveEndTime > reservedBeginTime && newReserveEndTime <= reservedEndTime)
                    return false;
            }

            reservedInfos.Add( reservation.HourRange + "(" + reservation.ReservePerson + ")");
            _reservationDic[reservation.RoomNumber] = reservedInfos;
            return true;
        }

        public List<Reservation> GetAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            foreach (var reserve in _reservationDic)
            {
                reservations.Add(new Reservation { RoomNumber = reserve.Key, HourRange = string.Join(",  ", reserve.Value)});
            }
            return reservations;
        }
    }
}
