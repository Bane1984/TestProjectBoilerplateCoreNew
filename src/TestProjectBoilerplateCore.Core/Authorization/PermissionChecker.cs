using Abp.Authorization;
using TestProjectBoilerplateCore.Authorization.Roles;
using TestProjectBoilerplateCore.Authorization.Users;

namespace TestProjectBoilerplateCore.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
