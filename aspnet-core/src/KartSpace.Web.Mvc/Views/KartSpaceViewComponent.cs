using Abp.AspNetCore.Mvc.ViewComponents;

namespace KartSpace.Web.Views
{
    public abstract class KartSpaceViewComponent : AbpViewComponent
    {
        protected KartSpaceViewComponent()
        {
            LocalizationSourceName = KartSpaceConsts.LocalizationSourceName;
        }
    }
}
