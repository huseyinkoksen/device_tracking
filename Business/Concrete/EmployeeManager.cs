using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.AutoFac;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class EmployeeManager:IEmployeeService
    {
        private IEmployeeDal _employeeDal;
        private readonly IAuthService _authService;


        public EmployeeManager(IEmployeeDal employeeDal, IAuthService authService)
        {
            _employeeDal = employeeDal;
            ;
            _authService = authService;
        }

        public IDataResult<List<Employee>> GetAll()
        {
            return new SuccessDataResult<List<Employee>>(_employeeDal.GetAll(),Messages.EmployeesListed);
        }

        public IDataResult<Employee> GetById(int id)
        {
            return new SuccessDataResult<Employee>(_employeeDal.Get(h => h.Id == id));
        }
        public IResult AddEmployee(Employee entity)
        {
            _employeeDal.Add(entity);
            return new SuccessResult(Messages.EmployeeAdded);
        }

        public IResult UpdateEmployee(Employee entity)
        {
            _employeeDal.Update(entity);
            return new SuccessResult(Messages.EmployeeUpdated);
        }

        [SecuredOperation("admin")]
        public IResult DeleteByIdEmployee(int id)
        {
            _employeeDal.DeleteById(id);
            if (_employeeDal.DeleteById(id).Success)
            {
                return new SuccessResult(Messages.EmployeeDeleted);
            }
            return new ErrorResult();
        }

        //public IResult DeleteEmployeeByYourself(int id)
        //{
        //    var originalUserId=_employeeDal.Get(c=>c.Id==id).UserId;

        //    var result =Abc(originalUserId);
        //    if (result.Success)
        //    {
        //        return new SuccessResult(Messages.EmployeeDeleted);
        //    }

        //    return new ErrorResult();
        //}
        public IDataResult<List<Employee>> GetByDeviceId(int deviceId)
        {
            var result = _employeeDal.GetAll(c => c.Id == deviceId);
            if (result.Count>0)
            {
                return new SuccessDataResult<List<Employee>>(result);
            }
            return new ErrorDataResult<List<Employee>>(Messages.NotFoundEmployee);
        }

        public IResult DeleteEmployee(Employee entity)
        {
            _employeeDal.Delete(entity);
            return new SuccessResult(Messages.EmployeeDeleted);
        }

        private IResult Abc(int ownerUserId)
        {
            var result = _authService.GetCurrentUser();
            if(!result.Success) return new ErrorResult(result.Message);

            if(ownerUserId != result.Data.Id) return new ErrorResult("Yorum sadece sahibi tarafından silinebilir");

            return new SuccessResult();
        }

       
    }
}
