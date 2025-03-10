using System;
using UniSearch.ViewModels;

namespace UniSearch.Models
{
    public class BranchDetail : TableBaseViewModel
    {
        public Guid BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }       
        public string BranchType { get; set; }
        
        public UserLogin UserLoginInpBy { get; set; }
        public UserLogin UserLoginModiBy { get; set; }

    }
}
