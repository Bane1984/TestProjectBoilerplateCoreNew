using System.Threading.Tasks;
using Abp.Application.Services;
using TestProjectBoilerplateCore.Sessions.Dto;

namespace TestProjectBoilerplateCore.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
