using System;
using System.Configuration;
using Intuit.Platform.Client.OAuth.Common;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class OAuthKeyStore : IOAuthKeyStore
    {
        private readonly OAuthConnection CurrentOAuthConnection;

        public OAuthKeyStore(string realmId)
        {
            var customer = ServmanCustomerService.FindByRealmId(realmId);
            CurrentOAuthConnection = OAuthConnection.GetByCustomerId(customer.Id) ??
                                     CreateNewOAuthConnection(customer);
        }

        private static OAuthConnection CreateNewOAuthConnection(ServmanCustomer servmanCustomer)
        {
            var result = new OAuthConnection
            {
                AccessTokenUrl = string.Empty,
                AuthorizeRequestUrl = string.Empty,
                DynamicKeyRetrievalUrl = string.Empty,
                RequestTokenUrl = string.Empty,
                AccessToken = string.Empty,
                AccessTokenSecret = string.Empty,
                ConsumerKey = string.Empty,
                ConsumerSecret = string.Empty,
                IsActive = false,
                ParentConsumerKey = ConfigurationManager.AppSettings["APP_TOKEN"],
                ServmanCustomerId = servmanCustomer.Id,
                DateCreated = DateTime.Now
            };

            //return OAuthConnection.Save(result);
            return result;
        }

        public string ParentConsumerKey
        {
            get 
            {
                return CurrentOAuthConnection.ParentConsumerKey;
            }
            set
            {
                CurrentOAuthConnection.ParentConsumerKey = value;
            }
        }

        public string ConsumerKey
        {
            get
            {
                return CurrentOAuthConnection.ConsumerKey;
            }
            set
            {
                CurrentOAuthConnection.ConsumerKey = value;
            }
        }

        public string ConsumerSecret
        {
            get
            {
                return CurrentOAuthConnection.ConsumerSecret;
            }
            set
            {
                CurrentOAuthConnection.ConsumerSecret = value;
            }
        }

        public string AccessToken
        {
            get
            {
                return CurrentOAuthConnection.AccessToken;
            }
            set
            {
                CurrentOAuthConnection.AccessToken = value;
            }
        }

        public string AccessTokenSecret
        {
            get
            {
                return CurrentOAuthConnection.AccessTokenSecret;
            }
            set
            {
                CurrentOAuthConnection.AccessTokenSecret = value;
            }
        }

        public void SetCurrentConnectionActive()
        {
            CurrentOAuthConnection.IsActive = true;
        }

        public void Flush()
        {
            OAuthConnection.Save(CurrentOAuthConnection);
        }
    }
}