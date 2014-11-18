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
	/// Provides Method to perform CRUD operations on CreditCardCredit Resource of QuickBooks
	/// </summary>
	public class CreditCardCreditService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all CreditCardCredits under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CreditCardCredits</returns>
		public List<CreditCardCredit> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.creditcardcredit;
           
			CreditCardCredits listOfObjects = (CreditCardCredits)base.FindAll(context, realmId, resource,typeof(CreditCardCredits));
			if (listOfObjects != null && listOfObjects.CreditCardCredit != null)
            {
                return new List<CreditCardCredit>(listOfObjects.CreditCardCredit);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"CreditCardCredit not found.");
                return new List<CreditCardCredit>();
            }
		}

		/// <summary>
        /// Returns a CreditCardCredit based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="creditcardcreditIdToFind">CreditCardCredit Id</param>
        /// <returns>CreditCardCredit object with specified id</returns>
		public CreditCardCredit FindById(PlatformSessionContext context, string realmId, IdType creditcardcreditIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			CreditCardCredit creditcardcreditFound = null;
			CreditCardCredits creditcardcredits = (CreditCardCredits)base.FindById(context, realmId, creditcardcreditIdToFind, IDSResource.creditcardcredit, typeof(CreditCardCredits));
			if (creditcardcredits.CreditCardCredit == null || creditcardcredits.CreditCardCredit.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"CreditCardCredit not found.");
				return null;
			}
			creditcardcreditFound = creditcardcredits.CreditCardCredit[0];
            return creditcardcreditFound;
		}
		
		/// <summary>
        /// Query on CreditCardCredit object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<CreditCardCredit> GetCreditCardCredits(PlatformSessionContext context, string realmId, CreditCardCreditQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			CreditCardCredits searchCreditCardCredits = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.creditcardcredit,realmId);
			
			if(searchQuery != null)
			{
            	searchCreditCardCredits = (CreditCardCredits)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCreditCardCredits = (CreditCardCredits)base.GetResources(context, operationContext, typeof(CreditCardCredits));
			}
                    
            if (searchCreditCardCredits == null || searchCreditCardCredits.CreditCardCredit == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "CreditCardCredit not found.");
                return null;
            }
            return new List<CreditCardCredit>(searchCreditCardCredits.CreditCardCredit);
        }

	}
}

