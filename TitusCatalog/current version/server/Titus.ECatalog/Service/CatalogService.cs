using System;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Titus.ECatalog.Entity;
using Titus.ECatalog.Data;
using Titus.ECatalog.Util;
using ICSharpCode.SharpZipLib.Zip;

namespace Titus.ECatalog.Service
{

    public class CatalogService
    {

        private const string DEFAULT_PAGE_CODE = "A";

        private const string STORAGE_URL_CONFIG_KEY = "documentStorageURL";

        private const string ZIP_STORAGE_URL_CONFIG_KEY = "zipDocumentStorageURL";

        private const string ZIP_STORAGE_DIR_CONFIG_KEY = "zipDocumentStorageDir";

        private const String PDFSAM_CONFIG_KEY = "PDFSAMPath";

        private const string ZIP_FILE_EXT = ".zip";
        private const string ZIP_FILE_PREFIX = "Package_";
        private const int ZIP_LEVEL = 9;

        protected string GetZipStorageDir()
        {
            return ConfigurationManager.AppSettings[ZIP_STORAGE_DIR_CONFIG_KEY];
        }

        protected string GetZipStorageUrl()
        {
            return ConfigurationManager.AppSettings[ZIP_STORAGE_URL_CONFIG_KEY];
        }

        private void GetCatalogFile(string section, int startPage, int pagesCount, string directory)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    SectionDataObject sectionInfo = Section.GetInstance().FindBySection(tran, section);

                    ProcessStartInfo startInfo = new ProcessStartInfo("java.exe", "-jar "
                        + ConfigurationManager.AppSettings[PDFSAM_CONFIG_KEY]
                        + " -f \""
                        + sectionInfo.PdfPath + "\""
                        + " -o \""
                        + directory + "\" -overwrite split");
                    // StreamWriter outputWriter = new StreamWriter("pdfsam.log");
                    
                    Process pdfsamProcess = new Process();
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    pdfsamProcess.StartInfo = startInfo;
                    pdfsamProcess.Start();
                    
                    /* outputWriter.WriteLine(startInfo.Arguments);
                    outputWriter.Write(pdfsamProcess.StandardOutput.ReadToEnd());
                    outputWriter.Close(); */

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

        private void GetCatalogFiles(DirectoryInfo directory, List<CatalogPartDataObject> catalogParts)
        {
            foreach (CatalogPartDataObject catalogPartInfo in catalogParts)
            {
                GetCatalogFile(catalogPartInfo.Section, catalogPartInfo.PageNumber, catalogPartInfo.PagesCount, directory.FullName);
            }
        }

        public string PrepareCartPackage(List<SubmittalDataObject> submittals)
        {
            string result = GetZipStorageUrl().EndsWith("/") ? GetZipStorageUrl() : GetZipStorageUrl() + "/";

            DirectoryInfo directory = GetFilePackage(submittals);

            result += ZipDirectory(directory).Name;

            Directory.Delete(directory.FullName, true);

            return result;
        }
        
        public string PrepareCartPackage(CartDataObject cartInfo)
        {
            string result = GetZipStorageUrl().EndsWith("/") ? GetZipStorageUrl() : GetZipStorageUrl() + "/";

            DirectoryInfo directory = GetFilePackage(cartInfo.Submittals);

            GetCatalogFiles(directory, cartInfo.CatalogParts);

            result += ZipDirectory(directory).Name;

            Directory.Delete(directory.FullName, true);

            return result;
        }
        
        private DirectoryInfo GetFilePackage(List<SubmittalDataObject> submittals)
        {
            string uniqueDirName = Guid.NewGuid().ToString();
            DirectoryInfo result;
            
            try
            {
                result = Directory.CreateDirectory(Path.Combine(GetZipStorageDir(), uniqueDirName));
            } 
            catch (Exception ex)
            {
                throw new ApplicationException("Can not create temporary directory.\n" + ex.Message);
            }

            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                foreach (SubmittalDataObject submittalInfo in submittals)
                {
                    FileDataObject fileInfo = Titus.ECatalog.Data.File.GetInstance().FindByFileId(tran, submittalInfo.FileId);

                    FileStream outputFile = new FileStream(Path.Combine(result.FullName, fileInfo.FileName), FileMode.Create);
                    outputFile.Write(fileInfo.Data, 0, fileInfo.Data.Length);
                    outputFile.Close();
                }
            }

