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
	/// Provides Method to perform CRUD operations on SalesTerm Resource of QuickBooks
	/// </summary>
	public class SalesTermService : IDSBaseService
	{

		/// <summary>
		/// Adds a SalesTerm under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesTerm">SalesTerm object to Add</param>
		/// <returns>Returns an updated version of the SalesTerm with updated IdType and sync token.</returns>
		public SalesTerm AddSalesTerm(PlatformSessionContext context, string realmId, SalesTerm newSalesTerm)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesTerm = (SalesTerm)base.AddResource(context, realmId, newSalesTerm, IDSResource.salesterm);
			return newSalesTerm;
		}

		/// <summary>
        /// Returns a list of all SalesTerms under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesTerms</returns>
		public List<SalesTerm> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salesterm;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.salesterms;
            }
			SalesTerms listOfObjects = (SalesTerms)base.FindAll(context, realmId, resource,typeof(SalesTerms));
			if (listOfObjects != null && listOfObjects.SalesTerm != null)
            {
                return new List<SalesTerm>(listOfObjects.SalesTerm);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesTerm not found.");
                return new List<SalesTerm>();
            }
		}

		/// <summary>
        /// Returns a SalesTerm based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salestermIdToFind">SalesTerm Id</param>
        /// <returns>SalesTerm object with specified id</returns>
		public SalesTerm FindById(PlatformSessionContext context, string realmId, IdType salestermIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesTerm salestermFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					SalesTerms salesterms = (SalesTerms)base.FindById(context, realmId, salestermIdToFind, IDSResource.salesterm, typeof(SalesTerms));
					if (salesterms.SalesTerm == null || salesterms.SalesTerm.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"SalesTerm not found.");
						return null;
					}
					salestermFound = salesterms.SalesTerm[0];
					break;
				case IntuitServicesType.QBO:
					salestermFound = (SalesTerm)base.FindById(context, realmId, salestermIdToFind, IDSResource.salesterm, typeof(SalesTerm));
					break;
			}
            return salestermFound;
		}
		
		/// <summary>
        /// Query on SalesTerm object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesTerm> GetSalesTerms(PlatformSessionContext context, string realmId, QBQBOTermQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesTerms searchSalesTerms = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salesterm, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.salesterms, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchSalesTerms = (SalesTerms)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesTerms = (SalesTerms)base.GetResources(context, operationContext, typeof(SalesTerms));
			}
                    
            if (searchSalesTerms == null || searchSalesTerms.SalesTerm == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesTerm not found.");
                return null;
            }
            return new List<SalesTerm>(searchSalesTerms.SalesTerm);
        }

		/// <summary>
		/// Updates a SalesTerm under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesTerm">SalesTerm object to Update</param>
		/// <returns>Returns an updated version of the SalesTerm with updated IdType and sync token.</returns>
		#warning 'SalesTerm Update operation is supported by QBO'
		public SalesTerm UpdateSalesTerm(PlatformSessionContext context, string realmId, SalesTerm newSalesTerm)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newSalesTerm = (SalesTerm)base.UpdateResource(context, realmId, newSalesTerm, IDSResource.salesterm);
			return newSalesTerm;
		}

		/// <summary>
		/// Deletes a SalesTerm under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newSalesTerm">SalesTerm object to Delete</param>
		public void DeleteSalesTerm(PlatformSessionContext context, string realmId, SalesTerm newSalesTerm)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newSalesTerm, IDSResource.salesterm);
		}

	}
}

