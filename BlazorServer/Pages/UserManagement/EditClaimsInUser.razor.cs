using BlazorServer.Repositories;
using BlazorServer.Shared;
using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorServer.Pages.UserManagement
{
    public partial class EditClaimsInUser
    {
        [Inject] protected IUserRepository UserRepository { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IJSRuntime js { get; set; }
        private JsInteropClasses jsClass;
        [Parameter]
        public string UserId { get; set; }
        public CustomUserClaimsViewModel UserClaimViewModel { get; set; } = new CustomUserClaimsViewModel();
        protected override async Task OnInitializedAsync()
        {
            await loadData();
            jsClass = new(js);
        }
        private async Task loadData()
        {
            UserClaimViewModel = (await UserRepository.EditClaimsInUserAsync(UserId));
        }


        public async Task HandleValidSubmit()
        {
            var result = await UserRepository.EditClaimsInUserAsync(UserClaimViewModel);

            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo($"/UserManagement/EditUser/{UserId}");
            }
            else
            {
                await jsClass.Alert(result.Message);
            }
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo($"/UserManagement/EditUser/{UserId}");
        }
    }
}
