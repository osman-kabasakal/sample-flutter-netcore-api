using System;
using System.Reflection;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.Core.Aspects.Abstract;
using Sample.Products.Backend.Core.Aspects.Interfaces;

namespace Sample.Products.Backend.Business.Concrete.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CatchErrorAttribute:AspectBaseAttribute,INRunAspect
    {
        
        public object OnRun<T>(MethodInfo method, T decorate, object[] args)
        {
            try
            {
                return method.Invoke(decorate, args);
            }
            catch (Exception e)
            {
                var rtType = method.ReturnType;
                if (rtType.Name != typeof(ServiceResponse<>).Name) return null;
                
                var rt = Activator.CreateInstance(rtType) as IServiceResponse;
                if (rt == null) return rt;
                rt.ExceptionMessage = e.InnerException?.Message??e.Message;
                rt.IsSuccessful = false;
                return rt;
            }
        }
    }
}