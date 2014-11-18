using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;
//using Patterns;
 
namespace DPI.Components
{
		public class Tax : DomainObj
		{
			/*        Data        */
			static string iName = "Tax";
			int tax_ID;
			string zipCode;
			string zipCode_Extension;
			decimal federal_Taxes;
			decimal state_Taxes;
			decimal local_Taxes;
			string taxCode;
			string geo_state;
			string county;
			string city;
			decimal surcharge1;
			string surcharge1_Indicator;
			decimal surcharge2;
			string surcharge2_Indicator;
			decimal surcharge3;
			string surcharge3_Indicator;
			decimal surcharge4;
			string surcharge4_Indicator;
			decimal surcharge5;
			string surcharge5_Indicator;
			decimal surcharge6;
			string surcharge6_Indicator;
			decimal surcharge7;
			string surcharge7_Indicator;
			decimal surcharge8;
			string surcharge8_Indicator;
			decimal surcharge9;
			string surcharge9_Indicator;
			decimal surcharge10;
			string surcharge10_Indicator;
        
			/*        Properties        */
			public override IDomKey IKey 
			{
				get { return new Key(iName, tax_ID.ToString()); }
			}
			public int Tax_ID
			{
				get { return tax_ID; }
			}
			public string ZipCode
			{
				get { return zipCode; }
				set
				{
					setState();
					zipCode = value;
				}
			}
			public string ZipCode_Extension
			{
				get { return zipCode_Extension; }
				set
				{
					setState();
					zipCode_Extension = value;
				}
			}
			public decimal Federal_Taxes
			{
				get { return federal_Taxes; }
				set
				{
					setState();
					federal_Taxes = Decimal.Round(value, 2);
				}
			}
			public decimal State_Taxes
			{
				get { return state_Taxes; }
				set
				{
					setState();
					state_Taxes = Decimal.Round(value, 2);
				}
			}
			public decimal Local_Taxes
			{
				get { return local_Taxes; }
				set
				{
					setState();
					local_Taxes = Decimal.Round(value, 2);
				}
			}
			public string TaxCode
			{
				get { return taxCode; }
				set
				{
					setState();
					taxCode = value;
				}
			}
			public string Geo_State
			{
				get { return geo_state; }
				set
				{
					setState();
					this.geo_state = value;
				}
			}
			public string County
			{
				get { return county; }
				set
				{
					setState();
					county = value;
				}
			}
			public string City
			{
				get { return city; }
				set
				{
					setState();
					city = value;
				}
			}
			public decimal Surcharge1
			{
				get { return surcharge1; }
				set
				{
					setState();
					surcharge1 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge1_Indicator
			{
				get { return surcharge1_Indicator; }
				set
				{
					setState();
					surcharge1_Indicator = value;
				}
			}
			public decimal Surcharge2
			{
				get { return surcharge2; }
				set
				{
					setState();
					surcharge2 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge2_Indicator
			{
				get { return surcharge2_Indicator; }
				set
				{
					setState();
					surcharge2_Indicator = value;
				}
			}
			public decimal Surcharge3
			{
				get { return surcharge3; }
				set
				{
					setState();
					surcharge3 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge3_Indicator
			{
				get { return surcharge3_Indicator; }
				set
				{
					setState();
					surcharge3_Indicator = value;
				}
			}
			public decimal Surcharge4
			{
				get { return surcharge4; }
				set
				{
					setState();
					surcharge4 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge4_Indicator
			{
				get { return surcharge4_Indicator; }
				set
				{
					setState();
					surcharge4_Indicator = value;
				}
			}
			public decimal Surcharge5
			{
				get { return surcharge5; }
				set
				{
					setState();
					surcharge5 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge5_Indicator
			{
				get { return surcharge5_Indicator; }
				set
				{
					setState();
					surcharge5_Indicator = value;
				}
			}
			public decimal Surcharge6
			{
				get { return surcharge6; }
				set
				{
					setState();
					surcharge6 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge6_Indicator
			{
				get { return surcharge6_Indicator; }
				set
				{
					setState();
					surcharge6_Indicator = value;
				}
			}
			public decimal Surcharge7
			{
				get { return surcharge7; }
				set
				{
					setState();
					surcharge7 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge7_Indicator
			{
				get { return surcharge7_Indicator; }
				set
				{
					setState();
					surcharge7_Indicator = value;
				}
			}
			public decimal Surcharge8
			{
				get { return surcharge8; }
				set
				{
					setState();
					surcharge8 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge8_Indicator
			{
				get { return surcharge8_Indicator; }
				set
				{
					setState();
					surcharge8_Indicator = value;
				}
			}
			public decimal Surcharge9
			{
				get { return surcharge9; }
				set
				{
					setState();
					surcharge9 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge9_Indicator
			{
				get { return surcharge9_Indicator; }
				set
				{
					setState();
					surcharge9_Indicator = value;
				}
			}
			public decimal Surcharge10
			{
				get { return surcharge10; }
				set
				{
					setState();
					surcharge10 = Decimal.Round(value, 2);
				}
			}
			public string Surcharge10_Indicator
			{
				get { return surcharge10_Indicator; }
				set
				{
					setState();
					surcharge10_Indicator = value;
				}
			}
        
			/*        Constructors			*/
			public Tax()
			{
				sql = new TaxSQL();
				tax_ID = random.Next(Int32.MinValue, -1);
				rowState = RowState.New;
			}
			public Tax(UOW uow) : this()
			{
				if(uow == null)
					throw new ArgumentException("Unit Of Work is required", "Unit Of Work");

				this.uow = uow;
	
				if (this.uow.Imap != null)
					this.uow.Imap.add(this);
			}
        
			/*        Methods        */
			public override void checkExists()
			{
				if ((Tax_ID < 1))
					throw new ArgumentException("Valid row is required for update and delete");
			}
			/*		Static methods		*/
			public static ProdTax2[] findTaxes(UOW uow, string zip, string taxCode, decimal amt, int extRef)
			{
							
				ProdTax[] ptaxes = findTaxes(uow, zip, taxCode);
				ProdTax2[] taxes = new ProdTax2[ptaxes.Length];
				for (int i = 0; i < taxes.Length; i++)
					taxes[i] = new ProdTax2(amt, ptaxes[i], extRef);
				
				return taxes;

			}
			public static ProdTax[] findTaxes(UOW uow, string zip, string taxCode)
			{
				ArrayList ar = new ArrayList(30);
				Tax[] taxes = Tax.getbyZip_TaxCode(uow, zip, taxCode);

				Tax tax = new Tax();
				for(int i = 0; i < taxes.Length; i++)
					tax.normalizeTaxes(uow, taxes[i], ar);

				ProdTax[] tis = new ProdTax[ar.Count];
				ar.CopyTo(tis);
				return tis;
			}

			public static Tax find(UOW uow, int tax_ID)
			{
				if (uow.Imap != null)
					if (uow.Imap.keyExists(Tax.getKey(tax_ID)))
						return (Tax)uow.Imap.find(Tax.getKey(tax_ID));
            
				Tax cls = new Tax();
				cls.uow = uow;
				cls.tax_ID = tax_ID;
				cls = getOne(((TaxSQL)cls.sql).getKey(cls));

				if (uow.Imap != null)
					uow.Imap.add(cls);
            
				return cls;
			}
			public static Tax find(int tax_ID)
			{
				Tax cls = new Tax();
				cls.tax_ID = tax_ID;
				return getOne(((TaxSQL)cls.sql).getKey(cls));
			}
			public static Tax[] getAll(UOW uow)
			{
				Tax[] objs = (new TaxSQL()).getAll(uow);
				DomainObj.addToIMap(uow, objs);
				return objs;
			}
			public static Tax[] getAll()
			{
				return (new TaxSQL()).getAll();
			}
			public static Key getKey(int tax_ID)
			{
				return new Key(iName, tax_ID.ToString());
			}
			
			/*		Implementation		*/
			static Tax[] getbyZip_TaxCode(UOW uow, string zip, string taxCode)
			{
				if (taxCode == null)
					return new Tax[] {};
				return (new TaxSQL()).getbyZip_TaxCode(uow, zip, taxCode);
			}
			static Tax[] getbyZip_TaxCode(string zip, string taxCode)
			{
				return (new TaxSQL()).getbyZip_TaxCode(zip, taxCode);
			}

			void normalizeTaxes(UOW uow, Tax tax, ArrayList ar)
			{
				if (FederalTax(tax) != null)
					ar.Add(FederalTax(tax));
		
				if (StateTax(tax) != null)
					ar.Add(StateTax(tax));

				if (LocalTax(tax) != null)
					ar.Add(LocalTax(tax));

				doSurcharges(uow, tax, ar);
			}
			ProdTax FederalTax(Tax tax)
			{
				if (tax.federal_Taxes == 0)
					return null;

				ProdTax ti = new ProdTax("X", tax.federal_Taxes, false);//, "Federal Tax");

				return ti;
			}
			ProdTax StateTax(Tax tax)
			{
				if (tax.state_Taxes == 0)
					return null;

				ProdTax ti = new ProdTax("Y", tax.state_Taxes, false);//, "State Tax");

				return ti;
			}
			ProdTax LocalTax(Tax tax)
			{
				if (tax.local_Taxes == 0)
					return null;

				ProdTax ti = new ProdTax("Z", tax.local_Taxes, false);//, "Local Tax");

				return ti;
			} 
	
			void doSurcharges(UOW uow, Tax tax, ArrayList ar)
			{
				ProdTax ti;
				
				ti = surcharge(uow, tax.surcharge1_Indicator, tax.Surcharge1);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge2_Indicator, tax.Surcharge2);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge3_Indicator, tax.Surcharge3);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge4_Indicator, tax.Surcharge4);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge5_Indicator, tax.Surcharge5);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge6_Indicator, tax.Surcharge6);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge7_Indicator, tax.Surcharge7);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge8_Indicator, tax.Surcharge8);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge9_Indicator, tax.Surcharge9);
				if (ti != null)
					ar.Add(ti);

				ti = surcharge(uow, tax.surcharge10_Indicator, tax.Surcharge10);
				if (ti != null)
					ar.Add(ti);
			}
			ProdTax surcharge(UOW uow, string id, decimal rate)
			{
				if (id == null)
					return null;

				if (id.Trim().Length == 0)
					return null;

				if (rate == 0)
					return null;
				
				Customer_Tax_Type ctt = Customer_Tax_Type.find(uow, id);
			
				ProdTax ti = new ProdTax(id, rate, ctt.Unit_Based); //, ctt.Description);

				return ti;
			}

			static Tax getOne(Tax[] acls)
			{
				if (acls.Length == 1)
					return acls[0];
            
				if (acls.Length == 0)
					throw new ArgumentException("Row not found");
            
				throw new ArgumentException("More than one row found");
			}
			static void copyAttrs(Tax src, Tax tar)
			{
				if (tar == null)
					throw new ArgumentNullException("Target object must not be null");
            
				if (src == null)
					throw new ArgumentNullException("Source object must not be null");
            
				tar.tax_ID = src.tax_ID;
				tar.zipCode = src.zipCode;
				tar.zipCode_Extension = src.zipCode_Extension;
				tar.federal_Taxes = src.federal_Taxes;
				tar.state_Taxes = src.state_Taxes;
				tar.local_Taxes = src.local_Taxes;
				tar.taxCode = src.taxCode;
				tar.rowState = src.rowState;
				tar.county = src.county;
				tar.city = src.city;
				tar.surcharge1 = src.surcharge1;
				tar.surcharge1_Indicator = src.surcharge1_Indicator;
				tar.surcharge2 = src.surcharge2;
				tar.surcharge2_Indicator = src.surcharge2_Indicator;
				tar.surcharge3 = src.surcharge3;
				tar.surcharge3_Indicator = src.surcharge3_Indicator;
				tar.surcharge4 = src.surcharge4;
				tar.surcharge4_Indicator = src.surcharge4_Indicator;
				tar.surcharge5 = src.surcharge5;
				tar.surcharge5_Indicator = src.surcharge5_Indicator;
				tar.surcharge6 = src.surcharge6;
				tar.surcharge6_Indicator = src.surcharge6_Indicator;
				tar.surcharge7 = src.surcharge7;
				tar.surcharge7_Indicator = src.surcharge7_Indicator;
				tar.surcharge8 = src.surcharge8;
				tar.surcharge8_Indicator = src.surcharge8_Indicator;
				tar.surcharge9 = src.surcharge9;
				tar.surcharge9_Indicator = src.surcharge9_Indicator;
				tar.surcharge10 = src.surcharge10;
				tar.surcharge10_Indicator = src.surcharge10_Indicator;
				tar.rowState = src.rowState;
			}
 
			override protected SqlGateway  loadSql()
			{
				return new TaxSQL();
			}
			

			/*		SQL		*/
			class TaxSQL : SqlGateway
			{
				public Tax[] getKey(Tax rec)
				{
					SqlCommand cmd = getCommand(rec);
					cmd.CommandText = "spTax_Get_Id";
					cmd.Parameters.Add("@Tax_ID", SqlDbType.Int, 0).Value = rec.tax_ID;
					return convert(execReader(cmd));
				}
				public override void insert(DomainObj obj)
				{
					Tax rec = (Tax)obj;
					SqlCommand cmd = getCommand(rec);
					cmd.CommandText = "spTax_Ins";
					setParam(cmd, rec);
                
					cmd.Parameters["@Tax_ID"].Direction = ParameterDirection.Output;
					execScalar(cmd);
					rec.tax_ID = (int)cmd.Parameters["@Tax_ID"].Value;
					rec.rowState = RowState.Clean;
				}
				public override void delete(DomainObj obj)
				{
					Tax rec = (Tax)obj;
					SqlCommand cmd = getCommand(rec);
					cmd.CommandText = "spTax_Del_Id";
					cmd.Parameters.Add("@Tax_ID", SqlDbType.Int, 0).Value = rec.tax_ID;
                
					execScalar(cmd);
					rec.rowState = RowState.Deleted;
				}
				public override void update(DomainObj obj)
				{
					Tax rec = (Tax)obj;
					SqlCommand cmd = getCommand(rec);
					cmd.CommandText = "spTax_Upd";
					setParam(cmd, rec);
                
					execScalar(cmd);
					rec.rowState = RowState.Clean;
				}
				public Tax[] getAll(UOW uow)
				{
					SqlCommand cmd = makeCommand(uow);
					cmd.CommandText = "spTax_Get_All";
					return convert(execReader(cmd));
				}
				/*            public Tax[] getAll(SqlTransaction xact)
							{
								SqlCommand cmd = makeCommand(xact);
								cmd.CommandText = "spTax_Get_All";
								return convert(execReader(cmd));
							}
							public Tax[] getAll(SqlConnection cn)
							{
								SqlCommand cmd = makeCommand(cn);
								cmd.CommandText = "spTax_Get_All";
								return convert(execReader(cmd));
							}
				*/
				public Tax[] getAll()
				{
					SqlCommand cmd = makeCommand();
					cmd.CommandText = "spTax_Get_All";
					return convert(execReader(cmd));
				}
				public Tax[] getbyZip_TaxCode(UOW uow, string zip, string taxCode)
				{
					SqlCommand cmd = makeCommand(uow);
					cmd.CommandText = "spTax_Get_Zip_TaxCode";
					cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 5).Value = zip;
					cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = taxCode;

					return convert(execReader(cmd));
				}
				/*			public Tax[] getbyZip_TaxCode(SqlTransaction xact, string zip, string taxCode)
							{
								SqlCommand cmd = makeCommand(xact);
								cmd.CommandText = "spTax_Get_Zip_TaxCode";
								cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 5).Value = zip;
								cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = taxCode;

								return convert(execReader(cmd));
							}
					*/
				public Tax[] getbyZip_TaxCode(string zip, string taxCode)
				{
					SqlCommand cmd = makeCommand();
					cmd.CommandText = "spTax_Get_Zip_TaxCode";
					cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 5).Value = zip;
					cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = taxCode;

					return convert(execReader(cmd));
				}
				/*        Implementation        */
				void setParam(SqlCommand cmd, Tax rec)
				{
					cmd.Parameters.Add("@Tax_ID", SqlDbType.Int, 0).Value = rec.tax_ID;
 
					cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 5).Value = rec.zipCode;
 
					if (rec.zipCode_Extension == null)
						cmd.Parameters.Add("@ZipCode_Extension", SqlDbType.VarChar, 4).Value = DBNull.Value;
					else
					{
						if (rec.ZipCode_Extension.Length == 0)
							cmd.Parameters.Add("@ZipCode_Extension", SqlDbType.VarChar, 4).Value = DBNull.Value;
						else
							cmd.Parameters.Add("@ZipCode_Extension", SqlDbType.VarChar, 4).Value = rec.zipCode_Extension;
					}
					cmd.Parameters.Add("@Federal_Taxes", SqlDbType.Decimal, 0).Value = rec.federal_Taxes;
					cmd.Parameters.Add("@State_Taxes", SqlDbType.Decimal, 0).Value = rec.state_Taxes;
					cmd.Parameters.Add("@Local_Taxes", SqlDbType.Decimal, 0).Value = rec.local_Taxes;
 
					cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = rec.taxCode;
 
					cmd.Parameters.Add("@GeoState", SqlDbType.VarChar, 2).Value = rec.geo_state;
 
					cmd.Parameters.Add("@County", SqlDbType.VarChar, 15).Value = rec.county;
 
					cmd.Parameters.Add("@City", SqlDbType.VarChar, 15).Value = rec.city;
					cmd.Parameters.Add("@Surcharge1", SqlDbType.Decimal, 0).Value = rec.surcharge1;
 
					cmd.Parameters.Add("@Surcharge1_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge1_Indicator;
					cmd.Parameters.Add("@Surcharge2", SqlDbType.Decimal, 0).Value = rec.surcharge2;
 
					cmd.Parameters.Add("@Surcharge2_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge2_Indicator;
					cmd.Parameters.Add("@Surcharge3", SqlDbType.Decimal, 0).Value = rec.surcharge3;
 
					cmd.Parameters.Add("@Surcharge3_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge3_Indicator;
					cmd.Parameters.Add("@Surcharge4", SqlDbType.Decimal, 0).Value = rec.surcharge4;
 
					cmd.Parameters.Add("@Surcharge4_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge4_Indicator;
					cmd.Parameters.Add("@Surcharge5", SqlDbType.Decimal, 0).Value = rec.surcharge5;
 
					cmd.Parameters.Add("@Surcharge5_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge5_Indicator;
					cmd.Parameters.Add("@Surcharge6", SqlDbType.Decimal, 0).Value = rec.surcharge6;
 
					cmd.Parameters.Add("@Surcharge6_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge6_Indicator;
					cmd.Parameters.Add("@Surcharge7", SqlDbType.Decimal, 0).Value = rec.surcharge7;
 
					cmd.Parameters.Add("@Surcharge7_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge7_Indicator;
					cmd.Parameters.Add("@Surcharge8", SqlDbType.Decimal, 0).Value = rec.surcharge8;
 
					cmd.Parameters.Add("@Surcharge8_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge8_Indicator;
					cmd.Parameters.Add("@Surcharge9", SqlDbType.Decimal, 0).Value = rec.surcharge9;
 
					cmd.Parameters.Add("@Surcharge9_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge9_Indicator;
					cmd.Parameters.Add("@Surcharge10", SqlDbType.Decimal, 0).Value = rec.surcharge10;
 
					cmd.Parameters.Add("@Surcharge10_Indicator", SqlDbType.VarChar, 1).Value = rec.surcharge10_Indicator;
				}
				protected override DomainObj reader(SqlDataReader rdr)
				{
					Tax rec = new Tax();
                
					if (rdr["Tax_ID"] != DBNull.Value)
						rec.tax_ID = (int) rdr["Tax_ID"];
 
					if (rdr["ZipCode"] != DBNull.Value)
						rec.zipCode = (string) rdr["ZipCode"];
 
					if (rdr["ZipCode_Extension"] != DBNull.Value)
						rec.zipCode_Extension = (string) rdr["ZipCode_Extension"];
 
					if (rdr["Federal_Taxes"] != DBNull.Value)
						rec.federal_Taxes = Decimal.Round((decimal)rdr["Federal_Taxes"], 2);
 
					if (rdr["State_Taxes"] != DBNull.Value)
						rec.state_Taxes = Decimal.Round((decimal)rdr["State_Taxes"], 2);
 
					if (rdr["Local_Taxes"] != DBNull.Value)
						rec.local_Taxes = Decimal.Round((decimal)rdr["Local_Taxes"], 2);
 
					if (rdr["TaxCode"] != DBNull.Value)
						rec.taxCode = (string) rdr["TaxCode"];
 
					if (rdr["GeoState"] != DBNull.Value)
						rec.geo_state = (string) rdr["GeoState"];
 
					if (rdr["County"] != DBNull.Value)
						rec.county = (string) rdr["County"];
 
					if (rdr["City"] != DBNull.Value)
						rec.city = (string) rdr["City"];
 
					if (rdr["Surcharge1"] != DBNull.Value)
						rec.surcharge1 = Decimal.Round((decimal)rdr["Surcharge1"], 2);
 
					if (rdr["Surcharge1_Indicator"] != DBNull.Value)
						rec.surcharge1_Indicator = (string) rdr["Surcharge1_Indicator"];
 
					if (rdr["Surcharge2"] != DBNull.Value)
						rec.surcharge2 = Decimal.Round((decimal)rdr["Surcharge2"], 2);
 
					if (rdr["Surcharge2_Indicator"] != DBNull.Value)
						rec.surcharge2_Indicator = (string) rdr["Surcharge2_Indicator"];
 
					if (rdr["Surcharge3"] != DBNull.Value)
						rec.surcharge3 = Decimal.Round((decimal)rdr["Surcharge3"], 2);
 
					if (rdr["Surcharge3_Indicator"] != DBNull.Value)
						rec.surcharge3_Indicator = (string) rdr["Surcharge3_Indicator"];
 
					if (rdr["Surcharge4"] != DBNull.Value)
						rec.surcharge4 = Decimal.Round((decimal)rdr["Surcharge4"], 2);
 
					if (rdr["Surcharge4_Indicator"] != DBNull.Value)
						rec.surcharge4_Indicator = (string) rdr["Surcharge4_Indicator"];
 
					if (rdr["Surcharge5"] != DBNull.Value)
						rec.surcharge5 = Decimal.Round((decimal)rdr["Surcharge5"], 2);
 
					if (rdr["Surcharge5_Indicator"] != DBNull.Value)
						rec.surcharge5_Indicator = (string) rdr["Surcharge5_Indicator"];
 
					if (rdr["Surcharge6"] != DBNull.Value)
						rec.surcharge6 = Decimal.Round((decimal)rdr["Surcharge6"], 2);
 
					if (rdr["Surcharge6_Indicator"] != DBNull.Value)
						rec.surcharge6_Indicator = (string) rdr["Surcharge6_Indicator"];
 
					if (rdr["Surcharge7"] != DBNull.Value)
						rec.surcharge7 = Decimal.Round((decimal)rdr["Surcharge7"], 2);
 
					if (rdr["Surcharge7_Indicator"] != DBNull.Value)
						rec.surcharge7_Indicator = (string) rdr["Surcharge7_Indicator"];
 
					if (rdr["Surcharge8"] != DBNull.Value)
						rec.surcharge8 = Decimal.Round((decimal)rdr["Surcharge8"], 2);
 
					if (rdr["Surcharge8_Indicator"] != DBNull.Value)
						rec.surcharge8_Indicator = (string) rdr["Surcharge8_Indicator"];
 
					if (rdr["Surcharge9"] != DBNull.Value)
						rec.surcharge9 = Decimal.Round((decimal)rdr["Surcharge9"], 2);
 
					if (rdr["Surcharge9_Indicator"] != DBNull.Value)
						rec.surcharge9_Indicator = (string) rdr["Surcharge9_Indicator"];
 
					if (rdr["Surcharge10"] != DBNull.Value)
						rec.surcharge10 = Decimal.Round((decimal)rdr["Surcharge10"], 2);
 
					if (rdr["Surcharge10_Indicator"] != DBNull.Value)
						rec.surcharge10_Indicator = (string) rdr["Surcharge10_Indicator"];
 
					rec.rowState = RowState.Clean;
					return rec;
				}
				Tax[] convert(DomainObj[] objs)
				{
					Tax[] acls  = new Tax[objs.Length];
					objs.CopyTo(acls, 0);
					return  acls;
				}
			}
		}
	}
