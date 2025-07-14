using FMDApplication.Response;
using FMDApplication.Services;
using FMDApplication.Services.Interfaces;
using FMDCore.Interfaces;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FMDUnitTests
{
    public class TriviaTest
    {

        [Fact(DisplayName = "Should get fake api trivia data")]
        public async Task GetTriviaAsync_ShouldReturnTriviaApiResponse()
        {
            // Arrange
            var responseObj = new
            {
                ResponseCode = 0,
                Results = new[]
                {
                    new
                    {
                        Category = "General Knowledge",
                        Type = "multiple",
                        Question = "What is the capital of France?",
                        Correct_Answer = "Paris",
                        IncorrectAnswers = new[] { "London", "Berlin", "Madrid" }
                    }
                }
            };

            string json = JsonSerializer.Serialize(responseObj);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(responseObj), Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://test.com/"),
            };

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new TriviaService(mockFactory.Object);

            // Act
            var result = await service.GetTriviaAsync();

            // Assert
            Assert.NotNull(result.Results);
            Assert.Equal(result.Results[0].Correct_Answer, responseObj.Results[0].Correct_Answer);
            Assert.Equal(result.Results[0].Question, responseObj.Results[0].Question);
        }
    }
}
