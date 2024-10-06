﻿using System.ComponentModel.DataAnnotations;

namespace BookAudioSystem.BusinessObjects.Models
{
    public class UserResDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }

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
        public string Token { get; set; }
    }
}
