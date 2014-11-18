#define DOUBLE_BUFFER
#define CASH_IMAGES
#define PRELOAD_IMAGES

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;


namespace QuickBooksAgent.Windows.UI.Controls
{
    
    public class MenuButton : Control, IMenuButton
    {
        const String DESING_MODE_IMAGE_PATH = @"c:\QuickBooksAgent\bitmaps";
        const String FILE_EXTENTION = "bmp";


        Color m_focusBorderColor = Color.FromArgb(255, 180, 0);

        public MenuButton()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            ShowBorder = true;            
        }

        #region Fields

#if CASH_IMAGES
        Dictionary<String, Bitmap> m_imageCash = new Dictionary<String,Bitmap>();
#endif

#if DOUBLE_BUFFER
        Bitmap m_backBuffer;
#endif

        bool m_pressed;
        #endregion

        #region IMenuButton Members

        #region Picture
        ImageKeys m_picture;
        public ImageKeys Picture
        {
            get
            {
                return m_picture;
            }
            set
            {
                if (m_picture != value)
                {
                    m_picture = value;
                
#if PRELOAD_IMAGES
                    PreloadImages();
#endif
                    if (IsDesignMode)
                        Invalidate();
                }
            }
        }
        #endregion

        #region Select
#if WINCE
        public void Select()
        {
            Focus();
        }
#endif
        #endregion

        #region ShowBorder
        bool m_showBorder;
        public bool ShowBorder
        {
            get
            {
                return m_showBorder;
            }
            set
            {
                m_showBorder = value;
            }
        }
        #endregion

        /*
        #region JoystickEnabled

        private bool m_isJoystickEnabled;

        public bool JoystickEnabled
        {
            get { return m_isJoystickEnabled; }
            set { m_isJoystickEnabled = value; }
        }

        #endregion

        #region JoystickPosition

        private Point m_JoystickPosition;

        public Point JoystickPosition
        {
            get { return m_JoystickPosition; }
            set { m_JoystickPosition = value; }
        }

        #endregion
        */

        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            m_pressed = true;

            Invalidate();
        }
        #endregion

        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            m_pressed = false;

            Invalidate();
        }
        #endregion

        #region OnEnabledChanged
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            Invalidate();
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

        #region OnKeyPress
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == '\r')
            {
                m_pressed = true;

                ForcePaint();

                OnClick(new EventArgs());

                m_pressed = false;

                Invalidate();
            }
        }
        #endregion

        #region OnPaintBackground
        /// <summary>
        /// Suppress background repainting
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }
        #endregion

        #region ForcePaint
        void ForcePaint()
        {
            using (Graphics graphics = CreateGraphics())
            {
                OnPaint(new PaintEventArgs(graphics, ClientRectangle));
            }
        }
        #endregion

        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {

#if DOUBLE_BUFFER
            if (m_backBuffer == null)
                m_backBuffer = new Bitmap(Width, Height);


            using (Graphics bufferGraphics = Graphics.FromImage(m_backBuffer))
            {
                Draw(bufferGraphics);
            }

            e.Graphics.DrawImage(m_backBuffer, 0, 0);

#else
                Debug.WriteLine("MenuButton::OnPaint, Button:" + Text);

                Draw(e.Graphics);
#endif
        }
        #endregion

        #region Draw
        private void Draw(Graphics graphics)
        {
            int shift = m_pressed ? 1 : 0;
            int width = Width - 4, height = Height - 4;

            #region Draw BackGround
            graphics.Clear(BackColor);
            #endregion

            #region Draw Text
            Color foreColor = Enabled ? ForeColor
                                      : Color.LightSlateGray;
            SizeF textSize = graphics.MeasureString(Text, Font);

            using (SolidBrush solidBrush = new SolidBrush(foreColor))
            {

                float x = 0, y = 0;

                x = (width - textSize.Width) / 2;

                if (textSize.Height < height)
                    y = (height - textSize.Height - 2);

                graphics.DrawString(Text, Font, solidBrush, x + shift, y + shift);
            }
            #endregion

            #region Draw Image

            String imagePath = GetImagePath();

            if (File.Exists(imagePath))
            {
#if CASH_IMAGES
                Bitmap bitmap = GetImage(imagePath);

                Rectangle dstRect = new Rectangle(
                    (int)((width - bitmap.Width) / 2) + shift,
                    (int)((height - bitmap.Height - textSize.Height) / 2) + shift,
                    bitmap.Width,
                    bitmap.Height);


                graphics.DrawImage(bitmap,
                    dstRect,
                    0, 0,
                    bitmap.Width, bitmap.Height,
                    GraphicsUnit.Pixel,
                    new ImageAttributes());


#else
                using (Bitmap bitmap = new Bitmap(imagePath))
                {
                    Rectangle dstRect = new Rectangle(
                        (int)((width - bitmap.Width) / 2) + shift,
                        (int)((height - bitmap.Height - textSize.Height) / 2) + shift,
                        bitmap.Width,
                        bitmap.Height);

                    graphics.DrawImage(bitmap,
                        dstRect,
                        0, 0,
                        bitmap.Width, bitmap.Height,
                        GraphicsUnit.Pixel,
                        new ImageAttributes());
                }
#endif

            }
            #endregion

            #region Draw Border
            if (ShowBorder || Focused)
            {
                int borderWidth = 1;

                using (Pen pen = new Pen(
                    Focused ? m_focusBorderColor : foreColor, 
                    borderWidth))
                {
                    graphics.DrawRectangle(pen,
                        new Rectangle(0 + shift,
                        0 + shift,
                        width - 1,
                        height - 1));
                }


                if (!m_pressed)
                {

                    using (Pen pen = new Pen(Color.LightGray))
                    {
                        graphics.DrawLine(pen, 1, height, width - 1, height);
                        graphics.DrawLine(pen, width, 1, width, height);
                    }

                }
            }
            #endregion

        }
        #endregion

