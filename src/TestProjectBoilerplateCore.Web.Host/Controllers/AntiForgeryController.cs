using Microsoft.AspNetCore.Antiforgery;
using TestProjectBoilerplateCore.Controllers;

namespace TestProjectBoilerplateCore.Web.Host.Controllers
{
    public class AntiForgeryController : TestProjectBoilerplateCoreControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
