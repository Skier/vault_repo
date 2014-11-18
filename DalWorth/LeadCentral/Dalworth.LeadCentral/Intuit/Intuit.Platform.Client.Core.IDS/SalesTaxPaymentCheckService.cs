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
	/// Provides Method to perform CRUD operations on SalesTaxPaymentCheck Resource of QuickBooks
	/// </summary>
	public class SalesTaxPaymentCheckService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all SalesTaxPaymentChecks under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesTaxPaymentChecks</returns>
		public List<SalesTaxPaymentCheck> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salestaxpaymentcheck;
           
			SalesTaxPaymentChecks listOfObjects = (SalesTaxPaymentChecks)base.FindAll(context, realmId, resource,typeof(SalesTaxPaymentChecks));
			if (listOfObjects != null && listOfObjects.SalesTaxPaymentCheck != null)
            {
                return new List<SalesTaxPaymentCheck>(listOfObjects.SalesTaxPaymentCheck);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesTaxPaymentCheck not found.");
                return new List<SalesTaxPaymentCheck>();
            }
		}

		/// <summary>
        /// Returns a SalesTaxPaymentCheck based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salestaxpaymentcheckIdToFind">SalesTaxPaymentCheck Id</param>
        /// <returns>SalesTaxPaymentCheck object with specified id</returns>
		public SalesTaxPaymentCheck FindById(PlatformSessionContext context, string realmId, IdType salestaxpaymentcheckIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesTaxPaymentCheck salestaxpaymentcheckFound = null;
			SalesTaxPaymentChecks salestaxpaymentchecks = (SalesTaxPaymentChecks)base.FindById(context, realmId, salestaxpaymentcheckIdToFind, IDSResource.salestaxpaymentcheck, typeof(SalesTaxPaymentChecks));
			if (salestaxpaymentchecks.SalesTaxPaymentCheck == null || salestaxpaymentchecks.SalesTaxPaymentCheck.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"SalesTaxPaymentCheck not found.");
				return null;
			}
			salestaxpaymentcheckFound = salestaxpaymentchecks.SalesTaxPaymentCheck[0];
            return salestaxpaymentcheckFound;
		}
		
		/// <summary>
        /// Query on SalesTaxPaymentCheck object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesTaxPaymentCheck> GetSalesTaxPaymentChecks(PlatformSessionContext context, string realmId, SalesTaxPaymentCheckQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesTaxPaymentChecks searchSalesTaxPaymentChecks = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salestaxpaymentcheck,realmId);
			
			if(searchQuery != null)
			{
            	searchSalesTaxPaymentChecks = (SalesTaxPaymentChecks)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesTaxPaymentChecks = (SalesTaxPaymentChecks)base.GetResources(context, operationContext, typeof(SalesTaxPaymentChecks));
			}
                    
            if (searchSalesTaxPaymentChecks == null || searchSalesTaxPaymentChecks.SalesTaxPaymentCheck == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesTaxPaymentCheck not found.");
                return null;
            }
            return new List<SalesTaxPaymentCheck>(searchSalesTaxPaymentChecks.SalesTaxPaymentCheck);
        }

	}
}

