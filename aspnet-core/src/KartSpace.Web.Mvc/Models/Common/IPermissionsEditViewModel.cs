using System.Collections.Generic;
using KartSpace.Roles.Dto;

namespace KartSpace.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}