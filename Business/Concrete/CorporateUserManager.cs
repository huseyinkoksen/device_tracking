using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CorporateUserManager:IUserService<CorporateUser>
    {
        private ICorporateUserDal _corporateUserDal;

        public CorporateUserManager(ICorporateUserDal corporateUserDal)
        {
            _corporateUserDal = corporateUserDal;
        }


        public IDataResult<List<OperationClaim>> GetClaims(CorporateUser entity)
        {
            return new SuccessDataResult<List<OperationClaim>>(_corporateUserDal.GetClaims(entity));
        }

        public IResult Add(CorporateUser entity)
        {
            _corporateUserDal.Add(entity);
            return new SuccessResult();
        }

        public IDataResult<CorporateUser> GetByMail(string email)
        {
            var result=_corporateUserDal.Get(cu=>cu.Email==email);
            if (result.Id>0)
            {
                return new SuccessDataResult<CorporateUser>(result);
            }
            return new ErrorDataResult<CorporateUser>();
        }
    }
}
