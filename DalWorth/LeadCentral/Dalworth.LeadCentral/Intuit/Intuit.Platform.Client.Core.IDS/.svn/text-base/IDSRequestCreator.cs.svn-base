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
using System.IO;
using System.Xml.Serialization;
using Intuit.Sb.Cdm;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

namespace Intuit.Platform.Client.Core.IDS
{
    public abstract class IDSRequestCreator
    {
        /// <summary>
        /// Send request to the REST-Like service
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="operationContext">IDSOperationContext object with realmId and resource property set</param>
        /// <param name="request">Request object</param>
        /// <returns>Response received from REST-Like Service</returns>
        protected object DoIDSPost(PlatformSessionContext context, IDSOperationContext operationContext, object request)
        {

            ObjectSerializer serializer = new ObjectSerializer();
            List<string> namespaces = new List<string>();
            if (context.ServiceType == IntuitServicesType.QBO)
            {
                namespaces.Add(Properties.Settings.Default.QboNameSpace);
            }
            StringBuilder requestXML = new StringBuilder();

            if (request != null)
            {
                if (request.GetType().Name.ToLower().CompareTo("string") == 0)
                {
                    requestXML.Append(request.ToString());
                }
                else
                    requestXML = serializer.SerializeObject(request, namespaces);
            }

            IDSRestClient restClient = new IDSRestClient(context.ServiceType, context);

            Logger.WriteToLog(TraceLevel.Info, "Request XML created is: " + requestXML);

            String postResponse = restClient.Send(context, Verb.POST, operationContext, requestXML.ToString());

            //Write log 
            Logger.WriteToLog(TraceLevel.Info, "Response received from REST-like service: " + postResponse);

            if (context.ServiceType == IntuitServicesType.QBO && (request == null || request.GetType() == typeof(string)))
            {
                request = new SearchResults();
            }

            return ParseResponse(postResponse, context.ServiceType, request.GetType());

        }

        /// <summary>
        /// Send request to the REST-Like service
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="operationContext">IDSOperationContext object with realmId and resource property set</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>Response received from REST-Like Service</returns>
        protected object DoIDSGet(PlatformSessionContext context, IDSOperationContext operationContext, Type objectType)
        {
            //FIXME the RestClient could be singleton
            IDSRestClient restClient = new IDSRestClient(context.ServiceType, context);
            String getResponse = restClient.Send(context, Verb.GET, operationContext, null);

            Logger.WriteToLog(TraceLevel.Info, "Response received from REST-like service: " + getResponse);

            return ParseResponse(getResponse, context.ServiceType, objectType);
        }
        /// <summary>
        /// Parse response from server and Deserialize it to CDM object.
        /// </summary>
        /// <param name="responseString">Response Received from REST server</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="requestObject">The request object.</param>
        /// <returns>CDM object</returns>
        protected object ParseResponse(String responseString, IntuitServicesType serviceType, Type requestObject)
        {
            object payload = new object();
            ObjectSerializer serializer = new ObjectSerializer();
            switch (serviceType)
            {
                case IntuitServicesType.QBD:
                    RestResponse restResponse = (RestResponse)serializer.DeserializeObject(responseString, typeof(RestResponse));
                    payload = (CdmComplexBase)restResponse.Item;
                    if (payload is ErrorResponse)
                    {
                        ErrorResponse errorResponse = (ErrorResponse)payload;
                        throw new IDSException(null, string.Format("Error Code:{0}, Error Description: {1}", errorResponse.ErrorCode, errorResponse.ErrorDesc));
                    }
                    break;
                case IntuitServicesType.QBO:
                    payload = serializer.DeserializeObject(responseString, requestObject);
                    break;
            }
            return payload;
        }

        /// <summary>
        /// Creates the query object.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns></returns>
        public object CreateQueryObject(QueryBase queryObject)
        {
            Type derivedType = queryObject.GetType();
            object baseQueryObject = Activator.CreateInstance(derivedType.BaseType, true);
            Type baseType = baseQueryObject.GetType();

