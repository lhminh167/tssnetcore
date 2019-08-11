using System;
using System.Runtime.Serialization;

namespace Tss.Core.Exceptions
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