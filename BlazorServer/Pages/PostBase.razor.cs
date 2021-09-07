using BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class PostBase : ComponentBase
    {
        public PostModel Post { get; set; }
        public EditContext editContext;

        protected override Task OnInitializedAsync()
        {
            Post = new PostModel()
            {
                Id = 1,
                Title = "這是標題",
                Content = "這是內容"
            };
            editContext = new EditContext(Post);
            editContext.SetFieldCssClassProvider(new CustomFieldClassProvider());
            return base.OnInitializedAsync();
        }
        protected void TitleChanged(string value)
        {
            Post.Title = value;
        }
    }
}
