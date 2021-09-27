using BlazorServer.Models;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorServer.Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<CustomUserViewModel>> GetUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var customUsers = new List<CustomUserViewModel>();
            foreach (var user in users)
            {
                customUsers.Add(new CustomUserViewModel { UserId = user.Id, UserName = user.UserName, Email = user.Email });
            }
            return await Task.Run(() => customUsers);
        }
        public async Task<CustomUserViewModel> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var result = new CustomUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(x => $"{x.Type} : {x.Value}").ToList()
            };
            return result;
        }

        public async Task<ResultViewModel> EditUserAsync(CustomUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return new ResultViewModel
                {
                    Message = $"找不到 Id 為{model.UserId} 的使用者",
                    IsSuccess = false
                };
            }
            user.UserName = model.UserName;
            user.Email = model.Email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "使用者更新成功！",
                    IsSuccess = true
                };
            }
            return new ResultViewModel
            {
                Message = "使用者更新失敗！",
                IsSuccess = false
            };
        }
        public async Task<ResultViewModel> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ResultViewModel
                {
                    Message = $"找不到 Id 為 {userId} 的使用者",
                    IsSuccess = false
                };
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "使用者刪除成功！",
                    IsSuccess = true
                };
            }
            return new ResultViewModel
            {
                Message = "使用者刪除失敗！",
                IsSuccess = false
            };
        }
        public async Task<CustomUserClaimsViewModel> EditClaimsInUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var claims = await _userManager.GetClaimsAsync(user);
            var model = new CustomUserClaimsViewModel
            {
                UserId = userId
            };

            foreach (var claim in ClaimsStore.AllClaims)
            {
                CustomUserClaimViewModel userClaim = new CustomUserClaimViewModel
                {
                    ClaimType = claim.Type
                };

                if (claims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }

                model.Cliams.Add(userClaim);
            }
            return model;
        }

        public async Task<ResultViewModel> EditClaimsInUserAsync(CustomUserClaimsViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "無法移除使用者的 Claim！",
                    IsSuccess = false
                };
            }

            result = await _userManager.AddClaimsAsync(user,
                model.Cliams.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));

            if (!result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "無法將指定的 Claim 指派給使用者！",
                    IsSuccess = false
                };
            }

            return new ResultViewModel
            {
                Message = "指派 Claim 成功",
                IsSuccess = true
            };
        }
    }
}
