using System.Globalization;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.AddressEdit
{
    public partial class AddressEditView : BaseForm
    {     
        public AddressEditView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Dalworth - Address";
        }
    }
}