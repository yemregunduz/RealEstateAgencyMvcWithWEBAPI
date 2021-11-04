using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Caching;
using Core.Apects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RealEstateImageManager : IRealEstateImageService
    {
        IRealEstateImageDal  _realEstateImageDal;
        public RealEstateImageManager(IRealEstateImageDal realEstateImageDal)
        {
            _realEstateImageDal = realEstateImageDal;
        }
        [SecuredOperation("realestate,admin", Priority = 1)]
        [ValidationAspect(typeof(RealEstateImageValidator), Priority = 2)]
        [CacheRemoveAspect("IUserImageService.Get")]
        public IResult Add(IFormFile file, RealEstateImage realEstateImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimitExceeded(realEstateImage.RealEstateId));
            if (result != null)
            {
                return result;
            }
            var imageResult = FileHelper.Upload(file);
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            realEstateImage.RealEstateImagePath = imageResult.Message;
            _realEstateImageDal.Add(realEstateImage);
            return new SuccessResult(Messages.RealEstateImageAdded);
        }
        [SecuredOperation("realestate,admin", Priority = 1)]
        public IResult Delete(RealEstateImage realEstateImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsNull(realEstateImage));
            if (result != null)
            {
                return result;
            }
            FileHelper.Delete(realEstateImage.RealEstateImagePath);
            _realEstateImageDal.Delete(realEstateImage);
            return new SuccessResult(Messages.UserImageDeleted);
        }
        [CacheAspect]
        public IDataResult<RealEstateImage> Get(int id)
        {
            return new SuccessDataResult<RealEstateImage>(_realEstateImageDal.Get(r => r.Id == id));
        }
        [CacheAspect]
        public IDataResult<List<RealEstateImage>> GetAllRealEstateImagesByRealEstateId(int realEstateId)
        {
            return new SuccessDataResult<List<RealEstateImage>>(_realEstateImageDal.GetAll(r => r.RealEstateId == realEstateId));
        }
        [SecuredOperation("realestate,admin", Priority = 1)]
        [ValidationAspect(typeof(RealEstateImageValidator), Priority = 2)]
        [CacheRemoveAspect("IUserImageService.Get")]
        public IResult Update(IFormFile file, RealEstateImage realEstateImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsNull(realEstateImage));
            if (result != null)
            {
                return result;
            }
            var updatedFile = FileHelper.Update(file, realEstateImage.RealEstateImagePath);
            if (!updatedFile.Success)
            {
                return new ErrorResult(updatedFile.Message);
            }
            realEstateImage.RealEstateImagePath = updatedFile.Message;
            _realEstateImageDal.Update(realEstateImage);
            return new SuccessResult(Messages.UserImageUpdated);
        }
        //BusinessRules
        private IResult CheckIfImageLimitExceeded(int realEstateId)
        {
            var countOfUserImages = GetAllRealEstateImagesByRealEstateId(realEstateId).Data.Count;
            if (countOfUserImages >= 10)
            {
                return new ErrorResult(Messages.UserImageLimitExceeded);
            }
            return new SuccessResult();
        }
        private IResult CheckIfImageIsNull(RealEstateImage realEstateImage)
        {
            var userImageToDeleted = Get(realEstateImage.Id).Data;
            if (userImageToDeleted == null)
            {
                return new ErrorResult(Messages.ImageNotFound);
            }
            return new SuccessResult();
        }
    }
}
