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
	/// Provides Method to perform CRUD operations on FixedAsset Resource of QuickBooks
	/// </summary>
	public class FixedAssetService : IDSBaseService
	{

		/// <summary>
		/// Adds a FixedAsset under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newFixedAsset">FixedAsset object to Add</param>
		/// <returns>Returns an updated version of the FixedAsset with updated IdType and sync token.</returns>
		public FixedAsset AddFixedAsset(PlatformSessionContext context, string realmId, FixedAsset newFixedAsset)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newFixedAsset = (FixedAsset)base.AddResource(context, realmId, newFixedAsset, IDSResource.fixedasset);
			return newFixedAsset;
		}

		/// <summary>
        /// Returns a list of all FixedAssets under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all FixedAssets</returns>
		public List<FixedAsset> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.fixedasset;
           
			FixedAssets listOfObjects = (FixedAssets)base.FindAll(context, realmId, resource,typeof(FixedAssets));
			if (listOfObjects != null && listOfObjects.FixedAsset != null)
            {
                return new List<FixedAsset>(listOfObjects.FixedAsset);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"FixedAsset not found.");
                return new List<FixedAsset>();
            }
		}

		/// <summary>
        /// Returns a FixedAsset based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="fixedassetIdToFind">FixedAsset Id</param>
        /// <returns>FixedAsset object with specified id</returns>
		public FixedAsset FindById(PlatformSessionContext context, string realmId, IdType fixedassetIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			FixedAsset fixedassetFound = null;
			FixedAssets fixedassets = (FixedAssets)base.FindById(context, realmId, fixedassetIdToFind, IDSResource.fixedasset, typeof(FixedAssets));
			if (fixedassets.FixedAsset == null || fixedassets.FixedAsset.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"FixedAsset not found.");
				return null;
			}
			fixedassetFound = fixedassets.FixedAsset[0];
            return fixedassetFound;
		}
		
		/// <summary>
        /// Query on FixedAsset object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<FixedAsset> GetFixedAssets(PlatformSessionContext context, string realmId, FixedAssetQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			FixedAssets searchFixedAssets = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.fixedasset,realmId);
			
			if(searchQuery != null)
			{
            	searchFixedAssets = (FixedAssets)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchFixedAssets = (FixedAssets)base.GetResources(context, operationContext, typeof(FixedAssets));
			}
                    
            if (searchFixedAssets == null || searchFixedAssets.FixedAsset == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "FixedAsset not found.");
                return null;
            }
            return new List<FixedAsset>(searchFixedAssets.FixedAsset);
        }

		/// <summary>
		/// Deletes a FixedAsset under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newFixedAsset">FixedAsset object to Delete</param>
		public void DeleteFixedAsset(PlatformSessionContext context, string realmId, FixedAsset newFixedAsset)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newFixedAsset, IDSResource.fixedasset);
		}

	}
}

