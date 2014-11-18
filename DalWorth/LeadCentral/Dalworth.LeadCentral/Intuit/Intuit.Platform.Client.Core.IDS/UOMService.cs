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
	/// Provides Method to perform CRUD operations on UOM Resource of QuickBooks
	/// </summary>
	public class UOMService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all UOMs under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all UOMs</returns>
		public List<UOM> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.uom;
           
			UOMs listOfObjects = (UOMs)base.FindAll(context, realmId, resource,typeof(UOMs));
			if (listOfObjects != null && listOfObjects.UOM != null)
            {
                return new List<UOM>(listOfObjects.UOM);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"UOM not found.");
                return new List<UOM>();
            }
		}

		/// <summary>
        /// Returns a UOM based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="uomIdToFind">UOM Id</param>
        /// <returns>UOM object with specified id</returns>
		public UOM FindById(PlatformSessionContext context, string realmId, IdType uomIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			UOM uomFound = null;
			UOMs uoms = (UOMs)base.FindById(context, realmId, uomIdToFind, IDSResource.uom, typeof(UOMs));
			if (uoms.UOM == null || uoms.UOM.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"UOM not found.");
				return null;
			}
			uomFound = uoms.UOM[0];
            return uomFound;
		}
		
		/// <summary>
        /// Query on UOM object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<UOM> GetUOMs(PlatformSessionContext context, string realmId, UOMQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			UOMs searchUOMs = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.uom,realmId);
			
			if(searchQuery != null)
			{
            	searchUOMs = (UOMs)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchUOMs = (UOMs)base.GetResources(context, operationContext, typeof(UOMs));
			}
                    
            if (searchUOMs == null || searchUOMs.UOM == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "UOM not found.");
                return null;
            }
            return new List<UOM>(searchUOMs.UOM);
        }

	}
}

