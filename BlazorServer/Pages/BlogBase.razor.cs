using BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class BlogBase : ComponentBase
    {
        public BlogModel Blog { get; set; }
        private int postId { get; set; } = 0;
        protected override Task OnInitializedAsync()
        {
            loadData();
            return base.OnInitializedAsync();
        }
        private void loadData()
        {
            Blog = new BlogModel
            {
                BlogId = 1,
                BlogName = "我的部落格",
                CreateDateTime = new(2021, 9, 7, 10, 20, 35),
            };
            if (Blog.Posts == null)
            {
                Blog.Posts = new List<PostModel>();
            }
        }
        public string ColorStyle { get; set; } = "color: goldenrod";

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
