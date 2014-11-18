/*
 * Copyright (c) 2009 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 * Contributors:
 *
 *    Intuit Partner Platform - initial contribution 
 *
 */
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.IO;
using Intuit.Ipp.Saml.Security;


namespace Intuit.Ipp.Saml
{
    public class SamlResponse
    {
        #region Member Variables
        private readonly string _mTargetUrl = string.Empty;
        private string _mTicket = string.Empty;
        private readonly string _mRealmId = string.Empty;
        private readonly string _mRealmIdPseudonym = string.Empty;

        readonly SamlAttributes _mAttributeCol;
        
        private string _mSessionKeyAlgorithm = EncryptedXml.XmlEncAES256Url;
        
        private readonly XmlDocument _mEncryptedAssertion;
        private XmlDocument _mAssertion;

        #endregion Member Variables

        #region Constructors

        /// <summary>
        /// Process SAML response received from Intuit workplace. 
        /// </summary>
        /// <param name="encryptedAssertion">Encrypted request from workpalce</param>
        /// <param name="privateKey">Private key used for encrypting SAML</param>
        /// <param name="intuitPublicKey">Intuit's public key to be used for Signature verification</param>
        public SamlResponse(XmlDocument encryptedAssertion, X509Certificate2 privateKey, X509Certificate2 intuitPublicKey)
        {
            
            _mAttributeCol = new SamlAttributes(encryptedAssertion.OuterXml);
            _mTargetUrl = _mAttributeCol.GetAttribute(SamlConstants.AttributeName.TARGET_URL);
            _mRealmId = _mAttributeCol.GetAttribute(SamlConstants.AttributeName.REALM);
            _mRealmIdPseudonym = _mAttributeCol.GetAttribute(SamlConstants.AttributeName.INTUIT_FEDERATION_REALMIDPSUDONYM);
            _mEncryptedAssertion = encryptedAssertion;

            // Set Private Key from our X509 Cert
            TransportKey = (RSA)privateKey.PrivateKey;

            // Process the encrypted Assertions
            DecryptKey();

            //TO:DO check for Null possibility
            XmlNodeList nodeList = _mEncryptedAssertion.DocumentElement.GetElementsByTagName(SamlConstants.AttributeName.ASSERTION, SamlConstants.XmlNamespace.ASSERTION);
            XmlElement signedAssertion = nodeList[0] as XmlElement;

            if (!SignatureValidator.VerifySignature(intuitPublicKey, signedAssertion))
            {
                throw new Exception("Signatures do not match.");
            }
        }

        #endregion Constructors

        #region Methods
        /// <summary>
        /// Decrypts keys and gets login ticket
        /// </summary>
        internal void DecryptKey()
        {
            if (TransportKey == null)
                throw new ArgumentNullException("The \"TransportKey\" property must contain the asymmetric key to decrypt the assertion.");

            if (_mEncryptedAssertion == null)
                throw new ArgumentNullException("Unable to find the <EncryptedAssertion> element.");


            XmlElement encryptedDataElement = Utility.GetElement(SamlConstants.ENCRYPTEDDATA, SamlConstants.XmlNamespace.XENC, _mEncryptedAssertion.DocumentElement);
            
            EncryptedData encryptedData = new EncryptedData();
            encryptedData.LoadXml(encryptedDataElement);

            SymmetricAlgorithm sessionKey;
            if (encryptedData.EncryptionMethod != null)
            {
                _mSessionKeyAlgorithm = encryptedData.EncryptionMethod.KeyAlgorithm;
                sessionKey = ExtractSessionKey(encryptedData.EncryptionMethod.KeyAlgorithm);
            }
            else
            {
                sessionKey = ExtractSessionKey();
            }

            EncryptedXml encryptedXml = new EncryptedXml();
            byte[] plaintext = encryptedXml.DecryptData(encryptedData, sessionKey);

            _mAssertion = new XmlDocument();
            _mAssertion.PreserveWhitespace = true;
            

            try
            {
                _mAssertion.Load(new StringReader(Encoding.UTF8.GetString(plaintext)));
                _mTicket = _mAssertion.InnerText;
            }
            catch (XmlException e)
            {
                _mAssertion = null;
                throw new Exception("Unable to parse the decrypted assertion.", e);
            }

            XmlElement signatureElement = Utility.GetElement("Signature", SamlConstants.XmlNamespace.DS, _mEncryptedAssertion.DocumentElement);
            signatureElement = Utility.GetElement("SignatureValue", SamlConstants.XmlNamespace.DS, signatureElement);
        }
                
