using BlazorServer.Repositories;
using BlazorServer.Shared;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorServer.Pages.UserManagement
{
    public partial class UserManagement
    {
        [Inject] protected IUserRepository UserRepository { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime js { get; set; }
        private JsInteropClasses jsClass;
        public List<CustomUserViewModel> Users { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            await loadData();
            jsClass = new(js);
        }
        private async Task loadData()
        {
            Users = await UserRepository.GetUsersAsync();
        }

        private async Task editUser(string userId)
        {
            NavigationManager.NavigateTo($"UserManagement/EditUser/{userId}");
        }

        private async Task deleteUser(string userId)
        {
            SweetConfirmViewModel sweetConfirm = new SweetConfirmViewModel()
            {
                RequestTitle = $"是否確定刪除使用者{userId}？",
                RequestText = "這個動作不可復原",
                ResponseTitle = "刪除成功",
                ResponseText = "使用者被刪除了",
            };
            string jsonString = JsonSerializer.Serialize(sweetConfirm);
            bool result = await jsClass.Confirm(jsonString);
            if (result)
            {
                var deleted = await UserRepository.DeleteUserAsync(userId);
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
