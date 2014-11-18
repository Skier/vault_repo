using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class SubCategoryDataObject
    {

        public long SubCategoryId;

        public long CategoryId;

        public int Sort;

        public string Name;

        public string Description;

        public List<ProductDataObject> Products;

        public string Color1;

        public string Color2;

        public int PageNumber;

        public SubCategoryDataObject()
        {
            Products = new List<ProductDataObject>();
        }

    }

}
