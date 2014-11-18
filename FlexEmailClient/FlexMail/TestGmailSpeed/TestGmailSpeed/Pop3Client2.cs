using System;
using System.IO;
using System.Text;

namespace TestGmailSpeed
{
    class Pop3Client2 : APop3Client
    {
        StreamReader reader;

        public override void Login(string userName, string userPassword) {
            
            reader = new StreamReader(stream, Encoding.Default);
            
            base.Login(userName, userPassword);
        }
        
        public override void Disconnect() {
            if (null != reader) {
                reader.Close();
                reader = null;
            }
            
            base.Disconnect();
        }
        
        protected override string ReadLine() {
            return reader.ReadLine();
        }

        protected override string ReadContent() {
            StringBuilder builder = new StringBuilder();
            
            string strResponse = reader.ReadLine();
            
            while (strResponse != ".") 
            {
                builder.Append(strResponse + Environment.NewLine);
                
                strResponse = reader.ReadLine();
            }

            return builder.ToString();
        }

    }
}
