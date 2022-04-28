using Smartass.Core.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.DTO.Group
{
    public class GroupInvitationListItemDTO
    {
        public GroupListItemDTO Group { get; set; }
        public UserListItemDTO SentFromUser { get; set; }
        public DateTime CreatedDateUTC { get; set; }
    }
}
