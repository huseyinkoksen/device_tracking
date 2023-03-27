using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class IndividualUserManager:IUserService<IndividualUser>
    {
        private IIndividualUserDal _individualUserDal;

        public IndividualUserManager(IIndividualUserDal individualUserDal)
        {
            _individualUserDal = individualUserDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(IndividualUser entity)
        {
            return new SuccessDataResult<List<OperationClaim>>(_individualUserDal.GetClaims(entity));
        }

        public IResult Add(IndividualUser entity)
        {
            _individualUserDal.Add(entity);
            return new SuccessResult();
        }

        public IDataResult<IndividualUser> GetByMail(string email)
        {
            var result=_individualUserDal.Get(iu => iu.Email == email);
            if (result.Id>0)
            {
                return new SuccessDataResult<IndividualUser>(result);
            }

            return new ErrorDataResult<IndividualUser>();
        }
    }
}
