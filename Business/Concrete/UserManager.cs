using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Caching;
using Core.Apects.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IUserImageService _userImageService;
        IUserOperationClaimService _userOperationClaimService;

        public UserManager(IUserDal userDal, IUserImageService userImageService, IUserOperationClaimService userOperationClaimService)
        {
            _userDal = userDal;
            _userImageService = userImageService;
            _userOperationClaimService = userOperationClaimService;
        }
        
        [ValidationAspect(typeof(UserValidator), Priority = 2)]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            IResult result = BusinessRules.Run(CheckIfEmailExist(user.Email));
            if (result!=null)
            {
                return result;
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            IResult result = BusinessRules.Run(CheckIfUserImagesAreDeleted(user), CheckIfUserOperationClaimsAreDeleted(user));
            if (result != null)
            {
                return result;
            }
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetAllClaims(user), Messages.ClaimsListed);
        }
        [CacheAspect]
        public IDataResult<User> GetUserByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        [SecuredOperation("admin", Priority = 1)]
        [ValidationAspect(typeof(UserValidator), Priority = 2)]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult UpdateUserStatus(User user)
        {
            _userDal.UpdateUserStatus(user);
            return new SuccessResult(Messages.UserStatusUpdated);
        }
        [CacheAspect]
        public IDataResult<User> GetUserById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }
        public IDataResult<List<User>> GetAllUsersByUserType(string userType)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(u => u.UserType == userType));
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }
        public IResult Update(User user)
        {
            IResult result = BusinessRules.Run(CheckIfEmailAvailable(user.Email));
            if (result!=null)
            {
                return result;
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        //businessRules
        private IResult CheckIfUserImagesAreDeleted(User user)
        {
            var userImages = _userImageService.GetAllUserImagesByUserId(user.Id).Data;
            if (userImages.Count > 0)
            {
                foreach (var userImage in userImages)
                {
                    var result = _userImageService.Delete(userImage);
                    if (result.Success == false)
                    {
                        return new ErrorResult();
                    }
                }

            }
            return new SuccessResult();
        }
        private IResult CheckIfUserOperationClaimsAreDeleted(User user)
        {
            var userOperationClaims = _userOperationClaimService.GetAllOperationClaimsByUserId(user.Id).Data;
            if (userOperationClaims.Count > 0)
            {
                foreach (var userOperationClaim in userOperationClaims)
                {
                    var result = _userOperationClaimService.Delete(userOperationClaim);
                    if (result.Success == false)
                    {
                        return new ErrorResult();
                    }
                }
            }
            return new SuccessResult();
        }
        private IResult CheckIfEmailExist(string userEmail)
        {
            var result = BaseCheckIfEmailExist(userEmail);
            if (result)
            {
                return new ErrorResult(Messages.UserEmailExist);
            }
            return new SuccessResult();
        }
        private bool BaseCheckIfEmailExist(string userEmail)
        {
            return _userDal.GetAll(u => u.Email == userEmail).Any();
        }
        private IResult CheckIfEmailAvailable(string userEmail)
        {
            var result = BaseCheckIfEmailExist(userEmail);
            if (!result)
            {
                return new ErrorResult(Messages.UserEmailNotAvailable);
            }
            return new SuccessResult();
        }


    }
}
