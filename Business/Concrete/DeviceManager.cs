using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class DeviceManager:IDeviceService
    {
        private readonly IDeviceDal _deviceDal;
        private readonly IEmployeeDal _employeeDal;

        public DeviceManager(IDeviceDal deviceDal, IEmployeeDal employeeDal)
        {
            _deviceDal=deviceDal;
            _employeeDal = employeeDal;
        }
        public IDataResult<List<Device>> GetAll()
        {
            if (_deviceDal.GetAll().Count>0)
            {
                return new SuccessDataResult<List<Device>>(_deviceDal.GetAll(), Messages.DeviceListed);
            }

            return new ErrorDataResult<List<Device>>(Messages.NotFoundDevice);
        }

        public IDataResult<Device> GetById(int id)
        {
            return new SuccessDataResult<Device>(_deviceDal.Get(h => h.Id == id));
        }
        public IResult AddDevice(Device entity)
        {
        
            if(!CheckIfEmployeeExists(entity.Consigned).Success)
            {
               return new ErrorResult(Messages.EmployeeNotExist);
            }
            else
            entity.DeviceType=entity.DeviceType.ToUpper();
            _deviceDal.Add(entity);
            return new SuccessResult(Messages.DeviceAdded);
       
        }

        public IResult UpdateDevice(Device entity)
        {
            _deviceDal.Update(entity);
            return new SuccessResult(Messages.DeviceUpdated);
        }

        public IResult DeleteDevice(Device entity)
        {
            _deviceDal.Delete(entity);
            return new SuccessResult(Messages.DeviceDeleted);
        }

        private IResult CheckIfEmployeeExists(int userId)
        {
            if (_employeeDal.Get(u => u.Id == userId) != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }

        public IDataResult<List<GetDeviceDetailDto>> GetDevicesDetails()
        {
            return new SuccessDataResult<List<GetDeviceDetailDto>>(_deviceDal.GetDevicesDetails());
        }
    }
}
