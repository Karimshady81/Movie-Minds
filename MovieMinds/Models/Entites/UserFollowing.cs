namespace MovieMinds.Models.Entites
{
    public class UserFollowing
    {
        public string FollowerId { get; set; } = string.Empty;
        public string FollowingId { get; set; } = string.Empty;
        public DateTime FollowedAt { get; set; }

        public User Follower { get; set; } 
        public User Following { get; set; }
    }
}
