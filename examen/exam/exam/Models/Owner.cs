namespace exam.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Fn { get; set; }
        public string Ln { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsDeletedByAdmin { get; set; }

    }
}
