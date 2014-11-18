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
	/// Provides Method to perform CRUD operations on ShipMethod Resource of QuickBooks
	/// </summary>
	public class ShipMethodService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all ShipMethods under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all ShipMethods</returns>
		public List<ShipMethod> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.shipmethod;
           
			ShipMethods listOfObjects = (ShipMethods)base.FindAll(context, realmId, resource,typeof(ShipMethods));
			if (listOfObjects != null && listOfObjects.ShipMethod != null)
            {
                return new List<ShipMethod>(listOfObjects.ShipMethod);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"ShipMethod not found.");
                return new List<ShipMethod>();
            }
		}

		/// <summary>
        /// Returns a ShipMethod based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="shipmethodIdToFind">ShipMethod Id</param>
        /// <returns>ShipMethod object with specified id</returns>
		public ShipMethod FindById(PlatformSessionContext context, string realmId, IdType shipmethodIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			ShipMethod shipmethodFound = null;
			ShipMethods shipmethods = (ShipMethods)base.FindById(context, realmId, shipmethodIdToFind, IDSResource.shipmethod, typeof(ShipMethods));
			if (shipmethods.ShipMethod == null || shipmethods.ShipMethod.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"ShipMethod not found.");
				return null;
			}
			shipmethodFound = shipmethods.ShipMethod[0];
            return shipmethodFound;
		}
		
		/// <summary>
        /// Query on ShipMethod object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<ShipMethod> GetShipMethods(PlatformSessionContext context, string realmId, ShipMethodQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			ShipMethods searchShipMethods = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.shipmethod,realmId);
			
			if(searchQuery != null)
			{
            	searchShipMethods = (ShipMethods)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchShipMethods = (ShipMethods)base.GetResources(context, operationContext, typeof(ShipMethods));
			}
                    
            if (searchShipMethods == null || searchShipMethods.ShipMethod == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "ShipMethod not found.");
                return null;
            }
            return new List<ShipMethod>(searchShipMethods.ShipMethod);
        }

	}
}

