using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyMVC.Models
{
    public class UserForRegisterDtoModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string UserType { get; set; }
        public bool Status { get; set; }
    }
}