using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CommentManager:ICommentService
    {
        private ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public IDataResult<List<Comment>> GetAll()
        {
            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll(),Messages.CommentsListed);
        }

        public IDataResult<Comment> GetById(int id)
        {
            return new SuccessDataResult<Comment>(_commentDal.Get(h => h.Id == id));
        }
        public IResult AddComment(Comment entity)
        {
            _commentDal.Add(entity);
            return new SuccessResult(Messages.CommentAdded);
        }

        public IResult UpdateComment(Comment entity)
        {
            _commentDal.Update(entity);
            return new SuccessResult(Messages.CommentUpdated);
        }

        public IResult DeleteByIdComment(int id)
        {
            _commentDal.DeleteById(id);
            if (_commentDal.DeleteById(id).Success)
            {
                return new SuccessResult(Messages.CommentDeleted);
            }
            return new ErrorResult();
        }

        public IDataResult<List<Comment>> GetByCompanyId(int companyId)
        {
            var result = _commentDal.GetAll(c => c.CompanyId == companyId);
            if (result.Count>0)
            {
                return new SuccessDataResult<List<Comment>>(result);
            }
            return new ErrorDataResult<List<Comment>>(Messages.NotFoundComment);
        }

        public IResult DeleteComment(Comment entity)
        {
            _commentDal.Delete(entity);
            return new SuccessResult(Messages.CommentDeleted);
        }
    }
}
