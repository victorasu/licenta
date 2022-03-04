using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KartSpace.EntityFrameworkCore;
using KartSpace.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace KartSpace.Web.Tests
{
    [DependsOn(
        typeof(KartSpaceWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class KartSpaceWebTestModule : AbpModule
    {
        public KartSpaceWebTestModule(KartSpaceEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KartSpaceWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(KartSpaceWebMvcModule).Assembly);
        }
    }
}