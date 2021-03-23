namespace Sample.Products.Backend.Core.Aspects.Interfaces
{
    public interface IBeforeAspect : IAspect
    {
        object OnBefore();
    }
}