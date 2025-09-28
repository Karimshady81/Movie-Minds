namespace MovieMinds.Models.Entites
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public double Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        
        public User User { get; set; } 
        public Movie Movie { get; set; }
    }
}
