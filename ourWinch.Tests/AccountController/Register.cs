using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ourWinch.Models.Account;
using Microsoft.AspNetCore.Http;


namespace ourWinch.Tests.AccountController
{
    public class Register
    {
        // Mock objects for UserManager, SignInManager, and RoleManager.
        private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> signInManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> roleManagerMock;
        private readonly Mock<INotyfService> _notyfServiceMock;

        // Constructor sets up the mock objects for each test.
        public Register()
        {
            userManagerMock = MockUserManager();
            signInManagerMock = MockSignInManager(userManagerMock);
            roleManagerMock = MockRoleManager();
            _notyfServiceMock = new Mock<INotyfService>();
        }

        // This test ensures that calling the Register method returns the RegisterViewModel.
        [Fact]
        public async Task Register_ReturnsRegisterViewModel_WhenInvoked()
        {
            // Arrange
            var controller = new global::AccountController(
                userManagerMock.Object,
                signInManagerMock.Object,
                roleManagerMock.Object,
                _notyfServiceMock.Object);

            // Act: Call the Register method
            var result = await controller.Register();

            // Assert: Verify that the roles "Admin" and "Ansatt" are created.
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<RegisterViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task Register_CreatesRoles_WhenTheyDoNotExist()
        {
            // Arrange
            roleManagerMock.Setup(x => x.RoleExistsAsync("Admin")).ReturnsAsync(false);
            roleManagerMock.Setup(x => x.RoleExistsAsync("Ansatt")).ReturnsAsync(false);
            roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success);

            var controller = new global::AccountController(
                userManagerMock.Object,
                signInManagerMock.Object,
                roleManagerMock.Object, _notyfServiceMock.Object);

            // Act
            await controller.Register();

            // Assert
            roleManagerMock.Verify(x => x.CreateAsync(It.Is<IdentityRole>(r => r.Name == "Admin")), Times.Once());
            roleManagerMock.Verify(x => x.CreateAsync(It.Is<IdentityRole>(r => r.Name == "Ansatt")), Times.Once());
        }


        // Helper method to create a mock UserManager.
        private static Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object,
                null, null, null, null, null, null, null, null);
        }
        // Helper method to create a mock SignInManager.
        private static Mock<SignInManager<ApplicationUser>> MockSignInManager(Mock<UserManager<ApplicationUser>> userManagerMock)
        {
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                contextAccessorMock.Object,
                userPrincipalFactoryMock.Object,
                null, null, null, null);

            return signInManagerMock;
        }


        // Helper method to create a mock RoleManager.
        private static Mock<RoleManager<IdentityRole>> MockRoleManager()
        {
            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            return new Mock<RoleManager<IdentityRole>>(
                roleStoreMock.Object,
                null, null, null, null);
        }
    }

}
