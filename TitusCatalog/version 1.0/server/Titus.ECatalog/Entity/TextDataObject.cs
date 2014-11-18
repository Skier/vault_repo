using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class TextDataObject
    {

        public decimal Top;

        public decimal Left;

        public decimal Width;

        public decimal Height;

        public string ID;

        public List<TokenDataObject> Tokens;

        public TextDataObject()
        {
            Tokens = new List<TokenDataObject>();
        }
        
    }

}
