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
	/// Provides Method to perform CRUD operations on CustomerType Resource of QuickBooks
	/// </summary>
	public class CustomerTypeService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all CustomerTypes under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CustomerTypes</returns>
		public List<CustomerType> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.customertype;
           
			CustomerTypes listOfObjects = (CustomerTypes)base.FindAll(context, realmId, resource,typeof(CustomerTypes));
			if (listOfObjects != null && listOfObjects.CustomerType != null)
            {
                return new List<CustomerType>(listOfObjects.CustomerType);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"CustomerType not found.");
                return new List<CustomerType>();
            }
		}

		/// <summary>
        /// Returns a CustomerType based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="customertypeIdToFind">CustomerType Id</param>
        /// <returns>CustomerType object with specified id</returns>
		public CustomerType FindById(PlatformSessionContext context, string realmId, IdType customertypeIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			CustomerType customertypeFound = null;
			CustomerTypes customertypes = (CustomerTypes)base.FindById(context, realmId, customertypeIdToFind, IDSResource.customertype, typeof(CustomerTypes));
			if (customertypes.CustomerType == null || customertypes.CustomerType.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"CustomerType not found.");
				return null;
			}
			customertypeFound = customertypes.CustomerType[0];
            return customertypeFound;
		}
		
		/// <summary>
        /// Query on CustomerType object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<CustomerType> GetCustomerTypes(PlatformSessionContext context, string realmId, CustomerTypeQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			CustomerTypes searchCustomerTypes = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.customertype,realmId);
			
			if(searchQuery != null)
			{
            	searchCustomerTypes = (CustomerTypes)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCustomerTypes = (CustomerTypes)base.GetResources(context, operationContext, typeof(CustomerTypes));
			}
                    
            if (searchCustomerTypes == null || searchCustomerTypes.CustomerType == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "CustomerType not found.");
                return null;
            }
            return new List<CustomerType>(searchCustomerTypes.CustomerType);
        }

	}
}

