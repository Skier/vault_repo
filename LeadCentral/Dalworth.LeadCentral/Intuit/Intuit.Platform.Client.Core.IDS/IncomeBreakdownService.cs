
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
	/// Provides Method to perform CRUD operations on IncomeBreakdown Resource of QuickBooks
	/// </summary>
	public class IncomeBreakdownService : IDSReportBaseService
	{
		
		/// <summary>
        /// Query on IncomeBreakdown object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public Report GetReport(PlatformSessionContext context, string realmId, ReportIncomeBreakdown searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			Report searchReport = null;

			IDSOperationContext operationContext = new IDSOperationContext(IDSResource.incomebreakdown,realmId);

			if(searchQuery != null)
			{
            	searchReport = (Report)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchReport = (Report)base.GetResources(context, operationContext, typeof(Report));
			}
                    
           
            return searchReport;

		}
		
		/// <summary>
        /// Query on IncomeBreakdown object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <returns>Returns Search result.</returns>
		public Report GetReport(PlatformSessionContext context, string realmId)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);

			Report searchReport = null;

			IDSOperationContext operationContext = new IDSOperationContext(IDSResource.incomebreakdown,realmId);

			searchReport = (Report)base.GetResources(context, operationContext, typeof(Report));
			
            return searchReport;

		}


	}
}

