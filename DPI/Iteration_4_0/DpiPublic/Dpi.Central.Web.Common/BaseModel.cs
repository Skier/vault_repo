using System;
using DPI.Interfaces;

namespace Dpi.Central.Web
{
    public abstract class BaseModel
    {
        private IMap _imap;

        protected BaseModel(IMap imap)
        {
            if (imap == null) {
                throw new ArgumentNullException("imap");
            }

            _imap = imap;
        }

        protected IMap Map
        {
            get { return _imap; }
        }
    }
}