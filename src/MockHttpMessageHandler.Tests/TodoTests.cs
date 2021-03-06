﻿using FluentAssertions;
using MockHttpMessageHandler.WebApi.Models;
using Moq;
using Moq.Protected;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MockHttpMessageHandler.Tests
{
    public class TodoTests : IClassFixture<TestFixture>
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _client;


        public TodoTests(TestFixture fixture)
        {
            _httpMessageHandlerMock = fixture.HttpMessageHandlerMock;
            _client = fixture.CreateClient();
        }


        [Fact(DisplayName = "Get Todos")]
        public async Task GetTodos()
        {
            //arrange
            _httpMessageHandlerMock
                    .Protected()
                    // Setup the PROTECTED method to mock
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    // prepare the expected response of the mocked http call
                    .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("[{\"userId\": 1, \"id\": 1,\"title\":\"delectus aut autem\", \"completed\": false  }]", Encoding.UTF8, "application/json")
                    })
                    .Verifiable();
            //act
            var response = await _client.GetAsync("api/todos");
            var data = await response.Content.ReadFromJsonAsync<IEnumerable<Todo>>();
            //assert
            response.EnsureSuccessStatusCode();
            data
                .Should()
                .HaveCount(1);

        }
    }
}
