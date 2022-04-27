using Smartass.Core.Model.DTO.Common;
using Smartass.Core.Model.DTO.Group;
using Smartass.Group.DAL.Contract;
using Smartass.Core.Utility.Abstraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Smartass.Core.Model.Dictionary;

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
                                                                      inputParams,
                                                                      outputParams,
                                                                      ConnectionStringDictionary.SmartassGroupDBConnectionString);

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

        #endregion
    }
}
