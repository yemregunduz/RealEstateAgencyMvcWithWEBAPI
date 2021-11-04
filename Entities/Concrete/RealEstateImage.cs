using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class RealEstateImage:IEntity
    {
        public int Id { get; set; }
        public int RealEstateId { get; set; }
        public string RealEstateImagePath { get; set; }
        public DateTime Date { get; set; }
        public RealEstateImage()
        {
            Date = DateTime.Now;
        }
    }
}
