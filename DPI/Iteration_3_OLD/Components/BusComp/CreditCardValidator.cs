using System;

namespace DPI.Components
{
    internal class CreditCardValidator
    {
        #region Helper classes


        private class Interval
        {
            private int _lowerBound;
            private int _upperBound;

            public Interval(int lowerBound, int upperBound)
            {
                _lowerBound = lowerBound;
                _upperBound = upperBound;
            }

            public Interval(int bound) : this(bound, bound)
            {
            }

            public bool IsIncluded(int number) 
            {
                return (number >= _lowerBound) && (number <= _upperBound);
            }
        }

        private class IntervalArray //: IInterval
        {
            private Interval[] _intervals;

            public IntervalArray(Interval[] intervals)
            {
                _intervals = intervals;
            }

            public bool IsIncluded(int number)
            {
                foreach (Interval interval in _intervals) {
                    if (interval.IsIncluded(number)) {
                        return true;
                    }
                }

                return false;
            }
        }

        private class CreditCardProvider
        {
            private CreditCardType _ccType;
            private int _length = 0;
            private IntervalArray _intervals;

            public CreditCardProvider(CreditCardType creditCardType, int length, IntervalArray intervals)
            {
                _ccType = creditCardType;
                _length = length;
                _intervals = intervals;
            }

            public bool IsIncluded(int number)
            {
                return _intervals.IsIncluded(number);
            }

            public int GetLength()
            {
                return _length;
            }

            public CreditCardType GetCreditCardType()
            {
                return _ccType;
            }
        }

        private class CreditCardProviderArray
        {
            private CreditCardProvider[] _providers;

            public CreditCardProviderArray(CreditCardProvider[] providers)
            {
                _providers = providers;
            }

            public CreditCardProvider GetProvider(int number)
            {
                foreach (CreditCardProvider provider in _providers) {
                    if (provider.IsIncluded(number)) {
                        return provider;
                    }
                }

                return null;
            }
        }

        #endregion

        #region Constants

        private const int CC_NUMBER_MAX_LENGTH = 30;
        private const int CC_NUMBER_MIN_LENGTH = 4; // TODO: Subject to change
        private const int CC_TYPE_ID_LENGTH = 4;

        // Some exception situations
        private const int VISA_CC_NUMBER_OTHER_LENGTH = 13;
        private const int MASTER_CARD_CC_NUMBER_OTHER_LENGTH = 14;

        #endregion

        #region Static Members

        // TIP: Information about credit card number prefixes was got from
        // http://en.wikipedia.org/wiki/Credit_card_number (last modified 19:49, 19 October 2005)

        private static readonly CreditCardProvider c_american_express_provider = new CreditCardProvider(
            CreditCardType.AmericanExpress, 15,
            new IntervalArray(new Interval[] {
                new Interval(3400, 3499),
                new Interval(3700, 3799)
            }));

        

        private static readonly CreditCardProvider c_discover_card_provider = new CreditCardProvider(
            CreditCardType.DiscoverCard, 16,
            new IntervalArray(new Interval[] {
                new Interval(6011),
                new Interval(6500, 6509)
            }));

       

        private static readonly CreditCardProvider c_master_card_provider = new CreditCardProvider(
            CreditCardType.MasterCard, 16,
            new IntervalArray(new Interval[] {
                new Interval(5100, 5599)
            }));

        private static readonly CreditCardProvider c_visa_provider = new CreditCardProvider(
            CreditCardType.VISA, 16,
            new IntervalArray(new Interval[] {
                new Interval(4000, 4999)
            }));

        private static readonly CreditCardProviderArray c_providers = new CreditCardProviderArray(
            new CreditCardProvider[] {
                c_american_express_provider, 
                c_discover_card_provider, 
				c_master_card_provider, 
				c_visa_provider
            });

        #endregion

        #region Public Methods

