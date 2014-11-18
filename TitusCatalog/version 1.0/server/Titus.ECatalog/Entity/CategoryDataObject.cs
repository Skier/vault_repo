using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class CategoryDataObject
    {

        public long CategoryId;

        public int Sort;

        public string Name;

        public string Description;

        public string Color1;

        public string Color2;

        public string Color3;

        public string Color4;

        public int PageNumber;

        public int DocumentId;

        public List<SubCategoryDataObject> SubCategories;

        public CategoryDataObject()
        {
            SubCategories = new List<SubCategoryDataObject>();
        }

    }

}
