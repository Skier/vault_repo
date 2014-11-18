/*
 * Copyright (c) 2009-2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 *
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Intuit.Platform.Client.Core.IDS
{
    /// <summary>
    /// Provides the Error received from Server
    /// </summary>
    public class IDSException : PlatformClientException
    {
        /// <summary>
        /// Creates IDSException Object
        /// </summary>
        /// <param name="host">PlatfromHost</param>
        public IDSException(IPlatformHost host)
            : base(host)
        {
        }

        /// <summary>
        /// Creates IDSException Object
        /// </summary>
        /// <param name="host">PlatfromHost</param>
        /// <param name="message">Exception Message</param>
        public IDSException(IPlatformHost host, string message)
            : base(host, message)
        {
        }

        /// <summary>
        /// Creates IDSException Object
        /// </summary>
        /// <param name="host">PlatfromHost</param>
        /// <param name="message">Message of Exception</param>
        /// <param name="innerException">Exception responsible for this object creattion</param>
        public IDSException(IPlatformHost host, string message, Exception innerException)
            : base(host, message, innerException)
        {
        }
    }
}