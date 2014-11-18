#if DEBUG
//#define DEBUG_TABLE_LEVEL_3
//#define DEBUG_TABLE_LEVEL_1
//#define DEBUG_TABLE_LEVEL_2
#endif

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;

namespace MobileTech.Windows.UI.Controls
{

    public enum SplitterMode
    {
        ResizeByRect,
        Default
    }


    /// <summary>
    /// 
    /// </summary>

    public delegate void RowHandler(int rowIndex);
    public delegate void CellValueHandler(TableCell cell);


    public class Table:Control
    {

        private enum TableMode
        {
            Interactive,
            ApplyEdit,
            BeginEdit
        }

        #region Events
        public event RowHandler RowChanged;
        public event CellValueHandler CellValueChanged;
        public event CellValueHandler CellValueStartEdit;
        public event CellValueHandler Enter;
        #endregion

        #region Fields

        TableMode m_mode = TableMode.Interactive;


        Dictionary<int, TableColumn> m_columns = new Dictionary<int, TableColumn>();

        Color m_selectionForeground = Color.Black;
        Color m_selectionBackground = System.Drawing.Color.FromArgb(192,192,255);

        Color m_BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);

        Color m_focusCellForeColor = Color.White;
        Color m_focusCellBackColor = Color.Black;

        Color m_altBackColor = Color.Linen;
        Color m_altForeColor = Color.Black;

        Color m_columnBackColor = Color.LightGray;
        Color m_columnForeColor = Color.Black;

        Color m_splitterColor = Color.Red;
        Color m_splitterStartColor = Color.Brown;

        int m_iSplitterWidth = 1;
        int m_iRowCount;

        bool m_bShowStartSplitter = true;
        bool m_bShowSplitterValue = true;


        SplitterMode m_splitterMode = SplitterMode.Default;



        Font m_columnFont;

        StringFormat m_defaultStringFormat;

        ITableModel m_model;

        static ITableCellRenderer m_defaultHeaderRenderer;
        static ITableCellRenderer m_defaultCellRenderer;
        static ITableCellEditor m_defaultCellEditor;

        HScrollBar m_hScrollBar = new HScrollBar();
        VScrollBar m_vScrollBar = new VScrollBar();

        bool m_bAutoColumnSize = true;


        int m_iDefaultRowHeight = 20;

        int m_iCanvasWidth;
        int m_iCanvasHeight;

        int m_iVirtualX;
        int m_iVirtualY;

        int m_iVisibleHeight;
        int m_iVisibleWidth;

        const int m_iHScrollHeight = 13;
        const int m_iVScrollWidth = 13;


        int m_iAvgColumnSize;

        int m_iFirstRow;
        int m_iFirstColumn;

        int m_iCurrentMouseX = 0;
        int m_iCurrentMouseY = 0;

        int m_iVisibleRowCount = 0;

        TableCell m_currentCell;
        Control m_currentEditor;

        DragAndDrop m_drag;

        #endregion

        #region Constructors

        public Table() 
        {
            InitControls();

            BindModel(new TestModel());
        }

        public Table(ITableModel tableModel)
        {
            InitControls();

            m_model = tableModel;
        }

        void InitControls()
        {

            Controls.Add(m_hScrollBar);
            Controls.Add(m_vScrollBar);

            m_defaultStringFormat = new StringFormat();
            m_defaultStringFormat.Alignment = StringAlignment.Center;
            m_defaultStringFormat.LineAlignment = StringAlignment.Center;

            m_columnFont = new Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);

            m_hScrollBar.ValueChanged += new EventHandler(OnScroll);
            m_vScrollBar.ValueChanged += new EventHandler(OnScroll);

        }

        #endregion

        #region Key, Mouse handlers

#if WIN32
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }
#endif

        protected override void OnMouseDown(MouseEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnMouseDown");
#endif

            base.OnMouseDown(e);

            m_iCurrentMouseX = e.X;
            m_iCurrentMouseY = e.Y;

            TableCell cell = HitTest(m_iCurrentMouseX, m_iCurrentMouseY);


            if (cell.RowIndex == -1)
            {
                ApplyEdit();

                if(AllowColumnResize)
                    m_drag = new DragAndDrop(e.X, e.Y);

                Invalidate();

                return;
            }


        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

#if WINCE
            Focus();
#else
            Select();
#endif

            TableCell cell = HitTest(m_iCurrentMouseX, m_iCurrentMouseY);

            if (cell.RowIndex > -1)
                FireCellClick(cell.RowIndex, cell.ColumnIndex);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnMouseMove");
#endif

            base.OnMouseMove(e);

            if (m_drag != null)
            {
                m_drag.SetEnd(e.X, e.Y);

                Invalidate();
            }
  
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnMouseUp");
#endif

            base.OnMouseUp(e);

            if (m_drag != null)
            {
                m_drag.SetEnd(e.X,e.Y);

                ApplyDragDrop();

            }

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::OnKeyUp {0}",
                e.KeyData));
