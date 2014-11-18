using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class FeaturesMatrix
	{	
		private static ArrayList aFeaturesCSV;
		RadioButton[]  rBtns;
		ArrayList eMsg = new ArrayList();
		public  FeaturesMatrix()
		{
		}
		public string[] ErrorMsg
		{
			get
			{
				if (eMsg.Count == 0)
					return null;
				string[] msg = new string[eMsg.Count];
				eMsg.CopyTo(msg);
				return msg;
			}
		}			

		public RadioButton[] RButns
		{
			get{return rBtns;}
		}			
		public Table  getFeaturesGrid(IProdPrice[] ProdsPrices,  IProdPrice selected)
		{ 
			Table tbl=null;
			try
			{			
				int cols=0; //  added because prods may not be in CSV file
				TableRow tr;
				TableHeaderCell th;
				TableCell td;
				tbl = new Table();

				
				if(aFeaturesCSV==null)  // singleton
					aFeaturesCSV = loadFromTabDelimFile(); 

				string[] csvHeaderIDsRow = (string[])aFeaturesCSV[0];
				string[] csvHeaderLabelsRow = (string[])aFeaturesCSV[1];
				
				//tbl.CssClass = "Level1Class";
				tbl.Width= 650;
				tbl.BorderWidth=1;
				tbl.CellPadding=0;
				tbl.CellSpacing=0;
				tbl.GridLines = GridLines.Horizontal;
				tbl.Font.Size = 10;
				tbl.Font.Bold = true ;
				
				#region add header labels
				tr = new TableRow();
				tr.BackColor = Color.Chocolate ;
				th = new TableHeaderCell();
				th.Text="Products";
				th.ForeColor = Color.White;
				th.Width=Unit.Pixel(660);
				tr.Cells.Add(th);
				bool lFoundProd=false;
				foreach(IProdPrice ProdPriceItem in ProdsPrices)
				{
					lFoundProd=false;
					//for(int i=1; i< csvHeaderIDsRow.Length; i++)
					for(int i=csvHeaderIDsRow.Length-1; i>0; i--)
						{						
						if (int.Parse(csvHeaderIDsRow[i])==ProdPriceItem.ProdId)
						{
							lFoundProd=true;
							th = new TableHeaderCell();
							th.Text=csvHeaderLabelsRow[i];
							th.ForeColor = Color.White ;
							th.Width=Unit.Pixel(450);
							tr.Cells.Add(th);
							cols++;
							break;
						}
					}
					if(!lFoundProd)
					{
						eMsg.Add("Product "+ProdPriceItem.ProdId+" not found in Marketing Table");
						//th = new TableHeaderCell();
						//th.Text=ProdPriceItem.ProdName.Substring(4);
						//th.Width=Unit.Pixel(450);
						//tr.Cells.Add(th);
						//cols++;
					}
				}
				tbl.Rows.Add(tr);
				#endregion

				#region add feature rows
				bool toggle = true ; // toggel for color for each row
				for(int FeatureRow=2; FeatureRow < aFeaturesCSV.Count; FeatureRow++)
				{
					tr = new TableRow();				
					string[] csvFeatureRow = (string[])aFeaturesCSV[FeatureRow];
					td = new TableCell();
					td.ForeColor = Color.SteelBlue;
					td.Font.Bold = true;
					td.Text="&nbsp;&nbsp;" + csvFeatureRow[0];
					tr.Cells.Add(td);
					
					foreach(IProdPrice ProdPriceItem in ProdsPrices)
					{
					//	lFoundProd=false;
						for(int col=1; col < csvHeaderIDsRow.Length; col++)
						{
							if(int.Parse(csvHeaderIDsRow[col]) == ProdPriceItem.ProdId)
							{
							//	string descr = ProdInfoCol.GetProd(ProdPriceItem.ProdId).Description;
							
								lFoundProd=true;
								td = new TableCell();
								td.Attributes["align"]="Center";
								if(csvFeatureRow[col].Trim().ToUpper()=="X")
								{
									HtmlImage image = new HtmlImage();
									image.Src = "images/free.gif";
									image.Border = 0;
									td.Controls.Add(image);
								}
								else
								{
									td.Text=csvFeatureRow[col];
								}
								td.ForeColor = Color.Red;
								td.Font.Bold = true;
								tr.Cells.Add(td);	
							}
						}
//						if(!lFoundProd)
//						{
//							td = new TableCell();
//							td.Attributes["align"]="Center";
//							td.Text="?";
//							tr.Cells.Add(td);									
//						}
					}
					// toggle row color alternating
					if(toggle == true)
					{
						tr.BackColor = Color.White ;
						toggle = false;
					}
					else if(toggle == false)
					{
						tr.BackColor = Color.WhiteSmoke ;
						toggle = true;
					}
					tbl.Rows.Add(tr);
				}
				#endregion

				#region add Initial Monthly Rate
				tr = new TableRow();				
				td = new TableCell();
				td.ForeColor = Color.SteelBlue;
				td.Font.Bold = true;
				td.Text="&nbsp;&nbsp;Initial Monthly Rate";
				tr.Cells.Add(td);
				foreach(IProdPrice ProdPriceItem in ProdsPrices)
				{
					for(int col=1; col < csvHeaderIDsRow.Length; col++)
					{
						if(int.Parse(csvHeaderIDsRow[col])==ProdPriceItem.ProdId)
						{
							td = new TableCell();
							td.Attributes["align"]="Center";
							td.Text=ProdPriceItem.UnitPrice.ToString("N");
							td.ForeColor = Color.Gray;
							td.Font.Bold = true;
							tr.BackColor = Color.White; 
							tr.Cells.Add(td);
						}
					}
				}
				tbl.Rows.Add(tr);
				#endregion
				#region add Prompt Pay Discount
				tr = new TableRow();				
				td = new TableCell();
				td.ForeColor = Color.SteelBlue;
				td.Font.Bold = true;
				td.Text="&nbsp;&nbsp;Prompt Pay Discount <font color='chocolate'><b>*</b></font>"; // red
				tr.Cells.Add(td);
				foreach(IProdPrice ProdPriceItem in ProdsPrices)
				{
					for(int col=1; col < csvHeaderIDsRow.Length; col++)
					{
						if(int.Parse(csvHeaderIDsRow[col])==ProdPriceItem.ProdId)
						{
							td = new TableCell();
							td.Attributes["align"]="Center";
							td.Text = "-10.00";
							td.ForeColor = Color.Gray;
							td.Font.Bold = true;
							tr.BackColor = Color.WhiteSmoke; 
							tr.Cells.Add(td);
						}
					}
				}
				tbl.Rows.Add(tr);
				#endregion

				#region add Monthly Recurring Rate
				
				tr = new TableRow();				
				td = new TableCell();
				td.ForeColor = Color.SteelBlue;
				td.Font.Bold = true;
				td.Text="&nbsp;&nbsp;Monthly Recurring Rate <font color='chocolate'><b>**</b></font>"; 
				tr.Cells.Add(td);
				foreach(IProdPrice ProdPriceItem in ProdsPrices)
				{
					for(int col=1; col < csvHeaderIDsRow.Length; col++)
					{
						if(int.Parse(csvHeaderIDsRow[col])==ProdPriceItem.ProdId)
						{
							td = new TableCell();
							td.Attributes["align"]="Center";
							td.Text = (ProdPriceItem.UnitPrice - 10.00m).ToString("C");
							td.ForeColor = Color.Chocolate;
							td.Font.Size = 9;
							td.Font.Bold = true;
							tr.BackColor = Color.White; 
							tr.Cells.Add(td);
						}
					}
				}
				tbl.Rows.Add(tr);
				
				#endregion
				#region add radio buttons
				tr = new TableRow();				
				td = new TableCell();
				td.Text = "&nbsp;&nbsp;Select a Package:";
				td.Font.Bold = true;
				tr.Cells.Add(td);
				rBtns = new RadioButton[cols];  // 
				int iCol=0;
				foreach(IProdPrice ProdPriceItem in ProdsPrices)
				{	
					for(int col=1; col < csvHeaderIDsRow.Length; col++)
					{
						if(int.Parse(csvHeaderIDsRow[col])==ProdPriceItem.ProdId)
						{
							td = new TableCell();
							td.Attributes["align"]="Center";
							ListItem li = new ListItem();
							RadioButton rb = new RadioButton();
							rb.GroupName="RBgroupName";
							rb.ID = "rbtn"+ProdPriceItem.ProdId;
							//rb.Text=ProdPriceItem.ProdId.ToString();


							if(ProdPriceItem.Equals(selected))
								rb.Checked=true;


							rBtns[iCol++]=rb;						
							//rb.EnableViewState=true;
							//rb.AutoPostBack=true;	
							//rb.Attributes["onclick"]="rememberButton(this)";
							td.Controls.Add(rb);
							tr.BackColor = Color.LightGray;
							tr.Cells.Add(td);
						}
					}
				}
				tbl.Rows.Add(tr);
				#endregion

			}
			catch(IOException ex)
			{
				eMsg.Add("system i/o error "+ex.Message);
				throw new Exception("system i/o error "+ex.Message);
			}
			catch(Exception ex)
			{
				eMsg.Add("runtime error "+ex.Message);
				throw new Exception("runtime error "+ex.Message);
			}
			return tbl;
		}
		private ArrayList loadFromTabDelimFile()
		{
			StreamReader din=null;
			ArrayList a1 = new ArrayList();
			string textFile=null;
			try 
			{
		//  permission password issue
			System.Configuration.AppSettingsReader configSettings= new System.Configuration.AppSettingsReader();
			textFile = (string)configSettings.GetValue("FEATURESFILEPATH",typeof(string));

		//		textFile = @"C:\Inetpub\wwwroot\FeaturesTest.csv";
				
				din = File.OpenText(textFile);

				String str;
				// kludge  account for carriage returns in label line
				// 1. get first line (ID line)
				str=din.ReadLine();
				a1.Add(str.Split((char)9));
				string[] s1 = (string[])a1[0];
				int numColsExpected = s1.Length;

				// all the rest of the lines 
				while ((str=din.ReadLine()) != null) 
				{
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					               // MAKE SURE TO TRIM THE FIRSTEST ONE
					sb.Append(str.Replace("\"",""));
					s1 = str.Split((char)9);
					int numColsFound = s1.Length;	
					// 2. build lines with carriage returns in them 
					while(numColsFound<numColsExpected)
					{	                               // remove double quotes
						string nextline = din.ReadLine().Replace("\"","");
						s1 = nextline.Split((char)9);
						numColsFound += s1.Length-1;
						sb.Append("<br>"+nextline);						
					}
					a1.Add(sb.ToString().Split((char)9));
				}
			}


//   exception catch moved to calling method
//			catch(Exception ex)
//			{
//				eMsg.Add(ex.ToString());
//				al=null;
//				//throw new Exception ex.ToString();
//				//al.Add("'ERROR',"+ex.ToString());
//			}
			finally
			{
				if(din!=null)
					din.Close();
			}
			return a1;
		}
	}

	public class Level2Products
	{
		CheckBox[]  chkBoxes;
		ArrayList eMsg = new ArrayList();
		public string[] ErrorMsg
		{
			get
			{
				if (eMsg.Count == 0)
					return null;
				string[] msg = new string[eMsg.Count];
				eMsg.CopyTo(msg);
				return msg;
			}
		}			
		public CheckBox[] checkBoxes
		{
			get{return chkBoxes;}
		}			
		public Table buildTable(IProdPrice[] Prods)
		{
			Table tbl = null;
			try
			{
				chkBoxes = new CheckBox[Prods.Length];
				tbl = new Table();
				tbl.Width = Unit.Pixel(640);
				tbl.CellPadding = 0;
				tbl.CellSpacing = 0;
				tbl.BorderColor = Color.Gainsboro;
				tbl.GridLines = GridLines.Horizontal;
				TableRow tr;
				TableCell td;
				CheckBox ckbx;
				bool toggle = true ; // toggel for color for each row
				string prodType="RE-DEFEAT BUSH";
				for(int iProd=0; iProd < Prods.Length; iProd++)
				{
					#region add lvl2 headers
					if(prodType!=Prods[iProd].ProdType)
					{
						tr = new TableRow();
						td = new TableCell();
						td.ColumnSpan=3;
						td.HorizontalAlign=HorizontalAlign.Center;
						prodType=Prods[iProd].ProdType;
						td.ForeColor = Color.White;
						td.Font.Bold = true;
						if(prodType == "Local Service")
						{
							td.BackColor = Color.Chocolate;
							prodType += " Selected";
						}
						else
						{
							td.BackColor = Color.DarkGray;
						}
						td.Text=prodType;
						tr.Controls.Add(td);
						tbl.Controls.Add(tr);
					}
					#endregion

					tr = new TableRow();
					td = new TableCell();
					ckbx = new CheckBox();
					ckbx.ID = "ckbx"+Prods[iProd].ProdId;

					if(Prods[iProd].ProdSelState==ProdSelectionState.Unavailable)
					{
						td.BackColor = Color.Gainsboro;
						tr.Font.Strikeout=true;
						ckbx.Enabled=false;
					}

					ckbx.Checked = (Prods[iProd].ProdSelState == ProdSelectionState.Selected ? true : false);

					if(Prods[iProd].Locked)
					{
						ckbx.Enabled = false;
					}

					ckbx.EnableViewState=true;
					ckbx.AutoPostBack=true;
					
					td.Controls.Add(ckbx);
					tr.Controls.Add(td);
					chkBoxes[iProd]=ckbx;
					td = new TableCell();
					

					if(Prods[iProd].ProdSelState==ProdSelectionState.Unavailable)
					{
						td.CssClass = "subitems";
						td.BackColor = Color.Gainsboro;
						tr.Font.Strikeout=false;
						td.ForeColor = Color.Gray;
						ckbx.Enabled=false;
						td.Text = Prods[iProd].UnitPrice.ToString("C");
					}
					else
					{
						td.CssClass = "subitems";
						td.Text = Prods[iProd].UnitPrice.ToString("C");
					}

					tr.Controls.Add(td);
					td = new TableCell();
					if(Prods[iProd].ProdSelState==ProdSelectionState.Unavailable)
					{
						td.BackColor = Color.Gainsboro;
						td.CssClass = "subitems";
						td.Font.Strikeout=false;
						td.ForeColor = Color.Gray;
						td.Text = Prods[iProd].ProdName;//.Substring(4);
					}
					else
					{

						if(Prods[iProd].Description != null)
						{
							td.CssClass = "subitems";
							string myString;
							myString = Regex.Replace(Prods[iProd].Description, "'", "un"); // code for ' &##39;
//							td.Text = "<a href='javascript:void(0);' onmouseover='return escape(\""+myString+"\")'>"+Prods[iProd].ProdName.Substring(4)+"</a>";
							td.Text = "<a href='javascript:void(0);' onmouseover='return escape(\""+myString+"\")'>"+Prods[iProd].ProdName +"</a>";
						}
						else
						{
							td.CssClass = "subitems";
							td.Text = Prods[iProd].ProdName;//.Substring(4);
						}
					}
					tr.Controls.Add(td);
					if((!Prods[iProd].Locked))
					{
						if(toggle == true)
						{
							tr.BackColor = Color.White;
							toggle = false;
						}
						else if(toggle == false)
						{
							tr.BackColor = Color.WhiteSmoke;
							toggle = true;
						}
					}
					else
					{
						tr.BackColor = Color.WhiteSmoke;
						tr.Font.Bold = true;
						tr.ForeColor = Color.White;
					}

					tbl.Controls.Add(tr);
				}
			}
			catch(Exception ex)
			{
				eMsg.Add("runtime error "+ex.Message);
			}
			return tbl;
		}
	
	}
	public class LoadSummaryData
	{
		protected IMap imap;
		protected WIP wip;
	//	decimal  netAmt;
		public LoadSummaryData()	{}
		LoadSummaryData(IMap Imap)
		{
			imap=Imap;
			wip = (WIP)imap.find(WIP.IKeyS);
		}

	//	public decimal  NetAmount
	//	{
	//		get{return netAmt;}
	//	}
		public Table buildTable(IProdPrice[] Prods)
		{
			Table tbl=null;
		//	netAmt=0;
			try
			{			
				TableRow tr;
				TableCell td;
				tbl = new Table();

				tbl.BorderWidth=1;
				tbl.CellPadding=0;
				tbl.CellSpacing=0;
				tbl.BorderColor = Color.Gainsboro;
				tbl.GridLines = GridLines.Horizontal;
				tbl.Font.Size = 10;
				tbl.Font.Bold = true ;
				tbl.Width=Unit.Percentage(97);

				tr = new TableRow();				
				td = new TableCell();
				td.Attributes["align"]=" left";
				td.Text = "&nbsp;&nbsp;Package and Features Selected";
				td.Font.Bold = true;
				td.ForeColor = Color.White;
				tr.Cells.Add(td);

				td = new TableCell();
				td.Width=Unit.Pixel(100);
				tr.Cells.Add(td);

				td = new TableCell();
				td.Attributes["align"]="right";
				td.Text = " Price &nbsp;&nbsp;";
				td.Font.Bold = true;
				td.ForeColor = Color.White;
				tr.Cells.Add(td);

				td = new TableCell();
				td.Width=Unit.Pixel(15);
				tr.Cells.Add(td);

				tr.BackColor = Color.Chocolate ;
				tbl.Rows.Add(tr);


				bool toggle = true ; // toggle for color for each row
				for(int i = 0; i < Prods.Length; i++)
				{
				//	if(Prods[i].ProdSelState==ProdSelectionState.Selected)
				//	{
						tr = new TableRow();

						td = new TableCell();
						td.Attributes["align"]="left";
						td.CssClass ="subitems";
						td.Text = "&nbsp;&nbsp;"+ Prods[i].ProdName;
					
						if (Prods[i].PackageId > 0)
                            td.Text = Const.COMP_INDENT + Prods[i].ProdName;

						tr.Cells.Add(td);

						td = new TableCell();
						td.Width=Unit.Pixel(100);
						tr.Cells.Add(td);

						td = new TableCell();
						td.Attributes["align"]="right";
						td.CssClass ="subitems";

/* Hidden policy */
						if (Prods[i].PackageId == 0)
							td.Text = Prods[i].UnitPrice.ToString("C") +"&nbsp;&nbsp;";
	
					//	netAmt+=Prods[i].PriceAmt;
						tr.Cells.Add(td);

						td = new TableCell();
						td.Width=Unit.Pixel(15);
						tr.Cells.Add(td);

						// toggle row color alternating
						if(toggle == true)
						{
							tr.BackColor = Color.White ;
							toggle = false;
						}
						else if(toggle == false)
						{
							tr.BackColor = Color.WhiteSmoke ;
							toggle = true;
						}
						tbl.Rows.Add(tr);
				//	}
				}
			}
			catch(Exception ex)
			{				
				throw new Exception("runtime error "+ex.Message);
			}
			return tbl;
		}
	}

	public class OrderConfirmPage
	{		
		public Table OrderedProductsTable(IProdPrice[] orderedProducts)
		{
			Table tbl = buildTableOrderedProductsTableHeader();
			TableRow tr; 
			TableCell td;
			//tbl.GridLines = GridLines.Horizontal;
			
			string color = "even";
			foreach(IProdPrice orderedProd in orderedProducts)
			{
				tr = new TableRow();
//				td = new TableCell();
//				td.Text="&nbsp;";
//				td.BackColor=System.Drawing.Color.LightYellow;//("#ffffe6");
//				tr.Cells.Add(td);
				
				td = new TableCell();
				td.HorizontalAlign=HorizontalAlign.Left;
				td.CssClass="subitems";
				td.Text = "&nbsp;&nbsp;" + orderedProd.ProdName;
	
				if (orderedProd.PackageId > 0)
					td.Text = "&nbsp;&nbsp;" + Const.COMP_INDENT + orderedProd.ProdName;
			//	td.Text=orderedProd.ProdName;
				tr.Cells.Add(td);
				
				td = new TableCell();
				td.HorizontalAlign=HorizontalAlign.Right;
				td.CssClass="subitems";
				
/* Hidden policy */
				if (orderedProd.PackageId == 0)
					td.Text = orderedProd.UnitPrice.ToString("C")+ "&nbsp;&nbsp;";
				
				if(color == "even")
				{
					tr.BackColor = System.Drawing.Color.White;
					color = "odd";
				}
				else
				{
					tr.BackColor = System.Drawing.Color.WhiteSmoke;
					color = "even";
				}
				tr.Cells.Add(td);
				tbl.Rows.Add(tr);
			}
			return tbl;
		}
		private Table buildTableOrderedProductsTableHeader()
		{
			Table tbl = new Table();
			TableRow tr; TableCell td; Label lbl;
			tbl.CellPadding=0;
			tbl.CellSpacing=0;
			tbl.BorderWidth=Unit.Point(0);
			tbl.Width=Unit.Percentage(100);

			// may be an unneed row
			tr = new TableRow();
			td = new TableCell();
			td.BackColor=System.Drawing.Color.AliceBlue;
			td.ColumnSpan=4;
			td.CssClass="05_con_bold";			
			td.Text="&nbsp;&nbsp;&nbsp;Order Details";
			tr.Cells.Add(td);
			tbl.Rows.Add(tr);

			// gold header row 
			tr = new TableRow();
			td = new TableCell();
			td.BackColor=System.Drawing.Color.Gold;
			td.HorizontalAlign=HorizontalAlign.Left;
			lbl = new Label();
			lbl.CssClass="05_con_subbold";
			lbl.Text="&nbsp;&nbsp;Product";
			td.ColumnSpan = 1 ;
			td.Controls.Add(lbl);
			tr.Cells.Add(td);

			td = new TableCell();
			td.BackColor=System.Drawing.Color.Gold;
			td.ColumnSpan = 3 ;
			td.HorizontalAlign=HorizontalAlign.Right;
			lbl = new Label();
			lbl.CssClass="05_con_subbold";
			lbl.Text="Price &nbsp;&nbsp;";
			td.Controls.Add(lbl);
			tr.Cells.Add(td);

			tbl.Rows.Add(tr);
		
			return tbl;
		}
	}
}