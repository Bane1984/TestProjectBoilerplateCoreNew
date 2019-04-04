using System.Threading.Tasks;
using Abp.Application.Services;
using TestProjectBoilerplateCore.Authorization.Accounts.Dto;

namespace TestProjectBoilerplateCore.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
