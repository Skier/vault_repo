using System;

namespace Weborb.Samples.Email.Entities
{
    [Serializable]
    public class ServerSettingsInfo
    {
        [NonSerialized] public const string CONNECTION_TYPE_REGULAR = "regular";
        [NonSerialized] public const string CONNECTION_TYPE_SECURE_TLS = "tls";

        [NonSerialized] public int Id;

        public string Host;
        public int Port;
        public string UserName;
        public string UserPassword;
        public string ConnectionType;

        public ServerSettingsInfo() {
            Host = string.Empty;
            Port = int.MinValue;
            UserName = string.Empty;
            UserPassword = string.Empty;
            ConnectionType = CONNECTION_TYPE_REGULAR;
        }

    }
}