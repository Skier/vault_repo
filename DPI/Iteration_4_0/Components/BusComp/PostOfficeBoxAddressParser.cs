using System;
using System.Text.RegularExpressions;
using DPI.Interfaces;

namespace DPI.Components
{
    /// <summary>
    /// Implements <see cref="IDeliveryAddressLineParser"/>IDeliveryAddressLineParser</see> 
    /// interface for parsing a delivery address line in US post office box address format.
    /// 
    /// <DeliveryAddressLine>::=
    ///       'P O Box' | 'P.O. Box' | 'PO Box' | 'Post Office Box'
    ///       <Separator><Number>
    ///      
    /// <Separator>::=' '
    /// <Number>::=^\d+$
    /// </summary>
	public class PostOfficeBoxAddressParser : IDeliveryAddressLineParser
	{
        #region Constants

        private const string INVALIDE_FORMAT_MSG = "Address format is invalid.";

        #endregion

        #region Fields

        private string _number;
        private bool _parsed;

        #endregion

        #region Constructors

        internal PostOfficeBoxAddressParser()
        {
            Reset();
        }

        #endregion

        #region IDeliveryAddressLineParser implementation

        public void Parse(string deliveryAddressLine)
        {
            Reset();

            if (deliveryAddressLine == null) {
                throw new ArgumentNullException("deliveryAddressLine");
            }

            deliveryAddressLine = deliveryAddressLine.Trim();

            if (deliveryAddressLine == string.Empty) {
                throw new ArgumentException("Delivery address line can not be empty.");
            }

            Regex r = new Regex(@"^(?<pobox>(P\.?\s*O\.?\s+Box)|(Post\s*Office\s+Box))\s+(?<number>\w+)$", RegexOptions.IgnoreCase);
            Match m = r.Match(deliveryAddressLine);
            if (m == null || m.Captures.Count == 0 || m.Captures[0].Value != deliveryAddressLine) {
                throw new DeliveryAddressLineParserException(INVALIDE_FORMAT_MSG);
            }

            _number = m.Result("${number}");

            _parsed = true;
        }

        #endregion

        #region Private Methods

        private void Reset() 
        {
            _parsed = false;
            _number = string.Empty;
        }

        private void AssertParsed() 
        {
            if (!_parsed) {
                throw new InvalidOperationException("No delivery address line has been parsed.");
            }
        }

        #endregion

        public string Number 
        {
            get 
            {
                AssertParsed();
                return _number;
            }
        }
	}
}
