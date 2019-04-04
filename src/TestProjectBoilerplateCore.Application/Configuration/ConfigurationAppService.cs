using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TestProjectBoilerplateCore.Configuration.Dto;

namespace TestProjectBoilerplateCore.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TestProjectBoilerplateCoreAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
