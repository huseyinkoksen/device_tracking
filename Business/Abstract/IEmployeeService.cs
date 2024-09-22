using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        IDataResult<List<Employee>> GetAll();
        IDataResult<Employee> GetById(int id);
        IResult AddEmployee(Employee entity);
        IResult DeleteEmployee(Employee entity);
        IResult UpdateEmployee(Employee entity);
        IResult DeleteByIdEmployee(int id);
        
        IDataResult<List<Employee>> GetByDeviceId(int deviceId);
    }
}
