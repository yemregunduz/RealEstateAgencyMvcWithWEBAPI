using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyMVC.Models
{
    public class ListResponseModel<T> : ResponseModel
    {
        public IEnumerable<T> Data { get; set; }
    }
}