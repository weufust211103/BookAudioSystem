namespace BookAudioSystem.BusinessObjects.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }  // Store password securely as hash
        public string Role { get; set; }
    }
}
