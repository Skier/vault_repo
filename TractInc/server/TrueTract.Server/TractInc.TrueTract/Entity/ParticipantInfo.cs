using System;
using System.Text;

namespace TractInc.TrueTract.Entity
{
    public class ParticipantInfo
    {
    
        #region Fields

        public int ParticipantID;
        public int DocID;
        public string AsNamed;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public bool IsSeler;
        
        #endregion

        #region Constructors

        public ParticipantInfo() {
        }

        #endregion
        
        public string FullName {
            get
            {
                StringBuilder result = new StringBuilder(FirstName);

                if (MiddleName.Length > 0)
                    result.AppendFormat(" {0}", MiddleName);

                if (LastName.Length > 0)
                    result.AppendFormat(" {0}", LastName);

                return result.ToString();
            }
            
        }

        private const string XML_TEMPLATE = @"<participant id=""{0}"" asNamed=""{1}"" firstName=""{2}"" middleName=""{3}"" lastName=""{4}""/>";
        public string toXml()
        {
            return String.Format(XML_TEMPLATE,
                                 ParticipantID,
                                 XmlString.validate(AsNamed),
                                 XmlString.validate(FirstName),
                                 XmlString.validate(MiddleName),
                                 XmlString.validate(LastName));
        }

        public string toSearchString()
        {
            return AsNamed + " " 
                   + FirstName + " " 
                   + MiddleName + " " 
                   + LastName;
        }
    }
}
