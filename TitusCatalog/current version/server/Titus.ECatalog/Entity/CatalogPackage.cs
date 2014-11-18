using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class CatalogPackage
    {

        public List<SectionPageDataObject> Pages;

        public List<CatalogItemDataObject> Categories;

        public List<SectionDataObject> Sections;

        public string StartPageUrl;

        public bool IsProcessingDocument = false;

        public CatalogPackage()
        {
            Pages = new List<SectionPageDataObject>();
            Categories = new List<CatalogItemDataObject>();
            Sections = new List<SectionDataObject>();
        }

    }

}
