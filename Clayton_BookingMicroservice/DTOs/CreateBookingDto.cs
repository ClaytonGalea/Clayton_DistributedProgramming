namespace Clayton_BookingMicroservice.DTOs
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }

        public string StartLocation { get; set; } = string.Empty;

        public string EndLocation { get; set; } = string.Empty;

        public DateTime BookingDateTime { get; set; }

        public int PassengerCount { get; set; }

        public string CabType { get; set; } = string.Empty;
    }
}