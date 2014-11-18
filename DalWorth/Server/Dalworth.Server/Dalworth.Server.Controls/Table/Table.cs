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
using System.Threading;

namespace Dalworth.Server.Controls
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
    public delegate void SelectionHandler();
    public delegate void CellValueHandler(TableCell cell);


    public class Table : Control
    {

        private enum TableMode
        {
            Interactive,
            ApplyEdit,
            BeginEdit
        }

        #region Events
        public event RowHandler RowChanged;
        public event SelectionHandler SelectionChanged;
        public event CellValueHandler CellFocusChanged;
        public event CellValueHandler CellValueChanged;
        public event CellValueHandler CellValueStartEdit;
        public event CellValueHandler CellValueEndEdit;
        public event CellValueHandler MoveCell;
        public event CellValueHandler CellClick;

        public new event CellValueHandler Enter;
        #endregion

        #region Fields

        TableMode m_mode = TableMode.Interactive;


        Dictionary<int, TableColumn> m_columns = new Dictionary<int, TableColumn>();

        Color m_selectionForeground = Color.Black;
        Color m_selectionBackground = Color.FromArgb(192, 192, 255);

        Color m_BorderColor = Color.FromArgb(224, 224, 224);

        Color m_focusCellForeColor = Color.White;
        Color m_focusCellBackColor = Color.Black;

        Color m_altBackColor = Color.Linen;
        Color m_altForeColor = Color.Black;

        Color m_columnBackColor = Color.LightGray;
        Color m_columnForeColor = Color.Black;

        Color m_splitterColor = Color.Red;
        Color m_splitterStartColor = Color.Brown;

        List<Color> m_ColorList = new List<Color>();
        List<Color> m_GreyscaleList = new List<Color>();

        int m_iSplitterWidth = 1;
        int m_iRowCount;

        bool m_bShowStartSplitter = true;
        bool m_bShowSplitterValue = true;


        SplitterMode m_splitterMode = SplitterMode.Default;

        int m_iColumnCount;

        Font m_columnFont;

        StringFormat m_defaultStringFormat;

        ITableModel m_model;

        static ITableCellRenderer m_defaultHeaderRenderer;
        static ITableCellRenderer m_defaultCellRenderer;
        //        static ITableCellEditor m_defaultCellEditor;

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

        private List<int> m_selectedRows = new List<int>();


        bool m_leftHeader;

        public bool LeftHeader
        {
            get { return m_leftHeader; }
            set { m_leftHeader = value; }
        }

        bool m_drawGridBorder = true;

        public bool DrawGridBorder
        {
            get { return m_drawGridBorder; }
            set { m_drawGridBorder = value; }
        }

        #endregion

        #region Constructors

        public Table()
        {
            InitDefaultSettings();

            BackupColors();

            BindModel(new TestModel());
        }

        public Table(ITableModel tableModel)
        {
            InitDefaultSettings();

            BackupColors();

            m_model = tableModel;
        }
        #endregion

        #region Focus

        #region InitDefaultSettings
        void InitDefaultSettings()
        {

            Controls.Add(m_hScrollBar);
            Controls.Add(m_vScrollBar);

            m_defaultStringFormat = new StringFormat();
            m_defaultStringFormat.Alignment = StringAlignment.Center;
            m_defaultStringFormat.LineAlignment = StringAlignment.Center;
            m_defaultStringFormat.FormatFlags = StringFormatFlags.NoWrap;

            m_columnFont = new Font("Tahoma", 10F, FontStyle.Regular);

            m_hScrollBar.ValueChanged += new EventHandler(OnScroll);
            m_vScrollBar.ValueChanged += new EventHandler(OnScroll);

            m_bAutoMoveRow = true;

        }
        #endregion

        #region BackupColors
        void BackupColors()
        {
            m_ColorList.Add(m_selectionForeground);
            m_ColorList.Add(m_selectionBackground);
            m_ColorList.Add(m_BorderColor);
            m_ColorList.Add(m_focusCellForeColor);
            m_ColorList.Add(m_focusCellBackColor);
            m_ColorList.Add(m_altBackColor);
            m_ColorList.Add(m_altForeColor);
            m_ColorList.Add(m_columnBackColor);
            m_ColorList.Add(m_columnForeColor);
            m_ColorList.Add(m_splitterColor);
            m_ColorList.Add(m_splitterStartColor);

            foreach (Color c in m_ColorList)
            {
                // making greyscale versions of all the colors.
                int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                m_GreyscaleList.Add(Color.FromArgb(luma, luma, luma));
            }
        }
        #endregion

        #region SwapColors

        void SwapColors()
        {
            if (m_greyOut == false)
            {
                m_selectionForeground = m_ColorList[0];
                m_selectionBackground = m_ColorList[1];
                m_BorderColor = m_ColorList[2];
                m_focusCellForeColor = m_ColorList[3];
                m_focusCellBackColor = m_ColorList[4];
                m_altBackColor = m_ColorList[5];
                m_altForeColor = m_ColorList[6];
                m_columnBackColor = m_ColorList[7];
                m_columnForeColor = m_ColorList[8];
                m_splitterColor = m_ColorList[9];
                m_splitterStartColor = m_ColorList[10];
            }
            else
            {
                m_selectionForeground = m_GreyscaleList[0];
                m_selectionBackground = m_GreyscaleList[1];
                m_BorderColor = m_GreyscaleList[2];
                m_focusCellForeColor = m_GreyscaleList[3];
                m_focusCellBackColor = m_GreyscaleList[4];
                m_altBackColor = m_GreyscaleList[5];
                m_altForeColor = m_GreyscaleList[6];
                m_columnBackColor = m_GreyscaleList[7];
                m_columnForeColor = m_GreyscaleList[8];
                m_splitterColor = m_GreyscaleList[9];
                m_splitterStartColor = m_GreyscaleList[10];
            }
        }

        #endregion

        #region OnGotFocus
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }
        #endregion

        #region OnLostFocus
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Invalidate();
        }
        #endregion

        #endregion

        #region Key, Mouse handlers

        #region IsInputKey

        protected override bool IsInputKey(Keys keyData)
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
        #endregion

        #region OnClick
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Select();
            TableCell cell = HitTest(m_iCurrentMouseX, m_iCurrentMouseY);
            if (CellClick != null)
                CellClick.Invoke(cell);

            if (cell.RowIndex > -1)
                FireCellClick(cell.RowIndex, cell.ColumnIndex);
        }

        #endregion

        #region OnMouseDown
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

                Select();
                ApplyEdit();

                if (AllowColumnResize)
                    m_drag = new DragAndDrop(e.X, e.Y);

                Invalidate();

                return;
            }


        }

        #endregion

        #region OnMouseMove
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
        #endregion

        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnMouseUp");
