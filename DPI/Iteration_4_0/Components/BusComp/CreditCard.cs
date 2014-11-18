using System;

namespace DPI.Components
{
    public enum CreditCardType
    {
        AmericanExpress,
        DiscoverCard,
        MasterCard,
        VISA
    }

    public class CreditCard
    {
        #region Constants

        public const int TYPE_MAX_LENGTH = 10;
        public const int NUMBER_MAX_LENGTH = 2048;
        public const int CV_NUMBER_MAX_LENGTH = 10;
        public const int BILL_FIRST_NAME_MAX_LENGTH = 25;
        public const int BILL_LAST_NAME_MAX_LENGTH = 25;
        public const int BILL_ZIP_MAX_LENGTH = 5;
        public const int BILL_STATE_MAX_LENGTH = 2;
        public const int BILL_CITY_MAX_LENGTH = 25;
        public const int BILL_ADDRESS_MAX_LENGTH = 35;
        public const int BILL_PHONE_NUMBER_MAX_LENGTH = 15;
        public const int BILL_EMAIL_MAX_LENGTH = 40;

        #endregion

        #region Fields

        private CreditCardType _type;
        private string _number;
        private string _cvNumber;
        private int _expMonth;
        private int _expYear;
        private string _firstName;
        private string _lastName;
        private string _zip;
        private string _state;
        private string _city;
        private string _streetAddress;
        private string _phoneNumber;
        private string _email;

        #endregion

        #region Constructors

        private CreditCard()
        {
        }

        public CreditCard(
            CreditCardType type, string number, string cvNumber,
            int expMonth, int expYear, string firstName, string lastName,
            string zip, string state, string city, string address,
            string phoneNumber, string email)
        {
            this._type = type;
            
            if (number == null || number.Trim().Length == 0) {
                throw new ArgumentException("number");
            }
            
            this._number = number;

            if (cvNumber == null || cvNumber.Trim().Length > CV_NUMBER_MAX_LENGTH) {
                throw new ArgumentException("cvNumber");
            }
            
            this._cvNumber = cvNumber;

            if (expMonth < 1 && expMonth > 12) {
                throw new ArgumentException("ExpMonth");
            }

            this._expMonth = expMonth;

            if (expYear < DateTime.Now.Year && expYear > DateTime.Now.Year + 20) {
                throw new ArgumentException("ExpYear");
            }
            
            this._expYear = expYear;

            if (firstName == null || firstName.Length > BILL_FIRST_NAME_MAX_LENGTH) {
                throw new ArgumentException("firstName");
            }
            
            this._firstName = firstName;

            if (lastName == null || lastName.Length > BILL_LAST_NAME_MAX_LENGTH) {
                throw new ArgumentException("lastName");
            }
            
            this._lastName = lastName;

            if (zip == null || zip.Length > BILL_ZIP_MAX_LENGTH) {
                throw new ArgumentException("zip");
            }
            
            this._zip = zip;

            if (state == null || state.Length > BILL_STATE_MAX_LENGTH) {
                throw new ArgumentException("state");
            }
            
            this._state = state;

            if (city == null || city.Length > BILL_CITY_MAX_LENGTH) {
                throw new ArgumentException("city");
            }
            
            this._city = city;

            if (address == null || address.Length > BILL_ADDRESS_MAX_LENGTH) {
                throw new ArgumentException("address");
            }
            
            this._streetAddress = address;

            if (phoneNumber == null || phoneNumber.Length > BILL_PHONE_NUMBER_MAX_LENGTH) {
                throw new ArgumentException("phoneNumber");
            }
            
            this._phoneNumber = phoneNumber;

            if (email == null || email.Length > BILL_EMAIL_MAX_LENGTH) {
                throw new ArgumentException("email");
            }

            this._email = email;

            CreditCardValidator validator = new CreditCardValidator();
            validator.Validate(this);
        }

        #endregion

        #region Methods

        public static CreditCardType GetCreditCardType(string number)
        {
            CreditCard creditCard = new CreditCard();

            creditCard._number = number;
            creditCard._expMonth = DateTime.Now.Month;
            creditCard._expYear = DateTime.Now.Year + 10;

            CreditCardValidator validator = new CreditCardValidator();

            try {
                creditCard._type = CreditCardType.AmericanExpress;
                validator.Validate(creditCard);
                return CreditCardType.AmericanExpress;
            } catch (CreditCardValidationException) {
            }

            try {
                creditCard._type = CreditCardType.DiscoverCard;
                validator.Validate(creditCard);
                return CreditCardType.DiscoverCard;
            } catch (CreditCardValidationException) {
            }

            try {
                creditCard._type = CreditCardType.MasterCard;
                validator.Validate(creditCard);
                return CreditCardType.MasterCard;
            } catch (CreditCardValidationException) {
            }

            try {
                creditCard._type = CreditCardType.VISA;
                validator.Validate(creditCard);
                return CreditCardType.VISA;
            } catch (CreditCardValidationException) {
            }

            throw new ApplicationException("Credit card number is invalid.");
        }

        #endregion

        #region Properties

        public CreditCardType Type
        {
            get { return _type; }
        }

        public string Number
        {
            get { return _number; }
        }

        public string CvNumber
        {
            get { return _cvNumber; }
        }

        public int ExpMonth
        {
            get { return _expMonth; }
        }

        public int ExpYear
        {
            get { return _expYear; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public string Zip
        {
            get { return _zip; }
        }

        public string State
        {
            get { return _state; }
        }

        public string City
        {
            get { return _city; }
        }

        public string StreetAddress
        {
            get { return _streetAddress; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
        }

        public string Email
        {
            get { return _email; }
        }

        #endregion
    }
}