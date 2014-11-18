using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Weborb.Management.CodeGen;
using Weborb.Management.ServiceBrowser;
using Weborb.Util;
using System.Collections;

namespace Weborb.Management
{
    public class ManagementService
    {

        public static String getCurrentUrl(bool includePage)
        {
            String absoluteUri = ThreadContext.currentRequest().Url.AbsoluteUri;

            if (includePage)
                return absoluteUri;

            return absoluteUri.Substring(0, absoluteUri.LastIndexOf('/')+1);
        }

        public List<ServiceNode> getServices()
        {
            ServiceScanner serviceScanner = new ServiceScanner();
            List<ServiceNode> serviceNodeList = serviceScanner.getServices();

            ServiceNode.Sort(serviceNodeList);

            return serviceNodeList;
        }

        public CodegeneratorResult generateCode(String className, ArrayList args, int type, bool saveOnServer)
        {
            ServiceScanner serviceScanner = new ServiceScanner();

            ServiceBrowser.Service service = serviceScanner.loadService(className.Split('#')[0]);

            if (service == null)
                throw new Exception(String.Format("Service {0} not found", className));

            if (className.IndexOf('#') != -1)
            {
                String methodName = className.Split('#')[1];

                foreach (ServiceMethod serviceMethod in service.Items)
                {
                    if (serviceMethod.Name == methodName)
                    {
                        //serviceMethod.BindArgs(args);
                        serviceMethod.Called = true;

                        break;
                    }
                }
            }

            CodegeneratorResult codegeneratorOutput = null;

            using (Codegenerator codegen = new Codegenerator())
            {
                codegeneratorOutput = codegen.Generate(service, type, args, getCurrentUrl(true));
            }



            if (saveOnServer)
            {
                codegeneratorOutput.DownloadUri = Guid.NewGuid().ToString();

                String tempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");

                if (!Directory.Exists(tempDirectory))
                    Directory.CreateDirectory(tempDirectory);

                using (FileStream fileStream = File.OpenWrite(getZipFilePath(codegeneratorOutput.DownloadUri)))
                {
                    CodeItem.SaveToZip(codegeneratorOutput.Result, fileStream);
                }

                codegeneratorOutput.SavedOnServer = saveOnServer;
            }

            return codegeneratorOutput;
        }

        internal static string getZipFilePath(String uri)
        {
            return String.Format("{0}\\{1}.zip",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp"),
                uri);

        }
    }
}
