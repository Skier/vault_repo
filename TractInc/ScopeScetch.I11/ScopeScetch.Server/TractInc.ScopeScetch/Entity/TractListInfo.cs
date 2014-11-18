using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.ScopeScetch.Entity
{
    public class TractListInfo
    {
        public string uid;
        public string referenceName;

        public TractListInfo() {
        }

        public TractListInfo(string uid, string referenceName) {
            this.uid = uid;
            this.referenceName = referenceName;
        }
    }
}
