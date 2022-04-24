using Smartass.Core.Model.DTO.Common;
using System;
using System.Collections.Generic;
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
    }
}
