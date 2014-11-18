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
using System.Xml;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Intuit.Ipp.Saml
{
    /// <summary>
    /// Common static utility class 
    /// </summary>
    internal static class Utility
    {
        #region Static Methods
        /// <summary>
        /// Decodes the http post from Base64 and returns and XML Document
        /// </summary>
        /// <param name="encodedSamlMessage">The Base64 encoded post from the Intuit IDFed Server</param>
        internal static XmlDocument DecodeBase64(String encodedSamlMessage)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecodeByte = Convert.FromBase64String(encodedSamlMessage);
            int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
            String result = new String(decodedChar);
            XmlDocument doc = new XmlDocument {PreserveWhitespace = true};
            doc.LoadXml(result);
            return doc;
        }

        /// <summary>
        /// Creates an instance of a XmlElement based the XmlDocument
        /// </summary>
        internal static XmlElement GetElement(string element, string elementNs, XmlElement doc)
        {
            XmlNodeList list = doc.GetElementsByTagName(element, elementNs);
            if (list.Count == 0)
                return null;

            return (XmlElement)list[0];
        }

        /// <summary>
        /// Creates an instance of a symmetric key, based on the algorithm identifier found in the Xml message.        
        /// </summary>
        internal static SymmetricAlgorithm GetKeyInstance(string algorithm)
        {
            SymmetricAlgorithm result;
            switch (algorithm)
            {
                case EncryptedXml.XmlEncTripleDESUrl:
                    result = TripleDES.Create();
                    break;
                case EncryptedXml.XmlEncAES128Url:
                    result = new RijndaelManaged();
                    result.KeySize = 128;
                    break;
                case EncryptedXml.XmlEncAES192Url:
                    result = new RijndaelManaged();
                    result.KeySize = 192;
                    break;
                case EncryptedXml.XmlEncAES256Url:
                    result = new RijndaelManaged();
                    result.KeySize = 256;
                    break;
                default:
                    result = new RijndaelManaged();
                    result.KeySize = 256;
                    break;
            }
            return result;
        }

        #endregion Methods
    }
}
