using System;

namespace TractInc.TrueTract.Entity
{
public class FileInfo
{
    private const string XML_TEMPLATE = @"<file id=""{0}"" name=""{1}"" url=""{2}"" path=""{3}"" description=""{4}"" created=""{5}"" createdBy=""{6}""/>";
    
    public int FileId;
    public string FileName;
    public string FileUrl;
    public string FilePath;
    public string Description;
    public DateTime Created;
    public int CreatedBy;
    public string CreatedByName;
    
    public string FileFullName
    {
        get
        {
            return (FilePath.EndsWith("\\") ? FilePath : (FilePath + "\\")) + FileName;
        }
    }
    
    public string toXml()
    {
        return String.Format(XML_TEMPLATE, 
                             FileId, 
                             XmlString.validate(FileName), 
                             XmlString.validate(FileUrl), 
                             XmlString.validate(FilePath), 
                             XmlString.validate(Description),
                             XmlString.validate(Created.ToString()), 
                             XmlString.validate(CreatedByName));
    }

    public string toSearchString()
    {
        return FileName + " " 
               + Description;
    }
}
}
