using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User entity);
        IResult Add(User entity);
        IResult AddImage(AddUserImageDto addUserImageDto);
        IResult DeleteImage(DeleteUserImageDto deleteUserImageDto);
        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetById(int id);

        IResult ChangePassword(UpdatePasswordDto updatePasswordDto);
    }
}
