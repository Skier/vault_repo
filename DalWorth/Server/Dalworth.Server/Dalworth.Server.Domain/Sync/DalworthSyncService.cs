using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain.Sync
{
    public class DalworthSyncService : ServerSyncService.ServerSyncService
    {
        public DalworthSyncService()
        {
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            Url = Configuration.WebServiceUrl;

            ClientCertificates.Add(Configuration.ClientCertificate);            
        }
    }
}
