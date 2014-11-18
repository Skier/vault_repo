using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
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
