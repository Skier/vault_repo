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
	/// Provides Method to perform CRUD operations on Invoice Resource of QuickBooks
	/// </summary>
	public class InvoiceService : IDSBaseService
	{

		/// <summary>
		/// Adds a Invoice under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newInvoice">Invoice object to Add</param>
		/// <returns>Returns an updated version of the Invoice with updated IdType and sync token.</returns>
		public Invoice AddInvoice(PlatformSessionContext context, string realmId, Invoice newInvoice)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newInvoice = (Invoice)base.AddResource(context, realmId, newInvoice, IDSResource.invoice);
			return newInvoice;
		}

		/// <summary>
        /// Returns a list of all Invoices under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Invoices</returns>
		public List<Invoice> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.invoice;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.invoices;
            }
			Invoices listOfObjects = (Invoices)base.FindAll(context, realmId, resource,typeof(Invoices));
			if (listOfObjects != null && listOfObjects.Invoice != null)
            {
                return new List<Invoice>(listOfObjects.Invoice);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Invoice not found.");
                return new List<Invoice>();
            }
		}

		/// <summary>
        /// Returns a Invoice based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="invoiceIdToFind">Invoice Id</param>
        /// <returns>Invoice object with specified id</returns>
		public Invoice FindById(PlatformSessionContext context, string realmId, IdType invoiceIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Invoice invoiceFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Invoices invoices = (Invoices)base.FindById(context, realmId, invoiceIdToFind, IDSResource.invoice, typeof(Invoices));
					if (invoices.Invoice == null || invoices.Invoice.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Invoice not found.");
						return null;
					}
					invoiceFound = invoices.Invoice[0];
					break;
				case IntuitServicesType.QBO:
					invoiceFound = (Invoice)base.FindById(context, realmId, invoiceIdToFind, IDSResource.invoice, typeof(Invoice));
					break;
			}
            return invoiceFound;
		}
		
		/// <summary>
        /// Query on Invoice object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Invoice> GetInvoices(PlatformSessionContext context, string realmId, QBQBOInvoiceQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Invoices searchInvoices = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.invoice, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.invoices, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchInvoices = (Invoices)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchInvoices = (Invoices)base.GetResources(context, operationContext, typeof(Invoices));
			}
                    
            if (searchInvoices == null || searchInvoices.Invoice == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Invoice not found.");
                return null;
            }
            return new List<Invoice>(searchInvoices.Invoice);
        }

		/// <summary>
		/// Updates a Invoice under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newInvoice">Invoice object to Update</param>
		/// <returns>Returns an updated version of the Invoice with updated IdType and sync token.</returns>
		public Invoice UpdateInvoice(PlatformSessionContext context, string realmId, Invoice newInvoice)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newInvoice = (Invoice)base.UpdateResource(context, realmId, newInvoice, IDSResource.invoice);
			return newInvoice;
		}

		/// <summary>
		/// Deletes a Invoice under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newInvoice">Invoice object to Delete</param>
		public void DeleteInvoice(PlatformSessionContext context, string realmId, Invoice newInvoice)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newInvoice, IDSResource.invoice);
		}

		/// <summary>
		/// Reverts a Invoice under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newInvoice">Invoice object to Revert</param>
		/// <returns>Returns an updated version of the Invoice with updated IdType and sync token.</returns>
		#warning 'Invoice Revert operation is supported by QB'
		public Invoice RevertInvoice(PlatformSessionContext context, string realmId, Invoice newInvoice)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newInvoice = (Invoice)base.RevertResource(context, realmId, newInvoice, IDSResource.invoice);
			return newInvoice;
		}

	}
}

