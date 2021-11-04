using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDistrictService
    {
        IResult Add(District district);
        IResult Delete(District district);
        IResult Update(District district);
        IDataResult<List<District>> GetAll();
        IDataResult<List<District>> GetAllDistrictsByCityId(int cityId);
    }
}
