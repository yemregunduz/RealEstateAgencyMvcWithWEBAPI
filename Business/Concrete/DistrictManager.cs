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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DistrictManager : IDistrictService
    {
        IDistrictDal _districtDal;
        public DistrictManager(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }
        [SecuredOperation("admin,realestate",Priority =1)]
        [ValidationAspect(typeof(DistrictValidator),Priority =2)]
        [CacheRemoveAspect("IDistrictService.Get")]
        public IResult Add(District district)
        {
            IResult result = BusinessRules.Run(CheckIfDistrictAlreadyAdded(district));
            if (result!=null)
            {
                return result;
            }
            _districtDal.Add(district);
            return new SuccessResult(Messages.DistrictAdded);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [CacheRemoveAspect("IDistrictService.Get")]
        public IResult Delete(District district)
        {
            _districtDal.Delete(district);
            return new SuccessResult(Messages.DistrictDeleted);
        }
        [CacheAspect]
        public IDataResult<List<District>> GetAll()
        {
            return new SuccessDataResult<List<District>>(_districtDal.GetAll(), Messages.DistrictsListed);
        }
        [CacheAspect]
        public IDataResult<List<District>> GetAllDistrictsByCityId(int cityId)
        {
            return new SuccessDataResult<List<District>>(_districtDal.GetAll(d => d.CityId == cityId),Messages.DistrictListedByCityId);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [ValidationAspect(typeof(DistrictValidator), Priority = 2)]
        [CacheRemoveAspect("IDistrictService.Get")]
        public IResult Update(District district)
        {
            IResult result = BusinessRules.Run(CheckIfDistrictAlreadyAdded(district));
            if (result!=null)
            {
                return result;
            }
            _districtDal.Update(district);
            return new SuccessResult(Messages.DistrictUpdated);
        }

        //businessRules
        private IResult CheckIfDistrictAlreadyAdded(District district)
        {
            var result = _districtDal.Get(d => d.Id == district.Id);
            if (result!=null)
            {
                return new ErrorResult(Messages.DistrictAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
