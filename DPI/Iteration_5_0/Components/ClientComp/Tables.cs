using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

using DPI.Components;
using DPI.Interfaces;
using DPI.Services;
using DPI.Components.EPSolutions;

namespace DPI.ClientComp
{
	#region TableProdL1
	public class TableProdL1 : Table
	{
		public TableProdL1(IKeyVal[] discounts, IProdPrice[] packages, string[][] matrix) : base()
		{
			InitTable(discounts, packages, matrix, Const.PrintWidth);
		}
		public TableProdL1(IKeyVal[] discounts, IProdPrice[] packages, string[][] matrix, EventHandler eh) : base()
		{
			InitTable(discounts, packages, matrix, Const.DisplWidth);
			Rows.Add(new ProdButtonRow(GetProdIds(matrix[0]), eh));	
		}
		void InitTable(IKeyVal[] discounts, IProdPrice[] packages, string[][] matrix, int tblWidth)
		{
			BorderWidth = 1;
			BorderColor = Color.Gainsboro;
			GridLines   = GridLines.Horizontal;
			CellPadding = 0;
			CellSpacing = 0;
			Width       = tblWidth;
			Font.Size   = 10;			
			
			Color rowBackDefault = Color.WhiteSmoke;
			Color rowBackAlt     = Color.White;
			const string indent  = "&nbsp;&nbsp;";
			bool alt             = false;
		
			Rows.Add(new ProdHeaderRow(matrix[1], Color.White, Color.Chocolate));
			Rows.Add(new ProdPriceMonth2Row(discounts, GetProdIds(matrix[0]),packages, indent, Color.Chocolate, rowBackDefault));
			
			for (int i = 2; i < matrix.Length; i++)
			{
				Rows.Add(new ProdFeatureRow(matrix[i], indent, Color.Red, alt? rowBackAlt : rowBackDefault));
				alt = !alt;
			}
	
			Rows.Add(new ProdPriceMonth1Row(GetProdIds(matrix[0]),packages, indent, Color.DarkGray, rowBackDefault)); 
		}
		int GetSelected(IProdPrice[] packages)
		{
			for (int i = 0; i < packages.Length; i++)
				if (packages[i].ProdSelState == ProdSelectionState.Selected)
					return packages[i].ProdId;

			throw new ApplicationException("No selected top level product found");
		}
		int[] GetProdIds(string[] packages)
		{
			int[] ints = new int[packages.Length - 1]; // skip first
			for (int i = 0; i < ints.Length; i++)
				ints[i] = int.Parse(packages[i + 1]);

			return ints;
		}
	}
	#endregion

	#region TableProdL2
	public class TableProdL2 : Table
	{
		const int cols = 3;
		const string LOCAL_SERVICE = "Local Service";
		const string PROMOTIONS = "Promotions";
		const int width = 642; 

		public TableProdL2(IProdPrice[] prods, EventHandler eh, bool isPostBack) : base()
		{
			string prodType = "";
			bool alt = false;
			
			BorderWidth = 1;
			BorderColor = Color.Gainsboro;
			GridLines = GridLines.Horizontal ;
			CellPadding = 0;
			CellSpacing = 0;
			Width = width;
			
			bool insertSeparator = false;
			for (int i = 0; i < prods.Length; i++)
			{
				if (prodType != prods[i].ProdType)
				{
					
					string text = prodType = prods[i].ProdType;
					Color backcolor = Color.DarkGray;
 

					if (prods[i].ProdType == LOCAL_SERVICE)
					{
						text = LOCAL_SERVICE + " Selected";
						backcolor = Color.Chocolate;
					}

					if (prods[i].ProdType == PROMOTIONS)
						Rows.Add(new HeaderRow("<img src='images/current_pomos.gif' border='0'>", cols, Color.Orange, Color.White));
					else
						Rows.Add(new HeaderRow(text, cols, backcolor));	

					alt = false;
				}
				
				Color back = Color.WhiteSmoke;
				if (alt)
					back = Color.White;

				if (prods[i].StartServMon > 1) 
					if (prods[i].OrdSumryStartMon2 == Const.ORD_Sumry_SUPPRESS)
						continue;

				Rows.Add(new ProductCheckBoxRow(prods[i], eh, back, isPostBack));
				
				if (insertSeparator)
				{
					Rows.Add(new SeparatorRow(Color.White, cols));
					insertSeparator = false;
				}
				alt = !alt;
			}
		}
	}
	#endregion

