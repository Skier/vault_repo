using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.Banking.WriteCheck
{
    public class ExpenceLineModel : IModel
    {
        #region Fields

        #region WriteCheckModel

        private WriteCheckModel m_writeCheckModel;
        public WriteCheckModel WriteCheckModel
        {
            get { return m_writeCheckModel; }
            set { m_writeCheckModel = value; }
        }

        #endregion

        #region EditedExpenceLine

        CheckExpenceLine m_editedCheckExpenceLine;
        public CheckExpenceLine EditedCheckExpenceLine
        {
            get { return m_editedCheckExpenceLine; }
            set { m_editedCheckExpenceLine = value; }
        }

        #endregion

        #endregion
        
        #region Init

        public void Init()
        {
            
        }

        #endregion
    }
}
