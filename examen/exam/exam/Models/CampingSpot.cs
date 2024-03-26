namespace exam.Models
{
    public class CampingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public double Price { get; set; }

        public string Image { get; set; }
        public bool availability { get; set; }
    }
}
