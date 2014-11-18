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
	/// Provides Method to perform CRUD operations on CreditCardRefund Resource of QuickBooks
	/// </summary>
	public class CreditCardRefundService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all CreditCardRefunds under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CreditCardRefunds</returns>
		public List<CreditCardRefund> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.creditcardrefund;
           
			CreditCardRefunds listOfObjects = (CreditCardRefunds)base.FindAll(context, realmId, resource,typeof(CreditCardRefunds));
			if (listOfObjects != null && listOfObjects.CreditCardRefund != null)
            {
                return new List<CreditCardRefund>(listOfObjects.CreditCardRefund);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"CreditCardRefund not found.");
                return new List<CreditCardRefund>();
            }
		}

		/// <summary>
        /// Returns a CreditCardRefund based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="creditcardrefundIdToFind">CreditCardRefund Id</param>
        /// <returns>CreditCardRefund object with specified id</returns>
		public CreditCardRefund FindById(PlatformSessionContext context, string realmId, IdType creditcardrefundIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			CreditCardRefund creditcardrefundFound = null;
			CreditCardRefunds creditcardrefunds = (CreditCardRefunds)base.FindById(context, realmId, creditcardrefundIdToFind, IDSResource.creditcardrefund, typeof(CreditCardRefunds));
			if (creditcardrefunds.CreditCardRefund == null || creditcardrefunds.CreditCardRefund.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"CreditCardRefund not found.");
				return null;
			}
			creditcardrefundFound = creditcardrefunds.CreditCardRefund[0];
            return creditcardrefundFound;
		}
		
		/// <summary>
        /// Query on CreditCardRefund object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<CreditCardRefund> GetCreditCardRefunds(PlatformSessionContext context, string realmId, CreditCardRefundQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			CreditCardRefunds searchCreditCardRefunds = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.creditcardrefund,realmId);
			
			if(searchQuery != null)
			{
            	searchCreditCardRefunds = (CreditCardRefunds)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCreditCardRefunds = (CreditCardRefunds)base.GetResources(context, operationContext, typeof(CreditCardRefunds));
			}
                    
            if (searchCreditCardRefunds == null || searchCreditCardRefunds.CreditCardRefund == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "CreditCardRefund not found.");
                return null;
            }
            return new List<CreditCardRefund>(searchCreditCardRefunds.CreditCardRefund);
        }

	}
}

