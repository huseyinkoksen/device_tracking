using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Microsoft.Identity.Client;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService<IndividualUser> _userServiceIU;
        IUserService<CorporateUser> _userServiceCU;
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService<IndividualUser> userServiceIU, ITokenHelper tokenHelper,
            IUserService<CorporateUser> userServiceCu)
        {
            _userServiceIU = userServiceIU;
            _tokenHelper = tokenHelper;
            _userServiceCU = userServiceCu;
        }

        public IDataResult<CorporateUser> RegisterCorporate(CorporateUserForRegisterDto userForRegisterDto,
            string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new CorporateUser()
            {
                Email = userForRegisterDto.Email,
                CompanyName = userForRegisterDto.CompanyName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userServiceCU.Add(user);
            return new SuccessDataResult<CorporateUser>(user, Messages.UserRegistered);
        }

        public IDataResult<IndividualUser> LoginIndividual(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userServiceIU.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<IndividualUser>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                    userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<IndividualUser>(Messages.PasswordError);
            }

            return new SuccessDataResult<IndividualUser>(userToCheck, Messages.SuccessfulLogin);
        }

        public IDataResult<IndividualUser> RegisterIndividual(IndividualUserForRegisterDto userForRegisterDto,
            string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new IndividualUser()
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userServiceIU.Add(user);
            return new SuccessDataResult<IndividualUser>(user, Messages.UserRegistered);
        }

        public IDataResult<CorporateUser> LoginCorporate(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userServiceCU.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<CorporateUser>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                    userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<CorporateUser>(Messages.PasswordError);
            }

            return new SuccessDataResult<CorporateUser>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userServiceCU.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessTokenForIndivualUser(IndividualUser user)
        {
            var claims = _userServiceIU.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateTokenForIndividualUser(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);

        }

        public IDataResult<AccessToken> CreateAccessTokenForCorporateUser(CorporateUser user)
        {
            var claims = _userServiceCU.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateTokenForCorporateUser(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
