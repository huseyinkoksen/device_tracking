using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Core.Entities.Concrete
{
    public class CorporateUser : User
    {
        public string CompanyName { get; set; }
    }
}
