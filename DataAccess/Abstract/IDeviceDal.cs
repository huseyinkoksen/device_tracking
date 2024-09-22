﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IDeviceDal:IEntityRepository<Device>
    {
        public List<GetDeviceDetailDto> GetDevicesDetails();
    }
}
