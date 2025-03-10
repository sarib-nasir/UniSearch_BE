using System;
using System.Collections.Generic;

namespace UniSearch.Models
{
    public class UserLogin
    {
        public UserLogin()
        {
            branchDetailInpBy = new List<BranchDetail>();
            branchDetailModiBy = new List<BranchDetail>();
        }
        public Guid LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool isActive { get; set; }
        public UserRole UserRole { get; set; }

        public List<BranchDetail> branchDetailInpBy { get; set; }
        public List<BranchDetail> branchDetailModiBy { get; set; }

    }
}

