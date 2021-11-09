using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebUI.Models
{
    public class DistrictViewModel
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int CityId { get; set; }
    }
}