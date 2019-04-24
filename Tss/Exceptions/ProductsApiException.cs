using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tss
{
    public class ProductsApiException : Exception
    {
        public ProductsApiException()
        {
        }

        public ProductsApiException(string message) : base(message)
        {
        }

        public ProductsApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductsApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}