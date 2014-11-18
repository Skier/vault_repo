using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace Weborb.Management.CodeGen
{
    public abstract class CodeItem
    {
        private String m_name;

        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private CodeDirectory m_directoty;

        public CodeDirectory Directory
        {
            get { return m_directoty; }
            set { m_directoty = value; }
        }

        internal bool IsDirectory()
        {
            return this is CodeDirectory;
        }

        internal bool IsFile()
        {
            return this is CodeFile;
        }

        internal String GetPath()
        {
            String path = Name;
            CodeDirectory codeDirectory = Directory;

            while (codeDirectory != null)
            {
                path = String.Format("{0}/{1}", codeDirectory.Name, path);
                codeDirectory = codeDirectory.Directory;
            }

            if (IsDirectory())
                path += "/";

            return path;
        }

        public static void SaveToZip(CodeItem codeItem, Stream stream)
        {
            ZipOutputStream zipOutput = new ZipOutputStream(stream);

            CreateZip(codeItem, zipOutput);

            zipOutput.Finish();
        }

        private static void CreateZip(CodeItem codeItem, ZipOutputStream zipOutput)
        {
            ZipEntry zipEntry = new ZipEntry(codeItem.GetPath());
            zipOutput.PutNextEntry(zipEntry);

            if (codeItem.IsFile())
            {
                Byte[] code = System.Text.ASCIIEncoding.ASCII.GetBytes(((CodeFile)codeItem).Content);

                zipOutput.Write(code, 0, code.Length);
            }
            else
            {
                foreach (CodeItem childItem in ((CodeDirectory)codeItem).Items)
                    CreateZip(childItem, zipOutput);
            }

        }
    }
}
