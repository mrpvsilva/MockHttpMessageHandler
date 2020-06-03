using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MockHttpMessageHandler.WebApi;
using MockHttpMessageHandler.WebApi.Services;
using Moq;
using System;
using System.Linq;
using System.Net.Http;

namespace MockHttpMessageHandler.Tests
{
    public class TestFixture : WebApplicationFactory<Startup>
    {
        public Mock<HttpMessageHandler> HttpMessageHandlerMock { get; private set; }

        public TestFixture()
        {
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ITodoService registration.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITodoService));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services
                .AddHttpClient<ITodoService, TodoService>(x => x.BaseAddress = new Uri("http://localhost:3000"))
                .ConfigurePrimaryHttpMessageHandler(x => HttpMessageHandlerMock.Object);
            });
        }
    }
}
