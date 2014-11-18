using System;
using System.Collections;

namespace DPI.Components
{
    public class CreditCardValidationException : ApplicationException
    {
        #region Constants

        public const string CC_NUMBER_LENGTH_INVALID_CODE = "CCV-01";
        public const string CC_NUMBER_TOO_SHORT_CODE = "CCV-02";
        public const string CC_NUMBER_TOO_LONG_CODE = "CCV-03";
        public const string CC_NUMBER_INVALID_CHARS_CODE = "CCV-04";
        public const string CC_TYPE_UNKNOWN_CODE = "CCV-05";
        public const string CC_NUMBER_INVALID_CODE = "CCV-06";
        public const string CC_EXPIRED_CODE = "CCV-07";
        public const string CC_INVALID_TYPE_NUMBER_CODE = "CCV-08";

        private const string CC_NUMBER_LENGTH_INVALID_MSG = "Credit card number length ({0}) is invalid (should be {1}).";
        private const string CC_NUMBER_TOO_SHORT_MSG = "Credit card number length ({0}) is too short.";
        private const string CC_NUMBER_TOO_LONG_MSG = "Credit card number length ({0}) is too long.";
        private const string CC_NUMBER_INVALID_CHARS_MSG = "Credit card number contains invalid characters.";
        private const string CC_TYPE_UNKNOWN_MSG = "Credit card type with prefix ({0}) is unknown or is not supported.";
        private const string CC_NUMBER_INVALID_MSG = "Credit card number is invalid.";
        private const string CC_EXPIRED_MSG = "Credit card is expired.";
        private const string CC_INVALID_TYPE_NUMBER_MSG = "Credit card number ({0}) is invalid for credit card type ({1}).";

        #endregion

        #region Static Members

        private static readonly Hashtable s_messages = new Hashtable();

        static CreditCardValidationException()
        {
            AddMessage(CC_NUMBER_LENGTH_INVALID_CODE, CC_NUMBER_LENGTH_INVALID_MSG);
            AddMessage(CC_NUMBER_TOO_SHORT_CODE, CC_NUMBER_TOO_SHORT_MSG);
            AddMessage(CC_NUMBER_TOO_LONG_CODE, CC_NUMBER_TOO_LONG_MSG);
            AddMessage(CC_NUMBER_INVALID_CHARS_CODE, CC_NUMBER_INVALID_CHARS_MSG);
            AddMessage(CC_TYPE_UNKNOWN_CODE, CC_TYPE_UNKNOWN_MSG);
            AddMessage(CC_NUMBER_INVALID_CODE, CC_NUMBER_INVALID_MSG);
            AddMessage(CC_EXPIRED_CODE, CC_EXPIRED_MSG);
            AddMessage(CC_INVALID_TYPE_NUMBER_CODE, CC_INVALID_TYPE_NUMBER_MSG);
        }

        private static void AddMessage(string code, string message)
        {
            s_messages[code] = message;
        }

        #endregion

        #region Fields

        private string _code;

        #endregion

        #region Constructors

        public CreditCardValidationException(string code, params object[] args)
            : base(string.Format((string)s_messages[code], args))
        {
            _code = code;
        }

        #endregion

        #region Properties

        public string Code
        {
            get { return _code; }
        }

        #endregion
    }
}