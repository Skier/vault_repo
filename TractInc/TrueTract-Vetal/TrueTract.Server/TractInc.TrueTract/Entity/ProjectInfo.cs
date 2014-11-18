namespace TractInc.TrueTract.Entity
{
    public class ProjectInfo
    {
        public int ProjectId;
        public string Name;
        public string ShortName;
        public int ClientId;
        public int ClientAccountId;
        public int StatusId;
        public string Description;
        
        public ClientInfo Client;
    }
}
