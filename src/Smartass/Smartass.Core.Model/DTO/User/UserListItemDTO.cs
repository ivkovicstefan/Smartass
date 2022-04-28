using System;
using System.Collections.Generic;
using System.Text;

namespace Smartass.Core.Model.DTO.User
{
    public class UserListItemDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
    }
}
