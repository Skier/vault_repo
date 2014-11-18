using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Dalworth.Common.SDK
{
    public class Printer : IDisposable
    {
        private const int PageLenghtInLines = 26;
        public const int PageColumnsCount = 53;

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
        public const string Crlf = "\r\n";

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
            PrinterName = printerName;
            Di.pDataType = "RAW";
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
                if (!OpenPrinter(PrinterName, ref PrinterHandle, 0))
                {
                    PrinterHandle = IntPtr.Zero;
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                Di.pDocName = docName ?? "";
                if (StartDocPrinter(PrinterHandle, 1, ref Di) == 0)
                {
                    ClosePrinter(PrinterHandle);
                    PrinterHandle = IntPtr.Zero;
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                if (!StartPagePrinter(PrinterHandle))
                {
                    EndDocPrinter(PrinterHandle);
                    ClosePrinter(PrinterHandle);
                    PrinterHandle = IntPtr.Zero;
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
                EndPagePrinter(PrinterHandle);
                EndDocPrinter(PrinterHandle);
                ClosePrinter(PrinterHandle);
                PrinterHandle = IntPtr.Zero;
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
                byte[] data = Encoding866.GetBytes(s);
                if (!WritePrinter(PrinterHandle, data, data.Length, ref pcWritten))
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
                if (!WritePrinter(PrinterHandle, BCrlf, 2, ref pcWritten))
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
                if (!WritePrinter(PrinterHandle, BBreak, 1, ref pcWritten))
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
                return PrinterHandle != IntPtr.Zero;
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
                return PrinterName;
            }
        }

        #endregion ��������

        #region ���������� ����

        /// <summary>
        /// ������� ������
        /// </summary>
        static readonly byte[] BCrlf = new byte[] { 0x0d, 0x0a };

        /// <summary>
        /// ������� ��������
        /// </summary>
        static readonly byte[] BBreak = new byte[] { 0x0c };

        /// <summary>
        /// ��� ��������
        /// </summary>
        readonly string PrinterName;

        /// <summary>
        /// Handle ��������
        /// </summary>
        IntPtr PrinterHandle = IntPtr.Zero;

        /// <summary>
        /// ���������, ����������� �������� ��� ������
        /// </summary>
        DocInfo1 Di;

        /// <summary>
        /// ������������� � DOS-866
        /// </summary>
        readonly Encoding Encoding866 = Encoding.GetEncoding(866);

        #endregion ���������� ����

        #region ��������������� ������� API

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern int StartDocPrinter(IntPtr hPrinter, int level, ref DocInfo1 pDocInfo);

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
        struct DocInfo1
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPTStr)] 
            private readonly string pOutputFile;
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
            WriteUnescaped(@"\x1B\x43\x" + PageLenghtInLines.ToString("X"));            
        }

        public void GoToNextPage()
        {
            WriteUnescaped(@"\x0C");
        }

        public void MovePrintHeadTo(int columnsCountFromRight)
        {
            Write(Regex.Unescape(@"\x1B\x10\x40\x06\x00\x00") 
                + ((PageColumnsCount - columnsCountFromRight) * 24).ToString("0000"));
        }

    }
}
