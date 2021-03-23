using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Sample.Products.Backend.Business.Concrete.Models
{
    public class ServiceResponse<T>:IServiceResponse
    {
        public bool HasExceptionError => !string.IsNullOrEmpty(ExceptionMessage);

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptionMessage { get; set; }

        public IList<T> List { get; set; }

        [JsonProperty]
        public T Entity { get; set; }

        public int Count { get; set; }

        public bool IsValid => !HasExceptionError && string.IsNullOrEmpty(ExceptionMessage);

        public bool IsSuccessful { get; set; }

        public ServiceResponse()
        {
            List = new List<T>();
        }
    }

    public interface IServiceResponse
    {
        public bool HasExceptionError { get; }

        public string ExceptionMessage { get; set; }


        public bool IsSuccessful { get; set; }
    }
}