using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ourWinch.Models.Account;


namespace ourWinch.Tests.AccountController
{
    public class AccountControllerTests
    {
        // Arrange part: Setup the controller with necessary dependencies mocked.
        private readonly global::AccountController _controller;

        public AccountControllerTests()
        {
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null);

            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            var notyfServiceMock = new Mock<INotyfService>();

            _controller = new global::AccountController(
                userManagerMock.Object,
                signInManagerMock.Object,
                roleManagerMock.Object,
                notyfServiceMock.Object);
        }

        [Fact]
        public void Login_GET_ReturnsViewResult()
        {
            // Act part: Call the action.
            var result = _controller.Login();

            // Assert part: Check that a view is returned.
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName); // The view name should be null if it's defaulting to the action name
            Assert.Null(viewResult.Model); // No model is expected as the action doesn't specify a model.
        }

        
    }
}