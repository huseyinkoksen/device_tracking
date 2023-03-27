using Core.Entities;

namespace Entities.Concrete
{
    public class Company : IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string CompanyType { get; set; }
    }
}