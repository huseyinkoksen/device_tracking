using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfIndividualUserDal:EfEntityRepositoryBase<IndividualUser,CeswaContext>,IIndividualUserDal
    {
        public List<OperationClaim> GetClaims(IndividualUser user)
        {
            using (var context=new CeswaContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals
                        userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.Id
                    select new OperationClaim { Id = operationClaim.Id,Name = operationClaim.Name};
                return result.ToList();
            }
        }
    }
}
