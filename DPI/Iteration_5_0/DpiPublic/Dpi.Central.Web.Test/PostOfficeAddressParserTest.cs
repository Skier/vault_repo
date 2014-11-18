using System;
using DPI.Components;
using DPI.Interfaces;
using NUnit.Framework;

namespace Dpi.Central.Web.Test
{
    [TestFixture]
    public class PostOfficeAddressParserTest 
    {
        public string _void;
        private PostOfficeBoxAddressParser _sap = (PostOfficeBoxAddressParser)DeliveryAddressLineParserFactory.Instance.GetParser(DeliveryAddressType.PostOfficeBox);

        [Test]
        public void TestAccessors() {
            try {
                _void = _sap.Number;
                Assert.Fail("Exception is expected.");
            } catch (InvalidOperationException) {
            }
        }

        [Test]
        public void TestParserExceptions() {
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
        public void TestParsingAlgorithm() {
            _sap.Parse("P O Box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("P.O. Box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("PO. Box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("P.O Box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("PO Box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("Post Office Box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("PostOffice Box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("p o box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("p.o. box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("po. box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("p.o box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("po box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("post office box 249A");
            Assert.AreEqual("249A", _sap.Number); 

            _sap.Parse("postoffice box 249A");
            Assert.AreEqual("249A", _sap.Number); 
        }
    }
}
