using MeetingRoomReserveationBack;
using MeetingRoomReserveationBack.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MeetingRoomReservationUnitTest
{
    [TestClass]
    public class ReservationTests
    {
        Mock<IReservationService> _fakeService = new Mock<IReservationService>();

        [TestMethod]
        public void Test_roomNumberShouldBeLessThan10()
        {           
            var controller = new ReservationController(_fakeService.Object);

            IActionResult roomNumberShouldBeLessThan10 = controller.AddNewReservations(new Reservation() { RoomNumber = 123, HourRange = "1:00-2:00" });
            Assert.Equals(400, (roomNumberShouldBeLessThan10 as ObjectResult)?.StatusCode);

        }
        [TestMethod]
        public void Test_hourRangeShoudMakeSense()
        {
            var controller = new ReservationController(_fakeService.Object);

            IActionResult hourRangeShoudMakeSense = controller.AddNewReservations(new Reservation() { RoomNumber = 2, HourRange = "1:00-1:00" });
            Assert.Equals(400, (hourRangeShoudMakeSense as ObjectResult)?.StatusCode);

        }

        [TestMethod]
        public void Test_hoursShouldMakeSense()
        {
            var controller = new ReservationController(_fakeService.Object);

            IActionResult hoursShouldMakeSense = controller.AddNewReservations(new Reservation() { RoomNumber = 2, HourRange = "331-232" });
            Assert.Equals(400, (hoursShouldMakeSense as ObjectResult)?.StatusCode);

        }

        [TestMethod]
        public void Test_hourRangeShouldBeCorrectForm()
        {
            var controller = new ReservationController(_fakeService.Object);

            IActionResult hourRangeShouldBeCorrectForm = controller.AddNewReservations(new Reservation() { RoomNumber = 2, HourRange = "test1etet" });
            Assert.Equals(400, (hourRangeShouldBeCorrectForm as ObjectResult)?.StatusCode);

        }
    }
    
}