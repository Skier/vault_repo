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
using System.IO;
using System.Text;
using System.Net;
using Intuit.Sb.Cdm;
using Intuit.Platform.Client.Core;
using System.Diagnostics;

namespace Intuit.Platform.Client.Core.IDS
{
    public class IDSRestClient
    {
        private const string SLASH_CHAR = "/";
        private const string RESOURCE = "resource";
        private string m_HostURL;
        private string m_Version;

        public IDSRestClient(IntuitServicesType serviceType, PlatformSessionContext context)
        {
            
            switch (serviceType)
            {
                case IntuitServicesType.QBD:
                    m_Version = Properties.Settings.Default.IDSVersion;
                    if (string.IsNullOrEmpty(m_Version))
                    {
                        m_Version = "v2";
                    }
                    m_HostURL = Properties.Settings.Default.IDSUrl;
                    break;
                case IntuitServicesType.QBO:
                    m_Version = Properties.Settings.Default.QBOVersion;
                    if (string.IsNullOrEmpty(m_Version))
                    {
                        m_Version = "v2";
                    }

                    if (!string.IsNullOrEmpty(context.QboBaseURI))
                    {
                        m_HostURL = context.QboBaseURI + SLASH_CHAR + RESOURCE + SLASH_CHAR;
                    }
                    else
                    {
                        m_HostURL = Properties.Settings.Default.QBOUrl;
                    }
                    break;
            }
        }

        public string Send(PlatformSessionContext context, Verb verb, IDSOperationContext entityDesc, string requestPayload)
        {
            //FIXME: The offering ID in the operation context must always be -1 in IDS 6.2
            // This should work but does not.
            //entityDesc.OfferingId = offeringId.ipp.ToString();

            Uri address = BuildURI(verb, entityDesc, context.ServiceType);

            string responsePayload = string.Empty;

            HttpWebRequest request = Intuit.Common.Util.HttpRequestHelper.CreateHttpRequest(address);
            request.Method = verb.ToString();
            if (verb == Verb.POST)
            {
                request.ContentType = entityDesc.ContentType;
            }

            context.AddRESTAuthorization(request, entityDesc.RealmId);

            if (verb == Verb.POST)
            {
                using (Stream postStream = request.GetRequestStream())
                {
                    using (StreamWriter writer = new StreamWriter(postStream))
                    {
                        writer.Write(requestPayload);
                    }
                }
            }
            HttpWebResponse response = null;
            try
            {
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responsePayload = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException we)
            {
                HttpWebResponse errorResponse = (HttpWebResponse)we.Response;
                int statusCode = (int)errorResponse.StatusCode;
                string statusCodeDescription = errorResponse.StatusCode.ToString();
                StreamReader reader = new StreamReader(errorResponse.GetResponseStream());
                string errorString = reader.ReadToEnd();

                

                try
                {
                    //To handle QuickBooks Online Exceptions
                    ObjectSerializer serialize = new ObjectSerializer();
                    FaultInfo error = (FaultInfo)serialize.DeserializeObject(errorString, typeof(FaultInfo));

                    Logger.WriteToLog(TraceLevel.Info, "Error message: " + error.Message);
                    
                    throw new IDSException(null, string.Format("Error Code:{0}, Error Message:{1}, " +
                                                                "Error Cause: {2}", error.ErrorCode,
                                                                error.Message, error.Cause), we);
                }
                catch (InvalidOperationException)
                {
                    Logger.WriteToLog(TraceLevel.Info, "Error message: " + errorString);
                    throw new IDSException(null, string.Format("Error Code:{0}, Error Code Description:{1}, " +
                                                                "Error Description: {2}", statusCode.ToString(),
                                                                statusCodeDescription, errorString), we);
                }
            }
            catch (Exception ex)
            {
                string errorString = string.Empty;
                HttpStatusCode statusCode = HttpStatusCode.OK;
                try
                {
                    statusCode = response.StatusCode;
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    errorString = reader.ReadToEnd();
                    Logger.WriteToLog(TraceLevel.Info, "Error message: " + errorString);
                }
                catch (Exception e2)
                {
                    Logger.WriteToLog(TraceLevel.Info, "Error message: " + e2.Message);
                    throw e2;
                }

                throw new IDSException(null, string.Format("Error Code:{0}, Error Code Description:{1}, " +
                                                            "Error Description: {2}", (int)statusCode,
                                                            statusCode.ToString(), errorString), ex);
            }
            return responsePayload;
        }

