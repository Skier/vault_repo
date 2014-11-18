/// ///////////////////////////////////////////////////////////////////
/// 
/// Color Button Control
/// Designed by Paul Olsen 
/// PocketPC Controls.com
/// June 2004
/// 
/// All rights reserved
/// 
/// ///////////////////////////////////////////////////////////////////
/// 
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.ComponentModel;
using MobileTech.Windows.UI;

namespace MobileTech.Windows.UI.Controls
{
	#region MenuButton class

	public enum Shape	{	Ellipse, Rectangle }

	/// <summary>
	/// Class to hold overidden button, which tripgers the ColorPicker
	/// </summary>
	public class MenuButton : Control
    {

        #region Fields

        // internal variables
		private Color m_BackDownColor = Color.Black;
		private Color m_ForeDownColor = Color.White;	
		private Color m_BackColor = Color.White;
		private Color m_ForeColor = Color.Black;
        private Color m_ForeColorDisabled = Color.LightSlateGray;
		private State m_State = State.Up;
		private Shape m_Shape = Shape.Rectangle;		

		// gdi objects
		private Pen m_pen;
		private SolidBrush m_brush;
		private SolidBrush m_textBrush;

		// offscreen bitmap
		private Image m_Icon; // user defined image for button
        private Image m_IconFocus;
        private Image m_IconDisabled;
        private ImageKeys m_IconKey;
        private ImageKeys m_IconFocusKey;
        private ImageKeys m_IconDisabledKey;
		private Bitmap m_ImageUp = null; // user defined image for button
		private Bitmap m_ImageDown = null; // user defined image for button		
		private int m_ImageLeftMargin = 3;
		private int m_TextGraphicSpace = 3;
		private bool m_ImageShift = false;
		private bool m_TextShift = false;
		private bool m_TransparentIcon = true;
		private bool m_TransparentImage = true;
        private bool m_ShowBorder = false;
        private bool m_ShowFocusBorder = true;

		private enum State { Up, Down }

        #endregion
        
        #region Properties

        /// <summary>
		/// Have the text shift on Mouse Down.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Have the text shift on Mouse Down.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public bool TextShift
		{
			get { return m_TextShift; }
			set 
			{				
				m_TextShift = value;		
				Invalidate();
			}
		}

		/// <summary>
		/// Have the button image shift on Mouse Down.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Have the button image shift on Mouse Down.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public bool IconShift
		{
			get { return m_ImageShift; }
			set 
			{
				m_ImageShift = value;		
				Invalidate();
			}
		}


		/// <summary>
		/// Set the transparency of the image (Top Left Pixel).
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Set the transparency of the image (Top Left Pixel).")]
		[System.ComponentModel.Localizable(true)]
#endif
		public bool TransparentIcon
		{
			get { return m_TransparentIcon; }
			set 
			{
				m_TransparentIcon = value;		
				Invalidate();
			}
		}
		
		/// <summary>
		/// Set the transparency of the image (Top Left Pixel).
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Set the transparency of the image (Top Left Pixel).")]
		[System.ComponentModel.Localizable(true)]
#endif
		public bool TransparentImage
		{
			get { return m_TransparentImage; }
			set 
			{
				m_TransparentImage = value;		
				Invalidate();
			}
		}

		/// <summary>
		/// Sets or gets the left margin for graphic of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Set or get the space between the graphic and the text for the control.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public int IconTextSpace
		{
			get { return m_TextGraphicSpace; }
			set 
			{
				m_TextGraphicSpace = value;		
				Invalidate();
			}
		}

		/// <summary>
		/// Sets or gets the left margin for graphic of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Set or get the left margin for graphic of the control.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public int IconMargin
		{
			get { return m_ImageLeftMargin; }
			set 
			{
				m_ImageLeftMargin = value;		
				Invalidate();
			}
		}

        /// <summary>
        /// Sets the graphic of the control.
        /// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Icon for the face of the button.")]
		[System.ComponentModel.Localizable(true)]
#endif
        public ImageKeys Picture
        {
            get { return m_IconKey; }
            set
            {
                m_IconKey = value;
                m_Icon = GUI.GetImage(m_IconKey);
                Invalidate();
            }
        }

        /// <summary>
        /// Sets the graphic of the control.
        /// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Icon for the face of the button.")]
		[System.ComponentModel.Localizable(true)]
#endif
        public ImageKeys PictureFocus
        {
            get { return m_IconFocusKey; }
            set
            {
                m_IconFocusKey = value;
                m_IconFocus = GUI.GetImage(m_IconFocusKey);
                Invalidate();
            }
        }

        /// <summary>
        /// Sets the graphic of the control.
        /// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Icon for the face of the button.")]
		[System.ComponentModel.Localizable(true)]
#endif
        public ImageKeys PictureDisabled
        {
            get { return m_IconDisabledKey; }
            set
            {
                m_IconDisabledKey = value;
                m_IconDisabled = GUI.GetImage(m_IconDisabledKey);
                Invalidate();
            }
        }

