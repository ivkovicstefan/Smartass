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

        #endregion
    }
}
