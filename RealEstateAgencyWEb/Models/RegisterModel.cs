using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAgencyWEb.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string CompanyName { get; set; }
        public bool Status { get; set; }
    }
}
