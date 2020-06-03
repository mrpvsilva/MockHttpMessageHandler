using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MockHttpMessageHandler.WebApi;
using MockHttpMessageHandler.WebApi.Services;
using Moq;
using System;
using System.Net.Http;

namespace MockHttpMessageHandler.Tests
{
    public class TestFixture : WebApplicationFactory<Startup>
    {
        public Mock<HttpMessageHandler> HttpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services
                .AddHttpClient<ITodoService, TodoService>(x => x.BaseAddress = new Uri("http://localhost:3000"))
                .ConfigurePrimaryHttpMessageHandler(x => HttpMessageHandlerMock.Object);
            });
        }
    }
}
