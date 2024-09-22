using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDeviceDal:EfEntityRepositoryBase<Device,DeviceTrackingContext>,IDeviceDal
    {
        public List<GetDeviceDetailDto> GetDevicesDetails()
        {
            using (DeviceTrackingContext context = new DeviceTrackingContext())
            {
                var result = from d in context.Devices 
                             join e in context.Employees on d.Consigned equals e.Id
                             select new GetDeviceDetailDto
                             {
                                 Id = d.Id,
                                 SerialNumber = d.SerialNumber,
                                 DeviceType = d.DeviceType,
                                 Brand = d.Brand,
                                 Model = d.Model,
                                 OS = d.OS,
                                 Cpu = d.Cpu,
                                 Ram = d.Ram,
                                 Storage = d.Storage,
                                 ConsignedFullName = e.FirstName + " " + e.LastName,
                                 DeviceName = d.DeviceName
                             };
                return result.ToList();
            }
        }
    }
}
