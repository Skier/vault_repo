using System;

namespace TractInc.TrueTract.Entity
{
    public class ProjectInfo
    {
        public int ProjectId;
        public int ContractId;
        public int AccountId;
        public int ProjectStatusId;
        public string Name;
        public string ProjectName;
        public string ShortName;
        public string Description;
        public string CreatedBy;
        
        public ClientInfo Client;
        public ProjectTabInfo[] Tabs;

        public ProjectAttachmentInfo[] Attachments;

        public DateTime Changed;

        private const string XML_TEMPLATE = @"<project itemtype=""project"" id=""{0}"" name=""{1}"" changed=""{2}""><attachments>{3}</attachments><tabs>{4}</tabs></project>";
        
        public string toXml()
        {
            string attachments = String.Empty;
            foreach (ProjectAttachmentInfo attachment in Attachments)
            {
                attachments += attachment.toXml();
            }

            string tabs = String.Empty;
            foreach (ProjectTabInfo tab in Tabs)
            {
                tabs += tab.toXml();
            }

            return String.Format(XML_TEMPLATE,
                                 ProjectId,
                                 XmlString.validate(Name),
                                 XmlString.validate(Changed.ToString()),
                                 attachments,
                                 tabs);
        }

        public string toSearchString()
        {
            string attachments = String.Empty;
            foreach (ProjectAttachmentInfo attachment in Attachments)
            {
                attachments += attachment.toSearchString();
            }

            string tabs = String.Empty;
            foreach (ProjectTabInfo tab in Tabs)
            {
                tabs += tab.toSearchString();
            }

            return Name + " "
                    + attachments + " "
                    + tabs;
        }
    }
}