#endif

            base.OnMouseUp(e);

            if (m_drag != null)
            {
                m_drag.SetEnd(e.X, e.Y);

                ApplyDragDrop();

            }

        }
        #endregion

        #region OnKeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::OnKeyPress {0}",
                e.KeyChar));
#endif
            base.OnKeyPress(e);

            if (e.Handled || e.KeyChar != '\r')
                return;

            if (m_model == null)
                return;

            int rowIndex = m_currentCell != null ? m_currentCell.RowIndex : 0;
            int columnIndex = m_currentCell != null ? m_currentCell.ColumnIndex : 0;

            FireCellClick(rowIndex, columnIndex);
            FireCellEnter(rowIndex, columnIndex);

            e.Handled = true;

            Select(rowIndex, columnIndex);

        }

        #endregion

        #region OnKeyDown

        protected override void OnKeyDown(KeyEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::OnKeyDown {0}",
                e.KeyData));
#endif
            base.OnKeyDown(e);

            if (e.Handled)
                return;

            if (m_model == null)
                return;

            int rowIndex = m_currentCell != null ? m_currentCell.RowIndex : 0;
            int columnIndex = m_currentCell != null ? m_currentCell.ColumnIndex : 0;

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

        #endregion        

        #region FireCellEnter

        void FireCellEnter(int rowIndex, int columnIndex)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::FireCellEnter Row:{0}, Column:{1}",
                rowIndex, columnIndex));
