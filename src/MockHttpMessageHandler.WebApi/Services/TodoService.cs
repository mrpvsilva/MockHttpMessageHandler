using MockHttpMessageHandler.WebApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MockHttpMessageHandler.WebApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _client;

        public TodoService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<Todo>> GetTodos()
        {
            var response = await _client.GetAsync("/todos");
            return await response.Content.ReadFromJsonAsync<IEnumerable<Todo>>();
        }
    }
}
