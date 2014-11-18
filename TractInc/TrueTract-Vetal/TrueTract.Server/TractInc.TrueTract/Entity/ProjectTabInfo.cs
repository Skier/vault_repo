using System.Collections.Generic;

namespace TractInc.TrueTract.Entity
{
public class ProjectTabInfo
{
    public int ProjectTabId;
    public int ProjectId;
    public string Name;
    public string Description;

    public List<ProjectTabDocumentInfo> DocumentList;
}
}
