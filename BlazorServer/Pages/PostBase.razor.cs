using BlazorServer.Models;
using BlazorServer.Repositories;
using BlazorServer.Shared;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class PostBase : ComponentBase, IDisposable
    {
        [Inject] protected IJSRuntime js { get; set; }
        [Inject] protected IPostRepository PostRepository { get; set; }

        [Parameter]
        public PostViewModel Post { get; set; }
        [Parameter]
        public EventCallback<int> getPostId { get; set; }
        [Parameter]
        public EventCallback postCreated { get; set; }
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
            SweetConfirmViewModel sweetConfirm = new SweetConfirmViewModel()
            {
                RequestTitle = $"是否確定刪除日誌{Post.Title}？",
                RequestText = "這個動作不可復原",
                ResponseTitle = "刪除成功",
                ResponseText = "日誌被刪除了",
            };
            string jsonString = JsonSerializer.Serialize(sweetConfirm);
            bool result = await jsClass.Confirm(jsonString);
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
            if (result.IsSuccess)
            {
                await postCreated.InvokeAsync();
            }
            await jsClass.Alert(result.Message);
        }

        public void Dispose()
        {
            jsClass?.Dispose();
        }
    }
}
