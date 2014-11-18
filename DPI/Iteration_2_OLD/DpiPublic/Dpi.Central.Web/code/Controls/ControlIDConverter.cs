using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Dpi.Central.Web.Controls
{
    public class ControlIDConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
            return false;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
            if ((context == null) || (context.Container == null)) {
                return null;
            }
            Object[] serverControls = GetControls(context.Container);
            if (serverControls != null) {
                return new StandardValuesCollection(serverControls);
            }
            return null;
        }

        private object[] GetControls(IContainer container) {
            ArrayList availableControls = new ArrayList();
            foreach (IComponent component in container.Components) {
                Control serverControl = component as Control;
                if (IsAllowedControl(serverControl)) {
                    availableControls.Add(serverControl.ID);
                }
            }
            availableControls.Sort(Comparer.Default);
            return availableControls.ToArray();
        }

        private bool IsAllowedControl(Control control) {
            return control != null && !(control is Page) && (control.ID != null) && (control.ID.Length > 0);
        }
    }
}