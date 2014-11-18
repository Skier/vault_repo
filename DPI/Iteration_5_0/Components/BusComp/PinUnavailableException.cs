using System;
using System.Runtime.Serialization;

namespace DPI.Components
{
    [Serializable]
    public class PinUnavailableException : Exception
    {
        private const string MESSAGE = "Pin is unavailable. Wireless product ID is {0}.";

        private int _wirelessProductId;

        public PinUnavailableException(int wirelessProductId) : base(string.Format(MESSAGE, wirelessProductId))
        {
            _wirelessProductId = wirelessProductId;
        }

        protected PinUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _wirelessProductId = info.GetInt32("wirelessProductId");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) 
        {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            info.AddValue("wirelessProductId", _wirelessProductId, typeof(Int32));

            base.GetObjectData (info, context);
        }

        public int WirelessProductId
        {
            get { return _wirelessProductId; }
        }
    }
}