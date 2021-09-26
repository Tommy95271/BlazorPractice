using BlazorServer.Services;
using BlazorServer.Shared;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages.RolesManagement
{
    public partial class EditUsersInRole
    {
        [Inject] protected IRolesRepository RolesRepository { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime js { get; set; }
        private JsInteropClasses jsClass;
        [Parameter]
        public string RoleId { get; set; }
        public List<CustomUserRoleViewModel> UserRoleViewModel { get; set; } = new List<CustomUserRoleViewModel>();
        protected override async Task OnInitializedAsync()
        {
            await loadData();
            jsClass = new(js);
        }
        private async Task loadData()
        {
            UserRoleViewModel = (await RolesRepository.EditUsersInRoleAsync(RoleId)).ToList();
        }


        public async Task HandleValidSubmit()
        {
            var result = await RolesRepository.EditUsersInRoleAsync(UserRoleViewModel, RoleId);

            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo($"/RolesManagement/EditRole/{RoleId}");
            }
            else
            {
                await jsClass.Alert(result.Message);
            }
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo($"/RolesManagement/EditRole/{RoleId}");
        }
    }
}
