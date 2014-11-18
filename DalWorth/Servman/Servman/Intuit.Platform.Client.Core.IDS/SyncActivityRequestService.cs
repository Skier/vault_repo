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
using System.Diagnostics;
using Intuit.Platform.Client.Core.IDS;

namespace Intuit.Sb.Cdm.QB
{
	/// <summary>
	/// Provides Method to perform CRUD operations on SyncActivityRequest Resource of QuickBooks
	/// </summary>
	public class SyncActivityRequestService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all SyncActivityResponses under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SyncActivityResponses</returns>
		public List<SyncActivityResponse> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.syncactivityrequest;
           
			SyncActivityResponses listOfObjects = (SyncActivityResponses)base.FindAll(context, realmId, resource,typeof(SyncActivityResponses));
			if (listOfObjects != null && listOfObjects.SyncActivityResponse != null)
            {
                return new List<SyncActivityResponse>(listOfObjects.SyncActivityResponse);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SyncActivityResponse not found.");
                return new List<SyncActivityResponse>();
            }
		}

		/// <summary>
        /// Returns a SyncActivityRequest based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="syncactivityrequestIdToFind">SyncActivityRequest Id</param>
        /// <returns>SyncActivityRequest object with specified id</returns>
		public SyncActivityResponse FindById(PlatformSessionContext context, string realmId, IdType syncactivityrequestIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SyncActivityResponse syncactivityrequestFound = null;
			SyncActivityResponses syncactivityresponses = (SyncActivityResponses)base.FindById(context, realmId, syncactivityrequestIdToFind, IDSResource.syncactivityrequest, typeof(SyncActivityResponses));
			if (syncactivityresponses.SyncActivityResponse == null || syncactivityresponses.SyncActivityResponse.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"SyncActivityResponse not found.");
				return null;
			}
			syncactivityrequestFound = syncactivityresponses.SyncActivityResponse[0];
            return syncactivityrequestFound;
		}

	}
}

