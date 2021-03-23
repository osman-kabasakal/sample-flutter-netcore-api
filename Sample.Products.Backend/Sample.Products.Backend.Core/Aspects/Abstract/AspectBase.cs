using System;
using Sample.Products.Backend.Core.Aspects.Interfaces;

namespace Sample.Products.Backend.Core.Aspects.Abstract
{
    [Serializable]
    public abstract class AspectBaseAttribute:Attribute,IAspect
    {

    }
}