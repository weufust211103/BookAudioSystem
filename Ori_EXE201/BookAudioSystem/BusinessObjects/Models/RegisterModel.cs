using System;
using System.ComponentModel.DataAnnotations;

namespace RentalBook.BusinessObjects.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string IdentityCard { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Ward { get; set; }

        public string District { get; set; }

        public string Province { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }
    }
}