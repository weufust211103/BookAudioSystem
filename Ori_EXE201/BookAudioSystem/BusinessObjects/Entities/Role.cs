﻿namespace BookAudioSystem.BusinessObjects.Entities
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        // Navigation property
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
