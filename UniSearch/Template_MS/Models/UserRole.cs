using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class UserRole
    {
        public UserRole()
        {
            userLogins = new List<UserLogin>();
        }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        public List<UserLogin> userLogins { get; set; }
    }
}
