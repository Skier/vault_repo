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
	/// Provides Method to perform CRUD operations on BillPaymentCreditCard Resource of QuickBooks
	/// </summary>
	public class BillPaymentCreditCardService : IDSBaseService
	{

		/// <summary>
		/// Adds a BillPaymentCreditCard under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBillPaymentCreditCard">BillPaymentCreditCard object to Add</param>
		/// <returns>Returns an updated version of the BillPaymentCreditCard with updated IdType and sync token.</returns>
		public BillPaymentCreditCard AddBillPaymentCreditCard(PlatformSessionContext context, string realmId, BillPaymentCreditCard newBillPaymentCreditCard)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newBillPaymentCreditCard = (BillPaymentCreditCard)base.AddResource(context, realmId, newBillPaymentCreditCard, IDSResource.billpaymentcreditcard);
			return newBillPaymentCreditCard;
		}

		/// <summary>
        /// Returns a list of all BillPaymentCreditCards under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all BillPaymentCreditCards</returns>
		public List<BillPaymentCreditCard> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.billpaymentcreditcard;
           
			BillPaymentCreditCards listOfObjects = (BillPaymentCreditCards)base.FindAll(context, realmId, resource,typeof(BillPaymentCreditCards));
			if (listOfObjects != null && listOfObjects.BillPaymentCreditCard != null)
            {
                return new List<BillPaymentCreditCard>(listOfObjects.BillPaymentCreditCard);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"BillPaymentCreditCard not found.");
                return new List<BillPaymentCreditCard>();
            }
		}

		/// <summary>
        /// Returns a BillPaymentCreditCard based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="billpaymentcreditcardIdToFind">BillPaymentCreditCard Id</param>
        /// <returns>BillPaymentCreditCard object with specified id</returns>
		public BillPaymentCreditCard FindById(PlatformSessionContext context, string realmId, IdType billpaymentcreditcardIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			BillPaymentCreditCard billpaymentcreditcardFound = null;
			BillPaymentCreditCards billpaymentcreditcards = (BillPaymentCreditCards)base.FindById(context, realmId, billpaymentcreditcardIdToFind, IDSResource.billpaymentcreditcard, typeof(BillPaymentCreditCards));
			if (billpaymentcreditcards.BillPaymentCreditCard == null || billpaymentcreditcards.BillPaymentCreditCard.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"BillPaymentCreditCard not found.");
				return null;
			}
			billpaymentcreditcardFound = billpaymentcreditcards.BillPaymentCreditCard[0];
            return billpaymentcreditcardFound;
		}
		
		/// <summary>
        /// Query on BillPaymentCreditCard object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<BillPaymentCreditCard> GetBillPaymentCreditCards(PlatformSessionContext context, string realmId, BillPaymentCreditCardQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			BillPaymentCreditCards searchBillPaymentCreditCards = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.billpaymentcreditcard,realmId);
			
			if(searchQuery != null)
			{
            	searchBillPaymentCreditCards = (BillPaymentCreditCards)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchBillPaymentCreditCards = (BillPaymentCreditCards)base.GetResources(context, operationContext, typeof(BillPaymentCreditCards));
			}
                    
            if (searchBillPaymentCreditCards == null || searchBillPaymentCreditCards.BillPaymentCreditCard == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "BillPaymentCreditCard not found.");
                return null;
            }
            return new List<BillPaymentCreditCard>(searchBillPaymentCreditCards.BillPaymentCreditCard);
        }

		/// <summary>
		/// Deletes a BillPaymentCreditCard under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newBillPaymentCreditCard">BillPaymentCreditCard object to Delete</param>
		public void DeleteBillPaymentCreditCard(PlatformSessionContext context, string realmId, BillPaymentCreditCard newBillPaymentCreditCard)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newBillPaymentCreditCard, IDSResource.billpaymentcreditcard);
		}

	}
}

