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
    public abstract class IDSBaseService : IDSRequestCreator
    {
       
        /// <summary>
        /// Adds a CdmBase resource of the specified type under the specified realm.
        /// Returns an updated version of the CdmBase with updated IdType, sync token and,
        /// if the the resource is an instance of RoleBase, PartyReferenceId.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="newResource">New Resource object</param>
        /// <param name="resource">Resource name</param>
        /// <returns>New created object</returns>
        public CdmBase AddResource(PlatformSessionContext context, String realmId, CdmBase newResource, IDSResource resource)
        {
            Logger.WriteToLog(TraceLevel.Info, "RealmId: " + realmId);

            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(realmId, ref context);

            IDSOperationContext operationContext = new IDSOperationContext(resource, realmId);
          
            object payload = new object();
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;

                    //Standard Add Request processing
                    AddRequest addRequest = new AddRequest();
                    String uuid = Guid.NewGuid().ToString("N");
                    addRequest.RequestId = uuid;
                    addRequest.OfferingId = offeringId.ipp;
                    addRequest.ExternalRealmId = realmId;
                    addRequest.Item = newResource;

                    //Trace
                    Logger.WriteToLog(TraceLevel.Info, "Add request parameters are :");
                    Logger.WriteToLog(TraceLevel.Info, "    RequestId: " + uuid);
                    Logger.WriteToLog(TraceLevel.Info, "    OfferingId: " + offeringId.ipp.ToString());
                    Logger.WriteToLog(TraceLevel.Info, "    ExternalRealmId: " + realmId);

                    payload = DoIDSPost(context, operationContext, addRequest);
                    SuccessResponse success = (SuccessResponse)payload;

                    //Update the id, token and ref so the client doesn't get conflicts
                    CdmObjectRef objectRef = (CdmObjectRef)success.Item;
                    newResource.Id = objectRef.Id;
                    newResource.SyncToken = objectRef.SyncToken;

                    //Trace
                    Logger.WriteToLog(TraceLevel.Info, "Add response data received is :");
                    Logger.WriteToLog(TraceLevel.Info, "    ID: " + objectRef.Id);
                    Logger.WriteToLog(TraceLevel.Info, "    SyncToken: " + objectRef.SyncToken);

                    if (newResource is RoleBase)
                    {
                        ((RoleBase)newResource).PartyReferenceId = ((PartyRoleRef)objectRef).PartyReferenceId;
                    }
                    payload = newResource;
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationXML;
                    payload = (CdmBase)DoIDSPost(context, operationContext, newResource);
                    break;

            }

            return (CdmBase)payload;

        }

        /// <summary>
        /// Updates an existing CdmBase resource of the specified type under the specified realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="objectToUpdate">Object to update</param>
        /// <param name="resource">Resource name</param>
        /// <returns>Updated resource object</returns>
        public CdmBase UpdateResource(PlatformSessionContext context, String realmId, CdmBase objectToUpdate, IDSResource resource)
        {
            Logger.WriteToLog(TraceLevel.Info, "RealmId: " + realmId);

            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(realmId, ref context);

            IDSOperationContext operationContext = new IDSOperationContext(resource, realmId);
            object payload = new object();
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;

                    ModRequest updateRequest = new ModRequest();
                    String uuid = Guid.NewGuid().ToString("N");
                    updateRequest.RequestId = uuid;
                    updateRequest.OfferingId = offeringId.ipp;
                    updateRequest.ExternalRealmId = realmId;
                    updateRequest.Item = objectToUpdate;

                    //Trace 
                    Logger.WriteToLog(TraceLevel.Info, "Update request parameters are :");
                    Logger.WriteToLog(TraceLevel.Info, "    RequestId: " + uuid);
                    Logger.WriteToLog(TraceLevel.Info, "    OfferingId: " + offeringId.ipp.ToString());
                    Logger.WriteToLog(TraceLevel.Info, "    ExternalRealmId: " + realmId);

                    payload = (CdmComplexBase)DoIDSPost(context, operationContext, updateRequest);
                    SuccessResponse success = (SuccessResponse)payload;

                    //Update the id, token and ref so the client doesn't get conflicts
                    CdmObjectRef objectRef = (CdmObjectRef)success.Item;
                    objectToUpdate.Id = objectRef.Id;
                    objectToUpdate.SyncToken = objectRef.SyncToken;

                    //Trace
                    Logger.WriteToLog(TraceLevel.Info, "Update response data received is :");
                    Logger.WriteToLog(TraceLevel.Info, "    ID: " + objectRef.Id);
                    Logger.WriteToLog(TraceLevel.Info, "    SyncToken: " + objectRef.SyncToken);


                    if (objectToUpdate is RoleBase)
                    {
                        ((RoleBase)objectToUpdate).PartyReferenceId = ((PartyRoleRef)objectRef).PartyReferenceId;
                    }
                    payload = objectToUpdate;
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationXML;
                    payload = DoIDSPost(context, operationContext, objectToUpdate);
                    break;
            }
            return (CdmBase)payload;
        }

        /// <summary>
        /// Delete an existing Resource under the specified realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="objectToDelete">Resource to delete</param>
        /// <param name="resource">Resource name</param>
        public void DeleteResource(PlatformSessionContext context, String realmId, CdmBase objectToDelete, IDSResource resource)
        {
            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(realmId, ref context);

            IDSOperationContext operationContext = new IDSOperationContext(resource, realmId);
            object payload = new object();
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;

                    DelRequest deleteRequest = new DelRequest();
                    String uuid = Guid.NewGuid().ToString("N");
                    deleteRequest.RequestId = uuid;
                    deleteRequest.OfferingId = offeringId.ipp;
                    deleteRequest.ExternalRealmId = realmId;
                    deleteRequest.Item = objectToDelete;

                    //Trace 
                    Logger.WriteToLog(TraceLevel.Info, "Delete request parameters are :");
                    Logger.WriteToLog(TraceLevel.Info, "    RequestId: " + uuid);
                    Logger.WriteToLog(TraceLevel.Info, "    OfferingId: " + offeringId.ipp.ToString());
                    Logger.WriteToLog(TraceLevel.Info, "    ExternalRealmId: " + realmId);

                    payload = (CdmComplexBase)DoIDSPost(context, operationContext, deleteRequest);
                    SuccessResponse success = (SuccessResponse)payload;

                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationXML;

                    operationContext.Parameters = new string[] { objectToDelete.Id.Value + "?methodx=delete" };

                    payload = DoIDSPost(context, operationContext, objectToDelete);
                    break;
            }

        }

        /// <summary>
        /// Returns a list of all resources CdmComplexBase of the specified type under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="resource">Resource name</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>List of all Objects</returns>
        public CdmComplexBase FindAll(PlatformSessionContext context, String realmId, IDSResource resource, Type objectType)
        {
            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(realmId, ref context);

            Logger.WriteToLog(TraceLevel.Info, "RealmId: " + realmId);
            CdmComplexBase listOfObjects = null;

            IDSOperationContext operationContext = new IDSOperationContext(resource, realmId);
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;
                    listOfObjects = (CdmComplexBase)DoIDSGet(context, operationContext, objectType);
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationURLEncoded;
                    SearchResults resultObject = (SearchResults)DoIDSPost(context, operationContext, null);
                    listOfObjects = resultObject.CdmCollections;
                    break;
            }

            if (listOfObjects == null)
                Logger.WriteToLog(TraceLevel.Info, "Objects not found");

            return listOfObjects;
        }

        /// <summary>
        /// Returns a list of all resources CdmComplexBase of the specified type under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="operationContext">The operation context.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>List of all Objects</returns>
        public CdmComplexBase GetResourcesForQuery(PlatformSessionContext context, IDSOperationContext operationContext,String queryString)
        {
            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(operationContext.RealmId, ref context);

            Logger.WriteToLog(TraceLevel.Info, "RealmId: " + operationContext.RealmId);
            CdmComplexBase listOfObjects = null;

            operationContext.ContentType = Properties.Settings.Default.ApplicationURLEncoded;
            SearchResults resultObject = (SearchResults)DoIDSPost(context, operationContext, queryString);
            listOfObjects = resultObject.CdmCollections;


            return listOfObjects;
        }

        /// <summary>
        /// Returns a CdmComplexBase based on the Id string and resource type.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="resourceIdToFind">Resource Id</param>
        /// <param name="resource">Resource name</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>object with specified id</returns>
        public object FindById(PlatformSessionContext context, String realmId, IdType resourceIdToFind, IDSResource resource, Type objectType)
        {
            Logger.WriteToLog(TraceLevel.Info, "RealmId: " + realmId);
            
            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(realmId, ref context);

            IDSOperationContext operationContext = new IDSOperationContext(resource, realmId);
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.Parameters = new string[] { resourceIdToFind.Value + "?idDomain=" + resourceIdToFind.idDomain.ToString() };
                    break;
                case IntuitServicesType.QBO:
                    operationContext.EntityId = resourceIdToFind.Value;
                    break;
            }
            return DoIDSGet(context, operationContext, objectType);
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
            //Ignore API_GetIsRealmQBO to get companies list from IDS...
            //It will always connect to QBD....
            if (operationContext.Resource != IDSResource.company)
            {
                //Call API_GetIsRealmQBO to know service type to connect to...
                //Also call QBO User API if realm is of QBO service..
                base.SetServiceTypeProperty(operationContext.RealmId, ref context);
            }

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

        /// <summary>
        /// Returns a CdmComplexBase based on the supplied IDSOperationContext. Issues a Get request.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="operationContext">IDSOperationContext object with realmId and resource property set</param>
        /// <param name="queryDocument">QueryObject</param>
        /// <returns>List of Objects matching Query criteria</returns>
        public CdmComplexBase GetResourcesForQuery(PlatformSessionContext context, IDSOperationContext operationContext, QueryBase queryDocument)
        {
            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(operationContext.RealmId, ref context);

            CdmComplexBase queryResult = null;
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;
                    if (queryDocument.GetType().Name.StartsWith("QBQBO"))
                    {
                        queryDocument = (QueryBase)base.CreateQueryObject(queryDocument);
                    }
                    queryResult = (CdmComplexBase)DoIDSPost(context, operationContext, queryDocument);
                    break;
                case IntuitServicesType.QBO:
                    operationContext.ContentType = Properties.Settings.Default.ApplicationXML;
                    string querystring = CreateQueryString(queryDocument);
                    queryResult = (CdmComplexBase)GetResourcesForQuery(context, operationContext, querystring);
                    break;
            }
            return queryResult;
        }


        /// <summary>
        /// Revert changes from IDS
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="objectToRevert">Object to Revert</param>
        /// <param name="resource">Resource name</param>
        /// <returns>Reverted object</returns>
        public CdmBase RevertResource(PlatformSessionContext context, String realmId, CdmBase objectToRevert, IDSResource resource)
        {
            //Call API_GetIsRealmQBO to know service type to connect to...
            //Also call QBO User API if realm is of QBO service..
            base.SetServiceTypeProperty(realmId, ref context);

            IDSOperationContext operationContext = new IDSOperationContext(resource, realmId);
            object payload = new object();
            switch (context.ServiceType)
            {
                case IntuitServicesType.QBD:
                    operationContext.ContentType = Properties.Settings.Default.TextXML;
                    RevertRequest revertRequest = new RevertRequest();
                    String uuid = Guid.NewGuid().ToString("N");
                    revertRequest.RequestId = uuid;
                    revertRequest.OfferingId = offeringId.ipp;
                    revertRequest.ExternalRealmId = realmId;
                    revertRequest.Item = objectToRevert;

                    payload = (CdmComplexBase)DoIDSPost(context, operationContext, revertRequest);
                    SuccessResponse success = (SuccessResponse)payload;

                    //Update the id, token and ref so the client doesn't get conflicts
                    CdmObjectRef objectRef = (CdmObjectRef)success.Item;
                    objectToRevert.Id = objectRef.Id;
                    objectToRevert.SyncToken = objectRef.SyncToken;
                    if (objectToRevert is RoleBase)
                    {
                        ((RoleBase)objectToRevert).PartyReferenceId = ((PartyRoleRef)objectRef).PartyReferenceId;
                    }
                    payload = objectToRevert;
                    break;
                case IntuitServicesType.QBO:
                    throw new NotSupportedException("This Operation is not supported by Quickbooks Online Services");

            }

            return (CdmBase)payload;
        }
    }
}