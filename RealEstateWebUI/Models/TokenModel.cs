using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyMVC.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }
    }
}