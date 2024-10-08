﻿using BookAudioSystem.BusinessObjects.Entities;

namespace BookAudioSystem.BusinessObjects.Models
{
    public class BookResponseDto
    {
        public int BookID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }

    }
}