using System.Collections.Generic;
using StudyRoomMvc.Models;
using StudyRoomMvc.Data;

namespace StudyRoomMvc.Services
{
    public class BookingService
    {
        private readonly BookingRepository _repo;

        public BookingService(AppDbContext context)
        {
            _repo = new BookingRepository(context);
        }

        public List<Booking> GetAll()
        {
            return _repo.GetAll();
        }

        public Booking GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(Booking booking)
        {
            _repo.Add(booking);
        }

        public void Update(Booking booking)
        {
            _repo.Update(booking);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
