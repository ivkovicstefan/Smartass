using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.Dictionary
{
    /// <summary>
    /// A static class which contains a list of all stored procedures in all Smartass databases.
    /// TO-DO: Move stored procedure names to web.config
    /// </summary>
    public static class StoredProcedureDictionary
    {
        #region Smartass.Group.DB

        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupCreate = "[GRP].[USP_Group_Create]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupListByUserGet = "[GRP].[USP_GroupListByUser_Get]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupListByUserSearch = "[GRP].[USP_GroupListByUser_Search]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupInviteListByUserGet = "[GRP].[USP_GroupInviteListByUser_Get]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupInviteSend = "[GRP].[USP_GroupInvite_Send]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupInviteCancel = "[GRP].[USP_GroupInvite_Cancel]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupInviteRespond = "[GRP].[USP_GroupInvite_Respond]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupRequestListByGroupAdminGet = "[GRP].[USP_GroupRequestListByGroupAdmin_Get]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupRequestSend = "[GRP].[USP_GroupRequest_Send]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupRequestCancel = "[GRP].[USP_GroupRequest_Cancel]";
        /// <summary>
        /// 
        /// </summary>
        public const string UspGroupRequestRespond = "[GRP].[USP_GroupRequest_Respond]";

        #endregion
    }
}
