namespace Weborb.Samples.Email.Entities
{
    public class ServerSettingsInfo
    {
	    public const string REGULAR_CONNECTION_TYPE = "reqular";
	    public const string SECURE_TLS_CONNECTION_TYPE = "tls";

        private int _id;
        private string _host;
        private int _port;
        private string _userName;
        private string _password;
        private string _connectionType;

        public ServerSettingsInfo() {
        }

        public ServerSettingsInfo(int id, string host, int port, string connectionType, string userName, string password) {
            _id = id;
            _host = host;
            _port = port;
            _connectionType = connectionType;
            _userName = userName;
            _password = password;
        }

        public int Id {
            get { return _id; }
            set { _id = value; }
        }

        public string Host {
            get { return _host; }
            set { _host = value; }
        }

        public int Port {
            get { return _port; }
            set { _port = value; }
        }

        public string UserName {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password {
            get { return _password; }
            set { _password = value; }
        }

        public string ConnectionType {
            get { return _connectionType; }
            set { _connectionType = value; }
        }
        
    }
}