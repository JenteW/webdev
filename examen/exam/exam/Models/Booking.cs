namespace exam.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CampingSpotId { get; set; }
        public int UserId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public double Price { get; set; }  

    }
}
