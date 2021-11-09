using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Delete(User user);
        IDataResult<List<User>> GetAll();
        IResult UpdateUserStatus(User user);
        IDataResult<List<User>> GetAllUsersByUserType(string userType);
        IDataResult<User> GetUserByMail(string email);
        IDataResult<User> GetUserById(int userId);
        IResult Update(User user);
    }
}