#endif
            base.OnKeyDown(e);

            if(e.Handled)
                return;

            if (m_model == null)
                return;

            int rowIndex = m_currentCell != null ? m_currentCell.RowIndex : 0;
            int columnIndex = m_currentCell != null  ? m_currentCell.ColumnIndex : 0;

            switch (e.KeyData)
            {
                case Keys.Up:
                    if (rowIndex > 0)
                        --rowIndex;
                    break;
                case Keys.Down:
                    if (rowIndex < Model.GetRowCount() - 1)
                        ++rowIndex;
                    break;
                case Keys.Left:
                    if (columnIndex > 0)
                        --columnIndex;
                    break;
                case Keys.Right:
                    if (columnIndex < Model.GetColumnCount() - 1)
                        ++columnIndex;
                    break;
                case Keys.Return:
                    FireCellClick(rowIndex, columnIndex);
                    FireCellEnter(rowIndex, columnIndex);
                    return;
                case Keys.PageUp:
                    rowIndex = m_iFirstRow - (m_iVisibleHeight / m_iDefaultRowHeight) + 1;
                    break;
                case Keys.PageDown:
                    rowIndex = m_iFirstRow + m_iVisibleRowCount - 1;
                    break;
                default:
                    return;
            }

            e.Handled = true;

            Select(rowIndex, columnIndex);
        }

        void FireCellEnter(int rowIndex, int columnIndex)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::FireCellEnter Row:{0}, Column:{1}",
                rowIndex, columnIndex));
#endif
            if(Enter != null)
                Enter.Invoke(new TableCell(rowIndex,columnIndex));
        }

        void FireCellClick(int rowIndex, int columnIndex)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::FireCellClick Row:{0}, Column:{1}",
                rowIndex,columnIndex));
#endif
            OnCellClick(rowIndex, columnIndex);
        }


        void OnCellClick(int rowIndex, int columnIndex)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::OnCellClick Row:{0}, Column:{1}",
                rowIndex, columnIndex));
#endif

            if (m_iRowCount == 0)
                return;

            TableCell cell = new TableCell(rowIndex, columnIndex);

            ApplyEdit();
   
            if (Model.IsCellEditable(cell.RowIndex,
                cell.ColumnIndex))
            {
                BeginEdit(cell);
            }
         

            TableCell oldCell = m_currentCell;

            m_currentCell = cell;

            Invalidate();


            if (oldCell == null
                || cell.RowIndex != oldCell.RowIndex)
            {
                if (RowChanged != null)
                {
                    RowChanged.Invoke(cell.RowIndex);
                }
            }
        }

        void ApplyDragDrop()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::ApplyDragDrop");
#endif
            TableCell cell = HitTest(m_drag.Start.X, m_drag.Start.Y);

            TableColumn column = m_columns[cell.ColumnIndex];

            int columnWidth = 0;

            if (m_splitterMode == SplitterMode.ResizeByRect)
            {
                columnWidth = m_drag.X > 0 ? m_drag.X : m_drag.X * -1;
            }
            else
                columnWidth = column.Width - m_drag.X;


            column.Width = columnWidth;

            m_drag = null;

            RecalculateSize();

            Invalidate();
        }


        #endregion

        #region Edit

        public void BeginEdit()
        {
            BeginEdit(m_currentCell);
        }

        public void BeginEdit(TableCell cell)
        {

#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::BeginEdit {0}",cell));
#endif

            if (cell == null)
                return;

            Rectangle rect = GetCellRect(cell.ColumnIndex, cell.RowIndex);

            if (m_mode != TableMode.Interactive)
                return;

            m_mode = TableMode.BeginEdit;

            m_currentEditor = m_columns[cell.ColumnIndex].CellEditor.getTableCellEditorComponent(
                this,
                Model.GetValueAt(cell.RowIndex, cell.ColumnIndex),
                true,
                cell.RowIndex,
                cell.ColumnIndex);

            if (m_columns[cell.ColumnIndex].CellEditor.TableControl)
            {
                m_currentEditor.Bounds = rect;
                Controls.Add(m_currentEditor);
            }

            if (m_currentEditor is TextBox)
                ((TextBox)m_currentEditor).SelectAll();

            if (CellValueStartEdit != null)
                CellValueStartEdit.Invoke(cell);
           
            m_currentEditor.Show();
#if WINCE
            m_currentEditor.Focus();
#else
            m_currentEditor.Select();
#endif
            m_currentEditor.LostFocus += new EventHandler(OnEditorLostFocus);
            m_currentEditor.KeyDown += new KeyEventHandler(OnEditorKeyDown);
            m_currentEditor.KeyPress += new KeyPressEventHandler(OnEditorKeyPress);
           

            m_mode = TableMode.Interactive;
        }

        void OnEditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        void OnEditorKeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::OnEditorKeyDown {0}", e.KeyData));
