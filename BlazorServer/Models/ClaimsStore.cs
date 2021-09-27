using System.Collections.Generic;
using System.Security.Claims;

namespace BlazorServer.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("ManageUser", string.Empty),
            new Claim("CreateUser", string.Empty),
            new Claim("EditUser", string.Empty),
            new Claim("DeleteUser", string.Empty)
        };
    }
}
