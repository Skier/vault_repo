using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;

namespace Dalworth.Domain.Sync
{
    public class DalworthSyncService : ServerSyncService
    {
        public DalworthSyncService()
        {
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            Url = Configuration.WebServiceUrl;

#if NETCF35
            ClientCertificates.Add(Configuration.ClientCertificate);            
#endif
        }
    }
}
