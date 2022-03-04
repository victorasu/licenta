using System.Collections.Generic;
using KartSpace.Roles.Dto;

namespace KartSpace.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
