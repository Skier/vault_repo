using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.Components
{
    public class EquipmentQuantityTextEdit : TextEdit
    {
        private const string NULL_TEXT = "0/0/0";

        public EquipmentQuantityTextEdit()
        {
            Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            Properties.Mask.EditMask = "\\d{1,2}/\\d{1,2}/\\d{1,2}";
            Properties.NullText = NULL_TEXT;            
        }

        protected override void OnLeave(EventArgs e)
        {
            if (Text == string.Empty)
                Text = NULL_TEXT;

            base.OnLeave(e);
        }

        public Dictionary<int, int> Quantities
        {
            get
            {
                Dictionary<int, int> result = new Dictionary<int, int>();
                string[] vanQuantities = Text.Split('/');

                for (int i = 1; i <= 3; i++)
                {
                    int quantity = 0;
                    try
                    {
                        quantity = int.Parse(vanQuantities[i - 1]);
                    }
                    catch (Exception) {}

                    result.Add(i, quantity);
                }
                    
                return result;
            }

            set
            {
                if (value.Count == 0)
                {
                    Text = NULL_TEXT;
                    return;
                }                    

                string result = string.Empty;
                foreach (int quantity in value.Values)
                    result += quantity + "/";
                Text = result.TrimEnd('/');
            }
        }
    }
}
