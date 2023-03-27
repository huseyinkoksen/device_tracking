using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal:EfEntityRepositoryBase<Comment,CeswaContext>,ICommentDal
    {
        public IResult DeleteById(int id)
        {
            using (CeswaContext context=new CeswaContext())
            {
                var commentForDelete = context.Comments.SingleOrDefault(c=>c.Id==id);
                if (commentForDelete != null)
                {
                    var deletetedComment=context.Entry(commentForDelete);
                    deletetedComment.State=EntityState.Deleted;
                    context.SaveChanges();
                    return new SuccessResult();
                }
                return new ErrorResult();
            }
        }
    }

    
}
