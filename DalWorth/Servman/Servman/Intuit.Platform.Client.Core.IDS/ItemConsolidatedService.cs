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
	/// Provides Method to perform CRUD operations on ItemConsolidated Resource of QuickBooks
	/// </summary>
	public class ItemConsolidatedService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all ItemsConsolidated under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all ItemsConsolidated</returns>
		public List<ItemConsolidated> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.itemconsolidated;
           
			ItemsConsolidated listOfObjects = (ItemsConsolidated)base.FindAll(context, realmId, resource,typeof(ItemsConsolidated));
			if (listOfObjects != null && listOfObjects.ItemConsolidated != null)
            {
                return new List<ItemConsolidated>(listOfObjects.ItemConsolidated);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"ItemConsolidated not found.");
                return new List<ItemConsolidated>();
            }
		}

		/// <summary>
        /// Returns a ItemConsolidated based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="itemconsolidatedIdToFind">ItemConsolidated Id</param>
        /// <returns>ItemConsolidated object with specified id</returns>
		public ItemConsolidated FindById(PlatformSessionContext context, string realmId, IdType itemconsolidatedIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			ItemConsolidated itemconsolidatedFound = null;
			ItemsConsolidated itemsconsolidated = (ItemsConsolidated)base.FindById(context, realmId, itemconsolidatedIdToFind, IDSResource.itemconsolidated, typeof(ItemsConsolidated));
			if (itemsconsolidated.ItemConsolidated == null || itemsconsolidated.ItemConsolidated.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"ItemConsolidated not found.");
				return null;
			}
			itemconsolidatedFound = itemsconsolidated.ItemConsolidated[0];
            return itemconsolidatedFound;
		}
		
		/// <summary>
        /// Query on ItemConsolidated object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<ItemConsolidated> GetItemsConsolidated(PlatformSessionContext context, string realmId, ItemConsolidatedQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			ItemsConsolidated searchItemsConsolidated = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.itemconsolidated,realmId);
			
			if(searchQuery != null)
			{
            	searchItemsConsolidated = (ItemsConsolidated)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchItemsConsolidated = (ItemsConsolidated)base.GetResources(context, operationContext, typeof(ItemsConsolidated));
			}
                    
            if (searchItemsConsolidated == null || searchItemsConsolidated.ItemConsolidated == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "ItemConsolidated not found.");
                return null;
            }
            return new List<ItemConsolidated>(searchItemsConsolidated.ItemConsolidated);
        }

	}
}

