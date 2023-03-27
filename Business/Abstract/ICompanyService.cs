using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        IDataResult<List<Company>> GetAll();
        public IDataResult<Company> GetById(int id);
        public IResult AddCompany(Company entity);
        public IResult UpdateCompany(Company entity);
        public IResult DeleteCompany(Company entity);

    }
}
