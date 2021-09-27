using System.Collections.Generic;

namespace BlazorServer.ViewModels
{
    public class CustomUserViewModel
    {
        public CustomUserViewModel()
        {
            Claims = new();
        }
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public List<string> Claims { get; set; }
    }
}
