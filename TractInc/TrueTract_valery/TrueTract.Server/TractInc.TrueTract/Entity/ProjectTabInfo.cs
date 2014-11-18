using System;
using System.Collections.Generic;

namespace TractInc.TrueTract.Entity
{
public class ProjectTabInfo
{
    private const string XML_TEMPLATE = @"<projectTab id=""{0}"" name=""{1}"" label=""{2}""><contacts>{3}</contacts><documents>{4}</documents></projectTab>";

    public int ProjectTabId;
    public int ProjectId;
    public string Name;
    public string Label;
    public int TabOrder;

    public ProjectInfo ProjectRef;
    public List<ProjectTabDocumentInfo> Documents;
    public List<ProjectTabContactInfo> Contacts;

    public ProjectTabDocumentInfo getActiveTabDocument()
    {
        foreach (ProjectTabDocumentInfo entry in Documents)
        {
            if (entry.IsActive)
            {
                return entry;
            }
        }

        return null;
    }

    public string toXml()
    {
        string contacts = String.Empty;
        foreach (ProjectTabContactInfo contact in Contacts)
        {
            contacts += contact.toXml();
        }

        string documents = String.Empty;
        foreach (ProjectTabDocumentInfo document in Documents)
        {
            documents += document.toXml();
        }
        
        return String.Format(XML_TEMPLATE,
                             ProjectTabId,
                             XmlString.validate(Name),
                             XmlString.validate(Label),
                             contacts,
                             documents);
    }

    public string toSearchString()
    {
        string contacts = String.Empty;
        foreach (ProjectTabContactInfo contact in Contacts)
        {
            contacts += contact.toSearchString();
        }

        string documents = String.Empty;
        foreach (ProjectTabDocumentInfo document in Documents)
        {
            documents += document.toSearchString();
        }

        return Name + " " 
               + Label + " "
               + contacts + " " 
               + documents;
    }
}
}
