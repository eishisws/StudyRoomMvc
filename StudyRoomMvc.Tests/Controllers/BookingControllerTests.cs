using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyRoomMvc.Data;
using StudyRoomMvc.Models;
using StudyRoomMvc.Services;
using StudyRoomMvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace StudyRoomMvc.Tests.Controllers
{
    public class BookingControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfBookings()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            mockService.Setup(service => service.GetAll()).Returns(new List<Booking>());
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.Index();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Booking>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            mockService.Setup(service => service.GetById(It.IsAny<int>())).Returns((Booking)null);
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.Details(1);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsViewResult_WithBooking()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var booking = new Booking { BookingId = 1, Room = "101", Date = DateTime.Now };
            mockService.Setup(service => service.GetById(1)).Returns(booking);
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.Details(1);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Booking>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var controller = new BookingController(mockService.Object);
            var booking = new Booking { BookingId = 1, Room = "101", Date = DateTime.Now };
            // Act
            var result = controller.Create(booking);
            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Create_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var controller = new BookingController(mockService.Object);
            controller.ModelState.AddModelError("Room", "Required");
            var booking = new Booking { BookingId = 1, Date = DateTime.Now };
            // Act
            var result = controller.Create(booking);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Booking>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Edit_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            mockService.Setup(service => service.GetById(It.IsAny<int>())).Returns((Booking)null);
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.Edit(1);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WithBooking()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var booking = new Booking { BookingId = 1, Room = "101", Date = DateTime.Now };
            mockService.Setup(service => service.GetById(1)).Returns(booking);
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.Edit(1);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Booking>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var controller = new BookingController(mockService.Object);
            var booking = new Booking { BookingId = 1, Room = "101", Date = DateTime.Now };
            // Act
            var result = controller.Edit(booking);
            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var controller = new BookingController(mockService.Object);
            controller.ModelState.AddModelError("Room", "Required");
            var booking = new Booking { BookingId = 1, Date = DateTime.Now };
            // Act
            var result = controller.Edit(booking);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Booking>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenBookingDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            mockService.Setup(service => service.GetById(It.IsAny<int>())).Returns((Booking)null);
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.Delete(1);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsViewResult_WithBooking()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var booking = new Booking { BookingId = 1, Room = "101", Date = DateTime.Now };
            mockService.Setup(service => service.GetById(1)).Returns(booking);
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.Delete(1);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Booking>(viewResult.ViewData.Model);
        }

        [Fact]
        public void DeleteConfirmed_RedirectsToIndex()
        {
            // Arrange
            var mockService = new Mock<IBookingService>();
            var controller = new BookingController(mockService.Object);
            // Act
            var result = controller.DeleteConfirmed(1);
            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
