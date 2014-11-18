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
	/// Provides Method to perform CRUD operations on OtherName Resource of QuickBooks
	/// </summary>
	public class OtherNameService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all OtherNames under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all OtherNames</returns>
		public List<OtherName> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.othername;
           
			OtherNames listOfObjects = (OtherNames)base.FindAll(context, realmId, resource,typeof(OtherNames));
			if (listOfObjects != null && listOfObjects.OtherName != null)
            {
                return new List<OtherName>(listOfObjects.OtherName);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"OtherName not found.");
                return new List<OtherName>();
            }
		}

		/// <summary>
        /// Returns a OtherName based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="othernameIdToFind">OtherName Id</param>
        /// <returns>OtherName object with specified id</returns>
		public OtherName FindById(PlatformSessionContext context, string realmId, IdType othernameIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			OtherName othernameFound = null;
			OtherNames othernames = (OtherNames)base.FindById(context, realmId, othernameIdToFind, IDSResource.othername, typeof(OtherNames));
			if (othernames.OtherName == null || othernames.OtherName.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"OtherName not found.");
				return null;
			}
			othernameFound = othernames.OtherName[0];
            return othernameFound;
		}
		
		/// <summary>
        /// Query on OtherName object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<OtherName> GetOtherNames(PlatformSessionContext context, string realmId, OtherNameQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			OtherNames searchOtherNames = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.othername,realmId);
			
			if(searchQuery != null)
			{
            	searchOtherNames = (OtherNames)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchOtherNames = (OtherNames)base.GetResources(context, operationContext, typeof(OtherNames));
			}
                    
            if (searchOtherNames == null || searchOtherNames.OtherName == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "OtherName not found.");
                return null;
            }
            return new List<OtherName>(searchOtherNames.OtherName);
        }

	}
}

