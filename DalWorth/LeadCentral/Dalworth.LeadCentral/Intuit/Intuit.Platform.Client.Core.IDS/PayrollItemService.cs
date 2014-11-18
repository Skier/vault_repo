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
	/// Provides Method to perform CRUD operations on PayrollItem Resource of QuickBooks
	/// </summary>
	public class PayrollItemService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all PayrollItems under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all PayrollItems</returns>
		public List<PayrollItem> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.payrollitem;
           
			PayrollItems listOfObjects = (PayrollItems)base.FindAll(context, realmId, resource,typeof(PayrollItems));
			if (listOfObjects != null && listOfObjects.PayrollItem != null)
            {
                return new List<PayrollItem>(listOfObjects.PayrollItem);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"PayrollItem not found.");
                return new List<PayrollItem>();
            }
		}

		/// <summary>
        /// Returns a PayrollItem based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="payrollitemIdToFind">PayrollItem Id</param>
        /// <returns>PayrollItem object with specified id</returns>
		public PayrollItem FindById(PlatformSessionContext context, string realmId, IdType payrollitemIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			PayrollItem payrollitemFound = null;
			PayrollItems payrollitems = (PayrollItems)base.FindById(context, realmId, payrollitemIdToFind, IDSResource.payrollitem, typeof(PayrollItems));
			if (payrollitems.PayrollItem == null || payrollitems.PayrollItem.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"PayrollItem not found.");
				return null;
			}
			payrollitemFound = payrollitems.PayrollItem[0];
            return payrollitemFound;
		}
		
		/// <summary>
        /// Query on PayrollItem object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<PayrollItem> GetPayrollItems(PlatformSessionContext context, string realmId, PayrollItemQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			PayrollItems searchPayrollItems = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.payrollitem,realmId);
			
			if(searchQuery != null)
			{
            	searchPayrollItems = (PayrollItems)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchPayrollItems = (PayrollItems)base.GetResources(context, operationContext, typeof(PayrollItems));
			}
                    
            if (searchPayrollItems == null || searchPayrollItems.PayrollItem == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "PayrollItem not found.");
                return null;
            }
            return new List<PayrollItem>(searchPayrollItems.PayrollItem);
        }

	}
}

