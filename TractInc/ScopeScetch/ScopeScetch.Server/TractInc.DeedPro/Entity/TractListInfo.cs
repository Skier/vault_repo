namespace TractInc.DeedPro.Entity
{
    public class TractListInfo
    {
        public int tractId;
        public string referenceName;
        public int docId;

        public TractListInfo() {
        }

        public TractListInfo(int tractId, string referenceName, int docId) {
            this.tractId = tractId;
            this.referenceName = referenceName;
            this.docId = docId;
        }
    }
}
