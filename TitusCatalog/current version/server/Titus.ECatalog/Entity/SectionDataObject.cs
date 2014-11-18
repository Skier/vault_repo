using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class SectionDataObject
    {

        public int SectionId;

        public int CatalogId;

        public int StartPageNumber;

        public int PagesTotal;

        public string SectionPrefix;

        public string PdfPath;

        public int Sort;

        public List<SectionPageDataObject> Pages;

        public SectionDataObject()
        {
            Pages = new List<SectionPageDataObject>();
        }
    
    }

}
