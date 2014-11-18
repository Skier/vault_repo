using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPivotGrid;
using SmartSchedule.Domain;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.VisitAdd
{
    public class VisitAddController : Controller<VisitAddModel, VisitAddView>
    {
        private bool m_isInRerankMode;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion        

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.BookingEngine = (BookingEngine) data[0];
            base.OnModelInitialize(data);
        }

        #endregion


        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_txtDuration.LostFocus += OnDurationLostFocus;
            View.m_txtLocation.LostFocus += OnLocationLostFocus;

            View.m_gridTimeFrames.CustomFieldSort += OnTimeFramesFieldSort;                        
            View.m_gridTimeFrames.CustomDrawCell += OnTimeFramesCustomDrawCell;
            View.m_gridTimeFrames.CellDoubleClick += OnTimeFramesCellDoubleClick;
            View.m_gridSortedFramesView.DoubleClick += OnSortedFramesDoubleClick;

            View.m_gridTimeFrames.CellSelectionChanged += OnTimeFramesCellSelectionChanged;

            View.m_gridTimeFrames.FocusedCellChanged += OnGridTimeFramesFocusedCellChanged;
            View.m_gridSortedFramesView.FocusedRowChanged += OnGridSortedFramesFocusedRowChanged;            

            View.m_btnRerankSelection.Click += OnRerankSelectionClick;
        }


        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtDuration.Text = "60";
            View.m_txtLocation.Text = "0/0";

            View.m_gridTimeFrames.DataSource = Model.BookingEngine.TimeFrames;            
            RefreshTimeFrameRanks();            
        }

        #endregion

        #region Focus tracking

        private bool m_isColoredGridFocusChanging = false;
        private void OnGridTimeFramesFocusedCellChanged(object sender, EventArgs e)
        {
            TimeFrame focusedFrame = Model.TimeFrames[View.m_gridTimeFrames.Cells.GetCellInfo(
                    View.m_gridTimeFrames.Cells.FocusedCell.X,
                    View.m_gridTimeFrames.Cells.FocusedCell.Y
                    ).CreateDrillDownDataSource()[0].ListSourceRowIndex];

            List<TimeFrame> datasourceList = (List<TimeFrame>) View.m_gridSortedFrames.DataSource;
            if (datasourceList.IndexOf(focusedFrame) >= 0)
            {
                m_isColoredGridFocusChanging = true;
                View.m_gridSortedFramesView.FocusedRowHandle =
                    View.m_gridSortedFramesView.GetRowHandle(datasourceList.IndexOf(focusedFrame));
                m_isColoredGridFocusChanging = false;
            }
        }

        

        private void OnGridSortedFramesFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (m_isColoredGridFocusChanging)
                return;

            TimeFrame focusedFrame = (TimeFrame) View.m_gridSortedFramesView.GetRow(e.FocusedRowHandle);
            if (focusedFrame != null)
            {
                //TODO: rework this
                for (int x = 0; x < View.m_gridTimeFrames.Cells.ColumnCount; x++)
                    for (int y = 0; y < View.m_gridTimeFrames.Cells.RowCount; y++)
                    {
                        TimeFrame frame = Model.TimeFrames[View.m_gridTimeFrames.Cells.GetCellInfo(
                            x, y).CreateDrillDownDataSource()[0].ListSourceRowIndex];
                        if (frame == focusedFrame)
                        {
                            View.m_gridTimeFrames.Cells.MultiSelection.SetSelection(new System.Drawing.Point[0]);
                            View.m_gridTimeFrames.Cells.FocusedCell = new System.Drawing.Point(x, y);
                            return;
                        }
                    }                
            }
        }

        #endregion


        #region OnTimeFramesCellSelectionChanged

        private void OnTimeFramesCellSelectionChanged(object sender, EventArgs e)
        {
            if (m_isInRerankMode)
                View.m_btnRerankSelection.Enabled = true;
            else
                View.m_btnRerankSelection.Enabled =
                    View.m_gridTimeFrames.Cells.MultiSelection.SelectedCells.Count > 0;
        }

        #endregion

        #region OnRerankSelectionClick

        private void OnRerankSelectionClick(object sender, EventArgs e)
        {
            if (m_isInRerankMode)
            {
                Model.BookingEngine.SetPrimarySecondaryRanksBasedOnRank();
                View.m_btnRerankSelection.Text = "Rerank Selection";
                m_isInRerankMode = false;
                OnTimeFramesCellSelectionChanged(null, null);
                View.m_gridSortedFrames.DataSource = Model.BookingEngine.GetAllowedFramesSortedByRank();
                OnGridTimeFramesFocusedCellChanged(null, null);
                return;
            }

            if (View.m_gridTimeFrames.Cells.MultiSelection.SelectedCells.Count <= 0)
                return;

            List<TimeFrame> selectedFrames = new List<TimeFrame>();

            foreach (System.Drawing.Point cell in View.m_gridTimeFrames.Cells.MultiSelection.SelectedCells)
            {
                selectedFrames.Add(Model.TimeFrames[View.m_gridTimeFrames.Cells.GetCellInfo(
                    cell.X, cell.Y).CreateDrillDownDataSource()[0].ListSourceRowIndex]);
            }
            
            Model.BookingEngine.SetPrimarySecondaryRanksBasedOnRank(selectedFrames);            
            View.m_btnRerankSelection.Text = "Undo Rerank";
            m_isInRerankMode = true;
            View.m_gridTimeFrames.Cells.MultiSelection.SetSelection(new System.Drawing.Point[0]);
            View.m_gridSortedFrames.DataSource
                = Model.BookingEngine.GetAllowedFramesSortedByRank(selectedFrames);
            OnGridTimeFramesFocusedCellChanged(null, null);
        }

        #endregion


        #region OnTimeFramesCellDoubleClick

        private void OnTimeFramesCellDoubleClick(object sender, PivotCellEventArgs e)
        {
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            TimeFrame timeFrame = Model.TimeFrames[ds[0].ListSourceRowIndex];
            ProcessTimeFrameSelection(timeFrame);
        }

        private void OnSortedFramesDoubleClick(object sender, EventArgs e)
        {
            GridHitInfo hitInfo = View.m_gridSortedFramesView.CalcHitInfo(
                View.m_gridSortedFrames.PointToClient(Cursor.Position));

            if (hitInfo.InRow)
            {
                TimeFrame focusedFrame = (TimeFrame)View.m_gridSortedFramesView.GetRow(
                    View.m_gridSortedFramesView.FocusedRowHandle);
                if (focusedFrame != null)
                    ProcessTimeFrameSelection(focusedFrame);
            }                            
        }

        private void ProcessTimeFrameSelection(TimeFrame timeFrame)
        {
            if (GetDuration() == null)
                return;

            if (GetLocation() == null)
                return;

            Visit newVisit = Model.BookingEngine.GetNewVisit(
                timeFrame, GetDuration().Value, GetLocation());

            if (!Model.BookingEngine.InsertVisit(newVisit))
            {
                XtraMessageBox.Show("Selected time frame is not allowed");
                return;                
            }

            View.Destroy();            
        }

        #endregion

        #region OnTimeFramesCustomDrawCell

        private void OnTimeFramesCustomDrawCell(object sender, PivotCustomDrawCellEventArgs e)
        {
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            TimeFrame frame = Model.TimeFrames[ds[0].ListSourceRowIndex];

            if (!frame.IsAllowed)
                e.Appearance.BackColor = e.Selected || e.Focused ? Color.Black : Color.Gray;
            else if (frame.PrimaryRank < 0)
                e.Appearance.BackColor = e.Selected || e.Focused ? Color.Blue : Color.White;
            else if (frame.PrimaryRank == 0)
                e.Appearance.BackColor = e.Selected || e.Focused ? Color.Green : Color.LightGreen;
            else if (frame.PrimaryRank == 1)
                e.Appearance.BackColor = e.Selected || e.Focused ? Color.Goldenrod : Color.Yellow;
            else if (frame.PrimaryRank == 2)
                e.Appearance.BackColor = e.Selected || e.Focused ? Color.DarkRed : Color.Red;                            

            if (e.Focused)
                e.Appearance.ForeColor = Color.White;
        }

        #endregion


        #region OnTimeFramesFieldSort

        private void OnTimeFramesFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            e.Result = Model.TimeFrames[e.ListSourceRowIndex1].TimeStart.TimeOfDay.CompareTo(
                Model.TimeFrames[e.ListSourceRowIndex2].TimeStart.TimeOfDay);
            e.Handled = true;
        }

        #endregion        


        #region RefreshTimeFrameRanks

        private void RefreshTimeFrameRanks()
        {
            if (GetDuration() == null)
                return;

            if (GetLocation() == null)
                return;

            using (new WaitCursor())
            {
                Model.BookingEngine.RecalculateTimeFramesRanks(
                    GetDuration().Value,
                    GetLocation());
                View.m_gridSortedFrames.DataSource = Model.BookingEngine.GetAllowedFramesSortedByRank();
            }
        }

        #endregion

        #region OnDurationLostFocus

        private void OnDurationLostFocus(object sender, EventArgs e)
        {
            RefreshTimeFrameRanks();
        }

        #endregion

        #region OnLocationLostFocus

        private void OnLocationLostFocus(object sender, EventArgs e)
        {
            RefreshTimeFrameRanks();
        }

        #endregion


        #region GetDuration & Location

        private int? GetDuration()
        {
            int duration;

            try
            {
                duration = int.Parse(View.m_txtDuration.Text);
            }
            catch (Exception)
            {
                View.m_errorProvider.SetError(View.m_txtDuration, "Please enter valid duration");
                return null;
            }

            if (duration == 0)
            {
                View.m_errorProvider.SetError(View.m_txtDuration, "Duration should greater than 0");
                return null;
            }

            if (duration % 30 != 0)
            {
                View.m_errorProvider.SetError(View.m_txtDuration, "Duration should be 30 X");
                return null;
            }

            View.m_errorProvider.SetError(View.m_txtDuration, string.Empty);
            return duration;
        }

        private Point GetLocation()
        {
            if (View.m_txtLocation.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtLocation, "Please enter location");
            else
                View.m_errorProvider.SetError(View.m_txtLocation, string.Empty);

            string[] splittedValues = View.m_txtLocation.Text.Split(new char[1] {'/'});

            int x, y;
            if (int.TryParse(splittedValues[0], out x) && int.TryParse(splittedValues[1], out y))
                return new Point(x, y);
            return null;
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion
   
    }
}
