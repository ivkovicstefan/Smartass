using Microsoft.AspNetCore.Mvc;
using Smartass.Group.BLL.Contract;

namespace Smartass.Group.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        #region Private Fields

        private readonly IGroupLogic _groupLogic;

        #endregion

        #region Constructor
        public GroupController(IGroupLogic groupLogic)
        {
            _groupLogic = groupLogic;
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
