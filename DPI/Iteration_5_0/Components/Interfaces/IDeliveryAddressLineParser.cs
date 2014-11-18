using System;
using System.Runtime.Serialization;

namespace DPI.Interfaces
{
    public interface IDeliveryAddressLineParser
    {
        void Parse(string deliveryAddressLine);
    }

    public class DeliveryAddressLineParserException : ApplicationException
    {
        public DeliveryAddressLineParserException()
        {
        }

        public DeliveryAddressLineParserException(string message) : base(message)
        {
        }

        public DeliveryAddressLineParserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DeliveryAddressLineParserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}