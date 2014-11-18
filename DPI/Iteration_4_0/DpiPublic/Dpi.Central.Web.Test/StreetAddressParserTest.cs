using System;
using DPI.Components;
using DPI.Interfaces;
using NUnit.Framework;

namespace Dpi.Central.Web
{
    [TestFixture]
    public class StreetAddressParserTest
    {
        public string _void;
        private StreetAddressParser _sap = (StreetAddressParser)DeliveryAddressLineParserFactory.Instance.GetParser(DeliveryAddressType.StreetAddress);

        [Test]
        public void TestAccessors()
        {
            try {
                _void = _sap.PrimaryAddressNumber;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }

            try {
                _void = _sap.Predirectional;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }

            try {
                _void = _sap.StreetName;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }

            try {
                _void = _sap.Suffix;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }

            try {
                _void = _sap.Postdirectinal;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }

            try {
                _void = _sap.SecondaryAddressIdentifier;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }

            try {
                _void = _sap.SecondaryAddressRange;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }
        }

        [Test]
        public void TestParserExceptions() 
        {
            try {
                _sap.Parse(null);
                Assert.Fail("Exception is expected.");
            } catch (ArgumentNullException) {
            }

            try {
                _sap.Parse(string.Empty);
                Assert.Fail("Exception is expected.");
            } catch (ArgumentException) {
            }

            try {
                _sap.Parse(" , , , , ");
                Assert.Fail("Exception is expected.");
            } catch (DeliveryAddressLineParserException) {
            }

            try {
                _sap.Parse(" qwe, , , , ");
                Assert.Fail("Exception is expected.");
            } catch (DeliveryAddressLineParserException) {
            }

            try {
                _sap.Parse("QWE RTY UIO");
                Assert.Fail("Exception is expected.");
            } catch (DeliveryAddressLineParserException) {
            }
        }

        [Test]
        public void TestParsingAlgorithm()
        {
            _sap.Parse("9 Hill");
            Assert.AreEqual("9", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("Hill", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("2997 LBJ Freeway");
            Assert.AreEqual("2997", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("LBJ", _sap.StreetName);
            Assert.AreEqual("FWY", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A SAMPLE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A SAMPLE ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAMPLE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAMPLE ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAMPLE NE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAMPLE ST NE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAMPLE NE ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAMPLE NE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAMPLE ST NE STE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("STE", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAMPLE ST NE STE 509A");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAMPLE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("STE", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("509A", _sap.SecondaryAddressRange);

            // Space Street Name ///////////////////////////////////

            _sap.Parse("249A SAM P LE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("SAM P LE", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A SAM P LE ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("SAM P LE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SAM P LE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SAM P LE", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SA M PL E ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SA M PL E", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SA M PL E NE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SA M PL E", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SA M PL E ST NE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SA M PL E", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SA M PL E NE ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SA M PL E NE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SA M PL E ST NE STE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SA M PL E", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("STE", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W SA M PL E ST NE STE 509A");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("SA M PL E", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("STE", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("509A", _sap.SecondaryAddressRange);

            // Space Mixed Street Name ///////////////////////////////

            _sap.Parse("249A ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("ST", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("W", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W W ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("W", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W W NE NE ST");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("W NE NE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);
            
            _sap.Parse("249A W W NE NE ST NE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("W NE NE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);
            
            _sap.Parse("249A W W NE NE ST NE STE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("W NE NE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("STE", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);
             
            _sap.Parse("249A W W NE NE ST NE STE STE");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("W NE NE ST NE STE", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("STE", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);

            _sap.Parse("249A W W NE NE ST NE STE 509A");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("W", _sap.Predirectional);
            Assert.AreEqual("W NE NE", _sap.StreetName);
            Assert.AreEqual("ST", _sap.Suffix);
            Assert.AreEqual("NE", _sap.Postdirectinal);
            Assert.AreEqual("STE", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("509A", _sap.SecondaryAddressRange);

            _sap.Parse("249A QWE RTY UIO");
            Assert.AreEqual("249A", _sap.PrimaryAddressNumber); 
            Assert.AreEqual("", _sap.Predirectional);
            Assert.AreEqual("QWE RTY UIO", _sap.StreetName);
            Assert.AreEqual("", _sap.Suffix);
            Assert.AreEqual("", _sap.Postdirectinal);
            Assert.AreEqual("", _sap.SecondaryAddressIdentifier);
            Assert.AreEqual("", _sap.SecondaryAddressRange);
        }
    }
}