#endif

            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.Handled = true;

#if WINCE
                Focus();
#else
                Select();
#endif

                ApplyEdit();

                // maybe temporary -- 87879
                if (Select(m_currentCell.RowIndex + 1, m_currentCell.ColumnIndex))
                    BeginEdit(m_currentCell);

            }
            else if (e.KeyData == Keys.Down) // -- 87879
            {
                e.Handled = true;
#if WINCE
                Focus();
#else
                Select();
#endif
                ApplyEdit();

                if (Select(m_currentCell.RowIndex + 1, m_currentCell.ColumnIndex))
                    BeginEdit(m_currentCell);

            }
            else if (e.KeyData == Keys.Up)
            {
                e.Handled = true;
#if WINCE
                Focus();
#else
                Select();
#endif

                ApplyEdit();

                if (Select(m_currentCell.RowIndex - 1, m_currentCell.ColumnIndex))
                    BeginEdit(m_currentCell);
                    
            }
            else if (e.KeyData == Keys.PageUp)
            {
                e.Handled = true;
#if WINCE
                Focus();
#else
                Select();
#endif

                ApplyEdit();

                MoveBackPage();

                BeginEdit();

            }
            else if (e.KeyData == Keys.PageDown)
            {
                e.Handled = true;
#if WINCE
                Focus();
#else
                Select();
#endif

                ApplyEdit();

                MoveNextPage();

                BeginEdit();

            }
        }

        void OnEditorLostFocus(object sender, EventArgs e)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine("Table::OnEditorLostFocus");
#endif



            ApplyEdit();
        }



        public void ApplyEdit()
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::ApplyEdit {0}",m_currentCell));
#endif
            if (m_currentEditor == null)
                return;

            if (m_mode == TableMode.ApplyEdit)
                return;

            m_mode = TableMode.ApplyEdit;

            ITableCellEditor editor = m_columns[m_currentCell.ColumnIndex].CellEditor;

            bool error = false;

            try
            {
                object value = editor.ExtractControlValue(this, m_currentCell.RowIndex, m_currentCell.ColumnIndex,
                    m_currentEditor);

                Model.SetValueAt(value,
                    m_currentCell.RowIndex,
                    m_currentCell.ColumnIndex);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                error = true;
            }


            if (editor.TableControl)
            {

                m_currentEditor.Visible = false;
                m_currentEditor.KeyDown -= new KeyEventHandler(OnEditorKeyDown);
                m_currentEditor.LostFocus -= new EventHandler(OnEditorLostFocus);
                m_currentEditor.KeyPress -= new KeyPressEventHandler(OnEditorKeyPress);


                Controls.Remove(m_currentEditor);
            }

            m_currentEditor = null;


            if (!error && CellValueChanged != null)
                CellValueChanged.Invoke(m_currentCell);


            m_mode = TableMode.Interactive;

        }
        #endregion

        #region Resize

        protected override void OnResize(EventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnResize");
#endif

            RecalculateSize();
        }

        void RefreshColumnSize()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::RefreshColumnSize");
#endif

            if (m_bAutoColumnSize)
            {
                int width = m_iCanvasHeight < Height ? Width : Width - m_iHScrollHeight;
                int unassignColumnCount = 0;
                int assignWidth = 0;
                int columnCount = m_columns.Count;


                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    if (m_columns[columnIndex].AllowAutoWidth)
                        ++unassignColumnCount;
                    else
                        assignWidth += m_columns[columnIndex].Width;
                }

                if (unassignColumnCount > 0)
                {
                    int columnWidth = (width - assignWidth) / unassignColumnCount;

                    for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                    {
                        if (m_columns[columnIndex].AllowAutoWidth)
                        {
                            m_columns[columnIndex].Width = columnWidth;
                            m_columns[columnIndex].AllowAutoWidth = true;
                        }
                    }
                }

            }
        }

        void RecalculateSize()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::RecalculateSize");
