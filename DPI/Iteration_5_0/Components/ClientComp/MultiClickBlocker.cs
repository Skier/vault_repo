using System;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls;

namespace DPI.ClientComp
{
	public class MultiClickBlocker
	{
		public static void Block(System.Web.UI.WebControls.ImageButton button, string postBackScript)
		{
			button.Attributes.Add("onClick", 
				"clickedButton=true; this.disabled=true; " + postBackScript + ";");
		}
		public static void Block(Page page, ImageButton btn)
		{
			if (btn.Visible)
				btn.Attributes.Add("onClick", 
					"clickedButton=true; this.disabled=true; " + page.GetPostBackEventReference(btn) + ";");
		}
	}
}