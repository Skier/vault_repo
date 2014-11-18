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
	public class CustAddress : DomainObj, IAddr2
	{
		#region Data
		static string iName = "CustAddress";
		int addressID;
		string adrStatus;
		AddressType adrType;
		string streetNum;
		string streetPrefix;
		string street;
		string streetType;
		string streetSuffix;
		string unit;
		string unitType;
		string city;
		string state;
		string zipcode;
		string cLLI;
		string nPANXX;
		#endregion
		#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, addressID.ToString()); }
		}
		public int AddressID
		{
			get { return addressID; }
		}
		public string AdrStatus
		{
			get { return adrStatus; }
			set
			{
				setState();
				adrStatus = value;
			}
		}
		public AddressType AddrType
		{
			get { return adrType; }
			set
			{
				setState();
				adrType = value;
			}
		}
		public string StreetNum
		{
			get { return streetNum; }
			set
			{
				setState();
				streetNum = value;
			}
		}
		public string StreetPrefix
		{
			get { return streetPrefix; }
			set
			{
				setState();
				streetPrefix = value;
			}
		}
		public string Street
		{
			get { return street; }
			set
			{
				setState();
				street = value;
			}
		}
		public string StreetType
		{
			get { return streetType; }
			set
			{
				setState();
				streetType = value;
			}
		}
		public string StreetSuffix
		{
			get { return streetSuffix; }
			set
			{
				setState();
				streetSuffix = value;
			}
		}
		public string Unit
		{
			get { return unit; }
			set
			{
				setState();
				unit = value;
			}
		}
		public string UnitType
		{
			get { return unitType; }
			set
			{
				setState();
				unitType = value;
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
		public string State
		{
			get { return state; }
			set
			{
				setState();
				state = value;
			}
		}
		public string Zipcode
		{
			get { return zipcode; }
			set
			{
				setState();
				zipcode = value;
			}
		}

        public string Address1
        {
            get 
            { 
                StringBuilder sb = new StringBuilder();

                AddFormAddr(streetNum, sb);
                AddFormAddr(streetPrefix, sb);
                AddFormAddr(street, sb);
                AddFormAddr(streetType, sb);
                AddFormAddr(streetSuffix, sb);
				
                return sb.ToString(); 
            }
        }

        public string Address2 
        {
            get 
            {
                StringBuilder sb = new StringBuilder();

                AddFormAddr(unitType, sb);
                AddFormAddr(unit, sb);
				
                return sb.ToString(); 
            }
        }
		
		public string FormattedStreetAddress
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				AddFormAddr(streetNum, sb);
				AddFormAddr(streetPrefix, sb);
				AddFormAddr(street, sb);
				AddFormAddr(streetType, sb);
                AddFormAddr(streetSuffix, sb);
				AddFormAddr(unitType, sb);
                AddFormAddr(unit, sb);
				
				return sb.ToString();				
			}

            set
            {
                IDeliveryAddressLineParser parser = DeliveryAddressLineParserFactory.Instance.Parse(value);

                if (parser is PostOfficeBoxAddressParser) {
                    PostOfficeBoxAddressParser pobap = (PostOfficeBoxAddressParser)parser;
                    street = "PO BOX " + pobap.Number;
                } else if (parser is StreetAddressParser) {
                    StreetAddressParser sap = (StreetAddressParser)parser;
                    streetNum = sap.PrimaryAddressNumber;
                    streetPrefix = sap.Predirectional;
                    street = sap.StreetName;
                    streetType = sap.Suffix;
                    streetSuffix = sap.Postdirectinal;
                    unitType = sap.SecondaryAddressIdentifier;
                    unit = sap.SecondaryAddressRange;
                } else {
                    throw new ApplicationException("Street address has unsupported format: [" + value + "].");
                }
            }
		}

		public string FormattedCityStateZip
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				AddFormAddr(city, sb);
				AddFormAddr(state, sb);
				AddFormAddr(zipcode, sb);
		
				return sb.ToString();
			}
		}
		public string CLLI
		{
			get { return cLLI; }
			set { cLLI = value; }
		}
		public string NPANXX
		{
			get { return nPANXX; }
			set { nPANXX = value; }
		}
