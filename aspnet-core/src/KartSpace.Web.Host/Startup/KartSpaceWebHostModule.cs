using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KartSpace.Configuration;

namespace KartSpace.Web.Host.Startup
{
    [DependsOn(
       typeof(KartSpaceWebCoreModule))]
    public class KartSpaceWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public KartSpaceWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KartSpaceWebHostModule).GetAssembly());
        }
    }
}
