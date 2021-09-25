using BlazorServer.Services;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorServer.Pages.RolesManagement
{
    public partial class EditRole
    {
        [Inject] protected IRolesRepository RolesRepository { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        public CustomRoleViewModel Role { get; set; } = new();
        [Parameter]
        public string RoleId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var result = await RolesRepository.GetRoleAsync(RoleId);
            Role = new CustomRoleViewModel
            {
                RoleId = result.RoleId,
                RoleName = result.RoleName,
                Users = result.Users
            };
        }
        private async Task editRole()
        {
            await RolesRepository.EditRoleAsync(Role);
            NavigationManager.NavigateTo("/RolesManagement/RolesList");
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo($"/RolesManagement/RolesList");
        }
    }
}
