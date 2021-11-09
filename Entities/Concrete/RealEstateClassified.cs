using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class RealEstateClassified:IEntity
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
