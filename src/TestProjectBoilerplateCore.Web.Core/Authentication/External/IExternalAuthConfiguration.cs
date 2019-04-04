using System.Collections.Generic;

namespace TestProjectBoilerplateCore.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
