using Core.DataAccess.Concrete.EntitiyFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, RealEstateAgencyDbContext>, IUserDal
    {
        public List<OperationClaim> GetAllClaims(User user)
        {
            using (var context = new RealEstateAgencyDbContext())
            {
                var result = from oc in context.OPERATIONCLAIMS
                             join uoc in context.USEROPERATIONCLAIMS
                             on oc.Id equals uoc.OperationClaimId
                             where uoc.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = oc.Id,
                                 Name = oc.Name
                             };
                return result.ToList();
            }
        }

        public void UpdateUserStatus(User user)
        {
            using (var context = new RealEstateAgencyDbContext())
            {
                context.Attach(user);
                context.Entry(user).Property(u => u.Status);
                context.SaveChanges();
            }
        }
    }
}
