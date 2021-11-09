using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebUI.Models
{
    public class UserForChangePasswordDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}