        /// <summary>
        /// Throws CreditCardValidatorException when 
        /// credit card does not pass validation.
        /// </summary>
        public void Validate(CreditCard cc)
        {
            // Checking for min/max length criteria
            if (cc.Number.Length > CC_NUMBER_MAX_LENGTH) {
                throw new CreditCardValidationException(
                    CreditCardValidationException.CC_NUMBER_TOO_LONG_CODE, cc.Number.Length);
            } else if (cc.Number.Length < CC_NUMBER_MIN_LENGTH) {
                throw new CreditCardValidationException(
                    CreditCardValidationException.CC_NUMBER_TOO_SHORT_CODE, cc.Number.Length);
            }

            // Checking for invalid characters absence (only decimal digits allowed)
            for (int index = 0; index < cc.Number.Length; index ++) {
                if (!char.IsDigit(cc.Number[index])) {
                    throw new CreditCardValidationException(
                        CreditCardValidationException.CC_NUMBER_INVALID_CHARS_CODE);
                }
            }

            // Checking for credit card type (by first CC_TYPE_ID_LENGTH digits)
            int ccTypeId = int.Parse(cc.Number.Substring(0, CC_TYPE_ID_LENGTH));
            CreditCardProvider provider = c_providers.GetProvider(ccTypeId);
            if (null == provider) {
                throw new CreditCardValidationException(
                    CreditCardValidationException.CC_TYPE_UNKNOWN_CODE, ccTypeId);
            }

            // check is credit card type from number is the same as from credit card info
            CreditCardType providerCreditCardType = provider.GetCreditCardType();
            if (providerCreditCardType != cc.Type) {
                throw new CreditCardValidationException(
                    CreditCardValidationException.CC_INVALID_TYPE_NUMBER_CODE, cc.Number, cc.Type);
            }

            // Checking whether credit card number length corresponds with credit card type requirement
            if (provider.GetLength() != cc.Number.Length) {
                if (CreditCardType.VISA == provider.GetCreditCardType()) {
                    if (VISA_CC_NUMBER_OTHER_LENGTH != cc.Number.Length) {
                        throw new CreditCardValidationException(
                            CreditCardValidationException.CC_NUMBER_LENGTH_INVALID_CODE,
                            cc.Number.Length, string.Format("{0} or {1}", VISA_CC_NUMBER_OTHER_LENGTH, provider.GetLength()));
                    }
                } else if (CreditCardType.MasterCard == provider.GetCreditCardType()) {
                    if (MASTER_CARD_CC_NUMBER_OTHER_LENGTH != cc.Number.Length) {
                        throw new CreditCardValidationException(
                            CreditCardValidationException.CC_NUMBER_LENGTH_INVALID_CODE,
                            cc.Number.Length, string.Format("{0} or {1}", MASTER_CARD_CC_NUMBER_OTHER_LENGTH, provider.GetLength()));
                    }
                } else {
                    throw new CreditCardValidationException(
                        CreditCardValidationException.CC_NUMBER_LENGTH_INVALID_CODE, cc.Number.Length, provider.GetLength());
                }
            }

            // Luhn algorythm implementation
            // See http://en.wikipedia.org/wiki/Luhn_formula
            int checkSum = 0;
            int parity = cc.Number.Length % 2;
            for (int index = 0; index < cc.Number.Length; index ++) {
                int digit = int.Parse(cc.Number.Substring(index, 1));
                if (parity == index % 2) {
                    digit *= 2;
                }
                
                if (digit > 9) {
                    digit -= 9;
                }

                checkSum += digit;
            }

            // Checking whether credit card number is valid
            if (0 != checkSum % 10) {
                throw new CreditCardValidationException(
                    CreditCardValidationException.CC_NUMBER_INVALID_CODE);
            }

            // Checking whether credit card is expired
            if (IsExpired(cc)) {
                throw new CreditCardValidationException(CreditCardValidationException.CC_EXPIRED_CODE);
            }

            // Credit card is ok
        }

        #endregion

        #region Private Methods

        private bool IsExpired(CreditCard cc) 
        {
            DateTime now = DateTime.Now;
            return ((now.Year > cc.ExpYear) || ((now.Year == cc.ExpYear) 
                && (now.Month > cc.ExpMonth)));
        }

        #endregion
    }
}