using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRealEstateImageService
    {
        IResult Add(IFormFile file, RealEstateImage realEstateImage);
        IResult Delete(RealEstateImage realEstateImage);
        IResult Update(IFormFile file, RealEstateImage realEstateImage);
        IDataResult<RealEstateImage> Get(int id);
        IDataResult<List<RealEstateImage>> GetAllRealEstateImagesByRealEstateId (int realEstateId);
    }
}
