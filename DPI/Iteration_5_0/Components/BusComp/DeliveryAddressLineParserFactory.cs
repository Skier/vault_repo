using System;
using DPI.Interfaces;

namespace DPI.Components
{
    public enum DeliveryAddressType 
    {
        StreetAddress,
        PostOfficeBox, 
        Military,
        RuralRoute, 
        HighwayContractRoute, 
        GeneralDelivery,
        PuertoRico
    }

    public class DeliveryAddressLineParserFactory
	{
        private static DeliveryAddressLineParserFactory _instance;

        public static DeliveryAddressLineParserFactory Instance
        {
            get 
            {
                if (_instance == null) {
                    lock (typeof(DeliveryAddressLineParserFactory)) {
                        if (_instance == null) {
                            _instance = new DeliveryAddressLineParserFactory();
                        }
                    }
                }

                return _instance;
            }
        }

		private DeliveryAddressLineParserFactory()
		{
		}

        public IDeliveryAddressLineParser GetParser(DeliveryAddressType type) 
        {
            if (type == DeliveryAddressType.StreetAddress) {
                return new StreetAddressParser();
            } else if (type == DeliveryAddressType.PostOfficeBox) {
                return new PostOfficeBoxAddressParser();
            }

            throw new NotImplementedException("Parser for type " + type + " is not implemented.");
        }

        public IDeliveryAddressLineParser Parse(string deliveryAddressLine)
        {
            if (deliveryAddressLine == null) {
                throw new ArgumentNullException("deliveryAddressLine");
            }

            IDeliveryAddressLineParser parser;

            try {
                parser = new PostOfficeBoxAddressParser();
                parser.Parse(deliveryAddressLine);
            } catch (DeliveryAddressLineParserException) {
                try {
                    parser = new StreetAddressParser();
                    parser.Parse(deliveryAddressLine);
                } catch (DeliveryAddressLineParserException) {
                    throw new DeliveryAddressLineParserException("Delivery address line has incorrect or unsupported format: [" + deliveryAddressLine + "].");
                }
            }

            return parser;
        }
	}
}
