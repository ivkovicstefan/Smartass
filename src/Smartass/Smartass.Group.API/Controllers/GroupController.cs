using Microsoft.AspNetCore.Mvc;
using Smartass.Core.Model.DTO.Common;
using Smartass.Core.Model.DTO.Group;
using Smartass.Group.BLL.Contract;
using System;
using System.Net;

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

        [HttpPost]
        [Route("CreateGroup")]
        public IActionResult CreateGroup([FromBody] GroupCreationDTO groupCreationDTO)
        {
            ResponseDTO result;

            try
            {
                result = _groupLogic.CreateGroup(groupCreationDTO);

                switch (result.ResponseCode)
                {
                    case (int)HttpStatusCode.OK:
                        return Ok(result);
                    case (int)HttpStatusCode.BadRequest:
                        return BadRequest(result);
                    default:
                        return StatusCode((int)HttpStatusCode.InternalServerError, result);

                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO();
                return StatusCode((int)HttpStatusCode.InternalServerError, result);
                //TO-DO: Implement Errorlog
            }
        }

        [HttpGet]
        [Route("GetGroupListByUser/{userId:int}")]
        public IActionResult GetGroupListByUser(int userId)
        {
            ResponseDTO result;

            try
            {
                result = _groupLogic.GetGroupListByUser(userId);

                switch(result.ResponseCode)
                {
                    case (int)HttpStatusCode.OK:
                        return Ok(result);
                    default:
                        return StatusCode((int)HttpStatusCode.InternalServerError, result);
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO();
                return StatusCode((int)HttpStatusCode.InternalServerError, result);
                //TO-DO: Implement Errorlog
            }

        }

        [HttpGet]
        [Route("SearchGroupListByUser/{searchText}/{userId:int}")]
        public IActionResult SearchGroupListByUser(string searchText, int userId)
        {
            ResponseDTO result;

            try
            {
                result = _groupLogic.SearchGroupListByUser(searchText, userId);

                switch (result.ResponseCode)
                {
                    case (int)HttpStatusCode.OK:
                        return Ok(result);
                    default:
                        return StatusCode((int)HttpStatusCode.InternalServerError, result);
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO();
                return StatusCode((int)HttpStatusCode.InternalServerError, result);
                //TO-DO: Implement Errorlog
            }

        }

        [HttpPost]
        [Route("SendGroupInvite")]
        public IActionResult SendGroupInvite(GroupInvitationDTO groupInvitationDTO)
        {
            ResponseDTO result;

            try
            {
                result = _groupLogic.SendGroupInvite(groupInvitationDTO);

                switch (result.ResponseCode)
                {
                    case (int)HttpStatusCode.OK:
                        return Ok(result);
                    default:
                        return StatusCode((int)HttpStatusCode.InternalServerError, result);

                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO();
                return StatusCode((int)HttpStatusCode.InternalServerError, result);
                //TO-DO: Implement Errorlog
            }
        }

        [HttpGet]
        [Route("GetGroupInviteListByUser/{userId:int}")]
        public IActionResult GetGroupInviteListByUser(int userId)
        {
            ResponseDTO result;

            try
            {
                result = _groupLogic.GetGroupInviteListByUser(userId);

                switch (result.ResponseCode)
                {
                    case (int)HttpStatusCode.OK:
                        return Ok(result);
                    default:
                        return StatusCode((int)HttpStatusCode.InternalServerError, result);
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO();
                return StatusCode((int)HttpStatusCode.InternalServerError, result);
                //TO-DO: Implement Errorlog
            }

        }

        #endregion
    }
}
