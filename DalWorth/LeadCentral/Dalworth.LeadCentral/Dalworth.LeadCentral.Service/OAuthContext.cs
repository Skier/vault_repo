using Intuit.Platform.Client.OAuth;
using Intuit.Platform.Client.OAuth.Common;

namespace Dalworth.LeadCentral.Service
{
    public class OAuthContext
    {
        public const string OAuthContextKey = "oAuthContext";
        public const string RealmIdKey = "realmId";

        private IOAuthAccessGrantRequestSession OAuthAccessGrantRequestSession;
        public readonly OAuthConnector Connector;

        private readonly IOAuthKeyStore KeyStore;
        private readonly IOAuthUrls Urls;
        
        public OAuthContext(string realmId)
        {
            KeyStore = new OAuthKeyStore(realmId);
            Urls = new ProductionWorkplaceOAuthUrls();
            Connector = new OAuthConnector(Urls, KeyStore);
        }

        public static OAuthContext CreateContext(string realmId)
        {
            return new OAuthContext(realmId); 
        }

        public string GetAppToken()
        {
            return KeyStore.ParentConsumerKey;
        }

        public void RequestConsumerKeyIfNeeded()
        {
            Connector.RequestConsumerKeyIfNeeded();
        }

        public string GetGrantPageUrl(string callBackUrl)
        {
            return Connector.GetGrantPageUrl(callBackUrl, out OAuthAccessGrantRequestSession);
        }

        public void RequestAccessToken(string verifier)
        {
            Connector.ExchangeVerifierForAccessToken(OAuthAccessGrantRequestSession, verifier);
        }

        public void SetCurrentConnectionActive()
        {
            var keyStore = (OAuthKeyStore) KeyStore;
            keyStore.SetCurrentConnectionActive();
            keyStore.Flush();
        }
    }
}