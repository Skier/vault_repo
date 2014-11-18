using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Dpi.Central.Web.Account.Controls
{
    public class ControlIDConverter : StringConverter
    {
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context != null && context.Container == null) {
                Object[] controls = GetControls2(context.Container);
                if (controls != null && controls.Length > 0) {
                    return new StandardValuesCollection(controls);
                }
            }
            return null;
        }

        private object[] GetControls2(IContainer container)
        {
            ComponentCollection components = container.Components;
            ArrayList controls = new ArrayList();
            foreach (IComponent component in components) {
                if (component is Control) {
                    Control control = (Control) component;
                    if ((control.ID != null) && (control.ID.Length != 0)) {
                        /*ValidationPropertyAttribute attribute1 =
                            (ValidationPropertyAttribute)
                            TypeDescriptor.GetAttributes(control)[typeof (ValidationPropertyAttribute)];
                        if ((attribute1 != null) && (attribute1.Name != null)) {*/
                            controls.Add(string.Copy(control.ID));
                        //}
                    }
                }
            }
            controls.Sort(Comparer.Default);
            return controls.ToArray();
        }

        private object[] GetControls(IContainer container)
        {
            ArrayList controls = new ArrayList();
            foreach (IComponent component in container.Components) {
                Control control = component as Control;
                if (IsAllowedControl(control)) {
                    controls.Add(control.ID);
                }
            }
            controls.Sort(Comparer.Default);
            return controls.ToArray();
        }

        private bool IsAllowedControl(Control control)
        {
            return control != null && !(control is Page) && (control.ID != null) && (control.ID.Length > 0);
        }
    }
}