using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestProjectBoilerplateCore.MultiTenancy.Dto;

namespace TestProjectBoilerplateCore.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