	#region TableProdSum
	public class ErrorTable : Table
	{
		public ErrorTable(ArrayList errors)
		{
			BorderWidth = 0;
			CellPadding = 0;
			CellSpacing=0;
			//BorderColor = Color.Gainsboro;
			//GridLines = GridLines.Both;
			Font.Size = 10;
			Font.Bold = true ;
			Width = Unit.Percentage(97);

			for (int i = 0; i < errors.Count; i++)
				Rows.Add(new ErrorRow((string)errors[i]));
		}
		public ErrorTable(string error)
		{
			BorderWidth = 0;
			CellPadding = 0;
			CellSpacing=0;
			//BorderColor = Color.Gainsboro;
			//GridLines = GridLines.Both;
			Font.Size = 10;
			Font.Bold = true ;
			Width = Unit.Percentage(97);

			Rows.Add(new ErrorRow(error));
		}
	}
	public class TableProdSum : Table
	{
		public TableProdSum(IProdPrice[] prods) : base()
		{
			BorderWidth = 1;
			CellPadding = 0;
			CellSpacing=0;
			BorderColor = Color.Gainsboro;
			GridLines = GridLines.Both;
			Font.Size = 10;
			Font.Bold = true ;
			Width = Unit.Percentage(97);
			bool alt = false;
			
			Rows.Add(new HeaderRow2());

			for (int i = 0; i < prods.Length; i++)
			{
				if ((prods[i].StartServMon > 1) && (prods[i].OrdSumryStartMon2 == Const.ORD_Sumry_SUPPRESS))
					continue;
				
				Rows.Add(new ProductRow(prods[i], alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}
	}

    public class ProductSummaryTable : Table
    {
        #region Table building event helper classes

        public delegate void RowCreatedEventHandler(object sender, RowCreatedEventArgs e);

        public class RowCreatedEventArgs : EventArgs 
        {
            private ProductSummaryTableRow _row;

            public RowCreatedEventArgs(ProductSummaryTableRow row) : base()
            {
                _row = row;   
            }

            public ProductSummaryTableRow Row
            {
                get { return _row; }
            }
        }

        #endregion

        private event RowCreatedEventHandler RowCreated;

        public ProductSummaryTable(IProdPrice[] products, RowCreatedEventHandler onRawCreatedHandler) : base()
        {
            BorderWidth = 1;
            CellPadding = 0;
            CellSpacing = 0;
            BorderColor = Color.Gainsboro;
            GridLines = GridLines.Both;
            Font.Size = 10;
            Font.Bold = true;
            Width = Unit.Percentage(100);

            RowCreated += onRawCreatedHandler;

            Rows.Add(new ProductSummaryTableHeader());

            foreach (IProdPrice product in products) {
                if (product.StartServMon > 1 && product.OrdSumryStartMon2 == Const.ORD_Sumry_SUPPRESS) {
                    continue;
                }

                ProductSummaryTableRow row = new ProductSummaryTableRow(product, Rows.Count % 2 == 1 ? Color.WhiteSmoke : Color.White);
                Rows.Add(row);
                OnRowCreated(new RowCreatedEventArgs(row));
            }
        }

        private void OnRowCreated(RowCreatedEventArgs e)
        {
            if (RowCreated != null) {
                RowCreated(this, e);
            }
        }
    }

    #endregion

	#region TableDpiWLOrderSum
	public class TableDpiWLOrderSum : Table
	{
		public TableDpiWLOrderSum(IWireless_Products[] prods) : base()
		{
			BorderWidth = 1;
			CellPadding = 0;
			CellSpacing=0;
			BorderColor = Color.Gainsboro;
			GridLines = GridLines.Both;
			Font.Size = 10;
			Font.Bold = true ;
			Width = Unit.Percentage(97);
			bool alt = false;
			
			Rows.Add(new HeaderRow3());

			for (int i = 0; i < prods.Length; i++)
			{
				Rows.Add(new DpiWLProdRow(prods[i], alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}
	}
	#endregion

        #region TableDpiEngOrderSum
	public class TableDpiEngOrderSum : Table
	{
		public TableDpiEngOrderSum(IEnergyItems engItems) : base()
		{
			BorderWidth = 1;
			CellPadding = 0;
			CellSpacing=0;
			BorderColor = Color.Gainsboro;
			GridLines = GridLines.Both;
			Font.Size = 10;
			Font.Bold = true ;
			Width = Unit.Percentage(97);
			bool alt = false;
			
			Rows.Add(new HeaderDpiEng());

			for (int i = 0; i < engItems.Items.Length; i++)
			{
				Rows.Add(new DpiEngOrdSumRow(engItems.Items[i].Key, engItems.Items[i].Val, alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}

//			Rows.Add(new DpiEngOrdSumRow("Estimated Usage", quote.EstimatedUsage.ToString(), Color.White));
//			Rows.Add(new DpiEngOrdSumRow("Rate Per KWH", quote.RatePerKwh.ToString("C"), Color.WhiteSmoke));
//			
//			for (int i = 0; i < quote.EventCharges.Length; i++)
//			{
//				Rows.Add(new DpiEngOrdSumRow(quote.EventCharges[i].ChargeDescription, quote.EventCharges[i].ChargeAmount.ToString("C"), alt ? Color.WhiteSmoke : Color.White));
//				alt = ! alt;
//			}
//			for (int i = 0; i < quote.NonRecurringCharges.Length; i++)
//			{
//				Rows.Add(new DpiEngOrdSumRow(quote.NonRecurringCharges[i].ChargeDescription, quote.NonRecurringCharges[i].ChargeAmount.ToString("C"), alt ? Color.WhiteSmoke : Color.White));
//				alt = ! alt;
//			}
//			for (int i = 0; i < quote.RecurringCharges.Length; i++)
//			{
//				Rows.Add(new DpiEngOrdSumRow(quote.RecurringCharges[i].ChargeDescription, quote.RecurringCharges[i].ChargeAmount.ToString("C"), alt ? Color.WhiteSmoke : Color.White));
//				alt = ! alt;
//			}			
		}
	}
	#endregion

	#region TableProdOrdConfirm
	public class TableProdOrdConfirm : Table
	{
		public TableProdOrdConfirm(IProdPrice[] prods) : base()
		{
			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(0);
			Width=Unit.Percentage(100);
			bool alt = false;
	
			Rows.AddRange(ConfHeaderRows.ConfirmHeaders());
	
			for (int i = 0; i < prods.Length; i++)
			{
				if ((prods[i].StartServMon > 1)
					&& (prods[i].PackageId == 0))
					continue;

				Rows.Add(new ProdMon1Row(prods[i], alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}
	}
	#endregion

	#region TableTransVoid
	public class TableTransVoid : Table
	{
		public TableTransVoid( EventHandler eh, ILocalTransactionInfo[] data, VoidTranType tranType) : base()
		{
			string[] headers = { "", "Transaction #", "Payment Type", "Date", "Account #", "Phone", "LD Amount", "Local Amount" }; // these are the headers for RevVoid
			Rows.Add(new RevVoidHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			bool alt = false;

			for (int i = 0; i < data.Length; i++)
			{
				Rows.Add(new RevVoidRow(eh, data[i], tranType, alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}

	}
	#endregion

	#region TableAddressList
	public class TableAddressList : Table
	{
		public TableAddressList(ServiceLocation[] data) : base()
		{
			string[] headers = { "ESI ID", "Address", "City", "State", "Zip" }; // these are the headers for RevVoid
			Rows.Add(new RevVoidHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			bool alt = false;

			for (int i = 0; i < data.Length; i++)
			{
				Rows.Add(new AddressListRow(data[i], alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}

	}
	#endregion

	#region TableCustList
	public class TableCustList : Table
	{
		public TableCustList(IEnergy_CustData[] data) : base()
		{
			string[] headers = { "Account Number", "Name", "Full Address", "Contact #" }; 
			Rows.Add(new RevVoidHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			bool alt = false;

			for (int i = 0; i < data.Length; i++)
			{
				Rows.Add(new CustListRow(data[i], alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}

	}
	#endregion

	#region TableIntTransVoid
	public class TableIntTransVoid : Table
	{
		public TableIntTransVoid( EventHandler eh, IWireless_Transactions[] data, VoidTranType tranType) : base()
		{
			string[] headers = { "", "Transaction #", "Date", "Tran Amount", "Pin Number", "Supplier Tran #" }; // these are the headers for RevVoid
			Rows.Add(new RevVoidHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			bool alt = false;

			for (int i = 0; i < data.Length; i++)
			{
				Rows.Add(new RevIntVoidRow(eh, data[i], tranType, alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}

	}

	#endregion

	#region TableWLVoidRcpt
	public class TableWLVoidRcpt : Table
	{
		public TableWLVoidRcpt(IWireless_Transactions data, VoidTranType tranType) : base()
		{
			string[] headers = { "Voided Transaction" }; // Header
			//string[] headers = { "", "Transaction #", "Date", "Tran Amount", "Pin Number", "Supplier Tran #" }; // these are the headers for RevVoid
			Rows.Add(new RevVoidHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			
			Rows.Add(new GenTableRow("Transaction Number    : " + data.Wireless_Transaction_ID.ToString(), Color.WhiteSmoke));
			Rows.Add(new GenTableRow("Void Date             : " + DateTime.Now.ToLongDateString(), Color.White));
			Rows.Add(new GenTableRow("Transaction Amount    : " + data.Tran_Amount.ToString(), Color.WhiteSmoke));
			Rows.Add(new GenTableRow("Pin Number            : " + data.Pin, Color.White));
			Rows.Add(new GenTableRow("Supplier Tran Number  : " + data.Supplier_tran, Color.WhiteSmoke));			
		}

	}

	#endregion

	#region TableDebCardNotRedeemed
	public class TableDebCardNotRedeemed : Table
	{
		public TableDebCardNotRedeemed(IMap imap, EventHandler eh, IWireless_Transactions[] data, int selTran) : base()
		{
			string[] headers = {"", "Confirm #", "Type", "PIN #", "Date", "Store Code",  "Amount" };
			//, "Load Amount", "Fee Amount" }; // these are the headers
			
			Rows.Add(new RevVoidHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			bool alt = false;

			//			if (data.Length ==0)
			//               "No Debit Card Redeemable transactions found.";

			for (int i = 0; i < data.Length; i++)
			{
				Rows.Add(new DebCardNotRedeem(imap, eh, data[i], alt ? Color.WhiteSmoke : Color.White, selTran));
				alt = ! alt;
			}
		}
	}
	#endregion

	#region TableRecurringCustInfo
	public class TableRecurringCustInfo : Table
	{
		public TableRecurringCustInfo(EventHandler eh, ICustomerRecurringPayment[] data, int selAcct) : base()
		{
			string[] headers = {"Selection", "Type", "Account Number", "Priority", "Status" };
			//, "Load Amount", "Fee Amount" }; // these are the headers
			
			Rows.Add(new RevVoidHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			bool alt = false;

			//			if (data.Length ==0)
			//               "No Debit Card Redeemable transactions found.";

			for (int i = 0; i < data.Length; i++)
			{
				Rows.Add(new RecurringCustAcctInfo(eh, data[i], alt ? Color.WhiteSmoke : Color.White, selAcct));
				alt = ! alt;
			}
		}
	}

	#endregion

	#region PaymentMethod
	public class TablePaymentMethod : Table
	{
		public TablePaymentMethod(System.Web.UI.WebControls.DropDownList ddl) : base()
		{
			Rows.Add(new PayMethodRow(ddl));
			
			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;


			//			if (selected.Trim().ToLower() == "check")
			//			{
			//				Rows.Add(new CheckNum());
			//				Rows.Add(new CheckName());
			//			}
		}
	}
	#endregion

	#region ReprintReceipt
	public class ReprintReceipt : Table
	{
		public ReprintReceipt( EventHandler eh, IOrder[] data) : base()
		{
			string[] headers = { "", "Name", "Type", "Phone #", "Confirmation #", "Account #", "Date" }; // these are the headers for RevVoid
			Rows.Add(new ReprintHeader( headers, Color.Chocolate ));

			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(97);
			GridLines = GridLines.Horizontal;
			bool alt = false;

			for (int i = 0; i < data.Length; i++)
			{
				Rows.Add(new ReprintRow(eh, data[i], alt ? Color.WhiteSmoke : Color.White));
				alt = ! alt;
			}
		}
	}
	#endregion

    #region TaxDetails

    public class TaxDetailsTable : Table
    {
        public TaxDetailsTable(string[] rows, int month)
        {
            CellPadding = 2;
            CellSpacing = 0;
            BorderWidth = Unit.Point(1);
            Width=Unit.Percentage(100);
            GridLines = GridLines.Vertical;

            Rows.Add(new TaxDetailsHeader(month, Color.Chocolate));

            Array.Sort(rows);
			
            for (int i = 0; i < rows.Length; i++) {
                Rows.Add(new TaxDetailsRow(rows[i], i%2 == 0 ? Color.WhiteSmoke : Color.White));
            }
        }
    }

    #endregion

    #region TableMonthChart
	public class TableMonthChart : Table
	{
		public TableMonthChart(IOrderSum osum, int numMonths) : base()
		{
			if (osum == null)
				throw new ArgumentNullException("Order summary is null");

			if (numMonths < 1)
				throw new ArgumentException("Number of months is less than 1");
		
			CellPadding = 2;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Percentage(100);
			GridLines = GridLines.Vertical;

			bool alt = false;
			int fontSize = 8;
			int TAXROW = 2; // tax row from the bottom

			Rows.Add(new MonthChartHeader(numMonths, Color.Chocolate));

			string[][] matrix = osum.GetMonthlySummary(numMonths);
			
			for ( int i = 0; i < matrix.Length; i++)
			{
				string[] taxes = osum.GetSumTaxDesc(i);
			
				if (i == (matrix.Length - TAXROW))
				{
					Rows.Add(new MonthChartRowTax(matrix[i], taxes,   alt ? Color.WhiteSmoke : Color.White, false, fontSize));
				}
				else if ( i == matrix.Length - 1)
					Rows.Add(new MonthChartRow(matrix[i], Color.Wheat , fontSize + 2, true)); // last row with totals
				else
					Rows.Add(new MonthChartRow(matrix[i], alt ? Color.WhiteSmoke : Color.White , fontSize, false));



				alt = ! alt;

			}
		}
	}
    #endregion
	
	#region TableStats
	public class TableStats : Table
	{
		public TableStats(string[] attrs, int[] ranks, string[] storeNums,  string storeNumber, string title) : base()
		{
			ForeColor = Color.Black;
			HorizontalAlign = HorizontalAlign.Center;	
			CellPadding = 0;
			CellSpacing = 0;
			BorderWidth = Unit.Point(1);
			Width=Unit.Pixel(400);
			GridLines = GridLines.Horizontal;
		
			Rows.Add(new StatsHeaderRow(title));
			bool alt = false;

			for (int i = 0 ; i < attrs.Length ; i ++)
			{
				if (ranks[i] == 0)
					break;

				Rows.Add(new StatsRow(alt, storeNums[i], storeNums[i] == storeNumber, ranks[i].ToString(),  attrs[i])); 
				alt = !alt;
			}
		}
	}
	#endregion
	
	#region TableScheduleInstallation
		public class TableScheduleInstallation : Table
		{
			public TableScheduleInstallation(string frame) : base()
			{
				//ForeColor = Color.Black;
				Height = 62;
				HorizontalAlign = HorizontalAlign.Left;
				CellPadding = 0;
				CellSpacing = 0;
				BorderWidth = 0;//Unit.Point(0);
				Width=680;//Unit.Pixel(400);
				//Style = "WIDTH: 680px; HEIGHT: 62px";
				GridLines = GridLines.Horizontal;
		
				Rows.Add(new ScheduleInstRow());
				Rows.Add(new ScheduleInstRow(frame));
				Rows.Add(new ScheduleInstRow());				
			}
		}
			
		#endregion

}