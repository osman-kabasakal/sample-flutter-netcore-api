using System;
using System.Reflection;

namespace Sample.Products.Backend.Core.Aspects.Interfaces
{
    public interface INRunAspect
    {
        object OnRun<T>(MethodInfo method,T decorate,object[] args);
    }
}