using Core.DataAccess.Concrete.EntitiyFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfRealEstateClassifiedDal : EfEntityRepositoryBase<RealEstateClassified, RealEstateAgencyDbContext>, IRealEstateClassifiedDal
    {
        public void ChangeRealEstateClassifiedStatus(RealEstateClassified realEstateClassified)
        {
            using (var context = new RealEstateAgencyDbContext())
            {
                context.Attach(realEstateClassified);
                context.Entry(realEstateClassified).Property(r => r.Status);
                context.SaveChanges();
            }
        }

        public List<RealEstateClassifiedDetailsDto> GetAllRealEstateClassifiedDetailsDto(Expression<Func<RealEstateClassifiedDetailsDto, bool>> filter = null)
        {
            using (var context = new RealEstateAgencyDbContext())
            {
                var result = from r in context.REALESTATECLASSIFIEDS
                             join u in context.USERS
                             on r.UserId equals u.Id into gj1
                             from u in gj1.DefaultIfEmpty()

                             join nr in context.NUMBEROFROOMS
                             on r.NumberOfRoomId equals nr.Id into gj2
                             from nr in gj2.DefaultIfEmpty()

                             join n in context.NEIGHBORHOODS
                             on r.NeighborhoodId equals n.Id into gj3
                             from n in gj3.DefaultIfEmpty()

                             join d in context.DISTRICTS
                             on n.DistrictId equals d.Id into gj4
                             from d in gj4.DefaultIfEmpty()

                             join c in context.CITIES
                             on d.CityId equals c.Id into gj5
                             from c in gj5.DefaultIfEmpty()

                             select new RealEstateClassifiedDetailsDto
                             {
                                 Id = r.Id,
                                 BuildingAge = r.BuildingAge,
                                 CityId = c.Id,
                                 CityName = c.CityName,
                                 CompanyName = u.CompanyName,
                                 Date = r.Date,
                                 DistrictId = d.Id,
                                 DistrictName = d.DistrictName,
                                 Floor = r.Floor,
                                 FullAddress = r.FullAddress,
                                 NeighborhoodId = n.Id,
                                 NeighborhoodName = n.NeighborhoodName,
                                 NumberOfRoomId = r.NumberOfRoomId,
                                 RealEstatePrice = r.RealEstatePrice,
                                 RealEstateClassifiedTitle = r.RealEstateClassifiedTitle,
                                 RoomCount = nr.RoomCount,
                                 UserId = r.UserId,
                                 SquareMeters = r.SquareMeters,
                                 Status = r.Status
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }

        public RealEstateClassifiedDetailsDto GetRealEstateClassifiedDetalsDto(Expression<Func<RealEstateClassifiedDetailsDto, bool>> filter = null)
        {
            using (var context = new RealEstateAgencyDbContext())
            {
                var result = from r in context.REALESTATECLASSIFIEDS

                             join u in context.USERS
                             on r.UserId equals u.Id

                             join nr in context.NUMBEROFROOMS
                             on r.NumberOfRoomId equals nr.Id

                             join n in context.NEIGHBORHOODS
                             on r.NeighborhoodId equals n.Id

                             join d in context.DISTRICTS
                             on n.DistrictId equals d.Id

                             join c in context.CITIES
                             on d.CityId equals c.Id

                             select new RealEstateClassifiedDetailsDto
                             {
                                 Id = r.Id,
                                 BuildingAge = r.BuildingAge,
                                 CityId = c.Id,
                                 CityName = c.CityName,
                                 DistrictId = d.Id,
                                 DistrictName = d.DistrictName,
                                 Floor = r.Floor,
                                 NeighborhoodId = n.Id,
                                 NeighborhoodName = n.NeighborhoodName,
                                 RealEstatePrice = r.RealEstatePrice,
                                 NumberOfRoomId = nr.Id,
                                 RoomCount = nr.RoomCount,
                                 SquareMeters = r.SquareMeters,
                                 CompanyName = u.CompanyName,
                                 UserId = u.Id,
                                 Date = r.Date,
                                 FullAddress = r.FullAddress,
                                 Status = r.Status,
                                 RealEstateClassifiedTitle=r.RealEstateClassifiedTitle

                             };
                return result.SingleOrDefault(filter);
            }
        }
    }
}
