using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Neighborhood : IEntity
    {
        public int Id { get; set; }
        public string NeighborhoodName { get; set; }
        public int DisctrictId { get; set; }
    }
}
