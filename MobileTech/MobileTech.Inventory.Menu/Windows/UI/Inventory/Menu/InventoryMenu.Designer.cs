namespace MobileTech.Windows.UI.Inventory.Menu
{
    partial class InventoryMenu
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
            this.m_mbPrintReport = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbExit = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbReturnToStock = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbLoadTransfer = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbLoad = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbUnload = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbLoadRequest = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbPrintReport
            // 
            this.m_mbPrintReport.BackColor = System.Drawing.Color.White;
            this.m_mbPrintReport.BackDownColor = System.Drawing.Color.Black;
            this.m_mbPrintReport.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbPrintReport.Enabled = false;
            this.m_mbPrintReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbPrintReport.ForeDownColor = System.Drawing.Color.White;
            this.m_mbPrintReport.IconMargin = 3;
            this.m_mbPrintReport.IconShift = false;
            this.m_mbPrintReport.IconTextSpace = 3;
            this.m_mbPrintReport.Location = new System.Drawing.Point(4, 220);
            this.m_mbPrintReport.Name = "m_mbPrintReport";
            this.m_mbPrintReport.Picture = MobileTech.Windows.UI.ImageKeys.PrintReport;
            this.m_mbPrintReport.PictureDisabled = MobileTech.Windows.UI.ImageKeys.PrintReportDisabled;
            this.m_mbPrintReport.PictureFocus = MobileTech.Windows.UI.ImageKeys.PrintReportFocus;
            this.m_mbPrintReport.ShowBorder = false;
            this.m_mbPrintReport.ShowFocusBorder = true;
            this.m_mbPrintReport.Size = new System.Drawing.Size(114, 68);
            this.m_mbPrintReport.TabIndex = 7;
            this.m_mbPrintReport.Text = "Print Report";
            this.m_mbPrintReport.TextShift = false;
            this.m_mbPrintReport.TransparentIcon = true;
            this.m_mbPrintReport.TransparentImage = true;
            // 
            // m_mbExit
            // 
            this.m_mbExit.BackColor = System.Drawing.Color.White;
            this.m_mbExit.BackDownColor = System.Drawing.Color.Black;
            this.m_mbExit.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbExit.ForeDownColor = System.Drawing.Color.White;
            this.m_mbExit.IconMargin = 3;
            this.m_mbExit.IconShift = false;
            this.m_mbExit.IconTextSpace = 3;
            this.m_mbExit.Location = new System.Drawing.Point(123, 220);
            this.m_mbExit.Name = "m_mbExit";
            this.m_mbExit.Picture = MobileTech.Windows.UI.ImageKeys.Done;
            this.m_mbExit.PictureDisabled = MobileTech.Windows.UI.ImageKeys.DoneDisabled;
            this.m_mbExit.PictureFocus = MobileTech.Windows.UI.ImageKeys.DoneFocus;
            this.m_mbExit.ShowBorder = false;
            this.m_mbExit.ShowFocusBorder = true;
            this.m_mbExit.Size = new System.Drawing.Size(114, 68);
            this.m_mbExit.TabIndex = 8;
            this.m_mbExit.Text = "Exit";
            this.m_mbExit.TextShift = false;
            this.m_mbExit.TransparentIcon = true;
            this.m_mbExit.TransparentImage = true;
            this.m_mbExit.Click += new System.EventHandler(this.OnExitClick);
            // 
            // m_mbReturnToStock
            // 
            this.m_mbReturnToStock.BackColor = System.Drawing.Color.White;
            this.m_mbReturnToStock.BackDownColor = System.Drawing.Color.Black;
            this.m_mbReturnToStock.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbReturnToStock.Enabled = false;
            this.m_mbReturnToStock.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbReturnToStock.ForeDownColor = System.Drawing.Color.White;
            this.m_mbReturnToStock.IconMargin = 3;
            this.m_mbReturnToStock.IconShift = false;
            this.m_mbReturnToStock.IconTextSpace = 3;
            this.m_mbReturnToStock.Location = new System.Drawing.Point(4, 148);
            this.m_mbReturnToStock.Name = "m_mbReturnToStock";
            this.m_mbReturnToStock.Picture = MobileTech.Windows.UI.ImageKeys.ReturnToStock;
            this.m_mbReturnToStock.PictureDisabled = MobileTech.Windows.UI.ImageKeys.ReturnToStockDisabled;
            this.m_mbReturnToStock.PictureFocus = MobileTech.Windows.UI.ImageKeys.ReturnToStockFocus;
            this.m_mbReturnToStock.ShowBorder = false;
            this.m_mbReturnToStock.ShowFocusBorder = true;
            this.m_mbReturnToStock.Size = new System.Drawing.Size(114, 68);
            this.m_mbReturnToStock.TabIndex = 5;
            this.m_mbReturnToStock.Text = "Return To Stock ";
            this.m_mbReturnToStock.TextShift = false;
            this.m_mbReturnToStock.TransparentIcon = true;
            this.m_mbReturnToStock.TransparentImage = true;
            // 
            // m_mbLoadTransfer
            // 
            this.m_mbLoadTransfer.BackColor = System.Drawing.Color.White;
            this.m_mbLoadTransfer.BackDownColor = System.Drawing.Color.Black;
            this.m_mbLoadTransfer.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbLoadTransfer.Enabled = false;
            this.m_mbLoadTransfer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbLoadTransfer.ForeDownColor = System.Drawing.Color.White;
            this.m_mbLoadTransfer.IconMargin = 3;
            this.m_mbLoadTransfer.IconShift = false;
            this.m_mbLoadTransfer.IconTextSpace = 3;
            this.m_mbLoadTransfer.Location = new System.Drawing.Point(123, 4);
            this.m_mbLoadTransfer.Name = "m_mbLoadTransfer";
            this.m_mbLoadTransfer.Picture = MobileTech.Windows.UI.ImageKeys.LoadTransfer;
            this.m_mbLoadTransfer.PictureDisabled = MobileTech.Windows.UI.ImageKeys.LoadTransferDisabled;
            this.m_mbLoadTransfer.PictureFocus = MobileTech.Windows.UI.ImageKeys.LoadTransferFocus;
            this.m_mbLoadTransfer.ShowBorder = false;
            this.m_mbLoadTransfer.ShowFocusBorder = true;
            this.m_mbLoadTransfer.Size = new System.Drawing.Size(114, 68);
            this.m_mbLoadTransfer.TabIndex = 2;
            this.m_mbLoadTransfer.Text = "Load Transfer";
            this.m_mbLoadTransfer.TextShift = false;
            this.m_mbLoadTransfer.TransparentIcon = true;
            this.m_mbLoadTransfer.TransparentImage = true;
            // 
            // m_mbLoad
            // 
            this.m_mbLoad.BackColor = System.Drawing.Color.White;
            this.m_mbLoad.BackDownColor = System.Drawing.Color.Black;
            this.m_mbLoad.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbLoad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbLoad.ForeDownColor = System.Drawing.Color.White;
            this.m_mbLoad.IconMargin = 3;
            this.m_mbLoad.IconShift = false;
            this.m_mbLoad.IconTextSpace = 3;
            this.m_mbLoad.Location = new System.Drawing.Point(4, 4);
            this.m_mbLoad.Name = "m_mbLoad";
            this.m_mbLoad.Picture = MobileTech.Windows.UI.ImageKeys.Load;
            this.m_mbLoad.PictureDisabled = MobileTech.Windows.UI.ImageKeys.LoadDisabled;
            this.m_mbLoad.PictureFocus = MobileTech.Windows.UI.ImageKeys.LoadFocus;
            this.m_mbLoad.ShowBorder = false;
            this.m_mbLoad.ShowFocusBorder = true;
            this.m_mbLoad.Size = new System.Drawing.Size(114, 68);
            this.m_mbLoad.TabIndex = 1;
            this.m_mbLoad.Text = "Load";
            this.m_mbLoad.TextShift = false;
            this.m_mbLoad.TransparentIcon = true;
            this.m_mbLoad.TransparentImage = true;
            this.m_mbLoad.Click += new System.EventHandler(this.OnLoadClick);
            // 
            // m_mbUnload
            // 
            this.m_mbUnload.BackColor = System.Drawing.Color.White;
            this.m_mbUnload.BackDownColor = System.Drawing.Color.Black;
            this.m_mbUnload.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbUnload.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbUnload.ForeDownColor = System.Drawing.Color.White;
            this.m_mbUnload.IconMargin = 3;
            this.m_mbUnload.IconShift = false;
            this.m_mbUnload.IconTextSpace = 3;
            this.m_mbUnload.Location = new System.Drawing.Point(4, 76);
            this.m_mbUnload.Name = "m_mbUnload";
            this.m_mbUnload.Picture = MobileTech.Windows.UI.ImageKeys.Unload;
            this.m_mbUnload.PictureDisabled = MobileTech.Windows.UI.ImageKeys.UnloadDisabled;
            this.m_mbUnload.PictureFocus = MobileTech.Windows.UI.ImageKeys.UnloadFocus;
            this.m_mbUnload.ShowBorder = false;
            this.m_mbUnload.ShowFocusBorder = true;
            this.m_mbUnload.Size = new System.Drawing.Size(114, 68);
            this.m_mbUnload.TabIndex = 3;
            this.m_mbUnload.Text = "Unload";
            this.m_mbUnload.TextShift = false;
            this.m_mbUnload.TransparentIcon = true;
            this.m_mbUnload.TransparentImage = true;
            this.m_mbUnload.Click += new System.EventHandler(this.OnUnloadClick);
            // 
            // m_mbLoadRequest
            // 
            this.m_mbLoadRequest.BackColor = System.Drawing.Color.White;
            this.m_mbLoadRequest.BackDownColor = System.Drawing.Color.Black;
            this.m_mbLoadRequest.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbLoadRequest.Enabled = false;
            this.m_mbLoadRequest.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbLoadRequest.ForeDownColor = System.Drawing.Color.White;
            this.m_mbLoadRequest.IconMargin = 3;
            this.m_mbLoadRequest.IconShift = false;
            this.m_mbLoadRequest.IconTextSpace = 3;
            this.m_mbLoadRequest.Location = new System.Drawing.Point(123, 76);
            this.m_mbLoadRequest.Name = "m_mbLoadRequest";
            this.m_mbLoadRequest.Picture = MobileTech.Windows.UI.ImageKeys.LoadRequest;
            this.m_mbLoadRequest.PictureDisabled = MobileTech.Windows.UI.ImageKeys.LoadRequestDisabled;
            this.m_mbLoadRequest.PictureFocus = MobileTech.Windows.UI.ImageKeys.LoadRequestFocus;
            this.m_mbLoadRequest.ShowBorder = false;
            this.m_mbLoadRequest.ShowFocusBorder = true;
            this.m_mbLoadRequest.Size = new System.Drawing.Size(114, 68);
            this.m_mbLoadRequest.TabIndex = 4;
            this.m_mbLoadRequest.Text = "Load Request";
            this.m_mbLoadRequest.TextShift = false;
            this.m_mbLoadRequest.TransparentIcon = true;
            this.m_mbLoadRequest.TransparentImage = true;
            // 
            // InventoryMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbPrintReport);
            this.Controls.Add(this.m_mbExit);
            this.Controls.Add(this.m_mbReturnToStock);
            this.Controls.Add(this.m_mbLoadTransfer);
            this.Controls.Add(this.m_mbLoad);
            this.Controls.Add(this.m_mbUnload);
            this.Controls.Add(this.m_mbLoadRequest);
            this.Name = "InventoryMenu";
            this.Text = "2000 - Inventory Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_mbLoadRequest;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbUnload;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbLoad;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbLoadTransfer;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbReturnToStock;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbExit;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbPrintReport;
    }
}