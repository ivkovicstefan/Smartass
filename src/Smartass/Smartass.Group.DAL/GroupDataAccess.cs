using Smartass.Core.Model.DTO.Common;
using Smartass.Core.Model.DTO.Group;
using Smartass.Group.DAL.Contract;
using Smartass.Core.Utility.Abstraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Smartass.Core.Model.Dictionary;
using System.Linq;
using Smartass.Core.Model.DTO.User;

namespace Smartass.Group.DAL
{
    public class GroupDataAccess : IGroupDataAccess
    {
        #region Private Fields
        #endregion

        #region Constructor

        public GroupDataAccess()
        {

        }

        #endregion

        #region Methods

        public ResponseDTO CreateGroup(GroupCreationDTO groupCreationDTO)
        {
            ResponseDTO result;

            try
            {
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@GroupName", SqlDbType = SqlDbType.NVarChar, Value=groupCreationDTO.GroupName, Size = 40 },
                    new SqlParameter() { ParameterName = "@GroupDescription", SqlDbType = SqlDbType.NVarChar, Value=groupCreationDTO.GroupDescription, Size = 400 },
                    new SqlParameter() { ParameterName = "@CreatedByUserId", SqlDbType = SqlDbType.Int, Value=groupCreationDTO.CreatedByUserId }
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit },
                    new SqlParameter() { ParameterName = "@ResponseText", SqlDbType = SqlDbType.VarChar, Size = 100 },
                    new SqlParameter() { ParameterName = "@GroupId", SqlDbType = SqlDbType.Int }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupCreate,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();
                response = storedProcedure.ExecuteNonQuery();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    ResponseText = response.ResponseText,
                    Object = Convert.ToInt32(response.OutputParameters.Find(param => param.ParameterName == "@GroupId").Value)
                };

            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupCreationError,
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
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Value = userId }
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupListByUserGet,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();
                
                response = storedProcedure.ExecuteReader();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    Object = response.DataTables[0].AsEnumerable().Select(row => new GroupListItemDTO()
                    {
                        GroupId = row.Field<int>("GroupId"),
                        GroupName = row.Field<string>("GroupName"),
                        Image = (row.Field<Byte[]>("ProfileImage")!=null)?Convert.ToBase64String(row.Field<Byte[]>("ProfileImage")):String.Empty,
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError
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
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Value = userId },
                    new SqlParameter() { ParameterName = "@SearchText", SqlDbType = SqlDbType.NVarChar, Value = searchText, Size=40 }
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupListByUserSearch,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();

                response = storedProcedure.ExecuteReader();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    Object = response.DataTables[0].AsEnumerable().Select(row => new GroupListItemDTO()
                    {
                        GroupId = row.Field<int>("GroupId"),
                        GroupName = row.Field<string>("GroupName"),
                        Image = (row.Field<Byte[]>("ProfileImage") != null) ? Convert.ToBase64String(row.Field<Byte[]>("ProfileImage")) : String.Empty,
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError
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
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@GroupIdFrom", SqlDbType = SqlDbType.Int, Value=groupInvitationDTO.GroupIdFrom },
                    new SqlParameter() { ParameterName = "@UserIdFrom", SqlDbType = SqlDbType.Int, Value=groupInvitationDTO.UserIdFrom },
                    new SqlParameter() { ParameterName = "@UserIdTo", SqlDbType = SqlDbType.Int, Value=groupInvitationDTO.UserIdTo }
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit },
                    new SqlParameter() { ParameterName = "@ResponseText", SqlDbType = SqlDbType.VarChar, Size = 100 }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupInviteSend,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();
                response = storedProcedure.ExecuteNonQuery();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    ResponseText = response.ResponseText,
                };

            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.InvitationSendingError,
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
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Value = userId }
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupInviteListByUserGet,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();

