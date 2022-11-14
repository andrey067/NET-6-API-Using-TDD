using CloudCustomer.API.Config;
using CloudCustomer.API.Domain;
using CloudCustomer.API.Services;
using CloudCustomer.UnitTests.Fixtures;
using CloudCustomer.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomer.UnitTests.Systems.Services
{
    public class TestUserServices
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestsUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponse);
            var httpClint = new HttpClient(handlerMock.Object);
            var endpont = "https://exemple.com";
            var config = Options.Create(new UsersApiOptions { EndPoint = endpont });

            var sut = new UserService(httpClint, config);
            //Act
            await sut.GetAllUsers();

            //Assert
            handlerMock.Protected()
            .Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfUsers()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestsUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponse);
            var httpClint = new HttpClient(handlerMock.Object);
            var endpont = "https://exemple.com";
            var config = Options.Create(new UsersApiOptions { EndPoint = endpont });

            var sut = new UserService(httpClint, config);
            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnEmptyUsers()
        {
            //Arrange            
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClint = new HttpClient(handlerMock.Object);
            var endpont = "https://exemple.com";
            var config = Options.Create(new UsersApiOptions { EndPoint = endpont });

            var sut = new UserService(httpClint, config);
            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCallend_ReturnListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestsUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponse);
            var httpClint = new HttpClient(handlerMock.Object);
            var endpont = "https://exemple.com";
            var config = Options.Create(new UsersApiOptions { EndPoint = endpont });

            var sut = new UserService(httpClint, config);
            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfigureExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestsUsers();
            var endpont = "https://exemple.com";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResouceList(expectedResponse, endpont);
            var httpClint = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                EndPoint = endpont
            });

            var sut = new UserService(httpClint, config);

            var uri = new Uri(endpont);
            //Act
            var result = await sut.GetAllUsers();

            //Assert
            handlerMock.Protected()
            .Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
            ItExpr.IsAny<CancellationToken>());
        }

    }
}
