using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INumberOfRoomService
    {
        IResult Add(NumberOfRoom numberOfRoom);
        IResult Delete(NumberOfRoom numberOfRoom);
        IResult Update(NumberOfRoom numberOfRoom);
        IDataResult<List<NumberOfRoom>> GetAll();
        
    }
}
