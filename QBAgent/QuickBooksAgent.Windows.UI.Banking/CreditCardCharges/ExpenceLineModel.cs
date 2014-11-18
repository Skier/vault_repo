using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.Banking.CreditCardCharges
{
    public class ExpenceLineModel : IModel
    {
        #region Fields

        #region CreditCardModel

        private CreditCardModel m_creditCardModel;
        public CreditCardModel CreditCardModel
        {
            get { return m_creditCardModel; }
            set { m_creditCardModel = value; }
        }

        #endregion

        #region EditedExpenceLine

        CreditCardExpenceLine m_editedExpenceLine;
        public CreditCardExpenceLine EditedExpenceLine
        {
            get { return m_editedExpenceLine; }
            set { m_editedExpenceLine = value; }
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
