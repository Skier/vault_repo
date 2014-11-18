
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class QbCustomer : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbCustomer ( " +
      
        " CustomerId, " +
      
        " ProjectId, " +
      
        " ListId, " +
      
        " EditSequence, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " SubLevel, " +
      
        " Name, " +
      
        " FullName, " +
      
        " IsActive, " +
      
        " CompanyName, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Phone1, " +
      
        " Phone2, " +
      
        " Email, " +
      
        " BillingAddressAddr1, " +
      
        " BillingAddressAddr2, " +
      
        " BillingAddressCity, " +
      
        " BillingAddressState, " +
      
        " BillingAddressPostalCode, " +
      
        " BillingAddressCountry, " +
      
        " BillingAddressNote, " +
      
        " ShippingAddressAddr1, " +
      
        " ShippingAddressAddr2, " +
      
        " ShippingAddressCity, " +
      
        " ShippingAddressState, " +
      
        " ShippingAddressPostalCode, " +
      
        " ShippingAddressCountry, " +
      
        " ShippingAddressNote, " +
      
        " Balance, " +
      
        " QbCustomerTypeListId, " +
      
        " QbSalesRepListId " +
      
      ") Values (" +
      
        " ?CustomerId, " +
      
        " ?ProjectId, " +
      
        " ?ListId, " +
      
        " ?EditSequence, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?SubLevel, " +
      
        " ?Name, " +
      
        " ?FullName, " +
      
        " ?IsActive, " +
      
        " ?CompanyName, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?Phone1, " +
      
        " ?Phone2, " +
      
        " ?Email, " +
      
        " ?BillingAddressAddr1, " +
      
        " ?BillingAddressAddr2, " +
      
        " ?BillingAddressCity, " +
      
        " ?BillingAddressState, " +
      
        " ?BillingAddressPostalCode, " +
      
        " ?BillingAddressCountry, " +
      
        " ?BillingAddressNote, " +
      
        " ?ShippingAddressAddr1, " +
      
        " ?ShippingAddressAddr2, " +
      
        " ?ShippingAddressCity, " +
      
        " ?ShippingAddressState, " +
      
        " ?ShippingAddressPostalCode, " +
      
        " ?ShippingAddressCountry, " +
      
        " ?ShippingAddressNote, " +
      
        " ?Balance, " +
      
        " ?QbCustomerTypeListId, " +
      
        " ?QbSalesRepListId " +
      
      ")";

      public static void Insert(QbCustomer qbCustomer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", qbCustomer.CustomerId);
      
        Database.PutParameter(dbCommand,"?ProjectId", qbCustomer.ProjectId);
      
        Database.PutParameter(dbCommand,"?ListId", qbCustomer.ListId);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCustomer.EditSequence);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbCustomer.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbCustomer.TimeModified);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbCustomer.SubLevel);
      
        Database.PutParameter(dbCommand,"?Name", qbCustomer.Name);
      
        Database.PutParameter(dbCommand,"?FullName", qbCustomer.FullName);
      
        Database.PutParameter(dbCommand,"?IsActive", qbCustomer.IsActive);
      
        Database.PutParameter(dbCommand,"?CompanyName", qbCustomer.CompanyName);
      
        Database.PutParameter(dbCommand,"?FirstName", qbCustomer.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", qbCustomer.LastName);
      
        Database.PutParameter(dbCommand,"?Phone1", qbCustomer.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", qbCustomer.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", qbCustomer.Email);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr1", qbCustomer.BillingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr2", qbCustomer.BillingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?BillingAddressCity", qbCustomer.BillingAddressCity);
      
        Database.PutParameter(dbCommand,"?BillingAddressState", qbCustomer.BillingAddressState);
      
        Database.PutParameter(dbCommand,"?BillingAddressPostalCode", qbCustomer.BillingAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?BillingAddressCountry", qbCustomer.BillingAddressCountry);
      
        Database.PutParameter(dbCommand,"?BillingAddressNote", qbCustomer.BillingAddressNote);
      
        Database.PutParameter(dbCommand,"?ShippingAddressAddr1", qbCustomer.ShippingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?ShippingAddressAddr2", qbCustomer.ShippingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?ShippingAddressCity", qbCustomer.ShippingAddressCity);
      
        Database.PutParameter(dbCommand,"?ShippingAddressState", qbCustomer.ShippingAddressState);
      
        Database.PutParameter(dbCommand,"?ShippingAddressPostalCode", qbCustomer.ShippingAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?ShippingAddressCountry", qbCustomer.ShippingAddressCountry);
      
        Database.PutParameter(dbCommand,"?ShippingAddressNote", qbCustomer.ShippingAddressNote);
      
        Database.PutParameter(dbCommand,"?Balance", qbCustomer.Balance);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", qbCustomer.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", qbCustomer.QbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbCustomer.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(QbCustomer qbCustomer)
      {
        Insert(qbCustomer, null);
      }


      public static void Insert(List<QbCustomer>  qbCustomerList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbCustomer qbCustomer in  qbCustomerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", qbCustomer.CustomerId);
      
        Database.PutParameter(dbCommand,"?ProjectId", qbCustomer.ProjectId);
      
        Database.PutParameter(dbCommand,"?ListId", qbCustomer.ListId);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCustomer.EditSequence);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbCustomer.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbCustomer.TimeModified);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbCustomer.SubLevel);
      
        Database.PutParameter(dbCommand,"?Name", qbCustomer.Name);
      
        Database.PutParameter(dbCommand,"?FullName", qbCustomer.FullName);
      
        Database.PutParameter(dbCommand,"?IsActive", qbCustomer.IsActive);
      
        Database.PutParameter(dbCommand,"?CompanyName", qbCustomer.CompanyName);
      
        Database.PutParameter(dbCommand,"?FirstName", qbCustomer.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", qbCustomer.LastName);
      
        Database.PutParameter(dbCommand,"?Phone1", qbCustomer.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", qbCustomer.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", qbCustomer.Email);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr1", qbCustomer.BillingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr2", qbCustomer.BillingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?BillingAddressCity", qbCustomer.BillingAddressCity);
      
        Database.PutParameter(dbCommand,"?BillingAddressState", qbCustomer.BillingAddressState);
      
        Database.PutParameter(dbCommand,"?BillingAddressPostalCode", qbCustomer.BillingAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?BillingAddressCountry", qbCustomer.BillingAddressCountry);
      
        Database.PutParameter(dbCommand,"?BillingAddressNote", qbCustomer.BillingAddressNote);
      
        Database.PutParameter(dbCommand,"?ShippingAddressAddr1", qbCustomer.ShippingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?ShippingAddressAddr2", qbCustomer.ShippingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?ShippingAddressCity", qbCustomer.ShippingAddressCity);
      
        Database.PutParameter(dbCommand,"?ShippingAddressState", qbCustomer.ShippingAddressState);
      
        Database.PutParameter(dbCommand,"?ShippingAddressPostalCode", qbCustomer.ShippingAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?ShippingAddressCountry", qbCustomer.ShippingAddressCountry);
      
        Database.PutParameter(dbCommand,"?ShippingAddressNote", qbCustomer.ShippingAddressNote);
      
        Database.PutParameter(dbCommand,"?Balance", qbCustomer.Balance);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", qbCustomer.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", qbCustomer.QbSalesRepListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?CustomerId",qbCustomer.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?ProjectId",qbCustomer.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?ListId",qbCustomer.ListId);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbCustomer.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbCustomer.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbCustomer.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?SubLevel",qbCustomer.SubLevel);
      
        Database.UpdateParameter(dbCommand,"?Name",qbCustomer.Name);
      
        Database.UpdateParameter(dbCommand,"?FullName",qbCustomer.FullName);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbCustomer.IsActive);
      
        Database.UpdateParameter(dbCommand,"?CompanyName",qbCustomer.CompanyName);
      
        Database.UpdateParameter(dbCommand,"?FirstName",qbCustomer.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",qbCustomer.LastName);
      
        Database.UpdateParameter(dbCommand,"?Phone1",qbCustomer.Phone1);
      
        Database.UpdateParameter(dbCommand,"?Phone2",qbCustomer.Phone2);
      
        Database.UpdateParameter(dbCommand,"?Email",qbCustomer.Email);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressAddr1",qbCustomer.BillingAddressAddr1);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressAddr2",qbCustomer.BillingAddressAddr2);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressCity",qbCustomer.BillingAddressCity);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressState",qbCustomer.BillingAddressState);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressPostalCode",qbCustomer.BillingAddressPostalCode);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressCountry",qbCustomer.BillingAddressCountry);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressNote",qbCustomer.BillingAddressNote);
      
        Database.UpdateParameter(dbCommand,"?ShippingAddressAddr1",qbCustomer.ShippingAddressAddr1);
      
        Database.UpdateParameter(dbCommand,"?ShippingAddressAddr2",qbCustomer.ShippingAddressAddr2);
      
        Database.UpdateParameter(dbCommand,"?ShippingAddressCity",qbCustomer.ShippingAddressCity);
      
        Database.UpdateParameter(dbCommand,"?ShippingAddressState",qbCustomer.ShippingAddressState);
      
        Database.UpdateParameter(dbCommand,"?ShippingAddressPostalCode",qbCustomer.ShippingAddressPostalCode);
      
        Database.UpdateParameter(dbCommand,"?ShippingAddressCountry",qbCustomer.ShippingAddressCountry);
      
        Database.UpdateParameter(dbCommand,"?ShippingAddressNote",qbCustomer.ShippingAddressNote);
      
        Database.UpdateParameter(dbCommand,"?Balance",qbCustomer.Balance);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerTypeListId",qbCustomer.QbCustomerTypeListId);
      
        Database.UpdateParameter(dbCommand,"?QbSalesRepListId",qbCustomer.QbSalesRepListId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbCustomer.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<QbCustomer>  qbCustomerList)
      {
        Insert(qbCustomerList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbCustomer Set "
      
        + " CustomerId = ?CustomerId, "
      
        + " ProjectId = ?ProjectId, "
      
        + " ListId = ?ListId, "
      
        + " EditSequence = ?EditSequence, "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " SubLevel = ?SubLevel, "
      
        + " Name = ?Name, "
      
        + " FullName = ?FullName, "
      
        + " IsActive = ?IsActive, "
      
        + " CompanyName = ?CompanyName, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " Phone1 = ?Phone1, "
      
        + " Phone2 = ?Phone2, "
      
        + " Email = ?Email, "
      
        + " BillingAddressAddr1 = ?BillingAddressAddr1, "
      
        + " BillingAddressAddr2 = ?BillingAddressAddr2, "
      
        + " BillingAddressCity = ?BillingAddressCity, "
      
        + " BillingAddressState = ?BillingAddressState, "
      
        + " BillingAddressPostalCode = ?BillingAddressPostalCode, "
      
        + " BillingAddressCountry = ?BillingAddressCountry, "
      
        + " BillingAddressNote = ?BillingAddressNote, "
      
        + " ShippingAddressAddr1 = ?ShippingAddressAddr1, "
      
        + " ShippingAddressAddr2 = ?ShippingAddressAddr2, "
      
        + " ShippingAddressCity = ?ShippingAddressCity, "
      
        + " ShippingAddressState = ?ShippingAddressState, "
      
        + " ShippingAddressPostalCode = ?ShippingAddressPostalCode, "
      
        + " ShippingAddressCountry = ?ShippingAddressCountry, "
      
        + " ShippingAddressNote = ?ShippingAddressNote, "
      
        + " Balance = ?Balance, "
      
        + " QbCustomerTypeListId = ?QbCustomerTypeListId, "
      
        + " QbSalesRepListId = ?QbSalesRepListId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbCustomer qbCustomer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbCustomer.ID);
      
        Database.PutParameter(dbCommand,"?CustomerId", qbCustomer.CustomerId);
      
        Database.PutParameter(dbCommand,"?ProjectId", qbCustomer.ProjectId);
      
        Database.PutParameter(dbCommand,"?ListId", qbCustomer.ListId);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCustomer.EditSequence);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbCustomer.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbCustomer.TimeModified);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbCustomer.SubLevel);
      
        Database.PutParameter(dbCommand,"?Name", qbCustomer.Name);
      
        Database.PutParameter(dbCommand,"?FullName", qbCustomer.FullName);
      
        Database.PutParameter(dbCommand,"?IsActive", qbCustomer.IsActive);
      
        Database.PutParameter(dbCommand,"?CompanyName", qbCustomer.CompanyName);
      
        Database.PutParameter(dbCommand,"?FirstName", qbCustomer.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", qbCustomer.LastName);
      
        Database.PutParameter(dbCommand,"?Phone1", qbCustomer.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", qbCustomer.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", qbCustomer.Email);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr1", qbCustomer.BillingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr2", qbCustomer.BillingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?BillingAddressCity", qbCustomer.BillingAddressCity);
      
        Database.PutParameter(dbCommand,"?BillingAddressState", qbCustomer.BillingAddressState);
      
        Database.PutParameter(dbCommand,"?BillingAddressPostalCode", qbCustomer.BillingAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?BillingAddressCountry", qbCustomer.BillingAddressCountry);
      
        Database.PutParameter(dbCommand,"?BillingAddressNote", qbCustomer.BillingAddressNote);
      
        Database.PutParameter(dbCommand,"?ShippingAddressAddr1", qbCustomer.ShippingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?ShippingAddressAddr2", qbCustomer.ShippingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?ShippingAddressCity", qbCustomer.ShippingAddressCity);
      
        Database.PutParameter(dbCommand,"?ShippingAddressState", qbCustomer.ShippingAddressState);
      
        Database.PutParameter(dbCommand,"?ShippingAddressPostalCode", qbCustomer.ShippingAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?ShippingAddressCountry", qbCustomer.ShippingAddressCountry);
      
        Database.PutParameter(dbCommand,"?ShippingAddressNote", qbCustomer.ShippingAddressNote);
      
        Database.PutParameter(dbCommand,"?Balance", qbCustomer.Balance);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", qbCustomer.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", qbCustomer.QbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbCustomer qbCustomer)
      {
        Update(qbCustomer, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " CustomerId, "
      
        + " ProjectId, "
      
        + " ListId, "
      
        + " EditSequence, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " SubLevel, "
      
        + " Name, "
      
        + " FullName, "
      
        + " IsActive, "
      
        + " CompanyName, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " BillingAddressAddr1, "
      
        + " BillingAddressAddr2, "
      
        + " BillingAddressCity, "
      
        + " BillingAddressState, "
      
        + " BillingAddressPostalCode, "
      
        + " BillingAddressCountry, "
      
        + " BillingAddressNote, "
      
        + " ShippingAddressAddr1, "
      
        + " ShippingAddressAddr2, "
      
        + " ShippingAddressCity, "
      
        + " ShippingAddressState, "
      
        + " ShippingAddressPostalCode, "
      
        + " ShippingAddressCountry, "
      
        + " ShippingAddressNote, "
      
        + " Balance, "
      
        + " QbCustomerTypeListId, "
      
        + " QbSalesRepListId "
      

      + " From QbCustomer "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbCustomer FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbCustomer not found, search by primary key");

      }

      public static QbCustomer FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbCustomer qbCustomer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbCustomer.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbCustomer qbCustomer)
      {
      return Exists(qbCustomer, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbCustomer limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static QbCustomer Load(IDataReader dataReader, int offset)
      {
      QbCustomer qbCustomer = new QbCustomer();

      qbCustomer.ID = dataReader.GetInt32(0 + offset);
          qbCustomer.CustomerId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            qbCustomer.ProjectId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbCustomer.ListId = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            qbCustomer.EditSequence = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            qbCustomer.TimeCreated = dataReader.GetDateTime(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbCustomer.TimeModified = dataReader.GetDateTime(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbCustomer.SubLevel = dataReader.GetInt32(7 + offset);
          qbCustomer.Name = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbCustomer.FullName = dataReader.GetString(9 + offset);
          qbCustomer.IsActive = dataReader.GetBoolean(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            qbCustomer.CompanyName = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbCustomer.FirstName = dataReader.GetString(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            qbCustomer.LastName = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            qbCustomer.Phone1 = dataReader.GetString(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            qbCustomer.Phone2 = dataReader.GetString(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            qbCustomer.Email = dataReader.GetString(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            qbCustomer.BillingAddressAddr1 = dataReader.GetString(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            qbCustomer.BillingAddressAddr2 = dataReader.GetString(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            qbCustomer.BillingAddressCity = dataReader.GetString(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            qbCustomer.BillingAddressState = dataReader.GetString(20 + offset);
          
            if(!dataReader.IsDBNull(21 + offset))
            qbCustomer.BillingAddressPostalCode = dataReader.GetString(21 + offset);
          
            if(!dataReader.IsDBNull(22 + offset))
            qbCustomer.BillingAddressCountry = dataReader.GetString(22 + offset);
          
            if(!dataReader.IsDBNull(23 + offset))
            qbCustomer.BillingAddressNote = dataReader.GetString(23 + offset);
          
            if(!dataReader.IsDBNull(24 + offset))
            qbCustomer.ShippingAddressAddr1 = dataReader.GetString(24 + offset);
          
            if(!dataReader.IsDBNull(25 + offset))
            qbCustomer.ShippingAddressAddr2 = dataReader.GetString(25 + offset);
          
            if(!dataReader.IsDBNull(26 + offset))
            qbCustomer.ShippingAddressCity = dataReader.GetString(26 + offset);
          
            if(!dataReader.IsDBNull(27 + offset))
            qbCustomer.ShippingAddressState = dataReader.GetString(27 + offset);
          
            if(!dataReader.IsDBNull(28 + offset))
            qbCustomer.ShippingAddressPostalCode = dataReader.GetString(28 + offset);
          
            if(!dataReader.IsDBNull(29 + offset))
            qbCustomer.ShippingAddressCountry = dataReader.GetString(29 + offset);
          
            if(!dataReader.IsDBNull(30 + offset))
            qbCustomer.ShippingAddressNote = dataReader.GetString(30 + offset);
          
            if(!dataReader.IsDBNull(31 + offset))
            qbCustomer.Balance = dataReader.GetDecimal(31 + offset);
          
            if(!dataReader.IsDBNull(32 + offset))
            qbCustomer.QbCustomerTypeListId = dataReader.GetString(32 + offset);
          
            if(!dataReader.IsDBNull(33 + offset))
            qbCustomer.QbSalesRepListId = dataReader.GetString(33 + offset);
          

      return qbCustomer;
      }

      public static QbCustomer Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbCustomer "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbCustomer qbCustomer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbCustomer.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbCustomer qbCustomer)
      {
        Delete(qbCustomer, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbCustomer ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
        Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ID, "
      
        + " CustomerId, "
      
        + " ProjectId, "
      
        + " ListId, "
      
        + " EditSequence, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " SubLevel, "
      
        + " Name, "
      
        + " FullName, "
      
        + " IsActive, "
      
        + " CompanyName, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " BillingAddressAddr1, "
      
        + " BillingAddressAddr2, "
      
        + " BillingAddressCity, "
      
        + " BillingAddressState, "
      
        + " BillingAddressPostalCode, "
      
        + " BillingAddressCountry, "
      
        + " BillingAddressNote, "
      
        + " ShippingAddressAddr1, "
      
        + " ShippingAddressAddr2, "
      
        + " ShippingAddressCity, "
      
        + " ShippingAddressState, "
      
        + " ShippingAddressPostalCode, "
      
        + " ShippingAddressCountry, "
      
        + " ShippingAddressNote, "
      
        + " Balance, "
      
        + " QbCustomerTypeListId, "
      
        + " QbSalesRepListId "
      

      + " From QbCustomer ";
      public static List<QbCustomer> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbCustomer> rv = new List<QbCustomer>();

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      while(dataReader.Read())
      {
      rv.Add(Load(dataReader));
      }

      }

      return rv;
      }

      }

      public static List<QbCustomer> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbCustomer> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbCustomer obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && CustomerId == obj.CustomerId && ProjectId == obj.ProjectId && ListId == obj.ListId && EditSequence == obj.EditSequence && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && SubLevel == obj.SubLevel && Name == obj.Name && FullName == obj.FullName && IsActive == obj.IsActive && CompanyName == obj.CompanyName && FirstName == obj.FirstName && LastName == obj.LastName && Phone1 == obj.Phone1 && Phone2 == obj.Phone2 && Email == obj.Email && BillingAddressAddr1 == obj.BillingAddressAddr1 && BillingAddressAddr2 == obj.BillingAddressAddr2 && BillingAddressCity == obj.BillingAddressCity && BillingAddressState == obj.BillingAddressState && BillingAddressPostalCode == obj.BillingAddressPostalCode && BillingAddressCountry == obj.BillingAddressCountry && BillingAddressNote == obj.BillingAddressNote && ShippingAddressAddr1 == obj.ShippingAddressAddr1 && ShippingAddressAddr2 == obj.ShippingAddressAddr2 && ShippingAddressCity == obj.ShippingAddressCity && ShippingAddressState == obj.ShippingAddressState && ShippingAddressPostalCode == obj.ShippingAddressPostalCode && ShippingAddressCountry == obj.ShippingAddressCountry && ShippingAddressNote == obj.ShippingAddressNote && Balance == obj.Balance && QbCustomerTypeListId == obj.QbCustomerTypeListId && QbSalesRepListId == obj.QbSalesRepListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbCustomer> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCustomer));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbCustomer item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbCustomer>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCustomer));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbCustomer> itemsList
      = new List<QbCustomer>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbCustomer)
      itemsList.Add(deserializedObject as QbCustomer);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_customerId;
      
        protected int? m_projectId;
      
        protected String m_listId;
      
        protected String m_editSequence;
      
        protected DateTime? m_timeCreated;
      
        protected DateTime? m_timeModified;
      
        protected int? m_subLevel;
      
        protected String m_name;
      
        protected String m_fullName;
      
        protected bool m_isActive;
      
        protected String m_companyName;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
        protected String m_email;
      
        protected String m_billingAddressAddr1;
      
        protected String m_billingAddressAddr2;
      
        protected String m_billingAddressCity;
      
        protected String m_billingAddressState;
      
        protected String m_billingAddressPostalCode;
      
        protected String m_billingAddressCountry;
      
        protected String m_billingAddressNote;
      
        protected String m_shippingAddressAddr1;
      
        protected String m_shippingAddressAddr2;
      
        protected String m_shippingAddressCity;
      
        protected String m_shippingAddressState;
      
        protected String m_shippingAddressPostalCode;
      
        protected String m_shippingAddressCountry;
      
        protected String m_shippingAddressNote;
      
        protected decimal m_balance;
      
        protected String m_qbCustomerTypeListId;
      
        protected String m_qbSalesRepListId;
      
      #endregion

      #region Constructors
      public QbCustomer(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbCustomer(
        int 
          iD,int 
          customerId,int? 
          projectId,String 
          listId,String 
          editSequence,DateTime? 
          timeCreated,DateTime? 
          timeModified,int? 
          subLevel,String 
          name,String 
          fullName,bool 
          isActive,String 
          companyName,String 
          firstName,String 
          lastName,String 
          phone1,String 
          phone2,String 
          email,String 
          billingAddressAddr1,String 
          billingAddressAddr2,String 
          billingAddressCity,String 
          billingAddressState,String 
          billingAddressPostalCode,String 
          billingAddressCountry,String 
          billingAddressNote,String 
          shippingAddressAddr1,String 
          shippingAddressAddr2,String 
          shippingAddressCity,String 
          shippingAddressState,String 
          shippingAddressPostalCode,String 
          shippingAddressCountry,String 
          shippingAddressNote,decimal 
          balance,String 
          qbCustomerTypeListId,String 
          qbSalesRepListId
        ) : this()
        {
        
          m_iD = iD;
        
          m_customerId = customerId;
        
          m_projectId = projectId;
        
          m_listId = listId;
        
          m_editSequence = editSequence;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_subLevel = subLevel;
        
          m_name = name;
        
          m_fullName = fullName;
        
          m_isActive = isActive;
        
          m_companyName = companyName;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
          m_email = email;
        
          m_billingAddressAddr1 = billingAddressAddr1;
        
          m_billingAddressAddr2 = billingAddressAddr2;
        
          m_billingAddressCity = billingAddressCity;
        
          m_billingAddressState = billingAddressState;
        
          m_billingAddressPostalCode = billingAddressPostalCode;
        
          m_billingAddressCountry = billingAddressCountry;
        
          m_billingAddressNote = billingAddressNote;
        
          m_shippingAddressAddr1 = shippingAddressAddr1;
        
          m_shippingAddressAddr2 = shippingAddressAddr2;
        
          m_shippingAddressCity = shippingAddressCity;
        
          m_shippingAddressState = shippingAddressState;
        
          m_shippingAddressPostalCode = shippingAddressPostalCode;
        
          m_shippingAddressCountry = shippingAddressCountry;
        
          m_shippingAddressNote = shippingAddressNote;
        
          m_balance = balance;
        
          m_qbCustomerTypeListId = qbCustomerTypeListId;
        
          m_qbSalesRepListId = qbSalesRepListId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int? ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public String ListId
        {
        get { return m_listId;}
        set { m_listId = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      
        [XmlElement]
        public DateTime? TimeCreated
        {
        get { return m_timeCreated;}
        set { m_timeCreated = value; }
        }
      
        [XmlElement]
        public DateTime? TimeModified
        {
        get { return m_timeModified;}
        set { m_timeModified = value; }
        }
      
        [XmlElement]
        public int? SubLevel
        {
        get { return m_subLevel;}
        set { m_subLevel = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public String FullName
        {
        get { return m_fullName;}
        set { m_fullName = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public String CompanyName
        {
        get { return m_companyName;}
        set { m_companyName = value; }
        }
      
        [XmlElement]
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        [XmlElement]
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      
        [XmlElement]
        public String Phone1
        {
        get { return m_phone1;}
        set { m_phone1 = value; }
        }
      
        [XmlElement]
        public String Phone2
        {
        get { return m_phone2;}
        set { m_phone2 = value; }
        }
      
        [XmlElement]
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      
        [XmlElement]
        public String BillingAddressAddr1
        {
        get { return m_billingAddressAddr1;}
        set { m_billingAddressAddr1 = value; }
        }
      
        [XmlElement]
        public String BillingAddressAddr2
        {
        get { return m_billingAddressAddr2;}
        set { m_billingAddressAddr2 = value; }
        }
      
        [XmlElement]
        public String BillingAddressCity
        {
        get { return m_billingAddressCity;}
        set { m_billingAddressCity = value; }
        }
      
        [XmlElement]
        public String BillingAddressState
        {
        get { return m_billingAddressState;}
        set { m_billingAddressState = value; }
        }
      
        [XmlElement]
        public String BillingAddressPostalCode
        {
        get { return m_billingAddressPostalCode;}
        set { m_billingAddressPostalCode = value; }
        }
      
        [XmlElement]
        public String BillingAddressCountry
        {
        get { return m_billingAddressCountry;}
        set { m_billingAddressCountry = value; }
        }
      
        [XmlElement]
        public String BillingAddressNote
        {
        get { return m_billingAddressNote;}
        set { m_billingAddressNote = value; }
        }
      
        [XmlElement]
        public String ShippingAddressAddr1
        {
        get { return m_shippingAddressAddr1;}
        set { m_shippingAddressAddr1 = value; }
        }
      
        [XmlElement]
        public String ShippingAddressAddr2
        {
        get { return m_shippingAddressAddr2;}
        set { m_shippingAddressAddr2 = value; }
        }
      
        [XmlElement]
        public String ShippingAddressCity
        {
        get { return m_shippingAddressCity;}
        set { m_shippingAddressCity = value; }
        }
      
        [XmlElement]
        public String ShippingAddressState
        {
        get { return m_shippingAddressState;}
        set { m_shippingAddressState = value; }
        }
      
        [XmlElement]
        public String ShippingAddressPostalCode
        {
        get { return m_shippingAddressPostalCode;}
        set { m_shippingAddressPostalCode = value; }
        }
      
        [XmlElement]
        public String ShippingAddressCountry
        {
        get { return m_shippingAddressCountry;}
        set { m_shippingAddressCountry = value; }
        }
      
        [XmlElement]
        public String ShippingAddressNote
        {
        get { return m_shippingAddressNote;}
        set { m_shippingAddressNote = value; }
        }
      
        [XmlElement]
        public decimal Balance
        {
        get { return m_balance;}
        set { m_balance = value; }
        }
      
        [XmlElement]
        public String QbCustomerTypeListId
        {
        get { return m_qbCustomerTypeListId;}
        set { m_qbCustomerTypeListId = value; }
        }
      
        [XmlElement]
        public String QbSalesRepListId
        {
        get { return m_qbSalesRepListId;}
        set { m_qbSalesRepListId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 34; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    