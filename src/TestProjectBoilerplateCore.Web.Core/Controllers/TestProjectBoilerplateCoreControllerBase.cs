using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TestProjectBoilerplateCore.Controllers
{
    public abstract class TestProjectBoilerplateCoreControllerBase: AbpController
    {
        protected TestProjectBoilerplateCoreControllerBase()
        {
            LocalizationSourceName = TestProjectBoilerplateCoreConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
