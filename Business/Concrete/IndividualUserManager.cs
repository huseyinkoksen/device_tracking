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
using Entities.DTOs;

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

        public IDataResult<IndividualUser> GetById(int id)
        {
            return new SuccessDataResult<IndividualUser>(_individualUserDal.Get(u => u.Id == id));
        }

        public IResult ChangePassword(UpdatePasswordDto updatePasswordDto)
        {
            var rulesResult = BusinessRules.Run(CheckIfNewPasswordsMatch(updatePasswordDto.NewPassword, updatePasswordDto.NewPasswordAgain), CheckSamePasswords(updatePasswordDto.Password, updatePasswordDto.NewPassword));
            if (rulesResult != null) return rulesResult;

            var userResult = _individualUserDal.Get(u => u.Id == updatePasswordDto.Id);

            var verifyResult = HashingHelper.VerifyPasswordHash(updatePasswordDto.Password, userResult.PasswordHash, userResult.PasswordSalt);
            if (!verifyResult) return new ErrorResult(Messages.PasswordError);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(updatePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            userResult.PasswordHash = passwordHash;
            userResult.PasswordSalt = passwordSalt;

            _individualUserDal.Update(userResult);
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