            foreach (PropertyInfo pInfo1 in derivedType.GetProperties())
                foreach (PropertyInfo pInfo2 in baseType.GetProperties())
                    if (pInfo1.Name == pInfo2.Name)
                    {
                        if (pInfo1.CanRead && pInfo2.CanWrite)
                            pInfo2.SetValue(baseQueryObject, pInfo1.GetValue(queryObject, null), null);
                        break;
                    }

            return baseQueryObject;
        }

        /// <summary>
        /// Creates the query string.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns></returns>
        public static string CreateQueryString(QueryBase queryObject)
        {
            object val = null;

            //Create a string for QBO
            StringBuilder searchQuery = new StringBuilder();
            searchQuery.Append("Filter=");

            //Load mapping XML into memory.
            XmlDocument xDoc = new XmlDocument();

            //load currently running assembly.
            Assembly currentAssembly = null;

            currentAssembly = Assembly.GetExecutingAssembly();

            //Read XML and load into XMLDocument
            string xmlResouce = currentAssembly.GetName().Name + ".QBQBOFilterAttributeMapping.xml";

            using (Stream s = currentAssembly.GetManifestResourceStream(xmlResouce))
            {
                xDoc.Load(s);
            }

            //Find out the resource name to create Query for...
            string queryName = string.Empty;
            if (queryObject.GetType().Name.Contains("CashPurchase"))
            {
                queryName = queryObject.GetType().Name;

                queryName = queryName.Replace("QBQBO", "");
            }
            else
            {
                queryName = queryObject.GetType().BaseType.Name;
            }

            queryName = queryName.Replace("Query", "");

            //Get type of query object..
            string typeName = queryObject.GetType().FullName;

            //Locate the XML node for selected resource...[@Name='" + queryName + "']
            string path = "Resources/Resource[@Name='" + queryName + "']";
            XmlNode queryObjectNode = xDoc.SelectSingleNode(path);

            //For each XML node if value specified in query object add string to query input...
            foreach (XmlNode childNode in queryObjectNode.ChildNodes)
            {
                val = null;
                if (childNode.Name.CompareTo("Sort") != 0)
                {
                    string operand = string.Empty;
                    string valuePropertyName = string.Empty;
                    string objectPropertyName = string.Empty;

                    if (childNode.Attributes["ValuePropertyName"] != null)
                    {
                        valuePropertyName = childNode.Attributes["ValuePropertyName"].Value;
                    }
                    if (childNode.Attributes["PropertyName"] != null)
                    {
                        objectPropertyName = childNode.Attributes["PropertyName"].Value;
                    }

                    //Find Property name in Query object...
                    string propertyName = childNode.Name;

                    //Find Filter Attribute name for Property name
                    string propertyValue = childNode.InnerText;


                    //Find out operand for filter attribute
                    if (propertyName.StartsWith("Start") && (propertyName.EndsWith("TMS") || propertyName.EndsWith("Date")))
                    {
                        operand = " :AFTER: ";
                    }
                    else if (propertyName.StartsWith("End") && (propertyName.EndsWith("TMS") || propertyName.EndsWith("Date")))
                    {
                        operand = " :BEFORE: ";
                    }
                    else
                        operand = " :EQUALS: ";



                    if (objectPropertyName != string.Empty)
                    {

                        //read property and its value
                        object objectPropertyAttributes = currentAssembly.GetType(queryObject.GetType().FullName).GetProperty(objectPropertyName).GetValue(queryObject, null);
                        object objectPropertyValues = currentAssembly.GetType(queryObject.GetType().FullName).GetProperty(valuePropertyName).GetValue(queryObject, null);

                        if (objectPropertyAttributes != null && objectPropertyValues != null)
                        {
                            //If property is an array find the values for particular filter attribute...
                            if (objectPropertyAttributes.GetType().BaseType.Name.CompareTo("Array") == 0 &&
                                objectPropertyValues.GetType().BaseType.Name.CompareTo("Array") == 0)
                            {
                                Array objProp = (Array)objectPropertyAttributes;
                                Array objVal = (Array)objectPropertyValues;
                                for (int index = 0; index < objProp.Length; index++)
                                {
                                    if (objProp.GetValue(index).ToString().CompareTo(propertyName) == 0)
                                    {
                                        val = objVal.GetValue(index);
                                    }
                                }
                            }
                            //If property is and IdSet get first IDtype Value and use it...
                            //Note: QBO does not support multiple values for single filter attribute
                            //  So using just first value and ignoring other....
                            else if (objectPropertyValues.GetType().Name.CompareTo("IdSet") == 0)
                            {
                                if (objectPropertyAttributes.ToString().CompareTo(propertyName) == 0)
                                {
                                    IdSet setOfIds = (IdSet)objectPropertyValues;
                                    if (setOfIds != null)
                                    {
                                        foreach (IdType id in setOfIds.Id)
                                        {
                                            val = id.Value;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //If Specified property of the particular attribute is not set ignore the actual value...
                        if (currentAssembly.GetType(typeName).GetProperty(propertyName + "Specified") != null)
                        {
                            bool isSpecified = Convert.ToBoolean(currentAssembly.GetType(typeName).GetProperty(propertyName + "Specified").GetValue(queryObject, null));
                            if (isSpecified)
                            {
                                val = currentAssembly.GetType(typeName).GetProperty(propertyName).GetValue(queryObject, null);
                            }
                        }
                        else
                        {
                            val = currentAssembly.GetType(typeName).GetProperty(propertyName).GetValue(queryObject, null);
                        }

                    }

                    //If value exist add string to query string along with Filter attribute and Operand
                    if (val != null)
                    {
                        string queryInputValue = string.Empty;
                        if (val.GetType().Name.Contains("Date"))
                        {
                            if (childNode.InnerText.Contains("Time"))
                            {
                                queryInputValue = Convert.ToDateTime(val).ToString("yyyy-MM-ddTHH:mm:ss");
                                // Get the local time zone and the current local time and year.
                                string timeZoneName = TimeZone.CurrentTimeZone.IsDaylightSavingTime(Convert.ToDateTime(val)) ? TimeZone.CurrentTimeZone.DaylightName : TimeZone.CurrentTimeZone.StandardName;

                                String abbrTimeZone = string.Empty;
                                String[] sSplit = timeZoneName.Split(new char[] { ' ' });
                                foreach (String s in sSplit)
                                    if (s.Length >= 1)
                                        abbrTimeZone += s.Substring(0, 1);
                                queryInputValue += abbrTimeZone;
                            }
                            else
                            {
                                queryInputValue = Convert.ToDateTime(val).ToString("yyyy-MM-dd");
                            }
                        }
                        else
                            queryInputValue = val.ToString();

                        searchQuery.Append(" ");
                        searchQuery.Append(propertyValue);
                        searchQuery.Append(operand);
                        searchQuery.Append(queryInputValue);
                        searchQuery.Append(" ");
                        searchQuery.Append(" :AND: ");
                    }
                }
               
            }
            //Trim query string for AND 
            if (searchQuery.ToString().EndsWith(" :AND: "))
            {

                searchQuery = new StringBuilder(searchQuery.ToString().TrimEnd(" :AND: ".ToCharArray()));
            }

            //If no filter criteria added remove the string Filter=
            if (searchQuery.ToString().EndsWith("Filter="))
            {
                searchQuery = new StringBuilder();
            }

            //Check if  chunk size is specified if yes add to QBO query string
            if (currentAssembly.GetType(typeName).GetProperty("ChunkSize") != null)
            {
                val = currentAssembly.GetType(typeName).GetProperty("ChunkSize").GetValue(queryObject, null);
                if (val != null)
                {
                    searchQuery.Append("\n ResultsPerPage");
                    searchQuery.Append("=");
                    searchQuery.Append(val.ToString());
                    searchQuery.Append(" ");
                }
            }

            //Check id start page is specified if yes add it to QVO query string...
            if (currentAssembly.GetType(typeName).GetProperty("ItemElementName") != null)
            {
                object objectPropertyAttributes = currentAssembly.GetType(queryObject.GetType().FullName).GetProperty("ItemElementName").GetValue(queryObject, null);
                object objectPropertyValues = currentAssembly.GetType(queryObject.GetType().FullName).GetProperty("Item").GetValue(queryObject, null);

                if (objectPropertyAttributes != null && objectPropertyValues != null)
                {
                    if (objectPropertyAttributes.ToString().CompareTo("StartPage") == 0)
                    {
                        val = objectPropertyValues.ToString();
                    }
                    if (val != null)
                    {
                        searchQuery.Append("\n PageNum");
                        searchQuery.Append("=");
                        searchQuery.Append(val.ToString());
                        searchQuery.Append(" ");
                    }
                }

            }

            //Handle the Sort Criteria....
            //Check id Sort is specified if yes add it to QVO query string...
            if (currentAssembly.GetType(typeName).GetProperty("SortByColumn") != null)
            {
                path = "Resources/Resource[@Name='" + queryName + "']/Sort";
                queryObjectNode = xDoc.SelectSingleNode(path);

                object sortAttribute = null;
                object sortOrder = null;

                object c = (object)currentAssembly.GetType(typeName).GetProperty("SortByColumn").GetValue(queryObject, null);
                if (c != null)
                {
                    sortAttribute = c.GetType().GetProperty("Value").GetValue(c, null);
                    sortOrder = c.GetType().GetProperty("sortOrder").GetValue(c, null);
                }

                //For each XML node if value specified in query object add string to query input...
                foreach (XmlNode childNode in queryObjectNode.ChildNodes)
                {
                    if (sortAttribute != null)
                    {
                        string attributeName = sortAttribute.ToString();
                        if (childNode.Name.CompareTo(attributeName) == 0)
                        {
                            searchQuery.Append("\n Sort");
                            searchQuery.Append("=");
                            searchQuery.Append(childNode.InnerText.ToString());
                            searchQuery.Append(" ");
                            if (sortOrder != null)
                            {
                                string order = sortOrder.ToString();
                                if (childNode.Attributes["Order"] != null)
                                {
                                    string qboOder = childNode.Attributes["Order"].Value;
                                    if (order.CompareTo("Ascending") != 0)
                                    {
                                        string[] trimString = new string[] { "To" };
                                        string[] orderVal = qboOder.Split(trimString, StringSplitOptions.RemoveEmptyEntries);
                                        qboOder = orderVal[1] + "To" + orderVal[0];
                                    }
                                    searchQuery.Append(":" + qboOder);
                                }
                            }
                        }
                    }

                }
            }
            return searchQuery.ToString();
        }

        public void SetServiceTypeProperty(string realmId, ref PlatformSessionContext sessionContext)
        {
            if (sessionContext.IsRealmAPICalled == false)
            {
                bool isQBORealm = sessionContext.GetIsRealmQBO(sessionContext.AppDbId);
                sessionContext.IsRealmAPICalled = true;
                if (isQBORealm)
                {
                    sessionContext.ServiceType = IntuitServicesType.QBO;
                    if ( string.IsNullOrEmpty(sessionContext.QboBaseURI))
                    {
                        QBOService service = new QBOService();
                        QboUser user = service.User(ref sessionContext, realmId);

                        if (user != null)
                            sessionContext.QboBaseURI = user.CurrentCompany.BaseURI;
                    }
                }
                else
                    sessionContext.ServiceType = IntuitServicesType.QBD;
            }
        }
    }
}
