using System.Collections.Generic;

namespace TractInc.TrueTract.Entity
{
public class ProjectTabDocumentInfo
{
    public int ProjectTabDocumentId;
    public int ProjectTabId;
    public int DocumentId;
    public string Description;
    public string Remarks;
    public bool IsActive;

    public DocumentInfo DocumentRef;

    public ProjectTabDocumentTractInfo[] Tracts;
    
    public void updateTracts()
    {
        if (DocumentRef != null && DocumentRef.Tracts != null && Tracts != null)
        {
            List<ProjectTabDocumentTractInfo> newTracts = new List<ProjectTabDocumentTractInfo>();
            
            foreach (TractInfo tract in DocumentRef.Tracts)
            {
                ProjectTabDocumentTractInfo localTract = getTractByUniqueId(tract.UniqueId);
                
                if (localTract != null)
                {
                    localTract.TractId = tract.TractId;
                    newTracts.Add(localTract);
                }
            }

            Tracts = newTracts.ToArray();
        }
    }
    
    private ProjectTabDocumentTractInfo getTractByUniqueId(string id)
    {
        foreach (ProjectTabDocumentTractInfo tabTract in Tracts)
        {
            if (tabTract.TractRef != null && tabTract.TractRef.UniqueId == id)
                return tabTract;
        }

        return null;
    }
    
    private const string XML_TEMPLATE = @"<tabDocument id=""{0}"" documentId=""{1}"" description=""{2}"" remarks=""{3}""/>";

    public string toXml()
    {
        return string.Format(XML_TEMPLATE,
                             ProjectTabDocumentId,
                             DocumentId,
                             XmlString.validate(Description),
                             XmlString.validate(Remarks));
    }

    public string toSearchString()
    {
        return Description + " " 
               + Remarks;
    }
}
}
