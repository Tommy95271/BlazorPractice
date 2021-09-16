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
        public async Task<BlogModel> GetBlog()
        {
            var blog = await _appDbContext.Blogs.Include(b => b.Posts).FirstOrDefaultAsync();
            if (blog == null)
            {
                blog = new BlogModel();
            }
            return blog;
        }
        public async Task<ResultViewModel> CreateBlog(BlogModel blog)
        {
            var data = await _appDbContext.Blogs
                .FirstOrDefaultAsync(x => x.BlogId == blog.BlogId);
            if (data == null)
            {
                blog.CreateDateTime = DateTime.Now;
                _appDbContext.Blogs.Add(blog);
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
