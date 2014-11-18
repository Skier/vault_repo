using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class CatalogItem
    {

        public int Id;

        public int Sort;

        public string Name;

        public string Color1;

        public string Color2;

        public string Color3;

        public string Color4;

        public int PageNumber;

        public string PageCode;

        public string Description;

        public string Information;

        public bool IsBranch = true;

        public bool IsModelItem = false;

        public List<CatalogItem> SubItems;

        public CatalogItem()
        {
            SubItems = new List<CatalogItem>();
        }

    }

}
