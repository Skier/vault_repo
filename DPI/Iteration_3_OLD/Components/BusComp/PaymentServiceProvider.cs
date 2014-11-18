using System;
using System.Collections.Specialized;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace DPI.Components
{
    public enum PaymentResultCode
    {
        Completed,
        Rejected,
        NeedVerification,
        UnableToComplete
    }

    public struct PaymentResult
    {
        private PaymentResultCode _code;
		private string _providerCode;
        private string _description;
		private Payment _payment;

		public PaymentResult(PaymentResultCode code,  string description)
		{
			_code = code;
			_description = description;
			_payment = null;
			this._providerCode = null;
		}

        public PaymentResult(PaymentResultCode code, string providerCode,  string description)
        {
            _code = code;
			this._providerCode = providerCode;
            _description = description;
			_payment = null;
        }

        public PaymentResultCode Code
        {
            get { return _code; }
        }

		public String ProviderCode
		{
			get { return this._providerCode; }
		}


        public string Description
        {
            get { return _description; }
        }

		public Payment Payment
		{
			get
			{
				return _payment;
			}
			set
			{
				_payment = value;
			}
		}
		
    }

	public class PaymentServiceProvider
	{
 #region Request Fields

        private const string MERCHANT_CODE = "merchant_code";
		private const string TEST_MODE = "test_mode";
        private const string CUSTOMER_NUMBER = "customer_number";
        private const string PAYMENT_SOURCE = "payment_source";
        private const string BILL_LAST_NAME = "bill_last_name";
        private const string BILL_FIRST_NAME = "bill_first_name";
        private const string BILL_PHONE = "bill_phone";
        private const string BILL_ADDRESS = "bill_address_one";
        private const string BILL_CITY = "bill_city";
        private const string BILL_STATE = "bill_state";
        private const string BILL_ZIP = "bill_zip";
        private const string BILL_EMAIL = "bill_email";
        private const string BILL_DRIVER_LICENSE_NUMBER = "bill_driver_license_number";
        private const string BILL_DRIVER_LICENSE_STATE = "bill_driver_license_state";
        private const string ROUTING_NUMBER = "routing_number";
        private const string CHECKING_ACCOUNT_NUMBER = "checking_account_number";
        private const string CC_NUMBER = "credit_card_number";
        private const string CC_EXPIRATION_MONTH = "expire_month";
        private const string CC_EXPIRATION_YEAR = "expire_year";
        private const string CC_CV_NUMBER = "credit_card_verification_number";
        private const string PAYMENT_AMOUNT = "charge_total";

        #endregion

        #region Response Fields

        private const string RESPONSE_CODE = "response_code";
        private const string RESPONSE_CODE_DESC = "response_code_text";

        #endregion

        private const string RESPONSE_SEPARATOR = "\r\n";

        #region Configuration Keys

        private const string ECOMPLISH_CC_URL_KEY = "ecomplish_cc_url";
        private const string ECOMPLISH_CK_URL_KEY = "ecomplish_ck_url";

        #endregion

      

        #region IPaymentServiceProvider Members

        public virtual PaymentResult MakeCreditCardPayment(
            int accountNumber, CreditCard creditCard, decimal paymentAmount)
        {
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add(MERCHANT_CODE, "DPI_CC_90823481"); 
			// TODO:  Add read from config of test mode
			parameters.Add(TEST_MODE, "true");

            parameters.Add(PAYMENT_SOURCE, "Web");
            parameters.Add(CUSTOMER_NUMBER, accountNumber.ToString());
            parameters.Add(BILL_FIRST_NAME, creditCard.FirstName);  
            parameters.Add(BILL_LAST_NAME, creditCard.LastName);  
            parameters.Add(BILL_PHONE, creditCard.PhoneNumber);  
            parameters.Add(BILL_ADDRESS, creditCard.StreetAddress);
            parameters.Add(BILL_CITY, creditCard.City);  
            parameters.Add(BILL_STATE, creditCard.State);
            parameters.Add(BILL_ZIP, creditCard.Zip); 
            parameters.Add(BILL_EMAIL, creditCard.Email);
            parameters.Add(CC_NUMBER, creditCard.Number);
            parameters.Add(CC_EXPIRATION_MONTH, creditCard.ExpMonth.ToString()); 
            parameters.Add(CC_EXPIRATION_YEAR, creditCard.ExpYear.ToString()); 
            parameters.Add(CC_CV_NUMBER, creditCard.CvNumber);
            parameters.Add(PAYMENT_AMOUNT, paymentAmount.ToString());

            string url = ConfigurationSettings.AppSettings[ECOMPLISH_CC_URL_KEY];
            PaymentResult result = MakePayment(url, parameters, "1");
            return result;
        }

        // TODO: create parameters
        public virtual PaymentResult MakeCheckPayment(int accountNumber,BankCheck bankCheck, decimal paymentAmount)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add(MERCHANT_CODE, "DPI_CK_57815484"); // (please use DPI_01)
			// TODO:  Add read from config of test mode
			parameters.Add(TEST_MODE, "true");
			// TODO Read payment source from config
			parameters.Add(PAYMENT_SOURCE, "Web");   
            parameters.Add(CUSTOMER_NUMBER, accountNumber.ToString()); //  (place dpi account number in here) 
            parameters.Add(BILL_LAST_NAME, bankCheck.LastName ); //  
            parameters.Add(BILL_FIRST_NAME, bankCheck.FirstName); //  
            parameters.Add(BILL_PHONE, bankCheck.PhoneNumber); //  
            parameters.Add(BILL_ADDRESS, bankCheck.StreetAddress); //  
            parameters.Add(BILL_CITY, bankCheck.City); //  
            parameters.Add(BILL_STATE, bankCheck.State); //  
            parameters.Add(BILL_ZIP, bankCheck.Zip); //  
            parameters.Add(BILL_EMAIL, bankCheck.Email); //  
            parameters.Add(BILL_DRIVER_LICENSE_NUMBER, bankCheck.DriverLicenseNumber); //
            parameters.Add(BILL_DRIVER_LICENSE_STATE, bankCheck.DriverLicenseState); //
            parameters.Add(ROUTING_NUMBER, bankCheck.BankRoutingNumber); //
            parameters.Add(CHECKING_ACCOUNT_NUMBER, bankCheck.BankAccountNumber); //
            parameters.Add(PAYMENT_AMOUNT, paymentAmount.ToString()); //  (with a decimal) 

            string url = ConfigurationSettings.AppSettings[ECOMPLISH_CK_URL_KEY];
            PaymentResult result = MakePayment(url, parameters, "0");
            return result;
        }

        #endregion

        #region Private Methods

        private PaymentResult MakePayment(string url, NameValueCollection parameters, string successCode)
        {
            StringBuilder parametersSb = new StringBuilder();
            for (int i = 0; i < parameters.Count; i++) {
                EncodeAndAddItem(ref parametersSb, parameters.GetKey(i), parameters[i]);
            }

			

            try {
                string response = PostData(url, parametersSb.ToString());
                PaymentResult result = AnalyseResponse(response, successCode);
                return result;
            } catch (WebException ex) {

			//	System.Diagnostics.Debug.WriteLine(new StreamReader( ex.Response.GetResponseStream()).ReadToEnd());

                switch (ex.Status) {
                    case WebExceptionStatus.ReceiveFailure:
                        goto case WebExceptionStatus.Timeout;
                    case WebExceptionStatus.ServerProtocolViolation:
                        goto case WebExceptionStatus.Timeout;
                    case WebExceptionStatus.Timeout:
                        return new PaymentResult(PaymentResultCode.NeedVerification, ex.Message);
                    default:
                        return new PaymentResult(PaymentResultCode.UnableToComplete, ex.Message);
                }
            }
        }

        private PaymentResult AnalyseResponse(string response, string successCode)
        {

			string code = null, description = null;

			if (response.StartsWith("Error:"))
			{
				description = response.Substring(5, response.Length - 6);
				return new PaymentResult(PaymentResultCode.Rejected, description);
			}
            
			string[] parts = response.Split('\n');
			
			foreach (string str in parts)
			{
				string [] keyValue = str.Split('=');
				if (keyValue[0] == RESPONSE_CODE)
				{
					code = keyValue[1];
				}
				else if (keyValue[0] == RESPONSE_CODE_DESC)
				{
					description = keyValue[1];
				}
			}

			if (code != null && description != null) 
			{
				// TODO: clarify possible code values from Boris.

				if (code == successCode) 
				{
					return new PaymentResult(PaymentResultCode.Completed, code, description);
				} 
				else 
				{
					return new PaymentResult(PaymentResultCode.Rejected, code, description);
				}
			}
			else
			{
				return new PaymentResult(PaymentResultCode.UnableToComplete, "Invalid response format");
			}
            
        }

        /// <summary>
        /// Posts data to a specified url. Note that this assumes 
        /// that you have already url encoded the post data.
        /// </summary>
        /// <param name="postData">The data to post.</param>
        /// <param name="url">the url to post to.</param>
        /// <returns>Returns the result of the post.</returns>
        private string PostData(string url, string postData)
        {
            HttpWebRequest request = null;

            Uri uri = new Uri(url);
            request = (HttpWebRequest) WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;

            using (Stream writeStream = request.GetRequestStream()) {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(postData);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse()) {
                using (Stream responseStream = response.GetResponseStream()) {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8)) {
                        result = readStream.ReadToEnd();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Encodes an item and adds it to the string.
        /// </summary>
        /// <param name="baseRequest">The previously encoded data.</param>
        /// <param name="dataItem">The data to encode.</param>
        /// <returns>A string containing the old data and the previously encoded data.</returns>
        private void EncodeAndAddItem(ref StringBuilder baseRequest, string key, string dataItem)
        {
            if (baseRequest == null) {
                baseRequest = new StringBuilder();
            }

            if (baseRequest.Length != 0) {
                baseRequest.Append("&");
            }

            baseRequest.Append(key);
            baseRequest.Append("=");
            baseRequest.Append(HttpUtility.UrlEncode(dataItem));
        }

        #endregion
   
       
    }
}