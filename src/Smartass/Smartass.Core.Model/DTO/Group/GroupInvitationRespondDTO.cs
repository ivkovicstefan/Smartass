using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.DTO.Group
{
    public class GroupInvitationRespondDTO
    {
        public int GroupInviteId { get; set; }
        public bool IsInvitationAccepted { get; set; }
    }
}
