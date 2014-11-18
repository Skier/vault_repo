using System.Globalization;
using System.Windows.Forms;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.MainForm
{
    public partial class MainFormView : BaseForm
    {
        public MainFormView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Restoration.NET V19.2";
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {            
            if (e.KeyChar != (char)Keys.Escape)
            {
                base.OnKeyPress(e);
            }
        }
    }
}