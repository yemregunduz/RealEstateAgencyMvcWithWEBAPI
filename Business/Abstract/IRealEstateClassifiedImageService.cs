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
    public interface IRealEstateClassifiedImageService
    {
        IResult Add(IFormFile file, RealEstateClassifiedImage realEstateImage);
        IResult Delete(RealEstateClassifiedImage realEstateImage);
        IResult Update(IFormFile file, RealEstateClassifiedImage realEstateImage);
        IDataResult<RealEstateClassifiedImage> Get(int id);
        IDataResult<List<RealEstateClassifiedImage>> GetAllRealEstateImagesByRealEstateId (int realEstateClassifiedId);
    }
}