#endif
            RefreshColumnSize();

            m_iCanvasWidth = 0;
            m_iCanvasHeight = Model.GetRowCount() * m_iDefaultRowHeight + m_iDefaultRowHeight /* Column Height */;

            for (int i = 0; i < m_columns.Count; i++)
            {
                m_iCanvasWidth += m_columns[i].Width;
            }

            m_iAvgColumnSize = m_iCanvasWidth / Model.GetColumnCount();


            m_iVisibleWidth = Width;
            m_iVisibleHeight = Height;

            if (m_iCanvasWidth > Width)
                m_iVisibleHeight -= m_iVScrollWidth;
            else
                m_iFirstColumn = 0;

            if (m_iCanvasHeight > Height)
                m_iVisibleWidth -= m_iHScrollHeight;
            else
                m_iFirstRow = 0;

            ShowHScroll(m_iVisibleWidth < m_iCanvasWidth);
            ShowVScroll(m_iVisibleHeight < m_iCanvasHeight);

            if (m_currentEditor != null)
            {
                if (m_columns[m_currentCell.ColumnIndex].CellEditor.TableControl)
                {
                    m_currentEditor.Bounds = GetCellRect(m_currentCell.ColumnIndex,
                        m_currentCell.RowIndex);
                }
            }

            if (m_currentCell != null)
            {
                Select(m_currentCell.RowIndex, m_currentCell.ColumnIndex);
            }
        }


        #endregion

        #region Properties

        bool m_allowColumnResize;

        public bool AllowColumnResize
        {
            get { return m_allowColumnResize; }
            set { m_allowColumnResize = value; }
        }

        public int CurrentRowIndex
        {
            get
            {
                if (m_currentCell != null)
                    return m_currentCell.RowIndex;

                return -1;
            }
        }

        public bool AutoColumnSize
        {
            get { return m_bAutoColumnSize; }
            set { m_bAutoColumnSize = value; }
        }

        public bool ShowSplitterValue
        {
            get { return m_bShowSplitterValue; }
            set { m_bShowSplitterValue = value; }
        }

        public SplitterMode SplitterMode
        {
            get { return m_splitterMode; }
            set { m_splitterMode = value; }
        }

        public bool ShowStartSplitter
        {
            get { return m_bShowStartSplitter; }
            set { m_bShowStartSplitter = value; }
        }

        public int SplitterWidth
        {
            get { return m_iSplitterWidth; }
            set { m_iSplitterWidth = value; }
        }

        public Color SplitterColor
        {
            get { return m_splitterColor; }
            set { m_splitterColor = value; }
        }


        public Color SplitterStartColor
        {
            get { return m_splitterStartColor; }
            set { m_splitterStartColor = value; }
        }

        public Font ColumnFont
        {
            get { return m_columnFont; }
            set { m_columnFont = value; }
        }

        public Color ColumnBackColor
        {
            get { return m_columnBackColor; }
            set { m_columnBackColor = value; }
        }

        public Color ColumnForeColor
        {
            get { return m_columnForeColor; }
            set { m_columnForeColor = value; }
        }

        public int DefaultRowHeight
        {
            get { return m_iDefaultRowHeight; }
            set { m_iDefaultRowHeight = value; }
        }

        public StringAlignment DefaultTextAligment
        {
            get
            {
                return m_defaultStringFormat.Alignment;
            }
            set
            {
                m_defaultStringFormat.Alignment = value;
            }
        }


        public StringAlignment DefaultLineAligment
        {
            get
            {
                return m_defaultStringFormat.LineAlignment;
            }
            set
            {
                m_defaultStringFormat.LineAlignment = value;
            }
        }


        internal StringFormat DefaultStringFormat
        {
            get { return m_defaultStringFormat; }
            set { m_defaultStringFormat = value; }
        }

        public Color AltBackColor
        {
            get { return m_altBackColor; }
            set { m_altBackColor = value; }
        }

        public Color AltForeColor
        {
            get { return m_altForeColor; }
            set { m_altForeColor = value; }
        }


        public Color BorderColor
        {
            get { return m_BorderColor; }
            set { m_BorderColor = value; }
        }


        public Color SelectionForeColor
        {
            get { return m_selectionForeground; }
            set { m_selectionForeground = value; }
        }

        public Color SelectionBackColor
        {
            get { return m_selectionBackground; }
            set { m_selectionBackground = value; }
        }

        public Color FocusCellForeColor
        {
            get
            {
                return m_focusCellForeColor;
            }
            set
            {
                m_focusCellForeColor = value;
            }
        }

        public Color FocusCellBackColor
        {
            get
            {
                return m_focusCellBackColor;
            }
            set
            {
                m_focusCellBackColor = value;
            }
        }

        #endregion

        #region Scroll bar

        void ShowHScroll(bool bShow)
        {
            m_hScrollBar.Visible = bShow;

            if (bShow)
            {
                m_hScrollBar.Top = m_iVisibleHeight;
                m_hScrollBar.Left = 0;
                m_hScrollBar.Width = m_iVisibleWidth;

                m_hScrollBar.Minimum = 0;
                m_hScrollBar.Maximum = m_iCanvasWidth;
                m_hScrollBar.LargeChange = m_iAvgColumnSize;
                m_hScrollBar.SmallChange = m_hScrollBar.LargeChange;
            }
        }

        void ShowVScroll(bool bShow)
        {
            m_vScrollBar.Visible = bShow;

            if (bShow)
            {
                m_vScrollBar.Top = 0;
                m_vScrollBar.Left = m_iVisibleWidth-3;
                m_vScrollBar.Height = m_iVisibleHeight;

                m_vScrollBar.Minimum = 0;
                m_vScrollBar.Maximum = m_iCanvasHeight;

                m_vScrollBar.LargeChange = m_iVisibleHeight-m_iDefaultRowHeight > 0 ?
                    m_iVisibleHeight - m_iDefaultRowHeight : m_iDefaultRowHeight;
                m_vScrollBar.SmallChange = m_iDefaultRowHeight;
            }
        }


        void OnScroll(object sender, EventArgs e)
        {
            if (sender == m_vScrollBar)
            {
                int scrollRow = m_vScrollBar.Value / DefaultRowHeight;

                if (scrollRow > Model.GetRowCount())
                    m_iFirstRow = Model.GetRowCount() - 1;
                else
                    m_iFirstRow = scrollRow;


                m_iVirtualY = m_iFirstRow * DefaultRowHeight;
            }
            else
            {
                int scrollColumn = m_hScrollBar.Value / m_iAvgColumnSize;

                if (scrollColumn > m_columns.Count)
                    m_iFirstColumn = m_columns.Count - 1;
                else
                    m_iFirstColumn = scrollColumn;

                m_iVirtualX = m_iFirstColumn * m_iAvgColumnSize;

            }


            if (m_currentEditor != null)
            {
                if (m_columns[m_currentCell.ColumnIndex].CellEditor.TableControl)
                {
                    /*
                    m_currentEditor.Bounds = GetCellRect(m_currentCell.ColumnIndex,
                        m_currentCell.RowIndex);*/

                    ApplyEdit();
                }
            }

            Invalidate();
        }

        bool IsPairScrollShow
        {
            get
            {
                return m_hScrollBar.Visible && m_vScrollBar.Visible;
            }
        }

        #endregion

        #region Model
        //[EditorBrowsable(EditorBrowsableState.Never)]
        public ITableModel Model
        {
            get
            {
                return m_model;
            }
        }

        public void BindModel(ITableModel model)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::BindModel");
