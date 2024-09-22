using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IDeviceService
    {
        IDataResult<List<Device>> GetAll();
        public IDataResult<Device> GetById(int id);
        public IResult AddDevice(Device entity);
        public IResult UpdateDevice(Device entity);
        public IResult DeleteDevice(Device entity);
        public IDataResult<List<GetDeviceDetailDto>> GetDevicesDetails();

    }
}
