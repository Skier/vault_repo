package Domain
{
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.ServerSettingsInfo")]
    public class ServerSettings
    {

        public static const CONNECTION_TYPE_REGULAR:String = "reqular";
        public static const CONNECTION_TYPE_SECURE_TLS:String = "tls";
            
        public var Host:String = "";
        public var Port:int = int.MIN_VALUE;
        public var UserName:String = "";
        public var UserPassword:String = "";
        public var ConnectionType:String = CONNECTION_TYPE_REGULAR;
     
        public function get IsRegularConnection():Boolean 
        {
            return ConnectionType == CONNECTION_TYPE_REGULAR;
        }
    }
}