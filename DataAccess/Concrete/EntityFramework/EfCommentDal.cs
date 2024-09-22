using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeDal:EfEntityRepositoryBase<Employee,DeviceTrackingContext>,IEmployeeDal
    {
        public IResult DeleteById(int id)
        {
            using (DeviceTrackingContext context=new DeviceTrackingContext())
            {
                var employeeForDelete = context.Employees.SingleOrDefault(c=>c.Id==id);
                if (employeeForDelete != null)
                {
                    var deletetedEmployee=context.Entry(employeeForDelete);
                    deletetedEmployee.State=EntityState.Deleted;
                    context.SaveChanges();
                    return new SuccessResult();
                }
                return new ErrorResult();
            }
        }
    }

    
}
