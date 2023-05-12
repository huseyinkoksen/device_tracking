using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService<T>
    {
        IDataResult<List<OperationClaim>> GetClaims(T entity);
        IResult Add(T entity);
        IDataResult<T> GetByMail(string email);
        IDataResult<T> GetById(int id);

        IResult ChangePassword(UpdatePasswordDto updatePasswordDto);
    }
}
