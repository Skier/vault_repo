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
using System.IO;
using System.Xml.Serialization;
using Intuit.Sb.Cdm;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

using System.Reflection;

namespace Intuit.Platform.Client.Core.IDS
{
    /// <summary>
    /// This class is intended to encapsulate common functionality for the concrete Domain Services.
    /// </summary>
    public abstract class IDSReportBaseService : IDSRequestCreator
    {

        /// <summary>
        /// Returns a CdmComplexBase based on the supplied IDSOperationContext. Issues a Get request.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="operationContext">IDSOperationContext object with realmId and resource property set</param>
        /// <param name="queryDocument">QueryObject</param>
        /// <returns>List of Objects matching Query criteria</returns>
        public CdmComplexBase GetResourcesForQuery(PlatformSessionContext context, IDSOperationContext operationContext, AdvancedReportQuery queryDocument)
        {
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationXML;
                    break;
            }
            return (CdmComplexBase)DoIDSPost(context, operationContext, queryDocument);
        }

        /// <summary>
        /// Returns a CdmComplexBase based on the supplied IDSOperationContext. Issues a Get request.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="operationContext">IDSOperationContext object with realmId and resource property set</param>
        /// <param name="queryDocument">QueryObject</param>
        /// <returns>List of Objects matching Query criteria</returns>
        public CdmComplexBase GetResourcesForQuery(PlatformSessionContext context, IDSOperationContext operationContext, ReportQueryBase queryDocument)
        {
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationXML;
                    break;
            }
            return (CdmComplexBase)DoIDSPost(context, operationContext, queryDocument);
        }
        /// <summary>
        /// Returns a list of all resources CdmComplexBase of the specified type under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="resource">Resource name</param>
        /// <param name="queryString">The query string.</param>
        /// <param name="queryObjectName">Name of the query object.</param>
        /// <returns>List of all Objects</returns>
        public CdmComplexBase GetResourcesForQuery(PlatformSessionContext context, String realmId, IDSResource resource, String queryString, string queryObjectName)
        {
            Logger.WriteToLog(TraceLevel.Info, "RealmId: " + realmId);
            CdmComplexBase listOfObjects = null;
            IDSOperationContext operationContext = new IDSOperationContext(resource, realmId);
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:

                    Type queryType = null;
                    object q = null;// CreateQueryObject(queryString, queryObjectName, ref queryType);
                    IDSOperationContext operationcontext = new IDSOperationContext(resource, realmId);
                    if (queryType.BaseType.Name == "ReportQueryBase")
                    {
                        return GetResourcesForQuery(context, operationcontext, (ReportQueryBase)q);
                    }
                    else if (queryType.Name == "AdvancedReportQuery")
                    {
                        return GetResourcesForQuery(context, operationcontext, (AdvancedReportQuery)q);
                    }
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationURLEncoded;
                    SearchResults resultObject = (SearchResults)DoIDSPost(context, operationContext, queryString);
                    listOfObjects = resultObject.CdmCollections;
                    break;
            }
            return listOfObjects;
        }

        /// <summary>
        /// Returns a CdmComplexBase based on the supplied IDSOperationContext. Issues a Get request.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="operationContext">IDSOperationContext object with realmId and resource property set</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns>List of Objects matching Query criteria</returns>
        public CdmComplexBase GetResources(PlatformSessionContext context, IDSOperationContext operationContext, Type resourceType)
        {
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationXML;
                    break;
            }
            return (CdmComplexBase)DoIDSGet(context, operationContext, resourceType);
        }
    }

}
