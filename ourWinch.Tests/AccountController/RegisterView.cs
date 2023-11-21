using AspNetCoreHero.ToastNotification.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ourWinch.Models.Account;


public class RegisterView
{
    // Mocks for the UserManager, SignInManager, and RoleManager are created
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock = MockUserManager();
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock = MockSignInManager();
    private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock = MockRoleManager();
    private readonly Mock<INotyfService> _notyfServiceMock;

    public RegisterView()
    {
      

        // Use 'this' to reference the current instance's fields
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(),
            null, null, null, null, null, null, null, null);

       

        // You need to create signInManagerMock here instead of using field initializer
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            null, null, null, null);

        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
            Mock.Of<IRoleStore<IdentityRole>>(),
            null, null, null, null);

        _notyfServiceMock = new Mock<INotyfService>(MockBehavior.Strict);
    }
    [Fact]
    public async Task Register_Post_ValidModel_CreatesUserAndReturnsRedirect()
    {

        
        _notyfServiceMock.Setup(s => s.Success(It.IsAny<string>(), It.IsAny<int?>())).Verifiable();
        // Arrange
        var model = new RegisterViewModel
        {
            // Populate the model with valid data required for registration
        };
        // Setup the UserManager to return a successful result when CreateAsync is called
        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Instantiate the controller with the mocked dependencies
        var controller = new AccountController(_userManagerMock.Object, _signInManagerMock.Object, _roleManagerMock.Object,_notyfServiceMock.Object);

        // Act
        var result = await controller.Register(model);

        // Assert
        // The result should be a redirect to the Dashboard's Index action
        result.Should().BeOfType<RedirectToActionResult>();
        var redirectToActionResult = result as RedirectToActionResult;
        _notyfServiceMock.Setup(s => s.Success(It.IsAny<string>(), It.IsAny<int?>())).Verifiable();

        redirectToActionResult.ControllerName.Should().Be("Dashboard");
        redirectToActionResult.ActionName.Should().Be("Index");
        // Verify that CreateUser was called once
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    }

    // More tests go here, following the same structure as the one above

    // Helper method to create a mock UserManager
    private static Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        return new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);
    }

    // Helper method to create a mock SignInManager
    private static Mock<SignInManager<ApplicationUser>> MockSignInManager()
    {
        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        return new Mock<SignInManager<ApplicationUser>>();

    }

    // Helper method to create a mock RoleManager
    private static Mock<RoleManager<IdentityRole>> MockRoleManager()
    {
        var roleStore = new Mock<IRoleStore<IdentityRole>>();
        return new Mock<RoleManager<IdentityRole>>(
            roleStore.Object, null, null, null, null);
    }
}
