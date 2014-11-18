using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Weborb.Management.ServiceBrowser;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace Weborb.Management.CodeGen
{
    public class CodegeneratorHttpHandler:IHttpHandler
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "binary/octet-stream";
            context.Response.AppendHeader("Content-Disposition",
                String.Format("attachment;filename={0}",
                "weborb.codegen.zip"));

            String serviceName = context.Request["service"];

            if (context.Request["uri"] != null)
            {
                String filePath = ManagementService.getZipFilePath(context.Request["uri"]);

                using (FileStream stream = File.OpenRead(filePath))
                {

                    WriteStream(context, stream);
                }

                return;
            }

            int type = int.Parse(context.Request["type"]);

            ServiceScanner serviceScanner = new ServiceScanner();
            Weborb.Management.ServiceBrowser.Service service = serviceScanner.loadService(serviceName);


            CodegeneratorResult codegenResult = null;

            using (Codegenerator codegen = new Codegenerator())
            {
                codegenResult = codegen.Generate(service, type, null, context.Request.Url.AbsoluteUri);
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                CodeItem.SaveToZip(codegenResult.Result, memoryStream);

                WriteStream(context, memoryStream);
            }
            
        }

        #endregion

        private void WriteStream(HttpContext context, Stream stream)
        { 
                byte[] buffer = new byte[100];
                int i = 0;
                stream.Position = 0;

                while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
                    context.Response.OutputStream.Write( buffer, 0, i);
        }

    }
}
