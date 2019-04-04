using Abp.MultiTenancy;
using TestProjectBoilerplateCore.Authorization.Users;

namespace TestProjectBoilerplateCore.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
