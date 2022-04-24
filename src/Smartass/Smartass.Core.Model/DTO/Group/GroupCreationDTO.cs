using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.DTO.Group
{
    public class GroupCreationDTO
    {
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
