using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudyRoomMvc.Models;
using Xunit;

namespace StudyRoomMvc.Tests.Models
{
    public class BookingModelTests
    {
        private List<ValidationResult> ValidateModel(Booking model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.TryValidateObject(model, context, results, validateAllProperties: true);
            return results;
        }

        [Fact]
        public void Booking_WithAllValidProperties_ShouldPassValidation()
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = 1,
                Room = "Room A",
                Date = DateTime.Now,
                StudentName = "Alice"
            };

            // Act
            var results = ValidateModel(booking);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Booking_MissingRoom_ShouldFailValidation()
        {
            var booking = new Booking
            {
                BookingId = 1,
                Room = string.Empty, 
                Date = DateTime.Now,
                StudentName = "Alice"
            };

            var results = ValidateModel(booking);

            Assert.Contains(results, r => r.MemberNames.Contains("Room"));
        }

        [Fact]
        public void Booking_MissingStudentName_ShouldFailValidation()
        {
            var booking = new Booking
            {
                BookingId = 1,
                Room = "Room A",
                Date = DateTime.Now,
                StudentName = string.Empty 
            };

            var results = ValidateModel(booking);

            Assert.Contains(results, r => r.MemberNames.Contains("StudentName"));
        }

        [Fact]
        public void Booking_MissingDate_ShouldFailValidation()
        {
            var booking = new Booking
            {
                BookingId = 1,
                Room = "Room A",
                Date = default, 
                StudentName = "Alice"
            };

            var results = ValidateModel(booking);

            Assert.Contains(results, r => r.MemberNames.Contains("Date"));
        }
    }
}
