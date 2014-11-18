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
	/// Provides Method to perform CRUD operations on VendorType Resource of QuickBooks
	/// </summary>
	public class VendorTypeService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all VendorTypes under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all VendorTypes</returns>
		public List<VendorType> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.vendortype;
           
			VendorTypes listOfObjects = (VendorTypes)base.FindAll(context, realmId, resource,typeof(VendorTypes));
			if (listOfObjects != null && listOfObjects.VendorType != null)
            {
                return new List<VendorType>(listOfObjects.VendorType);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"VendorType not found.");
                return new List<VendorType>();
            }
		}

		/// <summary>
        /// Returns a VendorType based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="vendortypeIdToFind">VendorType Id</param>
        /// <returns>VendorType object with specified id</returns>
		public VendorType FindById(PlatformSessionContext context, string realmId, IdType vendortypeIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			VendorType vendortypeFound = null;
			VendorTypes vendortypes = (VendorTypes)base.FindById(context, realmId, vendortypeIdToFind, IDSResource.vendortype, typeof(VendorTypes));
			if (vendortypes.VendorType == null || vendortypes.VendorType.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"VendorType not found.");
				return null;
			}
			vendortypeFound = vendortypes.VendorType[0];
            return vendortypeFound;
		}
		
		/// <summary>
        /// Query on VendorType object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<VendorType> GetVendorTypes(PlatformSessionContext context, string realmId, VendorTypeQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			VendorTypes searchVendorTypes = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.vendortype,realmId);
			
			if(searchQuery != null)
			{
            	searchVendorTypes = (VendorTypes)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchVendorTypes = (VendorTypes)base.GetResources(context, operationContext, typeof(VendorTypes));
			}
                    
            if (searchVendorTypes == null || searchVendorTypes.VendorType == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "VendorType not found.");
                return null;
            }
            return new List<VendorType>(searchVendorTypes.VendorType);
        }

	}
}

