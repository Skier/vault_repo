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
	/// Provides Method to perform CRUD operations on Employee Resource of QuickBooks
	/// </summary>
	public class EmployeeService : IDSBaseService
	{

		/// <summary>
		/// Adds a Employee under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEmployee">Employee object to Add</param>
		/// <returns>Returns an updated version of the Employee with updated IdType and sync token.</returns>
		public Employee AddEmployee(PlatformSessionContext context, string realmId, Employee newEmployee)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newEmployee = (Employee)base.AddResource(context, realmId, newEmployee, IDSResource.employee);
			return newEmployee;
		}

		/// <summary>
        /// Returns a list of all Employees under the specified Realm.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <returns>List of all Employees</returns>
		public List<Employee> FindAll(PlatformSessionContext context, string realmId)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			IDSResource resource = IDSResource.employee;
           
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                resource = IDSResource.employees;
            }
			Employees listOfObjects = (Employees)base.FindAll(context, realmId, resource,typeof(Employees));
			if (listOfObjects != null && listOfObjects.Employee != null)
            {
                return new List<Employee>(listOfObjects.Employee);
            }
            else
            {	
				Logger.WriteToLog(TraceLevel.Info,"Employee not found.");
                return new List<Employee>();
            }
		}

		/// <summary>
        /// Returns a Employee based on the Id string.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">Users RealmID</param>
        /// <param name="employeeIdToFind">Employee Id</param>
        /// <returns>Employee object with specified id</returns>
		public Employee FindById(PlatformSessionContext context, string realmId, IdType employeeIdToFind)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmId: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			Employee employeeFound = null;
				
			switch (context.ServiceType)
			{
				case IntuitServicesType.QBD:
					Employees employees = (Employees)base.FindById(context, realmId, employeeIdToFind, IDSResource.employee, typeof(Employees));
					if (employees.Employee == null || employees.Employee.Length == 0)
					{
						Logger.WriteToLog(TraceLevel.Info,"Employee not found.");
						return null;
					}
					employeeFound = employees.Employee[0];
					break;
				case IntuitServicesType.QBO:
					employeeFound = (Employee)base.FindById(context, realmId, employeeIdToFind, IDSResource.employee, typeof(Employee));
					break;
			}
            return employeeFound;
		}
		
		/// <summary>
        /// Query on Employee object basis on the search criteria and company Id.
        /// </summary>
        /// <param name="context">Session information</param>
        /// <param name="realmId">Company, for which search query to be executed.</param>        
        /// <param name="searchQuery">Criteria for the search
		///</param>
        /// <returns>Returns Search result.</returns>
		
		public List<Employee> GetEmployees(PlatformSessionContext context, string realmId, QBQBOEmployeeQuery searchQuery)
        {
			Logger.WriteToLog(TraceLevel.Info,"Realm Id: " +realmId);
			
			base.SetServiceTypeProperty(realmId,ref context);
			
			Employees searchEmployees = null;

						IDSOperationContext operationContext = new IDSOperationContext(IDSResource.employee, realmId);
			if (context.ServiceType == IntuitServicesType.QBO)
            {
                operationContext = new IDSOperationContext(IDSResource.employees, realmId);
            }
           
			if(searchQuery != null)
			{
            	searchEmployees = (Employees)base.GetResourcesForQuery(context,operationContext,searchQuery);
			}
			else
			{
				searchEmployees = (Employees)base.GetResources(context, operationContext, typeof(Employees));
			}
                    
            if (searchEmployees == null || searchEmployees.Employee == null)
            {
				Logger.WriteToLog(TraceLevel.Info, "Employee not found.");
                return null;
            }
            return new List<Employee>(searchEmployees.Employee);
        }

		/// <summary>
		/// Updates a Employee under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEmployee">Employee object to Update</param>
		/// <returns>Returns an updated version of the Employee with updated IdType and sync token.</returns>
		public Employee UpdateEmployee(PlatformSessionContext context, string realmId, Employee newEmployee)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newEmployee = (Employee)base.UpdateResource(context, realmId, newEmployee, IDSResource.employee);
			return newEmployee;
		}

		/// <summary>
		/// Deletes a Employee under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEmployee">Employee object to Delete</param>
		public void DeleteEmployee(PlatformSessionContext context, string realmId, Employee newEmployee)
		{
			Logger.WriteToLog(TraceLevel.Info,"RealmID: " +realmId);
			base.SetServiceTypeProperty(realmId,ref context);
			base.DeleteResource(context, realmId, newEmployee, IDSResource.employee);
		}

		/// <summary>
		/// Reverts a Employee under the specified realm. 
		/// </summary>
		/// <param name="context">PlatformSessionContext object with session info filled</param>
		/// <param name="realmId">Users RealmID</param>
		/// <param name="newEmployee">Employee object to Revert</param>
		/// <returns>Returns an updated version of the Employee with updated IdType and sync token.</returns>
		#warning 'Employee Revert operation is supported by QB'
		public Employee RevertEmployee(PlatformSessionContext context, string realmId, Employee newEmployee)
		{
			base.SetServiceTypeProperty(realmId,ref context);
			newEmployee = (Employee)base.RevertResource(context, realmId, newEmployee, IDSResource.employee);
			return newEmployee;
		}

	}
}

