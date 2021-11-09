using RealEstateAgencyMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateWebUI.Models
{
    public class MultipleViewModel
    {
        public IEnumerable<DistrictViewModel> Districts { get; set; }
        public IEnumerable<CityViewModel> Cities { get; set; }
        public IEnumerable<NeighborhoodViewModel> Neighborhoods { get; set; }
        public IEnumerable<NumberOfRoomViewModel> NumberOfRooms { get; set; }
        public RealEstateClassifiedModel RealEstateClassifiedModel { get; set; }
    }
    
}