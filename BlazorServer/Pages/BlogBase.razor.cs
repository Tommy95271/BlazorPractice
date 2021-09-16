using BlazorServer.Models;
using BlazorServer.Repositories;
using BlazorServer.Shared;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class BlogBase : ComponentBase
    {
        [Inject] protected IJSRuntime js { get; set; }
        [Inject] protected IBlogRepository BlogRepository { get; set; }
        private JsInteropClasses jsClass;

        public BlogModel Blog { get; set; }
        private int postId { get; set; } = 0;
        public string ColorStyle { get; set; } = "color: goldenrod";
        protected override async Task OnInitializedAsync()
        {
            jsClass = new(js);
            await loadData();
        }
        private async Task loadData()
        {
            Blog = await BlogRepository.GetBlog();
        }

        protected async Task createBlog()
        {
            var result = await BlogRepository.CreateBlog(Blog);
            if (result.IsSuccess)
            {
                await loadData();
            }
            else
            {
                await jsClass.Alert(result.Message);
            }
        }

        protected void add()
        {
            postId++;
            Blog.Posts.Add(new PostModel() { PostId = postId });
        }

        protected void getPostId(int id)
        {
            Blog.Posts.Remove(Blog.Posts.FirstOrDefault(p => p.PostId == id));
        }
    }
}
