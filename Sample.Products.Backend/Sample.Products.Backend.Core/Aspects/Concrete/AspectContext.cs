using System;
using System.Reflection;

namespace Sample.Products.Backend.Core.Aspects.Concrete
{
    public class AspectContext
    {
        private readonly static  Lazy<AspectContext> _instance=new Lazy<AspectContext>(()=>new AspectContext());

        private AspectContext()
        {

        }

        public static AspectContext instance => _instance.Value;

        public string MethodName { get; set; }
        public object[] Arguments { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public IServiceProvider Services { get; set; }
    }
}