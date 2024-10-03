using System.ComponentModel.DataAnnotations;

namespace RentalBook.BusinessObjects.Models
{
    public class UserResponseDto
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; } // Optional: if you want to return a token on login
    }
}