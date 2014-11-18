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
	/// Provides Method to perform CRUD operations on Customer Resource of QuickBooks
	/// </summary>
	public class CustomerService : IDSBaseService
	{

		/// <summary>
		/// Adds a Customer under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCustomer">Customer object to Add</param>
		/// <returns>Returns an updated version of the Customer with updated IdType and sync token.</returns>
		public Customer AddCustomer(PlatformSessionContext context, string realmId, Customer newCustomer)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCustomer = (Customer)base.AddResource(context, realmId, newCustomer, IDSResource.customer);
			return newCustomer;
		}

		/// <summary>
        /// Returns a list of all Customers under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Customers</returns>
		public List<Customer> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.customer;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.customers;
            }
			Customers listOfObjects = (Customers)base.FindAll(context, realmId, resource,typeof(Customers));
			if (listOfObjects != null && listOfObjects.Customer != null)
            {
                return new List<Customer>(listOfObjects.Customer);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Customer not found.");
                return new List<Customer>();
            }
		}

		/// <summary>
        /// Returns a Customer based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="customerIdToFind">Customer Id</param>
        /// <returns>Customer object with specified id</returns>
		public Customer FindById(PlatformSessionContext context, string realmId, IdType customerIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Customer customerFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Customers customers = (Customers)base.FindById(context, realmId, customerIdToFind, IDSResource.customer, typeof(Customers));
					if (customers.Customer == null || customers.Customer.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Customer not found.");
						return null;
					}
					customerFound = customers.Customer[0];
					break;
				case IntuitServicesType.QBO:
					customerFound = (Customer)base.FindById(context, realmId, customerIdToFind, IDSResource.customer, typeof(Customer));
					break;
			}
            return customerFound;
		}
		
		/// <summary>
        /// Query on Customer object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Customer> GetCustomers(PlatformSessionContext context, string realmId, QBQBOCustomerQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Customers searchCustomers = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.customer, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.customers, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchCustomers = (Customers)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchCustomers = (Customers)base.GetResources(context, operationContext, typeof(Customers));
			}
                    
            if (searchCustomers == null || searchCustomers.Customer == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Customer not found.");
                return null;
            }
            return new List<Customer>(searchCustomers.Customer);
        }

		/// <summary>
		/// Updates a Customer under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCustomer">Customer object to Update</param>
		/// <returns>Returns an updated version of the Customer with updated IdType and sync token.</returns>
		public Customer UpdateCustomer(PlatformSessionContext context, string realmId, Customer newCustomer)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCustomer = (Customer)base.UpdateResource(context, realmId, newCustomer, IDSResource.customer);
			return newCustomer;
		}

		/// <summary>
		/// Deletes a Customer under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCustomer">Customer object to Delete</param>
		public void DeleteCustomer(PlatformSessionContext context, string realmId, Customer newCustomer)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newCustomer, IDSResource.customer);
		}

		/// <summary>
		/// Reverts a Customer under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newCustomer">Customer object to Revert</param>
		/// <returns>Returns an updated version of the Customer with updated IdType and sync token.</returns>
		#warning 'Customer Revert operation is supported by QB'
		public Customer RevertCustomer(PlatformSessionContext context, string realmId, Customer newCustomer)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newCustomer = (Customer)base.RevertResource(context, realmId, newCustomer, IDSResource.customer);
			return newCustomer;
		}

	}
}

