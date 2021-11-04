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
    public class NumberOfRoomManager : INumberOfRoomService
    {
        INumberOfRoomDal _numberOfRoomDal;
        public NumberOfRoomManager(INumberOfRoomDal numberOfRoomDal)
        {
            _numberOfRoomDal = numberOfRoomDal;
        }
        [SecuredOperation("admin,realestate",Priority =1)]
        [ValidationAspect(typeof(NumberOfRoomValidator),Priority =2)]
        [CacheRemoveAspect("INumberOfRoomService.Get")]
        public IResult Add(NumberOfRoom numberOfRoom)
        {
            IResult result = BusinessRules.Run(CheckIfNumberOfRoomAlreadyExist(numberOfRoom));
            if (result!=null)
            {
                return result;
            }
            _numberOfRoomDal.Add(numberOfRoom);
            return new SuccessResult(Messages.NumberOfRoomAdded);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [CacheRemoveAspect("INumberOfRoomService.Get")]
        public IResult Delete(NumberOfRoom numberOfRoom)
        {
            _numberOfRoomDal.Delete(numberOfRoom);
            return new SuccessResult(Messages.NumberOfRoomDeleted);
        }
        [CacheAspect]
        public IDataResult<List<NumberOfRoom>> GetAll()
        {
            return new SuccessDataResult<List<NumberOfRoom>>(_numberOfRoomDal.GetAll(), Messages.NumberOfRoomListed);
        }
        [SecuredOperation("admin,realestate", Priority = 1)]
        [ValidationAspect(typeof(NumberOfRoomValidator), Priority = 2)]
        [CacheRemoveAspect("INumberOfRoomService.Get")]
        public IResult Update(NumberOfRoom numberOfRoom)
        {
            IResult result = BusinessRules.Run(CheckIfNumberOfRoomAlreadyExist(numberOfRoom));
            if (result != null)
            {
                return result;
            }
            _numberOfRoomDal.Update(numberOfRoom);
            return new SuccessResult(Messages.NumberOfRoomUpdated);
        }
        //businessRules
        private IResult CheckIfNumberOfRoomAlreadyExist(NumberOfRoom numberOfRoom)
        {
            var addedToNumberOfRoom = _numberOfRoomDal.Get(n => n.Id == numberOfRoom.Id);
            if (addedToNumberOfRoom!=null)
            {
                return new ErrorResult(Messages.NumberOfRoomAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
