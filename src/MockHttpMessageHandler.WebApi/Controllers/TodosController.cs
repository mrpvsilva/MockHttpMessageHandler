using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MockHttpMessageHandler.WebApi.Services;

namespace MockHttpMessageHandler.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodosController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await _service.GetTodos());
        }
    }
}