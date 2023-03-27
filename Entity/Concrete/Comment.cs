using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }

    }
}
