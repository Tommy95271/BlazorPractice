using BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class PostBase : ComponentBase
    {
        [Parameter]
        public PostModel Post { get; set; }
        public EditContext editContext;

        protected override Task OnInitializedAsync()
        {
            editContext = new EditContext(Post);
            editContext.SetFieldCssClassProvider(new CustomFieldClassProvider());
            return base.OnInitializedAsync();
        }
        protected void TitleChanged(string value)
        {
            Post.Title = value;
        }

        [Parameter]
        public Dictionary<string, object> InputAttributes { get; set; } =
            new Dictionary<string, object>()
            {
                { "value", "Submit" },
                { "class", "btn btn-primary" },
                { "type", "button" },
            };
    }
}
