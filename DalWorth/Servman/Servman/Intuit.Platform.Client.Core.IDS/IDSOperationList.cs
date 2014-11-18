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
using Intuit.Sb.Cdm;
using Intuit.Platform.Client.Core;
using Intuit.Platform.Client.Core.IDS;

namespace Intuit.Platform.Client.Core.IDS
{
    /// <summary>
    /// A list of Operations supported by IDS.  Used by the REST infrastructure
    /// to construct the relevant calls.
    /// </summary>
    public enum IDSOperationList
    {
        /// <summary>
        /// Create operation on resource
        /// </summary>
        Add,
        /// <summary>
        /// Read operation on resource
        /// </summary>
        FindById,
        /// <summary>
        /// Read operation on resource
        /// </summary>
        FindAll,
        /// <summary>
        /// Read operation on resource
        /// </summary>
        Get,
        /// <summary>
        /// Update operation on resource
        /// </summary>
        Update,
        /// <summary>
        /// Delete operation on resource
        /// </summary>
        Delete,
        /// <summary>
        /// Revert operation on resource
        /// </summary>
        Revert

    }
}
