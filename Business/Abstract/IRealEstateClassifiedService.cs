using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRealEstateClassifiedService
    {
        IResult Add(RealEstateClassified realEstateClassified);
        IResult Delete(RealEstateClassified realEstateClassified);
        IResult Update(RealEstateClassified realEstateClassified);
        IDataResult<List<RealEstateClassifiedDetailsDto>> GetAllRealEstateClassifiedDetailsDtoByUserIdAndStatus(int userId,bool status);
        IDataResult<List<RealEstateClassifiedDetailsDto>> GetAllRealEstateClassifiedDetailsDtoByUserId(int userId);
        IDataResult<RealEstateClassified> GetRealEstateClassifiedById(int id);
        IResult ChangeRealEstateClassifiedStatus(RealEstateClassified realEstateClassified);
        IDataResult<List<RealEstateClassifiedDetailsDto>> GetAllRealEstateClassifiedDetailsDtoBySearchParametrs(SearchFilterForRealEstateClassified searchFilterForRealEstateClassified);
    }
}
