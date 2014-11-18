using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain.package
{
    public class LeadPrint
    {
        private readonly Lead m_lead;
        private ProjectType  m_projectType;

        #region Constructor

        public LeadPrint(Lead lead)
        {
            m_lead = lead;

            if (lead.ProjectTypeId.HasValue)
                m_projectType = ProjectType.FindByPrimaryKey(lead.ProjectTypeId.Value);
        }

        #endregion

        #region LeadName

        public string LeadName
        {
            get
            {
                if (m_projectType != null)
                    return m_projectType.Type.ToUpper() + " LEAD";
                
                return "MARKETING LEAD";
            }
        }

        #endregion

        #region

        public string LeadId
        {
            get { return m_lead.ID.ToString(); }
        }

        #endregion

        #region

        public string CustomerName
        {
            get { return Utils.JoinStrings(", ",m_lead.LastName , m_lead.FirstName).ToUpper(); }
        }

        #endregion

        #region AddressFirstLine

        public string AddressFirstLine
        {
            get
            {
                if (m_lead.ContainsAddress)
                    return m_lead.Address1.ToUpper() + " " + m_lead.Address2.ToUpper();
                else
                    return string.Empty;
            }
        }

        #endregion

        #region AddressSecondLine

        public string AddressSecondLine
        {
            get 
            {
                if (m_lead.ContainsAddress)
                    return Utils.JoinStrings(", ", m_lead.City, m_lead.State, m_lead.Zip.ToString());
                else
                    return string.Empty;
            }
        }

        #endregion

        #region Phones

        public string Phones
        {
            get
            {
                string result = string.Empty;

                if (m_lead.Phone1 != string.Empty)
                    result += "HM " + Utils.FormatPhone(m_lead.Phone1) + "   ";

                if (m_lead.Phone2 != string.Empty)
                    result += "BS " + Utils.FormatPhone(m_lead.Phone2);

                return result;
            }
        }

        #endregion

        #region CustomerNotes

        public string CustomerNotes
        {
            get 
            {
                if (m_lead.CustomerNotes != null)
                    return m_lead.CustomerNotes;
                else
                    return string.Empty;
            }
        }

        #endregion

        #region DesiredDateTime

        public string PreferredDateTime
        {
            get
            {
                if (m_lead.PreferredServiceDate.HasValue)
                    return m_lead.PreferredServiceDate.Value.ToString("d") + " " + m_lead.PreferredTime;
                else
                    return string.Empty;
            }
        }

        #endregion

        #region Print

        public void Print()
        {
            using (Printer printer = new Printer(Configuration.VisitPrinter))
            {
                printer.Open("Lead " + m_lead.ID);

                printer.Initialize();

                printer.StartBoldDoubleWidth();
                printer.Write(LeadName);
                printer.MovePrintHeadTo(LeadId.Length * 2);
                printer.WriteLine(LeadId);
                printer.EndBoldDoubleWidth();
                printer.WriteLine();

                printer.StartBoldDoubleWidth();
                printer.WriteLine(CustomerName);
                printer.WriteLine(AddressFirstLine);
                printer.WriteLine(AddressSecondLine);
                printer.EndBoldDoubleWidth();

                printer.WriteLine(Phones);
                printer.WriteLine(PreferredDateTime);
                printer.WriteLine();

                if (CustomerNotes != null && CustomerNotes.Length > 0)
                {
                    List<string> formattedDescriptionLines = Utils.DivideText(
                           CustomerNotes, Printer.PAGE_COLUMNS_COUNT);

                    for (int i = 0; i < formattedDescriptionLines.Count; i++)
                    {
                        string descriptionLine = formattedDescriptionLines[i];
                        if (i > 0)
                            descriptionLine = "   " + descriptionLine;
                        printer.WriteLine(descriptionLine);
                    }
                }

                printer.WriteLine();
                printer.WriteLine("Printed: " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());
                printer.GoToNextPage();
            }
        }

        #endregion
    }
}
