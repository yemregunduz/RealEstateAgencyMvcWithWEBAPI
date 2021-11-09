using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRealEstateClassifiedDal:IEntityRepository<RealEstateClassified>
    {
        List<RealEstateClassifiedDetailsDto> GetAllRealEstateClassifiedDetailsDto(Expression<Func<RealEstateClassifiedDetailsDto, bool>> filter = null);
        RealEstateClassifiedDetailsDto GetRealEstateClassifiedDetalsDto(Expression<Func<RealEstateClassifiedDetailsDto, bool>> filter = null);
        void ChangeRealEstateClassifiedStatus(RealEstateClassified realEstateClassified);
    }
}