            return result;
        }
        
        private FileInfo ZipDirectory(DirectoryInfo directory)
        {
            FileStream zipFile = System.IO.File.Create(Path.Combine(directory.Parent.FullName, ZIP_FILE_PREFIX) + directory.Name + ZIP_FILE_EXT);

            using (ZipOutputStream zipOutput = new ZipOutputStream(zipFile))
            {
                zipOutput.SetLevel(ZIP_LEVEL);

                byte[] buffer = new byte[4096];

                foreach (FileInfo fileInfo in directory.GetFiles())
                {
                    ZipEntry entry = new ZipEntry(fileInfo.Name);

                    entry.DateTime = DateTime.Now;
                    zipOutput.PutNextEntry(entry);

                    using (FileStream fs = System.IO.File.OpenRead(fileInfo.FullName))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            zipOutput.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }

                zipOutput.Finish();
                zipOutput.Close();
            }

            return new FileInfo(zipFile.Name);
        }

        public void StoreModelItem(int modelId, int catalogLevel, string code)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    string section = code.Substring(0, 1);
                    string pageNumberString = code.Substring(1);
                    int pageNumber = Int32.Parse(pageNumberString);

                    SectionPageDataObject pageInfo = SectionPage.GetInstance().FindByCode(tran, section, pageNumber);

                    SectionItemDataObject itemInfo = SectionItem.GetInstance().FindById(tran, modelId, catalogLevel);
                    if (null == itemInfo)
                    {
                        itemInfo = new SectionItemDataObject();
                        itemInfo.ModelId = modelId;
                        itemInfo.SectionPageId = pageInfo.SectionPageId;
                        itemInfo.CatalogLevel = catalogLevel;
                        SectionItem.GetInstance().Insert(tran, itemInfo);
                    }
                    else
                    {
                        itemInfo.SectionPageId = pageInfo.SectionPageId;
                        itemInfo.ModelId = modelId;
                        itemInfo.CatalogLevel = catalogLevel;
                        SectionItem.GetInstance().Update(tran, itemInfo);
                    }

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

        public List<SectionPageDataObject> GetPagesInfo()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    List<SectionPageDataObject> pages = SectionPage.GetInstance().FindPages(tran);

                    tran.Commit();

                    return pages;
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

        public SectionDataObject GetSection(int modelId, int catalogLevel)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    SectionDataObject sectionInfo = Section.GetInstance().FindByModelAndLevel(tran, modelId, catalogLevel);

                    tran.Commit();

                    return sectionInfo;
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

