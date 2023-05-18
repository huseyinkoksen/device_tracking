using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class IndividualUser : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
