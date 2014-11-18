namespace TractInc.TrueTract.Entity
{
    public class ModuleInfo
    {
        public int ModuleId;
        public string Description;

        public ModuleInfo()
        {
        }

        public ModuleInfo(string description)
        {
            Description = description;
        }

        public ModuleInfo(int moduleId, string description)
            : this(description)
        {

            ModuleId = moduleId;

        }
    }
}