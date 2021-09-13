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

        public async ValueTask<bool> Confirm(string title)
        {
            bool confirm = await js.InvokeAsync<bool>("SweetConfirm", $"是否確定刪除日誌{title}？");
            return confirm;
        }

        public void Dispose()
        {
        }
    }
}
