using Microsoft.EntityFrameworkCore;
using StudyRoomMvc.Models;

namespace StudyRoomMvc.Services
{
    public interface IBookingRepository
    {
        public List<Booking> GetAll();

        public Booking GetById(int id);

        public void Add(Booking booking);

        public void Update(Booking booking);

        public void Delete(int id);
    }
}
