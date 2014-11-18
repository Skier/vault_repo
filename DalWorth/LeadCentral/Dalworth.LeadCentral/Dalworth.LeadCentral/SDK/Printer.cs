using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Dalworth.LeadCentral.SDK
{
    public class Printer : IDisposable
    {
        private const int PAGE_LENGHT_IN_LINES = 26;
        public const int PAGE_COLUMNS_COUNT = 53;

        public static string StartBoldEscapeString
        {
            get { return Regex.Unescape(@"\x1B\x45"); }
        }

        public static string EndBoldEscapeString
        {
            get { return Regex.Unescape(@"\x1B\x46"); }
        }

        #region ���������

        /// <summary>
        /// ������, ������������� �������� ������
        /// </summary>
        public const string CRLF = "\r\n";

        /// <summary>
        /// ������, ������������� �������� �������
        /// </summary>
        public const string Break = "\f";

        #endregion ���������

        #region �����������

        /// <summary>
        /// ������� �������
        /// </summary>
        /// <param name="printerName">��� �������� 
        /// (������ ��������� � ������ ���������������� �������� � ������ ��������� 
        /// "������ ����������" - "�������� � �����")
        /// </param>
        public Printer(string printerName)
        {
            this.printerName = printerName;
            di.pDataType = "RAW";
        }

        #endregion �����������

        #region ����������

        ~Printer()
        {
            Close();
        }

        #endregion ����������

        #region ������

        /// <summary>
        /// ������� ������������� ������� �� ������
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// � ������ ������ ������������ ���������� Win32Exception � ����� ������ ErrorCode, 
        /// ������������ �������� WinAPI GetLastError()
        /// </exception>
        public void Open()
        {
            Open("");
        }

        /// <summary>
        /// ������� ����������� ������� �� ������
        /// </summary>
        /// <param name="docName">��� ��������� � ������� ������</param>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// � ������ ������ ������������ ���������� Win32Exception � ����� ������ ErrorCode, 
        /// ������������ �������� WinAPI GetLastError()
        /// </exception>
        public void Open(string docName)
        {
            if (!Active)
            {
                if (!OpenPrinter(printerName, ref printerHandle, 0))
                {
                    printerHandle = IntPtr.Zero;
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                di.pDocName = docName == null ? "" : docName;
                if (StartDocPrinter(printerHandle, 1, ref di) == 0)
                {
                    ClosePrinter(printerHandle);
                    printerHandle = IntPtr.Zero;
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                if (!StartPagePrinter(printerHandle))
                {
                    EndDocPrinter(printerHandle);
                    ClosePrinter(printerHandle);
                    printerHandle = IntPtr.Zero;
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// ������� ������� �� ������
        /// </summary>
        public void Close()
        {
            if (Active)
            {
                EndPagePrinter(printerHandle);
                EndDocPrinter(printerHandle);
                ClosePrinter(printerHandle);
                printerHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// � ������ ������ ������������ ���������� Win32Exception � ����� ������ ErrorCode, 
        /// ������������ �������� WinAPI GetLastError()
        /// </exception>
        public void Write(string s)
        {
            if (Active)
            {
                int pcWritten = 0;
                byte[] data = encoding866.GetBytes(s);
                if (!WritePrinter(printerHandle, data, data.Length, ref pcWritten))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// �������� ������ � ��������� �������
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// � ������ ������ ������������ ���������� Win32Exception � ����� ������ ErrorCode, 
        /// ������������ �������� WinAPI GetLastError()
        /// </exception>
        public void WriteLine(string s)
        {
            if (Active)
            {
                Write(s);
                WriteLine();
            }
        }

        /// <summary>
        /// �������� ������ � ������� esc-�������� �� ��������������� ����
        /// �������������� ��������� esc-�������������������, ��������� � ms-help://MS.NETFrameworkSDKv1.1/cpgenref/html/cpconcharacterescapes.htm
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// � ������ ������ ������������ ���������� Win32Exception � ����� ������ ErrorCode, 
        /// ������������ �������� WinAPI GetLastError()
        /// </exception>
        public void WriteUnescaped(string s)
        {
            if (Active)
                Write(Regex.Unescape(s));
        }

        /// <summary>
        /// ������� �������
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// � ������ ������ ������������ ���������� Win32Exception � ����� ������ ErrorCode, 
        /// ������������ �������� WinAPI GetLastError()
        /// </exception>
        public void WriteLine()
        {
            if (Active)
            {
                int pcWritten = 0;
                if (!WritePrinter(printerHandle, bCRLF, 2, ref pcWritten))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// � ������ ������ ������������ ���������� Win32Exception � ����� ������ ErrorCode, 
        /// ������������ �������� WinAPI GetLastError()
        /// </exception>
        public void PageBreak()
        {
            if (Active)
            {
                int pcWritten = 0;
                if (!WritePrinter(printerHandle, bBreak, 1, ref pcWritten))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        #endregion ������

        #region ��������

        /// <summary>
        /// ������� ������� ��� ���
        /// ��������� �������� ������������ ������� Open() � Close() 
        /// </summary>
        public bool Active
        {
            get
            {
                return printerHandle != IntPtr.Zero;
            }
            set
            {
                if (value)
                    Open();
                else
                    Close();
            }
        }

        /// <summary>
        /// ��� ��������
        /// </summary>
        public string Name
        {
            get
            {
                return printerName;
            }
        }

        #endregion ��������

        #region ���������� ����

        /// <summary>
        /// ������� ������
        /// </summary>
        static byte[] bCRLF = new byte[] { 0x0d, 0x0a };

        /// <summary>
        /// ������� ��������
        /// </summary>
        static byte[] bBreak = new byte[] { 0x0c };

        /// <summary>
        /// ��� ��������
        /// </summary>
        string printerName;

        /// <summary>
        /// Handle ��������
        /// </summary>
        IntPtr printerHandle = IntPtr.Zero;

        /// <summary>
        /// ���������, ����������� �������� ��� ������
        /// </summary>
        DOC_INFO_1 di = new DOC_INFO_1();

        /// <summary>
        /// ������������� � DOS-866
        /// </summary>
        Encoding encoding866 = Encoding.GetEncoding(866);

        #endregion ���������� ����

        #region ��������������� ������� API

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern int StartDocPrinter(IntPtr hPrinter, int Level, ref DOC_INFO_1 pDocInfo);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool WritePrinter(IntPtr hPrinter, byte[] data, int buf, ref int pcWritten);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool ClosePrinter(IntPtr hPrinter);

        #endregion ��������������� ������� API

        #region ��������������� ���������

        /// <summary>
        /// ��������� DOC_INFO_1 �� ������� WinAPI 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct DOC_INFO_1
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDataType;
        }

        #endregion ��������������� ���������

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Close();
        }

        #endregion


        public void Initialize()
        {
            WriteUnescaped(@"\x1B\x7B\x41"); //Setting emulation                                
            WriteUnescaped(@"\x1B\x40"); //reset to initial state
            WriteUnescaped(@"\x12"); //cancel compressed pitch            
            WriteUnescaped(@"\x1B\x50"); //set font 10 cpi      
            SetPageLength();
            AlignLeft();
        }

        public void StartDoubleWidth()
        {
            WriteUnescaped(@"\x1B\x57\x31");
        }

        public void EndDoubleWidth()
        {
            WriteUnescaped(@"\x1B\x57\x30");
        }

        public void StartBold()
        {
            WriteUnescaped(@"\x1B\x45");
        }

        public void EndBold()
        {
            WriteUnescaped(@"\x1B\x46");
        }

        public void StartBoldDoubleWidth()
        {
            StartBold();
            StartDoubleWidth();
        }

        public void EndBoldDoubleWidth()
        {
            EndDoubleWidth();
            EndBold();
        }

        public void AlignLeft()
        {
            WriteUnescaped(@"\x1B\x61\x00");
        }

        public void AlignCenter()
        {
            WriteUnescaped(@"\x1B\x61\x01");
        }

        public void AlignRight()
        {
            WriteUnescaped(@"\x1B\x61\x02");
        }

        public void SetPageLength()
        {
            WriteUnescaped(@"\x1B\x43\x" + PAGE_LENGHT_IN_LINES.ToString("X"));            
        }

        public void GoToNextPage()
        {
            WriteUnescaped(@"\x0C");
        }

        public void MovePrintHeadTo(int columnsCountFromRight)
        {
            Write(Regex.Unescape(@"\x1B\x10\x40\x06\x00\x00") 
                + ((PAGE_COLUMNS_COUNT - columnsCountFromRight) * 24).ToString("0000"));
        }

    }
}
