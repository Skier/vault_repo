using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Xsl;
using System.Xml;

namespace Servman.Codegen
{
    public class CodeGenerator
    {

        public delegate void ProgressEventHandler(String message);

        public event ProgressEventHandler Progress;

        public String OutputRootDirectory = ".";
        public String XsltFilePath = "codegen.xslt";
        public String TempFilePath = "temp.xml";
        public String DbSchemaFilePath = "dbschema.xsd";

        public bool GenerateEmptyClass = true;
        public bool GenerateActionScriptClass = true;

        public void Generate()
        {
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            XmlTextWriter xmlWriter = new XmlTextWriter(TempFilePath, System.Text.Encoding.UTF8);
            XmlTextReader xmlReader = new XmlTextReader(DbSchemaFilePath);
            XsltArgumentList args = new XsltArgumentList();


            xslTransform.Load(XsltFilePath);

            args.AddExtensionObject("urn:cogegen-xslt-lib:xslt", new XsltExtention(this));

            xslTransform.Transform(xmlReader, args, xmlWriter, null);

            xmlWriter.Close();
            xmlReader.Close();


            Deploy();

            File.Delete(TempFilePath);
        }


        private void Deploy()
        {
            XmlDocument xmlDocument = new XmlDocument();


            xmlDocument.Load(TempFilePath);

            foreach (XmlElement xmlElement in xmlDocument.DocumentElement.SelectNodes("file"))
            {

                String filePath = String.Format(@"{0}\{1}", OutputRootDirectory, xmlElement.GetAttribute("name"));

                if (File.Exists(filePath))
                {
                    if (Boolean.Parse(xmlElement.GetAttribute("overwrite")))
                        File.Delete(filePath);
                    else
                    {
                        AddMessage("Skiped file " + filePath);
                        continue;
                    }
                }


                using (StreamWriter writer = new StreamWriter(File.OpenWrite(filePath)))
                {
                    writer.Write(xmlElement.InnerText);
                }
            }
         

        }

        internal void AddMessage(string message)
        {
            if (Progress != null)
                Progress.Invoke(message);
        }
    }
}
