using System;
using System.ComponentModel.DataAnnotations;

namespace StudyRoomMvc.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public string Room { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }

        [Required]
        public string StudentName { get; set; } = string.Empty;
    }
}
