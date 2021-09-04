using BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class PostBase : ComponentBase
    {
        public PostModel Post { get; set; }

        protected override Task OnInitializedAsync()
        {
            Post = new PostModel()
            {
                Id = 1,
                Title = "這是標題",
                Content = "這是內容"
            };
            return base.OnInitializedAsync();
        }
    }
}
