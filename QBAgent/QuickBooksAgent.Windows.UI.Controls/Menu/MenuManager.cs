using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Controls.Menu
{
    public partial class MenuManager : Panel
    {
        private const int SCROLL_WIDTH = 13;
        private const int SCROLL_INDENT = 2;

        private enum DirectionEnum { Left, Right, Up, Down }

        #region Fields

        #region HorisontalInterval

        private int m_horisontalInterval = 5;
        public int HorisontalInterval
        {
            get { return m_horisontalInterval; }
            set { m_horisontalInterval = value; }
        }

        #endregion

        #region VerticalInterval

        private int m_verticalInterval = 5;
        public int VerticalInterval
        {
            get { return m_verticalInterval; }
            set { m_verticalInterval = value; }
        }

        #endregion

        #region ShowScrollBar

        private bool m_isShowScrollBar = true;
        public bool ShowScrollBar
        {
            get { return m_isShowScrollBar; }
            set { m_isShowScrollBar = value; }
        }

        #endregion

        private Size m_buttonSize;
        Dictionary<int, List<MenuButton>> m_buttons;
        List<MenuButton> m_buttonList;
        Dictionary<int, List<Point>> m_visiblePoints;

        private int m_actualHorInterval;
        private int m_rowsOnScreen;
        private int m_actualVertInterval;

        private bool m_isConstructed = false;
        private int m_currentRow;
        private int m_availableWidth; //control width w/o scroll and scroll indent


        #endregion

        #region MenuManager

        public MenuManager()
        {
            InitializeComponent();

        }

        #endregion

        #region OnResize

        Size m_previousSize = new Size(0, 0);
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (m_isConstructed && m_previousSize != Size)
            {
                Init(GetCurrentFocus());
            }
            m_previousSize = Size;
        }

        #endregion



        #region OnMenuKeyDown

        private void OnMenuKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                GetNextButton((MenuButton)sender, DirectionEnum.Left).Focus();
            }
            else if (e.KeyCode == Keys.Right)
            {
                GetNextButton((MenuButton)sender, DirectionEnum.Right).Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                MenuButton nextButton = GetNextButton((MenuButton)sender, DirectionEnum.Down);
                Point nextButtonPos = (Point)nextButton.Tag;

                if (nextButtonPos.Y < m_currentRow
                    || nextButtonPos.Y > m_currentRow + m_rowsOnScreen - 1) // To Focus new button scroll needed
                {
                    Show(nextButtonPos.Y - m_rowsOnScreen + 1);
                }

                nextButton.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                MenuButton nextButton = GetNextButton((MenuButton)sender, DirectionEnum.Up);
                Point nextButtonPos = (Point)nextButton.Tag;

                if (nextButtonPos.Y < m_currentRow
                    || nextButtonPos.Y > m_currentRow + m_rowsOnScreen - 1) // To Focus new button scroll needed
                {
                    Show(nextButtonPos.Y);
                }

                nextButton.Focus();
            }
        }

        #endregion

        #region OnScrollValueChanged

        private int m_scrollPreviousValue = 0;
        private void OnScrollValueChanged(object sender, EventArgs e)
        {
            DirectionEnum direction;
            if (m_scrollPreviousValue < m_vScrollBar.Value)
                direction = DirectionEnum.Down;
            else
                direction = DirectionEnum.Up;

            MenuButton focusedButton = GetCurrentFocus();

            Show(m_vScrollBar.Value);


            if (focusedButton != null && IsButtonOnScreen(focusedButton))
                focusedButton.Focus();
            else
            {
                MenuButton nextButton = GetNextButtonOnScreen(focusedButton, direction);
                if (nextButton != null)
                    nextButton.Focus();
            }

            m_scrollPreviousValue = m_vScrollBar.Value;
        }

        #endregion

        #region Init

        public void Init(MenuButton focus)
        {
            m_buttons = new Dictionary<int, List<MenuButton>>();
            m_buttonList = new List<MenuButton>();

            foreach (Control control in Controls)
            {
                if (control is MenuButton)
                {
                    m_buttonList.Add(control as MenuButton);
                    control.Visible = false;
                }
            }

            m_buttonList.Sort(delegate(MenuButton x, MenuButton y)
                            {
                                return x.TabIndex.CompareTo(y.TabIndex);
                            });

            m_buttonSize = m_buttonList[0].Size;
            foreach (MenuButton button in m_buttonList)
            {
                button.Size = m_buttonSize;
                if (!m_isConstructed)
                {
                    button.KeyDown += new KeyEventHandler(OnMenuKeyDown);
                }

            }

            m_availableWidth = Width;
            int buttonsInRow = m_availableWidth / (m_buttonSize.Width + m_horisontalInterval);

            int totalRowsCount = m_buttonList.Count / buttonsInRow;
            if ((decimal)m_buttonList.Count / buttonsInRow > totalRowsCount)
                totalRowsCount++;

            m_rowsOnScreen = Height / (m_buttonSize.Height + m_verticalInterval);


            //Scroll Bar
            if (m_isShowScrollBar && m_rowsOnScreen < totalRowsCount)
            {
                if (!Controls.Contains(m_vScrollBar))
                {
                    m_vScrollBar = new VScrollBar();
                    m_vScrollBar.ValueChanged += new EventHandler(OnScrollValueChanged);
                    Controls.Add(m_vScrollBar);
                }

                m_vScrollBar.Location = new Point(Width - m_vScrollBar.Width, 0);
                m_vScrollBar.Size = new Size(SCROLL_WIDTH, Height);
                m_availableWidth = Width - SCROLL_WIDTH - SCROLL_INDENT;

                //recalculate layout
                buttonsInRow = m_availableWidth / (m_buttonSize.Width + m_horisontalInterval);

                totalRowsCount = m_buttonList.Count / buttonsInRow;
                if ((decimal)m_buttonList.Count / buttonsInRow > totalRowsCount)
                    totalRowsCount++;

                m_rowsOnScreen = Height / (m_buttonSize.Height + m_verticalInterval);

                m_vScrollBar.Minimum = 0;
                m_vScrollBar.Maximum = totalRowsCount - m_rowsOnScreen;
                m_vScrollBar.SmallChange = 1;
                m_vScrollBar.LargeChange = 1;
            }
            else
            {
                if (Controls.Contains(m_vScrollBar))
                {
                    m_vScrollBar.ValueChanged -= new EventHandler(OnScrollValueChanged);
                    Controls.Remove(m_vScrollBar);
                    m_vScrollBar.Dispose();
                    m_vScrollBar = null;
                }
            }

            m_actualHorInterval = (m_availableWidth - (m_buttonSize.Width * buttonsInRow)) / Math.Max(buttonsInRow - 1, 1);
            m_actualVertInterval
                = (Height - (m_buttonSize.Height * m_rowsOnScreen)) / Math.Max(m_rowsOnScreen - 1, 1);



            int currentRow = 0;
            m_buttons[currentRow] = new List<MenuButton>();

            foreach (MenuButton button in m_buttonList)
            {
                button.Tag = new Point(m_buttons[currentRow].Count, currentRow);
                m_buttons[currentRow].Add(button);

                if (m_buttons[currentRow].Count == buttonsInRow)
                {
                    currentRow++;
                    if (currentRow < totalRowsCount)
                        m_buttons[currentRow] = new List<MenuButton>();
                }
            }


            //Init Visible points table
            m_visiblePoints = new Dictionary<int, List<Point>>();

            int currentX = 0;
            int currentY = 0;

            for (int row = 0; row < m_buttons.Keys.Count; row++)
            {
                m_visiblePoints[row] = new List<Point>();

                if (row == 0) // First row
                {
                    currentY = 0;
                }
                else if (row == m_buttons.Keys.Count - 1) // Last Row
                {
                    currentY = Height - m_buttonSize.Height;
                }
                else
                {
                    currentY += m_buttonSize.Height + m_actualVertInterval;
                }


                for (int column = 0; column < buttonsInRow; column++)
                {
                    if (column == 0) // First column
                    {
                        currentX = 0;
                    }
                    else if (column == buttonsInRow - 1) // Last column
                    {
                        currentX = m_availableWidth - m_buttonSize.Width;
                    }
                    else
                    {
                        currentX += m_buttonSize.Width + m_actualHorInterval;
                    }

                    m_visiblePoints[row].Add(new Point(currentX, currentY));
                }
            }

            if (focus == null)
                Show(0);
            else
                SetFocus(focus);


            m_isConstructed = true;

        }

        #endregion

        #region Show

        public void Show(int rowNumber)
        {
            m_currentRow = rowNumber;

            if (rowNumber < 0)
                m_currentRow = 0;

            if (rowNumber > m_buttons.Keys.Count - m_rowsOnScreen)
                m_currentRow = m_buttons.Keys.Count - m_rowsOnScreen;


            foreach (List<MenuButton> buttonList in m_buttons.Values)
            {
                foreach (MenuButton button in buttonList)
                {
                    button.Visible = false;
                }
            }

            int currentVisibleRow = 0;

            for (int row = m_currentRow; row <= m_currentRow + m_rowsOnScreen - 1; row++)
            {
                for (int column = 0; column < m_buttons[row].Count; column++)
                {
                    m_buttons[row][column].Location = m_visiblePoints[currentVisibleRow][column];
                    m_buttons[row][column].Visible = true;
                }

                currentVisibleRow++;
            }

            if (m_vScrollBar != null)
            {
                m_scrollPreviousValue = m_currentRow;
                m_vScrollBar.Value = m_currentRow;
            }

        }

        #endregion

        #region ScrollForward

        public void ScrollForward()
        {
            ScrollForward(true);
        }

        private void ScrollForward(bool trackFocus)
        {
            MenuButton focusedButton = null;

            if (trackFocus)
                focusedButton = GetCurrentFocus();

            if (m_currentRow + m_rowsOnScreen >= m_buttons.Keys.Count) // lower row
                Show(0);
            else
                Show(m_currentRow + 1);

            if (trackFocus)
            {
                if (focusedButton != null && IsButtonOnScreen(focusedButton))
                    focusedButton.Focus();
                else
                {
                    MenuButton nextButton = GetNextButtonOnScreen(focusedButton, DirectionEnum.Down);
                    if (nextButton != null)
                        nextButton.Focus();
                }
            }
        }

        #endregion

        #region ScrollBackward

        public void ScrollBackward()
        {
            ScrollBackward(true);
        }

        private void ScrollBackward(bool trackFocus)
        {
            MenuButton focusedButton = null;

            if (trackFocus)
                focusedButton = GetCurrentFocus();

            if (m_currentRow == 0)
                Show(m_buttons.Keys.Count - 1);
            else
                Show(m_currentRow - 1);

            if (trackFocus)
            {
                if (focusedButton != null && IsButtonOnScreen(focusedButton))
                    focusedButton.Focus();
                else
                {
                    MenuButton nextButton = GetNextButtonOnScreen(focusedButton, DirectionEnum.Up);
                    if (nextButton != null)
                        nextButton.Focus();
                }
            }
        }

        #endregion

        #region GetCurrentFocus

        private MenuButton GetCurrentFocus()
        {
            for (int row = m_currentRow; row <= m_currentRow + m_rowsOnScreen - 1; row++)
            {
                for (int column = 0; column < m_buttons[row].Count; column++)
                {
                    if (m_buttons[row][column].Focused)
                        return m_buttons[row][column];
                }

            }

            return null;
        }

        #endregion

        #region SetFocus

        public void SetFocus(MenuButton button)
        {
            Point position = (Point)button.Tag;
            Show(position.Y);
            button.Focus();
        }

        #endregion

        #region GetNextButton

        private MenuButton GetNextButton(MenuButton button, DirectionEnum direction)
        {
            Point pos = (Point)button.Tag;

            if (direction == DirectionEnum.Left)
            {
                if (pos.X != 0)
                    for (int i = pos.X - 1; i >= 0; i--)
                    {
                        if (m_buttons[pos.Y][i].Enabled)
                            return m_buttons[pos.Y][i];
                    }


                for (int i = m_buttons[pos.Y].Count - 1; i >= pos.X; i--)
                {
                    if (m_buttons[pos.Y][i].Enabled)
                        return m_buttons[pos.Y][i];
                }


            }
            else if (direction == DirectionEnum.Right)
            {

                if (pos.X != m_buttons[pos.Y].Count - 1)
                    for (int i = pos.X + 1; i <= m_buttons[pos.Y].Count - 1; i++)
                    {
                        if (m_buttons[pos.Y][i].Enabled)
                            return m_buttons[pos.Y][i];
                    }

                for (int i = 0; i <= pos.X; i++)
                {
                    if (m_buttons[pos.Y][i].Enabled)
                        return m_buttons[pos.Y][i];
                }

            }
            else if (direction == DirectionEnum.Up)
            {
                //Find nearest buttons in another rows                
                MenuButton nearestButton;

                //Try to find enabled buttons on next rows in direction left-right
                for (int i = pos.Y - 1; i >= 0; i--)
                {
                    nearestButton = GetNearestButtonInRow(i, pos.X, DirectionEnum.Right);
                    if (nearestButton != null)
                        return nearestButton;
                }

                for (int i = m_buttons.Keys.Count - 1; i >= pos.Y + 1; i--)
                {
                    nearestButton = GetNearestButtonInRow(i, pos.X, DirectionEnum.Right);
                    if (nearestButton != null)
                        return nearestButton;
                }

                return m_buttons[pos.Y][pos.X];

            }
            else if (direction == DirectionEnum.Down)
            {
                //Find nearest buttons in another rows                
                MenuButton nearestButton;

                //Try to find enabled buttons on next rows in direction left-right
                for (int i = pos.Y + 1; i <= m_buttons.Keys.Count - 1; i++)
                {
                    nearestButton = GetNearestButtonInRow(i, pos.X, DirectionEnum.Right);
                    if (nearestButton != null)
                        return nearestButton;
                }

                for (int i = 0; i <= pos.Y - 1; i++)
                {
                    nearestButton = GetNearestButtonInRow(i, pos.X, DirectionEnum.Right);
                    if (nearestButton != null)
                        return nearestButton;
                }

                return m_buttons[pos.Y][pos.X];

            }

            return null;
        }

        #endregion

        #region GetLastRowButton

        /// <summary>
        /// Gets buttons on last row by specified X position if this button enabled
        /// If this X position not exist in this row, returns rightest buttons if it enabled also
        /// Otherwise returns false
        /// </summary>
        /// <returns></returns>
