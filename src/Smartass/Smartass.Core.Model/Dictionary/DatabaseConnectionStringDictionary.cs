using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Smartass.Core.Model.Dictionary
{
    /// <summary>
    /// 
    /// </summary>
    public static class DatabaseConnectionStringDictionary
    {
        /// <summary>
        /// 
        /// </summary>
        public static string SmartassGroupDBConnectionString 
            = ConfigurationManager.ConnectionStrings["Smartass.Group.DB"].ToString();
    }
}
