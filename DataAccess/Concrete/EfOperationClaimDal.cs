﻿using Core.DataAccess.Concrete.EntitiyFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfOperationClaimDal:EfEntityRepositoryBase<OperationClaim,RealEstateAgencyDbContext>,IOperationClaimDal
    {
    }
}
