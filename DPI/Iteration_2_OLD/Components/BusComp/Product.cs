using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class Product : DomainObj
	{
		/*        Data        */
		static string iName = "Product";
		int id;
		string prodType;
		string prodName;
		string billText;
		string prodCode;
		string oldPriceCode;
		string prodSubClass;
		bool isComponentOnly;
		bool isBillable;
		bool isProvisionable;
		bool isProvViaMapping;
		string description;
		string eligibilityCriteria;
		string provCategory;
		int supplier;
		int vendor;
		string taxCode;
		bool isTaxExempt;
		string status;
		string prodCategory;
		DateTime startDate;
		DateTime endDate;
		string acctCode;
		string compCode;
		string deptCode;
		int startServMon;
		int endServMon;
		int predId;
		int mappingProd;
		bool isAgentVisible;
		string webDescription;
		string ordSumryStartMon2;
		bool isPreselectedWebOrderL2;
		bool suppressZeroPriceProd;
		bool isExcludedFromTotalL2;
		bool displayUnclickMessage;
		bool isRestricted;
        
		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id; }
		}
		public string ProdType
		{
			get { return prodType; }
			set
			{
				setState();
				prodType = value;
			}
		}
		public string ProdName
		{
			get { return prodName; }
			set
			{
				setState();
				prodName = value;
			}
		}
		public string BillText
		{
			get { return billText; }
			//	get { return id.ToString() + " " + billText; }
			set
			{
				setState();
				billText = value;
			}
		}
		public string ProdCode
		{
			get { return prodCode; }
			set
			{
				setState();
				prodCode = value;
			}
		}
		public string OldPriceCode
		{
			get { return oldPriceCode; }
			set
			{
				setState();
				oldPriceCode = value;
			}
		}
		public string ProdSubClass
		{
			get { return prodSubClass; }
			set
			{
				setState();
				prodSubClass = value;
			}
		}
		public bool IsComponentOnly
		{
			get { return isComponentOnly; }
			set
			{
				setState();
				isComponentOnly = value;
			}
		}
		public bool IsBillable
		{
			get { return isBillable; }
			set
			{
				setState();
				isBillable = value;
			}
		}
		public bool IsProvisionable
		{
			get { return isProvisionable; }
			set
			{
				setState();
				isProvisionable = value;
			}
		}
		public bool IsProvViaMapping
		{
			get { return isProvViaMapping; }
			set
			{
				setState();
				isProvViaMapping = value;
			}
		}
		public string Description
		{
			get { return description; }
			set
			{
				setState();
				description = value;
			}
		}
		public string EligibilityCriteria
		{
			get { return eligibilityCriteria; }
			set
			{
				setState();
				eligibilityCriteria = value;
			}
		}
		public string ProvCategory
		{
			get { return provCategory; }
			set
			{
				setState();
				provCategory = value;
			}
		}
		public int Supplier
		{
			get { return supplier; }
			set
			{
				setState();
				supplier = value;
			}
		}
		public int Vendor
		{
			get { return vendor; }
			set
			{
				setState();
				vendor = value;
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
		public bool IsTaxExempt
		{
			get { return isTaxExempt; }
			set
			{
				setState();
				isTaxExempt = value;
			}
		}
		public string Status
		{
			get { return status; }
			set
			{
				setState();
				status = value;
			}
		}
		public string ProdCategory
		{
			get { return prodCategory; }
			set
			{
				setState();
				prodCategory = value;
			}
		}
		public DateTime StartDate
		{
			get { return startDate; }
			set
			{
				setState();
				startDate = value;
			}
		}
		public DateTime EndDate
		{
			get { return endDate; }
			set
			{
				setState();
				endDate = value;
			}
		}
		public string AcctCode
		{
			get { return acctCode; }
			set
			{
				setState();
				acctCode = value;
			}
		}
		public string CompCode
		{
			get { return compCode; }
			set
			{
				setState();
				compCode = value;
			}
		}
		public string DeptCode
		{
			get { return deptCode; }
			set
			{
				setState();
				deptCode = value;
			}
		}
		public int StartServMon
		{
			get { return startServMon; }
			set
			{
				setState();
				startServMon = value;
			}
		}
		public int EndServMon
		{
			get { return endServMon; }
			set
			{
				setState();
				endServMon = value;
			}
		}
		public int PredId
		{
			get { return predId; }
			set
			{
				setState();
				predId = value;
			}
		}
		public int MappingProd
		{
			get { return mappingProd; }
			set
			{
				setState();
				mappingProd = value;
			}
		}
		public bool IsAgentVisible
		{
			get { return isAgentVisible; }
			set
			{
				setState();
				isAgentVisible = value;
			}
		}
		public string WebDescription
		{
			get { return webDescription; }
			set
			{
				setState();
				webDescription = value;
			}
		}
		public string OrdSumryStartMon2
		{
			get { return ordSumryStartMon2; }
		}
		public bool IsPreselectedWebOrderL2 
		{ 
			get { return isPreselectedWebOrderL2; }
		//	set {isPreselectedWebOrderL2 = value; }
		}        
		public bool SuppressZeroPriceProd 
		{ 
			get { return suppressZeroPriceProd; }
		//	set {suppressZeroPriceProd = value; }
		}        
		public bool IsExcludedFromTotalL2 
		{ 
			get { return isExcludedFromTotalL2; }
		//	set {isExcludedFromTotalL2 = value; }
		}
        
		public bool DisplayUnclickMessage 
		{ 
			get { return displayUnclickMessage; }
		}        
		public bool IsRestricted
		{
			get { return  isRestricted; }
		}
		/*        Constructors			*/
		public Product()
		{
			sql = new ProductSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public Product(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
        
		/*        Methods        */
		protected override SqlGateway loadSql()
		{
			return new ProductSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static Product find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(Product.getKey(id)))
				return (Product)uow.Imap.find(Product.getKey(id));
            
			Product cls = new Product();
			cls.uow = uow;
			cls.id = id;
			cls = (Product)DomainObj.addToIMap(uow, getOne(((ProductSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static Product[] getAll(UOW uow)
		{
			Product[] objs = (Product[])DomainObj.addToIMap(uow, (new ProductSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}
		public static bool AllowRestrictedProd(UOW uow, RestrictedProdRule[] rules, int prod)
		{
			if (!Product.find(uow, prod).IsRestricted)
				return true;

			for (int i = 0; i < rules.Length; i++)
				if (rules[i].EnabledProd == prod)
					return true;

			return false;
		}
		/*		Implementation		*/
		static Product getOne(Product[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(Product src, Product tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
			tar.prodType = src.prodType;
			tar.prodName = src.prodName;
			tar.billText = src.billText;
			tar.prodCode = src.prodCode;
			tar.oldPriceCode = src.oldPriceCode;
			tar.prodSubClass = src.prodSubClass;
			tar.isComponentOnly = src.isComponentOnly;
			tar.isBillable = src.isBillable;
			tar.isProvisionable = src.isProvisionable;
			tar.isProvViaMapping = src.isProvViaMapping;
			tar.description = src.description;
			tar.eligibilityCriteria = src.eligibilityCriteria;
			tar.provCategory = src.provCategory;
			tar.supplier = src.supplier;
			tar.vendor = src.vendor;
			tar.taxCode = src.taxCode;
			tar.isTaxExempt = src.isTaxExempt;
			tar.status = src.status;
			tar.prodCategory  = src.prodCategory;
			tar.startDate = src.startDate;
			tar.endDate = src.endDate;
			tar.acctCode = src.acctCode;
			tar.compCode = src.compCode;
			tar.deptCode = src.deptCode;
			tar.startServMon = src.startServMon;
			tar.endServMon = src.endServMon;
			tar.predId = src.predId;
			tar.mappingProd = src.mappingProd;
			tar.isAgentVisible = src.isAgentVisible;
			tar.webDescription = src.webDescription;
			tar.isPreselectedWebOrderL2 = src.isPreselectedWebOrderL2;
			tar.suppressZeroPriceProd = src.suppressZeroPriceProd;
			tar.isExcludedFromTotalL2 = src.isExcludedFromTotalL2;
			tar.displayUnclickMessage = src.displayUnclickMessage;
			tar.isRestricted = src.isRestricted;

			tar.rowState = src.rowState;
		}
 
		/*		SQL		*/
		[Serializable]
			class ProductSQL : SqlGateway
		{
			public Product[] getKey(Product rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spProduct_Get_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				Product rec = (Product)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spProduct_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				Product rec = (Product)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spProduct_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				Product rec = (Product)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spProduct_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public Product[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spProduct_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, Product rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 15).Value = rec.prodType;
 
				cmd.Parameters.Add("@ProdName", SqlDbType.VarChar, 50).Value = rec.prodName;
 
				if (rec.billText == null)
					cmd.Parameters.Add("@BillText", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.BillText.Length == 0)
						cmd.Parameters.Add("@BillText", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@BillText", SqlDbType.VarChar, 50).Value = rec.billText;
				}
 
				if (rec.prodCode == null)
					cmd.Parameters.Add("@ProdCode", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.ProdCode.Length == 0)
						cmd.Parameters.Add("@ProdCode", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ProdCode", SqlDbType.VarChar, 20).Value = rec.prodCode;
				}
 
				if (rec.oldPriceCode == null)
					cmd.Parameters.Add("@OldPriceCode", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.OldPriceCode.Length == 0)
						cmd.Parameters.Add("@OldPriceCode", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@OldPriceCode", SqlDbType.VarChar, 50).Value = rec.oldPriceCode;
				}
 
				if (rec.prodSubClass == null)
					cmd.Parameters.Add("@ProdSubClass", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.ProdSubClass.Length == 0)
						cmd.Parameters.Add("@ProdSubClass", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ProdSubClass", SqlDbType.VarChar, 15).Value = rec.prodSubClass;
				}
 
				cmd.Parameters.Add("@IsComponentOnly", SqlDbType.Char, 1).Value 
					= (rec.isComponentOnly == true) ? "T" : "F";
 
				cmd.Parameters.Add("@IsBillable", SqlDbType.Char, 1).Value 
					= (rec.isBillable == true) ? "T" : "F";
 
				cmd.Parameters.Add("@IsProvisionable", SqlDbType.Char, 1).Value 
					= (rec.isProvisionable == true) ? "T" : "F";
 
				cmd.Parameters.Add("@IsProvViaMapping", SqlDbType.Char, 1).Value 
					= (rec.isProvViaMapping == true) ? "T" : "F";
 
				if (rec.description == null)
					cmd.Parameters.Add("@Description", SqlDbType.VarChar, 4000).Value = DBNull.Value;
				else
				{
					if (rec.Description.Length == 0)
						cmd.Parameters.Add("@Description", SqlDbType.VarChar, 4000).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Description", SqlDbType.VarChar, 4000).Value = rec.description;
				}
 
				if (rec.eligibilityCriteria == null)
					cmd.Parameters.Add("@EligibilityCriteria", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.EligibilityCriteria.Length == 0)
						cmd.Parameters.Add("@EligibilityCriteria", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@EligibilityCriteria", SqlDbType.VarChar, 50).Value 
							= rec.eligibilityCriteria;
				}
 
				cmd.Parameters.Add("@ProvCategory", SqlDbType.VarChar, 15).Value = rec.provCategory;
                
				// Numeric, nullable foreign key treatment:
				if (rec.Supplier == 0)
					cmd.Parameters.Add("@Supplier", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Supplier", SqlDbType.Int, 0).Value = rec.supplier;
                
				// Numeric, nullable foreign key treatment:
				if (rec.Vendor == 0)
					cmd.Parameters.Add("@Vendor", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Vendor", SqlDbType.Int, 0).Value = rec.vendor;
 
				if (rec.taxCode == null)
					cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.TaxCode.Length == 0)
						cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 50).Value = rec.taxCode;
				}
 
				cmd.Parameters.Add("@IsTaxExempt", SqlDbType.Char, 1).Value 
					= (rec.isTaxExempt == true) ? "T" : "F";
 
				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.Status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
				}
				if (rec.prodCategory == null)
					cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.ProdCategory.Length == 0)
						cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 15).Value = rec.prodCategory;
				}
				if (rec.startDate == DateTime.MinValue)
					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
				if (rec.endDate == DateTime.MinValue)
					cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
 
				if (rec.acctCode == null)
					cmd.Parameters.Add("@AcctCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.AcctCode.Length == 0)
						cmd.Parameters.Add("@AcctCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@AcctCode", SqlDbType.VarChar, 10).Value = rec.acctCode;
				}
 
				if (rec.compCode == null)
					cmd.Parameters.Add("@CompCode", SqlDbType.VarChar, 4).Value = DBNull.Value;
				else
				{
					if (rec.CompCode.Length == 0)
						cmd.Parameters.Add("@CompCode", SqlDbType.VarChar, 4).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@CompCode", SqlDbType.VarChar, 4).Value = rec.compCode;
				}
 
				if (rec.deptCode == null)
					cmd.Parameters.Add("@DeptCode", SqlDbType.VarChar, 4).Value = DBNull.Value;
				else
				{
					if (rec.DeptCode.Length == 0)
						cmd.Parameters.Add("@DeptCode", SqlDbType.VarChar, 4).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DeptCode", SqlDbType.VarChar, 4).Value = rec.deptCode;
				}
				cmd.Parameters.Add("@StartServMon", SqlDbType.Int, 0).Value = rec.startServMon;
				cmd.Parameters.Add("@EndServMon", SqlDbType.Int, 0).Value = rec.endServMon;
				cmd.Parameters.Add("@PredId", SqlDbType.Int, 0).Value = rec.predId;
                
				// Numeric, nullable foreign key treatment:
				if (rec.MappingProd == 0)
					cmd.Parameters.Add("@MappingProd", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@MappingProd", SqlDbType.Int, 0).Value = rec.mappingProd;
 
				cmd.Parameters.Add("@IsAgentVisible", SqlDbType.Char, 1).Value 
					= (rec.isAgentVisible == true) ? "T" : "F";

				if (rec.webDescription == null)
					cmd.Parameters.Add("@WebDescription", SqlDbType.VarChar, 3000).Value = DBNull.Value;
				else
				{
					if (rec.WebDescription.Length == 0)
						cmd.Parameters.Add("@WebDescription", SqlDbType.VarChar, 3000).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@WebDescription", SqlDbType.VarChar, 3000).Value 
							= rec.webDescription;
				}

				cmd.Parameters.Add("@IsPreselectedWebOrderL2", SqlDbType.Bit, 1).Value 
					= rec.isPreselectedWebOrderL2;

				cmd.Parameters.Add("@SuppressZeroPriceProd", SqlDbType.Bit, 1).Value 
					= rec.suppressZeroPriceProd;
				
				cmd.Parameters.Add("@IsExcludedFromTotalL2", SqlDbType.Bit, 1).Value 
					= rec.isExcludedFromTotalL2;

				cmd.Parameters.Add("@DisplayUnclickMessage", SqlDbType.Bit, 1).Value 
					= rec.displayUnclickMessage;

				cmd.Parameters.Add("@IsRestricted", SqlDbType.Bit, 1).Value 
					= rec.isRestricted;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				Product rec = new Product();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["ProdType"] != DBNull.Value)
					rec.prodType = (string) rdr["ProdType"];
 
				if (rdr["ProdName"] != DBNull.Value)
					rec.prodName = (string) rdr["ProdName"];
 
				if (rdr["BillText"] != DBNull.Value)
					rec.billText = (string) rdr["BillText"];
 
				if (rdr["ProdCode"] != DBNull.Value)
					rec.prodCode = (string) rdr["ProdCode"];
 
				if (rdr["OldPriceCode"] != DBNull.Value)
					rec.oldPriceCode = (string) rdr["OldPriceCode"];
 
				if (rdr["ProdSubClass"] != DBNull.Value)
					rec.prodSubClass = (string) rdr["ProdSubClass"];
 
				if (rdr["IsComponentOnly"] != DBNull.Value)
					rec.isComponentOnly = (string) rdr["IsComponentOnly"] == "T" ?  true : false;
 
				if (rdr["IsBillable"] != DBNull.Value)
					rec.isBillable = (string) rdr["IsBillable"] == "T" ?  true : false;
 
				if (rdr["IsProvisionable"] != DBNull.Value)
					rec.isProvisionable = (string) rdr["IsProvisionable"] == "T" ?  true : false;
 
				if (rdr["IsProvViaMapping"] != DBNull.Value)
					rec.isProvViaMapping = (string) rdr["IsProvViaMapping"] == "T" ?  true : false;
 
				if (rdr["Description"] != DBNull.Value)
					rec.description = (string) rdr["Description"];
 
				if (rdr["EligibilityCriteria"] != DBNull.Value)
					rec.eligibilityCriteria = (string) rdr["EligibilityCriteria"];
 
				if (rdr["ProvCategory"] != DBNull.Value)
					rec.provCategory = (string) rdr["ProvCategory"];
 
				if (rdr["Supplier"] != DBNull.Value)
					rec.supplier = (int) rdr["Supplier"];
 
				if (rdr["Vendor"] != DBNull.Value)
					rec.vendor = (int) rdr["Vendor"];
 
				if (rdr["TaxCode"] != DBNull.Value)
					rec.taxCode = (string) rdr["TaxCode"];
 
				if (rdr["IsTaxExempt"] != DBNull.Value)
					rec.isTaxExempt = (string) rdr["IsTaxExempt"] == "T" ?  true : false;
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];

				if (rdr["ProdCategory"] != DBNull.Value)
					rec.prodCategory = (string) rdr["ProdCategory"];

				if (rdr["StartDate"] != DBNull.Value)
					rec.startDate = (DateTime) rdr["StartDate"];
 
				if (rdr["EndDate"] != DBNull.Value)
					rec.endDate = (DateTime) rdr["EndDate"];
 
				if (rdr["AcctCode"] != DBNull.Value)
					rec.acctCode = (string) rdr["AcctCode"];
 
				if (rdr["CompCode"] != DBNull.Value)
					rec.compCode = (string) rdr["CompCode"];
 
				if (rdr["DeptCode"] != DBNull.Value)
					rec.deptCode = (string) rdr["DeptCode"];
 
				if (rdr["StartServMon"] != DBNull.Value)
					rec.startServMon = (int) rdr["StartServMon"];
 
				if (rdr["EndServMon"] != DBNull.Value)
					rec.endServMon = (int) rdr["EndServMon"];
 
				if (rdr["PredId"] != DBNull.Value)
					rec.predId = (int) rdr["PredId"];
 
				if (rdr["MappingProd"] != DBNull.Value)
					rec.mappingProd = (int) rdr["MappingProd"];
 
				if (rdr["IsAgentVisible"] != DBNull.Value)
					rec.isAgentVisible = (string) rdr["IsAgentVisible"] == "T" ?  true : false;
 
				if (rdr["WebDescription"] != DBNull.Value)
					rec.webDescription = (string) rdr["WebDescription"];

				if (rdr["OrdSumryStartMon2"] != DBNull.Value)
					rec.ordSumryStartMon2 = ((string) rdr["OrdSumryStartMon2"]).Trim().ToUpper();

				if (rdr["IsPreselectedWebOrderL2"] != DBNull.Value)
					rec.isPreselectedWebOrderL2 = (bool)rdr["IsPreselectedWebOrderL2"];

				if (rdr["SuppressZeroPriceProd"] != DBNull.Value)
					rec.suppressZeroPriceProd = (bool)rdr["suppressZeroPriceProd"];
				
				if (rdr["IsExcludedFromTotalL2"] != DBNull.Value)
					rec.isExcludedFromTotalL2 = (bool)rdr["isExcludedFromTotalL2"];
			
				if (rdr["DisplayUnclickMessage"] != DBNull.Value)
					rec.displayUnclickMessage = (bool)rdr["displayUnclickMessage"];

				if (rdr["IsRestricted"] != DBNull.Value)
					rec.isRestricted = (bool)rdr["IsRestricted"];

				rec.rowState = RowState.Clean;
				return rec;
			}
			Product[] convert(DomainObj[] objs)
			{
				Product[] acls  = new Product[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
	}
}