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


namespace MobileTech.Windows.UI.Controls
{
    public class MenuButton : Control, IMenuButton
    {
        const String DESING_MODE_IMAGE_PATH = @"c:\mobiletech\bitmaps";
        const String FILE_EXTENTION = "bmp";

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

                base.OnClick(new EventArgs());

                m_pressed = false;
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

            #region Draw BackGround
            graphics.Clear(m_pressed ? Color.Black : BackColor);
            #endregion

            #region Draw Text
            Color foreColor = Enabled ?
                m_pressed ? Color.White : ForeColor
                                      : Color.LightSlateGray;
            SizeF textSize = graphics.MeasureString(Text, Font);

            using (SolidBrush solidBrush = new SolidBrush(foreColor))
            {

                float x = 0, y = 0;

                x = (Width - textSize.Width) / 2;

                if (textSize.Height < Height)
                    y = (Height - textSize.Height - 2);

                graphics.DrawString(Text, Font, solidBrush, x, y);
            }
            #endregion

            #region Draw Image

            String imagePath = GetImagePath();

            if (File.Exists(imagePath))
            {
#if CASH_IMAGES
                Bitmap bitmap = GetImage(imagePath);

                Rectangle dstRect = new Rectangle(
                    (int)((Width - bitmap.Width) / 2),
                    (int)((Height - bitmap.Height - textSize.Height) / 2),
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
                        (int)((Width - bitmap.Width) / 2),
                        (int)((Height - bitmap.Height - textSize.Height) / 2),
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
                int borderWidth = Focused ? 3 : 1;

                using (Pen pen = new Pen(ForeColor, borderWidth))
                {
                    graphics.DrawRectangle(pen,
                        new Rectangle(0,
                        0,
                        Width - 1,
                        Height - 1));
                }
            }
            #endregion

        }
        #endregion

#if CASH_IMAGES

        #region GetImage
        public Bitmap GetImage(String path)
        {
            if (!m_imageCash.ContainsKey(path))
                m_imageCash[path] = new Bitmap(path);

            return m_imageCash[path];
        }
        #endregion

#endif
#if PRELOAD_IMAGES

        #region PreloadImages
        private void PreloadImages()
        {
            m_imageCash.Clear();

            String imagePathEnabled = ImagePathEnabled;
            String imagePathDisabled = ImagePathDisabled;

            if (File.Exists(imagePathDisabled))
                m_imageCash[imagePathDisabled] = new Bitmap(imagePathDisabled);

            if (File.Exists(ImagePathEnabled))
                m_imageCash[ImagePathEnabled] = new Bitmap(ImagePathEnabled);
        }
        #endregion

#endif
        #region GetImagePath

        #region ImagePathDisabled
        private String ImagePathFocused
        {
            get
            {
                return String.Format(@"{0}\{1}Focus.{2}",
                    IsDesignMode ? DESING_MODE_IMAGE_PATH : Host.GetPath("Bitmaps"),
                    m_picture.ToString(),
                    FILE_EXTENTION);                                        
            }
        }
        #endregion
        #region ImagePathDisabled
        private String ImagePathDisabled
        {
            get
            {
                return String.Format(@"{0}\{1}Disabled.{2}",
                    IsDesignMode ? DESING_MODE_IMAGE_PATH : Host.GetPath("Bitmaps"),
                    m_picture.ToString(),
                    FILE_EXTENTION);

            }
        }
        #endregion
        #region ImagePathEnabled
        private String ImagePathEnabled
        {
            get
            {
                return String.Format(@"{0}\{1}.{2}",
                    IsDesignMode ? DESING_MODE_IMAGE_PATH : Host.GetPath("Bitmaps"),
                    m_picture.ToString(),
                    FILE_EXTENTION);

            }
        }
        #endregion

        private String GetImagePath()
        {
            if (Enabled && !m_pressed)
                return ImagePathEnabled;
            else if (!Enabled)
                return ImagePathDisabled;
            else
                return ImagePathFocused;
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
    }
}
