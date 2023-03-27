using Autofac;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommentManager>().As<ICommentService>();
            builder.RegisterType<EfCommentDal>().As<ICommentDal>();

            builder.RegisterType<IndividualUserManager>().As<IUserService<IndividualUser>>();
            builder.RegisterType<EfIndividualUserDal>().As<IIndividualUserDal>();

            builder.RegisterType<CorporateUserManager>().As<IUserService<CorporateUser>>();
            builder.RegisterType<EfCorporateUserDal>().As<ICorporateUserDal>();


            builder.RegisterType<CompanyManager>().As<ICompanyService>();
            builder.RegisterType<EfCompanyDal>().As<ICompanyDal>();

            //builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