                response = storedProcedure.ExecuteReader();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    Object = response.DataTables[0].AsEnumerable().Select(row => new GroupInvitationListItemDTO()
                    {
                        GroupInviteId = row.Field<int>("GroupInviteId"),
                        Group = new GroupListItemDTO()
                        {
                            GroupId = row.Field<int>("GroupId"),
                            GroupName = row.Field<string>("GroupName"),
                            Image = (row.Field<Byte[]>("GroupImage") != null) ? Convert.ToBase64String(row.Field<Byte[]>("GroupImage")) : String.Empty,
                        },

                        SentFromUser = new UserListItemDTO()
                        {
                            UserId = row.Field<int>("UserId"),
                            FirstName = row.Field<string>("FirstName"),
                            LastName = row.Field<string>("LastName"),
                            ProfileImage = (row.Field<Byte[]>("ProfileImage") != null) ? Convert.ToBase64String(row.Field<Byte[]>("ProfileImage")) : String.Empty
                        },

                        CreatedDateUTC = row.Field<DateTime>("CreatedDateUTC")
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError
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
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@UserIdFrom", SqlDbType = SqlDbType.Int, Value=groupRequestDTO.UserIdFrom },
                    new SqlParameter() { ParameterName = "@GroupIdTo", SqlDbType = SqlDbType.Int, Value=groupRequestDTO.GroupIdTo },                
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit },
                    new SqlParameter() { ParameterName = "@ResponseText", SqlDbType = SqlDbType.VarChar, Size = 100 }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupRequestSend,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();
                response = storedProcedure.ExecuteNonQuery();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    ResponseText = response.ResponseText,
                };

            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.InvitationSendingError,
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
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@GroupInviteId", SqlDbType = SqlDbType.Int, Value=groupInvitationRespondDTO.GroupInviteId },
                    new SqlParameter() { ParameterName = "@IsInvitationAccepted", SqlDbType = SqlDbType.Bit, Value=groupInvitationRespondDTO.IsInvitationAccepted },               
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit },
                    new SqlParameter() { ParameterName = "@ResponseText", SqlDbType = SqlDbType.VarChar, Size = 100 }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupInviteRespond,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();
                response = storedProcedure.ExecuteNonQuery();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    ResponseText = response.ResponseText,
                };

            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.InvitationSendingError,
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
                List<SqlParameter> inputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@UserId", SqlDbType = SqlDbType.Int, Value = userId },
                    new SqlParameter() { ParameterName = "@GroupId", SqlDbType = SqlDbType.Int, Value = groupId }
                };

                List<SqlParameter> outputParams = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IsSuccessful", SqlDbType = SqlDbType.Bit }
                };

                StoredProcedure storedProcedure = new StoredProcedure(StoredProcedureDictionary.UspGroupRequestListByGroupAdminGet,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString,
                                                                      inputParams,
                                                                      outputParams);

                StoredProcedureResponseDTO response = new StoredProcedureResponseDTO();

                response = storedProcedure.ExecuteReader();

                result = new ResponseDTO()
                {
                    IsSuccessful = response.IsSuccessful,
                    Object = response.DataTables[0].AsEnumerable().Select(row => new GroupRequestListItemDTO()
                    {
                        GroupRequestId = row.Field<int>("GroupRequestId"),
                        User = new UserListItemDTO()
                        {
                            UserId = row.Field<int>("UserId"),
                            FirstName = row.Field<string>("FirstName"),
                            LastName = row.Field<string>("LastName"),
                            ProfileImage = (row.Field<Byte[]>("ProfileImage") != null) ? Convert.ToBase64String(row.Field<Byte[]>("ProfileImage")) : String.Empty
                        },

                        CreatedDateUTC = row.Field<DateTime>("CreatedDateUTC")
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                result = new ResponseDTO()
                {
                    IsSuccessful = false,
                    ResponseText = MessageDictionary.GroupListGetError
                };

                //TO-DO: Implement Errorlog
                throw;
            }

            return result;
        }

        #endregion
    }
}
