using Smartass.Core.Model.Dictionary;
using Smartass.Core.Model.DTO.Common;
using Smartass.Core.Model.DTO.Group;
using Smartass.Core.Utility.Helper;
using Smartass.Group.BLL.Contract;
using Smartass.Group.DAL.Contract;
using System;
using System.Net;

namespace Smartass.Group.BLL
{
    public class GroupLogic : IGroupLogic
    {
        #region Private Fields

        private readonly IGroupDataAccess _groupDataAcess;

        #endregion

        #region Constructor
        public GroupLogic(IGroupDataAccess groupDataAcess)
        {
            _groupDataAcess = groupDataAcess;
        }

        #endregion

        #region Methods
        public ResponseDTO CreateGroup(GroupCreationDTO groupCreationDTO)
        {
            ResponseDTO result;

            try
            {
                bool IsValidationSuccessful;
                string errorMessage;
                FieldStringDTO[] fields = { new FieldStringDTO() { FieldName = "Group name", Value = groupCreationDTO.GroupName } };

                errorMessage = Helper.ValidateStringFields(fields, out IsValidationSuccessful);

                if(IsValidationSuccessful)
                {
                    result = _groupDataAcess.CreateGroup(groupCreationDTO);
                    
                    if(result.IsSuccessful)
                    {
                        result.ResponseCode = (int)HttpStatusCode.OK;
                    }
                }
                else
                {
                    result = new ResponseDTO()
                    {
                        IsSuccessful = false,
                        ResponseText = errorMessage,
                        ResponseCode = (int)HttpStatusCode.BadRequest
                    };
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupCreationError
                };

                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        public ResponseDTO GetGroupListByUser(int userId)
        {
            ResponseDTO result;

            try
            {
                result = _groupDataAcess.GetGroupListByUser(userId);

                if(result.IsSuccessful)
                {
                    result.ResponseCode = (int)HttpStatusCode.OK;
                    result.ResponseText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                };
                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        public ResponseDTO SearchGroupListByUser(string searchText, int userId)
        {
            ResponseDTO result;

            try
            {
                result = _groupDataAcess.SearchGroupListByUser(searchText!=null ? searchText : String.Empty, 
                                                               userId);

                if (result.IsSuccessful)
                {
                    result.ResponseCode = (int)HttpStatusCode.OK;
                    result.ResponseText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                };
                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        public ResponseDTO SendGroupInvite(GroupInvitationDTO groupInvitationDTO)
        {
            ResponseDTO result;

            try
            {
                result = _groupDataAcess.SendGroupInvite(groupInvitationDTO);

                if (result.IsSuccessful)
                {
                    result.ResponseCode = (int)HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupCreationError
                };

                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        public ResponseDTO GetGroupInviteListByUser(int userId)
        {
            ResponseDTO result;

            try
            {
                result = _groupDataAcess.GetGroupInviteListByUser(userId);

                if (result.IsSuccessful)
                {
                    result.ResponseCode = (int)HttpStatusCode.OK;
                    result.ResponseText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                };
                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        public ResponseDTO SendGroupRequest(GroupRequestDTO groupRequestDTO)
        {
            ResponseDTO result;

            try
            {
                result = _groupDataAcess.SendGroupRequest(groupRequestDTO);

                if (result.IsSuccessful)
                {
                    result.ResponseCode = (int)HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupCreationError
                };

                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        public ResponseDTO RespondGroupInvite(GroupInvitationRespondDTO groupInvitationRespondDTO)
        {
            ResponseDTO result;

            try
            {
                result = _groupDataAcess.RespondGroupInvite(groupInvitationRespondDTO);

                if (result.IsSuccessful)
                {
                    result.ResponseCode = (int)HttpStatusCode.OK;
                }

            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupCreationError
                };

                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        public ResponseDTO GetGroupRequestListByGroupAdmin(int groupId, int userId)
        {
            ResponseDTO result;

            try
            {
                result = _groupDataAcess.GetGroupRequestListByGroupAdmin(groupId, userId);

                if (result.IsSuccessful)
                {
                    result.ResponseCode = (int)HttpStatusCode.OK;
                    result.ResponseText = String.Empty;
                }
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                };

                //TO-DO: Implement Errorlog
                throw;
            }

            return result;

        }

        #endregion
    }
}
