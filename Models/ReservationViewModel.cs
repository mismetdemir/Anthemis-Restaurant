using System.ComponentModel.DataAnnotations;

namespace Anthemis.Models{
    public class ReservationViewModel{
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Reservation date is required.")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Reservation time is required.")]
        [DataType(DataType.Time)]
        public TimeSpan ReservationTime { get; set; } = new TimeSpan(19, 0, 0);

        [Required(ErrorMessage = "Guest count is required.")]
        [Range(1, 20, ErrorMessage = "Guest count must be between 1 and 20.")]
        public int GuestCount { get; set; } = 2;

        public string? Note { get; set; }
    }
}