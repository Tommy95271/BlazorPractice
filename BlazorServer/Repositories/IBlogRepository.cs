using BlazorServer.Models;
using BlazorServer.ViewModels;
using System.Threading.Tasks;

namespace BlazorServer.Repositories
{
    public interface IBlogRepository
    {
        Task<ResultViewModel> CreateBlog(BlogViewModel blog);
        Task<BlogViewModel> GetBlog();
    }
}