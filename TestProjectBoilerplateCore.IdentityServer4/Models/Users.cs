using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace TestProjectBoilerplateCore.IdentityServer4.Models
{
    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "bane",
                    Password = "bane",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "bane@bild.studio"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