#endif

            if (model == null)
                throw new Exception("Model can not be null");

            ApplyEdit();


            m_model = model;

            m_model.Change += new TableModelChangeHandler(OnModelChange);

            OnTableSettingsChanged();

        }

        private void OnModelChange()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnModelChange");
#endif
            if (m_model.GetRowCount() != m_iRowCount)
            {
                m_iRowCount = m_model.GetRowCount();

                RecalculateSize();
            }

            Invalidate();
        }

        void OnTableSettingsChanged()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnTableSettingsChanged");
#endif

            m_currentCell = null;
            m_iAvgColumnSize = 0;
            m_iFirstColumn = 0;
            m_iFirstRow = 0;
            m_iVirtualX = 0;
            m_iVirtualY = 0;

            m_hScrollBar.Value = 0;
            m_vScrollBar.Value = 0;

            m_iRowCount = m_model.GetRowCount();

            int columnCount = m_model.GetColumnCount();


            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                TableColumn tableColumn = GetColumn(columnIndex);

                if (tableColumn.Value == null)
                {
                    tableColumn.Value = Model.GetColumnName(columnIndex);
                }

                if (tableColumn.HeaderRenderer == null)
                    tableColumn.HeaderRenderer = DefaultHeaderRenderer;

                if (tableColumn.CellRenderer == null)
                    tableColumn.CellRenderer = DefaultCellRenderer;

                if (tableColumn.CellEditor == null)
                    tableColumn.CellEditor = DefaultCellEditor;

            }

            RecalculateSize();

            Invalidate();
        }


        #endregion

        #region Default Renderers and Editors

        public static ITableCellRenderer DefaultCellRenderer
        {
            get
            {
                if (m_defaultCellRenderer == null)
                {
                    m_defaultCellRenderer = new DefaultTableCellRenderer();
                }

                return m_defaultCellRenderer;
            }
        }

        public static ITableCellRenderer DefaultHeaderRenderer
        {
            get
            {
                if (m_defaultHeaderRenderer == null)
                {
                    m_defaultHeaderRenderer = new DefaultTableHeaderRenderer();
                }

                return m_defaultHeaderRenderer;
            }
        }

        public static ITableCellEditor DefaultCellEditor
        {
            get
            {
                if (m_defaultCellEditor == null)
                {
                    m_defaultCellEditor = new DefaultTableCellEditor();
                }

                return m_defaultCellEditor;
            }
        }
        #endregion

        #region Brushes,Pens,Canvas, ect.



        static SolidBrush m_brush;
        internal static Brush GetBrush(Color color)
        {
            if (Table.m_brush == null)
            {
                Table.m_brush = new SolidBrush(color);
            }
            else
                Table.m_brush.Color = color;

            
            return Table.m_brush;
        }

        static Pen m_pen;
        internal static Pen GetPen()
        {
            return GetPen(Color.Black, 1);
        }

        internal static Pen GetPen(Color color)
        {
            return GetPen(color, 1);
        }

        internal static Pen GetPen(Color color, float width)
        {
            if (Table.m_pen == null)
            {
                Table.m_pen = new Pen(color, width);
            }
            else
            {
                Table.m_pen.Color = color;
                Table.m_pen.Width = width;
            }

            return Table.m_pen;
        }
        #endregion

        #region API

        public void MoveNextPage()
        {
            Move(m_iVisibleRowCount);
        }

        public void MoveBackPage()
        {
            Move(-m_iVisibleRowCount);
        }

        public void MoveNext()
        {
            Move(1);
        }

        public void MoveBack()
        {
            Move(-1);
        }

