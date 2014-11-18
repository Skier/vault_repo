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
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using Intuit.Common.Util;
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
    /// <summary>
    /// Abstracts the context of a session with the platform (QuickBase or Workplace), as defined by the combination of a host, an application or developer (apptoken/devkey), and an authenticated user.
    /// </summary>
    public class PlatformSessionContext : INotifyPropertyChanged
    {

        #region Convenience constants

        /// <summary>
        /// QuickBase (possibly Workplace too) restriction
        /// </summary>
        public const int MaxLenAppName = 225;

        /// <summary>
        /// DoQuery only supports up to 100 criteria
        /// </summary>
        public const int MaxQueryParameters = 100;

        private const string ForbiddenAppName = "QuickBase FAQ"; // Do NOT localize

        /// <summary>
        /// Use this as the clist if you want to have all columns of a table, not just the default view columns
        /// </summary>
        public const string CListAll = "a";

        // ReSharper disable InconsistentNaming
        //TODO: Add all the other operators supported by QuickBase/Workplace
        /// <summary>
        /// "EX": exact match
        /// </summary>
        public const string QueryComparisonOp_EX = "EX";

        /// <summary>
        /// Logical OR
        /// </summary>
        public const string QueryCriteriaCombinationOp_OR = "OR";

        /// <summary>
        /// Logical AND
        /// </summary>
        public const string QueryCriteriaCombinationOp_AND = "AND";

        /// <summary>
        /// The value of a field of type "CheckBox" when it's checked aka true.
        /// </summary>
        public const string CheckBoxChecked = "1";

        /// <summary>
        /// The value of a field of type "CheckBox" when it's unchecked aka false.
        /// </summary>
        public const string CheckBoxUnchecked = "0";

        #endregion

        #region Private fields

        private string m_AppToken;
        private IPlatformHost m_Host;
        private string m_Password;
        private string m_Ticket;
        private string m_UserId;
        private string m_PlatformRequestLoggingToDiskFilePrefix = Resources.PlatformSessionContext_m_PlatformRequestLoggingToDiskFilePrefix_c__platformclient_;
        private bool m_LogPlatformRequestsToDisk;
        private bool m_LogDiagnosticDetailsOnRequestErrorsToNotifier;
        private IRequestAuthorizer m_RequestAuthorizer;
        private string m_QboBaseURI;
        private IntuitServicesType m_ServiceType;
        private string m_AppDbId;
        private bool m_IsRealmAPICalled = false;
        #endregion

        #region Setup

        /// <summary>
        /// New context for API calls. Defaults to <see cref="PlatformHost.WorkPlaceSecure"/> as the <see cref="Host"/>.
        /// </summary>
        /// <returns></returns>
        public PlatformSessionContext()
        {
            Host = PlatformHost.WorkPlaceSecure;
        }

        /// <summary>
        /// Creates the platform session context.
        /// </summary>
        /// <param name="host">The host to connect to.</param>
        /// <param name="userId">The user id for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        /// <returns></returns>
        public static PlatformSessionContext Create(IPlatformHost host, string userId, string password)
        {
            if (host == null || userId == null || password == null)
                return null;

            return new PlatformSessionContext(host, userId, password);
        }

        /// <summary>
        /// Creates the platform session context.
        /// </summary>
        /// <param name="appToken">The app token.</param>
        /// <param name="appDBId">The app DB id.</param>
        /// <param name="host">The host to connect to.</param>
        /// <param name="ticket">The ticket for connection.</param>
        /// <returns></returns>
        public static PlatformSessionContext Create(string appToken, string appDBId, IPlatformHost host, string ticket)
        {
            if (appToken == null || appDBId == null || host == null || ticket == null)
                return null;

            return new PlatformSessionContext(appToken, appDBId, host, ticket);
        }

        /// <summary>
        /// Creates the platform session context.
        /// </summary>
        /// <param name="appToken">The app token.</param>
        /// <param name="appDBId">The app DB id.</param>
        /// <param name="host">The host to connect to.</param>
        /// <param name="userId">The user id for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        /// <returns></returns>
        public static PlatformSessionContext Create(string appToken, string appDBId, IPlatformHost host, string userId, string password)
        {
            if (appToken == null || appDBId == null || host == null || userId == null || password == null)
                return null;

            return new PlatformSessionContext(appToken, appDBId, host, userId, password);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSessionContext"/> class.
        /// </summary>
        /// <param name="appToken">The app token.</param>
        /// <param name="appDBId">The app DB id.</param>
        /// <param name="host">The host to connect to.</param>
        /// <param name="ticket">The ticket for connection.</param>
        private PlatformSessionContext(string appToken, string appDBId, IPlatformHost host, string ticket)
        {
            if (appDBId != m_AppDbId)
            {
                m_AppDbId = appDBId;
                OnPropertyChanged("AppDbId");
            }

            if (appToken != m_AppToken)
            {
                m_AppToken = appToken;
                OnPropertyChanged("AppToken");
            }

            if (host != m_Host)
            {
                if (host == null)
                {
                    throw new ArgumentNullException("value", Resources.PlatformClientException_HostMustNotBeNull_Host_must_not_be_null);
                }
                OnBeforeHostChange();
                m_Host = host;
                OnPropertyChanged("Host");
                Ticket = null;
            }

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSessionContext"/> class.
        /// </summary>
        /// <param name="appToken">The app token.</param>
        /// <param name="appDBId">The app DB id.</param>
        /// <param name="host">The host to connect to.</param>
        /// <param name="userId">The user id for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        private PlatformSessionContext(string appToken, string appDBId, IPlatformHost host, string userId, string password)
        {
            if (appToken != m_AppToken)
            {
                m_AppToken = appToken;
                OnPropertyChanged("AppToken");
            }

            if (appDBId != m_AppDbId)
            {
                m_AppDbId = appDBId;
                OnPropertyChanged("AppDbId");
            }

            if (host != m_Host)
            {
                if (host == null)
                {
                    throw new ArgumentNullException("value", Resources.PlatformClientException_HostMustNotBeNull_Host_must_not_be_null);
                }
                OnBeforeHostChange();
                m_Host = host;
                OnPropertyChanged("Host");
                Ticket = null;
            }

            if (password != m_Password)
            {
                m_Password = password;
                OnPropertyChanged("Password");
            }

            if (userId != m_UserId)
            {
                m_UserId = userId;
                OnPropertyChanged("UserID");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSessionContext"/> class.
        /// </summary>
        /// <param name="host">The host to connect to.</param>
        /// <param name="userId">The user id for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        private PlatformSessionContext(IPlatformHost host, string userId, string password)
        {
            if (host != m_Host)
            {
                if (host == null)
                {
                    throw new ArgumentNullException("value", Resources.PlatformClientException_HostMustNotBeNull_Host_must_not_be_null);
                }
                OnBeforeHostChange();
                m_Host = host;
                OnPropertyChanged("Host");
                Ticket = null;
            }

            if (password != m_Password)
            {
                m_Password = password;
                OnPropertyChanged("Password");
            }

            if (userId != m_UserId)
            {
                m_UserId = userId;
                OnPropertyChanged("UserID");
            }
        }

        /// <summary>
        /// If you're databinding some properties of this class to UI,
        /// make sure to set SyncInvoke to your Form or the Control object.
        /// This way, if any of the properties change on a different thread,
        /// your UI will be notified of the changes on its own thread.
        /// This object is use by most code in this assembly to synchronize with the UI thread.
        /// </summary>
        public static ISynchronizeInvoke SyncInvoke { get; set; }

        /// <summary>
        /// Set this if you want to get notifications from inside the client
        /// </summary>
        public WorkNotification WorkNotification { get; set; }

        /// <summary>
        /// The host to send API requests to
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public IPlatformHost Host
        {
            get
            {
                return m_Host;
            }
            set
            {
                if (value != m_Host)
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("value", Resources.PlatformClientException_HostMustNotBeNull_Host_must_not_be_null);
                    }
                    OnBeforeHostChange();
                    m_Host = value;
                    OnPropertyChanged("Host");
                    Ticket = null;
                }
            }
        }

        /// <summary>
        /// The hostname this context sends API requests to. Taken from <see cref="Host"/>.
        /// </summary>
        /// <returns></returns>
        public string GetHostname()
        {
            if (Host != null)
            {
                return Host.Hostname;
            }
            return null;
        }

        /// <summary>
        /// Sets the <see cref="Host"/> property indirectly by setting the hostname. Will try to guess if it's one of the <see cref="PlatformHost.KnownHosts"/>, otherwise instantiates a <see cref="CustomPlatformHost"/>.
        /// </summary>
        public void SetHostname(string hostname)
        {
            if (hostname != null && Host.Hostname != hostname)
            {
                Host = PlatformHost.GuessFromHostname(hostname) ?? new CustomPlatformHost(hostname, true, hostname);
            }
        }


        /// <summary>
        /// The DevKey or AppToken for the application
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string AppToken
        {
            private get
            {
                return m_AppToken;
            }
            set
            {
                if (value != m_AppToken)
                {
                    m_AppToken = value;
                    OnPropertyChanged("AppToken");
                }
            }
        }

        /// <summary>
        /// See if the AppToken is set on this context.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public bool HasAppToken
        {
            get
            {
                return !String.IsNullOrEmpty(m_AppToken);
            }
        }

        /// <summary>
        /// The DevKey or AppToken for the application
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string AppDbId
        {
            get
            {
                return m_AppDbId;
            }
            set
            {
                if (value != m_AppDbId)
                {
                    m_AppDbId = value;
                    OnPropertyChanged("AppDbId");
                }
            }
        }

        /// <summary>
        /// See if the AppToken is set on this context.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public bool HasAppDbId
        {
            get
            {
                return !String.IsNullOrEmpty(m_AppDbId);
            }
        }

        /// <summary>
        /// Check to which Intuit service SDK connects to.
        /// Either QBD or QBO
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public IntuitServicesType ServiceType
        {
            get
            {
                return m_ServiceType;
            }
            set
            {
                if (value != m_ServiceType)
                {
                    m_ServiceType = value;
                    OnPropertyChanged("ServiceType");
                }
            }
        }
        /// <summary>
        /// Check to which Intuit service SDK connects to.
        /// Either QBD or QBO
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public bool IsRealmAPICalled
        {
            get
            {
                return m_IsRealmAPICalled;
            }
            set
            {
                if (value != m_IsRealmAPICalled)
                {
                    m_IsRealmAPICalled = value;
                    OnPropertyChanged("IsRealmAPICalled");
                }
            }
        }

        #endregion

        #region Request logging

        /// <summary>
        /// Whether or not to log API requests and responses to the local file system.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public bool LogPlatformRequestsToDisk
        {
            get
            {
                return m_LogPlatformRequestsToDisk;
            }
            set
            {
                if (value != m_LogPlatformRequestsToDisk)
                {
                    m_LogPlatformRequestsToDisk = value;
                    OnPropertyChanged("LogPlatformRequestsToDisk");
                }
            }
        }

        /// <summary>
        /// Whether or not to log diagnostic details of API calls to the provided <see cref="WorkNotification"/>.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public bool LogDiagnosticDetailsOnRequestErrorsToNotifier
        {
            get
            {
                return m_LogDiagnosticDetailsOnRequestErrorsToNotifier;
            }
            set
            {
                if (value != m_LogDiagnosticDetailsOnRequestErrorsToNotifier)
                {
                    m_LogDiagnosticDetailsOnRequestErrorsToNotifier = value;
                    OnPropertyChanged("LogDiagnosticDetailsOnRequestErrorsToNotifier");
                }
            }
        }

        /// <summary>
        /// Desired prefix for files created on file system while logging API requests and responses (if <see cref="LogPlatformRequestsToDisk"/> is set to true).
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string PlatformRequestLoggingToDiskFilePrefix
        {
            get
            {
                return m_PlatformRequestLoggingToDiskFilePrefix;
            }
            set
            {
                if (value != m_PlatformRequestLoggingToDiskFilePrefix)
                {
                    m_PlatformRequestLoggingToDiskFilePrefix = value;
                    OnPropertyChanged("PlatformRequestLoggingToDiskFilePrefix");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        /// <summary>
        /// Fires whenever a property of this class changes, part of the <see cref="INotifyPropertyChanged"/> implementation to aid with data binding.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Part of <see cref="INotifyPropertyChanged"/> implementation, called by properties in this class when they change, will fire the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">name of the property of this class that changed</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                if (SyncInvoke != null && SyncInvoke.InvokeRequired)
                {
                    SyncInvoke.Invoke(PropertyChanged, new object[] { this, new PropertyChangedEventArgs(propertyName) });
                }
                else
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            switch (propertyName)
            {
                case "Host":
                    OnAfterHostChange();
                    Ticket = null;
                    break;
                case "Password":
                case "UserID":
                    Ticket = null;
                    break;
            }
        }

        /// <summary>
        /// Propagates changes happening inside a CustomPlatformHost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyOfHostChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Host");
        }

        private void OnAfterHostChange()
        {
            // see if we need to connect to a new CustomPlatformHost
            CustomPlatformHost customHost = m_Host as CustomPlatformHost;
            if (customHost != null)
            {
                customHost.PropertyChanged += PropertyOfHostChanged;
            }
        }

        private void OnBeforeHostChange()
        {
            // see if we need to disconnect from a prior CustomPlatformHost
            CustomPlatformHost customHost = m_Host as CustomPlatformHost;
            if (customHost != null)
            {
                customHost.PropertyChanged -= PropertyOfHostChanged;
            }
        }

        #endregion

        #region Authentication and authorization

        /// <summary>
        /// When using QBO, User API will set BASE URI which is URL fro correct QBO cluster
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string QboBaseURI
        {
            get
            {
                return m_QboBaseURI;
            }
            set
            {
                if (value != m_QboBaseURI)
                {
                    m_QboBaseURI = value;
                    OnPropertyChanged("QboBaseURI");
                }
            }
        }

        /// <summary>
        /// When using API_Authenticate instead of OAuth, the user's Workplace/QuickBase account password. OBSOLETE for Workplace, use OAuth instead!
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string UserID
        {
            get
            {
                return m_UserId;
            }
            set
            {
                if (value != m_UserId)
                {
                    m_UserId = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        /// <summary>
        /// When using API_Authenticate instead of OAuth, set this to the user's Workplace/QuickBase account password. OBSOLETE for Workplace, use OAuth instead!
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                if (value != m_Password)
                {
                    m_Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        /// <summary>
        /// The ticket given to context after API_Authenticate (when not using OAuth). 
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string Ticket
        {
            get
            {
                return m_Ticket;
            }
            set
            {
                if (value != m_Ticket)
                {
                    m_Ticket = value;
                    OnPropertyChanged("Ticket");
                    OnPropertyChanged("HasTicket");
                }
            }
        }

        /// <summary>
        /// Whether or not we're authenticated so we can make API calls
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true), Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public bool HasTicket
        {
            get
            {
                return !String.IsNullOrEmpty(Ticket);
            }
        }

        ///<summary>
        ///Set to an implementation instance of IRequestAuthorizer to use other authentication mechanisms, such as OAuth.
        ///</summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true), Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public IRequestAuthorizer RequestAuthorizer
        {
            get
            {
                return m_RequestAuthorizer;
            }
            set
            {
                if (m_RequestAuthorizer != value)
                {
                    m_RequestAuthorizer = value;
                    if (m_RequestAuthorizer != null)
                    {
                        m_RequestAuthorizer.Initialize(AppToken);
                    }
                    OnPropertyChanged("RequestAuthorizer");
                }
            }
        }

        /// <summary>
        /// The Workplace cookie extracted from the last API call
        /// </summary>
        private string WorkplaceCookie { get; set; }

        /// <summary>
        /// Force an authentication, instead of just doing it on-demand on the next API call.
        /// </summary>
        public void ForceAuthentication()
        {
            GetTicketAndAuthenticateIfNeeded();
        }

        private string GetTicketAndAuthenticateIfNeeded()
        {
            return Ticket ?? (Ticket = Authenticate());
        }

        /// <summary>
        /// Adds a request header with authorization information.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="realmId"></param>
        public void AddRESTAuthorization(HttpWebRequest request, string realmId)
        {
            if (RequestAuthorizer != null)
            {
                RequestAuthorizer.Authorize(request, null);
            }
            else
            {
                string authHeader = "INTUITAUTH intuit-app-token=\"" + AppToken + "\"";
                authHeader += ",intuit-token=\"" + Ticket + "\"";

                if (realmId == null || WorkplaceCookie != null)
                {
                    // Only one call is supported without realm, use qbn cookies in this context
                    request.Headers.Add("Cookie", WorkplaceCookie);
                }
                else
                {
                    // Set intuit-token
                    authHeader += ",intuit-token=\"" + Ticket + "\"";
                }
                request.Headers.Add("Authorization", authHeader);
            }
        }

        private PlatformApiXmlHttpPostRequest CreateAuthenticatedApiRequest(string apiAction, string dbid, bool appTokenNotRequired)
        {
            PlatformApiXmlHttpPostRequest request = CreateRawApiRequest(apiAction, dbid);
            if (RequestAuthorizer != null)
            {
                RequestAuthorizer.Authorize(request.Request, null);
            }
            else
            {
                request.ReqDoc.AddTextParameter("ticket", GetTicketAndAuthenticateIfNeeded());
            }
            if (!appTokenNotRequired && !String.IsNullOrEmpty(AppToken))
            {
                request.ReqDoc.AddTextParameter("devkey", AppToken);
                request.ReqDoc.AddTextParameter("apptoken", AppToken);
            }
            return request;
        }

        private PlatformApiXmlHttpPostRequest CreateRawApiRequest(string apiAction, string dbid)
        {
            if (String.IsNullOrEmpty(dbid))
            {
                throw new ArgumentException(Resources.PlatformClientException_MustSpecifyValidDBID_Must_provide_a_valid_dbid, "dbid");
            }
            if (String.IsNullOrEmpty(apiAction))
            {
                throw new ArgumentException(Resources.PlatformClientException_MustProvideAValidApiAction_Must_provide_a_valid_apiAction, "apiAction");
            }
            var req = new PlatformApiXmlHttpPostRequest(Host, dbid, apiAction);
            if (LogPlatformRequestsToDisk)
            {
                req.LogApiRequest += PlatformApiXmlHttpPostRequest.CreateDiskLogger(PlatformRequestLoggingToDiskFilePrefix);
            }
            req.TrackRequestDetailsForErrorLogging = LogDiagnosticDetailsOnRequestErrorsToNotifier;
            return req;
        }

        /// <summary>
        /// OBSOLETE for Workplace, use OAuth instead!
        /// </summary>
        /// <returns>the new ticket</returns>
        private string Authenticate()
        {
            PlatformApiXmlHttpPostRequest request = CreateRawApiRequest("API_Authenticate", "main");
            request.ReqDoc.AddTextParameter("username", UserID);
            request.ReqDoc.AddTextParameter("password", Password);

            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            WorkplaceCookie = request.WorkplaceCookie;
            XmlNode ticket = respXml.SelectSingleNode("//ticket");
            if (ticket != null)
            {
                return ticket.InnerText;
            }
            throw new PlatformClientException(m_Host, Resources.PlatformClientException_ExceptionNoSessionTicket_Could_not_get_session_ticket);
        }

        /// <summary>
        /// End authenticated session. Used for ticket-based authentication only.
        /// </summary>
        public void SignOut()
        {
            // There's no point in sending a sign out request if we don't have a ticket to send. We're not using cookies here.
            if (Ticket != null)
            {
                // ticket is an optional parameter for this call which we actually need since we're not carrying a cookie with us
                PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_SignOut", "main", true);
                request.ExecuteRequest(WorkNotification);
                Ticket = null;
            }
        }

        #endregion

        #region Filter helpers

        /// <summary>
        /// Builds a filter that filters DBs by whether their <see cref="DbInfo.PrettyName"/> contains <paramref name="dbNameMustContain"/>.
        /// </summary>
        /// <param name="dbNameMustContain">the string the instances's name must contain</param>
        /// <returns>a filter object you can use calls to <see cref="GetGrantedDBs(DbInfo.FilterDbInfo , bool , bool , bool , string , bool , bool )"/></returns>
        public static DbInfo.FilterDbInfo GetNameMustContainFilter(string dbNameMustContain)
        {
            return dbInfo => dbInfo.PrettyName.Contains(dbNameMustContain);
        }

        /// <summary>
        /// Builds a filter that filters DBs by whether their <see cref="DbInfo.PrettyName"/> starts with <paramref name="dbNameMustStartWith"/>.
        /// </summary>
        /// <param name="dbNameMustStartWith">the string the instances's name must start with</param>
        /// <returns>a filter object you can use calls to <see cref="GetGrantedDBs(DbInfo.FilterDbInfo , bool , bool , bool , string , bool , bool )"/></returns>
        public static DbInfo.FilterDbInfo GetNameMustStartWithFilter(string dbNameMustStartWith)
        {
            return dbInfo => dbInfo.PrettyName.StartsWith(dbNameMustStartWith);
        }

        /// <summary>
        /// Builds a filter that filters DBs by whether their <see cref="DbInfo.PrettyName"/> ends with <paramref name="dbNameMustEndWith"/>.
        /// </summary>
        /// <param name="dbNameMustEndWith">the string the instances's name must end with</param>
        /// <returns>a filter object you can use calls to <see cref="GetGrantedDBs(DbInfo.FilterDbInfo , bool , bool , bool , string , bool , bool )"/></returns>
        public static DbInfo.FilterDbInfo GetNameMustEndWithFilter(string dbNameMustEndWith)
        {
            return dbInfo => dbInfo.PrettyName.EndsWith(dbNameMustEndWith);
        }

        #endregion

        #region API calls

        /// <summary>
        /// Check if Realm is QBO realm or not.
        /// </summary>
        /// <param name="dbid">The dbid.</param>
        /// <returns>Returns true if realm attached to this application is QBO realm; Otherwise false.</returns>
        public bool GetIsRealmQBO(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetIsRealmQBO", dbid, false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            bool isRealmQBO = bool.Parse(respXml.SelectSingleNode("//IsQBO").InnerText);
            return isRealmQBO;

        }


        /// <summary>
        /// Gets the record info.
        /// </summary>
        /// <param name="recordId">The record id.</param>
        /// <param name="dbid">The dbid.</param>
        /// <returns></returns>
        public RecordSet GetRecordInfo(string recordId, string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetRecordInfo", dbid, false);
            request.ReqDoc.AddTextParameter("rid", recordId);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            return RecordSet.ParseQueryResponse(respXml.ChildNodes[1]);
        }

        /// <summary>
        /// Adds the field to the Table.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldType">Type of the field.</param>
        /// <param name="dbid">The Table dbid to which Fields to add.</param>
        /// <returns></returns>
        public FieldInfo AddField(string fieldName, string fieldType, string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_AddField", dbid, false);
            request.ReqDoc.AddTextParameter("label", fieldName);
            request.ReqDoc.AddTextParameter("type", fieldType);

            XmlDocument respXml = request.ExecuteRequest(WorkNotification);

            FieldInfo retVal = new FieldInfo(respXml.ChildNodes[1]);

            return retVal;
        }

        /// <summary>
        /// Requests the service provider id.
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="inboundGatewayURL">The inbound gateway URL.</param>
        /// <param name="certFileBody">The cert file body.</param>
        /// <param name="certFilename">The cert filename.</param>
        /// <param name="dbid">The dbid.</param>
        /// <returns></returns>
        public FederationInfo RequestServiceProviderId(string companyName, string inboundGatewayURL, string certFileBody, string certFilename, string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_RequestServiceProviderID", dbid, false);
            request.ReqDoc.AddTextParameter("companyName", companyName);
            request.ReqDoc.AddTextParameter("inboundGatewayURL", inboundGatewayURL);
            request.ReqDoc.AddTextParameter("certFileBody", certFileBody);
            request.ReqDoc.AddTextParameter("certFilename", certFilename);

            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            FederationInfo retval = new FederationInfo(respXml);
            return retval;

        }
        /// <summary>
        /// Sets the app federation info.
        /// </summary>
        /// <param name="appFederatedInfo">The app federated info.</param>
        /// <param name="dbid">The dbid.</param>
        public void SetAppFederationInfo(FederationInfo appFederatedInfo, string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_SetAppFederationInfo", dbid, false);
            request.ReqDoc.AddTextParameter("appDestinationURL", appFederatedInfo.AppDestinationURL);
            request.ReqDoc.AddTextParameter("appDestinationURLVarReplaced", appFederatedInfo.AppDestinationURLVarReplaced);
            request.ReqDoc.AddTextParameter("userManagementURL", appFederatedInfo.UserManagementURL);
            request.ReqDoc.AddTextParameter("serviceProviderId", appFederatedInfo.ServiceProviderId);
            request.ReqDoc.AddTextParameter("customCancelAppURL", appFederatedInfo.CustomCancelAppURL);

            request.ReqDoc.AddTextParameter("usesIDS", appFederatedInfo.UsesIDS ? "1" : "0");
            request.ReqDoc.AddTextParameter("externallyHosted", appFederatedInfo.ExternallyHosted ? "1" : "0");
            request.ReqDoc.AddTextParameter("usesIDS_ShowEnterDataDirectly", appFederatedInfo.UsesIDS_ShowEnterDataDirectly ? "1" : "0");
            request.ReqDoc.AddTextParameter("usesIDS_ShowQBooksDTData", appFederatedInfo.UsesIDS_ShowQBooksDTData ? "1" : "0");
            request.ReqDoc.AddTextParameter("usesIDS_ShowQBOData", appFederatedInfo.UsesIDS_ShowQBOData ? "1" : "0");
            request.ReqDoc.AddTextParameter("usesIDS_ShowQBooksDTSampleData", appFederatedInfo.UsesIDS_ShowQBooksDTSampleData ? "1" : "0");

            XmlDocument respXml = request.ExecuteRequest(WorkNotification);

            return;
        }
        /// <summary>
        /// Gets the app federation info.
        /// </summary>
        /// <param name="dbid">The dbid.</param>
        /// <returns>FederationInfo of application</returns>
        public FederationInfo GetAppFederationInfo(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetAppFederationInfo", dbid, false);

            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            FederationInfo retval = new FederationInfo(respXml);
            return retval;
        }
        /// <summary>
        /// Convenience function for <see cref="GetGrantedDBs(DbInfo.FilterDbInfo , bool , bool , bool , string , bool , bool )"/>.
        /// </summary>
        public IList<DbInfo> GetGrantedDBs(bool excludeParents, bool adminOnly, bool withEmbeddedTables, string dbNameMustContain)
        {
            return GetGrantedDBs(excludeParents, adminOnly, withEmbeddedTables, dbNameMustContain, null, false, false);
        }

        /// <summary>
        /// Convenience function for <see cref="GetGrantedDBs(DbInfo.FilterDbInfo , bool , bool , bool , string , bool , bool )"/>.
        /// </summary>
        public IList<DbInfo> GetGrantedDBs(bool excludeParents, bool adminOnly, bool withEmbeddedTables, string dbNameMustContain, string realm, bool showAppData, bool includeAncestors)
        {
            DbInfo.FilterDbInfo filter = null;
            if (dbNameMustContain != null)
            {
                filter = GetNameMustContainFilter(dbNameMustContain);
            }
            return GetGrantedDBs(filter, excludeParents, adminOnly, withEmbeddedTables, realm, showAppData, includeAncestors);
        }

        /// <summary>
        /// Get's the list of instances and tables the user has access to.
        /// </summary>
        /// <param name="filterFunction">a filter applied to the list of DBs returned by API_GrantedDBs</param>
        /// <param name="excludeParents">exclude the parent table/instance</param>
        /// <param name="adminOnly">only the ones where the user has an admin-level role</param>
        /// <param name="withEmbeddedTables">include (child) tables</param>
        /// <param name="realm">limit instances to the ones associated with given realm</param>
        /// <param name="showAppData">return the application data</param>
        /// <param name="includeAncestors">includeAncestors</param>
        /// <returns>a (filtered) list of instances and/or tables</returns>
        public IList<DbInfo> GetGrantedDBs(DbInfo.FilterDbInfo filterFunction, bool excludeParents, bool adminOnly, bool withEmbeddedTables, string realm, bool showAppData, bool includeAncestors)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GrantedDBs", "main", false);
            request.ReqDoc.AddTextParameter("Excludeparents", excludeParents ? "1" : "0");
            request.ReqDoc.AddTextParameter("adminOnly", adminOnly ? "1" : "0");
            request.ReqDoc.AddTextParameter("withembeddedtables", withEmbeddedTables ? "1" : "0");
            if (includeAncestors)
            {
                request.ReqDoc.AddTextParameter("includeancestors", "1");
            }
            if (!String.IsNullOrEmpty(realm))
            {
                request.ReqDoc.AddTextParameter("realm", realm);
            }
            if (showAppData)
            {
                request.ReqDoc.AddTextParameter("ShowAppData", "true");
            }

            XmlDocument respXml = request.ExecuteRequest(WorkNotification);

            List<DbInfo> retval = DbInfo.ParseGetGrantedDBs(respXml, filterFunction);
            if (retval == null)
            {
                throw new PlatformClientException(m_Host, Resources.PlatformClientException_ExceptionUnableToGetGrantedDBs_Could_not_get_list_of_granted_databases);
            }
            return retval;
        }

        /// <summary>
        /// Convenience function for <see cref="GetSchema(string, bool)"/>, retrieves the schema for the given instance/table and all sub-tables
        /// </summary>
        /// <param name="dbid">instance or table you want the schema for</param>
        /// <returns>the schema, recursively retrieved for all table structures</returns>
        public Schema GetSchema(IDbid dbid)
        {
            return GetSchema(dbid.Dbid, true);
        }

        /// <summary>
        /// Gets the schema for the given instance or table, and retrieves (child-)tables' schemas recursively if <paramref name="recursive"/> is true.
        /// </summary>
        public Schema GetSchema(string dbidToGetSchemaFor, bool recursive)
        {
            XmlDocument respXml = GetSchemaXml(dbidToGetSchemaFor);

            return GetSchema(dbidToGetSchemaFor, respXml, recursive);
        }

        private Schema GetSchema(string dbidToGetSchemaFor, XmlNode schemaXml, bool recursive)
        {
            Schema schema = new Schema(dbidToGetSchemaFor, schemaXml);
            if (recursive)
            {
                schema.LoadMissingChildSchemas(GetSchema);
            }
            return schema;
        }

        private XmlDocument GetSchemaXml(string dbidToGetSchemaFor)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetSchema", dbidToGetSchemaFor, false);
            return request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Tells platform to upgrade the instance's schema to the latest version if necessary.
        /// </summary>
        public void UpdateSchema(String dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_UpdateSchema", dbid, false);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Import (add/update) a bunch of records at the same time.
        /// Make sure to build the csv correctly (e.g. escape the values using <see cref="CsvHelper.EscapeForCsv"/>, make sure to include the record id column if you're doing updates).
        /// </summary>
        /// <param name="dbid">the table to import to</param>
        /// <param name="csv">CSV with content of records to be added/updated</param>
        /// <param name="clist">dot-separated list of field IDs</param>
        /// <returns>the result of the import</returns>
        public ImportResult ImportCsv(string dbid, string csv, string clist)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_ImportFromCSV", dbid, false);
            request.ReqDoc.AddCDataParameter("records_csv", csv);
            request.ReqDoc.AddTextParameter("clist", clist);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            return new ImportResult(respXml);
        }

        /// <summary>
        /// Convenience method that calls PurgeRecords with an empty query clause... deletes all records in the given table.
        /// </summary>
        /// <param name="tableDbid">dbId of table in which you want to delete all records</param>
        /// <returns>number of records deleted</returns>
        public int PurgeTable(string tableDbid)
        {
            return PurgeRecords(tableDbid, null);
        }

        /// <summary>
        /// Deletes all records in the given table that match the query clause... deletes ALL records in the table if query clause is empty.
        /// </summary>
        /// <param name="tableDbid">dbId of table in which you want to delete the records</param>
        /// <param name="query">the query clause to find the records to be deleted. leave empty to delete all records in table</param>
        /// <returns>number of records deleted</returns>
        public int PurgeRecords(string tableDbid, string query)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_PurgeRecords", tableDbid, false);
            if (!String.IsNullOrEmpty(query))
            {
                request.ReqDoc.AddQueryParameter(query);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            return Int32.Parse(respXml.SelectSingleNode("//num_records_deleted").InnerText, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Renames application instance so it shows up under that new name in the App Dashboard. This method will call ValidateAppName before attempting to send the request.
        /// </summary>
        /// <param name="dbid"></param>
        /// <param name="appName"></param>
        public void RenameApp(string dbid, string appName)
        {
            ValidateAppName(appName);
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_RenameApp", dbid, false);
            request.ReqDoc.AddTextParameter("newappname", appName);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Call this to see if the chosen appName is valid, in preparation to renaming or creating an application instance. Will throw ArgumentException (or a derived exception) if it's not valid. The message should be OK to be seen by (English language) end users.
        /// An Application name:
        /// 1. cannot begin or end with whitespace
        /// 2. cannot be empty
        /// 3. cannot be "QuickBase FAQ" (or any equivalent upper/lower case variant)
        /// 4. cannot start with the underscore character
        /// 5. cannot be longer than 225 characters
        /// 6. should not contain a Carriage Return (CR) or Line Feed (LF) character
        /// </summary>
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        /// <exception cref="ArgumentException">Application name cannot begin or end with whitespace.</exception>
        /// <exception cref="ArgumentException">Application name cannot begin with underscore.</exception>
        /// <exception cref="ArgumentException">Application name must not be longer than 225 characters.</exception>
        /// <exception cref="ArgumentException">'QuickBase FAQ' is a reserved application name.</exception>
        /// <exception cref="ArgumentException">Application name cannot contain a CR or LF character.</exception>
        public static void ValidateAppName(string appName)
        {
            IsValidAppName(appName, true); // TODO: This validation could be useful for CloneDatabase and CreateDatabase. We should check and apply to those methods as well
        }

        /// <summary>
        /// Checks if appName is a valid name for a QuickBase or Workplace instance.
        /// </summary>
        /// <seealso cref="ValidateAppName" />
        public static bool IsValidAppName(string appName, bool throwExceptionIfInvalid)
        {
            if (String.IsNullOrEmpty(appName))
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ArgumentNullException("appName", Resources.PlatformClientException_ApplicationNameMustNotBeNullNorEmpty_Application_name_must_not_be_null_nor_empty_);
                }
                return false;
            }
            if (!appName.Trim().Equals(appName))
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ArgumentException(Resources.PlatformClientException_ApplicationNameCannotBeginOrEndWithWhitespace_Application_name_cannot_begin_or_end_with_whitespace_, "appName");
                }
                return false;
            }
            if (appName.StartsWith("_"))
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ArgumentException(Resources.PlatformClientException_ApplicationNameCannotBeginWithUnderscore_Application_name_cannot_begin_with_underscore_, "appName");
                }
                return false;
            }
            if (appName.Length > MaxLenAppName)
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ArgumentException(String.Format(Resources.PlatformClientException_ApplicationNameMustNotBeLongerThanCharacters_Application_name_must_not_be_longer_than__0__characters_, MaxLenAppName), "appName");
                }
                return false;
            }
            if (appName.Equals(ForbiddenAppName, StringComparison.InvariantCultureIgnoreCase))
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ArgumentException(String.Format(Resources.PlatformClientException_QuickbaseFaqIsAReservedApplicationName___0___is_a_reserved_application_name_, appName), "appName");
                }
                return false;
            }
            if (appName.Contains("\n") || appName.Contains("\r"))
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ArgumentException(Resources.PlatformClientException_ApplicationNameCannotContainACrOrLfCharacter_Application_name_cannot_contain_a_CR_or_LF_character_, "appName");
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Assuming you have an name you want to name an instance, and it might not be valid according to ValidateAppName(), use this function to scrub anything that's invalid.
        /// Replaces underscores and all CR/LF combinations with spaces, then trims the spaces, then cuts down to size.
        /// Returns null if appName is null or empty, or if the appName is still invalid after that (e.g. if you used a forbidden name).
        /// </summary>
        /// <seealso cref="ValidateAppName" />
        public static string ScrubFutureAppName(string appName)
        {
            if (appName == null)
            {
                return null;
            }
            appName = appName.Replace('_', ' ').Replace("\n\r", " ").Replace("\r\n", " ").Replace('\r', ' ').Replace('\n', ' ');
            appName = appName.Trim();
            if (appName.Length > MaxLenAppName)
            {
                appName = appName.Substring(0, MaxLenAppName).Trim();
            }
            return IsValidAppName(appName, false) ? appName : null;
        }
        /// <summary>
        /// Gets the upload token.
        /// </summary>
        public void associateDevKeyWithApplication(string username, string password, string dbId, string description)
        {
            // QBIS_AddApplicationDeveloperKey 
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("QBIS_AddApplicationDeveloperKey", dbId, false);
            request.ReqDoc.AddTextParameter("keytype", "U");
            request.ReqDoc.AddTextParameter("dbid", dbId);
            request.ReqDoc.AddTextParameter("refdbid", dbId);
            request.ReqDoc.AddTextParameter("keydescription", description);
            request.ReqDoc.AddTextParameter("username", username);
            request.ReqDoc.AddTextParameter("password", password);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            
            return;
        }
        /// <summary>
        /// CAUTION: Creates a whole new application in the app store, not just a new instance of one.
        /// </summary>
        public DatabaseCreateStatus CreateDatabase(string appName, string appDesc, bool createAppToken)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_CreateDatabase", "main", true);
            if (createAppToken)
            {
                request.ReqDoc.AddTextParameter("createapptoken", "1");
            }
            request.ReqDoc.AddTextParameter("dbname", appName);
            request.ReqDoc.AddTextParameter("dbdesc", appDesc);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            DatabaseCreateStatus retval = DatabaseCreateStatus.ParseCreateDatabase(respXml);
            return retval;
        }

        /// <summary>
        /// If you have application administration rights, 
        /// you can use this call to delete either a child table 
        /// or the entire application, depending on the dbid you supply. 
        /// </summary>
        public void DeleteDatabase(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_DeleteDatabase", dbid, false);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Clones the specified database/application.
        /// </summary>
        public string CloneDatabase(string dbid, string newAppName, string newAppDesc, bool keepData, bool excludeFiles)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_CloneDatabase", dbid, false);
            request.ReqDoc.AddTextParameter("newdbname", newAppName);
            if (!String.IsNullOrEmpty(newAppDesc))
            {
                request.ReqDoc.AddTextParameter("newdbdesc", newAppDesc);
            }
            //If keepData is omitted, it will just create the schema
            if (keepData)
            {
                request.ReqDoc.AddTextParameter("keepData", "1");
            }
            //Omit exclude files to clone without attached files
            if (excludeFiles)
            {
                request.ReqDoc.AddTextParameter("excludefiles", "1");
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            //DatabaseCreateStatus retval = DatabaseCreateStatus.ParseCreateDatabase(respXml);
            XmlNode node = respXml.SelectSingleNode("//newdbid");
            return node.InnerText;
        }

        /// <summary>
        /// Retrieve an application instance-level DB variable.
        /// </summary>
        /// <param name="dbid">instance dbid</param>
        /// <param name="varname">A varname that exists in the target application.</param>
        /// <returns></returns>
        public string GetDBVar(string dbid, string varname)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetDBvar", dbid, false);
            request.ReqDoc.AddTextParameter("varname", varname);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            string returnValue = String.Empty;

            XmlNode node = respXml.SelectSingleNode("//value");
            if (node != null)
            {
                returnValue = node.InnerText;
            }

            return returnValue;
        }

        /// <summary>
        /// Update an application instance-level DB variable.
        /// </summary>
        /// <param name="dbid">instance dbid</param>
        /// <param name="varname">A varname that exists in the target application</param>
        /// <param name="value"></param>
        public void SetDBVar(string dbid, string varname, string value)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_SetDBvar", dbid, false);
            request.ReqDoc.AddTextParameter("varname", varname);
            request.ReqDoc.AddTextParameter("value", value);
            request.ExecuteRequest(WorkNotification);
        }


        /// <summary>
        /// Add new record to the database.
        /// </summary>
        /// <param name="dbid">The table dbid</param>
        /// <param name="fieldIdsOrNamesAndValues">List of fields and values</param>
        /// <returns></returns>
        public EditResult AddRecord(string dbid, List<KeyValuePair<string, string>> fieldIdsOrNamesAndValues)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_AddRecord", dbid, false);
            foreach (KeyValuePair<string, string> field in fieldIdsOrNamesAndValues)
            {
                request.ReqDoc.AddFieldParameter(field.Key, field.Value);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);

            return new EditResult(respXml);
        }

        /// <summary>
        /// Deletes a specific record in a table
        /// </summary>
        /// <param name="dbid">The table dbid</param>
        /// <param name="rid">The record id# of the record to be deleted</param>
        public void DeleteRecord(string dbid, string rid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_DeleteRecord", dbid, false);
            request.ReqDoc.AddTextParameter("rid", rid);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Convenience function to edit a single field in a record.
        /// </summary>
        /// <param name="dbid">dbid of the table</param>
        /// <param name="rid">record id# of the record</param>
        /// <param name="updateId">optional update_id parameter</param>
        /// <param name="fidOrName">id# of the field</param>
        /// <param name="value">the new value to store</param>
        /// <returns>the result of the operation</returns>
        public EditResult EditRecord(string dbid, string rid, string updateId, string fidOrName, string value)
        {
            return EditRecord(dbid, rid, updateId, SingleField(fidOrName, value));
        }

        /// <summary>
        /// Edits a record.
        /// </summary>
        /// <param name="dbid">dbid of the table</param>
        /// <param name="rid">record id# of the record</param>
        /// <param name="updateId">optional update_id parameter</param>
        /// <param name="fieldIdsOrNamesAndValues">a list of key-value-pairs, where the keys must be field id#s, and the values must be the new desired values for the fields</param>
        /// <returns>the result of the operation</returns>
        public EditResult EditRecord(string dbid, string rid, string updateId, List<KeyValuePair<string, string>> fieldIdsOrNamesAndValues)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_EditRecord", dbid, false);
            request.ReqDoc.AddTextParameter("rid", rid);
            if (!String.IsNullOrEmpty(updateId))
            {
                request.ReqDoc.AddTextParameter("update_id", updateId);
            }
            foreach (KeyValuePair<string, string> field in fieldIdsOrNamesAndValues)
            {
                request.ReqDoc.AddFieldParameter(field.Key, field.Value);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            return new EditResult(respXml);
        }

        /// <summary>
        /// Queries data from table (using "structured" format). Optional arguments can be null or empty string.
        /// </summary>
        /// <param name="dbid">dbid of the table</param>
        /// <param name="query">Query criteria statement</param>
        /// <param name="clist">Optional. Dot-separated list of columns (field id#s) you want to retrieve. If omitted, retuns columns contained default view. To get all columns, simply specify 'a' (or use the CListAll constant)</param>
        /// <param name="slist">Optional. Dot-separated list of columns (field id#s) for how the results should be sorted.</param>
        /// <param name="options">Optional. Other options (see IPP documentation).</param>
        /// <returns>a RecordSet with all the records that matched the query.</returns>
        public RecordSet DoQuery(string dbid, string query, string clist, string slist, string options)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_DoQuery", dbid, false);
            if (!String.IsNullOrEmpty(options))
            {
                request.ReqDoc.AddTextParameter("options", options);
            }
            if (!String.IsNullOrEmpty(clist))
            {
                request.ReqDoc.AddTextParameter("clist", clist);
            }
            if (!String.IsNullOrEmpty(slist))
            {
                request.ReqDoc.AddTextParameter("slist", slist);
            }
            request.ReqDoc.AddTextParameter("fmt", "structured");
            request.ReqDoc.AddQueryParameter(query);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            return RecordSet.ParseQueryResponse(respXml);
        }

        /// <summary>
        /// Get the number of records that match the query.
        /// </summary>
        /// <param name="dbid">dbid of the table</param>
        /// <param name="query">Query criteria statement</param>
        /// <returns>number of records matching the query in the given table</returns>
        public int DoQueryCount(string dbid, string query)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_DoQueryCount", dbid, false);
            request.ReqDoc.AddQueryParameter(query);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//numMatches");
            if (node != null)
            {
                return Int32.Parse(node.InnerText);
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_DoQueryCount_DoQueryCount_did_not_return_numMatches_);
        }

        /// <summary>
        /// Add more choices to a multi-choice field
        /// </summary>
        public int FieldAddChoices(string dbid, string fid, IEnumerable<string> choices)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_FieldAddChoices", dbid, false);
            request.ReqDoc.AddTextParameter("fid", fid);
            foreach (string choice in choices)
            {
                request.ReqDoc.AddTextParameter("choice", choice);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//numadded");
            if (node != null)
            {
                return Int32.Parse(node.InnerText);
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_FieldAddChoices_Choices_could_not_be_added_to_field_);
        }

        /// <summary>
        /// Removes choicses from a multi-choice field
        /// </summary>
        public int FieldRemoveChoices(string dbid, string fid, IEnumerable<string> choices)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_FieldRemoveChoices", dbid, false);
            request.ReqDoc.AddTextParameter("fid", fid);
            foreach (string choice in choices)
            {
                request.ReqDoc.AddTextParameter("choice", choice);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//numremoved");
            if (node != null)
            {
                return Int32.Parse(node.InnerText);
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_FieldRemoveChoices_Choices_could_not_be_removed_from_field_);
        }

        /// <summary>
        /// Gets a new token for uploading a file.
        /// </summary>
        /// <returns></returns>
        public string GetFileUploadToken()
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetFileUploadToken", "main", true);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//fileUploadToken");
            if (node != null)
            {
                return node.InnerText;
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_GetFileUploadToken_GetFileUploadToken_did_not_return_Token);
        }

        /// <summary>
        /// Attach the IDS Realm
        /// </summary>
        /// <param name="dbid">dbid</param>
        /// <param name="realmId">realm Id</param>
        public void AttachIDSRealm(string dbid, string realmId)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_AttachIDSRealm", dbid, false);
            request.ReqDoc.AddTextParameter("realm", realmId);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Detach the IDS Realm
        /// </summary>
        /// <param name="dbid">dbid</param>
        /// <param name="realmId">realm Id</param>
        public void DetachIDSRealm(string dbid, string realmId)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_DetachIDSRealm", dbid, false);
            request.ReqDoc.AddTextParameter("realm", realmId);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Get the ID of the IDS Realm the application instance is associated with.
        /// </summary>
        /// <param name="dbid">dbid</param>
        /// <returns>realm ID</returns>
        public string GetIDSRealm(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetIDSRealm", dbid, false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//realm");
            if (node != null)
            {
                return node.InnerText;
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_GetIDSRealm_GetIDSRealm_did_not_return_realm);
        }

        /// <summary>
        /// Change Record Owner
        /// </summary>
        /// <param name="dbid">the table dbid</param>
        /// <param name="rid">record id</param>
        /// <param name="newOwner">new owner</param>
        public void ChangeRecordOwner(string dbid, string rid, string newOwner)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_ChangeRecordOwner", dbid, false);
            request.ReqDoc.AddTextParameter("rid", rid);
            request.ReqDoc.AddTextParameter("newowner", newOwner);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Create Table
        /// </summary>
        /// <param name="dbid">dbid</param>
        /// <param name="tname">table name</param>
        /// <param name="pnoun">pronoun</param>
        /// <returns></returns>
        public string CreateTable(string dbid, string tname, string pnoun)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_CreateTable", dbid, false);
            if (!String.IsNullOrEmpty(tname))
            {
                request.ReqDoc.AddTextParameter("tname", tname);
            }
            if (!String.IsNullOrEmpty(pnoun))
            {
                request.ReqDoc.AddTextParameter("pnoun", pnoun);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//newdbid");
            if (node != null)
            {
                return node.InnerText;
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_CreateTable_CreateTable_did_not_return_newdbid);
        }

        /// <summary>
        /// Run Import
        /// </summary>
        /// <param name="dbid">the table dbid</param>
        /// <param name="id">import id</param>
        /// <returns></returns>
        public string RunImport(string dbid, string id)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_RunImport", dbid, false);
            request.ReqDoc.AddTextParameter("id", id);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//import_status");
            if (node != null)
            {
                return node.InnerText;
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_RunImport_RunImport_did_not_return_import_status);
        }

        /// <summary>
        /// Get UserInfo for currently logged in user. Convenience function that calls GetUserInfo without specifying a user.
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetCurrentUserInfo()
        {
            return GetUserInfo(null);
        }

        /// <summary>
        /// Get user info for a given screenname or email.
        /// </summary>
        /// <param name="emailOrScreenname">screenname or email of user to provide info for. leave empty to get info on currently logged in user</param>
        /// <returns></returns>
        public List<UserInfo> GetUserInfo(string emailOrScreenname)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetUserInfo", "main", false);
            if (!String.IsNullOrEmpty(emailOrScreenname))
            {
                request.ReqDoc.AddTextParameter("email", emailOrScreenname);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNodeList nodes = respXml.SelectNodes("//user");
            return UserInfo.ParseUsers(nodes);
        }

        /// <summary>
        /// Quick way to get total number of records without querying for all the data.
        /// </summary>
        public long GetNumRecords(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetNumRecords", dbid, false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//num_records");
            if (node != null)
            {
                return Int64.Parse(node.InnerText);
            }
            throw new PlatformClientException(Host, Resources.PlatformSessionContext_GetNumRecords_GetNumRecords_did_not_return_num_records);
        }

        /// <summary>
        /// Users in the application
        /// </summary>
        public List<UserInfo> GetUserRoles(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_UserRoles", dbid, false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNodeList nodes = respXml.SelectNodes("//users/user");
            return UserInfo.ParseUsers(nodes);
        }

        /// <summary>
        /// Info about specific user for application
        /// </summary>
        /// <returns>UserInfo structure with role information, or null</returns>
        public UserInfo GetUserRole(string dbid, string userId)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetUserRole", dbid, false);
            request.ReqDoc.AddTextParameter("userid", userId);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//user");
            if (node != null)
            {
                return new UserInfo(node);
            }
            return null;
        }

        /// <summary>
        /// Add the specified role to the user in this application
        /// </summary>
        public void AddUserToRole(string dbid, string userId, string roleId)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_AddUserToRole", dbid, false);
            request.ReqDoc.AddTextParameter("userid", userId);
            request.ReqDoc.AddTextParameter("roleid", roleId);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Removes old role and adds new role
        /// </summary>
        public void ChangeUserRole(string dbid, string userId, string oldRoleId, string newRoleId)
        {
            //PlatformApiXmlHttpPostRequest request = CreateApiRequest("API_ChangeUserRole", dbid);
            //AddTicketAndToken(request);
            //request.ReqDoc.AddTextParameter("userid", userId);
            //request.ReqDoc.AddTextParameter("roleid", roleId);
            //request.ReqDoc.AddTextParameter("newroleid", newroleId);
            //newRoleId = (newRoleId == null) ? string.Empty : newRoleId;
            //request.ReqDoc.AddTextParameter("newroleid", newRoleId);
            //XmlDocument respXml = request.ExecuteRequest(WorkNotification);

            // TODO: This is a work around till API_ChangeUserRole issue will get resolved.
            AddUserToRole(dbid, userId, newRoleId);
            RemoveUserFromRole(dbid, userId, oldRoleId);
        }

        /// <summary>
        /// Retrieves BillingInfo structure describing billing status for application instance.
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public BillingInfo GetBillingStatus(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetBillingStatus", dbid, false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            return new BillingInfo(respXml);
        }

        /// <summary>
        /// Queries if the gives instance is in grace status. For free plans that means the user canceled the instance.
        /// </summary>
        /// <param name="dbid">application dbid</param>
        /// <returns>true if the Billing Status Code is GRACE</returns>
        public bool InstanceHasGraceStatus(string dbid)
        {
            try
            {
                return GetBillingStatus(dbid).HasStatusGrace();
            }
            catch (PlatformApiXmlHttpError e)
            {
                if (e.IsErrorCode(PlatformApiXmlHttpError.ErrorCodes.InvalidInput))
                {
                    // probably a development instance
                    return false;
                }
                throw;
            }
        }


        /// <summary>
        /// Roles defined in specified application
        /// </summary>
        public List<RoleInfo> GetRoleInfo(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetRoleInfo", dbid, false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNodeList roleNodes = respXml.SelectNodes("//roles/role");
            return RoleInfo.ParseRoles(roleNodes);
        }

        /// <summary>
        /// Generic user info for specified email or User ID.
        /// </summary>
        public UserInfo GetUser(string emailOrScreenname)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetUserInfo", "main", false);
            request.ReqDoc.AddTextParameter("email", emailOrScreenname);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//user");
            UserInfo user = new UserInfo(node);
            return user;
        }

        /// <summary>
        /// Removes role for specified user in specified instance.
        /// </summary>
        public void RemoveUserFromRole(string dbid, string userId, string roleId)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_RemoveUserFromRole", dbid, false);
            request.ReqDoc.AddTextParameter("userid", userId);
            request.ReqDoc.AddTextParameter("roleid", roleId);
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Convenience function that will query a user's roles and then remove them one by one.
        /// If the user is referenced by any User-type field in the database, that user will remain visible in the user list with a role of "none".
        /// </summary>
        /// <param name="dbid"></param>
        /// <param name="userId"></param>
        public void RemoveUser(string dbid, string userId)
        {
            UserInfo user = GetUserRole(dbid, userId);
            if (user != null)
            {
                foreach (RoleInfo role in user.Roles)
                {
                    RemoveUserFromRole(dbid, userId, role.ID);
                }
            }
        }

        /// <summary>
        /// Create provisional user account for given user information, so user can be invited and assigned roles.
        /// </summary>
        /// <returns>Unique internal user-ID for the provisioned user account</returns>
        public string ProvisionUser(string dbid, string email, string firstName, string lastName, string roleID)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_ProvisionUser", dbid, false);
            request.ReqDoc.AddTextParameter("email", email);
            request.ReqDoc.AddTextParameter("fname", firstName);
            request.ReqDoc.AddTextParameter("lname", lastName);
            if (!String.IsNullOrEmpty(roleID))
            {
                request.ReqDoc.AddTextParameter("roleid", roleID);
            }
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.SelectSingleNode("//userid");
            return node.InnerText;
        }

        /// <summary>
        /// Send an invitation email to the specified user-id. Optionally provide custom text to be included in the email.
        /// </summary>
        public void SendInvitation(string dbid, string userId, string userText)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_SendInvitation", dbid, false);
            request.ReqDoc.AddTextParameter("userid", userId);
            if (!String.IsNullOrEmpty(userText))
            {
                request.ReqDoc.AddTextParameter("usertext", userText);
            }
            request.ExecuteRequest(WorkNotification);
        }

        /// <summary>
        /// Retrieve list of entitlements
        /// </summary>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public EntitlementInfo GetEntitlementValues(string dbid)
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetEntitlementValues", dbid, false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.DocumentElement;
            EntitlementInfo entitlement = new EntitlementInfo(node);
            return entitlement;
        }

        /// <summary>
        /// List of admins for all published applications
        /// </summary>
        /// <returns></returns>
        public List<AdminInfo> GetAdminsForAllProducts()
        {
            PlatformApiXmlHttpPostRequest request = CreateAuthenticatedApiRequest("API_GetAdminsForAllProducts", "main", false);
            XmlDocument respXml = request.ExecuteRequest(WorkNotification);
            XmlNode node = respXml.DocumentElement;
            return AdminInfo.ParseAdmins(node);
        }

        #endregion

        #region Query helpers

        /// <summary>
        /// Helps you escape a value that you need to put into a Query clause.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string EscapeForQuery(string txt)
        {
            return String.IsNullOrEmpty(txt) ? String.Empty : txt.Replace("'", "''");
        }

        /// <summary>
        /// Append a query criteria so that the field <paramref name="fi"/> has to match <paramref name="matchingValue"/> exactly, using a logical <strong>OR</strong> to append to rest of query (if this is not the first criteria in query).
        /// </summary>
        /// <returns><paramref name="query"/></returns>
        public static StringBuilder AddExQueryCriteriaWithOr(StringBuilder query, FieldInfo fi, string matchingValue)
        {
            return AddExQueryCriteriaWithOr(query, fi.Id, matchingValue);
        }

        /// <summary>
        /// Append a query criteria so that the field with field-ID <paramref name="fieldId"/> has to match <paramref name="matchingValue"/> exactly, using a logical <strong>OR</strong> to append to rest of query (if this is not the first criteria in query).
        /// </summary>
        /// <returns><paramref name="query"/></returns>
        public static StringBuilder AddExQueryCriteriaWithOr(StringBuilder query, string fieldId, string matchingValue)
        {
            return AddQueryCriteria(query, QueryCriteriaCombinationOp_OR, fieldId, matchingValue, QueryComparisonOp_EX);
        }

        /// <summary>
        /// Append a query criteria so that the field <paramref name="fi"/> has to match <paramref name="matchingValue"/> exactly, using a logical <strong>AND</strong> to append to rest of query (if this is not the first criteria in query).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="fi">The FieldInfo.</param>
        /// <param name="matchingValue">The matching value: Value from comparision operator.</param>
        /// <returns><paramref name="query"/></returns>
        public static StringBuilder AddExQueryCriteriaWithAnd(StringBuilder query, FieldInfo fi, string matchingValue)
        {
            return AddExQueryCriteriaWithAnd(query, fi.Id, matchingValue);
        }

        /// <summary>
        /// Append a query criteria so that the field with field-ID <paramref name="fieldId"/> has to match <paramref name="matchingValue"/> exactly, using a logical <strong>AND</strong> to append to rest of query (if this is not the first criteria in query).
        /// </summary>
        /// <returns><paramref name="query"/></returns>
        public static StringBuilder AddExQueryCriteriaWithAnd(StringBuilder query, string fieldId, string matchingValue)
        {
            return AddQueryCriteria(query, QueryCriteriaCombinationOp_AND, fieldId, matchingValue, QueryComparisonOp_EX);
        }

        /// <summary>
        /// Start a new query string, where the first query criteria is that field <paramref name="fi"/> has to match <paramref name="matchingValue"/> using the provided <paramref name="comparisonOperator"/>.
        /// </summary>
        /// <returns>the query string</returns>
        public static StringBuilder StartQueryCriteria(FieldInfo fi, string matchingValue, string comparisonOperator)
        {
            return StartQueryCriteria(fi.Id, matchingValue, comparisonOperator);
        }

        /// <summary>
        /// Start a new query string, where the first query criteria is that field with field-ID <paramref name="fieldId"/> has to match <paramref name="matchingValue"/> using the provided <paramref name="comparisonOperator"/>.
        /// </summary>
        /// <returns>the query string</returns>
        public static StringBuilder StartQueryCriteria(string fieldId, string matchingValue, string comparisonOperator)
        {
            return AddQueryCriteria(null, null, fieldId, matchingValue, comparisonOperator);
        }

        /// <summary>
        /// Useful for building a query. Appends to an existing query string. Will add booleanOperator only if the query already has criteria.
        /// </summary>
        /// <param name="query">the query you're appending to. if null, this method will instantiate a new StringBuilder. If the query is null orempty, the booleanOperator is ignored</param>
        /// <param name="fieldId">ID of field to query</param>
        /// <param name="matchingValue">the value the field has to match</param>
        /// <param name="booleanOperator">OR or AND</param>
        /// <param name="comparisonOperator">one of the supported comparison operators, e.g. "EX"</param>
        /// <returns>the query you passed in with the new criteria appended, or a new query StringBuilder if you passed in null as the query parameter</returns>
        public static StringBuilder AddQueryCriteria(StringBuilder query, string booleanOperator, string fieldId, string matchingValue, string comparisonOperator)
        {
            if (query == null)
            {
                query = new StringBuilder(20);
            }
            else if (query.Length > 0)
            {
                query.Append(booleanOperator);
            }
            AppendQueryCriteria(query, fieldId, comparisonOperator, matchingValue);
            return query;
        }

        /// <summary>
        /// Appends to the StringBuilder <paramref name="query"/> a new query criteria (DOES NOT ADD A LOGICAL OPERATOR if the query already contains criteria). The field with the field-ID <paramref name="fieldId"/> has to match <paramref name="matchingValue"/> using the provided <paramref name="comparisonOperator"/>.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="fieldId"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="matchingValue"></param>
        public static void AppendQueryCriteria(StringBuilder query, string fieldId, string comparisonOperator, string matchingValue)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query", Resources.PlatformClientException_MethodExpectsANonNullStringbuilderForTheQueryParameter_Non_null_StringBuilder_expected_for_the_query_parameter);
            }
            query.Append("{'");
            query.Append(fieldId);
            query.Append("'.");
            query.Append(comparisonOperator);
            query.Append(".'");
            query.Append(matchingValue);
            query.Append("'}");
        }

        #endregion

        #region Field helpers

        /// <summary>
        /// Convenience function for when a function requires a idOrName-value-pair list, and all you have is a single <paramref name="idOrName"/>-<paramref name="value"/>-pair.
        /// </summary>
        /// <param name="idOrName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> SingleField(string idOrName, string value)
        {
            return new List<KeyValuePair<string, string>>(1) { new KeyValuePair<string, string>(idOrName, value) };
        }

        /// <summary>
        /// Assuming the value represents a date or date/time returned by a query, parses the timestamp and converts it to a local date/time.
        /// </summary>
        /// <param name="value">the time stamp returned by the query</param>
        /// <returns>the local time or date represented by the time stamp, or DateTime.MinValue if not a valid time stamp</returns>
        public static DateTime ParseDateTimeField(string value)
        {
            long dateValue;
            if (Int64.TryParse(value, out dateValue))
            {
                return GetLocalDateFromQuickBaseDate(dateValue);
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// A so-called "QuickBaseDate" is a date used by Intuit QuickBase (and WorkPlace, if the app uses QuickBase as the underlying data store),
        /// which is stored as the number of milliseconds since 1/1/1970 00:00:00 UTC.
        /// This function returns the local equivalent of that date.
        /// </summary>
        /// <param name="quickbaseDate">a date returned from QuickBase as part of a query</param>
        /// <returns>the local equivalent of that date as a DateTime object</returns>
        public static DateTime GetLocalDateFromQuickBaseDate(long quickbaseDate)
        {
            return DateHelper.EpochJanFirst1970UTC.AddMilliseconds(quickbaseDate).ToLocalTime();
        }

        #endregion

       
    }
}