#endregion
		#region Constructors
		public CustAddress()
		{
			sql = new CustAddressSQL();
			addressID = random.Next(Int32.MinValue, -1);
			priority = 9000;
			rowState = RowState.New;
		}
		public CustAddress(AddressType adrType) : this()
		{
			this.adrType = adrType;
		}
		public CustAddress(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
		#endregion
		/*        Methods        */
		protected override SqlGateway loadSql()
		{
			return new CustAddressSQL();
		}
		public override void checkExists()
		{
			if ((AddressID < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static IAddr2 find(UOW uow, int addressID)
		{
			if (uow.Imap.keyExists(CustAddress.getKey(addressID)))
				return (CustAddress)uow.Imap.find(CustAddress.getKey(addressID));

			return (CustAddress)DomainObj.addToIMap(uow, (DomainObj)getOne(new CustAddressSQL().getKey(uow, addressID)));
		}
		public static IAddr2[] getAll(UOW uow)
		{
			CustAddress[] objs = (CustAddress[])DomainObj.addToIMap(uow, (new CustAddressSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int addressID)
		{
			return new Key(iName, addressID.ToString());
		}

		/*		Implementation		*/
		static IAddr getOne(CustAddress[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(CustAddress src, CustAddress tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.addressID = src.addressID;
			tar.adrStatus = src.adrStatus;
			tar.adrType = src.adrType;
			tar.streetNum = src.streetNum;
			tar.streetPrefix = src.streetPrefix;
			tar.street = src.street;
			tar.streetType = src.streetType;
			tar.streetSuffix = src.streetSuffix;
			tar.unit = src.unit;
			tar.unitType = src.unitType;
			tar.city = src.city;
			tar.state = src.state;
			tar.zipcode = src.zipcode;
			tar.cLLI = src.cLLI;
			tar.nPANXX = src.nPANXX;

			tar.state = src.state;
		}
 		void AddFormAddr(string attr, StringBuilder sb)
		{
			string spacer = " "; 

			if (attr == null)
				return;

			if (attr.Trim().Length == 0)
				return;

			if (sb.Length > 0)
				sb.Append(spacer);
	
			sb.Append(attr.Trim());
		}
		/*		SQL		*/
		[Serializable]
			class CustAddressSQL : SqlGateway
		{
			public CustAddress[] getKey(CustAddress rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustAddress_Get_Id";
				cmd.Parameters.Add("@AddressID", SqlDbType.Int, 0).Value = rec.addressID;
				return convert(execReader(cmd));
			}
			public CustAddress[] getKey(UOW uow, int id)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustAddress_Get_Id";
				cmd.Parameters.Add("@AddressID", SqlDbType.Int, 0).Value = id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				CustAddress rec = (CustAddress)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustAddress_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@AddressID"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.addressID = (int)cmd.Parameters["@AddressID"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				CustAddress rec = (CustAddress)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustAddress_Del_Id";
				cmd.Parameters.Add("@AddressID", SqlDbType.Int, 0).Value = rec.addressID;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				CustAddress rec = (CustAddress)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustAddress_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public CustAddress[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustAddress_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, CustAddress rec)
			{
				cmd.Parameters.Add("@AddressID", SqlDbType.Int, 0).Value = rec.addressID;
 
				if (rec.adrStatus == null)
					cmd.Parameters.Add("@AdrStatus", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.AdrStatus.Length == 0)
						cmd.Parameters.Add("@AdrStatus", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@AdrStatus", SqlDbType.VarChar, 20).Value = rec.adrStatus;
				}
 
				cmd.Parameters.Add("@AdrType", SqlDbType.Int, 4).Value = rec.adrType;
 
				if (rec.streetNum == null)
					cmd.Parameters.Add("@StreetNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.StreetNum.Length == 0)
						cmd.Parameters.Add("@StreetNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StreetNum", SqlDbType.VarChar, 10).Value = rec.streetNum;
				}
 
				if (rec.streetPrefix == null)
					cmd.Parameters.Add("@StreetPrefix", SqlDbType.VarChar, 7).Value = DBNull.Value;
				else
				{
					if (rec.StreetPrefix.Length == 0)
						cmd.Parameters.Add("@StreetPrefix", SqlDbType.VarChar, 7).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StreetPrefix", SqlDbType.VarChar, 7).Value = rec.streetPrefix;
				}
 
				if (rec.street == null)
					cmd.Parameters.Add("@Street", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.Street.Length == 0)
						cmd.Parameters.Add("@Street", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Street", SqlDbType.VarChar, 50).Value = rec.street;
				}
 
				if (rec.streetType == null)
					cmd.Parameters.Add("@StreetType", SqlDbType.VarChar, 4).Value = DBNull.Value;
				else
				{
					if (rec.StreetType.Length == 0)
						cmd.Parameters.Add("@StreetType", SqlDbType.VarChar, 4).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StreetType", SqlDbType.VarChar, 4).Value = rec.streetType;
				}
 
				if (rec.streetSuffix == null)
					cmd.Parameters.Add("@StreetSuffix", SqlDbType.VarChar, 4).Value = DBNull.Value;
				else
				{
					if (rec.StreetSuffix.Length == 0)
						cmd.Parameters.Add("@StreetSuffix", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StreetSuffix", SqlDbType.VarChar, 50).Value = rec.streetSuffix;
				}
 
				if (rec.unit == null)
					cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 8).Value = DBNull.Value;
				else
				{
					if (rec.Unit.Length == 0)
						cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 8).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 8).Value = rec.unit;
				}
 
				if (rec.unitType == null)
					cmd.Parameters.Add("@UnitType", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.UnitType.Length == 0)
						cmd.Parameters.Add("@UnitType", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@UnitType", SqlDbType.VarChar, 10).Value = rec.unitType;
				}
 
				if (rec.city == null)
					cmd.Parameters.Add("@City", SqlDbType.VarChar, 28).Value = DBNull.Value;
				else
				{
					if (rec.City.Length == 0)
						cmd.Parameters.Add("@City", SqlDbType.VarChar, 28).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@City", SqlDbType.VarChar, 28).Value = rec.city;
				}
 
				if (rec.state == null)
					cmd.Parameters.Add("@State", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.State.Length == 0)
						cmd.Parameters.Add("@State", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@State", SqlDbType.VarChar, 15).Value = rec.state;
				}
 
				if (rec.zipcode == null)
					cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Zipcode.Length == 0)
						cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar, 10).Value = rec.zipcode;
				}
				if (rec.cLLI == null)
					cmd.Parameters.Add("@CLLI", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.cLLI.Length == 0)
                cmd.Parameters.Add("@CLLI", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@CLLI", SqlDbType.VarChar, 20).Value = rec.cLLI;
				}
				if (rec.nPANXX == null)
                cmd.Parameters.Add("@NPANXX", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.nPANXX.Length == 0)
						cmd.Parameters.Add("@NPANXX", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@NPANXX", SqlDbType.VarChar, 20).Value = rec.nPANXX;
				}
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
	//			CustAddress rec = AddrFactory.GetAddress((AddressType) ((int) rdr["AdrType"]));

				CustAddress rec = new CustAddress();

				if (rdr["AddressID"] != DBNull.Value)
					rec.addressID = (int) rdr["AddressID"];
 
				if (rdr["AdrStatus"] != DBNull.Value)
					rec.adrStatus = (string) rdr["AdrStatus"];
 
				if (rdr["StreetNum"] != DBNull.Value)
					rec.streetNum = (string) rdr["StreetNum"];
 
				if (rdr["StreetPrefix"] != DBNull.Value)
					rec.streetPrefix = (string) rdr["StreetPrefix"];
 
				if (rdr["Street"] != DBNull.Value)
					rec.street = (string) rdr["Street"];
 
				if (rdr["StreetType"] != DBNull.Value)
					rec.streetType = (string) rdr["StreetType"];
 
				if (rdr["StreetSuffix"] != DBNull.Value)
					rec.streetSuffix = (string) rdr["StreetSuffix"];
 
				if (rdr["Unit"] != DBNull.Value)
					rec.unit = (string) rdr["Unit"];
 
				if (rdr["UnitType"] != DBNull.Value)
					rec.unitType = (string) rdr["UnitType"];
 
				if (rdr["City"] != DBNull.Value)
					rec.city = (string) rdr["City"];
 
				if (rdr["State"] != DBNull.Value)
					rec.state = (string) rdr["State"];
 
				if (rdr["Zipcode"] != DBNull.Value)
					rec.zipcode = (string) rdr["Zipcode"];
 
				if (rdr["CLLI"] != DBNull.Value)
					rec.cLLI = (string) rdr["CLLI"];

				if (rdr["NPANXX"] != DBNull.Value)
					rec.nPANXX = (string) rdr["NPANXX"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			CustAddress[] convert(DomainObj[] objs)
			{
				CustAddress[] acls  = new CustAddress[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
				
	}
}