        public List<SubmittalDataObject> GetSubmittals(int modelId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    List<SubmittalDataObject> submittals = Submittal.GetInstance().FindByModelId(tran, modelId);

                    tran.Commit();

                    return submittals;
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

        public List<SectionPageDataObject> GetSectionPages(int sectionId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    List<SectionPageDataObject> pages = SectionPage.GetInstance().FindBySectionId(tran, sectionId);

                    /* foreach (SectionPageDataObject pageInfo in pages)
                    {
                        pageInfo.Notes = Note.GetInstance().FindByPageId(tran, pageInfo.DocumentPageId);
                    } */

                    tran.Commit();

                    return pages;
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

        public string GetStorageURL()
        {
            return ConfigurationManager.AppSettings[STORAGE_URL_CONFIG_KEY];
        }

        public CatalogPackage GetCatalog()
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    CatalogPackage package = new CatalogPackage();

                    XmlReader reader;
                    using (reader = SqlHelper.ExecuteProcedureXML(tran, "GetTTSCatalogMenuNew_XML", new DbParameter[0] { }))
                    {
                        while (reader.Read())
                        {
                            if (("ROOT" == reader.Name.ToUpper())
                                    && (XmlNodeType.Element == reader.NodeType))
                            {
                                ParseRoot(reader, package);
                            }
                        }
                    }

                    List<CatalogItemDataObject> modelItems = CatalogItem.GetInstance().FindAll(tran);

                    Hashtable modelItemsHash = new Hashtable();
                    foreach (CatalogItemDataObject modelItemInfo in modelItems)
                    {
                        List<CatalogItemDataObject> hashedModelItems = (List<CatalogItemDataObject>)modelItemsHash[(int)modelItemInfo.ParentId];
                        if (null == hashedModelItems)
                        {
                            hashedModelItems = new List<CatalogItemDataObject>();
                            modelItemsHash[(int)modelItemInfo.ParentId] = hashedModelItems;
                        }
                        hashedModelItems.Add(modelItemInfo);
                    }

                    List<SectionDataObject> sections = Section.GetInstance().FindByCatalogId(tran, 1);
                    foreach (SectionDataObject sectionInfo in sections)
                    {
                        List<SectionPageDataObject> pages = SectionPage.GetInstance().FindBySectionId(tran, sectionInfo.SectionId);
                        package.Pages.AddRange(pages);

                        package.Sections.Add(sectionInfo);
                    }

                    foreach (CatalogItemDataObject categoryInfo in package.Categories)
                    {
                        categoryInfo.CatalogLevel = 1;
                        CalculateLocation(tran, categoryInfo);

                        foreach (CatalogItemDataObject subCategoryInfo in categoryInfo.SubItems)
                        {
                            subCategoryInfo.CatalogLevel = 2;
                            CalculateLocation(tran, subCategoryInfo);

                            foreach (CatalogItemDataObject productInfo in subCategoryInfo.SubItems)
                            {
                                productInfo.CatalogLevel = 3;
                                CalculateLocation(tran, productInfo);

                                List<CatalogItemDataObject> hashedModelItems = (List<CatalogItemDataObject>)modelItemsHash[productInfo.ModelId];
                                if (null != hashedModelItems)
                                {
                                    productInfo.IsBranch = true;

                                    foreach (CatalogItemDataObject modelItemInfo in hashedModelItems)
                                    {
                                        modelItemInfo.CatalogLevel = 4;
                                        modelItemInfo.IsBranch = false;
                                        CalculateLocation(tran, modelItemInfo);
                                        if (0 >= modelItemInfo.PageNumber)
                                        {
                                            modelItemInfo.PageNumber = productInfo.PageNumber;
                                            modelItemInfo.PageCode = productInfo.PageCode;
                                        }


                                        productInfo.SubItems.Add(modelItemInfo);
                                    }
                                }
                                else
                                {
                                    productInfo.IsBranch = false;
                                }
                            }
                        }
                    }

                    tran.Commit();

                    return package;
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

        public FileDataObject GetFile(int fileId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    FileDataObject fileInfo = Titus.ECatalog.Data.File.GetInstance().FindByFileId(tran, fileId);

                    tran.Commit();

                    return fileInfo;
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

        public void FixCatalog()
        {
            CatalogPackage package = GetCatalog();

            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    FixCatalogLevel(tran, package.Categories);

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

        private void FixCatalogLevel(SqlTransaction tran, List<CatalogItemDataObject> catalogItems)
        {
            foreach (CatalogItemDataObject itemInfo in catalogItems)
            {
                SectionItemDataObject sectionInfo = SectionItem.GetInstance().FindById(tran, itemInfo.ModelId, itemInfo.CatalogLevel);
                if (null == sectionInfo)
                {
                    List<SectionItemDataObject> sectionItems = SectionItem.GetInstance().FindByModelId(tran, itemInfo.ModelId);
                    foreach (SectionItemDataObject sectionItemToFix in sectionItems)
                    {
                        if (4 == sectionItemToFix.CatalogLevel)
                        {
                            continue;
                        }

                        sectionItemToFix.CatalogLevel = itemInfo.CatalogLevel;
                        SectionItem.GetInstance().Update(tran, sectionItemToFix);
                    }
                }

                FixCatalogLevel(tran, itemInfo.SubItems);
            }
        }

        private void ParseRoot(XmlReader reader, CatalogPackage package)
        {
            while (reader.Read())
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    if ("CATEGORY" == reader.Name.ToUpper())
                    {
                        CatalogItemDataObject categoryInfo = new CatalogItemDataObject();

                        ParseCategory(reader, package, categoryInfo);

                        package.Categories.Add(categoryInfo);
                    }
                }

                if ((XmlNodeType.EndElement == reader.NodeType)
                        && ("ROOT" == reader.Name.ToUpper()))
                {
                    break;
                }
            }
        }

        private void ParseCategory(XmlReader reader, CatalogPackage package, CatalogItemDataObject categoryInfo)
        {
            categoryInfo.Name = reader.GetAttribute("catname");
            categoryInfo.ModelId = (int)Int64.Parse(reader.GetAttribute("catid"));
            categoryInfo.Sort = Int32.Parse(reader.GetAttribute("sort"));
            /* categoryInfo.Color1 = reader.GetAttribute("catimage");
            categoryInfo.Color2 = reader.GetAttribute("catimagehover"); */

            while (reader.Read())
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    if ("PRODUCT" == reader.Name.ToUpper())
                    {
                        CatalogItemDataObject subCategoryInfo = new CatalogItemDataObject();

                        ParseSubCategory(reader, package, subCategoryInfo);

                        categoryInfo.SubItems.Add(subCategoryInfo);
                    }
                }

                if ((XmlNodeType.EndElement == reader.NodeType)
                        && ("CATEGORY" == reader.Name.ToUpper()))
                {
                    break;
                }
            }
        }

