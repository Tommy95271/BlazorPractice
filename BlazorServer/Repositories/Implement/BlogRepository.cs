using BlazorServer.Models;
using BlazorServer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Repositories.Implement
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _appDbContext;

        public BlogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<BlogViewModel> GetBlog()
        {
            BlogModel blog = await _appDbContext.Blogs.Include(b => b.Posts).FirstOrDefaultAsync();
            if (blog == null)
            {
                return new BlogViewModel();
            }
            else
            {
                BlogViewModel blogViewModel = new BlogViewModel()
                {
                    BlogId = blog.BlogId,
                    BlogName = blog.BlogName,
                    CreateDateTime = blog.CreateDateTime,
                    Posts = blog.Posts.Select(p => new PostViewModel()
                    {
                        PostId = p.PostId,
                        BlogId = p.BlogId,
                        Blog = new()
                        {
                            BlogId = p.BlogId,
                            BlogName = p.Blog.BlogName,
                            CreateDateTime = p.CreateDateTime
                        },
                        Title = p.Title,
                        Content = p.Content,
                        CreateDateTime = p.CreateDateTime,
                        UpdateDateTime = p.UpdateDateTime,
                    }).ToList()
                };
                return blogViewModel;
            }
        }
        public async Task<ResultViewModel> CreateBlog(BlogViewModel blog)
        {
            BlogModel data = await _appDbContext.Blogs
                .FirstOrDefaultAsync(x => x.BlogId == blog.BlogId);
            if (data == null)
            {
                data = new();
                data.BlogName = blog.BlogName;
                data.CreateDateTime = DateTime.Now;
                _appDbContext.Blogs.Add(data);
                _appDbContext.SaveChanges();
                return new ResultViewModel() { IsSuccess = true, Message = $"{blog.BlogName} 建立成功" };
            }
            else
            {
                return new ResultViewModel() { IsSuccess = false, Message = $"{blog.BlogName} 已存在" };
            }
        }
    }
}
