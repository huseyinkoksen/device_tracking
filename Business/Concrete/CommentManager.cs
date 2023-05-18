using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.AutoFac;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CommentManager:ICommentService
    {
        private ICommentDal _commentDal;
        private readonly IAuthService _authService;


        public CommentManager(ICommentDal commentDal, IAuthService authService)
        {
            _commentDal = commentDal;
            ;
            _authService = authService;
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

        [SecuredOperation("admin")]
        public IResult DeleteByIdComment(int id)
        {
            _commentDal.DeleteById(id);
            if (_commentDal.DeleteById(id).Success)
            {
                return new SuccessResult(Messages.CommentDeleted);
            }
            return new ErrorResult();
        }

        public IResult DeleteCommentByYourself(int id)
        {
            var originalUserId=_commentDal.Get(c=>c.Id==id).UserId;

            var result =Abc(originalUserId);
            if (result.Success)
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

        private IResult Abc(int ownerUserId)
        {
            var result = _authService.GetCurrentUser();
            if(!result.Success) return new ErrorResult(result.Message);

            if(ownerUserId != result.Data.Id) return new ErrorResult("Yorum sadece sahibi tarafından silinebilir");

            return new SuccessResult();
        }
    }
}
