using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace KartSpace.Web.Views
{
    public abstract class KartSpaceRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected KartSpaceRazorPage()
        {
            LocalizationSourceName = KartSpaceConsts.LocalizationSourceName;
        }
    }
}
