using System.Diagnostics.Contracts;

namespace exam.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string description { get; set; }
        public string rating { get; set; }
        public int SpotId { get; set; }
        public int UserId { get; set; }


    
    }
}
