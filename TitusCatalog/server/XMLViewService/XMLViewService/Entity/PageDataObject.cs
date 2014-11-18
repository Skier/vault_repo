using System;
using System.Collections.Generic;
using System.Text;

namespace XMLViewService.Entity
{

    public class PageDataObject
    {

        public int PageNumber;

        public decimal Width;

        public decimal Height;

        public string ID;

        public List<TextDataObject> Texts;

        public PageDataObject()
        {
            Texts = new List<TextDataObject>();
        }

    }

}
