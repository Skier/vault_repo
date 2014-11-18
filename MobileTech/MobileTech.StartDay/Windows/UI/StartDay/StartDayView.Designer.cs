namespace MobileTech.Windows.UI.StartDay
{
	partial class StartDayView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
        private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_lbSalesRep = new System.Windows.Forms.Label();
            this.m_lbLocation = new System.Windows.Forms.Label();
            this.m_lbRoute = new System.Windows.Forms.Label();
            this.m_mbAccept = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_cbEmployee = new System.Windows.Forms.ComboBox();
            this.m_dtpRouteDate = new System.Windows.Forms.DateTimePicker();
            this.m_lbDate = new System.Windows.Forms.Label();
            this.m_lbLocation2 = new System.Windows.Forms.Label();
            this.m_lbRoute2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lbSalesRep
            // 
            this.m_lbSalesRep.Location = new System.Drawing.Point(7, 87);
            this.m_lbSalesRep.Name = "m_lbSalesRep";
            this.m_lbSalesRep.Size = new System.Drawing.Size(58, 21);
            this.m_lbSalesRep.Text = "Sales Rep";
            // 
            // m_lbLocation
            // 
            this.m_lbLocation.Location = new System.Drawing.Point(7, 13);
            this.m_lbLocation.Name = "m_lbLocation";
            this.m_lbLocation.Size = new System.Drawing.Size(72, 13);
            this.m_lbLocation.Text = "Location";
            // 
            // m_lbRoute
            // 
            this.m_lbRoute.Location = new System.Drawing.Point(7, 49);
            this.m_lbRoute.Name = "m_lbRoute";
            this.m_lbRoute.Size = new System.Drawing.Size(72, 13);
            this.m_lbRoute.Text = "Route";
            // 
            // m_mbAccept
            // 
            this.m_mbAccept.BackColor = System.Drawing.Color.White;
            this.m_mbAccept.BackDownColor = System.Drawing.Color.Black;
            this.m_mbAccept.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbAccept.ForeDownColor = System.Drawing.Color.White;
            this.m_mbAccept.IconMargin = 3;
            this.m_mbAccept.IconShift = false;
            this.m_mbAccept.IconTextSpace = 3;
            this.m_mbAccept.Location = new System.Drawing.Point(192, 230);
            this.m_mbAccept.Name = "m_mbAccept";
            this.m_mbAccept.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbAccept.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbAccept.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbAccept.ShowBorder = true;
            this.m_mbAccept.ShowFocusBorder = true;
            this.m_mbAccept.Size = new System.Drawing.Size(48, 64);
            this.m_mbAccept.TabIndex = 1;
            this.m_mbAccept.Text = "Done";
            this.m_mbAccept.TextShift = false;
            this.m_mbAccept.TransparentIcon = true;
            this.m_mbAccept.TransparentImage = true;
            this.m_mbAccept.Click += new System.EventHandler(this.OnAcceptClick);
            // 
            // m_cbEmployee
            // 
            this.m_cbEmployee.Location = new System.Drawing.Point(81, 86);
            this.m_cbEmployee.Name = "m_cbEmployee";
            this.m_cbEmployee.Size = new System.Drawing.Size(152, 22);
            this.m_cbEmployee.TabIndex = 2;
            // 
            // m_dtpRouteDate
            // 
            this.m_dtpRouteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpRouteDate.Location = new System.Drawing.Point(81, 123);
            this.m_dtpRouteDate.Name = "m_dtpRouteDate";
            this.m_dtpRouteDate.Size = new System.Drawing.Size(152, 22);
            this.m_dtpRouteDate.TabIndex = 3;
            // 
            // m_lbDate
            // 
            this.m_lbDate.Location = new System.Drawing.Point(7, 124);
            this.m_lbDate.Name = "m_lbDate";
            this.m_lbDate.Size = new System.Drawing.Size(72, 20);
            this.m_lbDate.Text = "Date";
            // 
            // m_lbLocation2
            // 
            this.m_lbLocation2.Location = new System.Drawing.Point(78, 13);
            this.m_lbLocation2.Name = "m_lbLocation2";
            this.m_lbLocation2.Size = new System.Drawing.Size(150, 13);
            // 
            // m_lbRoute2
            // 
            this.m_lbRoute2.Location = new System.Drawing.Point(78, 49);
            this.m_lbRoute2.Name = "m_lbRoute2";
            this.m_lbRoute2.Size = new System.Drawing.Size(150, 13);
            // 
            // StartDayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_lbRoute2);
            this.Controls.Add(this.m_lbLocation2);
            this.Controls.Add(this.m_lbDate);
            this.Controls.Add(this.m_dtpRouteDate);
            this.Controls.Add(this.m_cbEmployee);
            this.Controls.Add(this.m_mbAccept);
            this.Controls.Add(this.m_lbSalesRep);
            this.Controls.Add(this.m_lbLocation);
            this.Controls.Add(this.m_lbRoute);
            this.Name = "StartDayView";
            this.Text = "1000 - Start day";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Label m_lbSalesRep;
        private System.Windows.Forms.Label m_lbLocation;
        private System.Windows.Forms.Label m_lbRoute;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbAccept;
        private System.Windows.Forms.ComboBox m_cbEmployee;
        private System.Windows.Forms.DateTimePicker m_dtpRouteDate;
        private System.Windows.Forms.Label m_lbDate;
        private System.Windows.Forms.Label m_lbLocation2;
        private System.Windows.Forms.Label m_lbRoute2;
	}
}