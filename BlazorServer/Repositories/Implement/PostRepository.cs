using BlazorServer.Models;
using BlazorServer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Repositories.Implement
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResultViewModel> CreatePost(PostModel post)
        {
            var data = await _appDbContext.Posts
                .FirstOrDefaultAsync(x => x.PostId == post.PostId);
            if (data == null)
            {
                _appDbContext.Posts.Add(post);
                _appDbContext.SaveChanges();
                return new ResultViewModel() { IsSuccess = true, Message = $"{post.Title} 建立成功" };
            }
            else
            {
                data.Title = post.Title;
                data.Content = post.Content;
                data.UpdateDateTime = DateTime.Now;
                _appDbContext.SaveChanges();
                return new ResultViewModel() { IsSuccess = false, Message = $"{post.Title} 修改成功" };
            }
        }
        public async Task<ResultViewModel> DeletePost(int postId)
        {
            var data = await _appDbContext.Posts
                .FirstOrDefaultAsync(x => x.PostId == postId);
            if (data == null)
            {
                return new ResultViewModel() { IsSuccess = false, Message = "Post 不存在！" };
            }
            else
            {
                var blog = _appDbContext.Blogs.Include(b => b.Posts).FirstOrDefault();
                if (blog.Posts.Any(p => string.IsNullOrWhiteSpace(p.Title)))
                {
                    return new ResultViewModel() { IsSuccess = false, Message = $"不能有標題為空的 Post" };
                }
                _appDbContext.Posts.Remove(data);
                _appDbContext.SaveChanges();
                return new ResultViewModel() { IsSuccess = true, Message = $"{data.Title} 成功刪除" };
            }
        }

    }
}
