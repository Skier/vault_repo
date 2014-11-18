using System;
using System.Text;

namespace TestGmailSpeed
{
    class Pop3Client1 : APop3Client
    {

        static string contentEndSign = Environment.NewLine + "." + Environment.NewLine;
        
        protected override string ReadLine() {
            StringBuilder resultBuilder = new StringBuilder();

            byte[] bytes = new byte[1];
            
            while ((stream.Read(bytes, 0, 1)) != 0) {
                
                resultBuilder.Append( Encoding.Default.GetString(bytes));
                if (resultBuilder.ToString().IndexOf(Environment.NewLine) != -1) {
                    break;
                }
            }
            
            return resultBuilder.ToString();
        }

        protected override string ReadContent() {
            
            StringBuilder resultBuilder = new StringBuilder();
            
            byte[] bytes = new byte[1];
            
            while (true) {
                
                stream.Read(bytes, 0, 1);
                
                resultBuilder.Append(Encoding.Default.GetString(bytes));
                
                if (resultBuilder.ToString().EndsWith(contentEndSign)) {
                    break;
                }
            }
            
            return resultBuilder.ToString();
        }
    }
}
