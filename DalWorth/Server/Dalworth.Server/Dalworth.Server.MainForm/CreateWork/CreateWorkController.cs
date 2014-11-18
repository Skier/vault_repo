using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Controls;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.CreateWork
{
    public class CreateWorkController : Controller<CreateWorkModel, CreateWorkView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.CurrentDispatch = (Employee) data[0];
            Model.Work = (Work) data[1];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_cmbVans.Properties.DataSource = Model.AvailableVans;

            View.m_lblWorkDate.Text = Model.Work.StartDate.Value.ToShortDateString();
            View.m_lblTechnician.Text = Model.Technician.DisplayName;
            View.m_gridEquipment.DataSource = Model.WorkEquipments;

            if (Model.AvailableVans.Count > 0)
            {
                if (Model.Work.VanId.HasValue)
                    View.m_cmbVans.EditValue = Model.AvailableVans[0];
                else
                {
                    Van defaultVan = null;

                    if (Model.Technician.DefaultVanId.HasValue)
                    {
                        defaultVan = Model.AvailableVans.Find(
                            delegate(Van obj) { return obj.ID == Model.Technician.DefaultVanId.Value; });
                    }

                    if (defaultVan != null)
                        View.m_cmbVans.EditValue = defaultVan;
                    else
                        View.m_cmbVans.EditValue = Model.AvailableVans[0];                    
                }
            }

        }

        #endregion

        #region OnViewShow

        protected override void OnViewShow(object sender, EventArgs e)
        {
            View.m_cmbVans.Focus();
        }

        #endregion        

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (View.m_cmbVans.EditValue == null)
            {
                XtraMessageBox.Show(
                    "Cannot create work. All vans are assigned for this date. Please select another date",
                    "Cannot create work", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                View.m_cmbVans.Focus();
                return;
            }

            if (Model.Work.VanId != null 
                && Model.Work.VanId.Value != ((Van)View.m_cmbVans.EditValue).ID)
            {
                if (Work.FindWorkByVanAndDate(
                    ((Van)View.m_cmbVans.EditValue).ID, Model.Work.StartDate.Value) != null)
                {
                    XtraMessageBox.Show(
                        "Cannot create work. Selected van is already used by another technician",
                        "Cannot create work", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    View.m_cmbVans.Focus();
                    return;
                }
            }

            WorkPackage package = new WorkPackage();
            package.Dispatch = Model.CurrentDispatch;
            package.Technician = Model.Technician;
            package.Van = (Van) View.m_cmbVans.EditValue;
            package.Work = Model.Work;

            package.WorkEquipments = new List<WorkEquipment>();
            foreach (WorkEquipment equipment in Model.WorkEquipments)
            {
                package.WorkEquipments.Add(equipment);
            }

            try
            {
                Database.Begin(IsolationLevel.RepeatableRead);
                Model.CreateWork(package);
                Database.Commit();
                Host.TraceUserAction("Create Work " + package.Technician.DisplayName);

            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
            View.Destroy();
        }

        #endregion        
    }
}
