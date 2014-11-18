using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Win32.Authenticate;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.TechnicianEdit;
using SmartSchedule.Windows;
using Point=System.Drawing.Point;

namespace SmartSchedule.Win32.TechnicianArrangement
{
    public enum ScrollDirection { None = 0, Up = 1, Down = 2 }

    public class TechnicianArrangementController : Controller<TechnicianArrangementModel, TechnicianArrangementView>
    {
        private const int SCROLL_START_DELAY = 200, SCROLL_DELAY = 80;
        private GridHitInfo m_gridDownRowInfo;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region DropTarget

        private class DropTargetInfo
        {
            #region Constructor

            public DropTargetInfo(GridView sourceView, GridView destinationView, int rowHandle)
            {
                m_sourceView = sourceView;
                m_destinationView = destinationView;
                RowHandle = rowHandle;
            }

            #endregion

            #region DestinationView

            private GridView m_destinationView;
            public GridView DestinationView
            {
                get { return m_destinationView; }
                set { m_destinationView = value; }
            }

            #endregion

            #region SourceView

            private GridView m_sourceView;
            public GridView SourceView
            {
                get { return m_sourceView; }
                set { m_sourceView = value; }
            }        

            #endregion

            #region RowHandle

            private int m_rowHandle;
            public int RowHandle
            {
                get { return m_rowHandle; }
                set
                {
                    m_rowHandle = value;
                    m_destinationView.Invalidate();
                }
            }

            #endregion

            #region IsOutOfSelection

            public bool IsOutOfSelection()
            {                    
                if (m_rowHandle < 0)
                    return false;

                if (m_sourceView != m_destinationView)
                    return true;

                if (m_sourceView.GetSelectedRows().Length == 0)
                    return true;

                int[] selectedRowHandles = m_sourceView.GetSelectedRows();

                if (m_rowHandle < selectedRowHandles[0]
                    || m_rowHandle > selectedRowHandles[selectedRowHandles.Length - 1] + 1)
                {
                    return true;
                }

                return false;                
            }

            #endregion
        }

        private DropTargetInfo m_dropTarget;
        private DropTargetInfo DropTarget
        {
            get { return m_dropTarget; }
            set { m_dropTarget = value; }
        }

        #endregion

        #region ScrollDirection

