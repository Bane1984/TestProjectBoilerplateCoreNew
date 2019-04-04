using System.Threading.Tasks;
using TestProjectBoilerplateCore.Configuration.Dto;

namespace TestProjectBoilerplateCore.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
