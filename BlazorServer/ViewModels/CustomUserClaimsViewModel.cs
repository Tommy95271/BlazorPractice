using System.Collections.Generic;

namespace BlazorServer.ViewModels
{
    public class CustomUserClaimsViewModel
    {
        public CustomUserClaimsViewModel()
        {
            Cliams = new();
        }

        public string UserId { get; set; }
        public List<CustomUserClaimViewModel> Cliams { get; set; }
    }
}
