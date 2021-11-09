using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class SearchFilterForRealEstateClassified
    {
        public string RealEstateClassifiedTitle { get; set; }
        public int? BuildingAge { get; set; }
        public decimal? SquareMeters { get; set; }
        public int? NumberOfRoomId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public decimal? RealEstateMin { get; set; }
        public decimal? RealEstatePriceMax { get; set; }
        public bool? Status { get; set; }

    }
}
