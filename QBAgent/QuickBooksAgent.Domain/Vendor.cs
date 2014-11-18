using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class Vendor : ICounterField
    {
        #region Default constructor

        public Vendor()
        {
            this.EntityState = new EntityState();
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_vendorId; }
            set { m_vendorId = value; }
        }

        public string CounterName
        {
            get { return "Vendor"; }
        }

        #endregion        

        #region FindBy Entity State

        private const string SqlFindByEntityState =
            @"SELECT VendorId, ModifiedVendorId, QuickBooksListId, EntityStateId, 
                EditSequence, Name, CompanyName, Salutation, FirstName, MiddleName, 
                LastName, Suffix, Addr1, Addr2, Addr3, Addr4, City, State, PostalCode, 
                Country, Phone, Mobile, Pager, AltPhone, Fax, Email, NameOnCheck, 
                VendorTaxIdent, IsVendorEligibleFor1099, Balance 
            FROM Vendor
                WHERE EntityStateId = @EntityStateId";

        public static List<Vendor> FindBy(EntityState entityState)
        {
            List<Vendor> vendorList = new List<Vendor>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        vendorList.Add(Load(dataReader));
                    }
                }
            }
            return vendorList;
        }

        #endregion        
        
        #region FindBy QuickBooksId

        private const string SqlFindByQuickBooksId =
            @"SELECT VendorId, ModifiedVendorId, QuickBooksListId, EntityStateId, 
                EditSequence, Name, CompanyName, Salutation, FirstName, MiddleName, 
                LastName, Suffix, Addr1, Addr2, Addr3, Addr4, City, State, PostalCode, 
                Country, Phone, Mobile, Pager, AltPhone, Fax, Email, NameOnCheck, 
                VendorTaxIdent, IsVendorEligibleFor1099, Balance 
            FROM Vendor
                WHERE QuickBooksListId = @QuickBooksListId";

        public static Vendor FindBy(int quickBooksListId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksListId", quickBooksListId.ToString());

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("Vendor with List ID "
                        + quickBooksListId.ToString() + " not found");
                }
            }
        }

        #endregion                

        #region ToString

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Equals & GetHashCode

        public override int GetHashCode()
        {
            return m_vendorId;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Vendor vendor = obj as Vendor;
            if (vendor == null) return false;
            if (m_vendorId != vendor.m_vendorId) return false;
            return true;
        }

        #endregion
    }
}
  