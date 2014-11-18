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
	/// Provides Method to perform CRUD operations on BOMComponent Resource of QuickBooks
	/// </summary>
	public class BOMComponentService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all BOMComponents under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all BOMComponents</returns>
		public List<BOMComponent> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.bomcomponent;
           
			BOMComponents listOfObjects = (BOMComponents)base.FindAll(context, realmId, resource,typeof(BOMComponents));
			if (listOfObjects != null && listOfObjects.BOMComponent != null)
            {
                return new List<BOMComponent>(listOfObjects.BOMComponent);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"BOMComponent not found.");
                return new List<BOMComponent>();
            }
		}

		/// <summary>
        /// Returns a BOMComponent based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="bomcomponentIdToFind">BOMComponent Id</param>
        /// <returns>BOMComponent object with specified id</returns>
		public BOMComponent FindById(PlatformSessionContext context, string realmId, IdType bomcomponentIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			BOMComponent bomcomponentFound = null;
			BOMComponents bomcomponents = (BOMComponents)base.FindById(context, realmId, bomcomponentIdToFind, IDSResource.bomcomponent, typeof(BOMComponents));
			if (bomcomponents.BOMComponent == null || bomcomponents.BOMComponent.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"BOMComponent not found.");
				return null;
			}
			bomcomponentFound = bomcomponents.BOMComponent[0];
            return bomcomponentFound;
		}
		
		/// <summary>
        /// Query on BOMComponent object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<BOMComponent> GetBOMComponents(PlatformSessionContext context, string realmId, BOMComponentQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			BOMComponents searchBOMComponents = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.bomcomponent,realmId);
			
			if(searchQuery != null)
			{
            	searchBOMComponents = (BOMComponents)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchBOMComponents = (BOMComponents)base.GetResources(context, operationContext, typeof(BOMComponents));
			}
                    
            if (searchBOMComponents == null || searchBOMComponents.BOMComponent == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "BOMComponent not found.");
                return null;
            }
            return new List<BOMComponent>(searchBOMComponents.BOMComponent);
        }

	}
}

