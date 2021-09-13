using BlazorServer.Models;
using BlazorServer.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class PostBase : ComponentBase, IDisposable
    {
        [Inject]
        protected IJSRuntime js { get; set; }

        [Parameter]
        public PostModel Post { get; set; }
        public EditContext editContext { get; set; }
        private JsInteropClasses jsClass;

        protected override Task OnInitializedAsync()
        {
            jsClass = new(js);
            editContext = new EditContext(Post);
            editContext.SetFieldCssClassProvider(new CustomFieldClassProvider());
            return base.OnInitializedAsync();
        }
        protected void TitleChanged(string value)
        {
            Post.Title = value;
        }

        [CascadingParameter(Name = "ColorStyle")]
        public string ColorStyle { get; set; }

        [Parameter]
        public EventCallback<int> getPostId { get; set; }

        protected async Task deletePost()
        {
            bool confirm = await jsClass.Confirm(Post.Title);
            if (confirm)
            {
                await getPostId.InvokeAsync(Post.PostId);
            }
        }

        public void Dispose()
        {
            jsClass?.Dispose();
        }
    }
}
