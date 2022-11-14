using CloudCustomer.API.Controllers;
using CloudCustomer.API.Domain;
using CloudCustomer.API.Services;
using CloudCustomer.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomer.UnitTests.Systems.Controllers
{
    public class TestsUsersControllers
    {
        [Fact]
        public async Task Get_OnSuccess_ResturnsCode200()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(UsersFixture.GetTestsUsers());
            var sut = new UsersController(mockUserService.Object);
            //Act
            var result = (OkObjectResult)await sut.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnNoUsersFound_ResturnsCode404()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(services => services.GetAllUsers()).ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUserService.Object);
            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            mockUserService.Verify(service => service.GetAllUsers(), Times.Once());

        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetAllUsers()).ReturnsAsync(UsersFixture.GetTestsUsers());
            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResulto = (OkObjectResult)result;
            objectResulto.Value.Should().NotBeNull();
            objectResulto.Value.Should().BeOfType<List<User>>();
        }

    }
}
