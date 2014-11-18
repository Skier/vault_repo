using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class DocumentDataObject
    {

        public int DocumentId;

        public string Name;

        public string Path;

        public string Code;

        public int PageNumber;

        public List<PageDataObject> Pages;

        public DocumentDataObject()
        {
            Pages = new List<PageDataObject>();
        }
    
    }

}
