using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class CatalogItemDataObject
    {

        public int ModelId;

        public int CatalogLevel;

        public int ParentId;

        public string Name;

        public string Description;

        public int PageNumber;

        public string PageCode;

        public int Sort;

        public List<CatalogItemDataObject> SubItems;

        public bool IsBranch = true;

        public CatalogItemDataObject()
        {
            SubItems = new List<CatalogItemDataObject>();
        }

    }

}
