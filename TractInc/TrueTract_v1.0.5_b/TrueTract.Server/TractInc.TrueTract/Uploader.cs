using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace TractInc.TrueTract
{
    
        class Uploader : IHttpHandler
        {
            private const String STORAGE_DIR_CONFIG_KEY = "TrueTractFileStorageDir";
            private const String STORAGE_URL_CONFIG_KEY = "TrueTractFileStorageUrl";
            private const String UPLOADER_URL = "/TrueTractFileUploader.aspx";

            private const String UPLOAD_DIR_PARAM = "UploadDir";
            private const String UNIQUE_KEY_PARAM = "UniqueKey";

            public static String StorageDir
            {
                get { return ConfigurationManager.AppSettings[STORAGE_DIR_CONFIG_KEY]; }
            }

            public static String StorageUrl
            {
                get { return ConfigurationManager.AppSettings[STORAGE_URL_CONFIG_KEY]; }
            }

            public static String UploaderUrl
            {
                get { return HttpUrlHelper.AbsoluteRoot + UPLOADER_URL; }
            }

            public void ProcessRequest(HttpContext context)
            {
                if (context.Request.Files.Count > 0)
                {
                    String directory = StorageDir;

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    if (!directory.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
                        directory += Path.DirectorySeparatorChar;

                    foreach (String key in context.Request.Files.Keys)
                    {
                        HttpPostedFile file = context.Request.Files[key];

                        if (file.ContentLength > 0)
                        {
                            String subDirectory = context.Request.Params[UPLOAD_DIR_PARAM];

                            if (subDirectory == null)
                                throw new InvalidDataException("Request parameters does not contain " + UPLOAD_DIR_PARAM + " parameter.");

                            if (!subDirectory.EndsWith(new String(Path.DirectorySeparatorChar, 1)))
                                subDirectory += Path.DirectorySeparatorChar;

                            if (!Directory.Exists(directory + subDirectory))
                                Directory.CreateDirectory(directory + subDirectory);

                            String fileName = file.FileName;
                            int index = fileName.LastIndexOf(Path.DirectorySeparatorChar);

                            if (index != -1)
                            {
                                if (index != fileName.Length - 1)
                                    fileName = fileName.Substring(index + 1);
                                else
                                    throw new NotSupportedException("File name is invalid: " + fileName);
                            }

                            String keyString = context.Request.Params[UNIQUE_KEY_PARAM];
                            
                            fileName = directory + subDirectory + keyString + fileName;

                            String rndName = fileName;

                            while (File.Exists(rndName))
                            {
                                rndName = fileName + "[" + (new Random(DateTime.Now.Second)).Next(1000) + "]";
                            }

                            fileName = rndName;

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
