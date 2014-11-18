using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.RugCleaning
{
    public class RugCleaningPage : DalworthPage
    {
        protected override WebSiteEnum WebSite
        {
            get { return WebSiteEnum.RugCleaning;}
        }

        protected override Domain.ProjectTypeEnum ProjectType
        {
            get { return ProjectTypeEnum.RugCleaning; }
        }
    }
}