        /// <exception cref="ArgumentException">IDSOperationContext with valid IDS Resource is required</exception>
        private Uri BuildURI(Verb verb, IDSOperationContext entityDesc, IntuitServicesType serviceType)
        {
            StringBuilder urlBuffer = new StringBuilder();
            urlBuffer.Append(m_HostURL);

            if (entityDesc.Resource == IDSResource.none)
            {
                throw new ArgumentException("IDSOperationContext with valid IDS Resource is required", "entityDesc");
            }

            if (entityDesc.Resource == IDSResource.user)
            {
                urlBuffer.Replace("resource", "rest");
            }

            //Change the IDSResource Enum value to add - in it as QBO requires it.
            string resourceString = entityDesc.Resource.ToString();

            if (serviceType == IntuitServicesType.QBD && entityDesc.Resource == IDSResource.salesterm)
            {
                resourceString = "term";
            }
            if (serviceType == IntuitServicesType.QBO)
            {
                switch (entityDesc.Resource)
                {
                    case IDSResource.billpayment:
                        resourceString = "bill-payment";
                        break;
                    case IDSResource.billpayments:
                        resourceString = "bill-payments";
                        break;
                    case IDSResource.creditcardcharge:
                        resourceString = "credit-card-charge";
                        break;
                    case IDSResource.creditcardcharges:
                        resourceString = "credit-card-charges";
                        break;
                    case IDSResource.paymentmethod:
                        resourceString = "payment-method";
                        break;
                    case IDSResource.paymentmethods:
                        resourceString = "payment-methods";
                        break;
                    case IDSResource.salesterm:
                        resourceString = "sales-term";
                        break;
                    case IDSResource.salesterms:
                        resourceString = "sales-terms";
                        break;
                    case IDSResource.cashpurchase:
                        resourceString = "cash-purchase";
                        break;
                    case IDSResource.cashpurchases:
                        resourceString = "cash-purchases";
                        break;
                    case IDSResource.salesreceipt:
                        resourceString = "sales-receipt";
                        break;
                    case IDSResource.salesreceipts:
                        resourceString = "sales-receipts";
                        break;
                    default:
                        resourceString = entityDesc.Resource.ToString();
                        break;
                }
            }

            //here tolower added because Rest Url requires resource name in all lowercase
            //And Resource Class if written in lower case throws error as class is keyword 
            //in .net.
            //so added 'Class' in IDSResource and applying tolower() to change it to lowercase.
            urlBuffer.Append(resourceString.ToLower());
            urlBuffer.Append(SLASH_CHAR);
            urlBuffer.Append(m_Version);

            string[] companyParameters = entityDesc.CompanyParameters;
            if (companyParameters != null)
            {
                for (int nIndex = 0; nIndex < companyParameters.Length; nIndex++)
                {
                    urlBuffer.Append(SLASH_CHAR);
                    urlBuffer.Append(companyParameters[nIndex]);
                }
            }

            String realmId = entityDesc.RealmId;
            if (realmId != null)
            {
                urlBuffer.Append(SLASH_CHAR);
                urlBuffer.Append(realmId);
            }

            // Handling id via URL (for special POST operations like deletes)
            string id = entityDesc.EntityId;
            if (id != null)
            {
                urlBuffer.Append(SLASH_CHAR);
                urlBuffer.Append(id);
            }

            // Handling roles
            string offeringId = entityDesc.OfferingId;
            if (offeringId != null)
            {
                urlBuffer.Append(SLASH_CHAR);
                urlBuffer.Append(offeringId);
            }

            string users = entityDesc.Users;
            if (users != null)
            {
                urlBuffer.Append(SLASH_CHAR);
                urlBuffer.Append(users);
            }

            string roleCommand = entityDesc.RoleCommand;
            if (roleCommand != null)
            {
                urlBuffer.Append(SLASH_CHAR);
                urlBuffer.Append(roleCommand);
            }


            string[] parameters = entityDesc.Parameters;
            if (parameters != null)
            {
                // For Verb.GET, payload is appended to the end point URL
                urlBuffer.Append(SLASH_CHAR);
                urlBuffer.Append(BuildStringToAppend(parameters));
            }

            //Write created URL to log File.
            Logger.WriteToLog(TraceLevel.Info, "Address: " + urlBuffer.ToString());

            Uri endPointURL = new Uri(urlBuffer.ToString());
            return endPointURL;
        }

        private static string BuildStringToAppend(IEnumerable<string> parameters)
        {
            StringBuilder paramStr = new StringBuilder();
            foreach (var parameter in parameters)
            {
                if (paramStr.Length > 0)
                {
                    paramStr.Append(',');
                }
                paramStr.Append(parameter);
            }
            return paramStr.ToString();
        }

        private string BuildPayload(string payload, IEnumerable<string> parameters)
        {
            string param = BuildParameter(parameters);
            const string namespaces = "xmlns=\"http://www.intuit.com/sb/cdm/v2/xmlrequest\"";
            param = "<" + payload + " " + namespaces + ">" + param + "</" + payload + ">";
            return param;
        }

        private static string BuildParameter(IEnumerable<string> parameters)
        {
            StringBuilder paramStr = new StringBuilder();
            foreach (var parameter in parameters)
            {
                paramStr.Append(parameter);
            }
            return paramStr.ToString();
        }
    }
}