using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Smartass.Core.Model.DTO.Common
{
    public class StoredProcedureResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseText { get; set; }
        public List<DataTable> DataTables { get; set; }
        public List<SqlParameter> OutputParameters { get; set; }

        public StoredProcedureResponseDTO()
        {
            IsSuccessful = false;
            OutputParameters = new List<SqlParameter>();
        }
    }
}
