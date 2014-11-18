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
	/// Provides Method to perform CRUD operations on CustomerMsg Resource of QuickBooks
	/// </summary>
	public class CustomerMsgService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all CustomerMsgs under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CustomerMsgs</returns>
		public List<CustomerMsg> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.customermsg;
           
			CustomerMsgs listOfObjects = (CustomerMsgs)base.FindAll(context, realmId, resource,typeof(CustomerMsgs));
			if (listOfObjects != null && listOfObjects.CustomerMsg != null)
            {
                return new List<CustomerMsg>(listOfObjects.CustomerMsg);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"CustomerMsg not found.");
                return new List<CustomerMsg>();
            }
		}

		/// <summary>
        /// Returns a CustomerMsg based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="customermsgIdToFind">CustomerMsg Id</param>
        /// <returns>CustomerMsg object with specified id</returns>
		public CustomerMsg FindById(PlatformSessionContext context, string realmId, IdType customermsgIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			CustomerMsg customermsgFound = null;
			CustomerMsgs customermsgs = (CustomerMsgs)base.FindById(context, realmId, customermsgIdToFind, IDSResource.customermsg, typeof(CustomerMsgs));
			if (customermsgs.CustomerMsg == null || customermsgs.CustomerMsg.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"CustomerMsg not found.");
				return null;
			}
			customermsgFound = customermsgs.CustomerMsg[0];
            return customermsgFound;
		}
		
		/// <summary>
        /// Query on CustomerMsg object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<CustomerMsg> GetCustomerMsgs(PlatformSessionContext context, string realmId, CustomerMsgQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			CustomerMsgs searchCustomerMsgs = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.customermsg,realmId);
			
			if(searchQuery != null)
			{
            	searchCustomerMsgs = (CustomerMsgs)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCustomerMsgs = (CustomerMsgs)base.GetResources(context, operationContext, typeof(CustomerMsgs));
			}
                    
            if (searchCustomerMsgs == null || searchCustomerMsgs.CustomerMsg == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "CustomerMsg not found.");
                return null;
            }
            return new List<CustomerMsg>(searchCustomerMsgs.CustomerMsg);
        }

	}
}

