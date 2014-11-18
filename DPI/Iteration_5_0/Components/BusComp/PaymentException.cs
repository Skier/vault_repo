using System;
using System.Runtime.Serialization;
using DPI.Interfaces;

namespace DPI.Components
{
    [Serializable]
    public class PaymentException : Exception
    {
        private Payment _payment;

        public PaymentException(Payment payment) : this(payment, string.Empty)
        {
        }

        public PaymentException(Payment payment, string message) : this(payment, message, null)
        {
        }

        public PaymentException(Payment payment, string message, Exception innerException) : base(message, innerException)
        {
            _payment = payment;
        }

        protected PaymentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _payment = (Payment)info.GetValue("payment", typeof(Payment));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) 
        {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            info.AddValue("payment", _payment, typeof(Payment));

            base.GetObjectData (info, context);
        }


        public Payment Payment
        {
            get { return _payment; }
        }

        public string ConfirmationNumber
        {
            get
            {
                if (_payment != null && _payment.PaymentInfo != null) {
                    if (_payment.PaymentInfo.RowState != RowState.New) {
                        return _payment.PaymentInfo.Id.ToString();
                    }
                }

                return "N/A";
            }
        }
    }
}