using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Security.JWT;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<CorporateUser> RegisterCorporate(CorporateUserForRegisterDto userForRegisterDto, string password);
        IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto);
        IDataResult<IndividualUser> RegisterIndividual(IndividualUserForRegisterDto userForRegisterDto, string password);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessTokenForUser(User user);
        IDataResult<User> GetCurrentUser();
    }
}
