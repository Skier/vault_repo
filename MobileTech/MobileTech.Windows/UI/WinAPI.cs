using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MobileTech.Windows.UI
{

	public static class WinAPI
	{

		public const UInt32 SHDB_SHOW = 0x0001;
		public const UInt32 SHDB_HIDE = 0x0002;
		public const int GWL_STYLE = -16;
		public const UInt32 WS_NONAVDONEBUTTON = 0x00010000;
		public const UInt32 WM_LBUTTONUP = 0x0202;
		public const int GWL_WNDPROC = -4;
		public const int SPI_GETWORKAREA = 0x0030;
		public const int WM_MOUSEMOVE = 0x0200;
        public const int SPI_GETSIPINFO = 225;
        public const int SPI_SETSIPINFO = 224;
        public const UInt32 SIPF_ON = 0x00000001;
        public const UInt32 SIPF_DISABLECOMPLETION = 0x08;

		public delegate int WndProc(IntPtr hwnd, uint msg, uint wParam, int lParam);

		public static int HIWORD(int Number)
		{
			return ((Number >> 16) & 0xffff);
		}

		public static int LOWORD(int Number)
		{
			return (Number & 0xffff);
		}

		public static int MAKELONG(int LoWord, int HiWord)
		{
			return (HiWord << 16) | (LoWord & 0xffff);
		}

		public static IntPtr MAKELPARAM(int LoWord, int HiWord)
		{
			return new IntPtr(MAKELONG(LoWord,HiWord));
		}


#if WINCE
		[DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
        public static extern bool PostMessage(
			IntPtr hWnd,
			UInt32 Msg,
			UInt32 wParam,
			UInt32 lParam);
#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern bool ShowWindow(
				IntPtr hWnd,
				UInt32 nCmdShow);

#if WINCE



        struct SIPINFO
        {
            public UInt32 cbSize;
            public UInt32 fdwFlags;
            public RECT rcVisibleDesktop;
            public RECT rcSipRect;
            public UInt32 dwImDataSize;
            public UIntPtr pvImData;
        }

        public static void ShowInputPanel(bool bShow)
        {
            SIPINFO si = new SIPINFO();
            si.cbSize = 48;// (UInt32)sizeof(SIPINFO);
         
            if (SHSipInfo(SPI_GETSIPINFO, 0, ref si, 0))
            {
                if (bShow)
                    si.fdwFlags |= SIPF_ON;
                else
                    si.fdwFlags &= ~SIPF_ON;

                SHSipInfo(SPI_SETSIPINFO, 0, ref si, 0);
            }
        }

        public static void SIPAllowSuggestions(bool bAllow)
        {
            SIPINFO si = new SIPINFO();
            si.cbSize = 48;// (UInt32)sizeof(SIPINFO);

            if (SHSipInfo(SPI_GETSIPINFO, 0, ref si, 0))
            {
                if (!bAllow)
                    si.fdwFlags |= SIPF_DISABLECOMPLETION;
                else
                    si.fdwFlags &= ~SIPF_DISABLECOMPLETION;

                SHSipInfo(SPI_SETSIPINFO, 0, ref si, 0);
            }
        }

		[DllImport("aygshell.dll")]
		private static extern bool SHDoneButton(
			IntPtr hWnd,
			UInt32 dwState);

        [DllImport("aygshell.dll")]
        private static extern bool SHSipInfo(
        uint uiAction, 
        uint uiParam,
        ref SIPINFO sipInfo, 
        uint fWinIni 
        );
#endif

#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public extern static IntPtr SetWindowLong(
			IntPtr hwnd,
			int nIndex,
			IntPtr dwNewLong);
#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern UInt32 GetWindowLong(
			IntPtr hWnd,
			int nIndex);
#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern IntPtr FindWindow(
			string lpClassName,
			string lpWindowName);
#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern int CallWindowProc(
			IntPtr lpPrevWndFunc,
			IntPtr hwnd,
			uint msg,
			uint wParam,
			int lParam);
#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern int CallWindowProc(
			IntPtr lpPrevWndFunc,
			IntPtr hwnd,
			uint msg,
			uint wParam,
			IntPtr lParam);
#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public extern static int DefWindowProc(
			IntPtr hwnd,
			uint msg,
			uint wParam,
			int lParam);
#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern bool SystemParametersInfo(
		  UInt32 uiAction,
		  UInt32 uiParam,
		  IntPtr pvParam,
		  UInt32 fWinIni
		);

#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern bool SystemParametersInfo(
		  UInt32 uiAction,
		  UInt32 uiParam,
		  ref RECT pvParam,
		  UInt32 fWinIni
		);

#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
		public static extern bool PtInRect(
		ref RECT lprc,
		POINT pt
		);


#if WINCE
        [DllImport("coredll.dll")]
#else
		[DllImport("user32.dll")]
#endif
        public static extern Boolean SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam); 

#if WINCE
        public static void DropDown(ComboBox combobox, bool value)
        {
            SendMessage(combobox.Handle, 0x14f, value ? -1 : 0, 0);

        }

        public static bool IsDropedDown(ComboBox combobox)
        {
            return SendMessage(combobox.Handle,0x157, 0, 0);
        }

#endif
		public static void CloseWindow(IntPtr hWnd)
		{
			PostMessage(hWnd, 0x0010, 0, 0);
		}
		public static void CloseWindow(Form form)
		{
            PostMessage(form.Handle, 0x0010, 0, 0);
		}

		public static void HideWindow(Form form)
		{
			ShowWindow(form.Handle, 0);
		}

#if WINCE
		public static void HideDoneButton(Form form)
		{
			SHDoneButton(form.Handle, SHDB_HIDE);
		}

		public static void HideXButton(Form form)
		{
			UInt32 dwStyle = GetWindowLong(form.Handle, GWL_STYLE);

			if ((dwStyle & WS_NONAVDONEBUTTON) == 0)
				SetWindowLong(form.Handle, GWL_STYLE,
					new IntPtr(dwStyle | WS_NONAVDONEBUTTON));
		}

#endif


	}

#if WINCE
    public class AutoDropDown
    {

        public void Add(ComboBox comboBox)
        {
            comboBox.KeyDown += new KeyEventHandler(OnKeyDown);
            comboBox.LostFocus += new EventHandler(OnLostFocus);
        }

        void OnLostFocus(object sender, EventArgs e)
        {
            WinAPI.DropDown((sender as ComboBox), false);
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return && 
                !WinAPI.IsDropedDown(sender as ComboBox))
            {

                e.Handled = true;

                WinAPI.DropDown(sender as ComboBox, true);
            }
        }
    }
#endif
}
