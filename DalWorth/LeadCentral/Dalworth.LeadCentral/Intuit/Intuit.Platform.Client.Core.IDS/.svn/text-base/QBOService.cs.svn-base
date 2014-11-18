
/*
 * Copyright (c) 2010-2011 Intuit, Inc.
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
using Intuit.Sb.Cdm;
using System.Diagnostics;
using Intuit.Platform.Client.Core;
using Intuit.Platform.Client.Core.IDS;

namespace Intuit.Platform.Client.Core.IDS
{
    /// <summary>
    /// Class provides Authentication service API to user along with Switch company.
    /// </summary>
    public class QBOService
    {


        /// <summary>
        /// Get the User for the given companyId
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">The realm id.</param>
        /// <returns>Returns QboUser object</returns>
        public QboUser User(ref PlatformSessionContext context, string realmId)
        {
            //call user API
            IDSOperationContext operationContext = new IDSOperationContext(IDSResource.user, realmId);
            operationContext.ContentType = Properties.Settings.Default.ApplicationXML;
            IDSRestClient restClient = new IDSRestClient(context.ServiceType, context);
            string payload = restClient.Send(context, Verb.GET, operationContext, string.Empty);
            ObjectSerializer deserialize = new ObjectSerializer();
            QboUser user = (QboUser)deserialize.DeserializeObject(payload, typeof(QboUser));
            context.QboBaseURI = user.CurrentCompany.BaseURI;
            return user;            
        }
    }
}