        private void ParseSubCategory(XmlReader reader, CatalogPackage package, CatalogItemDataObject subCategoryInfo)
        {
            subCategoryInfo.Name = reader.GetAttribute("prodname");
            subCategoryInfo.ModelId = (int)Int64.Parse(reader.GetAttribute("prodid"));
            subCategoryInfo.Sort = Int32.Parse(reader.GetAttribute("sort"));
            /* subCategoryInfo.Color1 = reader.GetAttribute("prodBg");
            subCategoryInfo.Color2 = reader.GetAttribute("prodHover"); */

            if (reader.IsEmptyElement)
            {
                subCategoryInfo.IsBranch = false;
                return;
            }
            else
            {
                while (reader.Read())
                {
                    if (XmlNodeType.Element == reader.NodeType)
                    {
                        if ("MODELS" == reader.Name.ToUpper())
                        {
                            CatalogItemDataObject productInfo = new CatalogItemDataObject();

                            ParseProduct(reader, package, productInfo);

                            subCategoryInfo.SubItems.Add(productInfo);
                        }
                    }

                    if ((XmlNodeType.EndElement == reader.NodeType)
                            && ("PRODUCT" == reader.Name.ToUpper()))
                    {
                        break;
                    }
                }
            }
        }

        private void ParseProduct(XmlReader reader, CatalogPackage package, CatalogItemDataObject productInfo)
        {
            productInfo.Name = reader.GetAttribute("model");
            productInfo.ModelId = (int)Int64.Parse(reader.GetAttribute("modelid"));
            /* productInfo.Color1 = reader.GetAttribute("modBg");
            productInfo.Color2 = reader.GetAttribute("modHover"); */
            productInfo.IsBranch = false;

            if (reader.IsEmptyElement)
            {
                return;
            }
            else
            {
                while (reader.Read())
                {
                    if ((XmlNodeType.EndElement == reader.NodeType)
                            && ("MODELS" == reader.Name.ToUpper()))
                    {
                        break;
                    }
                }
            }
        }

        private void CalculateLocation(SqlTransaction tran, CatalogItemDataObject item)
        {
            CatalogLocationDataObject location = SectionItem.GetInstance().GetItemLocation(tran, item.ModelId, item.CatalogLevel);
            if (null == location)
            {
                return;
            }
            item.PageCode = location.Section;
            item.PageNumber = location.CatalogPage;
        }

    }

}
