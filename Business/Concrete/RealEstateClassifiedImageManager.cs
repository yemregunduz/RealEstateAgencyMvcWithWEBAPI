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
    public class RealEstateClassifiedImageManager : IRealEstateClassifiedImageService
    {
        IRealEstateClassifiedImageDal  _realEstateClassifiedImageDal;
        public RealEstateClassifiedImageManager(IRealEstateClassifiedImageDal realEstateClassifiedImageDal)
        {
            _realEstateClassifiedImageDal = realEstateClassifiedImageDal;
        }
        [SecuredOperation("realestate,admin", Priority = 1)]
        [ValidationAspect(typeof(RealEstateClassifiedImageValidator), Priority = 2)]
        [CacheRemoveAspect("IUserImageService.Get")]
        public IResult Add(IFormFile file, RealEstateClassifiedImage realEstateClassifiedImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimitExceeded(realEstateClassifiedImage.RealEstateClassifiedId));
            if (result != null)
            {
                return result;
            }
            var imageResult = FileHelper.Upload(file);
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            realEstateClassifiedImage.RealEstateImagePath = imageResult.Message;
            _realEstateClassifiedImageDal.Add(realEstateClassifiedImage);
            return new SuccessResult(Messages.RealEstateImageAdded);
        }
        [SecuredOperation("realestate,admin", Priority = 1)]
        public IResult Delete(RealEstateClassifiedImage realEstateClassifiedImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsNull(realEstateClassifiedImage));
            if (result != null)
            {
                return result;
            }
            FileHelper.Delete(realEstateClassifiedImage.RealEstateImagePath);
            _realEstateClassifiedImageDal.Delete(realEstateClassifiedImage);
            return new SuccessResult(Messages.UserImageDeleted);
        }
        [CacheAspect]
        public IDataResult<RealEstateClassifiedImage> Get(int id)
        {
            return new SuccessDataResult<RealEstateClassifiedImage>(_realEstateClassifiedImageDal.Get(r => r.Id == id));
        }
        [CacheAspect]
        public IDataResult<List<RealEstateClassifiedImage>> GetAllRealEstateImagesByRealEstateId(int realEstateClassifiedId)
        {
            return new SuccessDataResult<List<RealEstateClassifiedImage>>(_realEstateClassifiedImageDal.GetAll(r => r.RealEstateClassifiedId == realEstateClassifiedId));
        }
        [SecuredOperation("realestate,admin", Priority = 1)]
        [ValidationAspect(typeof(RealEstateClassifiedImageValidator), Priority = 2)]
        [CacheRemoveAspect("IUserImageService.Get")]
        public IResult Update(IFormFile file, RealEstateClassifiedImage realEstateClassifiedImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsNull(realEstateClassifiedImage));
            if (result != null)
            {
                return result;
            }
            var updatedFile = FileHelper.Update(file, realEstateClassifiedImage.RealEstateImagePath);
            if (!updatedFile.Success)
            {
                return new ErrorResult(updatedFile.Message);
            }
            realEstateClassifiedImage.RealEstateImagePath = updatedFile.Message;
            _realEstateClassifiedImageDal.Update(realEstateClassifiedImage);
            return new SuccessResult(Messages.UserImageUpdated);
        }
        //BusinessRules
        private IResult CheckIfImageLimitExceeded(int realEstateClassifiedId)
        {
            var countOfUserImages = GetAllRealEstateImagesByRealEstateId(realEstateClassifiedId).Data.Count;
            if (countOfUserImages >= 10)
            {
                return new ErrorResult(Messages.RealEstatClassifiedImageLimitExceeded);
            }
            return new SuccessResult();
        }
        private IResult CheckIfImageIsNull(RealEstateClassifiedImage realEstateClassifiedImage)
        {
            var userImageToDeleted = Get(realEstateClassifiedImage.Id).Data;
            if (userImageToDeleted == null)
            {
                return new ErrorResult(Messages.ImageNotFound);
            }
            return new SuccessResult();
        }
    }
}
