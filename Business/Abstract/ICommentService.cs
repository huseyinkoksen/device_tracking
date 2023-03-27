using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<List<Comment>> GetAll();
        IDataResult<Comment> GetById(int id);
        IResult AddComment(Comment entity);
        IResult DeleteComment(Comment entity);
        IResult UpdateComment(Comment entity);
        IResult DeleteByIdComment(int id);
        IDataResult<List<Comment>> GetByCompanyId(int companyId);
    }
}
