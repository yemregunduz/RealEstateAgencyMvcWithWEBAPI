using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class RealEstateClassifiedImage:IEntity
    {
        public int Id { get; set; }
        public int RealEstateClassifiedId { get; set; }
        public string RealEstateImagePath { get; set; }
        public DateTime Date { get; set; }
        public RealEstateClassifiedImage()
        {
            Date = DateTime.Now;
        }
    }
}
