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
using System.Text;
using System.Xml;

namespace Intuit.Ipp.Saml
{
    /// <summary>
    /// Processes SAML Attributes from XML
    /// </summary>
    internal class SamlAttributes
    {
        #region Member Variables
        /// <summary>
        /// XmlNamespaceManager
        /// </summary>
        private readonly XmlNamespaceManager _mManager;    
        /// <summary>
        /// XmlDocument
        /// </summary>
        private readonly XmlDocument _mSamlXml;
        #endregion Member Variables

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="samlXmlString">SAML XML string</param>
        internal SamlAttributes(string samlXmlString)
        {
            _mSamlXml = new XmlDocument();
            _mSamlXml.LoadXml(samlXmlString);
            //TO:DO check for Null Exception
            _mManager = new XmlNamespaceManager(_mSamlXml.DocumentElement.OwnerDocument.NameTable);
            _mManager.AddNamespace(SamlConstants.XmlTags.SAML, SamlConstants.XmlNamespace.ASSERTION);
            _mManager.AddNamespace(SamlConstants.XmlTags.XENC, SamlConstants.XmlNamespace.XENC);
            _mManager.AddNamespace(SamlConstants.XmlTags.XMLDSIG, SamlConstants.XmlNamespace.XMLDSIG);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Return the SAML attribute value
        /// </summary>
        /// <param name="attributeName">The Encrypted XML Assertion as an XML Document</param>
        internal String GetAttribute(string attributeName)
        {
            //return getAttributeFromXML(samlXml, AttributeName);
            try
            {
                String attributeValue = "";
                StringBuilder xpath = new StringBuilder("//saml:Attribute[@Name='");
                xpath.Append(attributeName);
                xpath.Append("']/saml:AttributeValue");


                XmlNode selectedNode = _mSamlXml.SelectSingleNode(xpath.ToString(), _mManager);
                if (selectedNode != null)
                {
                    attributeValue = selectedNode.InnerText.ToString();
                }
                else
                {
                    throw new Exception("No Attribute:" + attributeName + " in SAML Message");
                }

                return attributeValue;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Methods

    }
}
