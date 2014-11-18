using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Platform.Client.Core.IDS;
using Intuit.Platform.Client.Core;
using Intuit.Sb.Cdm;

namespace Intuit.Sb.Cdm.QB
{
    public class CompanyService: IDSBaseService
	{
        /// <summary>
        /// Returns a list of all Companies.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <returns>List of all user companies</returns>
        public CompaniesMetaData GetAvailableCompaniesList(PlatformSessionContext context)
        {
            #region Local Variables
            IDSOperationContext operationContext = null;
            CdmComplexBase payload = null;
            CompaniesMetaData companiesMetaData = null;
            #endregion

            context.ServiceType = IntuitServicesType.QBD;
            operationContext = new IDSOperationContext(IDSResource.company, null);
            operationContext.ContentType = Intuit.Platform.Client.Core.IDS.Properties.Settings.Default.TextXML;
            operationContext.CompanyParameters = new String[] { "availablelist" };
            payload = base.GetResources(context, operationContext, typeof(CompaniesMetaData));
            companiesMetaData = (CompaniesMetaData)payload;
            return companiesMetaData;
        }

        /// <summary>
        /// Returns a list of all Companies.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <returns>List of all user companies</returns>
		public CompaniesMetaData GetAvailableCompanies(PlatformSessionContext context)
        {
            #region Local Variables
            IDSOperationContext     operationContext    = null;
            CdmComplexBase          payload             = null;
            CompaniesMetaData       companiesMetaData   = null;
            #endregion

            context.ServiceType = IntuitServicesType.QBD;
            operationContext    = new IDSOperationContext(IDSResource.company, null);
            operationContext.ContentType = Intuit.Platform.Client.Core.IDS.Properties.Settings.Default.TextXML;
			operationContext.CompanyParameters = new String[] {"available"};
            payload             = base.GetResources(context, operationContext, typeof(CompaniesMetaData));
			companiesMetaData   = (CompaniesMetaData)payload;
			return companiesMetaData;
		}

        /// <summary>
        /// Creates a new company using the provided CompanyMetadata.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="companyMetaData">New Company object</param>
        /// <returns>
        /// Returns an updated version of the CompanyMetaData with updated IdType and sync token.
        /// </returns>
		public CompanyMetaData CreateCompany(PlatformSessionContext context, CompanyMetaData companyMetaData)
        {
            #region Local Variables
            CompanyRequest      companyRequest      = null;
            IDSOperationContext operationContext    = null;
            CdmComplexBase      payload             = null;
            CompaniesMetaData   companiesMetaData   = null;
            #endregion

            context.ServiceType                 = IntuitServicesType.QBD;
            companyRequest                      = new CompanyRequest();
			companyRequest.CompanyMetaData      = companyMetaData;
			companyRequest.Action               = postAction.POST;

			operationContext                    = new IDSOperationContext(IDSResource.company, null);
            operationContext.ContentType        = "text/xml";
			operationContext.CompanyParameters  = new String[] {"create"};
            payload                             = (CdmComplexBase)DoIDSPost(context, operationContext, companyRequest);
			companiesMetaData                   = (CompaniesMetaData)payload;
			return companiesMetaData.CompanyMetaData[0];
			
		}
        /// <summary>
        /// Returns a list of all Companies.
        /// </summary>
        /// <param name="context">PlatformSessionContext object with session info filled</param>
        /// <param name="realmId">The realm id.</param>
        /// <returns>List of all user companies</returns>
        public CompaniesMetaData InitializeCompany(PlatformSessionContext context, string realmId)
        {
            #region Local Variables
            IDSOperationContext operationContext = null;
            CdmComplexBase payload = null;
            CompaniesMetaData companiesMetaData = null;
            #endregion

            context.ServiceType = IntuitServicesType.QBD;
            operationContext = new IDSOperationContext(IDSResource.company,realmId);
            operationContext.ContentType = Intuit.Platform.Client.Core.IDS.Properties.Settings.Default.TextXML;
            operationContext.CompanyParameters = new String[] { "initialize" };
            payload = base.GetResources(context, operationContext, typeof(CompaniesMetaData));
            companiesMetaData = (CompaniesMetaData)payload;
            return companiesMetaData;
        }

    }
}
