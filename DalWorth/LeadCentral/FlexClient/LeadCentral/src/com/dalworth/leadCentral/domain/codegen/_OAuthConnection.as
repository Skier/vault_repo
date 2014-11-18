
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _OAuthConnection implements IDomainEntity
    {
        public function _OAuthConnection()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var servmanCustomerId:int;
        public function get ServmanCustomerId():int { return servmanCustomerId; }
        public function set ServmanCustomerId(value:int):void 
        {
            servmanCustomerId = value;
        }
      
        private var parentConsumerKey:String;
        public function get ParentConsumerKey():String { return parentConsumerKey; }
        public function set ParentConsumerKey(value:String):void 
        {
            parentConsumerKey = value;
        }
      
        private var requestTokenUrl:String;
        public function get RequestTokenUrl():String { return requestTokenUrl; }
        public function set RequestTokenUrl(value:String):void 
        {
            requestTokenUrl = value;
        }
      
        private var dynamicKeyRetrievalUrl:String;
        public function get DynamicKeyRetrievalUrl():String { return dynamicKeyRetrievalUrl; }
        public function set DynamicKeyRetrievalUrl(value:String):void 
        {
            dynamicKeyRetrievalUrl = value;
        }
      
        private var accessTokenUrl:String;
        public function get AccessTokenUrl():String { return accessTokenUrl; }
        public function set AccessTokenUrl(value:String):void 
        {
            accessTokenUrl = value;
        }
      
        private var authorizeRequestUrl:String;
        public function get AuthorizeRequestUrl():String { return authorizeRequestUrl; }
        public function set AuthorizeRequestUrl(value:String):void 
        {
            authorizeRequestUrl = value;
        }
      
        private var consumerKey:String;
        public function get ConsumerKey():String { return consumerKey; }
        public function set ConsumerKey(value:String):void 
        {
            consumerKey = value;
        }
      
        private var consumerSecret:String;
        public function get ConsumerSecret():String { return consumerSecret; }
        public function set ConsumerSecret(value:String):void 
        {
            consumerSecret = value;
        }
      
        private var accessToken:String;
        public function get AccessToken():String { return accessToken; }
        public function set AccessToken(value:String):void 
        {
            accessToken = value;
        }
      
        private var accessTokenSecret:String;
        public function get AccessTokenSecret():String { return accessTokenSecret; }
        public function set AccessTokenSecret(value:String):void 
        {
            accessTokenSecret = value;
        }
      
        private var dateCreated:Date;
        public function get DateCreated():Date { return dateCreated; }
        public function set DateCreated(value:Date):void 
        {
            dateCreated = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      

        public function prepareToSend():OAuthConnection
        {
            var result:OAuthConnection = new OAuthConnection();
      
            result.Id = this.Id;
      
            result.ServmanCustomerId = this.ServmanCustomerId;
      
            result.ParentConsumerKey = this.ParentConsumerKey;
      
            result.RequestTokenUrl = this.RequestTokenUrl;
      
            result.DynamicKeyRetrievalUrl = this.DynamicKeyRetrievalUrl;
      
            result.AccessTokenUrl = this.AccessTokenUrl;
      
            result.AuthorizeRequestUrl = this.AuthorizeRequestUrl;
      
            result.ConsumerKey = this.ConsumerKey;
      
            result.ConsumerSecret = this.ConsumerSecret;
      
            result.AccessToken = this.AccessToken;
      
            result.AccessTokenSecret = this.AccessTokenSecret;
      
            result.DateCreated = this.DateCreated;
      
            result.IsActive = this.IsActive;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new OAuthConnection();
      
            this.Id = value["Id"];
      
            this.ServmanCustomerId = value["ServmanCustomerId"];
      
            this.ParentConsumerKey = value["ParentConsumerKey"];
      
            this.RequestTokenUrl = value["RequestTokenUrl"];
      
            this.DynamicKeyRetrievalUrl = value["DynamicKeyRetrievalUrl"];
      
            this.AccessTokenUrl = value["AccessTokenUrl"];
      
            this.AuthorizeRequestUrl = value["AuthorizeRequestUrl"];
      
            this.ConsumerKey = value["ConsumerKey"];
      
            this.ConsumerSecret = value["ConsumerSecret"];
      
            this.AccessToken = value["AccessToken"];
      
            this.AccessTokenSecret = value["AccessTokenSecret"];
      
            this.DateCreated = value["DateCreated"];
      
            this.IsActive = value["IsActive"];
      
        }
    }
}
      