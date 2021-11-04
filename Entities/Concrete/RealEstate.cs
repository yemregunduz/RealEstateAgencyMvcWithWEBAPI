using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class RealEstate:IEntity
    {
        public int Id { get; set; }
        public int RoomCountId { get; set; }
        public int AddressId { get; set; }
        public int BuildingAge { get; set; }
        public decimal SquareMeters { get; set; }
        public int Floor { get; set; }
        public decimal RealEstatePrice { get; set; }
        
    }
}
