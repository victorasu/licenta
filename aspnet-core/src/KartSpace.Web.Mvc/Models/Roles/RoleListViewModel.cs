using System.Collections.Generic;
using KartSpace.Roles.Dto;

namespace KartSpace.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