#if WIN32
        public new void Move(int index)
#else
        public void Move(int index)
#endif
        {
            int currentIndex = m_currentCell == null ? 0 : m_currentCell.RowIndex;

            Select(currentIndex + index);
        }

        public bool Select(int rowIndex)
        {
            return Select(rowIndex, 0);
        }

        public bool Select(int rowIndex, int columnIndex)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::Select Row:{0} Column:{1}",rowIndex,columnIndex));
#endif

            if (m_model == null)
                return false;

            if (Model.GetRowCount() == 0)
                return false;

            if (Model.GetColumnCount() <= columnIndex)
                return false;

            int currentRowIndex = m_currentCell != null ? 
                m_currentCell.RowIndex : 0;

            if (Model.GetRowCount() <= rowIndex)
                rowIndex = Model.GetRowCount() - 1;

            if (rowIndex < 0)
                rowIndex = 0;

            m_currentCell = new TableCell(rowIndex, columnIndex);

            if (m_vScrollBar.Visible/* && m_currentCell.RowIndex != rowIndex*/)
            {
                if (rowIndex + 2 > (m_iFirstRow + m_iVisibleRowCount)
                    || rowIndex < m_iFirstRow)
                {
                    if (currentRowIndex == rowIndex - 1)
                        m_vScrollBar.Value += m_iDefaultRowHeight;
                    else
                        m_vScrollBar.Value = rowIndex * m_iDefaultRowHeight;
                }
            }

            if (RowChanged != null)
                RowChanged.Invoke(rowIndex);

                Invalidate();


                return true;
            
        }

        public void SetColumnWidth(int columnIndex, int width)
        {

#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::SetColumnWidth");
#endif

            if (columnIndex >= m_columns.Count)
                throw new Exception("Invalid column index");

            TableColumn column = m_columns[columnIndex];


             column.Width = width;

             RecalculateSize();

        }

        public void SetColumnName(int columnIndex, String name)
        {

#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::SetColumnName");
#endif

            if (columnIndex >= m_columns.Count)
                throw new Exception("Invalid column index");

            TableColumn column = m_columns[columnIndex];


            column.Name = name;

        }


        public TableCell HitTest(int x, int y)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::HitTest");
