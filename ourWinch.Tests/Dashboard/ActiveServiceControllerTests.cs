/*
 *
 * using Microsoft.AspNetCore.Mvc;
 
using Microsoft.EntityFrameworkCore;
using Moq;
using ourWinch.Models.Dashboard;

namespace ourWinch.Tests.Dashboard
{

    public class ActiveServiceControllerTests
    {
        private readonly Mock<AppDbContext> _contextMock;
        private readonly ActiveServiceController _controller;
        private readonly Mock<DbSet<ServiceOrder>> _serviceOrderDbSetMock;
        private readonly Mock<DbSet<ActiveService>> _activeServiceDbSetMock;
        

        public ActiveServiceControllerTests()
        {
            _contextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
            _serviceOrderDbSetMock = new Mock<DbSet<ServiceOrder>>();
            _activeServiceDbSetMock = new Mock<DbSet<ActiveService>>();

            // Mock the ServiceOrders DbSet
            _contextMock.Setup(x => x.ServiceOrders).Returns(_serviceOrderDbSetMock.Object);
            // Mock the ActiveServices DbSet
            _contextMock.Setup(x => x.ActiveServices).Returns(_activeServiceDbSetMock.Object);

            _controller = new ActiveServiceController(_contextMock.Object);
        }

        [Fact]
        public async Task ActiveService_PageNumberIsOne_ReturnsViewResultWithServiceOrders()
        {
            // Arrange
            int testPageNumber = 1;
            var serviceOrdersData = Enumerable.Range(1, 20).Select(i => new ServiceOrder()).AsQueryable();

            _serviceOrderDbSetMock.As<IQueryable<ServiceOrder>>().Setup(m => m.Provider).Returns(serviceOrdersData.Provider);
            _serviceOrderDbSetMock.As<IQueryable<ServiceOrder>>().Setup(m => m.Expression).Returns(serviceOrdersData.Expression);
            _serviceOrderDbSetMock.As<IQueryable<ServiceOrder>>().Setup(m => m.ElementType).Returns(serviceOrdersData.ElementType);
            _serviceOrderDbSetMock.As<IQueryable<ServiceOrder>>().Setup(m => m.GetEnumerator()).Returns(serviceOrdersData.GetEnumerator());

            // Act
            var result = await _controller.ActiveService(testPageNumber);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ServiceOrder>>(viewResult.Model);
            Assert.Equal(ActiveServiceController.PageSizeValue, model.Count());
        }

        [Fact]
        public async Task DeleteService_ServiceExists_ReturnsNoContentResult()
        {
            // Arrange
            var serviceId = 1;
            var activeService = new ActiveService { Id = serviceId };
            _activeServiceDbSetMock.Setup(x => x.FindAsync(serviceId)).ReturnsAsync(activeService);

            // Act
            var result =  _controller.DeleteService(serviceId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        // Additional tests would go here to cover more scenarios.
    }
}
*/