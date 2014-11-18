using System;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Controllers
{
    public class PromiseToPayController : ControllerBase
    {
        #region Static Members

        private static PromiseToPayController _instance;

        public static PromiseToPayController Instance
        {
            get
            {
                lock (typeof (PromiseToPayController)) {
                    if (_instance == null) {
                        _instance = new PromiseToPayController();
                    }
                }

                return _instance;
            }
        }

        #endregion

        protected PromiseToPayController() : base()
        {
        }

        public bool IsEligibleForPromiseToPay() 
        {
            return CustSvc.IsEligibleForPromiseToPay(Map, AccountNumber);
        }

        public bool DoesPromiseToPayExist() 
        {
            return CustSvc.DoesPromiseToPayExist(Map, AccountNumber);
        }

        public PromiseToPay GetPromiseToPay() 
        {
            return CustSvc.GetPromiseToPay(Map, AccountNumber);
        }

        public DateTime GetPromiseToPayDate()
        {
            return CustSvc.GetPromiseToPayDate(Map, AccountNumber);
        }

        public void MakePromiseToPay(DateTime payDate, decimal payAmount)
        {
            CustSvc.MakePromiseToPay(Map, AccountNumber, 
                payDate, payAmount, Const.PUBLIC_WEB_USERID);

            SendConfirmation(payDate, payAmount);
        }

        private void SendConfirmation(DateTime payDate, decimal payAmount) 
        {
            ICustInfoExt2 custInfo = RetrieveCustInfoExt2();

            MailMessage msg = new MailMessage();

            msg.AddEmailTo(custInfo.CustInfo.Email);

            msg.EmailMessageType = MessageType.HTML;
            msg.EmailFrom = "customerservice@dpiteleconnect.com";
            msg.EmailSubject = "dPi Teleconnect Web Access";
            msg.EmailMessage = string.Format("Hello {0}.<br><br>"
                + "This email confirms that we have received your promise to " 
                + "pay {1} on {2}.<br><br>"                
                + "If you have any questions, please call Customer Service at: " 
                + "1-800-350-4009.<br><br>" 
                + "Thank you.<br><br>" 
                + "dPi Teleconnect Customer Service<br>"
                + "email: <a href=\"mailto:customerservice@dpiteleconnect.com\">" 
                + "customerservice@dpiteleconnect.com</a><br>"
                + "web: <a href=\"http://www.dpiteleconnect.com\">www.dpiteleconnect.com</a><br>"
                + "phone: 1-800-350-4009<br>", Convertor.MakeFriendlyName(custInfo.CustInfo.FirstName),
                payAmount.ToString("C"), payDate.ToLongDateString());

            msg.SendMail();
        }
    }
}