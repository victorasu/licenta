using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using KartSpace.Authorization;

namespace KartSpace
{
    [DependsOn(
        typeof(KartSpaceCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class KartSpaceApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<KartSpaceAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(KartSpaceApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
