using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using System.Diagnostics;
using Titus.ECatalog.Data;
using Titus.ECatalog.Entity;
using Titus.ECatalog.Util;

namespace Titus.ECatalog.Service
{
    
        public class UploaderService : IHttpHandler
        {
            private const String STORAGE_DIR_CONFIG_KEY = "XMLViewStorageDir";
            private const String STORAGE_URL_CONFIG_KEY = "XMLViewStorageUrl";
            private const String UPLOADER_URL = "/TCFileUploader.aspx";

            private const String UPLOAD_DIR_PARAM = "UploadDir";
            private const String UNIQUE_KEY_PARAM = "UniqueKey";

            private const String PDF2PPM_CONFIG_KEY = "PDF2PPMPath";
            private const String PDF2XML_CONFIG_KEY = "PDF2XMLPath";
            private const String IVIEW32_CONFIG_KEY = "IVIEW32Path";
            private const String WORKDIR_CONFIG_KEY = "XMLViewWorkDir";

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

            public String GetStorageUrl()
            {
                return UploaderService.StorageUrl;
            }

            public String GetUploaderUrl()
            {
                return UploaderService.UploaderUrl;
            }

            public void ProcessRequest(HttpContext context)
            {
                /* if (context.Request.Files.Count > 0)
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

                            fileName = fileName.Replace(' ', '_');

                            using (FileStream fileStream = new FileStream(fileName, FileMode.CreateNew))
                            {
                                fileStream.Write(data, 0, data.Length);
                                fileStream.Close();
                            }

                            DocumentDataObject documentInfo = new DocumentDataObject();
                            documentInfo.Path = fileName;

                            using (SqlConnection conn = SqlHelper.CreateConnection())
                            {
                                conn.Open();
                                SqlTransaction tran = conn.BeginTransaction();

                                try
                                {
                                    Document.GetInstance().Insert(tran, documentInfo);

                                    tran.Commit();
                                }
                                catch (SqlException ex)
                                {
                                    try
                                    {
                                        tran.Rollback();
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    throw ex;
                                }
                            }
                        }
                    }
                } */
            }

