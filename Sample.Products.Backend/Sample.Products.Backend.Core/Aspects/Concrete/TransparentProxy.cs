using System;
using System.Linq;
using System.Reflection;
using Sample.Products.Backend.Core.Aspects.Interfaces;

namespace Sample.Products.Backend.Core.Aspects.Concrete
{
    public class TransparentProxy<T> : DispatchProxy
    {
        private T _decorate;
        protected object response=null;

        //public TransparentProxy(T decorate, IServiceProvider services)
        //{

        //    _decorate = decorate;
        //    AspectContext.instance.Services = services;
        //}

        //public T GenerateProxy()
        //{
        //    Type type = this._decorate.GetType();
        //    object instance = Create<T, TransparentProxy<T>>();
        //    //((TransparentProxy<T>)instance).SetParameters(_decorate);
        //    return (T)instance;
        //}

        public static T GenerateProxy(T decorate,IServiceProvider service)
        {
            object instance = Create<T, TransparentProxy<T>>();
            ((TransparentProxy<T>)instance).SetParameters(((T)decorate));
            AspectContext.instance.Services = service;
            return (T)instance;
        }

        private void SetParameters(T decorated)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            _decorate = decorated;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                var param = targetMethod.GetParameters();
                //Tanımlı Attribute alınıyor.
                //MethodInfo da tanımlı getcustomattribute fonksiyonu çalışmıyor.
                //bu yüzden decorate den alıyoruz attribute listesini
                //MethodInfo da 
                
                var methodAspects = _decorate.GetType().GetMethod(targetMethod.Name)!.GetCustomAttributes().ToArray();
                var typeAttributes=_decorate.GetType().GetCustomAttributes()?.Where(x => x is ITypeAspectForMethods).ToArray();
                Attribute[] aspects = new Attribute[methodAspects.Length + (typeAttributes?.Length ?? 0)];
                methodAspects.CopyTo(aspects,0);
                typeAttributes.CopyTo(aspects,methodAspects.Length);
                
                //Method bilgileri Aspecte aktarılıyor.
                FillAspectContext(targetMethod, args);

                //object response = null;
                //Method Öncesinde Tanımlı aspect kontrolu
                CheckBeforeAspect( aspects, param);

                // Response boş değilse, buradaki veri cache üzerinden de geliyor olabilir ve tekrardan invoke etmeye
                // gerek yok, direkt olarak geriye response dönebiliriz bu durumda.
                if (response != null)
                {
                    return response;
                }
                //Method çalıştırılıyor.
                var runAttr = aspects.FirstOrDefault(x=>x is INRunAspect);
                if(runAttr==null)
                {
                    response = targetMethod.Invoke(_decorate, args);
                }
                else
                {
                   response= ((INRunAspect) runAttr).OnRun(targetMethod,_decorate,args);
                }


                //Method Bittikten sonra çalıştırılacak Aspectler
                CheckAfterAspect(response, aspects);

                // After aspectlerimizi'de çalıştırdıktan sonra artık geriye çıktıyı dönebiliriz.
                return response;
            }
            catch (Exception e)
            {
                return new Exception("");
            }
        }

        protected void FillAspectContext(MethodInfo methodCallMessage,object[] args)
        {
            AspectContext.instance.MethodName = methodCallMessage.Name;
            AspectContext.instance.Arguments = args;
            AspectContext.instance.MethodInfo = methodCallMessage;
        }

        protected void CheckBeforeAspect( object[] aspects,ParameterInfo[] prms)
        {
            foreach (IAspect loopAttribute in aspects)
            {
                if (loopAttribute is IBeforeVoidAspect)
                {
                    ((IBeforeVoidAspect)loopAttribute).OnBefore(prms);
                }
                else if (loopAttribute is IBeforeAspect)
                {
                    response = ((IBeforeAspect)loopAttribute).OnBefore();
                }
            }
        }

        protected void CheckAfterAspect(object result, object[] aspects)
        {
            foreach (IAspect loopAttribute in aspects)
            {
                if (loopAttribute is IAfterVoidAspect)
                {
                    ((IAfterVoidAspect)loopAttribute).OnAfter(result);
                }
            }
        }
    }
}