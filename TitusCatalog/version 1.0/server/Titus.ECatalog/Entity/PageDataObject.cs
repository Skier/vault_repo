using System;
using System.Collections.Generic;
using System.Text;

namespace Titus.ECatalog.Entity
{

    public class PageDataObject
    {

        public int DocumentPageId;

        public int DocumentId;

        public int PageNumber;

        public string ScreenshotURL;

        public int Width;

        public int Height;

        public List<TokenDataObject> Tokens;

        public List<NoteDataObject> Notes;

        public PageDataObject()
        {
            Tokens = new List<TokenDataObject>();

            Notes = new List<NoteDataObject>();
        }

    }

}
