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

namespace Intuit.Sb.Cdm.Common
{
	/// <summary>
	/// Provides Method to perform CRUD operations on CreditCardCharge Resource of QuickBooks
	/// </summary>
	public class CreditCardChargeService : IDSBaseService
	{

		/// <summary>
		/// Adds a CreditCardCharge under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCreditCardCharge">CreditCardCharge object to Add</param>
		/// <returns>Returns an updated version of the CreditCardCharge with updated IdType and sync token.</returns>
		#warning 'CreditCardCharge Add operation is supported by QBO'
		public CreditCardCharge AddCreditCardCharge(PlatformSessionContext context, string realmId, CreditCardCharge newCreditCardCharge)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCreditCardCharge = (CreditCardCharge)base.AddResource(context, realmId, newCreditCardCharge, IDSResource.creditcardcharge);
			return newCreditCardCharge;
		}

		/// <summary>
        /// Returns a list of all CreditCardCharges under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all CreditCardCharges</returns>
		public List<CreditCardCharge> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.creditcardcharge;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.creditcardcharges;
            }
			CreditCardCharges listOfObjects = (CreditCardCharges)base.FindAll(context, realmId, resource,typeof(CreditCardCharges));
			if (listOfObjects != null && listOfObjects.CreditCardCharge != null)
            {
                return new List<CreditCardCharge>(listOfObjects.CreditCardCharge);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"CreditCardCharge not found.");
                return new List<CreditCardCharge>();
            }
		}

		/// <summary>
        /// Returns a CreditCardCharge based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="creditcardchargeIdToFind">CreditCardCharge Id</param>
        /// <returns>CreditCardCharge object with specified id</returns>
		public CreditCardCharge FindById(PlatformSessionContext context, string realmId, IdType creditcardchargeIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			CreditCardCharge creditcardchargeFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					CreditCardCharges creditcardcharges = (CreditCardCharges)base.FindById(context, realmId, creditcardchargeIdToFind, IDSResource.creditcardcharge, typeof(CreditCardCharges));
					if (creditcardcharges.CreditCardCharge == null || creditcardcharges.CreditCardCharge.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"CreditCardCharge not found.");
						return null;
					}
					creditcardchargeFound = creditcardcharges.CreditCardCharge[0];
					break;
				case IntuitServicesType.QBO:
					creditcardchargeFound = (CreditCardCharge)base.FindById(context, realmId, creditcardchargeIdToFind, IDSResource.creditcardcharge, typeof(CreditCardCharge));
					break;
			}
            return creditcardchargeFound;
		}
		
		/// <summary>
        /// Query on CreditCardCharge object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<CreditCardCharge> GetCreditCardCharges(PlatformSessionContext context, string realmId, QBQBOCreditCardChargeQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			CreditCardCharges searchCreditCardCharges = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.creditcardcharge, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.creditcardcharges, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchCreditCardCharges = (CreditCardCharges)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCreditCardCharges = (CreditCardCharges)base.GetResources(context, operationContext, typeof(CreditCardCharges));
			}
                    
            if (searchCreditCardCharges == null || searchCreditCardCharges.CreditCardCharge == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "CreditCardCharge not found.");
                return null;
            }
            return new List<CreditCardCharge>(searchCreditCardCharges.CreditCardCharge);
        }

		/// <summary>
		/// Updates a CreditCardCharge under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCreditCardCharge">CreditCardCharge object to Update</param>
		/// <returns>Returns an updated version of the CreditCardCharge with updated IdType and sync token.</returns>
		#warning 'CreditCardCharge Update operation is supported by QBO'
		public CreditCardCharge UpdateCreditCardCharge(PlatformSessionContext context, string realmId, CreditCardCharge newCreditCardCharge)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCreditCardCharge = (CreditCardCharge)base.UpdateResource(context, realmId, newCreditCardCharge, IDSResource.creditcardcharge);
			return newCreditCardCharge;
		}

		/// <summary>
		/// Deletes a CreditCardCharge under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCreditCardCharge">CreditCardCharge object to Delete</param>
		#warning 'CreditCardCharge Delete operation is supported by QBO'
		public void DeleteCreditCardCharge(PlatformSessionContext context, string realmId, CreditCardCharge newCreditCardCharge)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newCreditCardCharge, IDSResource.creditcardcharge);
		}

	}
}

