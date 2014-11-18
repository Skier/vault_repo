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
	/// Provides Method to perform CRUD operations on SalesTaxCode Resource of QuickBooks
	/// </summary>
	public class SalesTaxCodeService : IDSBaseService
	{

		/// <summary>
        /// Returns a list of all SalesTaxCodes under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all SalesTaxCodes</returns>
		public List<SalesTaxCode> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.salestaxcode;
           
			SalesTaxCodes listOfObjects = (SalesTaxCodes)base.FindAll(context, realmId, resource,typeof(SalesTaxCodes));
			if (listOfObjects != null && listOfObjects.SalesTaxCode != null)
            {
                return new List<SalesTaxCode>(listOfObjects.SalesTaxCode);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"SalesTaxCode not found.");
                return new List<SalesTaxCode>();
            }
		}

		/// <summary>
        /// Returns a SalesTaxCode based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="salestaxcodeIdToFind">SalesTaxCode Id</param>
        /// <returns>SalesTaxCode object with specified id</returns>
		public SalesTaxCode FindById(PlatformSessionContext context, string realmId, IdType salestaxcodeIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			SalesTaxCode salestaxcodeFound = null;
			SalesTaxCodes salestaxcodes = (SalesTaxCodes)base.FindById(context, realmId, salestaxcodeIdToFind, IDSResource.salestaxcode, typeof(SalesTaxCodes));
			if (salestaxcodes.SalesTaxCode == null || salestaxcodes.SalesTaxCode.Length == 0)
			{
				Logger.WriteToLog(TraceLevel.Info,"SalesTaxCode not found.");
				return null;
			}
			salestaxcodeFound = salestaxcodes.SalesTaxCode[0];
            return salestaxcodeFound;
		}
		
		/// <summary>
        /// Query on SalesTaxCode object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<SalesTaxCode> GetSalesTaxCodes(PlatformSessionContext context, string realmId, SalesTaxCodeQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			SalesTaxCodes searchSalesTaxCodes = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.salestaxcode,realmId);
			
			if(searchQuery != null)
			{
            	searchSalesTaxCodes = (SalesTaxCodes)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchSalesTaxCodes = (SalesTaxCodes)base.GetResources(context, operationContext, typeof(SalesTaxCodes));
			}
                    
            if (searchSalesTaxCodes == null || searchSalesTaxCodes.SalesTaxCode == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "SalesTaxCode not found.");
                return null;
            }
            return new List<SalesTaxCode>(searchSalesTaxCodes.SalesTaxCode);
        }

	}
}

