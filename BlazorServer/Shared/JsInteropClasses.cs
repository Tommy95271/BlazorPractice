using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorServer.Shared
{
    public class JsInteropClasses
    {
        private readonly IJSRuntime js;

        public JsInteropClasses(IJSRuntime js)
        {
            this.js = js;
        }

        public async ValueTask<bool> Confirm(string jsonString)
        {
            bool confirm = await js.InvokeAsync<bool>("SweetConfirm", jsonString);
            return confirm;
        }

        public async Task Alert(string message)
        {
            await js.InvokeAsync<string>("SweetAlert", message);
        }

        public void Dispose()
        {
        }
    }
}