        /// <summary>
		/// Sets the graphic of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Mouse Up Image of the button.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public Bitmap ImageUp
		{
			get { return m_ImageUp; }
			set 
			{
				m_ImageUp = value;		
				Invalidate();
			}
		}

		/// <summary>
		/// Sets the graphic of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Mouse Down Image of the button.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public Bitmap ImageDown
		{
			get { return m_ImageDown; }
			set 
			{
				m_ImageDown = value;		
				Invalidate();
			}
		}


		/// <summary>
		/// Sets the Shape of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Shape of the control.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public Shape ButtonShape
		{
			get { return m_Shape; }
			set 
			{
				m_Shape = value;		
				Invalidate();
			}
		}


		/// <summary>
		/// Gets or sets the BackColor of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Gets or sets the BackColor of the control.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public override Color BackColor
		{
			get { return m_BackColor; }
			set 
			{
				m_BackColor = value;
				base.BackColor = value;
				Invalidate();
			}
		}


		/// <summary>
		/// Gets or sets the ForeColor of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Gets or sets the ForeColor of the control.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public override Color ForeColor
		{
			get { return m_ForeColor; }
			set 
			{				
				m_ForeColor = value;	
				base.ForeColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the BackDownColor of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("BackDown Color of the control.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public Color BackDownColor
		{
			get { return m_BackDownColor; }
			set 
			{
				m_BackDownColor = value;				
			}
		}

		/// <summary>
		/// Gets or sets the ForeDownColor of the control.
		/// </summary>
#if NETCFDESIGNTIME
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("ForeDown Color of the control.")]
		[System.ComponentModel.Localizable(true)]
#endif
		public Color ForeDownColor
		{
			get { return m_ForeDownColor; }
			set 
			{
				m_ForeDownColor = value;
			}
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				Invalidate();
			}
		}

        public bool ShowBorder
        {
            get
            {
                return m_ShowBorder;
            }
            set
            {
                m_ShowBorder = value;
            }
        }

        public bool ShowFocusBorder
        {
            get
            {
                return m_ShowFocusBorder;
            }
            set
            {
                m_ShowFocusBorder = value;
            }
        }

		#endregion

        #region Methods

        public MenuButton(){ }

        private void DrawButton(Graphics g)
		{
			if (m_ImageUp == null)
				DrawColorButton(g);
			else
				DrawImageButton(g);		
		}

		private void CollectColors()
		{
			//Work out the colors that we should be using for the text and background
			if (m_State == State.Up)
			{
				m_brush = new SolidBrush(m_BackColor);
				m_textBrush = new SolidBrush(Enabled ? m_ForeColor : m_ForeColorDisabled);
				m_pen = new Pen(m_ForeColor);
			}
			else
			{
				m_brush = new SolidBrush(m_BackDownColor);
				m_textBrush = new SolidBrush(m_ForeDownColor);
				m_pen = new Pen(m_ForeDownColor);
			}
		}
		
		private void DrawColorButton(Graphics g)
		{	
			CollectColors();
			
			// draw the button to the screen
            switch (m_Shape)
            {
                case Shape.Ellipse:
                    g.FillEllipse(m_brush, 0, 0, Width - 1, Height - 1);
                    g.DrawEllipse(m_pen, 0, 0, Width - 1, Height - 1);
                    break;
                case Shape.Rectangle:
                    g.FillRectangle(m_brush, 0, 0, Width, Height);

                    if (m_ShowBorder)
                    {
                        g.DrawRectangle(m_pen, 0, 0, Width - 1, Height - 1);
                    }

                    break;
            }


			DrawStringToButton(g);
			DrawIconToButton(g);

            if (Focused && m_State == State.Up)
            {
                ShowFocus();
            }
			
		}
		
		private void DrawImageButton(Graphics g)
		{
			CollectColors();

			ImageAttributes imageAttr = new ImageAttributes();				
			
			if (m_State == State.Up)
			{
				// set the transperancy of the image to be shown					
				if (m_TransparentImage)				
					imageAttr.SetColorKey(m_ImageUp.GetPixel(0,0),m_ImageUp.GetPixel(0,0));
				
				g.DrawImage(m_ImageUp, new Rectangle(0,0,Width,Height), 0, 0, m_ImageUp.Width, m_ImageUp.Height, GraphicsUnit.Pixel, imageAttr);				
			}
			else
			{
				if (m_ImageDown != null)
				{
					// set the transperancy of the image to be shown					
					if (m_TransparentImage)				
						imageAttr.SetColorKey(m_ImageDown.GetPixel(0,0),m_ImageDown.GetPixel(0,0));

					g.DrawImage(m_ImageDown, new Rectangle(0,0,Width,Height), 0, 0, m_ImageDown.Width, m_ImageDown.Height, GraphicsUnit.Pixel, imageAttr);				
				}
				else
				{
					g.Clear(m_BackDownColor);
				}
			}
			DrawStringToButton(g);
			DrawIconToButton(g);
		}

		private void DrawStringToButton(Graphics g)
		{
			//Find out the size of the text
			SizeF textSize = new SizeF();
			textSize = g.MeasureString(Text, Font);

			//Work out where to position the text horizontally
			float x=0, y=0;						
    
            x = (Width - textSize.Width) /2; 
			
			// place text at the bottom
            if (textSize.Height < Height)
                y = (Height - textSize.Height - 2); 

			//Draw the text in the centre of the button using the default font
			if (m_TextShift && m_State == State.Down)
			{
				x ++;
				y ++;
			}
			
			g.DrawString(Text, Font, m_textBrush, x, y);	
		}

		private void DrawIconToButton(Graphics g)
		{
            //Find out the size of the text
			SizeF textSize = new SizeF();
			textSize = g.MeasureString(Text, Font);
			// if there is a graphic to draw, do it

			if ( m_Icon != null) 
			{
				ImageAttributes imageAttr = new ImageAttributes();				
				
				// set the transperancy of the image to be shown					
				if (m_TransparentIcon)
                    imageAttr.SetColorKey(Color.Transparent, Color.Transparent);

                
				Rectangle dstRect;
				// set the area the image will occupy								
				if ((m_State == State.Down) && m_ImageShift == true)
                    dstRect = new Rectangle(((int)((Width - m_Icon.Width) / 2) + 1), (int)((Height - m_Icon.Height - textSize.Height) /2) + 1, m_Icon.Width, m_Icon.Height);
				else
                    dstRect = new Rectangle((int)((Width - m_Icon.Width) / 2), (int)((Height - m_Icon.Height - textSize.Height) /2), m_Icon.Width, m_Icon.Height);
				
				// finally draw the custom graphic to show on the button
                if (m_State == State.Up)
                {
                    g.DrawImage(Enabled ? m_Icon : m_IconDisabled, dstRect, 0, 0, m_Icon.Width, m_Icon.Height, GraphicsUnit.Pixel, imageAttr);
                }
                else
                {
                    g.DrawImage(m_IconFocus, dstRect, 0, 0, m_Icon.Width, m_Icon.Height, GraphicsUnit.Pixel, imageAttr);
                }
			}
        }

        #endregion

        #region Events

        protected override void Dispose(bool disposing)
		{
			base.Dispose (disposing);
		}

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            ShowFocus();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            HideFocus();
        }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);				
			Graphics g = CreateGraphics();
			g.Clear(Parent.BackColor); 
			DrawButton(g); 			
			g.Dispose();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}	

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e) 
		{	
			base.OnMouseDown(e); 			
			m_State = State.Down; 									
			Graphics g = CreateGraphics();
			DrawButton(g);
			g.Dispose();									
		} 

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e) 
		{	
			base.OnMouseUp(e);
			// had to get around the object being diposed before the rest of this method being carried out
			try
			{
				m_State = State.Up;
				Graphics g = CreateGraphics();
				DrawButton(g);
				g.Dispose();			
			}
			catch (Exception ex)
            {
                Exception shabba = ex;
                if (shabba == ex)
                { }
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if ((e.KeyChar == (char)System.Windows.Forms.Keys.Enter))
            {
                m_State = State.Down;
                Graphics g = CreateGraphics();
                DrawButton(g);
                base.OnClick(e);
                m_State = State.Up;
                DrawButton(g);
            }

        }

        protected void ShowFocus()
        {
            using (Pen pen = new Pen(m_ForeColor))
            {
                Graphics g = CreateGraphics();
                g.DrawRectangle(pen,
                    new Rectangle(0,
                    0,
                    Width - 1,
                    Height - 1));
                g.DrawRectangle(pen,
                    new Rectangle(1,
                    1,
                    Width - 3,
                    Height - 3));
                g.DrawRectangle(pen,
                    new Rectangle(2,
                    2,
                    Width - 5,
                    Height - 5));
                g.Dispose();
            }
        }

        protected void HideFocus()
        {
            using (Pen pen = new Pen(m_BackColor))
            {
                Graphics g = CreateGraphics();
                if (!m_ShowBorder)
                {
                    g.DrawRectangle(pen,
                        new Rectangle(0,
                        0,
                        Width - 1,
                        Height - 1));
                }
                g.DrawRectangle(pen,
                    new Rectangle(1,
                    1,
                    Width - 3,
                    Height - 3));
                g.DrawRectangle(pen,
                    new Rectangle(2,
                    2,
                    Width - 5,
                    Height - 5));
                g.Dispose();
            }
        }

        #endregion

    }
        

    #endregion
}
