using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyRoomMvc.Data;
using StudyRoomMvc.Services;
using StudyRoomMvc.Models;

namespace StudyRoomMvc.Tests.Services
{
    public class BookingServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void test_addBooking()
        {
            using var context = GetInMemoryDbContext();
            var repo = new BookingRepository(context);
            var service = new BookingService(repo);

            service.Add(new Booking { BookingId = 1, Room = "Room 1", Date = DateTime.UtcNow, StudentName = "Student 1" });

            var result = context.Bookings.Find(1);

            Assert.NotNull(result);
            Assert.Equal("Room 1", result.Room);
            Assert.Equal("Student 1", result.StudentName);
        }

        [Fact]
        public void test_addBooking_withEmptyBooking()
        {
            using var context = GetInMemoryDbContext();
            var repo = new BookingRepository(context);
            var service = new BookingService(repo);


            Assert.Throws<DbUpdateException>(() => service.Add(new Booking()));
        }

        [Fact]
        public void test_getAll()
        {
            using var context = GetInMemoryDbContext();
            var repo = new BookingRepository(context);
            var service = new BookingService(repo);

            service.Add(new Booking { BookingId = 1, Room = "Room 1", Date = DateTime.UtcNow, StudentName = "Student 1" });
            service.Add(new Booking { BookingId = 2, Room = "Room 2", Date = DateTime.UtcNow, StudentName = "Student 2" });
            service.Add(new Booking { BookingId = 3, Room = "Room 3", Date = DateTime.UtcNow, StudentName = "Student 3" });
            service.Add(new Booking { BookingId = 4, Room = "Room 4", Date = DateTime.UtcNow, StudentName = "Student 4" });

            var result = service.GetAll();
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void test_getById()
        {
            using var context = GetInMemoryDbContext();
            var repo = new BookingRepository(context);
            var service = new BookingService(repo);

            service.Add(new Booking { BookingId = 1, Room = "Room 1", Date = DateTime.UtcNow, StudentName = "Student 1" });

            var result = service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Room 1", result.Room);
            Assert.Equal("Student 1", result.StudentName);
        }

        [Fact]
        public void test_updateBooking()
        {
            using var context = GetInMemoryDbContext();
            var repo = new BookingRepository(context);
            var service = new BookingService(repo);

            service.Add(new Booking { BookingId = 1, Room = "Room 1", Date = DateTime.UtcNow, StudentName = "Student 1" });

            var bookingToUpdate = service.GetById(1);

            bookingToUpdate.Room = "Updated Room";
            bookingToUpdate.StudentName = "Updated Student";

            service.Update(bookingToUpdate);

            var result = service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal("Updated Room", result.Room);
            Assert.Equal("Updated Student", result.StudentName);
        }

        [Fact]
        public void test_updateBooking_withNullValuesForNullableAttributes()
        {
            using var context = GetInMemoryDbContext();
            var repo = new BookingRepository(context);
            var service = new BookingService(repo);

            service.Add(new Booking { BookingId = 1, Room = "Room 1", Date = DateTime.UtcNow, StudentName = "Student 1" });

            var bookingToUpdate = service.GetById(1);

            bookingToUpdate.Room = null;
            bookingToUpdate.StudentName = null;
            bookingToUpdate.Date = null;

            service.Update(bookingToUpdate);

            var result = service.GetById(1);

            Assert.NotNull(result);
            Assert.Null(result.Room);
            Assert.Null(result.StudentName); //should probably not allow null values in the first place
        }

        [Fact]
        public void test_deleteBooking()
        {
            using var context = GetInMemoryDbContext();
            var repo = new BookingRepository(context);
            var service = new BookingService(repo);

            service.Add(new Booking { BookingId = 1, Room = "Room 1", Date = DateTime.UtcNow, StudentName = "Student 1" });
            service.Delete(1);

            var result = service.GetById(1);

            Assert.Null(result);
        }
    }
}
