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

namespace Intuit.Ipp.Saml.Security
{
    /// <summary>
    /// Static class to check security related parameters
    /// </summary>
    internal static class SecurityValidator
    {
        #region Static Methods
        /// <summary>
        /// Check the overall size of the http post if its too large, it probably isnt valid
        /// </summary>
        internal static void ValidateHttpPostSize(HttpRequest httpRequest)
        {
            if (httpRequest.TotalBytes > SecurityConstants.EXPECTEDPOSTSIZE) 
            {
                throw new Exception("Security Exception: http post is larger than expected");
            }
        }

        /// <summary>
        /// Check the expected size of the encrypted assertion if its too large, it probably isnt valid
        /// </summary>
        internal static void ValidateEncryptedAssertionSize(XmlElement xmlAssertion)
        {
            if (xmlAssertion.ToString().Length > SecurityConstants.EXPECTEDASSERTIONSIZE)
            {
                throw new Exception("Security Exception: Encrypted Assertion is larger than expected");
             }
        }
        #endregion Static Methods
    }
}
