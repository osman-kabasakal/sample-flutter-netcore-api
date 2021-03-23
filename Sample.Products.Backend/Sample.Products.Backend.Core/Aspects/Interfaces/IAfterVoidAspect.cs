namespace Sample.Products.Backend.Core.Aspects.Interfaces
{
    public interface IAfterVoidAspect : IAspect
    {
        void OnAfter(object result);
    }
}