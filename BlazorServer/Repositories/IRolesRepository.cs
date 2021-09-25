using BlazorServer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public interface IRolesRepository
    {
        Task<CustomRoleViewModel> GetRoleAsync(string RoleId);
        Task<List<CustomRoleViewModel>> GetRolesAsync();
        Task<ResultViewModel> CreateRoleAsync(CustomRoleViewModel model);
        Task<ResultViewModel> EditRoleAsync(CustomRoleViewModel model);
        Task<ResultViewModel> DeleteRoleAsync(string roleId);
    }
}