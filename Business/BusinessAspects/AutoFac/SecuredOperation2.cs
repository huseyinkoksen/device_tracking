using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.AutoFac
{
    public class SecuredOperation2:MethodInterception
    {
        private int _userId;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation2(int userId)
        {
            _userId=userId;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var user = _httpContextAccessor.HttpContext.User;
            Console.WriteLine(user);
            //if (user.Identity.Name==_userId)
            //{
            //   return;
            //}
            

            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
