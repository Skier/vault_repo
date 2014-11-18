using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Dalworth.Server.Domain.Sync
{
    public class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint sp,
         X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }
    }
}
