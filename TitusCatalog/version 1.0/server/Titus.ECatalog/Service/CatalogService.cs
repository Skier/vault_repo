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

namespace Titus.ECatalog.Service
{

    public class CatalogService
    {

        public DocumentDataObject GetDocument(int modelId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    DocumentDataObject documentInfo = Document.GetInstance().FindByModelId(tran, modelId);

                    tran.Commit();

                    return documentInfo;
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

        public List<PageDataObject> GetDocumentPages(int documentId)
        {
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    List<PageDataObject> pages = Page.GetInstance().FindByDocumentId(tran, documentId);

                    foreach (PageDataObject pageInfo in pages)
                    {
                        pageInfo.Notes = Note.GetInstance().FindByPageId(tran, pageInfo.DocumentPageId);
                    }

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

        private void ParseRoot(XmlReader reader, CatalogPackage package)
        {
            while (reader.Read())
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    if ("CATEGORY" == reader.Name.ToUpper())
                    {
                        CatalogItem categoryInfo = new CatalogItem();

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

        private void ParseCategory(XmlReader reader, CatalogPackage package, CatalogItem categoryInfo)
        {
            categoryInfo.Name = reader.GetAttribute("catname");
            categoryInfo.Id = (int)Int64.Parse(reader.GetAttribute("catid"));
            categoryInfo.Sort = Int32.Parse(reader.GetAttribute("sort"));
            categoryInfo.Color1 = reader.GetAttribute("catimage");
            categoryInfo.Color2 = reader.GetAttribute("catimagehover");

            while (reader.Read())
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    if ("PRODUCT" == reader.Name.ToUpper())
                    {
                        CatalogItem subCategoryInfo = new CatalogItem();

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

        private void ParseSubCategory(XmlReader reader, CatalogPackage package, CatalogItem subCategoryInfo)
        {
            subCategoryInfo.Name = reader.GetAttribute("prodname");
            subCategoryInfo.Id = (int)Int64.Parse(reader.GetAttribute("prodid"));
            subCategoryInfo.Sort = Int32.Parse(reader.GetAttribute("sort"));
            subCategoryInfo.Color1 = reader.GetAttribute("prodBg");
            subCategoryInfo.Color2 = reader.GetAttribute("prodHover");

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
                            CatalogItem productInfo = new CatalogItem();

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

        private void ParseProduct(XmlReader reader, CatalogPackage package, CatalogItem productInfo)
        {
            productInfo.Name = reader.GetAttribute("model");
            productInfo.Id = (int)Int64.Parse(reader.GetAttribute("modelid"));
            productInfo.Color1 = reader.GetAttribute("modBg");
            productInfo.Color2 = reader.GetAttribute("modHover");
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

        private void CheckDocument(SqlTransaction tran, CatalogPackage package, int itemId)
        {
            DocumentDataObject documentInfo;

            PageDataObject pageInfo = Page.GetInstance().FindByModelId(tran, itemId);

            if (null == pageInfo)
            {
                return;
            }

            if (0 == package.Documents.Count)
            {
                documentInfo = Document.GetInstance().FindById(tran, pageInfo.DocumentId);

                package.Documents.Add(documentInfo);

                return;
            }

            int lastDocumentId = package.Documents[package.Documents.Count - 1].DocumentId;
            if (pageInfo.DocumentId == lastDocumentId)
            {
                return;
            }

            List<PageDataObject> pages = Page.GetInstance().FindByDocumentId(tran, lastDocumentId);

            package.Pages.AddRange(pages);

            documentInfo = Document.GetInstance().FindById(tran, pageInfo.DocumentId);

            package.Documents.Add(documentInfo);
        }

        private int GetPageNumber(SqlTransaction tran, CatalogPackage package, int itemId)
        {
            PageDataObject pageInfo = Page.GetInstance().FindByModelId(tran, itemId);
            if (null != pageInfo)
            {
                return pageInfo.PageNumber + package.Pages.Count - 1;
            }
            return 0;
        }

        private string GetPageCode(CatalogPackage package)
        {
            if (0 == package.Documents.Count)
            {
                return DEFAULT_PAGE_CODE;
            }

            return package.Documents[package.Documents.Count - 1].Code;
        }

    }

}
