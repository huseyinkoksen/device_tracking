using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.Services.ImageService.Abstracts;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private readonly ImageServiceBase _imageServiceBase;

        public UserManager(IUserDal userDal, ImageServiceBase imageServiceBase)
        {
            _userDal = userDal;
            _imageServiceBase = imageServiceBase;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User entity)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(entity));
        }

        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult();
        }

        public IDataResult<User> GetByMail(string email)
        {
            var result = _userDal.Get(iu => iu.Email == email);
            if (result == null)
            {
                return new ErrorDataResult<User>();
            }
            if (result.Id > 0)
            {
                return new SuccessDataResult<User>(result);
            }

            return new ErrorDataResult<User>();
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        public IResult ChangePassword(UpdatePasswordDto updatePasswordDto)
        {
            var rulesResult = BusinessRules.Run(CheckIfNewPasswordsMatch(updatePasswordDto.NewPassword, updatePasswordDto.NewPasswordAgain), CheckSamePasswords(updatePasswordDto.Password, updatePasswordDto.NewPassword));
            if (rulesResult != null) return rulesResult;

            var userResult = _userDal.Get(u => u.Id == updatePasswordDto.Id);

            var verifyResult = HashingHelper.VerifyPasswordHash(updatePasswordDto.Password, userResult.PasswordHash, userResult.PasswordSalt);
            if (!verifyResult) return new ErrorResult(Messages.PasswordError);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(updatePasswordDto.NewPassword, out passwordHash, out passwordSalt);

            userResult.PasswordHash = passwordHash;
            userResult.PasswordSalt = passwordSalt;

            _userDal.Update(userResult);
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

        public IResult AddImage(AddUserImageDto addUserImageDto)
        {
            User? user = _userDal.Get(u => u.Id == addUserImageDto.Id);
            user.ImagePath = _imageServiceBase.Upload(addUserImageDto.FormFile);
            _userDal.Update(user);

            return new SuccessResult();
        }

        public IResult DeleteImage(DeleteUserImageDto deleteUserImageDto)
        {
            User? user = _userDal.Get(u => u.Id == deleteUserImageDto.Id);
            _imageServiceBase.Delete(user.ImagePath);

            return new SuccessResult();
        }
    }
}