            public bool IsReusable
            {
                get { return true; }
            }
/*
            public List<DocumentDataObject> ProcessDocument(ModelDataObject[] models)
            {
                using (SqlConnection conn = SqlHelper.CreateConnection())
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();

                    try
                    {
                        List<DocumentDataObject> documents = Document.GetInstance().FindNotProcessed(tran);

                        foreach (DocumentDataObject documentInfo in documents)
                        {
                            StreamWriter outputWriter = new StreamWriter(ConfigurationManager.AppSettings[WORKDIR_CONFIG_KEY] + "external.log");

                            ProcessStartInfo startInfo = new ProcessStartInfo(
                                ConfigurationManager.AppSettings[PDF2XML_CONFIG_KEY],
                                "-noImage -noImageInline "
                                + documentInfo.Path);

                            Process pdf2xmlProcess = new Process();
                            startInfo.UseShellExecute = false;
                            startInfo.RedirectStandardError = true;
                            pdf2xmlProcess.StartInfo = startInfo;
                            pdf2xmlProcess.Start();

                            outputWriter.WriteLine(startInfo.Arguments);
                            outputWriter.Write(pdf2xmlProcess.StandardError.ReadToEnd());

                            string imagesDir = Path.GetDirectoryName(documentInfo.Path) + "\\" + documentInfo.DocumentId.ToString();
                            if (!Directory.Exists(imagesDir))
                            {
                                Directory.CreateDirectory(imagesDir);
                            }

                            startInfo = new ProcessStartInfo(
                                ConfigurationManager.AppSettings[PDF2PPM_CONFIG_KEY],
                                documentInfo.Path + " " + imagesDir + "\\");

                            Process pdf2ppmProcess = new Process();
                            startInfo.UseShellExecute = false;
                            startInfo.RedirectStandardError = true;
                            pdf2ppmProcess.StartInfo = startInfo;
                            pdf2ppmProcess.Start();

                            outputWriter.WriteLine(startInfo.Arguments);
                            outputWriter.Write(pdf2ppmProcess.StandardError.ReadToEnd());

                            outputWriter.Close();

                            ParseCatalog(tran, documentInfo.DocumentId);

                            // LinkPages(tran, documentInfo.DocumentId, models);
                        }

                        tran.Commit();

                        return documents;
                    }
                    catch (SqlException ex)
                    {
                        try
                        {
                            tran.Rollback();
                        }
                        catch (Exception)
                        {
                        }

                        throw ex;
                    }
                }
            }

            private void LinkPages(SqlTransaction tran, int documentId, ModelDataObject[] models)
            {
                List<PageDataObject> pages = Page.GetInstance().FindByDocumentId(tran, documentId);
                Hashtable pagesHash = new Hashtable();

                foreach (PageDataObject pageInfo in pages)
                {
                    pagesHash[pageInfo.PageNumber] = pageInfo;
                }

                foreach (ModelDataObject modelInfo in models)
                {
                    if (null == pagesHash[modelInfo.PageId])
                    {
                        continue;
                    }
                    modelInfo.PageId = ((PageDataObject)pagesHash[modelInfo.PageId]).DocumentPageId;
                    Model.GetInstance().Insert(tran, modelInfo);
                }
            }

            private SectionDataObject ParseCatalog(SqlTransaction tran, int id)
            {
                SectionDataObject sectionInfo = Section.GetInstance().FindById(tran, id);

                FileStream stream = new FileStream(Path.ChangeExtension(documentInfo.Path, ".xml"), FileMode.Open, FileAccess.Read);

                using (XmlTextReader reader = new XmlTextReader(stream))
                {
                    while (reader.Read())
                    {
                        if (("DOCUMENT" == reader.Name.ToUpper())
                                && (XmlNodeType.Element == reader.NodeType))
                        {
                            ParseDocument(reader, tran, sectionInfo);
                        }
                    }
                }

                SectionGetInstance().Update(tran, sectionInfo);

                return documentInfo;
            }

            public SearchResultDataObject SearchText(string text)
            {
                return null;
            }

            private void ParseDocument(XmlTextReader reader, SqlTransaction tran, SectionDataObject sectionInfo)
            {
                int pageCounter = 0;

                while (reader.Read())
                {
                    if (XmlNodeType.Element != reader.NodeType)
                    {
                        continue;
                    }

                    if ("METADATA" == reader.Name.ToUpper())
                    {
                        ParseMetadata(reader, sectionInfo);
                    }
                    else if ("PAGE" == reader.Name.ToUpper())
                    {
                        pageCounter++;

                        SectionPageDataObject pageInfo = new SectionPageDataObject();
                        pageInfo.SectionPageNumber = pageCounter;
                        pageInfo.ScreenshotURL =
                            Path.GetDirectoryName(sectionInfo.Path)
                            + "\\" + sectionInfo.DocumentId.ToString()
                            + "\\" + pageCounter.ToString("-000000") + ".png";
                        pageInfo.SectionId = sectionInfo.SectionId;

                        ProcessStartInfo startInfo = new ProcessStartInfo(
                            ConfigurationManager.AppSettings[IVIEW32_CONFIG_KEY],
                            Path.ChangeExtension(pageInfo.ScreenshotURL, ".ppm")
                            + " /convert=" + pageInfo.ScreenshotURL);

                        Process iview32Process = new Process();
                        startInfo.UseShellExecute = false;
                        iview32Process.StartInfo = startInfo;
                        iview32Process.Start();

                        pageInfo.ScreenshotURL =
                            UploaderService.StorageUrl + "PDF/"
                            + sectionInfo.SectionId.ToString()
                            + "/" + Path.GetFileName(pageInfo.ScreenshotURL);

                        // !!! ParsePage(reader, tran, pageInfo);

                        sectionInfo.Pages.Add(pageInfo);
                    }
                }
            }

            private void ParseMetadata(XmlTextReader reader, SectionDataObject sectionInfo)
            {
                while (reader.Read())
                {
                    if (XmlNodeType.Element != reader.NodeType)
                    {
                        continue;
                    }

                    if ("PDFFILENAME" == reader.Name.ToUpper())
                    {
                        reader.Read();

                        sectionInfo.Name = reader.ReadContentAsString();

                        break;
                    }
                }
            }

            private void ParsePage(XmlTextReader reader, SqlTransaction tran, SectionPageDataObject pageInfo)
            {
                pageInfo.Width = Int32.Parse(reader.GetAttribute("width"));
                pageInfo.Height = Int32.Parse(reader.GetAttribute("height"));

                SectionPage.GetInstance().Insert(tran, pageInfo);

                while (reader.Read())
                {
                    // if (XmlNodeType.Element == reader.NodeType)
                    // {
                    //    if ("TEXT" == reader.Name.ToUpper())
                    //    {
                    //        ParseText(reader, tran, pageInfo);
                    //    }
                    // }

                    if ((XmlNodeType.EndElement == reader.NodeType)
                            && ("PAGE" == reader.Name.ToUpper()))
                    {
                        break;
                    }
                }
            }

            private void ParseText(XmlTextReader reader, SqlTransaction tran, SectionPageDataObject pageInfo)
            {
                while (reader.Read())
                {
                    if (XmlNodeType.Element == reader.NodeType)
                    {
                        if ("TOKEN" == reader.Name.ToUpper())
                        {
                            TokenDataObject tokenInfo = new TokenDataObject();

                            ParseToken(reader, tran, tokenInfo);

                            pageInfo.Tokens.Add(tokenInfo);

                            tokenInfo.SectionPageId = pageInfo.SectionPageId;
                            Token.GetInstance().Insert(tran, tokenInfo);
                        }
                    }

                    if ((XmlNodeType.EndElement == reader.NodeType)
                            && ("TEXT" == reader.Name.ToUpper()))
                    {
                        break;
                    }
                }
            }

            private void ParseToken(XmlTextReader reader, SqlTransaction tran, TokenDataObject tokenInfo)
            {
                tokenInfo.Top = Int32.Parse(reader.GetAttribute("y"));
                tokenInfo.Left = Int32.Parse(reader.GetAttribute("x"));
                tokenInfo.Width = Int32.Parse(reader.GetAttribute("width"));
                tokenInfo.Height = Int32.Parse(reader.GetAttribute("height"));

                while (reader.Read())
                {
                    if (XmlNodeType.Text == reader.NodeType)
                    {
                        tokenInfo.Text = reader.ReadString();
                    }

                    if ((XmlNodeType.EndElement == reader.NodeType)
                            && ("TOKEN" == reader.Name.ToUpper()))
                    {
                        break;
                    }
                }
            }
*/
        }

}
