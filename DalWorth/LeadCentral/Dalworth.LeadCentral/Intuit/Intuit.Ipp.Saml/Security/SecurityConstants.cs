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
namespace Intuit.Ipp.Saml.Security
{
    internal class SecurityConstants
    {
        #region Constant declaration
        /// <summary>       
        /// SAMLResponse name
        /// </summary>
        public const string SAMLResponse = "SAMLResponse";
        /// <summary>
        /// SAMLRequest name
        /// </summary>
        public const string SAMLRequest = "SAMLRequest";
        /// <summary>
        /// SigAlg name
        /// </summary>
        public const string SigAlg = "SigAlg";
        /// <summary>
        /// Relaystate name
        /// </summary>
        public const string RelayState = "RelayState";
        /// <summary>
        /// Signature name
        /// </summary>
        public const string Signature = "Signature";
        /// <summary>
        /// Expected Http Post Size in total bytes
        /// </summary>
        public const int EXPECTEDPOSTSIZE = 8000;
        /// <summary>
        /// Expected Encrypted Size in total bytes
        /// </summary>
        public const int EXPECTEDASSERTIONSIZE = 700;

        #endregion
    }
}
