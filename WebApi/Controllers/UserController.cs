using System.Threading.Tasks;
using LogicHandlers.ILogicHandlers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogicHandler _userLogicHandler;
        public UserController(IUserLogicHandler userLogicHandler)
        {
            _userLogicHandler = userLogicHandler;
        }

        [HttpGet("")]
        public async Task<IActionResult> TestGetList()
        {
            return Ok(await _userLogicHandler.Test_GetLists());
        }
    }
}