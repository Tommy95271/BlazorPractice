using BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class PostBase : ComponentBase
    {
        [Parameter]
        public PostModel Post { get; set; }
        public EditContext editContext;

        protected override Task OnInitializedAsync()
        {
            editContext = new EditContext(Post);
            editContext.SetFieldCssClassProvider(new CustomFieldClassProvider());
            return base.OnInitializedAsync();
        }
        protected void TitleChanged(string value)
        {
            Post.Title = value;
        }

        [CascadingParameter(Name = "ColorStyle")]
        public string ColorStyle { get; set; }

        [Parameter]
        public EventCallback<int> getPostId { get; set; }
        [Parameter]
        public Action<int> getPostIdForDelegate { get; set; }

        protected void returnPostId()
        {
            //getPostId.InvokeAsync(Post.PostId);
            getPostIdForDelegate.Invoke(Post.PostId);
        }
    }
}
