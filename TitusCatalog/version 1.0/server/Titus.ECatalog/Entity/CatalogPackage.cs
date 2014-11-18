using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class CatalogPackage
    {

        public List<PageDataObject> Pages;

        public List<CatalogItem> Categories;

        public List<DocumentDataObject> Documents;

        public List<SectionInfo> Sections;

        public string StartPageUrl;

        public bool IsProcessingDocument = false;

        public CatalogPackage()
        {
            Pages = new List<PageDataObject>();
            Categories = new List<CatalogItem>();
            Documents = new List<DocumentDataObject>();
            Sections = new List<SectionInfo>();
        }

    }

}
