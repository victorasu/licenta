using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using KartSpace.Configuration.Dto;

namespace KartSpace.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : KartSpaceAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
