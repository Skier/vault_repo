using System;
using System.Collections.Generic;
using System.Text;

namespace XMLViewService.Entity
{

    public class TextDataObject
    {

        public int Top;

        public int Left;

        public int Width;

        public int Height;

        public string ID;

        public List<TokenDataObject> Tokens;

        public TextDataObject()
        {
            Tokens = new List<TokenDataObject>();
        }
        
    }

}