        private ScrollDirection m_scrollDirection;
        private ScrollDirection ScrollDirection
        {
            get { return m_scrollDirection; }
            set
            {
                if (m_scrollDirection == value)
                    return;

                m_scrollDirection = value;
                View.m_scrollTimer.Stop();
                if (m_scrollDirection != ScrollDirection.None)
                {
                    View.m_scrollTimer.Interval = SCROLL_START_DELAY;
                    View.m_scrollTimer.Start();
                }
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.IsDefaultSettingsMode = (bool) data[0];            
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;

            View.m_gridTechnicianUnorderedView.DoubleClick += OnGridTechnicianDoubleClick;
            View.m_gridTechnicianOrderedView.DoubleClick += OnGridTechnicianDoubleClick;

            View.m_gridTechnicianOrderedView.FocusedColumnChanged += OnGridTechnicianVisibleFocusedColumnChanged;
            View.m_gridTechnicianOrderedView.MouseDown += OnGridTechnicianMouseDown;
            View.m_gridTechnicianUnorderedView.MouseDown += OnGridTechnicianMouseDown;
            View.m_gridTechnicianOrderedView.MouseMove += OnGridTechnicianMouseMove;
            View.m_gridTechnicianUnorderedView.MouseMove += OnGridTechnicianMouseMove;
            View.m_gridTechnicianOrdered.Paint += OnGridTechnicianPaint;
            View.m_gridTechnicianUnordered.Paint += OnGridTechnicianPaint;
            View.m_gridTechnicianOrdered.DragOver += OnGridTechnicianDragOver;
            View.m_gridTechnicianUnordered.DragOver += OnGridTechnicianDragOver;
            View.m_gridTechnicianOrdered.DragDrop += OnGridTechnicianDragDrop;
            View.m_gridTechnicianUnordered.DragDrop += OnGridTechnicianDragDrop;
            View.m_gridTechnicianOrdered.DragLeave += OnGridTechnicianDragLeave;
            View.m_gridTechnicianUnordered.DragLeave += OnGridTechnicianDragLeave;
            View.m_scrollTimer.Tick += OnScrollTimerTick;

            View.m_btnMoveToVisible.Click += OnMoveToVisibleClick;
            View.m_btnMoveToInvisible.Click += OnMoveToInvisibleClick;
            View.m_btnMoveToVisibleAll.Click += OnMoveToVisibleAllClick;
            View.m_btnMoveToInvisibleAll.Click += OnMoveToInvisibleAllClick;
            View.m_gridTechnicianOrderedView.RowCountChanged += OnGridTechnicianRowCountChanged;
            View.m_gridTechnicianUnorderedView.RowCountChanged += OnGridTechnicianRowCountChanged;

            View.m_gridTechnicianUnorderedView.KeyPress += OnGridTechnicianKeyPress;
            View.m_gridTechnicianOrderedView.KeyPress += OnGridTechnicianKeyPress;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (Model.IsDefaultSettingsMode)
                View.Text = "Technician Arrangement (Default Settings)";
            else
                View.Text = "Technician Arrangement (Day Settings)";

            View.m_gridTechnicianOrdered.DataSource = Model.OrderedTechnicians;
            View.m_gridTechnicianUnordered.DataSource = Model.UnorderedTechnicians;
            View.m_gridTechnicianOrdered.Select();

            View.AlwaysAllowedControls.Add(View.m_btnCancel);
            View.MinRequiredUserRole = UserRoleEnum.Supervisor;
        }

        #endregion



        #region OnGridTechnicianVisibleDoubleClick

        private void OnGridTechnicianDoubleClick(object sender, EventArgs e)
        {
            GridView gridView = (GridView)sender;

            TechnicianArrange technician = (TechnicianArrange)gridView.GetRow(gridView.FocusedRowHandle);

            if (gridView.SelectedRowsCount != 1 || technician == null 
                || m_gridDownRowInfo == null || !Model.IsDefaultSettingsMode)
            {
                return;
            }

//            if (Control.ModifierKeys == Keys.Alt && gridView == View.m_gridTechnicianOrderedView)
//            {
//                technician.IsOff = !technician.IsOff;
//                Model.OrderedTechnicians.ResetItem(Model.OrderedTechnicians.IndexOf(technician));
//                return;
//            }

            
            using (TechnicianEditController controller = Prepare<TechnicianEditController>(
                technician.Technician, Model.IsDefaultSettingsMode))
            {
                controller.Execute(false);
            }
        }

        #endregion

        #region OnGridTechnicianVisibleFocusedColumnChanged

        private void OnGridTechnicianVisibleFocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn.Name != View.m_colTechnicianName.Name)
                View.m_gridTechnicianOrderedView.FocusedColumn = View.m_colTechnicianName;
        }

        #endregion

        #region OnGridTechnicianMouseDown

        private void OnGridTechnicianMouseDown(object sender, MouseEventArgs e)
        {
            GridView gridView = (GridView)sender;
            m_gridDownRowInfo = null;

            GridHitInfo hitInfo = gridView.CalcHitInfo(e.X, e.Y);
            if (e.Button == MouseButtons.Left && hitInfo.InRow && hitInfo.HitTest != GridHitTest.RowIndicator)
                m_gridDownRowInfo = hitInfo;            
        }

        #endregion

        #region OnGridTechnicianMouseMove

        private void OnGridTechnicianMouseMove(object sender, MouseEventArgs e)
        {
            GridView gridView = (GridView) sender;
            if (e.Button == MouseButtons.Left && m_gridDownRowInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(
                                                       m_gridDownRowInfo.HitPoint.X - dragSize.Width / 2,
                                                       m_gridDownRowInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(e.X, e.Y))
                {
                    gridView.GridControl.DoDragDrop(GetSelectedTechnicians(gridView), DragDropEffects.All);
                    m_gridDownRowInfo = null;
                }
            }
        }

        #endregion

        #region GetSelectedTechnicians

        private List<TechnicianArrange> GetSelectedTechnicians(GridView gridView)
        {
            List<TechnicianArrange> result = new List<TechnicianArrange>();

            if (gridView.RowCount == 0)
                return result;

            if (gridView.GetSelectedRows().Length == 0)
            {
                result.Add((TechnicianArrange)gridView.GetRow(gridView.FocusedRowHandle));
                return result;
            }

            foreach (int rowHandle in gridView.GetSelectedRows())
                result.Add((TechnicianArrange)gridView.GetRow(rowHandle));
            return result;
        }

        #endregion


        #region OnGridTechnicianDragOver

