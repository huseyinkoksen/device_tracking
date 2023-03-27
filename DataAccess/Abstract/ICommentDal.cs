using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICommentDal:IEntityRepository<Comment>
    {
        IResult DeleteById(int id);
    }
}
