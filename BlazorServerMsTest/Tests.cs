using BlazorServer.Pages.UserManagement;
using BlazorServer.Repositories;
using BlazorServer.ViewModels;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;

namespace BlazorServerMsTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void UserManagementTest()
        {
            // Arrange
            var ctx = new Bunit.TestContext();
            var mockService = Substitute.For<IUserRepository>();
            mockService.GetUsersAsync()
                .Returns(
                new List<CustomUserViewModel>() { new CustomUserViewModel { UserId = "84c3a2a2-5d0b-4e13-b655-d4adae9deb2e", UserName = "user@gmail.com", Email = "test@gmail.com" } });
            ctx.Services.AddSingleton<IUserRepository>(mockService);

            // Act
            var cut = ctx.RenderComponent<UserManagement>();
            var element = cut.Find(".card");

            // Assert
            element.MarkupMatches(
                @"<div class=""card mb-3 w-25""><div class=""card-header"">User Id : 84c3a2a2-5d0b-4e13-b655-d4adae9deb2e</div>
	                <div class=""card-body""><h5 class=""card-title"">user@gmail.com</h5></div>
	                <div class=""card-footer""><button type=""button"" class=""btn btn-primary"">
			                編輯使用者
		                </button>
		                <button type=""button"" class=""btn btn-danger"">
			                刪除使用者
		                </button>
	                </div>
                </div>");
        }
    }
}