        private void OnGridTechnicianDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<TechnicianArrange>)))
            {
                e.Effect = DragDropEffects.Move;

                GridView destinationView = (GridView) ((GridControl)sender).MainView;

                List<TechnicianArrange> sourceTechnicians = (List<TechnicianArrange>)e.Data.GetData(
                                                                                         typeof(List<TechnicianArrange>));

                GridView sourceView = Model.OrderedTechnicians.Contains(sourceTechnicians[0])
                                          ? View.m_gridTechnicianOrderedView : View.m_gridTechnicianUnorderedView;
               
                GridHitInfo newHitInfo = destinationView.CalcHitInfo(
                    destinationView.GridControl.PointToClient(new Point(e.X, e.Y)));

                if (newHitInfo.HitTest == GridHitTest.EmptyRow)
                    DropTarget = new DropTargetInfo(sourceView, destinationView, destinationView.DataRowCount);
                else
                    DropTarget = new DropTargetInfo(sourceView, destinationView, newHitInfo.RowHandle);

                ScrollDirection = GetScrollDirection((GridViewInfo) destinationView.GetViewInfo(),
                                                     destinationView.GridControl.PointToClient(new Point(e.X, e.Y)));
            }
            else
            {
                e.Effect = DragDropEffects.None;
                ScrollDirection = ScrollDirection.None;
                DropTarget = null;
            }
        }

        #endregion

        #region OnGridTechnicianDragDrop

        private void OnGridTechnicianDragDrop(object sender, DragEventArgs e)
        {
            if (DropTarget == null || !e.Data.GetDataPresent(typeof(List<TechnicianArrange>))
                || !DropTarget.IsOutOfSelection())
            {
                DropTarget = null;
                return;
            }

            List<TechnicianArrange> sourceTechnicians = (List<TechnicianArrange>)e.Data.GetData(
                                                                                     typeof(List<TechnicianArrange>));

            GridView destinationView = (GridView)((GridControl)sender).MainView;            
            GridView sourceView = DropTarget.SourceView;

            BindingList<TechnicianArrange> sourceList = (BindingList<TechnicianArrange>) sourceView.DataSource;
            BindingList<TechnicianArrange> destinationList = (BindingList<TechnicianArrange>) destinationView.DataSource;

            TechnicianArrange mainSourceTechnician
                = (TechnicianArrange)sourceView.GetRow(m_gridDownRowInfo.RowHandle);

            TechnicianArrange newTargetTechnician
                = (TechnicianArrange)destinationView.GetRow(DropTarget.RowHandle);

            if (DropTarget.RowHandle == destinationList.Count) //After last
            {
                foreach (TechnicianArrange technician in sourceTechnicians)
                    sourceList.Remove(technician);

                foreach (TechnicianArrange technician in sourceTechnicians)
                    destinationList.Add(technician);
            }                
            else
            {
                foreach (TechnicianArrange technician in sourceTechnicians)
                    sourceList.Remove(technician);

                int newIndex = destinationList.IndexOf(newTargetTechnician);
                for (int i = 0; i < sourceTechnicians.Count; i++)
                    destinationList.Insert(i + newIndex, sourceTechnicians[i]);
            }

            sourceView.ClearSelection();
            destinationView.ClearSelection();

            foreach (TechnicianArrange technician in sourceTechnicians)
                destinationView.SelectRow(destinationView.GetRowHandle(destinationList.IndexOf(technician)));

            destinationView.FocusedRowHandle = destinationView.GetRowHandle(
                destinationList.IndexOf(mainSourceTechnician));
            destinationView.Focus();

            ScrollDirection = ScrollDirection.None;
            DropTarget = null;
        }

        #endregion

        #region OnGridTechnicianDragLeave

        private void OnGridTechnicianDragLeave(object sender, EventArgs e)
        {
            DropTarget = null;
            ScrollDirection = ScrollDirection.None;

            View.m_gridTechnicianUnordered.Invalidate();
            View.m_gridTechnicianOrdered.Invalidate();
        }

        #endregion


        #region OnGridTechnicianPaint

        private void OnGridTechnicianPaint(object sender, PaintEventArgs e)
        {
            GridView gridView = (GridView)((GridControl)sender).MainView;

            if (DropTarget == null || m_gridDownRowInfo == null || !DropTarget.IsOutOfSelection()) 
                return;
            
            bool isBottomLine = DropTarget.RowHandle == gridView.DataRowCount;

            GridViewInfo viewInfo = (GridViewInfo)gridView.GetViewInfo();
            GridRowInfo rowInfo = viewInfo.GetGridRowInfo(isBottomLine 
                                                              ? DropTarget.RowHandle - 1 : DropTarget.RowHandle);

            if (rowInfo == null)
                return;

            Point p1, p2;
            if (isBottomLine)
            {
                p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Bottom - 1);
                p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Bottom - 1);
            }
            else
            {
                p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Top - 1);
                p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Top - 1);
            }

            e.Graphics.DrawLine(new Pen(Color.Black, 2), p1, p2);            
        }

        #endregion

        #region OnScrollTimerTick

        private void OnScrollTimerTick(object sender, EventArgs e)
        {
            if (DropTarget == null)
                return;

            GridView gridView = DropTarget.DestinationView;

            ScrollDirection = GetScrollDirection((GridViewInfo) gridView.GetViewInfo(), 
                                                 gridView.GridControl.PointToClient(Cursor.Position));

            gridView.TopRowIndex += GetScrollDelta(ScrollDirection);
            View.m_scrollTimer.Interval = SCROLL_DELAY;            
        }

        private ScrollDirection GetScrollDirection(GridViewInfo viewInfo, Point mousePoint)
        {
            if (mousePoint.Y <= viewInfo.ViewRects.Rows.Top + 10)
                return ScrollDirection.Up;
            if (mousePoint.Y >= viewInfo.ViewRects.Rows.Bottom - 10)
                return ScrollDirection.Down;

            return ScrollDirection.None;
        }

        private int GetScrollDelta(ScrollDirection direction)
        {
            if (direction == ScrollDirection.None)
                return 0;
            return ScrollDirection == ScrollDirection.Up ? -1 : 1;
        }

        #endregion

        #region MoveTechnicians

        private void MoveTechnicians(GridView sourceView, GridView destimationView, bool isMoveAll)
        {            
            BindingList<TechnicianArrange> sourceList = (BindingList<TechnicianArrange>) sourceView.DataSource;
            BindingList<TechnicianArrange> destinationList = (BindingList<TechnicianArrange>) destimationView.DataSource;

            List<TechnicianArrange> techniciansToMove;
            if (isMoveAll)
                techniciansToMove = new List<TechnicianArrange>(sourceList);
            else
                techniciansToMove = GetSelectedTechnicians(sourceView);

            foreach (TechnicianArrange technician in techniciansToMove)
                sourceList.Remove(technician);

            foreach (TechnicianArrange technician in techniciansToMove)
                destinationList.Add(technician);
        }

        private void OnMoveToVisibleClick(object sender, EventArgs args)
        {
            MoveTechnicians(View.m_gridTechnicianUnorderedView, View.m_gridTechnicianOrderedView, false);
        }

        private void OnMoveToInvisibleClick(object sender, EventArgs args)
        {
            MoveTechnicians(View.m_gridTechnicianOrderedView, View.m_gridTechnicianUnorderedView, false);
        }

        private void OnMoveToVisibleAllClick(object sender, EventArgs args)
        {
            MoveTechnicians(View.m_gridTechnicianUnorderedView, View.m_gridTechnicianOrderedView, true);
        }

        private void OnMoveToInvisibleAllClick(object sender, EventArgs args)
        {
            MoveTechnicians(View.m_gridTechnicianOrderedView, View.m_gridTechnicianUnorderedView, true);
        }

        #endregion


        #region OnGridTechnicianRowCountChanged

        private void OnGridTechnicianRowCountChanged(object sender, EventArgs args)
        {
            View.m_btnMoveToVisible.Enabled = Model.UnorderedTechnicians.Count > 0;
            View.m_btnMoveToVisibleAll.Enabled = Model.UnorderedTechnicians.Count > 0;
            View.m_btnMoveToInvisible.Enabled = Model.OrderedTechnicians.Count > 0;
            View.m_btnMoveToInvisibleAll.Enabled = Model.OrderedTechnicians.Count > 0;
        }

        #endregion

        #region OnGridTechnicianKeyPress

        private void OnGridTechnicianKeyPress(object sender, KeyPressEventArgs args)
        {
            if (args.KeyChar == '\r')
            {
                GridView sourceGrid = (GridView) sender;
                GridView destinationGrid = sourceGrid == View.m_gridTechnicianUnorderedView
                                               ? View.m_gridTechnicianOrderedView : View.m_gridTechnicianUnorderedView;
                MoveTechnicians(sourceGrid, destinationGrid, false);
            }            
        }

        #endregion

  
        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (Model.UnorderedTechnicians.Count > 0)
            {
                XtraMessageBox.Show("All the technicians should be in ordered list",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                                
            }

            User.ResetLogOutTimer();
            using (new WaitCursor())
                Model.Save();
            View.Destroy();
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
