using BlazorServer.Models;
using BlazorServer.ViewModels;
using System.Threading.Tasks;

namespace BlazorServer.Repositories
{
    public interface IPostRepository
    {
        Task<ResultViewModel> CreatePost(PostModel post);
        Task<ResultViewModel> DeletePost(int postId);
    }
}