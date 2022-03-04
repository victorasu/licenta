using Abp.Authorization;
using KartSpace.Authorization.Roles;
using KartSpace.Authorization.Users;

namespace KartSpace.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
