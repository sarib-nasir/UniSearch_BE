using System;

namespace UniSearch.Models
{
    public class ApiPermissions
    {
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
        public string ApiURL { get; set; }
        public bool IsActive { get; set; }
    }
}