//        private MenuButton GetLastRowButton(int xPos)
//        {
//            if (m_buttons[m_buttons.Keys.Count - 1].Count - 1 >= xPos) //button on this X position exist
//            {
//                if (m_buttons[m_buttons.Keys.Count - 1][xPos].Enabled)
//                    return m_buttons[m_buttons.Keys.Count - 1][xPos];
//            }
//            else //No buttons on this X position in last row
//            {
//                //return last control
//                if (m_buttons[m_buttons.Keys.Count - 1][m_buttons[m_buttons.Keys.Count - 1].Count - 1].Enabled)
//                    return m_buttons[m_buttons.Keys.Count - 1][m_buttons[m_buttons.Keys.Count - 1].Count - 1];
//            }
//
//            return null;
//        }

        #endregion

        #region GetNextButtonOnScreen
        /// <summary>
        /// Renurns enabled only buttons.
        /// First tries to find next nearest buttons at specified X position in order from bottom to top (acc to direction).
        /// If no such button then tries to find first button in order from right to left and from 
        /// bottom to top (acc to direction).
        /// If failure returns null
        /// </summary>
        /// <param name="button">Can be null then don't tries to search by X position</param>
        /// <param name="direction">Up and Down available only</param>
        /// <returns></returns>
        ///         
        private MenuButton GetNextButtonOnScreen(MenuButton button, DirectionEnum direction)
        {
            MenuButton nextButton;

            if (button == null)
                nextButton = null;
            else
                nextButton = GetNextButton(button, direction);


            if (nextButton != null)
            {
                Point pos = (Point)nextButton.Tag;

                if (pos.Y >= m_currentRow && pos.Y <= m_currentRow + m_rowsOnScreen - 1)
                    return nextButton;
            }

            if (direction == DirectionEnum.Down)//Try to find from Left to right and from Up to Down
            {
                for (int row = m_currentRow; row <= m_currentRow + m_rowsOnScreen - 1; row++)
                {
                    for (int column = 0; column < m_buttons[row].Count; column++)
                    {
                        if (m_buttons[row][column].Enabled)
                            return m_buttons[row][column];
                    }
                }

            }

            if (direction == DirectionEnum.Up)//Try to find from right to left and from Down to Up
            {
                for (int row = m_currentRow + m_rowsOnScreen - 1; row >= m_currentRow; row--)
                {
                    for (int column = m_buttons[row].Count - 1; column >= 0; column--)
                    {
                        if (m_buttons[row][column].Enabled)
                            return m_buttons[row][column];
                    }
                }
            }

            return null;
        }

        #endregion

        #region IsButtonOnScreen

        private bool IsButtonOnScreen(MenuButton button)
        {
            Point pos = (Point)button.Tag;
            if (pos.Y >= m_currentRow && pos.Y <= m_currentRow + m_rowsOnScreen - 1)
                return true;
            return false;
        }

        #endregion

        #region GetNearestButtonInRow
        /// <summary>
        /// Tries to find in specified row button, nearest to xPos position in this row
        /// Returns enabled only button
        /// </summary>
        /// <param name="row">Row in which find processed</param>
        /// <param name="xPos">position to which neares should be finded</param>
        /// <param name="directionPriority">If there is 2 buttons with same distance specifies left or right will be returned</param>
        /// <returns></returns>
        private MenuButton GetNearestButtonInRow(int row, int xPos, DirectionEnum directionPriority)
        {
            MenuButton leftButton = null;
            MenuButton rightButton = null;
            int leftDistance = m_buttons[row].Count + 1;
            int rightDistance = leftDistance;

            //Check Left side
            for (int i = Math.Min(xPos, m_buttons[row].Count - 1); i >= 0; i--)
            {
                if (m_buttons[row][i].Enabled)
                {
                    leftButton = m_buttons[row][i];
                    leftDistance = Math.Abs(xPos - i);
                    break;
                }
            }

            //Check Right side
            for (int i = Math.Min(xPos, m_buttons[row].Count - 1); i <= m_buttons[row].Count - 1; i++)
            {
                if (m_buttons[row][i].Enabled)
                {
                    rightButton = m_buttons[row][i];
                    rightDistance = Math.Abs(xPos - i);
                    break;
                }
            }

            //Checking Distance
            if (leftDistance < rightDistance)
                rightButton = null;
            else if (leftDistance > rightDistance)
                leftButton = null;


            if (leftButton != null && directionPriority == DirectionEnum.Left)
                return leftButton;

            if (rightButton != null && directionPriority == DirectionEnum.Right)
                return rightButton;

            if (leftButton != null)
                return leftButton;

            if (rightButton != null)
                return rightButton;

            return null;
        }

        #endregion
    }
    
}
