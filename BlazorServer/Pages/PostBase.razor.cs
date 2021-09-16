using BlazorServer.Models;
using BlazorServer.Repositories;
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
        [Inject] protected IJSRuntime js { get; set; }
        [Inject] protected IPostRepository PostRepository { get; set; }

        [Parameter]
        public PostModel Post { get; set; }
        [Parameter]
        public EventCallback<int> getPostId { get; set; }
        public EditContext editContext { get; set; }
        private JsInteropClasses jsClass;

        protected override Task OnInitializedAsync()
        {
            jsClass = new(js);
            editContext = new EditContext(Post);
            editContext.SetFieldCssClassProvider(new CustomFieldClassProvider());
            return base.OnInitializedAsync();
        }

        [CascadingParameter(Name = "ColorStyle")]
        public string ColorStyle { get; set; }

        protected async Task deletePost()
        {
            bool result = await jsClass.Confirm(Post.Title);
            if (result)
            {
                var deleted = await PostRepository.DeletePost(Post.PostId);
                if (deleted.IsSuccess)
                {
                    await getPostId.InvokeAsync(Post.PostId);
                }
                else
                {
                    await jsClass.Alert(deleted.Message);
                }
            }
        }
        protected async Task createPost()
        {
            var result = await PostRepository.CreatePost(Post);
            await jsClass.Alert(result.Message);
        }

        public void Dispose()
        {
            jsClass?.Dispose();
        }
    }
}
