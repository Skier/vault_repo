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
	/// Provides Method to perform CRUD operations on SalesRep Resource of QuickBooks
	/// </summary>
	public class SalesRepService : IDSBaseService
	{

		/// <summary>
		/// Adds a SalesRep under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesRep">SalesRep object to Add</param>
		/// <returns>Returns an updated version of the SalesRep with updated IdType and sync token.</returns>
		public SalesRep AddSalesRep(PlatformSessionContext context, string realmId, SalesRep newSalesRep)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesRep = (SalesRep)base.AddResource(context, realmId, newSalesRep, IDSResource.salesrep);
			return newSalesRep;
		}

		/// <summary>
        /// Returns a list of all SalesReps under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesReps</returns>
		public List<SalesRep> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salesrep;
           
			SalesReps listOfObjects = (SalesReps)base.FindAll(context, realmId, resource,typeof(SalesReps));
			if (listOfObjects != null && listOfObjects.SalesRep != null)
            {
                return new List<SalesRep>(listOfObjects.SalesRep);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesRep not found.");
                return new List<SalesRep>();
            }
		}

		/// <summary>
        /// Returns a SalesRep based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salesrepIdToFind">SalesRep Id</param>
        /// <returns>SalesRep object with specified id</returns>
		public SalesRep FindById(PlatformSessionContext context, string realmId, IdType salesrepIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesRep salesrepFound = null;
			SalesReps salesreps = (SalesReps)base.FindById(context, realmId, salesrepIdToFind, IDSResource.salesrep, typeof(SalesReps));
			if (salesreps.SalesRep == null || salesreps.SalesRep.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"SalesRep not found.");
				return null;
			}
			salesrepFound = salesreps.SalesRep[0];
            return salesrepFound;
		}
		
		/// <summary>
        /// Query on SalesRep object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesRep> GetSalesReps(PlatformSessionContext context, string realmId, SalesRepQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesReps searchSalesReps = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salesrep,realmId);
			
			if(searchQuery != null)
			{
            	searchSalesReps = (SalesReps)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesReps = (SalesReps)base.GetResources(context, operationContext, typeof(SalesReps));
			}
                    
            if (searchSalesReps == null || searchSalesReps.SalesRep == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesRep not found.");
                return null;
            }
            return new List<SalesRep>(searchSalesReps.SalesRep);
        }

		/// <summary>
		/// Deletes a SalesRep under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesRep">SalesRep object to Delete</param>
		public void DeleteSalesRep(PlatformSessionContext context, string realmId, SalesRep newSalesRep)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newSalesRep, IDSResource.salesrep);
		}

	}
}