#endif

            TableCell tableCell = new TableCell();

            int columnIndex = 0;
            int totalColumnWidth = 0;

            while (columnIndex < m_columns.Count)
            {
                if (x > totalColumnWidth && x < totalColumnWidth + m_columns[columnIndex].Width)
                    break;
 
                totalColumnWidth += m_columns[columnIndex].Width;
                ++columnIndex;
            }


            tableCell.ColumnIndex = columnIndex;// (m_iVirtualX + x) / m_iAvgColumnSize;

            if (tableCell.ColumnIndex >= m_columns.Count)
                tableCell.ColumnIndex = m_columns.Count - 1;

            if (y >= DefaultRowHeight)
            {
                tableCell.RowIndex = (m_iVirtualY + y) / DefaultRowHeight;

                if (tableCell.RowIndex > Model.GetRowCount())
                    tableCell.RowIndex = Model.GetRowCount();

                --tableCell.RowIndex;

            }
            else
                tableCell.RowIndex = -1;


            return tableCell;
        }

        public void AddColumn(TableColumn column)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::AddColumn");
#endif
            if (m_columns.ContainsKey(column.Index))
                m_columns.Remove(column.Index);

            m_columns.Add(column.Index, column);

            OnTableSettingsChanged();
        }

        public TableColumn GetColumn(int index)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::GetColumn");
#endif
            if (!m_columns.ContainsKey(index))
                m_columns.Add(index, new TableColumn(index));
            

            return m_columns[index];
        }
        #endregion

        #region Paint

        Bitmap m_backBuffer;

        Rectangle GetCellRect(int x, int y, int width, int height)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::GetCellRect");
#endif
            int allowWidth, allowHeight;

            if (x + width > m_iVisibleWidth)
                allowWidth = m_iVisibleWidth - x;
            else
                allowWidth = width;


            if (y + height > m_iVisibleHeight)
                allowHeight = m_iVisibleHeight - y;
            else
                allowHeight = height;

            return new Rectangle(x, y, allowWidth, allowHeight);
        }

        Rectangle GetCellRect(int columnIndex, int rowIndex)
        {

            int i = 0;
            int x = 0;
            int y = 0;

            while (i < columnIndex)
                x += m_columns[i++].Width;

            y = (rowIndex + 1) * DefaultRowHeight;

            x -= m_iVirtualX;

            y -= m_iVirtualY;

            return GetCellRect(
                x, 
                y, 
                m_columns[columnIndex].Width, 
                DefaultRowHeight);

        }

        void RedrawBackBuffer()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::RedrawBackBuffer");
