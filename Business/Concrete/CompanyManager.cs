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

namespace Business.Concrete
{
    public class CompanyManager:ICompanyService
    {
        private ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal=companyDal;
        }
        public IDataResult<List<Company>> GetAll()
        {
            if (_companyDal.GetAll().Count>0)
            {
                return new SuccessDataResult<List<Company>>(_companyDal.GetAll(), Messages.CompanyListed);
            }

            return new ErrorDataResult<List<Company>>(Messages.NotFoundCompany);
        }

        public IDataResult<Company> GetById(int id)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(h => h.Id == id));
        }
        public IResult AddCompany(Company entity)
        {
            entity.CompanyType=entity.CompanyType.ToUpper();
            _companyDal.Add(entity);
            return new SuccessResult(Messages.CompanyAdded);
        }

        public IResult UpdateCompany(Company entity)
        {
            _companyDal.Update(entity);
            return new SuccessResult(Messages.CompanyUpdated);
        }

        public IResult DeleteCompany(Company entity)
        {
            _companyDal.Delete(entity);
            return new SuccessResult(Messages.CompanyDeleted);
        }
    }
}
