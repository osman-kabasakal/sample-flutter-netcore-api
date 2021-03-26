using System;

namespace Sample.Products.Backend.Business.Concrete.Attributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class CatchErrorTypeForMethodsAttribute:CatchErrorAttribute
    {
        
    }
}