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
    public class NeighborhoodManager:INeighborhoodService
    {
        INeighborhoodDal _neighborhoodDal;
        public NeighborhoodManager(INeighborhoodDal neighborhoodDal)
        {
            _neighborhoodDal = neighborhoodDal;
        }
        [SecuredOperation("admin,realestate",Priority =1)]
        [ValidationAspect(typeof(NeighborhoodValidator),Priority =2)]
        [CacheRemoveAspect("INeighborhoodService.Get")]
        public IResult Add(Neighborhood neighborhood)
        {
            IResult result = BusinessRules.Run(CheckIfNeighborhoodAlreadyExist(neighborhood));
            if (result!=null)
            {
                return result;
            }
            _neighborhoodDal.Add(neighborhood);
            return new SuccessResult(Messages.NeighborhoodAdded);
        }
        [SecuredOperation("admin,realestate")]
        public IResult Delete(Neighborhood neighborhood)
        {
            _neighborhoodDal.Delete(neighborhood);
            return new SuccessResult(Messages.NeighborhoodDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Neighborhood>> GetAll()
        {
            return new SuccessDataResult<List<Neighborhood>>(_neighborhoodDal.GetAll(), Messages.NeighborhoodListed);
        }
        [CacheAspect]
        public IDataResult<List<Neighborhood>> GetAllNeighborhoodsByDistrictId(int districtId)
        {
            return new SuccessDataResult<List<Neighborhood>>(_neighborhoodDal.GetAll(n => n.DistrictId == districtId),Messages.NeighborhoodListedByDistrictId);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [ValidationAspect(typeof(NeighborhoodValidator), Priority = 2)]
        [CacheRemoveAspect("INeighborhoodService.Get")]
        public IResult Update(Neighborhood neighborhood)
        {
            throw new NotImplementedException();
        }
        //businesRules
        private IResult CheckIfNeighborhoodAlreadyExist(Neighborhood neighborhood)
        {
            var addedToNeighborhoodDal = _neighborhoodDal.Get(n => n.Id == neighborhood.Id);
            if (addedToNeighborhoodDal!=null)
            {
                return new ErrorResult(Messages.NeighborhoodAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
