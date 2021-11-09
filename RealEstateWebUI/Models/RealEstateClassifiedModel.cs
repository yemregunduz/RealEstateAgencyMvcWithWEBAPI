using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateWebUI.Models
{
    public class RealEstateClassifiedModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RealEstateClassifiedTitle { get; set; }
        public int NumberOfRoomId { get; set; }
        public int NeighborhoodId { get; set; }
        public string FullAddress { get; set; }
        public int BuildingAge { get; set; }
        public decimal SquareMeters { get; set; }
        public int Floor { get; set; }
        public decimal RealEstatePrice { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}