        /// <summary>
        /// Extracts the session key
        /// </summary>
        /// <returns>SymmetricAlgorithm</returns>
        private SymmetricAlgorithm ExtractSessionKey()
        {
            return ExtractSessionKey(string.Empty);
        }

        /// <summary>
        /// Extracts the session key
        /// </summary>
        /// <param name="keyAlgorithm">Key algorithm</param>
        /// <returns>SymmetricAlgorithm</returns>
        private SymmetricAlgorithm ExtractSessionKey(string keyAlgorithm)
        {
            // Check if there are any <EncryptedKey> elements immediately below the EncryptedAssertion element.
            //TO:DO check for null reference
            foreach (XmlNode node in _mEncryptedAssertion.DocumentElement.ChildNodes)
                if (node.LocalName == SamlConstants.ENCRYPTEDDATA && node.NamespaceURI == SamlConstants.XmlNamespace.XENC)
                {
                    return ToSymmetricKey((XmlElement)node, keyAlgorithm);
                }

            XmlElement encryptedData =
            Utility.GetElement(SamlConstants.ENCRYPTEDDATA, SamlConstants.XmlNamespace.XENC, _mEncryptedAssertion.DocumentElement);
            if (encryptedData != null)
            {
                XmlElement encryptedKeyElement =
                    Utility.GetElement(SamlConstants.ENCRYPTEDKEY, SamlConstants.XmlNamespace.XENC, _mEncryptedAssertion.DocumentElement);
                if (encryptedKeyElement != null)
                {
                    return ToSymmetricKey(encryptedKeyElement, keyAlgorithm);
                }
            }

            throw new Exception("Unable to locate assertion decryption key.");
        }

        /// <summary>
        /// Gets the symmetric key from Xml element
        /// </summary>
        /// <param name="encryptedKeyElement">Encrypted Key XMl Element</param>
        /// <param name="keyAlgorithm">Key Algorithm</param>
        /// <returns></returns>
        private SymmetricAlgorithm ToSymmetricKey(XmlElement encryptedKeyElement, string keyAlgorithm)
        {
            EncryptedKey encryptedKey = new EncryptedKey();
            encryptedKey.LoadXml(encryptedKeyElement);

            bool useOaep = Intuit.Ipp.Saml.SamlConstants.USE_OAEP_DEFAULT;
            if (encryptedKey.EncryptionMethod != null)
            {
                useOaep = (encryptedKey.EncryptionMethod.KeyAlgorithm == EncryptedXml.XmlEncRSAOAEPUrl);
            }

            if (encryptedKey.CipherData.CipherValue != null)
            {
                SymmetricAlgorithm key = Utility.GetKeyInstance(keyAlgorithm);

                key.Key = EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, TransportKey, useOaep);
                return key;
            }

            throw new Exception("Unable to decode CipherData of type \"CipherReference\".");
        }

        /// <summary>
        /// Returns the XML representation of the encrypted assertion.
        /// </summary>        
        public XmlDocument GetXml()
        {
            return _mEncryptedAssertion;
        }
        #endregion Methods

        
        #region Properties

        /// <summary>
        /// The transport key is used for securing the symmetric key that has encrypted the assertion.
        /// </summary>
        public RSA TransportKey { get; set; }

        /// <summary>
        /// The targetUrl is the url to your federated application specified in the Advanced Settings of your workplace application.
        /// </summary>
        public String TargetUrl
        {
            get { return _mTargetUrl; }
        }
        /// <summary>
        /// The realmId is the unique identifier of your realm, generally where you quickbooks data is stored in the cloud.
        /// </summary>
        public String RealmId
        {
            get { return _mRealmId; }
        }
        /// <summary>
        /// The ticket is the login ticket for workplace
        /// </summary>
        public String LoginTicket
        {
            get { return _mTicket; }
        }
         /// <summary>
        /// RealmID Pseudonym is GUID
        /// </summary>
        public String RealmIdPseudonym
        {
            get { return _mRealmIdPseudonym; }
        }
        #endregion Properties
    }
}
