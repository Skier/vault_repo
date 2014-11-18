using System;

namespace Weborb.Samples.Email.Entities
{
    [Serializable]
    public class ServerSettingsInfo
    {
        [NonSerialized] public const string CONNECTION_TYPE_REGULAR = "regular";
        [NonSerialized] public const string CONNECTION_TYPE_SECURE_TLS = "tls";

        private int m_id;
        private string m_host;
        private int m_port;
        private string m_userName;
        private string m_userPassword;
        private string m_connectionType;

        public ServerSettingsInfo() {
            m_host = string.Empty;
            m_port = int.MinValue;
            m_userName = string.Empty;
            m_userPassword = string.Empty;
            m_connectionType = CONNECTION_TYPE_REGULAR;
        }

        internal int Id {
            get { return m_id; }
            set { m_id = value; }
        }

        public string Host {
            get { return m_host; }
            set { m_host = value; }
        }

        public int Port {
            get { return m_port; }
            set { m_port = value; }
        }

        public string UserName {
            get { return m_userName; }
            set { m_userName = value; }
        }

        public string UserPassword {
            get { return m_userPassword; }
            set { m_userPassword = value; }
        }

        public string ConnectionType {
            get { return m_connectionType; }
            set { m_connectionType = value; }
        }
    }
}