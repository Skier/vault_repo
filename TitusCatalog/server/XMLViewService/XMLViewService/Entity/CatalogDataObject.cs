using System;
using System.Collections.Generic;
using System.Text;

namespace XMLViewService.Entity
{

    public class CatalogDataObject
    {

        public string CatalogName;

        public List<PageDataObject> Pages;

        public CatalogDataObject()
        {
            Pages = new List<PageDataObject>();
        }
    
    }

}
