namespace exam.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CampingSpotId { get; set; }
        public int UserId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

    }
}
