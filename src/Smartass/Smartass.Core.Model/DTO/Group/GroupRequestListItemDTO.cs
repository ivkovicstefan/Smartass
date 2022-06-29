using Smartass.Core.Model.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.DTO.Group
{
    public class GroupRequestListItemDTO
    {
        public int GroupRequestId { get; set; }
        public UserListItemDTO User { get; set; }
        public DateTime CreatedDateUTC { get; set; }
    }
}
