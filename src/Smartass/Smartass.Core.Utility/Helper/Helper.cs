using Smartass.Core.Model.DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Smartass.Core.Utility.Helper
{
    public static class Helper
    {
        #region Validation Helpers

        public static string ValidateStringFields(FieldStringDTO[] fields, out bool isSuccessful)
        {
            string errorMessage = String.Empty;
            isSuccessful = true;

            foreach(FieldStringDTO field in fields)
            {
                if(String.IsNullOrEmpty(field.Value))
                {
                    errorMessage += $"{field.FieldName} is required.\n";
                    isSuccessful = false;
                }
            }

            return errorMessage;
        }

        #endregion

        #region Conversion Helpers

        public static DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                dataTable.Columns.Add(property.Name);
            }

            foreach (T item in items)
            {
                var values = new object[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion
    }
}
