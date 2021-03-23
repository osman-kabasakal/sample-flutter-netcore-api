using System.Reflection;

namespace Sample.Products.Backend.Core.Aspects.Interfaces
{
    public interface IBeforeVoidAspect : IAspect
    {
        void OnBefore(ParameterInfo[] args);
    }
}