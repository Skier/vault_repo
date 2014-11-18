
      namespace TractInc.Expense.Domain
      {
      using System;
      
    
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using Weborb.Data.Management;

    
    using MySql.Data.MySqlClient;
    using Weborb.Data.Management.MySql;
  
    public class Database:TDatabase<MySqlConnection>
    {
      public Database()
      {
        ConnectionString = "server=localhost;port=3306;user id=root;password=admin;database=tractexpense;persist security info=true;";
      }
    }
  
    
    public partial class Afe
    {
    
      protected String _aFE;
    
      protected int _clientId;
    
      protected String _aFEName;
    
      protected String _aFEStatus;
    
      protected String _afeGuid;
    

    public Afe(){}

    public Afe(
    String 
            aFE
    )
    {
    
      _aFE = aFE;
    
    }

    

      public Afe(
      String 
        aFE,int 
        clientId,String 
        aFEName,String 
        aFEStatus,String 
        afeGuid
      )
      {
      
        _aFE = aFE;
      
        _clientId = clientId;
      
        _aFEName = aFEName;
      
        _aFEStatus = aFEStatus;
      
        _afeGuid = afeGuid;
      
      }

    
      public String AFE
      {
      get { return _aFE;}
      set { _aFE = value; }
      }
    
      public int ClientId
      {
      get { return _clientId;}
      set { _clientId = value; }
      }
    
      public String AFEName
      {
      get { return _aFEName;}
      set { _aFEName = value; }
      }
    
      public String AFEStatus
      {
      get { return _aFEStatus;}
      set { _aFEStatus = value; }
      }
    
      public String AfeGuid
      {
      get { return _afeGuid;}
      set { _afeGuid = value; }
      }
    
    
    }
  

    public partial class AfeDataMapper:TDataMapper<Afe,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "afe";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into afe (
    AFE,ClientId,AFEName,AFEStatus,AfeGuid) Values (
    
      ?AFE,
      ?ClientId,
      ?AFEName,
      ?AFEStatus,
      ?AfeGuid);";

    public override Afe create( Afe afe )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?AFE", afe.AFE);
                
                    sqlCommand.Parameters.Add("?ClientId", afe.ClientId);
                
                    sqlCommand.Parameters.Add("?AFEName", afe.AFEName);
                
                    sqlCommand.Parameters.Add("?AFEStatus", afe.AFEStatus);
                
                    sqlCommand.Parameters.Add("?AfeGuid", afe.AfeGuid);
                
    
            sqlCommand.ExecuteNonQuery();

              
            }
        }

      return afe;
    }

  

    private const String SqlSelectAll = @"Select
    AFE,ClientId,AFEName,AFEStatus,AfeGuid 
    From afe ";
    
    public List<Afe> findAll(Object args)
    {
      List<Afe> rv = new List<Afe>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    AFE,ClientId,AFEName,AFEStatus,AfeGuid
     From afe
       Where 
      AFE = ?AFE
    ";

    public Afe findByPrimaryKey(
    String aFE
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AFE", aFE);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("afe not found, search by primary key");
    }

    }


    public bool exists(Afe afe)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AFE", afe.AFE);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Afe doLoad(IDataReader dataReader)
    {
    Afe afe = new Afe();

    afe.AFE = dataReader.GetString(0);
        afe.ClientId = dataReader.GetInt32(1);
        afe.AFEName = dataReader.GetString(2);
        afe.AFEStatus = dataReader.GetString(3);
        afe.AfeGuid = dataReader.GetString(4);
        

    return afe;
    }


    protected override Afe doLoad(Hashtable hashtable)
    {
      Afe afe = new Afe();

      
        if(hashtable.ContainsKey("AFE"))
        afe.AFE = ( String )hashtable["AFE"];
          
        if(hashtable.ContainsKey("ClientId"))
        afe.ClientId = (int) Convert.ChangeType(hashtable["ClientId"], typeof(int));
          
        if(hashtable.ContainsKey("AFEName"))
        afe.AFEName = ( String )hashtable["AFEName"];
          
        if(hashtable.ContainsKey("AFEStatus"))
        afe.AFEStatus = ( String )hashtable["AFEStatus"];
          
        if(hashtable.ContainsKey("AfeGuid"))
        afe.AfeGuid = ( String )hashtable["AfeGuid"];
          

      return afe;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From afe
      Where
      AFE = ?AFE";
    public Afe remove(Afe afe)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AFE", afe.AFE);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return afe;
    }

    #endregion
  

    public Afe save( Afe afe )
    {
      if(exists(afe))
        return update(afe);
        return create(afe);
    }

  


    private const String SqlUpdate = @"Update afe Set 
    ClientId = ?ClientId,AFEName = ?AFEName,AFEStatus = ?AFEStatus,AfeGuid = ?AfeGuid
       Where 
      AFE = ?AFE";

    public Afe update(Afe afe)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AFE", afe.AFE);
              
                  sqlCommand.Parameters.Add("?ClientId", afe.ClientId);
              
                  sqlCommand.Parameters.Add("?AFEName", afe.AFEName);
              
                  sqlCommand.Parameters.Add("?AFEStatus", afe.AFEStatus);
              
                  sqlCommand.Parameters.Add("?AfeGuid", afe.AfeGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return afe;
    }

  
    }
    
  
    
    public partial class Afestatus
    {
    
      protected String _aFEStatus;
    

    public Afestatus(){}

    public Afestatus(
    String 
            aFEStatus
    )
    {
    
      _aFEStatus = aFEStatus;
    
    }

    
      public String AFEStatus
      {
      get { return _aFEStatus;}
      set { _aFEStatus = value; }
      }
    
    
    }
  

    public partial class AfestatusDataMapper:TDataMapper<Afestatus,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "afestatus";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into afestatus (
    AFEStatus) Values (
    
      ?AFEStatus);";

    public override Afestatus create( Afestatus afestatus )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?AFEStatus", afestatus.AFEStatus);
                
    
            sqlCommand.ExecuteNonQuery();

              
            }
        }

      return afestatus;
    }

  

    private const String SqlSelectAll = @"Select
    AFEStatus 
    From afestatus ";
    
    public List<Afestatus> findAll(Object args)
    {
      List<Afestatus> rv = new List<Afestatus>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    AFEStatus
     From afestatus
       Where 
      AFEStatus = ?AFEStatus
    ";

    public Afestatus findByPrimaryKey(
    String aFEStatus
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AFEStatus", aFEStatus);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("afestatus not found, search by primary key");
    }

    }


    public bool exists(Afestatus afestatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AFEStatus", afestatus.AFEStatus);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Afestatus doLoad(IDataReader dataReader)
    {
    Afestatus afestatus = new Afestatus();

    afestatus.AFEStatus = dataReader.GetString(0);
        

    return afestatus;
    }


    protected override Afestatus doLoad(Hashtable hashtable)
    {
      Afestatus afestatus = new Afestatus();

      
        if(hashtable.ContainsKey("AFEStatus"))
        afestatus.AFEStatus = ( String )hashtable["AFEStatus"];
          

      return afestatus;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From afestatus
      Where
      AFEStatus = ?AFEStatus";
    public Afestatus remove(Afestatus afestatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AFEStatus", afestatus.AFEStatus);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return afestatus;
    }

    #endregion
  

    public Afestatus save( Afestatus afestatus )
    {
      if(exists(afestatus))
        return update(afestatus);
        return create(afestatus);
    }

  


    private const String SqlUpdate = @"Update afestatus Set 
    
       Where 
      AFEStatus = ?AFEStatus";

    public Afestatus update(Afestatus afestatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AFEStatus", afestatus.AFEStatus);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return afestatus;
    }

  
    }
    
  
    
    public partial class Asset
    {
    
      protected int _assetId;
    
      protected String _assetType;
    
      protected int _chiefAssetId;
    
      protected String _businessName;
    
      protected String _firstName;
    
      protected String _middleName;
    
      protected String _lastName;
    
      protected String _sSN;
    
      protected String _assetGuid;
    

    public Asset(){}

    public Asset(
    int 
            assetId
    )
    {
    
      _assetId = assetId;
    
    }

    

      public Asset(
      int 
        assetId,String 
        assetType,int 
        chiefAssetId,String 
        businessName,String 
        firstName,String 
        middleName,String 
        lastName,String 
        sSN,String 
        assetGuid
      )
      {
      
        _assetId = assetId;
      
        _assetType = assetType;
      
        _chiefAssetId = chiefAssetId;
      
        _businessName = businessName;
      
        _firstName = firstName;
      
        _middleName = middleName;
      
        _lastName = lastName;
      
        _sSN = sSN;
      
        _assetGuid = assetGuid;
      
      }

    
      public int AssetId
      {
      get { return _assetId;}
      set { _assetId = value; }
      }
    
      public String AssetType
      {
      get { return _assetType;}
      set { _assetType = value; }
      }
    
      public int ChiefAssetId
      {
      get { return _chiefAssetId;}
      set { _chiefAssetId = value; }
      }
    
      public String BusinessName
      {
      get { return _businessName;}
      set { _businessName = value; }
      }
    
      public String FirstName
      {
      get { return _firstName;}
      set { _firstName = value; }
      }
    
      public String MiddleName
      {
      get { return _middleName;}
      set { _middleName = value; }
      }
    
      public String LastName
      {
      get { return _lastName;}
      set { _lastName = value; }
      }
    
      public String SSN
      {
      get { return _sSN;}
      set { _sSN = value; }
      }
    
      public String AssetGuid
      {
      get { return _assetGuid;}
      set { _assetGuid = value; }
      }
    
    
    }
  

    public partial class AssetDataMapper:TDataMapper<Asset,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "asset";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into asset (
    AssetType,ChiefAssetId,BusinessName,FirstName,MiddleName,LastName,SSN,AssetGuid) Values (
    
      ?AssetType,
      ?ChiefAssetId,
      ?BusinessName,
      ?FirstName,
      ?MiddleName,
      ?LastName,
      ?SSN,
      ?AssetGuid);";

    public override Asset create( Asset asset )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?AssetType", asset.AssetType);
                
                    sqlCommand.Parameters.Add("?ChiefAssetId", asset.ChiefAssetId);
                
                    sqlCommand.Parameters.Add("?BusinessName", asset.BusinessName);
                
                    sqlCommand.Parameters.Add("?FirstName", asset.FirstName);
                
                    sqlCommand.Parameters.Add("?MiddleName", asset.MiddleName);
                
                    sqlCommand.Parameters.Add("?LastName", asset.LastName);
                
                    sqlCommand.Parameters.Add("?SSN", asset.SSN);
                
                    sqlCommand.Parameters.Add("?AssetGuid", asset.AssetGuid);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                asset.AssetId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return asset;
    }

  

    private const String SqlSelectAll = @"Select
    AssetId,AssetType,ChiefAssetId,BusinessName,FirstName,MiddleName,LastName,SSN,AssetGuid 
    From asset ";
    
    public List<Asset> findAll(Object args)
    {
      List<Asset> rv = new List<Asset>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    AssetId,AssetType,ChiefAssetId,BusinessName,FirstName,MiddleName,LastName,SSN,AssetGuid
     From asset
       Where 
      AssetId = ?AssetId
    ";

    public Asset findByPrimaryKey(
    int assetId
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AssetId", assetId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("asset not found, search by primary key");
    }

    }


    public bool exists(Asset asset)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AssetId", asset.AssetId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Asset doLoad(IDataReader dataReader)
    {
    Asset asset = new Asset();

    asset.AssetId = dataReader.GetInt32(0);
        asset.AssetType = dataReader.GetString(1);
        asset.ChiefAssetId = dataReader.GetInt32(2);
        asset.BusinessName = dataReader.GetString(3);
        asset.FirstName = dataReader.GetString(4);
        asset.MiddleName = dataReader.GetString(5);
        asset.LastName = dataReader.GetString(6);
        asset.SSN = dataReader.GetString(7);
        asset.AssetGuid = dataReader.GetString(8);
        

    return asset;
    }


    protected override Asset doLoad(Hashtable hashtable)
    {
      Asset asset = new Asset();

      
        if(hashtable.ContainsKey("AssetId"))
        asset.AssetId = (int) Convert.ChangeType(hashtable["AssetId"], typeof(int));
          
        if(hashtable.ContainsKey("AssetType"))
        asset.AssetType = ( String )hashtable["AssetType"];
          
        if(hashtable.ContainsKey("ChiefAssetId"))
        asset.ChiefAssetId = (int) Convert.ChangeType(hashtable["ChiefAssetId"], typeof(int));
          
        if(hashtable.ContainsKey("BusinessName"))
        asset.BusinessName = ( String )hashtable["BusinessName"];
          
        if(hashtable.ContainsKey("FirstName"))
        asset.FirstName = ( String )hashtable["FirstName"];
          
        if(hashtable.ContainsKey("MiddleName"))
        asset.MiddleName = ( String )hashtable["MiddleName"];
          
        if(hashtable.ContainsKey("LastName"))
        asset.LastName = ( String )hashtable["LastName"];
          
        if(hashtable.ContainsKey("SSN"))
        asset.SSN = ( String )hashtable["SSN"];
          
        if(hashtable.ContainsKey("AssetGuid"))
        asset.AssetGuid = ( String )hashtable["AssetGuid"];
          

      return asset;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From asset
      Where
      AssetId = ?AssetId";
    public Asset remove(Asset asset)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AssetId", asset.AssetId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return asset;
    }

    #endregion
  

    public Asset save( Asset asset )
    {
      if(exists(asset))
        return update(asset);
        return create(asset);
    }

  


    private const String SqlUpdate = @"Update asset Set 
    AssetType = ?AssetType,ChiefAssetId = ?ChiefAssetId,BusinessName = ?BusinessName,FirstName = ?FirstName,MiddleName = ?MiddleName,LastName = ?LastName,SSN = ?SSN,AssetGuid = ?AssetGuid
       Where 
      AssetId = ?AssetId";

    public Asset update(Asset asset)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AssetId", asset.AssetId);
              
                  sqlCommand.Parameters.Add("?AssetType", asset.AssetType);
              
                  sqlCommand.Parameters.Add("?ChiefAssetId", asset.ChiefAssetId);
              
                  sqlCommand.Parameters.Add("?BusinessName", asset.BusinessName);
              
                  sqlCommand.Parameters.Add("?FirstName", asset.FirstName);
              
                  sqlCommand.Parameters.Add("?MiddleName", asset.MiddleName);
              
                  sqlCommand.Parameters.Add("?LastName", asset.LastName);
              
                  sqlCommand.Parameters.Add("?SSN", asset.SSN);
              
                  sqlCommand.Parameters.Add("?AssetGuid", asset.AssetGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return asset;
    }

  
    }
    
  
    
    public partial class Assetassignment
    {
    
      protected int _assetAssignmentId;
    
      protected String _aFE;
    
      protected String _subAFE;
    
      protected int _assetId;
    
      protected decimal _billRate;
    
      protected decimal _payRate;
    
      protected String _assetAssignmentGuid;
    

    public Assetassignment(){}

    public Assetassignment(
    int 
            assetAssignmentId
    )
    {
    
      _assetAssignmentId = assetAssignmentId;
    
    }

    

      public Assetassignment(
      int 
        assetAssignmentId,String 
        aFE,String 
        subAFE,int 
        assetId,decimal 
        billRate,decimal 
        payRate,String 
        assetAssignmentGuid
      )
      {
      
        _assetAssignmentId = assetAssignmentId;
      
        _aFE = aFE;
      
        _subAFE = subAFE;
      
        _assetId = assetId;
      
        _billRate = billRate;
      
        _payRate = payRate;
      
        _assetAssignmentGuid = assetAssignmentGuid;
      
      }

    
      public int AssetAssignmentId
      {
      get { return _assetAssignmentId;}
      set { _assetAssignmentId = value; }
      }
    
      public String AFE
      {
      get { return _aFE;}
      set { _aFE = value; }
      }
    
      public String SubAFE
      {
      get { return _subAFE;}
      set { _subAFE = value; }
      }
    
      public int AssetId
      {
      get { return _assetId;}
      set { _assetId = value; }
      }
    
      public decimal BillRate
      {
      get { return _billRate;}
      set { _billRate = value; }
      }
    
      public decimal PayRate
      {
      get { return _payRate;}
      set { _payRate = value; }
      }
    
      public String AssetAssignmentGuid
      {
      get { return _assetAssignmentGuid;}
      set { _assetAssignmentGuid = value; }
      }
    
    
    }
  

    public partial class AssetassignmentDataMapper:TDataMapper<Assetassignment,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "assetassignment";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into assetassignment (
    AFE,SubAFE,AssetId,BillRate,PayRate,AssetAssignmentGuid) Values (
    
      ?AFE,
      ?SubAFE,
      ?AssetId,
      ?BillRate,
      ?PayRate,
      ?AssetAssignmentGuid);";

    public override Assetassignment create( Assetassignment assetassignment )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?AFE", assetassignment.AFE);
                
                    sqlCommand.Parameters.Add("?SubAFE", assetassignment.SubAFE);
                
                    sqlCommand.Parameters.Add("?AssetId", assetassignment.AssetId);
                
                    sqlCommand.Parameters.Add("?BillRate", assetassignment.BillRate);
                
                    sqlCommand.Parameters.Add("?PayRate", assetassignment.PayRate);
                
                    sqlCommand.Parameters.Add("?AssetAssignmentGuid", assetassignment.AssetAssignmentGuid);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                assetassignment.AssetAssignmentId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return assetassignment;
    }

  

    private const String SqlSelectAll = @"Select
    AssetAssignmentId,AFE,SubAFE,AssetId,BillRate,PayRate,AssetAssignmentGuid 
    From assetassignment ";
    
    public List<Assetassignment> findAll(Object args)
    {
      List<Assetassignment> rv = new List<Assetassignment>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    AssetAssignmentId,AFE,SubAFE,AssetId,BillRate,PayRate,AssetAssignmentGuid
     From assetassignment
       Where 
      AssetAssignmentId = ?AssetAssignmentId
    ";

    public Assetassignment findByPrimaryKey(
    int assetAssignmentId
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AssetAssignmentId", assetAssignmentId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("assetassignment not found, search by primary key");
    }

    }


    public bool exists(Assetassignment assetassignment)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AssetAssignmentId", assetassignment.AssetAssignmentId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Assetassignment doLoad(IDataReader dataReader)
    {
    Assetassignment assetassignment = new Assetassignment();

    assetassignment.AssetAssignmentId = dataReader.GetInt32(0);
        assetassignment.AFE = dataReader.GetString(1);
        assetassignment.SubAFE = dataReader.GetString(2);
        assetassignment.AssetId = dataReader.GetInt32(3);
        assetassignment.BillRate = dataReader.GetDecimal(4);
        assetassignment.PayRate = dataReader.GetDecimal(5);
        assetassignment.AssetAssignmentGuid = dataReader.GetString(6);
        

    return assetassignment;
    }


    protected override Assetassignment doLoad(Hashtable hashtable)
    {
      Assetassignment assetassignment = new Assetassignment();

      
        if(hashtable.ContainsKey("AssetAssignmentId"))
        assetassignment.AssetAssignmentId = (int) Convert.ChangeType(hashtable["AssetAssignmentId"], typeof(int));
          
        if(hashtable.ContainsKey("AFE"))
        assetassignment.AFE = ( String )hashtable["AFE"];
          
        if(hashtable.ContainsKey("SubAFE"))
        assetassignment.SubAFE = ( String )hashtable["SubAFE"];
          
        if(hashtable.ContainsKey("AssetId"))
        assetassignment.AssetId = (int) Convert.ChangeType(hashtable["AssetId"], typeof(int));
          
        if(hashtable.ContainsKey("BillRate"))
        assetassignment.BillRate = (decimal) Convert.ChangeType(hashtable["BillRate"], typeof(decimal));
          
        if(hashtable.ContainsKey("PayRate"))
        assetassignment.PayRate = (decimal) Convert.ChangeType(hashtable["PayRate"], typeof(decimal));
          
        if(hashtable.ContainsKey("AssetAssignmentGuid"))
        assetassignment.AssetAssignmentGuid = ( String )hashtable["AssetAssignmentGuid"];
          

      return assetassignment;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From assetassignment
      Where
      AssetAssignmentId = ?AssetAssignmentId";
    public Assetassignment remove(Assetassignment assetassignment)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AssetAssignmentId", assetassignment.AssetAssignmentId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assetassignment;
    }

    #endregion
  

    public Assetassignment save( Assetassignment assetassignment )
    {
      if(exists(assetassignment))
        return update(assetassignment);
        return create(assetassignment);
    }

  


    private const String SqlUpdate = @"Update assetassignment Set 
    AFE = ?AFE,SubAFE = ?SubAFE,AssetId = ?AssetId,BillRate = ?BillRate,PayRate = ?PayRate,AssetAssignmentGuid = ?AssetAssignmentGuid
       Where 
      AssetAssignmentId = ?AssetAssignmentId";

    public Assetassignment update(Assetassignment assetassignment)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AssetAssignmentId", assetassignment.AssetAssignmentId);
              
                  sqlCommand.Parameters.Add("?AFE", assetassignment.AFE);
              
                  sqlCommand.Parameters.Add("?SubAFE", assetassignment.SubAFE);
              
                  sqlCommand.Parameters.Add("?AssetId", assetassignment.AssetId);
              
                  sqlCommand.Parameters.Add("?BillRate", assetassignment.BillRate);
              
                  sqlCommand.Parameters.Add("?PayRate", assetassignment.PayRate);
              
                  sqlCommand.Parameters.Add("?AssetAssignmentGuid", assetassignment.AssetAssignmentGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assetassignment;
    }

  
    }
    
  
    
    public partial class Assettype
    {
    
      protected String _assetType;
    

    public Assettype(){}

    public Assettype(
    String 
            assetType
    )
    {
    
      _assetType = assetType;
    
    }

    
      public String AssetType
      {
      get { return _assetType;}
      set { _assetType = value; }
      }
    
    
    }
  

    public partial class AssettypeDataMapper:TDataMapper<Assettype,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "assettype";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into assettype (
    AssetType) Values (
    
      ?AssetType);";

    public override Assettype create( Assettype assettype )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?AssetType", assettype.AssetType);
                
    
            sqlCommand.ExecuteNonQuery();

              
            }
        }

      return assettype;
    }

  

    private const String SqlSelectAll = @"Select
    AssetType 
    From assettype ";
    
    public List<Assettype> findAll(Object args)
    {
      List<Assettype> rv = new List<Assettype>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    AssetType
     From assettype
       Where 
      AssetType = ?AssetType
    ";

    public Assettype findByPrimaryKey(
    String assetType
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AssetType", assetType);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("assettype not found, search by primary key");
    }

    }


    public bool exists(Assettype assettype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AssetType", assettype.AssetType);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Assettype doLoad(IDataReader dataReader)
    {
    Assettype assettype = new Assettype();

    assettype.AssetType = dataReader.GetString(0);
        

    return assettype;
    }


    protected override Assettype doLoad(Hashtable hashtable)
    {
      Assettype assettype = new Assettype();

      
        if(hashtable.ContainsKey("AssetType"))
        assettype.AssetType = ( String )hashtable["AssetType"];
          

      return assettype;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From assettype
      Where
      AssetType = ?AssetType";
    public Assettype remove(Assettype assettype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AssetType", assettype.AssetType);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assettype;
    }

    #endregion
  

    public Assettype save( Assettype assettype )
    {
      if(exists(assettype))
        return update(assettype);
        return create(assettype);
    }

  


    private const String SqlUpdate = @"Update assettype Set 
    
       Where 
      AssetType = ?AssetType";

    public Assettype update(Assettype assettype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AssetType", assettype.AssetType);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assettype;
    }

  
    }
    
  
    
    public partial class Billitem
    {
    
      protected int _billItemId;
    
      protected int _billId;
    
      protected int _assetAssignmentId;
    
      protected DateTime _billingDate;
    
      protected int _dayQty;
    
      protected decimal _billRate;
    
      protected decimal? _totalHourlyBilling;
    
      protected decimal? _lodging;
    
      protected decimal? _meals;
    
      protected decimal? _phone;
    
      protected decimal? _copies;
    
      protected decimal? _filingFees;
    
      protected String _status;
    
      protected String _billItemGuid;
    

    public Billitem(){}

    public Billitem(
    int 
            billItemId
    )
    {
    
      _billItemId = billItemId;
    
    }

    

      public Billitem(
      int 
        billItemId,int 
        billId,int 
        assetAssignmentId,DateTime 
        billingDate,int 
        dayQty,decimal 
        billRate,decimal 
        totalHourlyBilling,decimal 
        lodging,decimal 
        meals,decimal 
        phone,decimal 
        copies,decimal 
        filingFees,String 
        status,String 
        billItemGuid
      )
      {
      
        _billItemId = billItemId;
      
        _billId = billId;
      
        _assetAssignmentId = assetAssignmentId;
      
        _billingDate = billingDate;
      
        _dayQty = dayQty;
      
        _billRate = billRate;
      
        _totalHourlyBilling = totalHourlyBilling;
      
        _lodging = lodging;
      
        _meals = meals;
      
        _phone = phone;
      
        _copies = copies;
      
        _filingFees = filingFees;
      
        _status = status;
      
        _billItemGuid = billItemGuid;
      
      }

    
      public int BillItemId
      {
      get { return _billItemId;}
      set { _billItemId = value; }
      }
    
      public int BillId
      {
      get { return _billId;}
      set { _billId = value; }
      }
    
      public int AssetAssignmentId
      {
      get { return _assetAssignmentId;}
      set { _assetAssignmentId = value; }
      }
    
      public DateTime BillingDate
      {
      get { return _billingDate;}
      set { _billingDate = value; }
      }
    
      public int DayQty
      {
      get { return _dayQty;}
      set { _dayQty = value; }
      }
    
      public decimal BillRate
      {
      get { return _billRate;}
      set { _billRate = value; }
      }
    
      public decimal? TotalHourlyBilling
      {
      get { return _totalHourlyBilling;}
      set { _totalHourlyBilling = value; }
      }
    
      public decimal? Lodging
      {
      get { return _lodging;}
      set { _lodging = value; }
      }
    
      public decimal? Meals
      {
      get { return _meals;}
      set { _meals = value; }
      }
    
      public decimal? Phone
      {
      get { return _phone;}
      set { _phone = value; }
      }
    
      public decimal? Copies
      {
      get { return _copies;}
      set { _copies = value; }
      }
    
      public decimal? FilingFees
      {
      get { return _filingFees;}
      set { _filingFees = value; }
      }
    
      public String Status
      {
      get { return _status;}
      set { _status = value; }
      }
    
      public String BillItemGuid
      {
      get { return _billItemGuid;}
      set { _billItemGuid = value; }
      }
    
    
    }
  

    public partial class BillitemDataMapper:TDataMapper<Billitem,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "billitem";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into billitem (
    BillId,AssetAssignmentId,BillingDate,DayQty,BillRate,TotalHourlyBilling,Lodging,Meals,Phone,Copies,FilingFees,Status,BillItemGuid) Values (
    
      ?BillId,
      ?AssetAssignmentId,
      ?BillingDate,
      ?DayQty,
      ?BillRate,
      ?TotalHourlyBilling,
      ?Lodging,
      ?Meals,
      ?Phone,
      ?Copies,
      ?FilingFees,
      ?Status,
      ?BillItemGuid);";

    public override Billitem create( Billitem billitem )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?BillId", billitem.BillId);
                
                    sqlCommand.Parameters.Add("?AssetAssignmentId", billitem.AssetAssignmentId);
                
                    sqlCommand.Parameters.Add("?BillingDate", billitem.BillingDate);
                
                    sqlCommand.Parameters.Add("?DayQty", billitem.DayQty);
                
                    sqlCommand.Parameters.Add("?BillRate", billitem.BillRate);
                
                  if(billitem.TotalHourlyBilling != null)
                    sqlCommand.Parameters.Add("?TotalHourlyBilling", billitem.TotalHourlyBilling);
                  else
                    sqlCommand.Parameters.Add("?TotalHourlyBilling", DBNull.Value);
                
                  if(billitem.Lodging != null)
                    sqlCommand.Parameters.Add("?Lodging", billitem.Lodging);
                  else
                    sqlCommand.Parameters.Add("?Lodging", DBNull.Value);
                
                  if(billitem.Meals != null)
                    sqlCommand.Parameters.Add("?Meals", billitem.Meals);
                  else
                    sqlCommand.Parameters.Add("?Meals", DBNull.Value);
                
                  if(billitem.Phone != null)
                    sqlCommand.Parameters.Add("?Phone", billitem.Phone);
                  else
                    sqlCommand.Parameters.Add("?Phone", DBNull.Value);
                
                  if(billitem.Copies != null)
                    sqlCommand.Parameters.Add("?Copies", billitem.Copies);
                  else
                    sqlCommand.Parameters.Add("?Copies", DBNull.Value);
                
                  if(billitem.FilingFees != null)
                    sqlCommand.Parameters.Add("?FilingFees", billitem.FilingFees);
                  else
                    sqlCommand.Parameters.Add("?FilingFees", DBNull.Value);
                
                    sqlCommand.Parameters.Add("?Status", billitem.Status);
                
                    sqlCommand.Parameters.Add("?BillItemGuid", billitem.BillItemGuid);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                billitem.BillItemId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return billitem;
    }

  

    private const String SqlSelectAll = @"Select
    BillItemId,BillId,AssetAssignmentId,BillingDate,DayQty,BillRate,TotalHourlyBilling,Lodging,Meals,Phone,Copies,FilingFees,Status,BillItemGuid 
    From billitem ";
    
    public List<Billitem> findAll(Object args)
    {
      List<Billitem> rv = new List<Billitem>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    BillItemId,BillId,AssetAssignmentId,BillingDate,DayQty,BillRate,TotalHourlyBilling,Lodging,Meals,Phone,Copies,FilingFees,Status,BillItemGuid
     From billitem
       Where 
      BillItemId = ?BillItemId
    ";

    public Billitem findByPrimaryKey(
    int billItemId
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?BillItemId", billItemId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("billitem not found, search by primary key");
    }

    }


    public bool exists(Billitem billitem)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?BillItemId", billitem.BillItemId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Billitem doLoad(IDataReader dataReader)
    {
    Billitem billitem = new Billitem();

    billitem.BillItemId = dataReader.GetInt32(0);
        billitem.BillId = dataReader.GetInt32(1);
        billitem.AssetAssignmentId = dataReader.GetInt32(2);
        billitem.BillingDate = dataReader.GetDateTime(3);
        billitem.DayQty = dataReader.GetInt32(4);
        billitem.BillRate = dataReader.GetDecimal(5);
        
          if(!dataReader.IsDBNull(6))
          billitem.TotalHourlyBilling = dataReader.GetDecimal(6);
        
          if(!dataReader.IsDBNull(7))
          billitem.Lodging = dataReader.GetDecimal(7);
        
          if(!dataReader.IsDBNull(8))
          billitem.Meals = dataReader.GetDecimal(8);
        
          if(!dataReader.IsDBNull(9))
          billitem.Phone = dataReader.GetDecimal(9);
        
          if(!dataReader.IsDBNull(10))
          billitem.Copies = dataReader.GetDecimal(10);
        
          if(!dataReader.IsDBNull(11))
          billitem.FilingFees = dataReader.GetDecimal(11);
        billitem.Status = dataReader.GetString(12);
        billitem.BillItemGuid = dataReader.GetString(13);
        

    return billitem;
    }


    protected override Billitem doLoad(Hashtable hashtable)
    {
      Billitem billitem = new Billitem();

      
        if(hashtable.ContainsKey("BillItemId"))
        billitem.BillItemId = (int) Convert.ChangeType(hashtable["BillItemId"], typeof(int));
          
        if(hashtable.ContainsKey("BillId"))
        billitem.BillId = (int) Convert.ChangeType(hashtable["BillId"], typeof(int));
          
        if(hashtable.ContainsKey("AssetAssignmentId"))
        billitem.AssetAssignmentId = (int) Convert.ChangeType(hashtable["AssetAssignmentId"], typeof(int));
          
        if(hashtable.ContainsKey("BillingDate"))
        billitem.BillingDate = (DateTime) Convert.ChangeType(hashtable["BillingDate"], typeof(DateTime));
          
        if(hashtable.ContainsKey("DayQty"))
        billitem.DayQty = (int) Convert.ChangeType(hashtable["DayQty"], typeof(int));
          
        if(hashtable.ContainsKey("BillRate"))
        billitem.BillRate = (decimal) Convert.ChangeType(hashtable["BillRate"], typeof(decimal));
          
        if(hashtable.ContainsKey("TotalHourlyBilling"))
        billitem.TotalHourlyBilling = (decimal) Convert.ChangeType(hashtable["TotalHourlyBilling"], typeof(decimal));
          
        if(hashtable.ContainsKey("Lodging"))
        billitem.Lodging = (decimal) Convert.ChangeType(hashtable["Lodging"], typeof(decimal));
          
        if(hashtable.ContainsKey("Meals"))
        billitem.Meals = (decimal) Convert.ChangeType(hashtable["Meals"], typeof(decimal));
          
        if(hashtable.ContainsKey("Phone"))
        billitem.Phone = (decimal) Convert.ChangeType(hashtable["Phone"], typeof(decimal));
          
        if(hashtable.ContainsKey("Copies"))
        billitem.Copies = (decimal) Convert.ChangeType(hashtable["Copies"], typeof(decimal));
          
        if(hashtable.ContainsKey("FilingFees"))
        billitem.FilingFees = (decimal) Convert.ChangeType(hashtable["FilingFees"], typeof(decimal));
          
        if(hashtable.ContainsKey("Status"))
        billitem.Status = ( String )hashtable["Status"];
          
        if(hashtable.ContainsKey("BillItemGuid"))
        billitem.BillItemGuid = ( String )hashtable["BillItemGuid"];
          

      return billitem;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From billitem
      Where
      BillItemId = ?BillItemId";
    public Billitem remove(Billitem billitem)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?BillItemId", billitem.BillItemId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billitem;
    }

    #endregion
  

    public Billitem save( Billitem billitem )
    {
      if(exists(billitem))
        return update(billitem);
        return create(billitem);
    }

  


    private const String SqlUpdate = @"Update billitem Set 
    BillId = ?BillId,AssetAssignmentId = ?AssetAssignmentId,BillingDate = ?BillingDate,DayQty = ?DayQty,BillRate = ?BillRate,TotalHourlyBilling = ?TotalHourlyBilling,Lodging = ?Lodging,Meals = ?Meals,Phone = ?Phone,Copies = ?Copies,FilingFees = ?FilingFees,Status = ?Status,BillItemGuid = ?BillItemGuid
       Where 
      BillItemId = ?BillItemId";

    public Billitem update(Billitem billitem)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?BillItemId", billitem.BillItemId);
              
                  sqlCommand.Parameters.Add("?BillId", billitem.BillId);
              
                  sqlCommand.Parameters.Add("?AssetAssignmentId", billitem.AssetAssignmentId);
              
                  sqlCommand.Parameters.Add("?BillingDate", billitem.BillingDate);
              
                  sqlCommand.Parameters.Add("?DayQty", billitem.DayQty);
              
                  sqlCommand.Parameters.Add("?BillRate", billitem.BillRate);
              
                if(billitem.TotalHourlyBilling != null)
                  sqlCommand.Parameters.Add("?TotalHourlyBilling", billitem.TotalHourlyBilling);
                else
                  sqlCommand.Parameters.Add("?TotalHourlyBilling", DBNull.Value);
              
                if(billitem.Lodging != null)
                  sqlCommand.Parameters.Add("?Lodging", billitem.Lodging);
                else
                  sqlCommand.Parameters.Add("?Lodging", DBNull.Value);
              
                if(billitem.Meals != null)
                  sqlCommand.Parameters.Add("?Meals", billitem.Meals);
                else
                  sqlCommand.Parameters.Add("?Meals", DBNull.Value);
              
                if(billitem.Phone != null)
                  sqlCommand.Parameters.Add("?Phone", billitem.Phone);
                else
                  sqlCommand.Parameters.Add("?Phone", DBNull.Value);
              
                if(billitem.Copies != null)
                  sqlCommand.Parameters.Add("?Copies", billitem.Copies);
                else
                  sqlCommand.Parameters.Add("?Copies", DBNull.Value);
              
                if(billitem.FilingFees != null)
                  sqlCommand.Parameters.Add("?FilingFees", billitem.FilingFees);
                else
                  sqlCommand.Parameters.Add("?FilingFees", DBNull.Value);
              
                  sqlCommand.Parameters.Add("?Status", billitem.Status);
              
                  sqlCommand.Parameters.Add("?BillItemGuid", billitem.BillItemGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billitem;
    }

  
    }
    
  
    
    public partial class Billitemstatus
    {
    
      protected String _status;
    

    public Billitemstatus(){}

    public Billitemstatus(
    String 
            status
    )
    {
    
      _status = status;
    
    }

    
      public String Status
      {
      get { return _status;}
      set { _status = value; }
      }
    
    
    }
  

    public partial class BillitemstatusDataMapper:TDataMapper<Billitemstatus,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "billitemstatus";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into billitemstatus (
    Status) Values (
    
      ?Status);";

    public override Billitemstatus create( Billitemstatus billitemstatus )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?Status", billitemstatus.Status);
                
    
            sqlCommand.ExecuteNonQuery();

              
            }
        }

      return billitemstatus;
    }

  

    private const String SqlSelectAll = @"Select
    Status 
    From billitemstatus ";
    
    public List<Billitemstatus> findAll(Object args)
    {
      List<Billitemstatus> rv = new List<Billitemstatus>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    Status
     From billitemstatus
       Where 
      Status = ?Status
    ";

    public Billitemstatus findByPrimaryKey(
    String status
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?Status", status);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("billitemstatus not found, search by primary key");
    }

    }


    public bool exists(Billitemstatus billitemstatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?Status", billitemstatus.Status);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Billitemstatus doLoad(IDataReader dataReader)
    {
    Billitemstatus billitemstatus = new Billitemstatus();

    billitemstatus.Status = dataReader.GetString(0);
        

    return billitemstatus;
    }


    protected override Billitemstatus doLoad(Hashtable hashtable)
    {
      Billitemstatus billitemstatus = new Billitemstatus();

      
        if(hashtable.ContainsKey("Status"))
        billitemstatus.Status = ( String )hashtable["Status"];
          

      return billitemstatus;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From billitemstatus
      Where
      Status = ?Status";
    public Billitemstatus remove(Billitemstatus billitemstatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?Status", billitemstatus.Status);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billitemstatus;
    }

    #endregion
  

    public Billitemstatus save( Billitemstatus billitemstatus )
    {
      if(exists(billitemstatus))
        return update(billitemstatus);
        return create(billitemstatus);
    }

  


    private const String SqlUpdate = @"Update billitemstatus Set 
    
       Where 
      Status = ?Status";

    public Billitemstatus update(Billitemstatus billitemstatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?Status", billitemstatus.Status);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billitemstatus;
    }

  
    }
    
  
    
    public partial class Client
    {
    
      protected int _clientId;
    
      protected String _clientName;
    
      protected byte _active;
    
      protected String _clientGuid;
    

    public Client(){}

    public Client(
    int 
            clientId
    )
    {
    
      _clientId = clientId;
    
    }

    

      public Client(
      int 
        clientId,String 
        clientName,byte 
        active,String 
        clientGuid
      )
      {
      
        _clientId = clientId;
      
        _clientName = clientName;
      
        _active = active;
      
        _clientGuid = clientGuid;
      
      }

    
      public int ClientId
      {
      get { return _clientId;}
      set { _clientId = value; }
      }
    
      public String ClientName
      {
      get { return _clientName;}
      set { _clientName = value; }
      }
    
      public byte Active
      {
      get { return _active;}
      set { _active = value; }
      }
    
      public String ClientGuid
      {
      get { return _clientGuid;}
      set { _clientGuid = value; }
      }
    
    
    }
  

    public partial class ClientDataMapper:TDataMapper<Client,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "client";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into client (
    ClientName,Active,ClientGuid) Values (
    
      ?ClientName,
      ?Active,
      ?ClientGuid);";

    public override Client create( Client client )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?ClientName", client.ClientName);
                
                    sqlCommand.Parameters.Add("?Active", client.Active);
                
                    sqlCommand.Parameters.Add("?ClientGuid", client.ClientGuid);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                client.ClientId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return client;
    }

  

    private const String SqlSelectAll = @"Select
    ClientId,ClientName,Active,ClientGuid 
    From client ";
    
    public List<Client> findAll(Object args)
    {
      List<Client> rv = new List<Client>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    ClientId,ClientName,Active,ClientGuid
     From client
       Where 
      ClientId = ?ClientId
    ";

    public Client findByPrimaryKey(
    int clientId
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?ClientId", clientId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("client not found, search by primary key");
    }

    }


    public bool exists(Client client)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?ClientId", client.ClientId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Client doLoad(IDataReader dataReader)
    {
    Client client = new Client();

    client.ClientId = dataReader.GetInt32(0);
        client.ClientName = dataReader.GetString(1);
        client.Active = dataReader.GetByte(2);
        client.ClientGuid = dataReader.GetString(3);
        

    return client;
    }


    protected override Client doLoad(Hashtable hashtable)
    {
      Client client = new Client();

      
        if(hashtable.ContainsKey("ClientId"))
        client.ClientId = (int) Convert.ChangeType(hashtable["ClientId"], typeof(int));
          
        if(hashtable.ContainsKey("ClientName"))
        client.ClientName = ( String )hashtable["ClientName"];
          
        if(hashtable.ContainsKey("Active"))
        client.Active = (byte) Convert.ChangeType(hashtable["Active"], typeof(byte));
          
        if(hashtable.ContainsKey("ClientGuid"))
        client.ClientGuid = ( String )hashtable["ClientGuid"];
          

      return client;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From client
      Where
      ClientId = ?ClientId";
    public Client remove(Client client)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?ClientId", client.ClientId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return client;
    }

    #endregion
  

    public Client save( Client client )
    {
      if(exists(client))
        return update(client);
        return create(client);
    }

  


    private const String SqlUpdate = @"Update client Set 
    ClientName = ?ClientName,Active = ?Active,ClientGuid = ?ClientGuid
       Where 
      ClientId = ?ClientId";

    public Client update(Client client)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?ClientId", client.ClientId);
              
                  sqlCommand.Parameters.Add("?ClientName", client.ClientName);
              
                  sqlCommand.Parameters.Add("?Active", client.Active);
              
                  sqlCommand.Parameters.Add("?ClientGuid", client.ClientGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return client;
    }

  
    }
    
  
    
    public partial class Rate
    {
    
      protected int _assetId;
    
      protected int _clientId;
    
      protected decimal _dateRate;
    
      protected decimal _milageRate;
    
      protected String _rateGuid;
    

    public Rate(){}

    public Rate(
    int 
            assetId,int 
            clientId
    )
    {
    
      _assetId = assetId;
    
      _clientId = clientId;
    
    }

    

      public Rate(
      int 
        assetId,int 
        clientId,decimal 
        dateRate,decimal 
        milageRate,String 
        rateGuid
      )
      {
      
        _assetId = assetId;
      
        _clientId = clientId;
      
        _dateRate = dateRate;
      
        _milageRate = milageRate;
      
        _rateGuid = rateGuid;
      
      }

    
      public int AssetId
      {
      get { return _assetId;}
      set { _assetId = value; }
      }
    
      public int ClientId
      {
      get { return _clientId;}
      set { _clientId = value; }
      }
    
      public decimal DateRate
      {
      get { return _dateRate;}
      set { _dateRate = value; }
      }
    
      public decimal MilageRate
      {
      get { return _milageRate;}
      set { _milageRate = value; }
      }
    
      public String RateGuid
      {
      get { return _rateGuid;}
      set { _rateGuid = value; }
      }
    
    
    }
  

    public partial class RateDataMapper:TDataMapper<Rate,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "rate";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into rate (
    AssetId,ClientId,DateRate,MilageRate,RateGuid) Values (
    
      ?AssetId,
      ?ClientId,
      ?DateRate,
      ?MilageRate,
      ?RateGuid);";

    public override Rate create( Rate rate )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?AssetId", rate.AssetId);
                
                    sqlCommand.Parameters.Add("?ClientId", rate.ClientId);
                
                    sqlCommand.Parameters.Add("?DateRate", rate.DateRate);
                
                    sqlCommand.Parameters.Add("?MilageRate", rate.MilageRate);
                
                    sqlCommand.Parameters.Add("?RateGuid", rate.RateGuid);
                
    
            sqlCommand.ExecuteNonQuery();

              
            }
        }

      return rate;
    }

  

    private const String SqlSelectAll = @"Select
    AssetId,ClientId,DateRate,MilageRate,RateGuid 
    From rate ";
    
    public List<Rate> findAll(Object args)
    {
      List<Rate> rv = new List<Rate>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    AssetId,ClientId,DateRate,MilageRate,RateGuid
     From rate
       Where 
      AssetId = ?AssetId and ClientId = ?ClientId
    ";

    public Rate findByPrimaryKey(
    int assetId,int clientId
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AssetId", assetId);
        
          sqlCommand.Parameters.Add("?ClientId", clientId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("rate not found, search by primary key");
    }

    }


    public bool exists(Rate rate)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AssetId", rate.AssetId);
          
            sqlCommand.Parameters.Add("?ClientId", rate.ClientId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Rate doLoad(IDataReader dataReader)
    {
    Rate rate = new Rate();

    rate.AssetId = dataReader.GetInt32(0);
        rate.ClientId = dataReader.GetInt32(1);
        rate.DateRate = dataReader.GetDecimal(2);
        rate.MilageRate = dataReader.GetDecimal(3);
        rate.RateGuid = dataReader.GetString(4);
        

    return rate;
    }


    protected override Rate doLoad(Hashtable hashtable)
    {
      Rate rate = new Rate();

      
        if(hashtable.ContainsKey("AssetId"))
        rate.AssetId = (int) Convert.ChangeType(hashtable["AssetId"], typeof(int));
          
        if(hashtable.ContainsKey("ClientId"))
        rate.ClientId = (int) Convert.ChangeType(hashtable["ClientId"], typeof(int));
          
        if(hashtable.ContainsKey("DateRate"))
        rate.DateRate = (decimal) Convert.ChangeType(hashtable["DateRate"], typeof(decimal));
          
        if(hashtable.ContainsKey("MilageRate"))
        rate.MilageRate = (decimal) Convert.ChangeType(hashtable["MilageRate"], typeof(decimal));
          
        if(hashtable.ContainsKey("RateGuid"))
        rate.RateGuid = ( String )hashtable["RateGuid"];
          

      return rate;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From rate
      Where
      AssetId = ?AssetId and ClientId = ?ClientId";
    public Rate remove(Rate rate)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AssetId", rate.AssetId);
        
          sqlCommand.Parameters.Add("?ClientId", rate.ClientId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return rate;
    }

    #endregion
  

    public Rate save( Rate rate )
    {
      if(exists(rate))
        return update(rate);
        return create(rate);
    }

  


    private const String SqlUpdate = @"Update rate Set 
    DateRate = ?DateRate,MilageRate = ?MilageRate,RateGuid = ?RateGuid
       Where 
      AssetId = ?AssetId and ClientId = ?ClientId";

    public Rate update(Rate rate)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AssetId", rate.AssetId);
              
                  sqlCommand.Parameters.Add("?ClientId", rate.ClientId);
              
                  sqlCommand.Parameters.Add("?DateRate", rate.DateRate);
              
                  sqlCommand.Parameters.Add("?MilageRate", rate.MilageRate);
              
                  sqlCommand.Parameters.Add("?RateGuid", rate.RateGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return rate;
    }

  
    }
    
  
    
    public partial class Subafe
    {
    
      protected String _subAFE;
    
      protected String _aFE;
    
      protected String _subAFEStatus;
    
      protected String _subAfeGuid;
    

    public Subafe(){}

    public Subafe(
    String 
            subAFE
    )
    {
    
      _subAFE = subAFE;
    
    }

    

      public Subafe(
      String 
        subAFE,String 
        aFE,String 
        subAFEStatus,String 
        subAfeGuid
      )
      {
      
        _subAFE = subAFE;
      
        _aFE = aFE;
      
        _subAFEStatus = subAFEStatus;
      
        _subAfeGuid = subAfeGuid;
      
      }

    
      public String SubAFE
      {
      get { return _subAFE;}
      set { _subAFE = value; }
      }
    
      public String AFE
      {
      get { return _aFE;}
      set { _aFE = value; }
      }
    
      public String SubAFEStatus
      {
      get { return _subAFEStatus;}
      set { _subAFEStatus = value; }
      }
    
      public String SubAfeGuid
      {
      get { return _subAfeGuid;}
      set { _subAfeGuid = value; }
      }
    
    
    }
  

    public partial class SubafeDataMapper:TDataMapper<Subafe,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "subafe";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into subafe (
    SubAFE,AFE,SubAFEStatus,SubAfeGuid) Values (
    
      ?SubAFE,
      ?AFE,
      ?SubAFEStatus,
      ?SubAfeGuid);";

    public override Subafe create( Subafe subafe )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?SubAFE", subafe.SubAFE);
                
                    sqlCommand.Parameters.Add("?AFE", subafe.AFE);
                
                    sqlCommand.Parameters.Add("?SubAFEStatus", subafe.SubAFEStatus);
                
                    sqlCommand.Parameters.Add("?SubAfeGuid", subafe.SubAfeGuid);
                
    
            sqlCommand.ExecuteNonQuery();

              
            }
        }

      return subafe;
    }

  

    private const String SqlSelectAll = @"Select
    SubAFE,AFE,SubAFEStatus,SubAfeGuid 
    From subafe ";
    
    public List<Subafe> findAll(Object args)
    {
      List<Subafe> rv = new List<Subafe>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    SubAFE,AFE,SubAFEStatus,SubAfeGuid
     From subafe
       Where 
      SubAFE = ?SubAFE
    ";

    public Subafe findByPrimaryKey(
    String subAFE
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?SubAFE", subAFE);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("subafe not found, search by primary key");
    }

    }


    public bool exists(Subafe subafe)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?SubAFE", subafe.SubAFE);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Subafe doLoad(IDataReader dataReader)
    {
    Subafe subafe = new Subafe();

    subafe.SubAFE = dataReader.GetString(0);
        subafe.AFE = dataReader.GetString(1);
        subafe.SubAFEStatus = dataReader.GetString(2);
        subafe.SubAfeGuid = dataReader.GetString(3);
        

    return subafe;
    }


    protected override Subafe doLoad(Hashtable hashtable)
    {
      Subafe subafe = new Subafe();

      
        if(hashtable.ContainsKey("SubAFE"))
        subafe.SubAFE = ( String )hashtable["SubAFE"];
          
        if(hashtable.ContainsKey("AFE"))
        subafe.AFE = ( String )hashtable["AFE"];
          
        if(hashtable.ContainsKey("SubAFEStatus"))
        subafe.SubAFEStatus = ( String )hashtable["SubAFEStatus"];
          
        if(hashtable.ContainsKey("SubAfeGuid"))
        subafe.SubAfeGuid = ( String )hashtable["SubAfeGuid"];
          

      return subafe;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From subafe
      Where
      SubAFE = ?SubAFE";
    public Subafe remove(Subafe subafe)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?SubAFE", subafe.SubAFE);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subafe;
    }

    #endregion
  

    public Subafe save( Subafe subafe )
    {
      if(exists(subafe))
        return update(subafe);
        return create(subafe);
    }

  


    private const String SqlUpdate = @"Update subafe Set 
    AFE = ?AFE,SubAFEStatus = ?SubAFEStatus,SubAfeGuid = ?SubAfeGuid
       Where 
      SubAFE = ?SubAFE";

    public Subafe update(Subafe subafe)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?SubAFE", subafe.SubAFE);
              
                  sqlCommand.Parameters.Add("?AFE", subafe.AFE);
              
                  sqlCommand.Parameters.Add("?SubAFEStatus", subafe.SubAFEStatus);
              
                  sqlCommand.Parameters.Add("?SubAfeGuid", subafe.SubAfeGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subafe;
    }

  
    }
    
  
    
    public partial class Subafestatus
    {
    
      protected String _subAFEStatus;
    

    public Subafestatus(){}

    public Subafestatus(
    String 
            subAFEStatus
    )
    {
    
      _subAFEStatus = subAFEStatus;
    
    }

    
      public String SubAFEStatus
      {
      get { return _subAFEStatus;}
      set { _subAFEStatus = value; }
      }
    
    
    }
  

    public partial class SubafestatusDataMapper:TDataMapper<Subafestatus,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "subafestatus";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into subafestatus (
    SubAFEStatus) Values (
    
      ?SubAFEStatus);";

    public override Subafestatus create( Subafestatus subafestatus )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?SubAFEStatus", subafestatus.SubAFEStatus);
                
    
            sqlCommand.ExecuteNonQuery();

              
            }
        }

      return subafestatus;
    }

  

    private const String SqlSelectAll = @"Select
    SubAFEStatus 
    From subafestatus ";
    
    public List<Subafestatus> findAll(Object args)
    {
      List<Subafestatus> rv = new List<Subafestatus>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    SubAFEStatus
     From subafestatus
       Where 
      SubAFEStatus = ?SubAFEStatus
    ";

    public Subafestatus findByPrimaryKey(
    String subAFEStatus
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?SubAFEStatus", subAFEStatus);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("subafestatus not found, search by primary key");
    }

    }


    public bool exists(Subafestatus subafestatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?SubAFEStatus", subafestatus.SubAFEStatus);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Subafestatus doLoad(IDataReader dataReader)
    {
    Subafestatus subafestatus = new Subafestatus();

    subafestatus.SubAFEStatus = dataReader.GetString(0);
        

    return subafestatus;
    }


    protected override Subafestatus doLoad(Hashtable hashtable)
    {
      Subafestatus subafestatus = new Subafestatus();

      
        if(hashtable.ContainsKey("SubAFEStatus"))
        subafestatus.SubAFEStatus = ( String )hashtable["SubAFEStatus"];
          

      return subafestatus;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From subafestatus
      Where
      SubAFEStatus = ?SubAFEStatus";
    public Subafestatus remove(Subafestatus subafestatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?SubAFEStatus", subafestatus.SubAFEStatus);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subafestatus;
    }

    #endregion
  

    public Subafestatus save( Subafestatus subafestatus )
    {
      if(exists(subafestatus))
        return update(subafestatus);
        return create(subafestatus);
    }

  


    private const String SqlUpdate = @"Update subafestatus Set 
    
       Where 
      SubAFEStatus = ?SubAFEStatus";

    public Subafestatus update(Subafestatus subafestatus)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?SubAFEStatus", subafestatus.SubAFEStatus);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subafestatus;
    }

  
    }
    
  
    
    public partial class Synclog
    {
    
      protected int _syncLogId;
    
      protected int _assetId;
    
      protected String _deviceId;
    

    public Synclog(){}

    public Synclog(
    int 
            syncLogId
    )
    {
    
      _syncLogId = syncLogId;
    
    }

    

      public Synclog(
      int 
        syncLogId,int 
        assetId,String 
        deviceId
      )
      {
      
        _syncLogId = syncLogId;
      
        _assetId = assetId;
      
        _deviceId = deviceId;
      
      }

    
      public int SyncLogId
      {
      get { return _syncLogId;}
      set { _syncLogId = value; }
      }
    
      public int AssetId
      {
      get { return _assetId;}
      set { _assetId = value; }
      }
    
      public String DeviceId
      {
      get { return _deviceId;}
      set { _deviceId = value; }
      }
    
    
    }
  

    public partial class SynclogDataMapper:TDataMapper<Synclog,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "synclog";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into synclog (
    AssetId,DeviceId) Values (
    
      ?AssetId,
      ?DeviceId);";

    public override Synclog create( Synclog synclog )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?AssetId", synclog.AssetId);
                
                    sqlCommand.Parameters.Add("?DeviceId", synclog.DeviceId);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                synclog.SyncLogId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return synclog;
    }

  

    private const String SqlSelectAll = @"Select
    SyncLogId,AssetId,DeviceId 
    From synclog ";
    
    public List<Synclog> findAll(Object args)
    {
      List<Synclog> rv = new List<Synclog>();

      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectAll,database.Connection))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
      }

      return rv;
     } 

    }
  

    private const String SqlSelectByPk = @"Select
    SyncLogId,AssetId,DeviceId
     From synclog
       Where 
      SyncLogId = ?SyncLogId
    ";

    public Synclog findByPrimaryKey(
    int syncLogId
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?SyncLogId", syncLogId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("synclog not found, search by primary key");
    }

    }


    public bool exists(Synclog synclog)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?SyncLogId", synclog.SyncLogId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
              return dataReader.Read();
            }
        }
      }
    }
  

    protected override Synclog doLoad(IDataReader dataReader)
    {
    Synclog synclog = new Synclog();

    synclog.SyncLogId = dataReader.GetInt32(0);
        synclog.AssetId = dataReader.GetInt32(1);
        synclog.DeviceId = dataReader.GetString(2);
        

    return synclog;
    }


    protected override Synclog doLoad(Hashtable hashtable)
    {
      Synclog synclog = new Synclog();

      
        if(hashtable.ContainsKey("SyncLogId"))
        synclog.SyncLogId = (int) Convert.ChangeType(hashtable["SyncLogId"], typeof(int));
          
        if(hashtable.ContainsKey("AssetId"))
        synclog.AssetId = (int) Convert.ChangeType(hashtable["AssetId"], typeof(int));
          
        if(hashtable.ContainsKey("DeviceId"))
        synclog.DeviceId = ( String )hashtable["DeviceId"];
          

      return synclog;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From synclog
      Where
      SyncLogId = ?SyncLogId";
    public Synclog remove(Synclog synclog)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?SyncLogId", synclog.SyncLogId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return synclog;
    }

    #endregion
  

    public Synclog save( Synclog synclog )
    {
      if(exists(synclog))
        return update(synclog);
        return create(synclog);
    }

  


    private const String SqlUpdate = @"Update synclog Set 
    AssetId = ?AssetId,DeviceId = ?DeviceId
       Where 
      SyncLogId = ?SyncLogId";

    public Synclog update(Synclog synclog)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?SyncLogId", synclog.SyncLogId);
              
                  sqlCommand.Parameters.Add("?AssetId", synclog.AssetId);
              
                  sqlCommand.Parameters.Add("?DeviceId", synclog.DeviceId);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return synclog;
    }

  
    }
    
  
      }
    