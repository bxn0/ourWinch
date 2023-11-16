using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using ourWinch.Models.Account;

namespace ourWinch.Tests.AccountController
{
    public class LoginView
    {
        // Mocks for the UserManager, SignInManager, and RoleManager
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<INotyfService> _notyfServiceMock;

        // The AccountController that we'll be testing
        private readonly global::AccountController _controller;

        // Constructor for the test class
        public LoginView()
        {
            // Create a mock of the IUserStore to be used by UserManager
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Create a mock of IHttpContextAccessor and IUserClaimsPrincipalFactory to be used by SignInManager
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object, contextAccessorMock.Object, userPrincipalFactoryMock.Object, null, null, null, null);

            // Create a mock of the IRoleStore to be used by RoleManager
            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                roleStoreMock.Object, null, null, null, null);

            _notyfServiceMock = new Mock<INotyfService>();

            // Instantiate the AccountController with the mocked dependencies
            _controller = new global::AccountController(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _roleManagerMock.Object,
                _notyfServiceMock.Object);

            // Clear the ModelState to ensure it's valid for tests (remove this if you want to test model validation logic)
            _controller.ModelState.Clear();
        }

        [Fact]
        public async Task Login_ValidModel_RedirectsToDashboardIndex()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Mobilno = "94719352",
                Password = "Icardi1990!",
                RememberMe = false
            };

            // Setup the SignInManager to return a successful result when a password sign-in is attempted
            _signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.TempData = new TempDataDictionary(_controller.ControllerContext.HttpContext, Mock.Of<ITempDataProvider>());
            // Act
            var result = await _controller.Login(model);

            // Assert
            // Verify that the result is a RedirectToActionResult with the correct action and controller names
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
        }

       
    }
}



