using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Intuit.Platform.Client.Core
{
    /// <summary>
    /// The result of a call to GetFederatedInfo() containing the new AppDestination URL of the
    /// created application as well as the other federating application information.
    /// </summary>
    public class FederationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FederationInfo"/> class.
        /// </summary>
        public FederationInfo()
        {
        }

        /// <summary>
        /// Parse the response from an API_GetAppFederationInfo call
        /// </summary>
        /// <param name="infoNode">The info node.</param>
        public FederationInfo(XmlNode infoNode)
        {
            XmlNode n = infoNode.SelectSingleNode("//externallyHosted");
            if (n != null)
            {
                ExternallyHosted = Convert.ToBoolean(Int32.Parse(n.InnerText));
            }
            n = infoNode.SelectSingleNode("//usesIDS");
            if (n != null)
            {
                UsesIDS = Convert.ToBoolean(Int32.Parse(n.InnerText));
            }
            n = infoNode.SelectSingleNode("//usesIDS_ShowEnterDataDirectly");
            if (n != null)
            {
                UsesIDS_ShowEnterDataDirectly = Convert.ToBoolean(Int32.Parse(n.InnerText));
            }
            n = infoNode.SelectSingleNode("//usesIDS_ShowQBooksDTData");
            if (n != null)
            {
                UsesIDS_ShowQBooksDTData = Convert.ToBoolean(Int32.Parse(n.InnerText));
            }
            n = infoNode.SelectSingleNode("//usesIDS_ShowQBOData");
            if (n != null)
            {
                UsesIDS_ShowQBOData = Convert.ToBoolean(Int32.Parse(n.InnerText));
            }
            n = infoNode.SelectSingleNode("//usesIDS_ShowQBooksDTSampleData");
            if (n != null)
            {
                UsesIDS_ShowQBooksDTSampleData = Convert.ToBoolean(Int32.Parse(n.InnerText));
            }
            n = infoNode.SelectSingleNode("//usesMSA");
            if (n != null)
            {
                UsesMSA = Convert.ToBoolean(Int32.Parse(n.InnerText));
            }
            n = infoNode.SelectSingleNode("//appDestinationURL");
            if (n != null)
            {
                AppDestinationURL = n.InnerText;
            }
            n = infoNode.SelectSingleNode("//appDestinationURLVarReplaced");
            if (n != null)
            {
                AppDestinationURLVarReplaced = n.InnerText;
               
            }
            n = infoNode.SelectSingleNode("//userManagementURL");
            if (n != null)
            {
                UserManagementURL = n.InnerText;
            }
            n = infoNode.SelectSingleNode("//serviceProviderId");
            if (n != null)
            {
                ServiceProviderId = n.InnerText;
            }
            n = infoNode.SelectSingleNode("//customCancelAppURL");
            if (n != null)
            {
                CustomCancelAppURL = n.InnerText;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [externally hosted].
        /// </summary>
        /// <value><c>true</c> if [externally hosted]; otherwise, <c>false</c>.</value>
        public bool ExternallyHosted {get;set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether [uses IDS].
        /// </summary>
        /// <value><c>true</c> if [uses IDS]; otherwise, <c>false</c>.</value>
        public bool UsesIDS {get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether [uses ID s_ show enter data directly].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [uses ID s_ show enter data directly]; otherwise, <c>false</c>.
        /// </value>
        public bool UsesIDS_ShowEnterDataDirectly {get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether [uses ID s_ show Q books DT data].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [uses ID s_ show Q books DT data]; otherwise, <c>false</c>.
        /// </value>
        public bool UsesIDS_ShowQBooksDTData {get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether [uses ID s_ show QBO data].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [uses ID s_ show QBO data]; otherwise, <c>false</c>.
        /// </value>
        public bool UsesIDS_ShowQBOData {get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether [uses ID s_ show Q books DT sample data].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [uses ID s_ show Q books DT sample data]; otherwise, <c>false</c>.
        /// </value>
        public bool UsesIDS_ShowQBooksDTSampleData {get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether [uses MSA].
        /// </summary>
        /// <value><c>true</c> if [uses MSA]; otherwise, <c>false</c>.</value>
        public bool UsesMSA {get;set;}

        /// <summary>
        /// Gets or sets the app destination URL.
        /// </summary>
        /// <value>The app destination URL.</value>
        public string AppDestinationURL { get; set; }

        /// <summary>
        /// Gets or sets the app destination URL var replaced.
        /// </summary>
        /// <value>The app destination URL var replaced.</value>
        public string AppDestinationURLVarReplaced {get;set;}

        /// <summary>
        /// Gets or sets the user management URL.
        /// </summary>
        /// <value>The user management URL.</value>
        public string UserManagementURL {get;set;}

        /// <summary>
        /// Gets or sets the service provider id.
        /// </summary>
        /// <value>The service provider id.</value>
        public string ServiceProviderId {get;set;}

        /// <summary>
        /// Gets or sets the custom cancel app URL.
        /// </summary>
        /// <value>The custom cancel app URL.</value>
        public string CustomCancelAppURL {get;set;}

    }
}
