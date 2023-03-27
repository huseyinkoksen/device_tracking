using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateTokenForIndividualUser(IndividualUser user, List<OperationClaim> operationClaims);
        AccessToken CreateTokenForCorporateUser(CorporateUser user, List<OperationClaim> operationClaims);
    }
}