#if CASH_IMAGES

        #region GetImage
        public Bitmap GetImage(String path)
        {
            return GetImage(path, Enabled, m_pressed);
        }

        public Bitmap GetImage(String path, bool enabled, bool pressed)
        {
/*
            String internalPath = path + (enabled ?
                pressed ? ".pressed" : ".enabled"
                : ".disabled");
*/

            String internalPath = path + (enabled ?  ".enabled" : ".disabled");

            if (!m_imageCash.ContainsKey(internalPath))
            {
                if (enabled)
                    m_imageCash[internalPath] = new Bitmap(path);
                else
                    m_imageCash[internalPath] = Color2BW(new Bitmap(path));
            }

            return m_imageCash[internalPath];
        }
        #endregion

#endif
#if PRELOAD_IMAGES

        #region PreloadImages
        private void PreloadImages()
        {
            m_imageCash.Clear();

            String imagePath = GetImagePath();

            if (File.Exists(imagePath))
            {
                GetImage(imagePath,Enabled,false);
            }
        }
        #endregion

#endif
        #region GetImagePath
        private String GetImagePath()
        {
            return String.Format(@"{0}\{1}.{2}",
                IsDesignMode ? DESING_MODE_IMAGE_PATH : Host.GetPath("Bitmaps"),
                (int)m_picture,
                FILE_EXTENTION);
        }
        #endregion

        #region IsDesignMode
        bool IsDesignMode
        {
            get
            {
                return Site != null && Site.DesignMode;
            }
        }
        #endregion

        #region OnResize
#if DOUBLE_BUFFER
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (m_backBuffer != null)
            {
                m_backBuffer.Dispose();
                m_backBuffer = null;
            }
        }
#endif
        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
#if CASH_IMAGES
            if (m_imageCash != null)
            {
                foreach (Bitmap bitmap in m_imageCash.Values)
                    bitmap.Dispose();

                m_imageCash = null;
            }
#endif

#if DOUBLE_BUFFER
            if (m_backBuffer != null)
            {
                m_backBuffer.Dispose();
                m_backBuffer = null;
            }
#endif
        }

        #endregion

        #region InvertImage
        public static Bitmap InvertImage(Bitmap bitmap)
        {
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);   
            unsafe   
            {      
                
                byte * rowPtr = (byte *)data.Scan0;

                int pixelSize = 3;    
                int remain = data.Stride - data.Width * pixelSize;      
    
                for(int y = 0; y < data.Height; y++)      
                {         
                    for(int x = 0; x < data.Width; x++)         
                    {            
                        rowPtr[0] = (byte)(255 - rowPtr[0]);            
                        rowPtr[1] = (byte)(255 - rowPtr[1]);            
                        rowPtr[2] = (byte)(255 - rowPtr[2]);            
                        rowPtr += pixelSize;         
                    }         
                    rowPtr += remain;      
                }   
            }

            bitmap.UnlockBits(data);

            return bitmap;
        }
        #endregion

        #region BW

        public static Bitmap Color2BW(Bitmap bitmap)
        {

            const float R = 0.299f;
            const float G = 0.587f;
            const float B = 0.114f;

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
               System.Drawing.Imaging.ImageLockMode.ReadWrite,
               PixelFormat.Format24bppRgb);

            const int pixelSize = 3; // rgb


            unsafe
            {
                for (int y = 0; y < data.Height; y++)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);

                    for (int x = 0; x < data.Width; x++)
                    {
                        int r = x * pixelSize;
                        int g = r + 1;
                        int b = g + 1;

                        if (row[r] > 240 && row[g] > 240 && row[b] > 240)
                            continue;

                        byte bw = (byte)(
                            (float)row[r] * R +
                            (float)row[g] * G +
                            (float)row[b] * B);

                        row[r] = row[g] = row[b] = bw;

                    }
                }
            }

            bitmap.UnlockBits(data);

            return bitmap;
        }

        #endregion
    }
}
