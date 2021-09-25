using BlazorServer.Services;
using BlazorServer.Shared;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorServer.Pages.RolesManagement
{
    public partial class RolesManagement
    {
        [Inject] protected IRolesRepository RolesRepository { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime js { get; set; }
        private JsInteropClasses jsClass;
        public List<CustomRoleViewModel> Roles { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await loadData();
            jsClass = new(js);
        }
        private async Task loadData()
        {
            Roles = await RolesRepository.GetRolesAsync();
        }

        private async Task editRole(string roleId)
        {
            NavigationManager.NavigateTo($"RolesManagement/EditRole/{roleId}");
        }

        private async Task deleteRole(string roleId)
        {
            SweetConfirmViewModel sweetConfirm = new SweetConfirmViewModel()
            {
                RequestTitle = $"是否確定刪除角色{roleId}？",
                RequestText = "這個動作不可復原",
                ResponseTitle = "刪除成功",
                ResponseText = "角色被刪除了",
            };
            string jsonString = JsonSerializer.Serialize(sweetConfirm);
            bool result = await jsClass.Confirm(jsonString);
            if (result)
            {
                var deleted = await RolesRepository.DeleteRoleAsync(roleId);
                if (deleted.IsSuccess)
                {
                    await loadData();
                }
                else
                {
                    await jsClass.Alert(deleted.Message);
                }
            }
        }
    }
}