#endif
            if (Enter != null)
                Enter.Invoke(new TableCell(rowIndex, columnIndex));
        }

        #endregion

        #region FireCellClick
        void FireCellClick(int rowIndex, int columnIndex)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::FireCellClick Row:{0}, Column:{1}",
                rowIndex,columnIndex));
#endif
            OnCellClick(rowIndex, columnIndex);
        }
        #endregion

        #region OnCellClick
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

            if (m_multipleSelection)
            {
                if (m_selectedRows.Contains(cell.RowIndex))
                    m_selectedRows.Remove(cell.RowIndex);
                else
                    m_selectedRows.Add(cell.RowIndex);

                if (SelectionChanged != null)
                {
                    SelectionChanged.Invoke();
                }
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
        #endregion

        #region ApplyDragDrop
        void ApplyDragDrop()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::ApplyDragDrop");
#endif
            TableCell cell = HitTest(m_drag.Start.X, m_drag.Start.Y);

            TableColumn column = m_columns[cell.ColumnIndex];

            int columnWidth;

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

        #endregion

        #region Edit

        #region BeginEdit
        public void BeginEdit()
        {
            BeginEdit(m_currentCell);
        }

        public void BeginEdit(int rowIndex, int columnIndex)
        {
            BeginEdit(new TableCell(rowIndex, columnIndex));
        }

        public void BeginEdit(TableCell cell)
        {

#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::BeginEdit {0}",cell));
#endif

            if (cell == null)
                return;

            if (Model == null
                || Model.GetRowCount() == 0
                || !Model.IsCellEditable(cell.RowIndex, cell.ColumnIndex))
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
            m_currentEditor.Select();
            m_currentEditor.LostFocus += new EventHandler(OnEditorLostFocus);
            m_currentEditor.KeyDown += new KeyEventHandler(OnEditorKeyDown);
            m_currentEditor.KeyPress += new KeyPressEventHandler(OnEditorKeyPress);

            if (m_currentCell == null
                || cell.RowIndex != m_currentCell.RowIndex)
            {

                m_currentCell = cell;

                if (RowChanged != null)
                    RowChanged.Invoke(cell.RowIndex);
            }
            else
                m_currentCell = cell;

            m_mode = TableMode.Interactive;
        }
        #endregion

        #region OnEditorKeyPress
        void OnEditorKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;

                Select();

                ApplyEdit();

                if (m_bAutoMoveRow)
                {
                    if (Select(m_currentCell.RowIndex + 1, m_currentCell.ColumnIndex))
                        BeginEdit(m_currentCell);
                }
                else if (MoveCell != null)
                    MoveCell.Invoke(m_currentCell);
            }


        }
        #endregion

        #region OnEditorKeyDown

        void OnEditorKeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::OnEditorKeyDown {0}", e.KeyData));
#endif

            if (e.KeyData == Keys.Down)
            {
                e.Handled = true;

                Select();

                ApplyEdit();
                // maybe temporary -- 87879
                if (Select(m_currentCell.RowIndex + 1, m_currentCell.ColumnIndex))
                    BeginEdit(m_currentCell);

            }
            else if (e.KeyData == Keys.Up)
            {
                e.Handled = true;
                Select();

                ApplyEdit();

                if (Select(m_currentCell.RowIndex - 1, m_currentCell.ColumnIndex))
                    BeginEdit(m_currentCell);

            }
            else if (e.KeyData == Keys.PageUp)
            {
                e.Handled = true;
                Select();
                ApplyEdit();

                MoveBackPage();

                BeginEdit();

            }
            else if (e.KeyData == Keys.PageDown)
            {
                e.Handled = true;
                Select();

                ApplyEdit();

                MoveNextPage();

                BeginEdit();

            }
            else if (e.KeyData == Keys.Escape)
            {
                e.Handled = true;

                e.SuppressKeyPress = true;

                m_mode = TableMode.ApplyEdit;

                ITableCellEditor editor = m_columns[m_currentCell.ColumnIndex].CellEditor;

                CloseEditor(editor);

                m_mode = TableMode.Interactive;

                Focus();
            }
        }

        #endregion

        #region OnEditorLostFocus
        void OnEditorLostFocus(object sender, EventArgs e)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine("Table::OnEditorLostFocus");
