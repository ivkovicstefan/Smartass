using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smartass.GroupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        #region Private Fields

        #endregion

        #region Constructor
        public GroupController()
        {

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
