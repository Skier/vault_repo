using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Text;

using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.ClientComp
{
	#region ProductCheckBoxRow
	public class ProductCheckBoxRow : TableRow 
	{
		public ProductCheckBoxRow(IProdPrice prod, EventHandler eh, Color back, bool isPostBack) : base() 
		{	
			bool isDisabled = 
				(prod.Locked) || (prod.ProdSelState == ProdSelectionState.Unavailable);
			
			Color forecolor = isDisabled ? Color.Black : Color.Black;
			Color backcolor = isDisabled ? Color.Gainsboro : back;
			
			// Checkbox column
		
			CheckBox checkbox = new CheckBox();

			checkbox.EnableViewState = checkbox.AutoPostBack = checkbox.Enabled  = true;
			checkbox.Checked         = prod.ProdSelState == ProdSelectionState.Selected;
			checkbox.ID              = prod.ProdId.ToString();
			
			checkbox.CheckedChanged += eh;			
			checkbox.Enabled         = !isDisabled;
			checkbox.BackColor       = backcolor;
			checkbox.ForeColor       = forecolor;

			TableCell cell    = new TableCell();

			cell.BackColor = backcolor;
			cell.ForeColor = forecolor;
			cell.Controls.Add(checkbox);

			Cells.Add(cell);

			// Price column	
			cell = new TableCell();
			
			cell.Text = prod.UnitPrice.ToString("C");
			cell.BackColor = backcolor;
			cell.ForeColor = forecolor;
			Cells.Add(cell);

			// Name column	
			cell = new TableCell();
			cell.BackColor = backcolor;
			cell.ForeColor = forecolor;	
			string text = 
				//	prod.BillText; 
				"(" + prod.ProdId.ToString() + ") " + prod.BillText;

			if ((prod.StartServMon == 2) && (prod.UnitPrice > 0m))
				text += "<font color='Chocolate'> (First month free)</font>"; 
			
			if ((prod.StartServMon == 3) && (prod.UnitPrice > 0m))
				text += "<font color='Chocolate'> (Two months free)</font>";
	
			if ((prod.StartServMon == 4) && (prod.UnitPrice > 0m))
				text += "<font color='Chocolate'> (Three months free)</font>";

			if ((prod.StartServMon > 4) && (prod.UnitPrice > 0m))
				text += "<font color='Chocolate'> (" + prod.StartServMon + " months free)</font>";			

			if (ProdInfoCol.GetProd(prod.ProdId).IsExcludedFromTotalL2)
				text += "<font color='Chocolate'> (Not included in Products Total below)</font>"; 

			if (ProdInfoCol.GetProd(prod.ProdId).DisplayUnclickMessage)
				text += "<font color='Chocolate'> (Unclick to select another LD product)</font>";
	
			cell.Text = text;

			if(prod.Description != null)
			{
				cell.Text =
					"<a href='javascript:void(0);' onmouseover='return escape(\""
					+ Regex.Replace(prod.Description, "'", "un") // code for ' &##39;
					+ "\")'>" 
					+ text
					+ "</a>";
			}
			Cells.Add(cell);
		}
	}
	#endregion


	public class ProdButtonRow : TableRow 
	{
		const string BUTTON_GROUP = "ButtonGroup";
	
		public ProdButtonRow(int[] prods, EventHandler eh) : base() 
		{	
			BackColor = Color.LightGray;
			
			// First Column 
			TableCell cell = new TableCell(); 
			cell.Text = "&nbsp;&nbsp;Select a Package:";
			cell.Font.Bold = true;
			Cells.Add(cell);

			//Product columns
			for (int i = 0; i < prods.Length; i++)
			{
				RadioButton button = new RadioButton();
				button.GroupName   = BUTTON_GROUP;
	
				//if (Conn.Env != Const.PROD) // except for production, show product ids 
				// 	button.Text = prods[i].ToString();
			
				button.ID          = prods[i].ToString();
				//button.Checked     = prods[i] == selected;

				button.EnableViewState = true;
				button.AutoPostBack =  false;
				button.CheckedChanged += eh;
				button.Attributes.Add("OnClick", "showlifeline(" + prods[i] + ");");
				
				cell = new TableCell();
				cell.HorizontalAlign = HorizontalAlign.Center;	
				cell.Controls.Add(button);

				Cells.Add(cell);
			}
		}
	}
	

	public class ProdFeatureRow : TableRow 
	{
		const bool bold = true;
		static Color firstCellForeColor = Color.SteelBlue;

		public ProdFeatureRow(string[] text, string indent, Color forecolor, Color backcolor) : base() 
		{
			// First column
			TableCell cell = new TableCell();
			cell.Text = indent + text[0];	
			cell.Font.Bold = true;

			cell.ForeColor = firstCellForeColor;
			cell.BackColor = backcolor;
			cell.HorizontalAlign = HorizontalAlign.Left;

			Cells.Add(cell);
			// Product columns
			for(int i = 1; i < text.Length; i++)
			{
				cell = new TableCell();
				cell.Text = text[i];
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.ForeColor = forecolor;				
				cell.BackColor = backcolor;
				cell.Font.Bold = bold;

				Cells.Add(cell);
			}
		}
	}


	public class ProdPriceMonth1Row : TableRow 
	{
		const bool bold = true;
		static Color firstCellForeColor = Color.SteelBlue; 
		public ProdPriceMonth1Row(int[] packages, IProdPrice[] prods, string indent, Color forecolor, Color backcolor) : base() 
		{
			TableCell cell = new TableCell();

			cell.Text = indent + "First Month Rate";
			cell.ForeColor = Color.SteelBlue;
			cell.BackColor = backcolor;

			Cells.Add(cell);

			for(int i = 0; i < packages.Length; i++)
			{
				cell = new TableCell();
				
				for (int j = 0; j < prods.Length; j++)
					if (packages[i] == prods[j].ProdId)
						cell.Text = prods[j].UnitPrice.ToString("C");
				
				cell.HorizontalAlign = HorizontalAlign.Center;

				cell.ForeColor = forecolor;				
				cell.Font.Bold = bold;
				Cells.Add(cell);
			}
		}
	}


	public class ProdPriceMonth2Row : TableRow 
	{
		const bool bold = true;
		static Color firstCellForeColor = Color.SteelBlue; 		

		public ProdPriceMonth2Row(IKeyVal[] discounts, int[] packages, IProdPrice[] prods, string indent, Color forecolor, Color backcolor) : base() 
		{
			TableCell cell = new TableCell();
			cell.Text      = indent + "Monthly Recurring Rate";
			cell.ForeColor = Color.SteelBlue;
			cell.BackColor = backcolor;

			Cells.Add(cell);

			for(int i = 0; i < packages.Length; i++)
			{
				cell = new TableCell();

				for (int j = 0; j < prods.Length; j++)
					if (packages[i] == prods[j].ProdId)
						cell.Text = GetProdPrice(discounts, prods[j]);
				
				cell.HorizontalAlign = HorizontalAlign.Center;

				cell.ForeColor = forecolor;				
				cell.Font.Bold = bold;
				cell.Font.Size = 12;
				Cells.Add(cell);
			}
		}
		static string GetProdPrice(IKeyVal[] discounts, IProdPrice prod)
		{
			for (int i = 0; i < discounts.Length; i++)
				if (discounts[i].Key == prod.ProdId.ToString())
					return (prod.UnitPrice + Decimal.Parse(discounts[i].Val)).ToString("C");

			return prod.UnitPrice.ToString("C");
		}
	}


	public class ProdHeaderRow : TableRow 
	{
		public ProdHeaderRow(string[] products, Color forecolor, Color backcolor) : base() 
		{
			for (int i = 0; i < products.Length; i++)
			{
				TableCell cell = new TableCell();

				cell.Text            = products[i];
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.ForeColor       = forecolor;
				cell.BackColor       = backcolor;
				cell.Font.Bold       = true;
				
				Cells.Add(cell);
			}
		}
	}


	public class SeparatorRow : TableRow
	{
		public SeparatorRow(Color backcolor, int span) : base()
		{
			TableCell cell = new TableCell();

			cell.ColumnSpan      = span;	
			cell.ForeColor       = backcolor; //Color.Black;
			cell.BackColor       = backcolor;
			cell.Text			 = "s";
			
			Cells.Add(cell);		
		}
	}


	public class HeaderRow : TableRow 
	{
		public HeaderRow(string text, int span, Color backcolor) : base() 
		{
			TableCell cell = new TableCell();
			
			cell.Text            = text;
			cell.ColumnSpan      = span;	
			cell.HorizontalAlign = HorizontalAlign.Center;
			cell.ForeColor       = Color.White;
			cell.BackColor       = backcolor;
			cell.Font.Bold       = true;
			
			Cells.Add(cell);
		}
		public HeaderRow(string text, int span, Color backcolor, Color forecolor) : base() 
		{
			TableCell cell = new TableCell();
			
			cell.Text            = text;
			cell.Font.Size       = 12;
			cell.ColumnSpan      = span;	
			cell.HorizontalAlign = HorizontalAlign.Center;
			cell.ForeColor       = forecolor;
			cell.BackColor       = backcolor;
			cell.Font.Bold       = true;
			
			Cells.Add(cell);
		}
	}

	public class ErrorRow : TableRow
	{
		public ErrorRow(string msg)
		{
			TableCell cell = new TableCell();
			cell.Text      = msg;
			cell.ForeColor = Color.Red;
			cell.HorizontalAlign = HorizontalAlign.Center; 
			//cell.BackColor = Color.Chocolate;;

			Cells.Add(cell);
		}
	}

	public class HeaderRow2 : TableRow 
	{
		public HeaderRow2() : base() 
		{
			BackColor = Color.Chocolate ;
			
			// Package & Features col
			TableCell cell       = new TableCell();
			cell.Text            = "&nbsp;&nbsp;Package and Features Selected";
			cell.HorizontalAlign = HorizontalAlign.Left;
			cell.Font.Bold       = true;
			cell.ForeColor       = Color.White;
			cell.ColumnSpan		 = 2;
			
			Cells.Add(cell);

			// Spacer col?
			//			cell       = new TableCell();
			//			cell.Width = Unit.Pixel(100);
			//			Cells.Add(cell);
			
			// Price col
			cell                 = new TableCell();
			cell.Font.Bold       = true;
			cell.Text            = "Price&nbsp;&nbsp;";
			cell.ForeColor       = Color.White;
			cell.HorizontalAlign = HorizontalAlign.Right;
			Cells.Add(cell);

			// 2 month Price col
			cell                 = new TableCell();
			cell.Font.Bold       = true;
			cell.ColumnSpan		 = 2;
			cell.Text            = "Month 2 Charges&nbsp;&nbsp;";
			cell.ForeColor       = Color.White;
			cell.HorizontalAlign = HorizontalAlign.Right;
			cell.Width			 = Unit.Pixel(120);
			Cells.Add(cell);

			// Second Spacer?
			//			cell       = new TableCell();
			//			cell.Width = Unit.Pixel(15);
			//			Cells.Add(cell);
		}
	}


	public class HeaderRow3 : TableRow 
	{
		public HeaderRow3() : base() 
		{
			BackColor = Color.Chocolate ;
			
			// Package & Features col
			TableCell cell       = new TableCell();
			cell.Text            = "&nbsp;&nbsp;Package and Features Selected";
			cell.HorizontalAlign = HorizontalAlign.Left;
			cell.Font.Bold       = true;
			cell.ForeColor       = Color.White;
			cell.ColumnSpan		 = 2;
			
			Cells.Add(cell);

			cell                 = new TableCell();
			cell.Font.Bold       = true;
			cell.Text            = "Price&nbsp;&nbsp;";
			cell.ForeColor       = Color.White;
			cell.HorizontalAlign = HorizontalAlign.Right;
			Cells.Add(cell);			
		}
	}


	public class ConfHeaderRows
	{	
		const string CSS_CLASS = "05_con_subbold";
		
		public static TableRow[] ConfirmHeaders()
		{
			return new TableRow[] { new ConfHeaderRow(), new GoldHeaderRow() }; 
		}
		class ConfHeaderRow : TableRow
		{
			public ConfHeaderRow() : base()
			{	
	
				TableCell cell      = new TableCell();
				cell.ColumnSpan = 4;				
				cell.BackColor  = Color.White;
				Cells.Add(cell);
			}
		}
		class GoldHeaderRow : TableRow
		{
			public GoldHeaderRow() : base()
			{
				// Product col
				TableCell cell       = new TableCell();	
				cell.ColumnSpan      = 1 ;
				cell.Height			 = 20;
				cell.BackColor		 = Color.Chocolate;
				cell.HorizontalAlign = HorizontalAlign.Left;
				cell.ForeColor = Color.White;
				
				//Label lbl    = new Label();
				cell.CssClass = CSS_CLASS;
				cell.Text     = "&nbsp;&nbsp;Package and Features Selected";

				//cell.Controls.Add(lbl);
				Cells.Add(cell);
				
				// Spacer
				cell  = new TableCell();
				cell.BackColor = Color.Chocolate;
				cell.Width    = Unit.Pixel(15);
				Cells.Add(cell);
				
				// 1 month Price col
				cell                 = new TableCell();
				cell.BackColor       = Color.Chocolate;
				cell.ColumnSpan      = 2 ;
				cell.HorizontalAlign = HorizontalAlign.Right;
				cell.ForeColor = Color.White;
				cell.CssClass = CSS_CLASS;
				cell.Text     = "Price&nbsp;&nbsp;&nbsp;";
				Cells.Add(cell);

				//2 month Price col
				//				cell                 = new TableCell();
				//				cell.BackColor       = Color.Chocolate;
				//				cell.ColumnSpan      = 2 ;
				//				cell.HorizontalAlign = HorizontalAlign.Right;
				//				cell.ForeColor = Color.White;
				//				cell.CssClass = CSS_CLASS;
				//				cell.Text     = "2nd Month Price &nbsp;&nbsp;";
				//				Cells.Add(cell);
			}
		}
	}


	public class ProductRow : TableRow 
	{
		const string CSS_CLASS = "subitems";

		public ProductRow(IProdPrice prod, Color back) : base() 
		{	
			BackColor = back;

			// Name column		
			TableCell cell = new TableCell();
			cell.CssClass        = CSS_CLASS;			
			cell.HorizontalAlign = HorizontalAlign.Left;
			cell.ColumnSpan = 2;

			string text = 
				prod.BillText;
			//	"(" + prod.ProdId.ToString() + "-" + prod.TaxCode + "  $" + prod.UnitPrice.ToString() +")" + prod.BillText;
			//				"(" + prod.ProdId.ToString()  +")" + prod.BillText;			
			//			if ((prod.StartServMon > 1)
			//				&& (prod.UnitPrice > 0m) 
			//				&& (prod.PackageId == 0))
			//					cell.Text += "<font color='Chocolate'> (First month free)</font>"; 

			cell.Text = "&nbsp;&nbsp;" + text;					
			if (prod.PackageId > 0)
				cell.Text = Const.COMP_INDENT + text;
			
			Cells.Add(cell);

			// Spacer col
			//			cell          = new TableCell();		
			//			cell.CssClass = CSS_CLASS;
			//			cell.Width    = Unit.Pixel(100);	
			//			Cells.Add(cell);			
			//			
			// 1st month Price col
			cell                 = new TableCell();
			cell.ForeColor		 = Color.Red;
			cell.CssClass        = CSS_CLASS;
			cell.HorizontalAlign = HorizontalAlign.Right;	
			cell.Text = OrderSummaryProdFormatter.FormatPrice(prod, 1);
			Cells.Add(cell);

			// 2nd month Price col
			cell                 = new TableCell();
			cell.ColumnSpan		 = 2;
			cell.ForeColor		 = Color.SlateGray;
			cell.CssClass        = CSS_CLASS;
			cell.HorizontalAlign = HorizontalAlign.Right;	
			cell.Text = OrderSummaryProdFormatter.FormatPrice(prod, 2);
			Cells.Add(cell);

			// Spacer col
			//			cell          = new TableCell();		
			//			cell.CssClass = CSS_CLASS;	
			//			Cells.Add(cell);
		}
	}


	public class DpiWLProdRow : TableRow 
	{
		const string CSS_CLASS = "subitems";

		public DpiWLProdRow(IWireless_Products prod, Color back) : base() 
		{	
			BackColor = back;

			// Name column		
			TableCell cell = new TableCell();
			cell.CssClass        = CSS_CLASS;			
			cell.HorizontalAlign = HorizontalAlign.Left;
			cell.ColumnSpan = 2;

			cell.Text = "&nbsp;&nbsp;" + prod.Product_name;
			Cells.Add(cell);

			cell                 = new TableCell();
			cell.ForeColor		 = Color.Red;
			cell.CssClass        = CSS_CLASS;
			cell.HorizontalAlign = HorizontalAlign.Right;	
			cell.Text = prod.Price.ToString("C");
			Cells.Add(cell);			
		}
	}

	
	public class ProdMon1Row : TableRow 
	{
		const string CSS_CLASS = "subitems";

		public ProdMon1Row(IProdPrice prod, Color back) : base() 
		{	
			BackColor = back;

			// Name column		
			TableCell cell = new TableCell();
			cell.CssClass        = CSS_CLASS;			
			cell.HorizontalAlign = HorizontalAlign.Left;
			cell.ColumnSpan = 2;

			string text = prod.BillText;

			cell.Text = "&nbsp;&nbsp;" + text;					
			if (prod.PackageId > 0)
				cell.Text = Const.COMP_INDENT + text;
			
			Cells.Add(cell);

			//Spacer col
			cell          = new TableCell();		
			cell.CssClass = CSS_CLASS;	
			Cells.Add(cell);
			
			cell                 = new TableCell();
			cell.ForeColor		 = Color.Red;
			cell.CssClass        = CSS_CLASS;
			cell.HorizontalAlign = HorizontalAlign.Right;	
			cell.Text = OrderSummaryProdFormatter.FormatPrice(prod, 1);
			Cells.Add(cell);
		}
	}


	public class ReprintHeader : TableRow
	{
		public ReprintHeader(string[] text, Color backcolor) : base() 
		{
			for (int i = 0; i < text.Length; i ++)
			{
				TableCell cell = new TableCell();
				cell.Text            = text[i];	
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.ForeColor       = Color.White;
				cell.BackColor       = backcolor;
				cell.Font.Bold       = true;	
				Cells.Add(cell);
			}
		}
	}


	public class ReprintRow : TableRow
	{
		public ReprintRow(EventHandler eh, IOrder order,  Color backColor): base()
		{
			// { "", "Name", "Phone #", "Confirmation #", "Account #", "Date" }
			const string BUTTON_GROUP = "ButtonGroup";
			BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;

			RadioButton button = new RadioButton();
				
			button.GroupName   = BUTTON_GROUP;			
			
			button.ID          = order.Id.ToString() + order.PayInfoId.ToString();

			button.EnableViewState = button.AutoPostBack =  true;
			button.CheckedChanged += eh;
			TableCell c5 = new TableCell();				
			c5.Controls.Add(button);
			Cells.Add(c5);
			
			// Name
			TableCell c10 = new TableCell();
			//		c10.Text = "("+ order.Id.ToString()+ ") " + order.Name;
			c10.Text = order.Name;
			Cells.Add(c10);

			// Type
			TableCell c8 = new TableCell();
			c8.Text = order.OrderType; 
			Cells.Add(c8);

			// Phone #
			TableCell c9 = new TableCell();
			c9.Text = order.Phone;
			Cells.Add(c9);

			// Confirmation #
			TableCell c = new TableCell();
			c.Text = order.ConfNumber;
			Cells.Add(c);

			// Account #
			TableCell c3 = new TableCell();
			c3.Text = order.AccNumber.ToString(); //account number
			Cells.Add(c3);

			// Date
			TableCell c2 = new TableCell();
			c2.Text = order.Date.ToString();
			Cells.Add(c2);

		}
	}


	public class RevVoidHeader : TableRow
	{
		public RevVoidHeader(string[] text, Color backcolor) : base() 
		{
			for (int i = 0; i < text.Length; i ++)
			{
				TableCell cell = new TableCell();
				cell.Text            = text[i];	
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.ForeColor       = Color.White;
				cell.BackColor       = backcolor;
				cell.Font.Bold       = true;	
				Cells.Add(cell);
			}
		}
	}
	public class GenTableRow : TableRow
	{
		public GenTableRow(string text, Color backcolor) : base() 
		{
			TableCell cell = new TableCell();
			cell.Text            = text;	
			cell.HorizontalAlign = HorizontalAlign.Left;
			cell.ForeColor       = Color.Black;
			cell.BackColor       = backcolor;
			cell.Font.Bold       = true;	
			Cells.Add(cell);			
		}
	}
	public class PayMethodRow : TableRow
	{
		public PayMethodRow(System.Web.UI.WebControls.DropDownList ddl) : base() 
		{
			TableCell cell = new TableCell();

			cell.HorizontalAlign = HorizontalAlign.Right;
			cell.ForeColor       = Color.White;
			cell.Controls.Add(ddl);
			Cells.Add(cell);
		}
		DropDownList SetupPayMethodDDL(EventHandler eh, PaymentType[] payMethods, string selected)
		{
			if (payMethods == null)
				throw new ArgumentNullException("Payment types");

			if (payMethods.Length == 0)
				throw new ArgumentNullException("Payment types");
			
			DropDownList ddl = new DropDownList();

			ddl.EnableViewState = ddl.AutoPostBack = true;
			ddl.SelectedIndexChanged += eh;
		
			//ddl.DataSource = payMethods;
			//ddl.DataBind();
		
			for (int i = 0; i < payMethods.Length; i++)
				ddl.Items.Add(payMethods[i].ToString());

			if (selected == null)
				return ddl;

			for (int i = 0; i < ddl.Items.Count; i++)
				if (ddl.Items[i].Value.Trim().ToLower() == selected.Trim().ToLower())
				{
					ddl.SelectedIndex = i;
					break;
				}
			return ddl;
		}
	}

	public class CheckNum : TableRow
	{
		public CheckNum(): base()
		{
			Label lbl = new Label();
			lbl.Width = 90;
			lbl.Text = "Check #:";

			TextBox txtBox = new TextBox();
			txtBox.Width = 220;
			
			
			//BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;

			TableCell cell0 = new TableCell();
			cell0.HorizontalAlign = HorizontalAlign.Right; 	
			cell0.Controls.Add(lbl);
			Cells.Add(cell0);

			TableCell cell1 = new TableCell();
			cell1.HorizontalAlign = HorizontalAlign.Right; 		
			cell1.Controls.Add(txtBox);
			Cells.Add(cell1);
		}		
	}
	public class CheckName : TableRow
	{
		public CheckName(): base()
		{
			Label lbl = new Label();
			lbl.Width = 90;
			lbl.Text = "Check Name:";

			TextBox txtBox = new TextBox();
			txtBox.Width = 220;
			
			
			//BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;

			TableCell cell0 = new TableCell();
			cell0.HorizontalAlign = HorizontalAlign.Right; 	
			cell0.Controls.Add(lbl);
			Cells.Add(cell0);

			TableCell cell1 = new TableCell();
			cell1.HorizontalAlign = HorizontalAlign.Right; 		
			cell1.Controls.Add(txtBox);
			Cells.Add(cell1);
		}
	}
	public class RevVoidRow : TableRow
	{
		public RevVoidRow(EventHandler eh, ILocalTransactionInfo data, VoidTranType tranType, Color backColor): base()
		{
			const string BUTTON_GROUP = "ButtonGroup";
			BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;

			RadioButton button = new RadioButton();
				
			button.GroupName   = BUTTON_GROUP;			
			button.ID          = data.Transaction_Id.ToString();
			button.Attributes.Add("TranType", ((int)tranType).ToString());
			button.EnableViewState = button.AutoPostBack =  true;
			button.CheckedChanged += eh;
			TableCell c5 = new TableCell();				
			c5.Controls.Add(button);
			Cells.Add(c5);

			TableCell c = new TableCell();
			c.Text = data.TrConfirm.ToString();
			Cells.Add(c);

			TableCell c8 = new TableCell();
			c8.Text = data.Transaction_Type_Id.ToString(); 
			Cells.Add(c8);

			TableCell c2 = new TableCell();
			c2.Text = data.PayDate.ToShortDateString();
			Cells.Add(c2);

			TableCell c3 = new TableCell();
			c3.Text = data.AccNumber.ToString();
			Cells.Add(c3);

			TableCell c9 = new TableCell();
			c9.Text = "-";
			if(data.PhNumber != null)
				c9.Text = FormatPhone.Format(data.PhNumber);

			Cells.Add(c9);

			TableCell c6 = new TableCell();
			c6.Text = data.LDAmount.ToString();
			Cells.Add(c6);

			TableCell c7 = new TableCell();
			c7.Text = data.LocalAmount.ToString();
			Cells.Add(c7);
		}
	}
	public class RevIntVoidRow : TableRow
	{
		public RevIntVoidRow(EventHandler eh, IWireless_Transactions data, VoidTranType tranType, Color backColor): base()
		{
			const string BUTTON_GROUP = "ButtonGroup";
			BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;

			RadioButton button = new RadioButton();
				
			button.GroupName   = BUTTON_GROUP;			
			button.ID          = data.Wireless_Transaction_ID.ToString();
			button.Attributes.Add("TranType", ((int)tranType).ToString());
			button.EnableViewState = button.AutoPostBack =  true;
			button.CheckedChanged += eh;
			TableCell c1 = new TableCell();				
			c1.Controls.Add(button);
			Cells.Add(c1);

			TableCell c2 = new TableCell();
			c2.Text = data.Wireless_Transaction_ID.ToString();
			Cells.Add(c2);

			TableCell c3 = new TableCell();
			c3.Text = data.PayDateTime.ToShortDateString();
			Cells.Add(c3);

			TableCell c4 = new TableCell();
			c4.Text = data.Tran_Amount.ToString();
			Cells.Add(c4);

			TableCell c5 = new TableCell();
			c5.Text = data.Pin;
			Cells.Add(c5);

			TableCell c6 = new TableCell();
			c6.Text = data.Supplier_tran;
			Cells.Add(c6);
		}
	}
	//	public class WLVoidRowRcpt : TableRow
	//	{
	//		public WLVoidRowRcpt(IWireless_Transactions data, VoidTranType tranType, Color backColor): base()
	//		{
	//			const string BUTTON_GROUP = "ButtonGroup";
	//			BackColor = backColor;
	//			HorizontalAlign = HorizontalAlign.Center;
	//
	//			RadioButton button = new RadioButton();
	//				
	//			button.GroupName   = BUTTON_GROUP;			
	//			button.ID          = data.Wireless_Transaction_ID.ToString();
	//			button.Attributes.Add("TranType", ((int)tranType).ToString());
	//			button.EnableViewState = button.AutoPostBack =  true;
	//			button.CheckedChanged += eh;
	//			TableCell c1 = new TableCell();				
	//			c1.Controls.Add(button);
	//			Cells.Add(c1);
	//
	//			TableCell c2 = new TableCell();
	//			c2.Text = data.Wireless_Transaction_ID.ToString();
	//			Cells.Add(c2);
	//
	//			TableCell c3 = new TableCell();
	//			c3.Text = data.PayDateTime.ToShortDateString();
	//			Cells.Add(c3);
	//
	//			TableCell c4 = new TableCell();
	//			c4.Text = data.Tran_Amount.ToString();
	//			Cells.Add(c4);
	//
	//			TableCell c5 = new TableCell();
	//			c5.Text = data.Pin;
	//			Cells.Add(c5);
	//
	//			TableCell c6 = new TableCell();
	//			c6.Text = data.Supplier_tran;
	//			Cells.Add(c6);
	//		}
	//	}
	public class DebCardNotRedeem : TableRow
	{
		public DebCardNotRedeem(IMap imap, EventHandler eh, IWireless_Transactions wlXact, 
			Color backColor, int selTran): base()
		{
			BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;

			TableCell cell = new TableCell();	// radio button			
			cell.Controls.Add(MakeWLButton(eh, wlXact, selTran));
			Cells.Add(cell);

			cell = new TableCell();              // Confirm number
			cell.Text = wlXact.TrConfirm.ToString();
			cell.HorizontalAlign = HorizontalAlign.Left;
			Cells.Add(cell);

			cell = new TableCell();             // Type
			cell.Text = GetDCType(imap, wlXact.Wireless_product_ID);
			cell.HorizontalAlign = HorizontalAlign.Left;
			Cells.Add(cell);
			
			cell = new TableCell();             // PIN
			cell.Text = wlXact.Pin.ToString();
			cell.HorizontalAlign = HorizontalAlign.Left;
			Cells.Add(cell);

			cell = new TableCell();            // Pay date / time
			cell.Text = wlXact.PayDateTime.ToString();
			cell.HorizontalAlign = HorizontalAlign.Left;
			Cells.Add(cell);

			cell = new TableCell();            // storecode
			cell.Text = wlXact.StoreCode.ToString();
			cell.HorizontalAlign = HorizontalAlign.Left;
			Cells.Add(cell);
			
			cell = new TableCell();           // tran amount
			cell.Text = wlXact.Tran_Amount.ToString();
			cell.HorizontalAlign = HorizontalAlign.Center;
			Cells.Add(cell);
		}
		string GetDCType(IMap imap, int prod)
		{
			if 	(WirelessTranSvc.GetWirelessProd(imap, prod).Vendor_id == 127) 
				return "Enroll";

			if 	(WirelessTranSvc.GetWirelessProd(imap, prod).Vendor_id == 128)
				return "Reload";

			return "Unknown";
		}
		RadioButton MakeWLButton(EventHandler eh, IWireless_Transactions wlXact, int selTran)
		{	
			const string BUTTON_GROUP = "ButtonGroup";
			RadioButton button = new RadioButton();

			button.GroupName        = BUTTON_GROUP;			
			button.ID               = wlXact.Wireless_Transaction_ID.ToString();
			button.CheckedChanged  += eh;

			button.Checked         = wlXact.Wireless_Transaction_ID == selTran;
			button.EnableViewState = button.AutoPostBack =  true;
			return button;
		}
	}

	public class RecurringCustAcctInfo : TableRow
	{
		public RecurringCustAcctInfo(EventHandler eh, ICustomerRecurringPayment data, 
			Color backColor, int selAcct): base()
		{
			BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;

			TableCell cell = new TableCell();	// radio button			
			cell.Controls.Add(MakeCustInfoButton(eh, data, selAcct));
			Cells.Add(cell);

			cell = new TableCell();             // Payment Type
			cell.Text = ShowPaymentType((PaymentType)data.AccountTypeId);
			cell.HorizontalAlign = HorizontalAlign.Center;
			Cells.Add(cell);
			
			cell = new TableCell();             // Account Number
			cell.Text = "******** - " + data.BAccNumber.Substring(data.BAccNumber.Length - 4);
			cell.HorizontalAlign = HorizontalAlign.Center;
			Cells.Add(cell);

			cell = new TableCell();            // Priority
			cell.Text = data.Priority.ToString();
			cell.HorizontalAlign = HorizontalAlign.Center;
			Cells.Add(cell);

			cell = new TableCell();            // Status
			cell.Text = ShowStatus(data.Active);
			cell.HorizontalAlign = HorizontalAlign.Center;
			Cells.Add(cell);			
		}
		string ShowStatus(bool active)
		{
			if (active)
				return "Active";

			return "Inactive";
		}
		string ShowPaymentType(PaymentType pType)
		{
			switch (pType)
			{
				case PaymentType.Cash :
					return "Cash";

				case PaymentType.Check :
					return "Check";

				case PaymentType.Credit :
					return "Credit/Debit";

				case PaymentType.Debit :
					return "Credit/Debit";
				
				default :
					return pType.ToString();
			}
		}
		RadioButton MakeCustInfoButton(EventHandler eh, ICustomerRecurringPayment data, int selAcct)
		{	
			const string BUTTON_GROUP = "ButtonGroup";
			RadioButton button = new RadioButton();

			button.GroupName        = BUTTON_GROUP;			
			button.ID               = data.Id.ToString();
			button.CheckedChanged  += eh;

			button.Checked         = data.Id == selAcct;
			//button.EnableViewState = button.AutoPostBack =  true;
			return button;
		}
	}

	public class MonthChartHeader : TableRow
	{
		public MonthChartHeader(int months, Color backcolor) : base() 
		{
			TableCell cell1 = new TableCell();
			cell1.Text            = "&nbsp;&nbsp;Product Name";	
			cell1.HorizontalAlign = HorizontalAlign.Left;
			cell1.ForeColor       = Color.White;
			cell1.BackColor       = backcolor;
			cell1.Font.Bold       = true;	
			Cells.Add(cell1);

			for (int i = 1; i < months + 1; i ++)
			{
				TableCell cell = new TableCell();
				cell.Text            = "Mo." + i.ToString() ;	
				cell.HorizontalAlign = HorizontalAlign.Center;
				cell.ForeColor       = Color.White;
				cell.BackColor       = backcolor;
				cell.Font.Bold       = true;	
				Cells.Add(cell);
			}
		}
	}


	public class MonthChartRow : TableRow
	{
		public MonthChartRow(string[] matrix ,Color backColor, int fontSize, bool bolded): base()
		{

			TableCell cell = new TableCell(); // row title
			cell.Font.Bold = true;
			cell.BackColor = backColor;
			cell.HorizontalAlign = HorizontalAlign.Left;
			cell.Text = "&nbsp;&nbsp;" + matrix[0];
			Cells.Add(cell);

			for (int i = 1; i < matrix.Length ; i ++)
			{
				TableCell c = new TableCell();

				c.Font.Size = fontSize;
				c.BackColor = backColor;
				c.Font.Bold = bolded ;
				c.Text = matrix[i];
				c.HorizontalAlign = HorizontalAlign.Center;
				Cells.Add(c);
			}
		}
	}


	public class MonthChartRowTax : TableRow
	{
		public MonthChartRowTax(string[] matrix, string[] taxDescr, Color backColor, bool bolded, int fontSize): base()
		{
			BackColor = backColor;
			HorizontalAlign = HorizontalAlign.Center;
			
			// first col - title
			TableCell cell = new TableCell();
			cell.Font.Size = fontSize;
			cell.HorizontalAlign = HorizontalAlign.Left;	
			cell.Font.Bold = bolded ;
			cell.Text = "&nbsp;&nbsp;" + matrix[0];
			Cells.Add(cell);

			// for each tax col
			for (int x = 1; x < matrix.Length; x++) // for each cell 
			{
				TableCell c = new TableCell();
				
				c.Font.Size = fontSize;
				c.HorizontalAlign = HorizontalAlign.Center;	
				c.Font.Bold = bolded ;
				c.Text = MouseOver( taxDescr, matrix[x]);	
				Cells.Add(c);
			}
		}
		string MouseOver(string[] taxDesc, string amount)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<a href='javascript:void(0);' onmouseover='return escape(\"");
				
			for (int j = 0; j < taxDesc.Length; j++) // for each tax descr within the cell
				sb.Append(taxDesc[j] +  "<BR>");

			sb.Append("\")'>");
			sb.Append(amount);
			sb.Append( "</a>");
			string x =	 sb.ToString();
			return sb.ToString();

		}
	}


	public class StatsHeaderRow : TableRow
	{
		public StatsHeaderRow(string title) : base()
		{
			HorizontalAlign = HorizontalAlign.Center;

			Cells.Add(StatsCell.MakeCell("&nbsp;&nbsp;Rank", Color.Chocolate, Color.White));
			Cells.Add(StatsCell.MakeCell("Store Number", Color.Chocolate, Color.White));
			Cells.Add(StatsCell.MakeCell(title, Color.Chocolate, Color.White));
		}
	}
	public class ScheduleInstRow : TableRow
	{
		public ScheduleInstRow(string frame) : base()
		{
			Cells.Add(new ScheduleInstCell(frame));
		}
		public ScheduleInstRow() : base()
		{
			Cells.Add(new ScheduleInstCell());
		}
	}


	public class StatsRow : TableRow
	{
		public StatsRow(bool alt, string storeNumber, bool thisStore, string rank, string attr )
		{
			HorizontalAlign = HorizontalAlign.Center;

			Color colorWhite  = alt ? Color.WhiteSmoke : Color.White;
			if(thisStore)
				colorWhite = Color.Yellow;

			Cells.Add(StatsCell.MakeCell(rank.ToString(), colorWhite, Color.Black));
			Cells.Add(StatsCell.MakeCell(storeNumber , colorWhite, Color.Black));
			Cells.Add(StatsCell.MakeCell(attr, colorWhite, Color.Black));
		}
	}


	public class StatsCell
	{
		public static TableCell MakeCell(string text, Color back, Color fontColor)
		{	
			TableCell cell = new TableCell();

			cell.BackColor = back;
			cell.ForeColor = fontColor;
			cell.Text = text;
			
			return cell;
		}
	}
	public class ScheduleInstCell : TableCell
	{
		public ScheduleInstCell()
		{
			Width = 670;
			ColumnSpan = 5;
			
		}
		public ScheduleInstCell(string frame)
		{
			Width = 670;
			ColumnSpan = 5;
			Text = frame;//@"<iframe src='http://fssdev.servicepower.com/AW/LoginAction.do?fromRW=Y&loginName=dvd00001&loginPassword=dvdtlaad' width='663' height='650' align='left' scrolling='yes'>";

		}
	}
}