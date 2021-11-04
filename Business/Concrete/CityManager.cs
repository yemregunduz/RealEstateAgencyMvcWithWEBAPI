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
    public class CityManager : ICityService
    {
        ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [ValidationAspect(typeof(CityValidator), Priority = 2)]
        [CacheRemoveAspect("ICityService.Get")]
        public IResult Add(City city)
        {
            IResult result = BusinessRules.Run(CheckIfCityAlreadyExist(city));
            if (result!=null)
            {
                return result;
            }
            _cityDal.Add(city);
            return new SuccessResult(Messages.CityAdded);
        }
        [CacheRemoveAspect("ICityService.Get")]
        public IResult Delete(City city)
        {
            _cityDal.Delete(city);
            return new SuccessResult(Messages.CityDeleted);
        }
        [CacheAspect]
        public IDataResult<List<City>> GetAll()
        {
            return new SuccessDataResult<List<City>>(_cityDal.GetAll(), Messages.CitiesListed);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [ValidationAspect(typeof(DistrictValidator), Priority = 2)]
        [CacheRemoveAspect("IDistrictService.Get")]
        public IResult Update(City city)
        {
            IResult result = BusinessRules.Run(CheckIfCityAlreadyExist(city));
            if (result!=null)
            {
                return result;
            }
            _cityDal.Update(city);
            return new SuccessResult(Messages.CityUpdated);
        }

        //businessRules
        private IResult CheckIfCityAlreadyExist(City city)
        {
            var cityToAdded = _cityDal.Get(c => c.Id == city.Id);
            if (cityToAdded!=null)
            {
                return new ErrorResult(Messages.CityAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
