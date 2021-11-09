using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Caching;
using Core.Apects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{

    public class RealEstateClassifiedManager : IRealEstateClassifiedService
    {
        IRealEstateClassifiedDal _realEstateClassifiedDal;
        public RealEstateClassifiedManager(IRealEstateClassifiedDal realEstateClassifiedDal)
        {
            _realEstateClassifiedDal = realEstateClassifiedDal;
        }
        [SecuredOperation("admin,realestate",Priority=1)]
        [ValidationAspect(typeof(RealEstateClassifiedValidator),Priority=2)]
        [CacheRemoveAspect("IRealEstateClassifiedService.Get")]
        public IResult Add(RealEstateClassified realEstateClassified)
        {
            _realEstateClassifiedDal.Add(realEstateClassified);
            return new SuccessResult(Messages.RealEstateClassifiedAdded);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [ValidationAspect(typeof(RealEstateClassifiedValidator), Priority = 2)]
        [CacheRemoveAspect("IRealEstateClassifiedService.Get")]
        public IResult ChangeRealEstateClassifiedStatus(RealEstateClassified realEstateClassified)
        {
            _realEstateClassifiedDal.ChangeRealEstateClassifiedStatus(realEstateClassified);
            return new SuccessResult(Messages.REalEstateClassifiedStatusUpdated);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [CacheRemoveAspect("IRealEstateClassifiedService.Get")]
        public IResult Delete(RealEstateClassified realEstateClassified)
        {
            _realEstateClassifiedDal.Delete(realEstateClassified);
            return new SuccessResult(Messages.RealEstateClassifiedDelted);
        }

        public IDataResult<List<RealEstateClassifiedDetailsDto>> GetAllRealEstateClassifiedDetailsDtoBySearchParametrs(SearchFilterForRealEstateClassified searchFilterForRealEstateClassified)
        {
            return new SuccessDataResult<List<RealEstateClassifiedDetailsDto>>(_realEstateClassifiedDal.GetAllRealEstateClassifiedDetailsDto(r =>
           !(searchFilterForRealEstateClassified.BuildingAge.HasValue) | r.BuildingAge == searchFilterForRealEstateClassified.BuildingAge &&
           !(searchFilterForRealEstateClassified.CityId.HasValue || searchFilterForRealEstateClassified.CityId == 0) | r.CityId == searchFilterForRealEstateClassified.CityId &&
           !(searchFilterForRealEstateClassified.DistrictId.HasValue || searchFilterForRealEstateClassified.DistrictId == 0) | r.DistrictId == searchFilterForRealEstateClassified.DistrictId &&
           !(searchFilterForRealEstateClassified.NumberOfRoomId.HasValue || searchFilterForRealEstateClassified.NumberOfRoomId == 0) | r.NumberOfRoomId == searchFilterForRealEstateClassified.NumberOfRoomId &&
           !(searchFilterForRealEstateClassified.RealEstateMin.HasValue || searchFilterForRealEstateClassified.RealEstateMin == 0) | r.RealEstatePrice >= searchFilterForRealEstateClassified.RealEstateMin &&
           !(searchFilterForRealEstateClassified.RealEstatePriceMax.HasValue || searchFilterForRealEstateClassified.RealEstatePriceMax == 0) | r.RealEstatePrice <= searchFilterForRealEstateClassified.RealEstatePriceMax &&
           !(searchFilterForRealEstateClassified.SquareMeters.HasValue || searchFilterForRealEstateClassified.SquareMeters == 0) | r.SquareMeters == searchFilterForRealEstateClassified.SquareMeters &&
           !(searchFilterForRealEstateClassified.Status.HasValue) | r.Status == searchFilterForRealEstateClassified.Status), Messages.RealEstateClassifiedListed);
        }
        [SecuredOperation("realestate",Priority =1)]
        [CacheAspect]
        public IDataResult<List<RealEstateClassifiedDetailsDto>> GetAllRealEstateClassifiedDetailsDtoByUserId(int userId)
        {
            return new SuccessDataResult<List<RealEstateClassifiedDetailsDto>>(_realEstateClassifiedDal.GetAllRealEstateClassifiedDetailsDto(r => r.UserId == userId));
        }
        [SecuredOperation("realestate", Priority = 1)]
        [CacheAspect]
        public IDataResult<List<RealEstateClassifiedDetailsDto>> GetAllRealEstateClassifiedDetailsDtoByUserIdAndStatus(int userId, bool status)
        {
            return new SuccessDataResult<List<RealEstateClassifiedDetailsDto>>(_realEstateClassifiedDal.GetAllRealEstateClassifiedDetailsDto(r => r.UserId == userId && r.Status == status));
        }
        [SecuredOperation("realestate", Priority = 1)]
        public IDataResult<RealEstateClassified> GetRealEstateClassifiedById(int id)
        {
            return new SuccessDataResult<RealEstateClassified>(_realEstateClassifiedDal.Get(r => r.Id == id));
        }

        [SecuredOperation("admin,realestate", Priority = 1)]
        [ValidationAspect(typeof(RealEstateClassifiedValidator), Priority = 2)]
        [CacheRemoveAspect("IRealEstateClassifiedService.Get")]
        public IResult Update(RealEstateClassified realEstateClassified)
        {
            _realEstateClassifiedDal.Update(realEstateClassified);
            return new SuccessResult(Messages.RealEstateClassifiedUpdated);
        }


       
    }
}
