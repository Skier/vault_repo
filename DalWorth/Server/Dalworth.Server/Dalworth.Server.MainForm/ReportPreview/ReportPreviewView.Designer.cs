namespace Dalworth.Server.MainForm.ReportPreview
{
    partial class ReportPreviewView
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
            this.m_reportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // m_reportViewer
            // 
            this.m_reportViewer.ActiveViewIndex = -1;
            this.m_reportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_reportViewer.Location = new System.Drawing.Point(0, 0);
            this.m_reportViewer.Name = "m_reportViewer";
            this.m_reportViewer.SelectionFormula = "";
            this.m_reportViewer.Size = new System.Drawing.Size(1108, 707);
            this.m_reportViewer.TabIndex = 0;
            this.m_reportViewer.ViewTimeSelectionFormula = "";
            // 
            // ReportPreviewView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 707);
            this.Controls.Add(this.m_reportViewer);
            this.Name = "ReportPreviewView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReportPreview";
            this.ResumeLayout(false);

        }

        #endregion

        internal CrystalDecisions.Windows.Forms.CrystalReportViewer m_reportViewer;
    }
}