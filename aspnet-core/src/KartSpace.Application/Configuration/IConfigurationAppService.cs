using System.Threading.Tasks;
using KartSpace.Configuration.Dto;

namespace KartSpace.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
