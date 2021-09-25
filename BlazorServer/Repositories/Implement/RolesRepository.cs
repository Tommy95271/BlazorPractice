﻿using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class RolesRepository : IRolesRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesRepository(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #region Roles
        public async Task<CustomRoleViewModel> GetRoleAsync(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            var result = new CustomRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Users = users.Select(u => u.UserName).ToList()
            };
            return result;
        }

        public async Task<List<CustomRoleViewModel>> GetRolesAsync()
        {
            var roles = _roleManager.Roles;
            var customRoles = new List<CustomRoleViewModel>();
            foreach (var role in roles)
            {
                customRoles.Add(new CustomRoleViewModel { RoleId = role.Id, RoleName= role.Name });
            }
            return await Task.Run(() => customRoles);
        }

        public async Task<ResultViewModel> CreateRoleAsync(CustomRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "角色建立成功！",
                    IsSuccess = true
                };
            }
            return new ResultViewModel
            {
                Message = "角色建立失敗！",
                IsSuccess = false
            };
        }

        public async Task<ResultViewModel> EditRoleAsync(CustomRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                return new ResultViewModel
                {
                    Message = $"找不到 Id 為 {model.RoleId} 的角色",
                    IsSuccess = false
                };
            }
            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "角色更新成功！",
                    IsSuccess = true
                };
            }
            return new ResultViewModel
            {
                Message = "角色更新失敗！",
                IsSuccess = false
            };
        }

        public async Task<ResultViewModel> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return new ResultViewModel
                {
                    Message = $"找不到 Id 為 {roleId} 的角色",
                    IsSuccess = false
                };
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new ResultViewModel
                {
                    Message = "角色刪除成功！",
                    IsSuccess = true
                };
            }
            return new ResultViewModel
            {
                Message = "角色刪除失敗！",
                IsSuccess = false
            };
        }
        #endregion
    }
}
