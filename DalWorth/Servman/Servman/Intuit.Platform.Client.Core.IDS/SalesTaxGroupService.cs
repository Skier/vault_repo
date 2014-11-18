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
	/// Provides Method to perform CRUD operations on SalesTaxGroup Resource of QuickBooks
	/// </summary>
	public class SalesTaxGroupService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all SalesTaxGroups under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesTaxGroups</returns>
		public List<SalesTaxGroup> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salestaxgroup;
           
			SalesTaxGroups listOfObjects = (SalesTaxGroups)base.FindAll(context, realmId, resource,typeof(SalesTaxGroups));
			if (listOfObjects != null && listOfObjects.SalesTaxGroup != null)
            {
                return new List<SalesTaxGroup>(listOfObjects.SalesTaxGroup);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesTaxGroup not found.");
                return new List<SalesTaxGroup>();
            }
		}

		/// <summary>
        /// Returns a SalesTaxGroup based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salestaxgroupIdToFind">SalesTaxGroup Id</param>
        /// <returns>SalesTaxGroup object with specified id</returns>
		public SalesTaxGroup FindById(PlatformSessionContext context, string realmId, IdType salestaxgroupIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesTaxGroup salestaxgroupFound = null;
			SalesTaxGroups salestaxgroups = (SalesTaxGroups)base.FindById(context, realmId, salestaxgroupIdToFind, IDSResource.salestaxgroup, typeof(SalesTaxGroups));
			if (salestaxgroups.SalesTaxGroup == null || salestaxgroups.SalesTaxGroup.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"SalesTaxGroup not found.");
				return null;
			}
			salestaxgroupFound = salestaxgroups.SalesTaxGroup[0];
            return salestaxgroupFound;
		}
		
		/// <summary>
        /// Query on SalesTaxGroup object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesTaxGroup> GetSalesTaxGroups(PlatformSessionContext context, string realmId, SalesTaxGroupQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesTaxGroups searchSalesTaxGroups = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salestaxgroup,realmId);
			
			if(searchQuery != null)
			{
            	searchSalesTaxGroups = (SalesTaxGroups)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesTaxGroups = (SalesTaxGroups)base.GetResources(context, operationContext, typeof(SalesTaxGroups));
			}
                    
            if (searchSalesTaxGroups == null || searchSalesTaxGroups.SalesTaxGroup == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesTaxGroup not found.");
                return null;
            }
            return new List<SalesTaxGroup>(searchSalesTaxGroups.SalesTaxGroup);
        }

	}
}

