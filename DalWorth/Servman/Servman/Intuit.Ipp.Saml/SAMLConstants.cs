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


namespace Intuit.Ipp.Saml
{
    internal class SamlConstants
    {
        #region General SAML Constants
        /// <summary>
        /// Use OEAP Default (false)
        /// </summary>
        internal const bool USE_OAEP_DEFAULT = false;

        /// <summary>
        /// SAML Version
        /// </summary>
        internal const String VERSION = "2.0";

        /// <summary>
        /// The XML namespace of the SAML 2.0 protocol schema
        /// </summary>
        internal const String PROTOCOL = "urn:oasis:names:tc:SAML:2.0:protocol";

        /// <summary>
        /// The XML namespace of the SAML 2.0 metadata schema
        /// </summary>
        internal const String METADATA = "urn:oasis:names:tc:SAML:2.0:metadata";

        /// <summary>
        /// The XML Node name for Encrypted Key
        /// </summary>
        internal const String ENCRYPTEDKEY = "EncryptedKey";
        /// <summary>
        /// The XML Node name for Encrypted Data
        /// </summary>
        internal const String ENCRYPTEDDATA = "EncryptedData";

        /// <summary>
        /// The XML Node name of Signature
        /// </summary>
        internal const string SIGNATURE = "Signature";

        /// <summary>
        /// The default value of the Format property for a NameID element
        /// </summary>
        internal const String DEFAULTNAMEIDFORMAT = "urn:oasis:names:tc:SAML:1.0:nameid-format:unspecified";

        /// <summary>
        /// The mime type that must be used when publishing a metadata document.
        /// </summary>
        internal const String METADATA_MIMETYPE = "application/samlmetadata+xml";

        #endregion General SAML Constants

        #region XmlNamespace Structure
        /// <summary>
        /// XmlNamespace constants
        /// </summary>
        internal struct XmlNamespace
        {
            /// <summary>
            /// Namespace of XMLDSig
            /// </summary>
            internal const string XMLDSIG = "http://www.w3.org/2000/09/xmldsig#";

            /// <summary>
            /// Namespace of XENC
            /// </summary>
            internal const string XENC = "http://www.w3.org/2001/04/xmlenc#";

            /// <summary>
            /// Namespace of SAML Assertion
            /// </summary>
            internal const string ASSERTION = "urn:oasis:names:tc:SAML:2.0:assertion";

            /// <summary>
            /// Namespace of SAML DS
            /// </summary>
            internal const string DS = "http://www.w3.org/2000/09/xmldsig#";


        }
        #endregion XmlNamespace Structure

        #region XmlTags Structure
        /// <summary>
        /// XmlTags constants
        /// </summary>
        internal struct XmlTags
        {
            /// <summary>
            /// ds Tag
            /// </summary>
            internal const string XMLDSIG = "ds";

            /// <summary>
            /// xenc Tag
            /// </summary>
            internal const string XENC = "xenc";

            /// <summary>
            /// saml Tag
            /// </summary>
            internal const string SAML = "saml";

        }
        #endregion XmlTags Structure

        #region AttributeName Structure
        /// <summary>
        /// AttributeName constants
        /// </summary>
        internal struct AttributeName
        {
            /// <summary>
            /// Intuit Federation RealnIDPseudonym
            /// </summary>
            internal const string INTUIT_FEDERATION_REALMIDPSUDONYM = "Intuit.Federation.realmIDPseudonym";

            /// <summary>
            /// Realm ID
            /// </summary>
            internal const string REALM = "realmID";

            /// <summary>
            /// Target URL
            /// </summary>
            internal const string TARGET_URL = "targetUrl";

            /// <summary>
            /// Assertion
            /// </summary>
            internal const string ASSERTION = "Assertion";
        }
        #endregion AttributeName Structure
    }

}

