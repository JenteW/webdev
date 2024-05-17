namespace exam.Models
{
    public class Accomodation
    {
        /// <summary>
        /// Accomodation = de exacte spot. bijvoorbeeld tent 3 op campingspot 2
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CampingSpotId { get; set; }

    }
}
