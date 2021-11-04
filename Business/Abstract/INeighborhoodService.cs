using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INeighborhoodService
    {
        IResult Add(Neighborhood neighborhood);
        IResult Delete(Neighborhood neighborhood);
        IResult Update(Neighborhood neighborhood);
        IDataResult<List<Neighborhood>> GetAll();
        IDataResult<List<Neighborhood>> GetAllNeighborhoodsByDistrictId(int districtId);
    }
}
