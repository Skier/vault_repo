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
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.Xml;
using System.Text;
using CONSTS = Intuit.Ipp.Saml.Security.SecurityConstants;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Intuit.Ipp.Saml.Security
{
    /// <summary>
    /// Parses and validates the signature in the Intuit SAML Message
    /// </summary>
    internal class SignatureValidator
    {


        #region Static Methods

        /// <summary>
        /// Verifies the signature of the SAML using Intuit's public key
        /// </summary>
        /// <param name="intuitCert"></param>
        /// <param name="signedAssertion"></param>
        /// <returns></returns>
        public static bool VerifySignature(X509Certificate2 intuitCert, XmlElement signedAssertion)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode copyNode = doc.ImportNode(signedAssertion, true);
            doc.AppendChild(copyNode);
            //TO:DO check for null reference
            SignedXml signedXml = new SignedXml(doc.DocumentElement);

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(intuitCert);
            keyInfo.AddClause(keyInfoData);
            //TO:DO check for null reference
            XmlNodeList nodeList = doc.DocumentElement.GetElementsByTagName(SamlConstants.SIGNATURE, SamlConstants.XmlNamespace.XMLDSIG);
            signedXml.LoadXml((XmlElement)nodeList[0]);
            signedXml.KeyInfo = keyInfo;
            signedXml.SigningKey = intuitCert.PublicKey.Key;
            // Check the signature
            return signedXml.CheckSignature();
        }

    #endregion Static Methods


    }
}
  
