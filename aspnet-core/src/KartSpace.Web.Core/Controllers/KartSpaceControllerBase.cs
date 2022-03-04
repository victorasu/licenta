using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace KartSpace.Controllers
{
    public abstract class KartSpaceControllerBase: AbpController
    {
        protected KartSpaceControllerBase()
        {
            LocalizationSourceName = KartSpaceConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