#endif
            m_backBuffer = new Bitmap(m_iVisibleWidth, m_iVisibleHeight);

            using (Graphics graphics = Graphics.FromImage(m_backBuffer))
            {
                graphics.FillRectangle(GetBrush(BackColor), 0, 0, m_iVisibleWidth, m_iVisibleHeight);

                int columnCount = m_model.GetColumnCount();

                int x = 0, y = 0;

                int selectedColumnIndex = m_currentCell != null ? m_currentCell.ColumnIndex : -1;
                int selectedRowIndex = m_currentCell != null ? m_currentCell.RowIndex : -1;

                // Rendering columns

                for (int columnIndex = m_iFirstColumn; columnIndex < columnCount; columnIndex++)
                {
                    TableColumn tableColumn = m_columns[columnIndex];

                    ITableCellRenderer renderer = tableColumn.HeaderRenderer;

                    DrawControl control = renderer.getTableCellRendererComponent(this,
                            tableColumn.Name == null ? 
                            Model.GetColumnName(columnIndex) : 
                            tableColumn.Name,
                            false,
                            false,
                            -1,
                            columnIndex);


                    control.Rect = GetCellRect(x, y, tableColumn.Width, m_iDefaultRowHeight);
                    control.Draw(graphics);

                    x += tableColumn.Width;

                    if (x > m_iVisibleWidth)
                        break;
                }

                y += m_iDefaultRowHeight;

                // Rendering rows

                m_iVisibleRowCount = 0;

                for (int rowIndex = m_iFirstRow; rowIndex < m_model.GetRowCount(); rowIndex++)
                {
                    x = 0;

                    ++m_iVisibleRowCount;

                    for (int columnIndex = m_iFirstColumn; columnIndex < columnCount; columnIndex++)
                    {
                        TableColumn tableColumn = m_columns[columnIndex];

                        ITableCellRenderer renderer = tableColumn.CellRenderer;

                        DrawControl control = renderer.getTableCellRendererComponent(this,
                            Model.GetValueAt(rowIndex, columnIndex),
                            rowIndex == selectedRowIndex,
                            rowIndex == selectedRowIndex && columnIndex == selectedColumnIndex,
                            rowIndex,
                            columnIndex);

                        control.Rect = GetCellRect(x, y, tableColumn.Width, m_iDefaultRowHeight);

                        control.Draw(graphics);

                        x += tableColumn.Width;


                        if (x > m_iVisibleWidth)
                            break;
                    }

                    y += m_iDefaultRowHeight;

                    if (y > m_iVisibleHeight)
                        break;
                }


                if (m_drag != null)
                {

                    if (m_bShowStartSplitter)
                    {

                        graphics.DrawLine(GetPen(m_splitterStartColor, m_iSplitterWidth),
                            m_drag.Start.X,
                            0,
                            m_drag.Start.X,
                            m_iVisibleHeight);

                        if (m_splitterMode == SplitterMode.ResizeByRect)
                        {
                            graphics.DrawLine(GetPen(m_splitterColor, m_iSplitterWidth),
                                m_drag.Start.X,
                                0,
                                m_drag.End.X,
                                0);
                        }
                    }

                    if (m_bShowSplitterValue)
                    {
                        int px = 0;

                        if(m_splitterMode == SplitterMode.Default)
                            px = m_drag.X * -1;
                        else
                            px = m_drag.X > 0 ? m_drag.X : m_drag.X * -1;

                        graphics.DrawString(String.Format(" {0}px",px),
                            Font,
                            GetBrush(m_splitterColor),
                            m_drag.End.X, m_iVisibleHeight / 2);
                    }

                    graphics.DrawLine(GetPen(m_splitterColor,m_iSplitterWidth),
                        m_drag.End.X, 
                        0,
                        m_drag.End.X, 
                        m_iVisibleHeight);


                }

                if (Focused)
                {
                    graphics.DrawRectangle(GetPen(m_BorderColor), this.ClientRectangle);
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnPaint");
#endif
            if (m_model == null)
                return;

            RedrawBackBuffer();

            e.Graphics.DrawImage(m_backBuffer, 0, 0);

            m_backBuffer.Dispose();
            m_backBuffer = null;
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnPaintBackground");
#endif
            if (IsPairScrollShow)
            {
                e.Graphics.FillRectangle(GetBrush(BackColor), 
                    m_iVisibleWidth,
                    m_iVisibleHeight, 
                    m_iHScrollHeight, 
                    m_iVisibleHeight);
            }
        }

        #endregion

        #region TestModel
        public class TestModel : ITableModel
        {

            int m_rowCount;

            public TestModel()
            {
                m_rowCount = 100000;
            }

            public TestModel(int rowCount)
            {
                m_rowCount = rowCount;
            }

            #region ITableModel Members

            private static string[] columnNames = { "Col0", "Col1"};

            public int GetRowCount()
            {
                return m_rowCount;
            }

            public int GetColumnCount()
            {
                return columnNames.Length;
            }

            public string GetColumnName(int columnIndex)
            {
                return columnNames[columnIndex];
            }

            public Type GetColumnClass(int columnIndex)
            {
                return String.Empty.GetType();
            }

            public bool IsCellEditable(int rowIndex, int columnIndex)
            {
                return true;
            }

            public object GetValueAt(int rowIndex, int columnIndex)
            {
                return String.Format("{0}:{1}", rowIndex, columnIndex);
            }

            public void SetValueAt(object aValue, int rowIndex, int columnIndex)
            {
                if (Change != null)
                    Change.Invoke();
            }

            public object GetObjectAt(int rowIndex, int columnIndex)
            {
                return String.Empty;
            }

            #endregion

            #region ITableModel Members

            public event TableModelChangeHandler Change;

            #endregion
}
        #endregion
    }
}
