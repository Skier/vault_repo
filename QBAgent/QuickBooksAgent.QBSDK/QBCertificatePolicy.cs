using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace QuickBooksAgent.QBSDK
{

    public class QBCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(
              ServicePoint srvPoint
            , X509Certificate certificate
            , WebRequest request
            , int certificateProblem)
        {
            return true;
        } 
    } 

}
