using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Security.JWT;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<CorporateUser> RegisterCorporate(CorporateUserForRegisterDto userForRegisterDto, string password);
        IDataResult<IndividualUser> LoginIndividual(UserForLoginDto userForLoginDto);
        IDataResult<IndividualUser> RegisterIndividual(IndividualUserForRegisterDto userForRegisterDto, string password);
        IDataResult<CorporateUser> LoginCorporate(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessTokenForIndivualUser(IndividualUser user);
        IDataResult<AccessToken> CreateAccessTokenForCorporateUser(CorporateUser user);
    }
}
