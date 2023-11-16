using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ourWinch.Controllers;
using ourWinch.Models.Account;
using Xunit;

namespace ourWinch.Tests.AccountController
{
    public class Logout
    {
        // Arrange: Mock necessary services and create an instance of the controller
        private readonly global::AccountController _controller;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<INotyfService> _notyfServiceMock;


        public Logout()
        {
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                contextAccessorMock.Object,
                userPrincipalFactoryMock.Object,
                null, null, null, null);

            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            _notyfServiceMock = new Mock<INotyfService>();

            _controller = new global::AccountController(
                userManagerMock.Object,
                _signInManagerMock.Object,
                roleManagerMock.Object,
                _notyfServiceMock.Object);
        }

        [Fact]
        public async Task Logout_RedirectsToLogin()
        {
            // Act: Call the Logout method
            var result = await _controller.Logout();

            // Assert: Verify the SignOutAsync method was called on the SignInManager
            _signInManagerMock.Verify(s => s.SignOutAsync(), Times.Once);

            // Assert: Check that the result is a RedirectToActionResult with the correct action and controller
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
            Assert.Equal("Account", redirectToActionResult.ControllerName);
        }
    }
}
