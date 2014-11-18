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
using System.Web;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using Intuit.Ipp.Saml.Security;

namespace Intuit.Ipp.Saml
{
    /// <summary>
    /// Static class to be accessed by clients
    /// </summary>
    public static class ServiceProvider
    {
        #region Methods
        
        /// <summary>
        /// Gets the SAMLResponse for specific SAML Request from workplace
        /// </summary>
        /// <param name="httpRequest">SAML Request received from workplace</param>
        /// <param name="privateKey">Developer's Private Key used for encryption SAML message</param>
        /// <param name="intuitPublicKey">Intuit's Public Key used for Signature verification</param>
        /// <returns></returns>
        public static SamlResponse GetSamlResponse(HttpRequest httpRequest, X509Certificate2 privateKey, X509Certificate2 intuitPublicKey)
        {
            try
            {
                SecurityValidator.ValidateHttpPostSize(httpRequest);
                XmlDocument encryptedAssertion = Utility.DecodeBase64(httpRequest.Form[0]);
                return new SamlResponse(encryptedAssertion, privateKey, intuitPublicKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Methods
    }
    
}
