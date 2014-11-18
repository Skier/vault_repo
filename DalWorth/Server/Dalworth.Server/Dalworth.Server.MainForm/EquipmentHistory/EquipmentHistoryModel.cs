using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.EquipmentHistory
{
    public class EquipmentHistoryModel : IModel
    {
        #region SelectedEquipments

        private IList<EquipmentWrapper> m_selectedEquipments;
        public IList<EquipmentWrapper> SelectedEquipments
        {
            get { return m_selectedEquipments; }
            set { m_selectedEquipments = value; }
        }

        #endregion

        #region EquipmentHistory

        private BindingList<EquipmentHistoryWrapper> m_equipmentHistory;
        public BindingList<EquipmentHistoryWrapper> EquipmentHistory
        {
            get { return m_equipmentHistory; }
        }

        #endregion

        #region Areas

        private List<Area> m_areas;
        public List<Area> Areas
        {
            get { return m_areas; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_areas = Area.Find();
            InitHistory(DateTime.Now.AddMonths(-1));
        }

        #endregion

        #region InitHistory

        public void InitHistory(DateTime? date)
        {
            m_equipmentHistory = new BindingList<EquipmentHistoryWrapper>(
                Domain.Equipment.FindEquipmentHistoryWrappers(date, m_selectedEquipments));
        }

        #endregion
    }
}
