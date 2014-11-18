using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

using Weborb.Management.ServiceBrowser;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections;

namespace Weborb.Management.CodeGen
{
    internal class Codegenerator:IDisposable
    {
        public const int FLEX_REMOTING_AS3 = 0;
        public const int FLEX_REMOTING_AS2 = 1;
        public const int FLEX_REMOTING_AS2_INLINE = 2;
        public const int ARP_FRAMEWORK = 3;
        public const int CAIRNGORM_FRAMEWORK = 4;
        public const int FLASHCOMM_FMS2 = 5;
        public const int AJAX_CLIENT = 6;

        static Dictionary<int, String> s_templateMap = new Dictionary<int, string>();


        static Codegenerator()
        {
            s_templateMap.Add( 0, "flex-remoting-as3.xslt" );
            s_templateMap.Add( 1, "flex-remoting-as2.xslt" );
            s_templateMap.Add( 2, "flex-remoting-as2-inline.xslt" );
            s_templateMap.Add( 3, "arp-framework.xslt" );
            s_templateMap.Add( 4, "cairngorm-framework.xslt" );
            s_templateMap.Add( 5, "flashcomm-fms2.xslt" );
            s_templateMap.Add( 6, "ajax-client.xslt" );
        }

        String m_xmlFilePath ;
        String m_ouputFilePath ;

        CodegeneratorResult m_codegeneratorResult;

        XslCompiledTransform m_xslTrasform;

        public Codegenerator()
        {
            m_xmlFilePath = getTempFilePath();
            m_ouputFilePath = getTempFilePath();
            m_xslTrasform = new XslCompiledTransform();
        }

        static String getOutputFileName(String serviceName)
        {
            return String.Format("{0}.as", serviceName);
        }

        static String getTemplatePath( int type )
        {
            return String.Format( "{0}\\{1}", Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "codegen" ), s_templateMap[ type ] );
        }

        static String getTempFilePath()
        {
            return String.Format( "{0}\\{1}.codegen", Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "codegen" ), Guid.NewGuid().ToString() );
        }

        public CodegeneratorResult Generate( ServiceBrowser.Service service, int type, ArrayList args, String weborbUrl)
        {
            ServiceSerializer serializer = new ServiceSerializer();
            m_codegeneratorResult = new CodegeneratorResult();
            
            serializer.WriteItem += delegate(XmlTextWriter writer,  ServiceNode serviceNide, ServiceSerializerItemType itemType)
            {
                if (itemType == ServiceSerializerItemType.Service)
                    writer.WriteAttributeString("url", weborbUrl);
            };

            serializer.Serialize(service, args,m_xmlFilePath);

            m_codegeneratorResult.Result = Generate(type);

            return m_codegeneratorResult;
        }

        private CodeItem Generate(int type)
        {

            m_xslTrasform.Load(getTemplatePath(type));
            m_xslTrasform.Transform(m_xmlFilePath, m_ouputFilePath);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(m_ouputFilePath);

            return LoadCodeFromXml(xmlDocument.DocumentElement, null);
        }

        private CodeItem LoadCodeFromXml(XmlElement xmlElement,CodeDirectory parent)
        { 
            CodeItem codeItem = null;
             
            if (xmlElement.Name == "folder")
                codeItem = new CodeDirectory();
            else
                codeItem = new CodeFile();

            codeItem.Directory = parent;
            codeItem.Name = xmlElement.GetAttribute("name");

            if (codeItem.IsDirectory())
            {
                CodeDirectory codeDirectory = (CodeDirectory)codeItem;

                foreach (XmlElement xmlChild in xmlElement.ChildNodes)
                {
                    if (xmlChild.Name.Equals("folder") || xmlChild.Name.Equals("file"))
                        codeDirectory.Items.Add(LoadCodeFromXml(xmlChild, codeDirectory));
                    else if (xmlChild.Name.Equals("info"))
                        m_codegeneratorResult.Info = xmlChild.InnerText.Replace("\r\n", "\n"); ;

                }
            }
            else
            {
                CodeFile codeFile = (CodeFile)codeItem;
                codeFile.Content = xmlElement.InnerText.Replace("\r\n", "\n");
            }


            return codeItem;
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                File.Delete(m_xmlFilePath);
                File.Delete(m_ouputFilePath);
            }
            catch { }
        }

        #endregion
    }
}
