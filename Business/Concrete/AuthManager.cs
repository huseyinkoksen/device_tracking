using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.Services.HttpContextAccessorService.Abstracts;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Identity.Client;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        IHttpContextAccessorService _httpContextAccessorService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IHttpContextAccessorService httpContextAccessorService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _httpContextAccessorService = httpContextAccessorService;
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
            _userService.Add(user);
            return new SuccessDataResult<CorporateUser>(user, Messages.UserRegistered);
        }

        public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                    userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>(Messages.PasswordError);
            }
            var result = CreateAccessTokenForUser(userToCheck);
            return new SuccessDataResult<AccessToken>(result.Data, Messages.SuccessfulLogin);
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
                Status = true,
                ImagePath = userForRegisterDto.ImagePath
            };
            _userService.Add(user);
            return new SuccessDataResult<IndividualUser>(user, Messages.UserRegistered);
        }



        public IResult UserExists(string email)
        {
            if ((_userService.GetByMail(email).Data != null) && (_userService.GetByMail(email).Data != null))
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessTokenForUser(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateTokenForUser(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);

        }

        public IDataResult<User> GetCurrentUser()
        {
            string nameIdentifier = _httpContextAccessorService.GetCurrentNameIdentifier();
            return _userService.GetById(Convert.ToInt32(nameIdentifier));
        }
    }
}
