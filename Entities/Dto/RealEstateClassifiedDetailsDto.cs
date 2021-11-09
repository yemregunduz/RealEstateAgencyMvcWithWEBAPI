using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class RealEstateClassifiedDetailsDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RealEstateClassifiedTitle { get; set; }
        public string CompanyName { get; set; }
        public int NumberOfRoomId { get; set; }
        public string RoomCount { get; set; }
        public string FullAddress { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; }
        public int BuildingAge { get; set; }
        public decimal SquareMeters { get; set; }
        public int Floor { get; set; }
        public decimal RealEstatePrice { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
