using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Smartass.Core.Model.Dictionary;
using Smartass.Core.Model.DTO.Common;

namespace Smartass.Core.Model.Common
{
    /// <summary>
    /// Represents an abstraction of the SQL Server stored procedure execution
    /// </summary>
    public class StoredProcedure
    {
        #region Private Fields

        private readonly string _storedProcedureName;
        private readonly List<SqlParameter> _inputParameters;
        private List<SqlParameter> outputParameters;
        private readonly string _connectionString;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for the complete initialization.
        /// </summary>
        /// <param name="storedProcedureName">Use <see cref="StoredProcedureDictionary">StoredProcedureDictionary</see> as an argument value</param>
        /// <param name="inputParameters">List of input parameters for the stored procedure</param>
        /// <param name="outputParameters">List of output parameters for the stored procedure</param>
        /// <param name="connectionString">Use <see cref="DatabaseConnectionStringDictionary">DatabaseConnectionStringDictionary</see> as an argument value</param>
        public StoredProcedure(string storedProcedureName, 
                               List<SqlParameter> inputParameters, 
                               List<SqlParameter> outputParameters, 
                               string connectionString)
        {
            _storedProcedureName = storedProcedureName;
            _inputParameters = (inputParameters != null) ? inputParameters : new List<SqlParameter>();
            this.outputParameters = outputParameters;
            _connectionString = connectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes stored procedure and returns one or more tables as a result.
        /// </summary>
        /// <returns></returns>
        public StoredProcedureResponseDTO ExecuteReader()
        {
            StoredProcedureResponseDTO result = new StoredProcedureResponseDTO();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(_storedProcedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter parameter in _inputParameters)
                        {
                            sqlCommand.Parameters.Add(parameter);
                        }

                        foreach (SqlParameter parameter in outputParameters)
                        {
                            parameter.Direction = ParameterDirection.Output;
                            sqlCommand.Parameters.Add(parameter);
                        }

                        sqlConnection.Open();
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                        DataSet dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        sqlConnection.Close();
                        
                        foreach (DataTable table in dataSet.Tables)
                        {
                            result.DataTables.Add(table);
                        }

                        foreach (SqlParameter parameter in sqlCommand.Parameters)
                        {
                            if(parameter.Direction == ParameterDirection.Output)
                            {
                                result.OutputParameters.Add(parameter);
                            }

                            if (parameter.ParameterName == "IsSuccessful")
                            {
                                result.IsSuccessful = Convert.ToBoolean(parameter.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.ResponseText = ex.Message;
                // TO-DO: Implement ErrorLog
                throw;
            }

            return result;
        }

        /// <summary>
        /// Executes stored procedure with no tables returned.
        /// </summary>
        /// <returns></returns>
        public StoredProcedureResponseDTO ExecuteNonQuery()
        {
            StoredProcedureResponseDTO result = new StoredProcedureResponseDTO();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(_storedProcedureName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter parameter in _inputParameters)
                        {
                            sqlCommand.Parameters.Add(parameter);
                        }

                        foreach (SqlParameter parameter in outputParameters)
                        {
                            parameter.Direction = ParameterDirection.Output;
                            sqlCommand.Parameters.Add(parameter);
                        }

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();

                        foreach (SqlParameter parameter in sqlCommand.Parameters)
                        {
                            if (parameter.Direction == ParameterDirection.Output)
                            {
                                result.OutputParameters.Add(parameter);
                            }

                            if (parameter.ParameterName == "IsSuccessful")
                            {
                                result.IsSuccessful = Convert.ToBoolean(parameter.Value);
                            }

                            if (parameter.ParameterName == "ResponseText")
                            {
                                result.ResponseText = parameter.Value.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = ex.Message;
                // TO-DO: Implement ErrorLog
                throw;
            }

            return result;
        }

        #endregion
    }
}
