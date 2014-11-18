using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Weborb.Samples.Email
{
    class Uploader : IHttpHandler
    {
        const string ATTACHMENT_DIR_UID_PARAM = "AttachmentDirUid";
        
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory + "attachments";

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (!directory.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
                    directory += Path.DirectorySeparatorChar;

                foreach (string key in context.Request.Files.Keys)
                {
                    HttpPostedFile file = context.Request.Files[key];

                    if (file.ContentLength > 0)
                    {
                        // Create subdirectory
                        string subDirectory = context.Request.Params[ATTACHMENT_DIR_UID_PARAM];
                        if (subDirectory == null)
                            throw new InvalidDataException("Request parameters does not contain " + ATTACHMENT_DIR_UID_PARAM + " parameter.");
                        if (!subDirectory.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
                            subDirectory += Path.DirectorySeparatorChar;
                        if (!Directory.Exists(directory + subDirectory))
                            Directory.CreateDirectory(directory + subDirectory);

                        // Get file name from the request
                        string fileName = file.FileName;
                        int index = fileName.LastIndexOf(Path.DirectorySeparatorChar);
                        if (index != -1)
                            if (index != fileName.Length - 1)
                                fileName = fileName.Substring(index + 1);
                            else
                                throw new NotSupportedException("File name is invalid: " + fileName);
                        fileName = directory + subDirectory + fileName;

                        // Check for file name uniquness.
                        string rndName = fileName;
                        while (File.Exists(rndName))
                            rndName = fileName + "[" + (new Random(DateTime.Now.Second)).Next(1000) + "]";
                        fileName = rndName;

                        // Write file to the subdirectory
                        byte[] data = new byte[file.ContentLength];
                        file.InputStream.Read(data, 0, file.ContentLength);
                        using (FileStream fileStream = new FileStream(fileName, FileMode.CreateNew))
                        {
                            fileStream.Write(data, 0, data.Length);
                            fileStream.Close();
                        }
                    }
                }
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
