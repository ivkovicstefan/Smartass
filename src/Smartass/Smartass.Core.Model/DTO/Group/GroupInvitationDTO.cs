using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.DTO.Group
{
    public class GroupInvitationDTO
    {
        public int GroupIdFrom { get; set; }
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
        public DateTime CreatedDateUTC { get; set; }
    }
}
