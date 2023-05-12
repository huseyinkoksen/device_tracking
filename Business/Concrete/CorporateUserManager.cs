using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;

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
            return new SuccessDataResult<CorporateUser>(result);
            
            
        }

        public IDataResult<CorporateUser> GetById(int id)
        {
            return new SuccessDataResult<CorporateUser>(_corporateUserDal.Get(u => u.Id == id));
        }

        public IResult ChangePassword(UpdatePasswordDto updatePasswordDto)
        {
            var rulesResult = BusinessRules.Run(CheckIfNewPasswordsMatch(updatePasswordDto.NewPassword, updatePasswordDto.NewPasswordAgain), CheckSamePasswords(updatePasswordDto.Password, updatePasswordDto.NewPassword));
            if (rulesResult != null) return rulesResult;

            var userResult = _corporateUserDal.Get(u => u.Id == updatePasswordDto.Id);

            var verifyResult = HashingHelper.VerifyPasswordHash(updatePasswordDto.Password, userResult.PasswordHash, userResult.PasswordSalt);
            if (!verifyResult) return new ErrorResult(Messages.PasswordError);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(updatePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            userResult.PasswordHash = passwordHash;
            userResult.PasswordSalt = passwordSalt;

            _corporateUserDal.Update(userResult);
            return new SuccessResult(Messages.PasswordUpdated);
        }
        private IResult CheckIfNewPasswordsMatch(string newPassword, string newPasswordAgain)
        {
            if (newPassword != newPasswordAgain) return new ErrorResult(Messages.NewPasswordsMatchError);
            return new SuccessResult();
        }

        private IResult CheckSamePasswords(string newPassword, string password)
        {
            if (newPassword == password) return new ErrorResult(Messages.PasswordsSame);
            return new SuccessResult();
        }
    }
}
