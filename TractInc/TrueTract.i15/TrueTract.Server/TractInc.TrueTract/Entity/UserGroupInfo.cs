using System.Collections.Generic;

namespace TractInc.TrueTract.Entity
{
    public class UserGroupInfo
    {
        
        public int groupId;

        public string groupName;

        public bool systemGroup;

        public List<DocumentInfo> groupDocuments;
        
        public List<TractInfo> groupDrawings;

    }
}
