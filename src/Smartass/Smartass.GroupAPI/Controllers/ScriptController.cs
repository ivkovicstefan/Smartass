using Microsoft.AspNetCore.Mvc;
using Smartass.Group.BLL.Contract;

namespace Smartass.Group.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptController : ControllerBase
    {
        #region Private Fields

        private readonly IScriptLogic _scriptLogic;

        #endregion

        #region Constructor
        public ScriptController(IScriptLogic scriptLogic)
        {
            _scriptLogic = scriptLogic;
        }

        #endregion

        #region Endpoints

        [HttpGet]
        [Route("Ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        #endregion

    }
}