#endif
            ApplyEdit();
        }
        #endregion

        #region ApplyEdit
        public void ApplyEdit()
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::ApplyEdit {0}",m_currentCell));
#endif
            if (m_currentEditor == null)
                return;

            if (m_mode != TableMode.Interactive)
                return;

            m_mode = TableMode.ApplyEdit;

            ITableCellEditor editor = m_columns[m_currentCell.ColumnIndex].CellEditor;

            bool error = false;

            try
            {
                object value = editor.ExtractControlValue(this, m_currentCell.RowIndex, m_currentCell.ColumnIndex,
                    m_currentEditor);

                if (value != null
                    && !value.Equals(Model.GetValueAt(m_currentCell.RowIndex,
                    m_currentCell.ColumnIndex)))
                {

                    Model.SetValueAt(value,
                        m_currentCell.RowIndex,
                        m_currentCell.ColumnIndex);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                error = true;
            }

            CloseEditor(editor);

            if (!error && CellValueChanged != null)
                CellValueChanged.Invoke(m_currentCell);

            m_mode = TableMode.Interactive;

            if (CellValueEndEdit != null)
                CellValueEndEdit.Invoke(m_currentCell);
        }
        #endregion

        #region CloseEditor
        private void CloseEditor(ITableCellEditor editor)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine("Table::CloseEditor");
#endif
            if (editor.TableControl)
            {

                m_currentEditor.Visible = false;
                m_currentEditor.KeyDown -= new KeyEventHandler(OnEditorKeyDown);
                m_currentEditor.LostFocus -= new EventHandler(OnEditorLostFocus);
                m_currentEditor.KeyPress -= new KeyPressEventHandler(OnEditorKeyPress);

                Controls.Remove(m_currentEditor);

            }

            m_currentEditor = null;

        }
        #endregion

        #endregion

        #region Resize

        #region OnResize
        protected override void OnResize(EventArgs e)
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnResize");
#endif

            RecalculateSize();
        }
        #endregion

        #region RefreshColumnSize
        void RefreshColumnSize()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::RefreshColumnSize");
