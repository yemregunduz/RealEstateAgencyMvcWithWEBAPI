using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebUI.Models
{
    public class NeighborhoodViewModel
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string NeighborhoodName { get; set; }
    }
}