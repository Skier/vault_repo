using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI
{
    public class Joystick
    {

#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
        public static extern Boolean SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);     

        public static bool IsDropedDown(ComboBox combobox)
        {
            return SendMessage(combobox.Handle, 0x157, 0, 0);
        }

        public enum JoystickKeyEnum
        { 
            Left,
            Right,
            Up,
            Down,
            Enter
        }
        
        private List<Control> m_findPassedElements = new List<Control>();

        public Control Find(Control startSearchFrom, JoystickKeyEnum key)
        {
            m_findPassedElements.Clear();
            return FindRecursive(startSearchFrom, key);
        }

        private Control FindRecursive(Control startSearchFrom, JoystickKeyEnum key)
        {
            if (m_findPassedElements.Contains(startSearchFrom))
                return null;
            else
                m_findPassedElements.Add(startSearchFrom);
            
            if (startSearchFrom == null)
                return null;

            if (!m_elements.ContainsKey(startSearchFrom)
                || m_elements[startSearchFrom][key] == null)
                return null;

            if (!m_elements[startSearchFrom][key].Enabled ||
                   !m_elements[startSearchFrom][key].Visible)
                return FindRecursive(m_elements[startSearchFrom][key], key);

            return m_elements[startSearchFrom][key];
        }


        Dictionary<Control, JoystickElement> m_elements = new Dictionary<Control, JoystickElement>();
        private AutoDropDown m_autoDropDown = new AutoDropDown();

        public void Add(Control targetControl, 
            Control leftControl,
            Control rightControl,
            Control topControl,
            Control bottomControl)
        {
            if (targetControl is ComboBox)
                m_autoDropDown.Add((ComboBox)targetControl);
            else if (targetControl is DateTimePicker)
                m_autoDropDown.Add((DateTimePicker)targetControl);                                                    
            
            if (!m_elements.ContainsKey(targetControl))
            {
                m_elements.Add(
                    targetControl,
                    new JoystickElement(
                    this,
                    targetControl,
                    leftControl,
                    rightControl,
                    topControl,
                    bottomControl));
            }
            else
            {
                m_elements[targetControl][JoystickKeyEnum.Left] = leftControl;
                m_elements[targetControl][JoystickKeyEnum.Right] = rightControl;
                m_elements[targetControl][JoystickKeyEnum.Up] = topControl;
                m_elements[targetControl][JoystickKeyEnum.Down] = bottomControl;
            }

            if (!m_elements.ContainsKey(leftControl))
            {
                m_elements.Add(
                    leftControl,
                    new JoystickElement(this,
                    leftControl,
                    null,
                    targetControl,
                    null,
                    null));
            }
            //else
            //    m_elements[leftControl][JoystickKeyEnum.Right] = targetControl;

            if (!m_elements.ContainsKey(rightControl))
            {
                m_elements.Add(
                    rightControl,
                    new JoystickElement(this,
                    rightControl,
                    targetControl,
                    null,
                    null,
                    null));
            }
            //else
            //    m_elements[rightControl][JoystickKeyEnum.Left] = targetControl;


            if (!m_elements.ContainsKey(topControl))
            {
                m_elements.Add(
                    topControl,
                    new JoystickElement(this,
                    topControl,
                    null,
                    null,
                    null,
                    targetControl));
            }
            //else
            //   m_elements[topControl][JoystickKeyEnum.Down] = targetControl;

            if (!m_elements.ContainsKey(bottomControl))
            {
                m_elements.Add(
                    bottomControl,
                    new JoystickElement(this,
                    bottomControl,
                    null,
                    null,
                    targetControl,
                    null));
            }
            //else
            //    m_elements[bottomControl][JoystickKeyEnum.Up] = targetControl;

        }

        public bool Navigate(Control control, JoystickKeyEnum joystickKey)
        {

            if (control is TextBox)
            {
                TextBox textBox = (TextBox)control;

                if ((joystickKey == JoystickKeyEnum.Left
                    && textBox.SelectionStart > 0)
                    ||
                    (joystickKey == JoystickKeyEnum.Right)
                    && textBox.SelectionStart < textBox.Text.Length)
                    return false;
            }
            else if (control is ComboBox)
            {
                ComboBox comboBox = (ComboBox)control;

                if (IsDropedDown(comboBox))
                    return false;
            }
            else if (control is DateTimePicker)
            {
                DateTimePicker dateTimePicker = (DateTimePicker)control;

                if (dateTimePicker.Format == DateTimePickerFormat.Time)
                { 
                    
                }
            }
            else if (control is Table)
            {
                Table table = (Table) control;
                
                if (table.Model.GetRowCount() > 0 
                    && ((joystickKey == JoystickKeyEnum.Left && table.CurrentColumnIndex > 0)
                    || (joystickKey == JoystickKeyEnum.Right && table.CurrentColumnIndex < table.Model.GetColumnCount() - 1)
                    || (joystickKey == JoystickKeyEnum.Up && table.CurrentRowIndex > 0)
                    || (joystickKey == JoystickKeyEnum.Down && table.CurrentRowIndex < table.Model.GetRowCount() - 1)))
                {
                    return false;
                }
            }
            else if (control is NumericUpDown)
            {
                if (joystickKey == JoystickKeyEnum.Up || joystickKey == JoystickKeyEnum.Down)                     
                    return false;
            }

            Control enabledControl = Find(control, joystickKey);

            if (enabledControl != null)
                return Focus(enabledControl);

            return false;
        }

        bool Focus(Control control)
        {
            if (control is TextBox)
            {
                TextBox textBox = (TextBox)control;
                textBox.Focus();
                textBox.SelectAll();

                return true;
            }
            else if (control is Table)
            {
                Table table = (Table) control;
                table.Focus();
                if (table.CurrentRowIndex < 0)
                    table.Select(0);
                
                return true;
            }

            return control.Focus();
        }

        TabControl m_tabs;
        Dictionary<int, Control[]> m_tabControls = new Dictionary<int, Control[]>();

        public void Add(TabControl targetControl, int tabIndex, Control upBehaviorControl, Control downBehaviorControl)
        {

            if (m_elements.ContainsKey(targetControl))
                m_elements.Remove(targetControl);

            if (m_tabs == null)
            {
                m_tabs = targetControl;
                targetControl.KeyDown += new KeyEventHandler(OnTabKeyDown);
            }

            if (!m_tabControls.ContainsKey(tabIndex))
                m_tabControls.Add(tabIndex, new Control[] { upBehaviorControl, downBehaviorControl });
            else
            {
                m_tabControls[tabIndex][0] = upBehaviorControl;
                m_tabControls[tabIndex][1] = downBehaviorControl;
            }
        }

        void OnTabKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (m_tabControls.ContainsKey(m_tabs.SelectedIndex))
                {
                    Control control = m_tabControls[m_tabs.SelectedIndex][0];

                    if (!control.Enabled || !control.Visible)
                        control = Find(control,JoystickKeyEnum.Up);

                    if (control != null)
                        e.Handled = Focus(control);
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (m_tabControls.ContainsKey(m_tabs.SelectedIndex))
                {
                    Control control = m_tabControls[m_tabs.SelectedIndex][1];

                    if (!control.Enabled || !control.Visible)
                        control = Find(control, JoystickKeyEnum.Down);

                    if (control != null)
                        e.Handled = Focus(control);
                }
            }
        }
    }

    public class JoystickElement
    {
        Dictionary<Joystick.JoystickKeyEnum, Control> m_controls 
            = new Dictionary<Joystick.JoystickKeyEnum, Control>();

        public Control this[Joystick.JoystickKeyEnum key]
        {
            get
            {
                if (m_controls.ContainsKey(key))
                    return m_controls[key];
                else
                    return null;
            }
            set
            {
                if (m_controls.ContainsKey(key))
                    m_controls[key] = value;
                else
                    m_controls.Add(key, value);
            }
        }

        Control m_control;

        public Control Control
        {
            get { return m_control; }
            set { m_control = value; }
        }


        Joystick m_joystick;

        public JoystickElement(Joystick joystik,
                                Control control, 
                                   Control left,
                                   Control right, 
                                      Control top, 
                                      Control bottom)
        {
            m_joystick = joystik;

            m_control = control;

            m_controls.Add(Joystick.JoystickKeyEnum.Left,left);
            m_controls.Add(Joystick.JoystickKeyEnum.Right,right);
            m_controls.Add(Joystick.JoystickKeyEnum.Up, top);
            m_controls.Add(Joystick.JoystickKeyEnum.Down,bottom);

            if (!(m_control is TabControl))
                m_control.KeyDown += new KeyEventHandler(OnKeyDown);
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            Joystick.JoystickKeyEnum joystickKey = Joystick.JoystickKeyEnum.Enter;

            if (e.KeyCode == Keys.Left)
                joystickKey = Joystick.JoystickKeyEnum.Left;
            else if (e.KeyCode == Keys.Right)
                joystickKey = Joystick.JoystickKeyEnum.Right;
            else if (e.KeyCode == Keys.Up)
                joystickKey = Joystick.JoystickKeyEnum.Up;
            else if (e.KeyCode == Keys.Down)
                joystickKey = Joystick.JoystickKeyEnum.Down;
            else
                return;


            e.Handled = m_joystick.Navigate(Control, joystickKey);
        }
    }
    
}
