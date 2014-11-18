namespace MobileTech.Windows.UI.MainMenu
{
	partial class MainMenuView
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
            this.m_mbSetup = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbCustomerService = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbEndDay = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbStartDay = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbInventory = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbTransmitData = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbReview = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbInformation = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbSetup
            // 
            this.m_mbSetup.BackColor = System.Drawing.Color.White;
            this.m_mbSetup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbSetup.Location = new System.Drawing.Point(123, 223);
            this.m_mbSetup.Name = "m_mbSetup";
            this.m_mbSetup.Picture = MobileTech.Windows.UI.ImageKeys.Setup;
            this.m_mbSetup.PictureDisabled = MobileTech.Windows.UI.ImageKeys.SetupDisabled;
            this.m_mbSetup.PictureFocus = MobileTech.Windows.UI.ImageKeys.SetupFocus;
            this.m_mbSetup.ShowBorder = false;
            this.m_mbSetup.ShowFocusBorder = true;
            this.m_mbSetup.Size = new System.Drawing.Size(114, 68);
            this.m_mbSetup.TabIndex = 8;
            this.m_mbSetup.Text = "Setup";
            this.m_mbSetup.Click += new System.EventHandler(this.OnSetupClick);
            // 
            // m_mbCustomerService
            // 
            this.m_mbCustomerService.BackColor = System.Drawing.Color.White;
            this.m_mbCustomerService.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbCustomerService.Location = new System.Drawing.Point(4, 76);
            this.m_mbCustomerService.Name = "m_mbCustomerService";
            this.m_mbCustomerService.Picture = MobileTech.Windows.UI.ImageKeys.CustomerService;
            this.m_mbCustomerService.PictureDisabled = MobileTech.Windows.UI.ImageKeys.CustomerServiceDisabled;
            this.m_mbCustomerService.PictureFocus = MobileTech.Windows.UI.ImageKeys.CustomerServiceFocus;
            this.m_mbCustomerService.ShowBorder = false;
            this.m_mbCustomerService.ShowFocusBorder = true;
            this.m_mbCustomerService.Size = new System.Drawing.Size(114, 68);
            this.m_mbCustomerService.TabIndex = 3;
            this.m_mbCustomerService.Text = "Customer Ops";
            this.m_mbCustomerService.Click += new System.EventHandler(this.OnCustomerServiceClick);
            // 
            // m_mbEndDay
            // 
            this.m_mbEndDay.BackColor = System.Drawing.Color.White;
            this.m_mbEndDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbEndDay.Location = new System.Drawing.Point(123, 76);
            this.m_mbEndDay.Name = "m_mbEndDay";
            this.m_mbEndDay.Picture = MobileTech.Windows.UI.ImageKeys.EndDay;
            this.m_mbEndDay.PictureDisabled = MobileTech.Windows.UI.ImageKeys.EndDayDisabled;
            this.m_mbEndDay.PictureFocus = MobileTech.Windows.UI.ImageKeys.EndDayFocus;
            this.m_mbEndDay.ShowBorder = false;
            this.m_mbEndDay.ShowFocusBorder = true;
            this.m_mbEndDay.Size = new System.Drawing.Size(114, 68);
            this.m_mbEndDay.TabIndex = 4;
            this.m_mbEndDay.Text = "End Day";
            this.m_mbEndDay.Click += new System.EventHandler(this.OnEndDayClick);
            // 
            // m_mbStartDay
            // 
            this.m_mbStartDay.BackColor = System.Drawing.Color.White;
            this.m_mbStartDay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbStartDay.Location = new System.Drawing.Point(4, 4);
            this.m_mbStartDay.Name = "m_mbStartDay";
            this.m_mbStartDay.Picture = MobileTech.Windows.UI.ImageKeys.StartDay;
            this.m_mbStartDay.PictureDisabled = MobileTech.Windows.UI.ImageKeys.StartDayDisabled;
            this.m_mbStartDay.PictureFocus = MobileTech.Windows.UI.ImageKeys.StartDayFocus;
            this.m_mbStartDay.ShowBorder = false;
            this.m_mbStartDay.ShowFocusBorder = true;
            this.m_mbStartDay.Size = new System.Drawing.Size(114, 68);
            this.m_mbStartDay.TabIndex = 1;
            this.m_mbStartDay.Text = "Start Day";
            this.m_mbStartDay.Click += new System.EventHandler(this.OnStartDayClick);
            // 
            // m_mbInventory
            // 
            this.m_mbInventory.BackColor = System.Drawing.Color.White;
            this.m_mbInventory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbInventory.Location = new System.Drawing.Point(123, 4);
            this.m_mbInventory.Name = "m_mbInventory";
            this.m_mbInventory.Picture = MobileTech.Windows.UI.ImageKeys.Inventory;
            this.m_mbInventory.PictureDisabled = MobileTech.Windows.UI.ImageKeys.InventoryDisabled;
            this.m_mbInventory.PictureFocus = MobileTech.Windows.UI.ImageKeys.InventoryFocus;
            this.m_mbInventory.ShowBorder = false;
            this.m_mbInventory.ShowFocusBorder = true;
            this.m_mbInventory.Size = new System.Drawing.Size(114, 68);
            this.m_mbInventory.TabIndex = 2;
            this.m_mbInventory.Text = "Inventory";
            this.m_mbInventory.Click += new System.EventHandler(this.OnInventoryClick);
            // 
            // m_mbTransmitData
            // 
            this.m_mbTransmitData.BackColor = System.Drawing.Color.White;
            this.m_mbTransmitData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbTransmitData.Location = new System.Drawing.Point(3, 223);
            this.m_mbTransmitData.Name = "m_mbTransmitData";
            this.m_mbTransmitData.Picture = MobileTech.Windows.UI.ImageKeys.TransmitData;
            this.m_mbTransmitData.PictureDisabled = MobileTech.Windows.UI.ImageKeys.TransmitDataDisabled;
            this.m_mbTransmitData.PictureFocus = MobileTech.Windows.UI.ImageKeys.TransmitDataFocus;
            this.m_mbTransmitData.ShowBorder = false;
            this.m_mbTransmitData.ShowFocusBorder = true;
            this.m_mbTransmitData.Size = new System.Drawing.Size(114, 68);
            this.m_mbTransmitData.TabIndex = 7;
            this.m_mbTransmitData.Text = "Transmit Data";
            this.m_mbTransmitData.Click += new System.EventHandler(this.OnTransmitDataClick);
            // 
            // m_mbReview
            // 
            this.m_mbReview.BackColor = System.Drawing.Color.White;
            this.m_mbReview.Enabled = false;
            this.m_mbReview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbReview.Location = new System.Drawing.Point(3, 150);
            this.m_mbReview.Name = "m_mbReview";
            this.m_mbReview.Picture = MobileTech.Windows.UI.ImageKeys.Review;
            this.m_mbReview.PictureDisabled = MobileTech.Windows.UI.ImageKeys.ReviewDisabled;
            this.m_mbReview.PictureFocus = MobileTech.Windows.UI.ImageKeys.ReviewFocus;
            this.m_mbReview.ShowBorder = false;
            this.m_mbReview.ShowFocusBorder = true;
            this.m_mbReview.Size = new System.Drawing.Size(114, 68);
            this.m_mbReview.TabIndex = 5;
            this.m_mbReview.Text = "Review";
            // 
            // m_mbInformation
            // 
            this.m_mbInformation.BackColor = System.Drawing.Color.White;
            this.m_mbInformation.Enabled = false;
            this.m_mbInformation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbInformation.Location = new System.Drawing.Point(123, 149);
            this.m_mbInformation.Name = "m_mbInformation";
            this.m_mbInformation.Picture = MobileTech.Windows.UI.ImageKeys.Information;
            this.m_mbInformation.PictureDisabled = MobileTech.Windows.UI.ImageKeys.InformationDisabled;
            this.m_mbInformation.PictureFocus = MobileTech.Windows.UI.ImageKeys.InformationFocus;
            this.m_mbInformation.ShowBorder = false;
            this.m_mbInformation.ShowFocusBorder = true;
            this.m_mbInformation.Size = new System.Drawing.Size(114, 68);
            this.m_mbInformation.TabIndex = 6;
            this.m_mbInformation.Text = "Information";
            // 
            // MainMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbInformation);
            this.Controls.Add(this.m_mbReview);
            this.Controls.Add(this.m_mbTransmitData);
            this.Controls.Add(this.m_mbInventory);
            this.Controls.Add(this.m_mbSetup);
            this.Controls.Add(this.m_mbCustomerService);
            this.Controls.Add(this.m_mbEndDay);
            this.Controls.Add(this.m_mbStartDay);
            this.KeyPreview = true;
            this.Name = "MainMenuView";
            this.Text = "0000 - Main Menu";
            this.Activated += new System.EventHandler(this.OnActivated);
            this.ResumeLayout(false);

		}

		#endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_mbSetup;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbCustomerService;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbEndDay;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbStartDay;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbInventory;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbTransmitData;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbReview;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbInformation;


    }
}