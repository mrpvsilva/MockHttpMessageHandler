using MockHttpMessageHandler.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MockHttpMessageHandler.WebApi.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetTodos();
    }
}
