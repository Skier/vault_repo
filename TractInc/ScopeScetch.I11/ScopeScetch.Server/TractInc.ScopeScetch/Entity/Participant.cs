using System;
using System.Text;

namespace TractInc.ScopeScetch.Entity
{
    public class Participant
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

        public Participant() {
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
    }
}
