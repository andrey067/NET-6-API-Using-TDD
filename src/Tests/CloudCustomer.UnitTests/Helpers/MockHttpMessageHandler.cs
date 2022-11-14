using System.Net;
using System.Net.Http.Headers;
using CloudCustomer.API.Domain;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CloudCustomer.UnitTests.Helpers
{
    internal static class MockHttpMessageHandler<T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResouceList(List<T> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                    ItExpr.IsAny<HttpRequestMessage>(),
                                                                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(mockResponse);

            return handlerMock;
        }

        internal static Mock<HttpMessageHandler> SetupBasicGetResouceList(List<User> expectedResponse, string endpoint)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                    ItExpr.IsAny<HttpRequestMessage>(),
                                                                    ItExpr.IsAny<CancellationToken>())
                                                                    .ReturnsAsync(mockResponse);

            return handlerMock;
        }

        internal static Mock<HttpMessageHandler> SetupReturn404()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("")
            };

            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                                                                    ItExpr.IsAny<HttpRequestMessage>(),
                                                                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(mockResponse);

            return handlerMock;
        }
    }
}
