using BlazorServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorServer.Shared
{
    public partial class FileUpload
    {
        [Inject] protected IJSRuntime js { get; set; }
        [Inject] protected IWebHostEnvironment env { get; set; }
        private JsInteropClasses jsClass;

        public List<string> ImageList = new List<string>();
        public IReadOnlyList<IBrowserFile> ImgFiles;
        public string ImgSrc;

        protected override Task OnInitializedAsync()
        {
            jsClass = new(js);
            return base.OnInitializedAsync();
        }

        public async Task OnChange(InputFileChangeEventArgs e)
        {
            ImageList = new List<string>();
            string format = "image/jpeg";
            ImgFiles = e.GetMultipleFiles();
            foreach (var file in ImgFiles)
            {
                var imageFile = await file.RequestImageFileAsync(format, 1280, 960);
                using var fileStream = imageFile.OpenReadStream();
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);
                ImgSrc = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
                ImageList.Add(ImgSrc);
            }

        }
        public async Task OnSubmit()
        {
            SweetConfirmViewModel sweetConfirm = new SweetConfirmViewModel()
            {
                RequestTitle = "是否確定上傳圖片？",
                ResponseTitle = "上傳成功",
            };
            string jsonString = JsonSerializer.Serialize(sweetConfirm);
            bool result = await jsClass.Confirm(jsonString);
            if (result && ImgFiles.Any())
            {
                long maxFileSize = 1024 * 1024 * 15;
                string folder = $@"{env.WebRootPath}\images";
                foreach (var file in ImgFiles)
                {
                    using (var stream = file.OpenReadStream(maxFileSize))
                    {
                        Directory.CreateDirectory(folder);
                        var path = $@"{env.WebRootPath}\images\{file.Name}";
                        FileStream fs = File.Create(path);
                        await stream.CopyToAsync(fs);
                        stream.Close();
                        fs.Close();
                    }
                }
            }
        }
    }
}
