using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyMVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string CompanyName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}