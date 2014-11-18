using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Dalworth.Controls
{
    public class Signature : Control
    {
        private ArrayList Points = new ArrayList();
        private Bitmap BackGroundImage;
        private Graphics GraphicsHandle;
        private Pen SignaturePen = new Pen(Color.Black);
        private Point LastMouseCoordinates = new Point(0, 0);
        private bool CaptureMouseCoordinates = false;

        const int XPelsPerMeter = 0xb12;
        const int YPelsPerMeter = 0xb12;
        const int GPTR = 0x40;
        const int SRCCOPY = 0x00CC0020;

        [DllImport("coredll.dll")]
        private static extern IntPtr GetFocus();
        [DllImport("coredll.dll")]
        private static extern IntPtr LocalAlloc(uint flags, uint cb);
        [DllImport("coredll.dll")]
        private static extern IntPtr LocalFree(IntPtr hMem);
        [DllImport("coredll.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, BITMAPINFOHEADER hdr, uint colors, ref IntPtr pBits, IntPtr hFile, uint offset);
        [DllImport("coredll.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, IntPtr hdr, uint colors, ref IntPtr pBits, IntPtr hFile, uint offset);
        [DllImport("coredll.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("coredll.dll")]
        private static extern void ReleaseDC(IntPtr hDC);
        [DllImport("coredll.dll")]
        private static extern void DeleteDC(IntPtr hDC);
        [DllImport("coredll.dll")]
        private static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("coredll.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("coredll.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObj);
        [DllImport("coredll.dll")]
        private static extern void DeleteObject(IntPtr hObj);
        struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
        }
        struct BITMAPFILEHEADER
        {
            public ushort bfType;
            public uint bfSize;
            public ushort bfReserved1;
            public ushort bfReserved2;
            public uint bfOffBits;
        }
        private struct LineToDraw
        {
            public int StartX;
            public int StartY;
            public int EndX;
            public int EndY;
        }
        public Signature()
        {
            LoadBackgroundImage();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            LoadBackgroundImageIfInvalid();
            e.Graphics.DrawImage(BackGroundImage, 0, 0);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CaptureMouseCoordinates) { return; }
            CaptureMouseCoordinates = true;
            LastMouseCoordinates.X = e.X;
            LastMouseCoordinates.Y = e.Y;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            CaptureMouseCoordinates = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!CaptureMouseCoordinates) { return; }
            LineToDraw l = new LineToDraw();
            l.StartX = LastMouseCoordinates.X;
            l.StartY = LastMouseCoordinates.Y;
            l.EndX = e.X;
            l.EndY = e.Y;
            Points.Add(l);
            GraphicsHandle.DrawLine(SignaturePen, l.StartX + 1, l.StartY, l.EndX + 1, l.EndY);
            GraphicsHandle.DrawLine(SignaturePen, l.StartX, l.StartY + 1, l.EndX, l.EndY + 1);
            GraphicsHandle.DrawLine(SignaturePen, l.StartX + 1, l.StartY + 1, l.EndX + 1, l.EndY + 1);
            GraphicsHandle.DrawLine(SignaturePen, l.StartX, l.StartY, l.EndX, l.EndY);
            LastMouseCoordinates.X = l.EndX;
            LastMouseCoordinates.Y = l.EndY;
            Invalidate();
        }
        public void Save(string FileName)
        {
            try
            {
                SaveToFile(this, FileName);
            }
            catch (Exception) { throw; }
        }
        public void Clear(bool ClearCapturedLines)
        {
            try
            {
                LoadBackgroundImage();
                Invalidate();
                if (ClearCapturedLines == false) { return; }
                Points.Clear();
            }
            catch (Exception) { throw; }
        }
        private void LoadBackgroundImageIfInvalid()
        {
            try
            {
                if (BackGroundImage == null || BackGroundImage.Width != this.Width || BackGroundImage.Height != this.Height)
                {
                    LoadBackgroundImage();
                }
            }
            catch (Exception) { throw; }
        }
        private void LoadBackgroundImage()
        {
            try
            {
                BackGroundImage = new Bitmap(this.Width, this.Height);
                GraphicsHandle = Graphics.FromImage(BackGroundImage);
                GraphicsHandle.Clear(System.Drawing.Color.White);

            }
            catch (Exception) { throw; }
        }
        private void SaveToFile(Control ctl, string FileName)
        {
            IntPtr hDC;
            if (ctl != null)
            {
                ctl.Focus();
                IntPtr hReal = GetFocus();
                hDC = GetDC(hReal);
            }
            else
            {
                hDC = GetDC(IntPtr.Zero);
            }
            IntPtr hMemDC = CreateCompatibleDC(hDC);
            BITMAPINFOHEADER bi = new BITMAPINFOHEADER();
            bi.biSize = (uint)Marshal.SizeOf(bi);
            bi.biBitCount = 16;
            bi.biClrUsed = 0;
            bi.biClrImportant = 0;
            bi.biCompression = 0;
            bi.biHeight = ctl != null ? ctl.Height : Screen.PrimaryScreen.Bounds.Height;
            bi.biWidth = ctl != null ? ctl.Width : Screen.PrimaryScreen.Bounds.Width;
            bi.biPlanes = 1;
            int cb = (int)(bi.biHeight * bi.biWidth * bi.biBitCount / 8);
            bi.biSizeImage = (uint)cb;
            bi.biXPelsPerMeter = XPelsPerMeter;
            bi.biYPelsPerMeter = YPelsPerMeter;
            IntPtr pBits = IntPtr.Zero;
            IntPtr pBI = LocalAlloc(GPTR, bi.biSize);
            Marshal.StructureToPtr(bi, pBI, false);
            IntPtr hBmp = CreateDIBSection(hDC, pBI, 0, ref pBits, IntPtr.Zero, 0);
            BITMAPINFOHEADER biNew = (BITMAPINFOHEADER)Marshal.PtrToStructure(pBI, typeof(BITMAPINFOHEADER));
            IntPtr hOldBitmap = SelectObject(hMemDC, hBmp);
            int nRet = BitBlt(hMemDC, 0, 0, bi.biWidth, bi.biHeight, hDC, 0, 0, SRCCOPY);
            byte[] RealBits = new byte[cb];
            Marshal.Copy(pBits, RealBits, 0, cb);
            BITMAPFILEHEADER bfh = new BITMAPFILEHEADER();
            bfh.bfSize = (uint)cb + 0x36;
            bfh.bfType = 0x4d42;
            bfh.bfOffBits = 0x36;
            int HdrSize = 14;
            byte[] header = new byte[HdrSize];
            BitConverter.GetBytes(bfh.bfType).CopyTo(header, 0);
            BitConverter.GetBytes(bfh.bfSize).CopyTo(header, 2);
            BitConverter.GetBytes(bfh.bfOffBits).CopyTo(header, 10);
            byte[] data = new byte[cb + bfh.bfOffBits];
            header.CopyTo(data, 0);
            header = new byte[Marshal.SizeOf(bi)];
            IntPtr pHeader = LocalAlloc(GPTR, (uint)Marshal.SizeOf(bi));
            Marshal.StructureToPtr(biNew, pHeader, false);
            Marshal.Copy(pHeader, header, 0, Marshal.SizeOf(bi));
            LocalFree(pHeader);
            header.CopyTo(data, HdrSize);
            RealBits.CopyTo(data, (int)bfh.bfOffBits);
            FileStream fs = new FileStream(FileName, FileMode.Create);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            DeleteObject(SelectObject(hMemDC, hOldBitmap));
            DeleteDC(hMemDC);
            ReleaseDC(hDC);

            //Works only on Mobile 5
            Bitmap a = new Bitmap(FileName);
            a.Save(FileName + ".GIF", ImageFormat.Gif);
            a.Dispose();

        }
        public void SetPenColor(Color penColour)
        {
            SignaturePen = new Pen(penColour);
        }
        public void LoadImage(String ImageFileName)
        {
            try
            {
                BackGroundImage = new Bitmap(ImageFileName);
                GraphicsHandle = Graphics.FromImage(BackGroundImage);
                Invalidate();
            }
            catch (Exception) { throw; }
        }
    }

}