#endif

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

            if (m_bAutoColumnSize)
            {
                int width = m_iCanvasHeight < Height ? Width : Width - m_iHScrollHeight;
                int unassignColumnCount = 0;
                int assignWidth = 0;
                //int columnCount = Model.GetColumnCount();


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
        #endregion

        #region RecalculateSize
        void RecalculateSize()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::RecalculateSize");
#endif
            m_iCanvasHeight = Model.GetRowCount() * m_iDefaultRowHeight + m_iDefaultRowHeight /* Column Height */;

            RefreshColumnSize();

            m_iCurrentMouseX = 0;
            m_iCurrentMouseY = 0;
            m_iVirtualX = 0;
            m_iVirtualY = 0;

            m_iCanvasWidth = 0;

            for (int i = 0; i < Model.GetColumnCount(); i++)
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

        #endregion

        #region Properties

        #region GreyOut
        bool m_greyOut = false;

        public bool GreyOut
        {
            get { return m_greyOut; }
            set
            {
                m_greyOut = value;
                SwapColors();
            }
        }
        #endregion

        #region AllowColumnResize
        bool m_allowColumnResize;

        public bool AllowColumnResize
        {
            get { return m_allowColumnResize; }
            set { m_allowColumnResize = value; }
        }
        #endregion

        #region CurrentRowIndex
        public int CurrentRowIndex
        {
            get
            {
                if (m_currentCell != null)
                    return m_currentCell.RowIndex;

                return -1;
            }
        }
        #endregion

        #region CurrentColumnIndex
        public int CurrentColumnIndex
        {
            get
            {
                if (m_currentCell != null)
                    return m_currentCell.ColumnIndex;

                return -1;
            }
        }
        #endregion

        #region AutoColumnSize
        public bool AutoColumnSize
        {
            get { return m_bAutoColumnSize; }
            set { m_bAutoColumnSize = value; }
        }
        #endregion

        #region ShowSplitterValue
        public bool ShowSplitterValue
        {
            get { return m_bShowSplitterValue; }
            set { m_bShowSplitterValue = value; }
        }
        #endregion

        #region SplitterMode
        public SplitterMode SplitterMode
        {
            get { return m_splitterMode; }
            set { m_splitterMode = value; }
        }
        #endregion

        #region ShowStartSplitter
        public bool ShowStartSplitter
        {
            get { return m_bShowStartSplitter; }
            set { m_bShowStartSplitter = value; }
        }
        #endregion

        #region SplitterWidth
        public int SplitterWidth
        {
            get { return m_iSplitterWidth; }
            set { m_iSplitterWidth = value; }
        }
        #endregion

        #region SplitterColor
        public Color SplitterColor
        {
            get { return m_splitterColor; }
            set { m_splitterColor = value; }
        }
        #endregion

        #region SplitterStartColor
        public Color SplitterStartColor
        {
            get { return m_splitterStartColor; }
            set { m_splitterStartColor = value; }
        }
        #endregion

        #region ColumnFont
        public Font ColumnFont
        {
            get { return m_columnFont; }
            set { m_columnFont = value; }
        }
        #endregion

        #region ColumnBackColor
        public Color ColumnBackColor
        {
            get { return m_columnBackColor; }
            set { m_columnBackColor = value; }
        }
        #endregion

        #region ColumnForeColor
        public Color ColumnForeColor
        {
            get { return m_columnForeColor; }
            set { m_columnForeColor = value; }
        }
        #endregion

        #region DefaultRowHeight
        public int DefaultRowHeight
        {
            get { return m_iDefaultRowHeight; }
            set { m_iDefaultRowHeight = value; }
        }
        #endregion

        #region DefaultTextAligment
        /// <summary>
        /// Gets or sets the text alignment information on the vertical plane.
        /// </summary>
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
        #endregion

        #region DefaultLineAligment
        /// <summary>
        /// Gets or sets the line alignment on the horizontal plane.
        /// </summary>
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
        #endregion

        #region DefaultStringFormat
        internal StringFormat DefaultStringFormat
        {
            get { return m_defaultStringFormat; }
            set { m_defaultStringFormat = value; }
        }
        #endregion

        #region AltBackColor
        public Color AltBackColor
        {
            get { return m_altBackColor; }
            set { m_altBackColor = value; }
        }
        #endregion

        #region AltForeColor
        public Color AltForeColor
        {
            get { return m_altForeColor; }
            set { m_altForeColor = value; }
        }
        #endregion

        #region BorderColor
        public Color BorderColor
        {
            get { return m_BorderColor; }
            set { m_BorderColor = value; }
        }
        #endregion

        #region SelectionForeColor
        public Color SelectionForeColor
        {
            get { return m_selectionForeground; }
            set { m_selectionForeground = value; }
        }
        #endregion

        #region SelectionBackColor
        public Color SelectionBackColor
        {
            get { return m_selectionBackground; }
            set { m_selectionBackground = value; }
        }
        #endregion

        #region FocusCellForeColor
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
        #endregion

        #region FocusCellBackColor
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

        #region MultipleSelection
        private bool m_multipleSelection;

        public bool MultipleSelection
        {
            get { return m_multipleSelection; }
            set { m_multipleSelection = value; }
        }
        #endregion

        #region Model
        public ITableModel Model
        {
            get
            {
                return m_model;
            }
        }
        #endregion

        #region AutoMoveRow
        bool m_bAutoMoveRow;
        /// <summary>
        /// Automatically moved on the next row when enter pressed on the edit control.
        /// </summary>
        public bool AutoMoveRow
        {
            get { return m_bAutoMoveRow; }
            set { m_bAutoMoveRow = value; }
        }

        #endregion

        #endregion

        #region Scroll bar

        #region ShowHScroll
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
        #endregion

        #region ShowVScroll
        void ShowVScroll(bool bShow)
        {
            m_vScrollBar.Visible = bShow;

            if (bShow)
            {
                m_vScrollBar.Top = 0;
                m_vScrollBar.Left = m_iVisibleWidth;
                m_vScrollBar.Height = m_iVisibleHeight;

                m_vScrollBar.Minimum = 0;
                m_vScrollBar.Maximum = m_iCanvasHeight;

                m_vScrollBar.LargeChange = m_iVisibleHeight - m_iDefaultRowHeight > 0 ?
                    m_iVisibleHeight - m_iDefaultRowHeight : m_iDefaultRowHeight;
                m_vScrollBar.SmallChange = m_iDefaultRowHeight;
            }
        }
        #endregion

        #region OnScroll
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
        #endregion

        #region IsPairScrollShow
        bool IsPairScrollShow
        {
            get
            {
                return m_hScrollBar.Visible && m_vScrollBar.Visible;
            }
        }
        #endregion

        #endregion

        #region Model

        #region BindModel
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
        #endregion

        #region OnModelChange
        private void OnModelChange()
        {
#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::OnModelChange");
#endif
            if (m_model.GetRowCount() != m_iRowCount
                || m_model.GetColumnCount() != m_iColumnCount)
            {
                m_iRowCount = m_model.GetRowCount();
                m_iColumnCount = m_model.GetColumnCount();

                RecalculateSize();
            }

            m_selectedRows.Clear();

            Invalidate();
        }
        #endregion

        #region OnTableSettingsChanged
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
            m_selectedRows.Clear();



            RecalculateSize();

            Invalidate();
        }
        #endregion

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
                return new DefaultTableCellEditor();
            }
        }
        #endregion

        #region Brushes,Pens,Canvas, ect.



        static SolidBrush m_brush;
        internal static Brush GetBrush(Color color)
        {
            if (m_brush == null)
            {
                m_brush = new SolidBrush(color);
            }
            else
                m_brush.Color = color;


            return m_brush;
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
            if (m_pen == null)
            {
                m_pen = new Pen(color, width);
            }
            else
            {
                m_pen.Color = color;
                m_pen.Width = width;
            }

            return m_pen;
        }
        #endregion

        #region API

        #region MoveNextPage
        public void MoveNextPage()
        {
            Move(m_iVisibleRowCount);
        }
        #endregion

        #region MoveBackPage
        public void MoveBackPage()
        {
            Move(-m_iVisibleRowCount);
        }
        #endregion

        #region MoveNext
        public void MoveNext()
        {
            Move(1);
        }
        #endregion

        #region MoveBack
        public void MoveBack()
        {
            Move(-1);
        }
        #endregion

        #region Move
        public new void Move(int index)
        {
            int currentRowIndex = m_currentCell == null ? 0 : m_currentCell.RowIndex;
            int currentColumnIndex = m_currentCell == null ? 0 : m_currentCell.ColumnIndex;

            Select(currentRowIndex + index, currentColumnIndex);
        }
        #endregion

        #region Select
        public bool Select(int rowIndex)
        {
            return Select(rowIndex, 0);
        }

        public bool Select(int rowIndex, int columnIndex)
        {
#if DEBUG_TABLE_LEVEL_3
            Debug.WriteLine(String.Format("Table::Select Row:{0} Column:{1}",rowIndex,columnIndex));
#endif

            Debug.Assert(100000 != Model.GetRowCount(), "Please init your model");

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

            if (CellFocusChanged != null)
                CellFocusChanged.Invoke(m_currentCell);

            Invalidate();


            return true;

        }

        public void Select(List<int> rowIndex)
        {
            foreach (int i in rowIndex)
                if (!m_selectedRows.Contains(i))
                    m_selectedRows.Add(i);

            Invalidate();
        }
        #endregion

        #region Deselect
        public void Deselect(int rowIndex)
        {
            if (m_multipleSelection)
                m_selectedRows.Remove(rowIndex);
            else
                m_currentCell = null;

            if (SelectionChanged != null)
                SelectionChanged.Invoke();

            Invalidate();
        }

        public void Deselect(List<int> rowIndex)
        {
            foreach (int i in rowIndex)
                m_selectedRows.Remove(i);

            if (SelectionChanged != null)
                SelectionChanged.Invoke();

            Invalidate();
        }
        #endregion

        #region ClearSelectedRows
        public bool ClearSelectedRows()
        {
            bool rv = false;

            if (m_multipleSelection)
            {
                m_selectedRows.Clear();

                if (SelectionChanged != null)
                    SelectionChanged.Invoke();

                rv = true;
            }

            Invalidate();
            return rv;
        }
        #endregion

        #region SelectedRows
        public List<int> SelectedRows()
        {
            return m_selectedRows;
        }
        #endregion

        #region SetColumnWidth
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
        #endregion

        #region SetColumnName
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
        #endregion

        #region HitTest
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
        #endregion

        #region AddColumn
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
        #endregion

        #region GetColumn
        public TableColumn GetColumn(int index)
        {

            Debug.Assert(index > -1, "Column index must be great then -1");

#if DEBUG_TABLE_LEVEL_1
            Debug.WriteLine("Table::GetColumn");
#endif
            if (!m_columns.ContainsKey(index))
                m_columns.Add(index, new TableColumn(index));


            return m_columns[index];
        }
        #endregion

        #region GetSelectedItems
        public List<T> GetSelectedItems<T>()
            where T : class
        {
            List<T> list = new List<T>();

            foreach (int i in m_selectedRows)
                list.Add(Model.GetObjectAt(i, 0) as T);

            return list;
        }
        #endregion

        #region IsRowSelected
        public bool IsRowSelected(int rowIndex)
        {
            if (m_multipleSelection)
                return m_selectedRows.Contains(rowIndex);
            else if (m_currentCell != null)
                return m_currentCell.RowIndex == rowIndex;

            return false;
        }
        #endregion

        #endregion

        #region Paint

        #region GetCellRect
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
            int y;

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
        #endregion

        #region RedrawBackBuffer
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

                // Assing column width
                y += m_iDefaultRowHeight;

                // Rendering rows

                m_iVisibleRowCount = 0;

                for (int rowIndex = m_iFirstRow; rowIndex < m_model.GetRowCount(); rowIndex++)
                {
                    if (y + m_iDefaultRowHeight
                        > m_iVisibleHeight)
                        break;

                    x = 0;

                    ++m_iVisibleRowCount;

                    for (int columnIndex = m_iFirstColumn; columnIndex < columnCount; columnIndex++)
                    {
                        TableColumn tableColumn = m_columns[columnIndex];

                        ITableCellRenderer renderer;

                        if (m_leftHeader && columnIndex == 0)
                            renderer = tableColumn.HeaderRenderer;
                        else
                            renderer = tableColumn.CellRenderer;

                        DrawControl control = renderer.getTableCellRendererComponent(this,
                            Model.GetValueAt(rowIndex, columnIndex),
                            m_multipleSelection ?
                            m_selectedRows.Contains(rowIndex) :
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

                    // if (y > m_iVisibleHeight)
                    //     break;
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
                        int px;

                        if (m_splitterMode == SplitterMode.Default)
                            px = m_drag.X * -1;
                        else
                            px = m_drag.X > 0 ? m_drag.X : m_drag.X * -1;

                        graphics.DrawString(String.Format(" {0}px", px),
                            Font,
                            GetBrush(m_splitterColor),
                            m_drag.End.X, m_iVisibleHeight / 2);
                    }

                    graphics.DrawLine(GetPen(m_splitterColor, m_iSplitterWidth),
                        m_drag.End.X,
                        0,
                        m_drag.End.X,
                        m_iVisibleHeight);


                }

                //if (Focused && m_drawGridBorder)
                //{
                //    graphics.DrawRectangle(GetPen(m_BorderColor, 3), this.ClientRectangle);
                //}
            }
        }
        #endregion

        #region OnPaint
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
        #endregion

        #region OnPaintBackground
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

            private static string[] columnNames = { "Col0", "Col1" };

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
