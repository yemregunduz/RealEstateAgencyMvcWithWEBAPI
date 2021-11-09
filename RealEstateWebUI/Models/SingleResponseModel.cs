using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyMVC.Models
{
    public class SingleResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }
    }
}