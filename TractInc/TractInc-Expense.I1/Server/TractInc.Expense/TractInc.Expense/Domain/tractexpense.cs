
      namespace TractInc.Expense.Domain
      {
      using System;
      
    
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using Weborb.Data.Management;

    
    using System.Data.SqlClient;
    using Weborb.Data.Management.MSSql;
  
    public class Database:TDatabase<SqlConnection>
    {
        public Database()
        {
          ConnectionString = "server=(local);user id=sa;password=gfhjkm;database=tractexpense;connect timeout=30;";
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
  

    public partial class AfeDataMapper:TDataMapper<Afe,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "Afe";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Afe] (
    AFE,ClientId,AFEName,AFEStatus,AfeGuid) Values (
    
      @AFE,
      @ClientId,
      @AFEName,
      @AFEStatus,
      @AfeGuid);
    ";

    public override Afe create( Afe afe )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@AFE", afe.AFE);
              
                  sqlCommand.Parameters.Add("@ClientId", afe.ClientId);
              
                  sqlCommand.Parameters.Add("@AFEName", afe.AFEName);
              
                  sqlCommand.Parameters.Add("@AFEStatus", afe.AFEStatus);
              
                  sqlCommand.Parameters.Add("@AfeGuid", afe.AfeGuid);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return afe;
    }

  

    private const String SqlSelectAll = @"Select
    AFE,ClientId,AFEName,AFEStatus,AfeGuid 
    From [Afe] ";
    
    public List<Afe> findAll(Object args)
    {
      List<Afe> rv = new List<Afe>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [Afe]
    
       Where 
      AFE = @AFE
    ";

    public Afe findByPrimaryKey(
    String aFE
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@AFE", aFE);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("Afe not found, search by primary key");
    }

    }


    public bool exists(Afe afe)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@AFE", afe.AFE);
          

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
            afe.AFE = ( String)hashtable["AFE"];
      
        if(hashtable.ContainsKey("ClientId"))
            afe.ClientId = ( int)hashtable["ClientId"];
      
        if(hashtable.ContainsKey("AFEName"))
            afe.AFEName = ( String)hashtable["AFEName"];
      
        if(hashtable.ContainsKey("AFEStatus"))
            afe.AFEStatus = ( String)hashtable["AFEStatus"];
      
        if(hashtable.ContainsKey("AfeGuid"))
            afe.AfeGuid = ( String)hashtable["AfeGuid"];
      

      return afe;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Afe]
    
      Where
      AFE = @AFE";
    public Afe remove(Afe afe)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@AFE", afe.AFE);
        
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

  


    private const String SqlUpdate = @"Update [Afe] Set 
    ClientId = @ClientId,AFEName = @AFEName,AFEStatus = @AFEStatus,AfeGuid = @AfeGuid
       Where 
      AFE = @AFE";

    public Afe update(Afe afe)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@AFE", afe.AFE);
              
                  sqlCommand.Parameters.AddWithValue("@ClientId", afe.ClientId);
              
                  sqlCommand.Parameters.AddWithValue("@AFEName", afe.AFEName);
              
                  sqlCommand.Parameters.AddWithValue("@AFEStatus", afe.AFEStatus);
              
                  sqlCommand.Parameters.AddWithValue("@AfeGuid", afe.AfeGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return afe;
    }

  
    }
    
  
    
    public partial class AfeStatus
    {
    
      protected String _aFEStatus;
    

    public AfeStatus(){}

    public AfeStatus(
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
  

    public partial class AfeStatusDataMapper:TDataMapper<AfeStatus,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "AfeStatus";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [AfeStatus] (
    AFEStatus) Values (
    
      @AFEStatus);
    ";

    public override AfeStatus create( AfeStatus afeStatus )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@AFEStatus", afeStatus.AFEStatus);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return afeStatus;
    }

  

    private const String SqlSelectAll = @"Select
    AFEStatus 
    From [AfeStatus] ";
    
    public List<AfeStatus> findAll(Object args)
    {
      List<AfeStatus> rv = new List<AfeStatus>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [AfeStatus]
    
       Where 
      AFEStatus = @AFEStatus
    ";

    public AfeStatus findByPrimaryKey(
    String aFEStatus
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@AFEStatus", aFEStatus);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("AfeStatus not found, search by primary key");
    }

    }


    public bool exists(AfeStatus afeStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@AFEStatus", afeStatus.AFEStatus);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override AfeStatus doLoad(IDataReader dataReader)
    {
    AfeStatus afeStatus = new AfeStatus();

    afeStatus.AFEStatus = dataReader.GetString(0);
        

    return afeStatus;
    }


    protected override AfeStatus doLoad(Hashtable hashtable)
    {
      AfeStatus afeStatus = new AfeStatus();

      
        if(hashtable.ContainsKey("AFEStatus"))
            afeStatus.AFEStatus = ( String)hashtable["AFEStatus"];
      

      return afeStatus;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [AfeStatus]
    
      Where
      AFEStatus = @AFEStatus";
    public AfeStatus remove(AfeStatus afeStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@AFEStatus", afeStatus.AFEStatus);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return afeStatus;
    }

    #endregion
  

    public AfeStatus save( AfeStatus afeStatus )
    {
      if(exists(afeStatus))
        return update(afeStatus);
        return create(afeStatus);
    }

  


    private const String SqlUpdate = @"Update [AfeStatus] Set 
    
       Where 
      AFEStatus = @AFEStatus";

    public AfeStatus update(AfeStatus afeStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@AFEStatus", afeStatus.AFEStatus);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return afeStatus;
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
  

    public partial class AssetDataMapper:TDataMapper<Asset,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "Asset";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Asset] (
    AssetType,ChiefAssetId,BusinessName,FirstName,MiddleName,LastName,SSN,AssetGuid) Values (
    
      @AssetType,
      @ChiefAssetId,
      @BusinessName,
      @FirstName,
      @MiddleName,
      @LastName,
      @SSN,
      @AssetGuid);
    
      select scope_identity();
    ";

    public override Asset create( Asset asset )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@AssetType", asset.AssetType);
              
                  sqlCommand.Parameters.Add("@ChiefAssetId", asset.ChiefAssetId);
              
                  sqlCommand.Parameters.Add("@BusinessName", asset.BusinessName);
              
                  sqlCommand.Parameters.Add("@FirstName", asset.FirstName);
              
                  sqlCommand.Parameters.Add("@MiddleName", asset.MiddleName);
              
                  sqlCommand.Parameters.Add("@LastName", asset.LastName);
              
                  sqlCommand.Parameters.Add("@SSN", asset.SSN);
              
                  sqlCommand.Parameters.Add("@AssetGuid", asset.AssetGuid);
              asset.AssetId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
          }
      }

      return asset;
    }

  

    private const String SqlSelectAll = @"Select
    AssetId,AssetType,ChiefAssetId,BusinessName,FirstName,MiddleName,LastName,SSN,AssetGuid 
    From [Asset] ";
    
    public List<Asset> findAll(Object args)
    {
      List<Asset> rv = new List<Asset>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [Asset]
    
       Where 
      AssetId = @AssetId
    ";

    public Asset findByPrimaryKey(
    int assetId
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@AssetId", assetId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("Asset not found, search by primary key");
    }

    }


    public bool exists(Asset asset)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@AssetId", asset.AssetId);
          

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
            asset.AssetId = ( int)hashtable["AssetId"];
      
        if(hashtable.ContainsKey("AssetType"))
            asset.AssetType = ( String)hashtable["AssetType"];
      
        if(hashtable.ContainsKey("ChiefAssetId"))
            asset.ChiefAssetId = ( int)hashtable["ChiefAssetId"];
      
        if(hashtable.ContainsKey("BusinessName"))
            asset.BusinessName = ( String)hashtable["BusinessName"];
      
        if(hashtable.ContainsKey("FirstName"))
            asset.FirstName = ( String)hashtable["FirstName"];
      
        if(hashtable.ContainsKey("MiddleName"))
            asset.MiddleName = ( String)hashtable["MiddleName"];
      
        if(hashtable.ContainsKey("LastName"))
            asset.LastName = ( String)hashtable["LastName"];
      
        if(hashtable.ContainsKey("SSN"))
            asset.SSN = ( String)hashtable["SSN"];
      
        if(hashtable.ContainsKey("AssetGuid"))
            asset.AssetGuid = ( String)hashtable["AssetGuid"];
      

      return asset;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Asset]
    
      Where
      AssetId = @AssetId";
    public Asset remove(Asset asset)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@AssetId", asset.AssetId);
        
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

  


    private const String SqlUpdate = @"Update [Asset] Set 
    AssetType = @AssetType,ChiefAssetId = @ChiefAssetId,BusinessName = @BusinessName,FirstName = @FirstName,MiddleName = @MiddleName,LastName = @LastName,SSN = @SSN,AssetGuid = @AssetGuid
       Where 
      AssetId = @AssetId";

    public Asset update(Asset asset)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@AssetId", asset.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@AssetType", asset.AssetType);
              
                  sqlCommand.Parameters.AddWithValue("@ChiefAssetId", asset.ChiefAssetId);
              
                  sqlCommand.Parameters.AddWithValue("@BusinessName", asset.BusinessName);
              
                  sqlCommand.Parameters.AddWithValue("@FirstName", asset.FirstName);
              
                  sqlCommand.Parameters.AddWithValue("@MiddleName", asset.MiddleName);
              
                  sqlCommand.Parameters.AddWithValue("@LastName", asset.LastName);
              
                  sqlCommand.Parameters.AddWithValue("@SSN", asset.SSN);
              
                  sqlCommand.Parameters.AddWithValue("@AssetGuid", asset.AssetGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return asset;
    }

  
    }
    
  
    
    public partial class AssetAssignment
    {
    
      protected int _assetAssignmentId;
    
      protected String _aFE;
    
      protected String _subAFE;
    
      protected int _assetId;
    
      protected decimal _billRate;
    
      protected decimal _payRate;
    
      protected String _assetAssignmentGuid;
    

    public AssetAssignment(){}

    public AssetAssignment(
    int 
            assetAssignmentId
    )
    {
    
      _assetAssignmentId = assetAssignmentId;
    
    }

    

      public AssetAssignment(
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
  

    public partial class AssetAssignmentDataMapper:TDataMapper<AssetAssignment,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "AssetAssignment";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [AssetAssignment] (
    AFE,SubAFE,AssetId,BillRate,PayRate,AssetAssignmentGuid) Values (
    
      @AFE,
      @SubAFE,
      @AssetId,
      @BillRate,
      @PayRate,
      @AssetAssignmentGuid);
    
      select scope_identity();
    ";

    public override AssetAssignment create( AssetAssignment assetAssignment )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@AFE", assetAssignment.AFE);
              
                  sqlCommand.Parameters.Add("@SubAFE", assetAssignment.SubAFE);
              
                  sqlCommand.Parameters.Add("@AssetId", assetAssignment.AssetId);
              
                  sqlCommand.Parameters.Add("@BillRate", assetAssignment.BillRate);
              
                  sqlCommand.Parameters.Add("@PayRate", assetAssignment.PayRate);
              
                  sqlCommand.Parameters.Add("@AssetAssignmentGuid", assetAssignment.AssetAssignmentGuid);
              assetAssignment.AssetAssignmentId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
          }
      }

      return assetAssignment;
    }

  

    private const String SqlSelectAll = @"Select
    AssetAssignmentId,AFE,SubAFE,AssetId,BillRate,PayRate,AssetAssignmentGuid 
    From [AssetAssignment] ";
    
    public List<AssetAssignment> findAll(Object args)
    {
      List<AssetAssignment> rv = new List<AssetAssignment>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [AssetAssignment]
    
       Where 
      AssetAssignmentId = @AssetAssignmentId
    ";

    public AssetAssignment findByPrimaryKey(
    int assetAssignmentId
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@AssetAssignmentId", assetAssignmentId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("AssetAssignment not found, search by primary key");
    }

    }


    public bool exists(AssetAssignment assetAssignment)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@AssetAssignmentId", assetAssignment.AssetAssignmentId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override AssetAssignment doLoad(IDataReader dataReader)
    {
    AssetAssignment assetAssignment = new AssetAssignment();

    assetAssignment.AssetAssignmentId = dataReader.GetInt32(0);
        assetAssignment.AFE = dataReader.GetString(1);
        assetAssignment.SubAFE = dataReader.GetString(2);
        assetAssignment.AssetId = dataReader.GetInt32(3);
        assetAssignment.BillRate = dataReader.GetDecimal(4);
        assetAssignment.PayRate = dataReader.GetDecimal(5);
        assetAssignment.AssetAssignmentGuid = dataReader.GetString(6);
        

    return assetAssignment;
    }


    protected override AssetAssignment doLoad(Hashtable hashtable)
    {
      AssetAssignment assetAssignment = new AssetAssignment();

      
        if(hashtable.ContainsKey("AssetAssignmentId"))
            assetAssignment.AssetAssignmentId = ( int)hashtable["AssetAssignmentId"];
      
        if(hashtable.ContainsKey("AFE"))
            assetAssignment.AFE = ( String)hashtable["AFE"];
      
        if(hashtable.ContainsKey("SubAFE"))
            assetAssignment.SubAFE = ( String)hashtable["SubAFE"];
      
        if(hashtable.ContainsKey("AssetId"))
            assetAssignment.AssetId = ( int)hashtable["AssetId"];
      
        if(hashtable.ContainsKey("BillRate"))
            assetAssignment.BillRate = ( decimal)hashtable["BillRate"];
      
        if(hashtable.ContainsKey("PayRate"))
            assetAssignment.PayRate = ( decimal)hashtable["PayRate"];
      
        if(hashtable.ContainsKey("AssetAssignmentGuid"))
            assetAssignment.AssetAssignmentGuid = ( String)hashtable["AssetAssignmentGuid"];
      

      return assetAssignment;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [AssetAssignment]
    
      Where
      AssetAssignmentId = @AssetAssignmentId";
    public AssetAssignment remove(AssetAssignment assetAssignment)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@AssetAssignmentId", assetAssignment.AssetAssignmentId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assetAssignment;
    }

    #endregion
  

    public AssetAssignment save( AssetAssignment assetAssignment )
    {
      if(exists(assetAssignment))
        return update(assetAssignment);
        return create(assetAssignment);
    }

  


    private const String SqlUpdate = @"Update [AssetAssignment] Set 
    AFE = @AFE,SubAFE = @SubAFE,AssetId = @AssetId,BillRate = @BillRate,PayRate = @PayRate,AssetAssignmentGuid = @AssetAssignmentGuid
       Where 
      AssetAssignmentId = @AssetAssignmentId";

    public AssetAssignment update(AssetAssignment assetAssignment)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@AssetAssignmentId", assetAssignment.AssetAssignmentId);
              
                  sqlCommand.Parameters.AddWithValue("@AFE", assetAssignment.AFE);
              
                  sqlCommand.Parameters.AddWithValue("@SubAFE", assetAssignment.SubAFE);
              
                  sqlCommand.Parameters.AddWithValue("@AssetId", assetAssignment.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@BillRate", assetAssignment.BillRate);
              
                  sqlCommand.Parameters.AddWithValue("@PayRate", assetAssignment.PayRate);
              
                  sqlCommand.Parameters.AddWithValue("@AssetAssignmentGuid", assetAssignment.AssetAssignmentGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assetAssignment;
    }

  
    }
    
  
    
    public partial class AssetType
    {
    
      protected String _type;
    

    public AssetType(){}

    public AssetType(
    String 
            type
    )
    {
    
      _type = type;
    
    }

    
      public String Type
      {
      get { return _type;}
      set { _type = value; }
      }
    
    
    }
  

    public partial class AssetTypeDataMapper:TDataMapper<AssetType,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "AssetType";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [AssetType] (
    Type) Values (
    
      @Type);
    ";

    public override AssetType create( AssetType assetType )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@Type", assetType.Type);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return assetType;
    }

  

    private const String SqlSelectAll = @"Select
    Type 
    From [AssetType] ";
    
    public List<AssetType> findAll(Object args)
    {
      List<AssetType> rv = new List<AssetType>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
    Type
     From [AssetType]
    
       Where 
      Type = @Type
    ";

    public AssetType findByPrimaryKey(
    String type
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@Type", type);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("AssetType not found, search by primary key");
    }

    }


    public bool exists(AssetType assetType)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@Type", assetType.Type);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override AssetType doLoad(IDataReader dataReader)
    {
    AssetType assetType = new AssetType();

    assetType.Type = dataReader.GetString(0);
        

    return assetType;
    }


    protected override AssetType doLoad(Hashtable hashtable)
    {
      AssetType assetType = new AssetType();

      
        if(hashtable.ContainsKey("Type"))
            assetType.Type = ( String)hashtable["Type"];
      

      return assetType;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [AssetType]
    
      Where
      Type = @Type";
    public AssetType remove(AssetType assetType)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@Type", assetType.Type);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assetType;
    }

    #endregion
  

    public AssetType save( AssetType assetType )
    {
      if(exists(assetType))
        return update(assetType);
        return create(assetType);
    }

  


    private const String SqlUpdate = @"Update [AssetType] Set 
    
       Where 
      Type = @Type";

    public AssetType update(AssetType assetType)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@Type", assetType.Type);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return assetType;
    }

  
    }
    
  
    
    public partial class Bill
    {
    
      protected int _billId;
    
      protected String _billStatus;
    
      protected String _notes;
    
      protected String _billGuid;
    

    public Bill(){}

    public Bill(
    int 
            billId
    )
    {
    
      _billId = billId;
    
    }

    

      public Bill(
      int 
        billId,String 
        billStatus,String 
        notes,String 
        billGuid
      )
      {
      
        _billId = billId;
      
        _billStatus = billStatus;
      
        _notes = notes;
      
        _billGuid = billGuid;
      
      }

    
      public int BillId
      {
      get { return _billId;}
      set { _billId = value; }
      }
    
      public String BillStatus
      {
      get { return _billStatus;}
      set { _billStatus = value; }
      }
    
      public String Notes
      {
      get { return _notes;}
      set { _notes = value; }
      }
    
      public String BillGuid
      {
      get { return _billGuid;}
      set { _billGuid = value; }
      }
    
    
    }
  

    public partial class BillDataMapper:TDataMapper<Bill,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "Bill";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Bill] (
    BillStatus,Notes,BillGuid) Values (
    
      @BillStatus,
      @Notes,
      @BillGuid);
    
      select scope_identity();
    ";

    public override Bill create( Bill bill )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@BillStatus", bill.BillStatus);
              
                  sqlCommand.Parameters.Add("@Notes", bill.Notes);
              
                  sqlCommand.Parameters.Add("@BillGuid", bill.BillGuid);
              bill.BillId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
          }
      }

      return bill;
    }

  

    private const String SqlSelectAll = @"Select
    BillId,BillStatus,Notes,BillGuid 
    From [Bill] ";
    
    public List<Bill> findAll(Object args)
    {
      List<Bill> rv = new List<Bill>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
    BillId,BillStatus,Notes,BillGuid
     From [Bill]
    
       Where 
      BillId = @BillId
    ";

    public Bill findByPrimaryKey(
    int billId
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@BillId", billId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("Bill not found, search by primary key");
    }

    }


    public bool exists(Bill bill)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@BillId", bill.BillId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Bill doLoad(IDataReader dataReader)
    {
    Bill bill = new Bill();

    bill.BillId = dataReader.GetInt32(0);
        bill.BillStatus = dataReader.GetString(1);
        bill.Notes = dataReader.GetString(2);
        bill.BillGuid = dataReader.GetString(3);
        

    return bill;
    }


    protected override Bill doLoad(Hashtable hashtable)
    {
      Bill bill = new Bill();

      
        if(hashtable.ContainsKey("BillId"))
            bill.BillId = ( int)hashtable["BillId"];
      
        if(hashtable.ContainsKey("BillStatus"))
            bill.BillStatus = ( String)hashtable["BillStatus"];
      
        if(hashtable.ContainsKey("Notes"))
            bill.Notes = ( String)hashtable["Notes"];
      
        if(hashtable.ContainsKey("BillGuid"))
            bill.BillGuid = ( String)hashtable["BillGuid"];
      

      return bill;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Bill]
    
      Where
      BillId = @BillId";
    public Bill remove(Bill bill)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@BillId", bill.BillId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return bill;
    }

    #endregion
  

    public Bill save( Bill bill )
    {
      if(exists(bill))
        return update(bill);
        return create(bill);
    }

  


    private const String SqlUpdate = @"Update [Bill] Set 
    BillStatus = @BillStatus,Notes = @Notes,BillGuid = @BillGuid
       Where 
      BillId = @BillId";

    public Bill update(Bill bill)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@BillId", bill.BillId);
              
                  sqlCommand.Parameters.AddWithValue("@BillStatus", bill.BillStatus);
              
                  sqlCommand.Parameters.AddWithValue("@Notes", bill.Notes);
              
                  sqlCommand.Parameters.AddWithValue("@BillGuid", bill.BillGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return bill;
    }

  
    }
    
  
    
    public partial class BillItem
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
    
      protected String _notes;
    
      protected String _billItemGuid;
    

    public BillItem(){}

    public BillItem(
    int 
            billItemId
    )
    {
    
      _billItemId = billItemId;
    
    }

    

      public BillItem(
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
        notes,String 
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
      
        _notes = notes;
      
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
    
      public String Notes
      {
      get { return _notes;}
      set { _notes = value; }
      }
    
      public String BillItemGuid
      {
      get { return _billItemGuid;}
      set { _billItemGuid = value; }
      }
    
    
    }
  

    public partial class BillItemDataMapper:TDataMapper<BillItem,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "BillItem";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [BillItem] (
    BillId,AssetAssignmentId,BillingDate,DayQty,BillRate,TotalHourlyBilling,Lodging,Meals,Phone,Copies,FilingFees,Status,Notes,BillItemGuid) Values (
    
      @BillId,
      @AssetAssignmentId,
      @BillingDate,
      @DayQty,
      @BillRate,
      @TotalHourlyBilling,
      @Lodging,
      @Meals,
      @Phone,
      @Copies,
      @FilingFees,
      @Status,
      @Notes,
      @BillItemGuid);
    
      select scope_identity();
    ";

    public override BillItem create( BillItem billItem )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@BillId", billItem.BillId);
              
                  sqlCommand.Parameters.Add("@AssetAssignmentId", billItem.AssetAssignmentId);
              
                  sqlCommand.Parameters.Add("@BillingDate", billItem.BillingDate);
              
                  sqlCommand.Parameters.Add("@DayQty", billItem.DayQty);
              
                  sqlCommand.Parameters.Add("@BillRate", billItem.BillRate);
              
                if(billItem.TotalHourlyBilling != null)
                  sqlCommand.Parameters.Add("@TotalHourlyBilling", billItem.TotalHourlyBilling);
                else
                  sqlCommand.Parameters.Add("@TotalHourlyBilling", DBNull.Value);
              
                if(billItem.Lodging != null)
                  sqlCommand.Parameters.Add("@Lodging", billItem.Lodging);
                else
                  sqlCommand.Parameters.Add("@Lodging", DBNull.Value);
              
                if(billItem.Meals != null)
                  sqlCommand.Parameters.Add("@Meals", billItem.Meals);
                else
                  sqlCommand.Parameters.Add("@Meals", DBNull.Value);
              
                if(billItem.Phone != null)
                  sqlCommand.Parameters.Add("@Phone", billItem.Phone);
                else
                  sqlCommand.Parameters.Add("@Phone", DBNull.Value);
              
                if(billItem.Copies != null)
                  sqlCommand.Parameters.Add("@Copies", billItem.Copies);
                else
                  sqlCommand.Parameters.Add("@Copies", DBNull.Value);
              
                if(billItem.FilingFees != null)
                  sqlCommand.Parameters.Add("@FilingFees", billItem.FilingFees);
                else
                  sqlCommand.Parameters.Add("@FilingFees", DBNull.Value);
              
                  sqlCommand.Parameters.Add("@Status", billItem.Status);
              
                  sqlCommand.Parameters.Add("@Notes", billItem.Notes);
              
                  sqlCommand.Parameters.Add("@BillItemGuid", billItem.BillItemGuid);
              billItem.BillItemId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
          }
      }

      return billItem;
    }

  

    private const String SqlSelectAll = @"Select
    BillItemId,BillId,AssetAssignmentId,BillingDate,DayQty,BillRate,TotalHourlyBilling,Lodging,Meals,Phone,Copies,FilingFees,Status,Notes,BillItemGuid 
    From [BillItem] ";
    
    public List<BillItem> findAll(Object args)
    {
      List<BillItem> rv = new List<BillItem>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
    BillItemId,BillId,AssetAssignmentId,BillingDate,DayQty,BillRate,TotalHourlyBilling,Lodging,Meals,Phone,Copies,FilingFees,Status,Notes,BillItemGuid
     From [BillItem]
    
       Where 
      BillItemId = @BillItemId
    ";

    public BillItem findByPrimaryKey(
    int billItemId
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@BillItemId", billItemId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("BillItem not found, search by primary key");
    }

    }


    public bool exists(BillItem billItem)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@BillItemId", billItem.BillItemId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override BillItem doLoad(IDataReader dataReader)
    {
    BillItem billItem = new BillItem();

    billItem.BillItemId = dataReader.GetInt32(0);
        billItem.BillId = dataReader.GetInt32(1);
        billItem.AssetAssignmentId = dataReader.GetInt32(2);
        billItem.BillingDate = dataReader.GetDateTime(3);
        billItem.DayQty = dataReader.GetInt32(4);
        billItem.BillRate = dataReader.GetDecimal(5);
        
          if(!dataReader.IsDBNull(6))
          billItem.TotalHourlyBilling = dataReader.GetDecimal(6);
        
          if(!dataReader.IsDBNull(7))
          billItem.Lodging = dataReader.GetDecimal(7);
        
          if(!dataReader.IsDBNull(8))
          billItem.Meals = dataReader.GetDecimal(8);
        
          if(!dataReader.IsDBNull(9))
          billItem.Phone = dataReader.GetDecimal(9);
        
          if(!dataReader.IsDBNull(10))
          billItem.Copies = dataReader.GetDecimal(10);
        
          if(!dataReader.IsDBNull(11))
          billItem.FilingFees = dataReader.GetDecimal(11);
        billItem.Status = dataReader.GetString(12);
        billItem.Notes = dataReader.GetString(13);
        billItem.BillItemGuid = dataReader.GetString(14);
        

    return billItem;
    }


    protected override BillItem doLoad(Hashtable hashtable)
    {
      BillItem billItem = new BillItem();

      
        if(hashtable.ContainsKey("BillItemId"))
            billItem.BillItemId = ( int)hashtable["BillItemId"];
      
        if(hashtable.ContainsKey("BillId"))
            billItem.BillId = ( int)hashtable["BillId"];
      
        if(hashtable.ContainsKey("AssetAssignmentId"))
            billItem.AssetAssignmentId = ( int)hashtable["AssetAssignmentId"];
      
        if(hashtable.ContainsKey("BillingDate"))
            billItem.BillingDate = ( DateTime)hashtable["BillingDate"];
      
        if(hashtable.ContainsKey("DayQty"))
            billItem.DayQty = ( int)hashtable["DayQty"];
      
        if(hashtable.ContainsKey("BillRate"))
            billItem.BillRate = ( decimal)hashtable["BillRate"];
      
        if(hashtable.ContainsKey("TotalHourlyBilling"))
            billItem.TotalHourlyBilling = ( decimal)hashtable["TotalHourlyBilling"];
      
        if(hashtable.ContainsKey("Lodging"))
            billItem.Lodging = ( decimal)hashtable["Lodging"];
      
        if(hashtable.ContainsKey("Meals"))
            billItem.Meals = ( decimal)hashtable["Meals"];
      
        if(hashtable.ContainsKey("Phone"))
            billItem.Phone = ( decimal)hashtable["Phone"];
      
        if(hashtable.ContainsKey("Copies"))
            billItem.Copies = ( decimal)hashtable["Copies"];
      
        if(hashtable.ContainsKey("FilingFees"))
            billItem.FilingFees = ( decimal)hashtable["FilingFees"];
      
        if(hashtable.ContainsKey("Status"))
            billItem.Status = ( String)hashtable["Status"];
      
        if(hashtable.ContainsKey("Notes"))
            billItem.Notes = ( String)hashtable["Notes"];
      
        if(hashtable.ContainsKey("BillItemGuid"))
            billItem.BillItemGuid = ( String)hashtable["BillItemGuid"];
      

      return billItem;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [BillItem]
    
      Where
      BillItemId = @BillItemId";
    public BillItem remove(BillItem billItem)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@BillItemId", billItem.BillItemId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billItem;
    }

    #endregion
  

    public BillItem save( BillItem billItem )
    {
      if(exists(billItem))
        return update(billItem);
        return create(billItem);
    }

  


    private const String SqlUpdate = @"Update [BillItem] Set 
    BillId = @BillId,AssetAssignmentId = @AssetAssignmentId,BillingDate = @BillingDate,DayQty = @DayQty,BillRate = @BillRate,TotalHourlyBilling = @TotalHourlyBilling,Lodging = @Lodging,Meals = @Meals,Phone = @Phone,Copies = @Copies,FilingFees = @FilingFees,Status = @Status,Notes = @Notes,BillItemGuid = @BillItemGuid
       Where 
      BillItemId = @BillItemId";

    public BillItem update(BillItem billItem)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@BillItemId", billItem.BillItemId);
              
                  sqlCommand.Parameters.AddWithValue("@BillId", billItem.BillId);
              
                  sqlCommand.Parameters.AddWithValue("@AssetAssignmentId", billItem.AssetAssignmentId);
              
                  sqlCommand.Parameters.AddWithValue("@BillingDate", billItem.BillingDate);
              
                  sqlCommand.Parameters.AddWithValue("@DayQty", billItem.DayQty);
              
                  sqlCommand.Parameters.AddWithValue("@BillRate", billItem.BillRate);
              
                if(billItem.TotalHourlyBilling != null)
                  sqlCommand.Parameters.AddWithValue("@TotalHourlyBilling", billItem.TotalHourlyBilling);
                else
                  sqlCommand.Parameters.AddWithValue("@TotalHourlyBilling", DBNull.Value);
              
                if(billItem.Lodging != null)
                  sqlCommand.Parameters.AddWithValue("@Lodging", billItem.Lodging);
                else
                  sqlCommand.Parameters.AddWithValue("@Lodging", DBNull.Value);
              
                if(billItem.Meals != null)
                  sqlCommand.Parameters.AddWithValue("@Meals", billItem.Meals);
                else
                  sqlCommand.Parameters.AddWithValue("@Meals", DBNull.Value);
              
                if(billItem.Phone != null)
                  sqlCommand.Parameters.AddWithValue("@Phone", billItem.Phone);
                else
                  sqlCommand.Parameters.AddWithValue("@Phone", DBNull.Value);
              
                if(billItem.Copies != null)
                  sqlCommand.Parameters.AddWithValue("@Copies", billItem.Copies);
                else
                  sqlCommand.Parameters.AddWithValue("@Copies", DBNull.Value);
              
                if(billItem.FilingFees != null)
                  sqlCommand.Parameters.AddWithValue("@FilingFees", billItem.FilingFees);
                else
                  sqlCommand.Parameters.AddWithValue("@FilingFees", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@Status", billItem.Status);
              
                  sqlCommand.Parameters.AddWithValue("@Notes", billItem.Notes);
              
                  sqlCommand.Parameters.AddWithValue("@BillItemGuid", billItem.BillItemGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billItem;
    }

  
    }
    
  
    
    public partial class BillItemStatus
    {
    
      protected String _status;
    

    public BillItemStatus(){}

    public BillItemStatus(
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
  

    public partial class BillItemStatusDataMapper:TDataMapper<BillItemStatus,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "BillItemStatus";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [BillItemStatus] (
    Status) Values (
    
      @Status);
    ";

    public override BillItemStatus create( BillItemStatus billItemStatus )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@Status", billItemStatus.Status);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return billItemStatus;
    }

  

    private const String SqlSelectAll = @"Select
    Status 
    From [BillItemStatus] ";
    
    public List<BillItemStatus> findAll(Object args)
    {
      List<BillItemStatus> rv = new List<BillItemStatus>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [BillItemStatus]
    
       Where 
      Status = @Status
    ";

    public BillItemStatus findByPrimaryKey(
    String status
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@Status", status);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("BillItemStatus not found, search by primary key");
    }

    }


    public bool exists(BillItemStatus billItemStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@Status", billItemStatus.Status);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override BillItemStatus doLoad(IDataReader dataReader)
    {
    BillItemStatus billItemStatus = new BillItemStatus();

    billItemStatus.Status = dataReader.GetString(0);
        

    return billItemStatus;
    }


    protected override BillItemStatus doLoad(Hashtable hashtable)
    {
      BillItemStatus billItemStatus = new BillItemStatus();

      
        if(hashtable.ContainsKey("Status"))
            billItemStatus.Status = ( String)hashtable["Status"];
      

      return billItemStatus;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [BillItemStatus]
    
      Where
      Status = @Status";
    public BillItemStatus remove(BillItemStatus billItemStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@Status", billItemStatus.Status);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billItemStatus;
    }

    #endregion
  

    public BillItemStatus save( BillItemStatus billItemStatus )
    {
      if(exists(billItemStatus))
        return update(billItemStatus);
        return create(billItemStatus);
    }

  


    private const String SqlUpdate = @"Update [BillItemStatus] Set 
    
       Where 
      Status = @Status";

    public BillItemStatus update(BillItemStatus billItemStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@Status", billItemStatus.Status);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billItemStatus;
    }

  
    }
    
  
    
    public partial class BillStatus
    {
    
      protected String _status;
    

    public BillStatus(){}

    public BillStatus(
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
  

    public partial class BillStatusDataMapper:TDataMapper<BillStatus,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "BillStatus";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [BillStatus] (
    Status) Values (
    
      @Status);
    ";

    public override BillStatus create( BillStatus billStatus )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@Status", billStatus.Status);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return billStatus;
    }

  

    private const String SqlSelectAll = @"Select
    Status 
    From [BillStatus] ";
    
    public List<BillStatus> findAll(Object args)
    {
      List<BillStatus> rv = new List<BillStatus>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [BillStatus]
    
       Where 
      Status = @Status
    ";

    public BillStatus findByPrimaryKey(
    String status
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@Status", status);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("BillStatus not found, search by primary key");
    }

    }


    public bool exists(BillStatus billStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@Status", billStatus.Status);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override BillStatus doLoad(IDataReader dataReader)
    {
    BillStatus billStatus = new BillStatus();

    billStatus.Status = dataReader.GetString(0);
        

    return billStatus;
    }


    protected override BillStatus doLoad(Hashtable hashtable)
    {
      BillStatus billStatus = new BillStatus();

      
        if(hashtable.ContainsKey("Status"))
            billStatus.Status = ( String)hashtable["Status"];
      

      return billStatus;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [BillStatus]
    
      Where
      Status = @Status";
    public BillStatus remove(BillStatus billStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@Status", billStatus.Status);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billStatus;
    }

    #endregion
  

    public BillStatus save( BillStatus billStatus )
    {
      if(exists(billStatus))
        return update(billStatus);
        return create(billStatus);
    }

  


    private const String SqlUpdate = @"Update [BillStatus] Set 
    
       Where 
      Status = @Status";

    public BillStatus update(BillStatus billStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@Status", billStatus.Status);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return billStatus;
    }

  
    }
    
  
    
    public partial class Client
    {
    
      protected int _clientId;
    
      protected String _clientName;
    
      protected bool _active;
    
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
        clientName,bool 
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
    
      public bool Active
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
  

    public partial class ClientDataMapper:TDataMapper<Client,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "Client";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Client] (
    ClientName,Active,ClientGuid) Values (
    
      @ClientName,
      @Active,
      @ClientGuid);
    
      select scope_identity();
    ";

    public override Client create( Client client )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@ClientName", client.ClientName);
              
                  sqlCommand.Parameters.Add("@Active", client.Active);
              
                  sqlCommand.Parameters.Add("@ClientGuid", client.ClientGuid);
              client.ClientId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
          }
      }

      return client;
    }

  

    private const String SqlSelectAll = @"Select
    ClientId,ClientName,Active,ClientGuid 
    From [Client] ";
    
    public List<Client> findAll(Object args)
    {
      List<Client> rv = new List<Client>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [Client]
    
       Where 
      ClientId = @ClientId
    ";

    public Client findByPrimaryKey(
    int clientId
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@ClientId", clientId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("Client not found, search by primary key");
    }

    }


    public bool exists(Client client)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@ClientId", client.ClientId);
          

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
        client.Active = dataReader.GetBoolean(2);
        client.ClientGuid = dataReader.GetString(3);
        

    return client;
    }


    protected override Client doLoad(Hashtable hashtable)
    {
      Client client = new Client();

      
        if(hashtable.ContainsKey("ClientId"))
            client.ClientId = ( int)hashtable["ClientId"];
      
        if(hashtable.ContainsKey("ClientName"))
            client.ClientName = ( String)hashtable["ClientName"];
      
        if(hashtable.ContainsKey("Active"))
            client.Active = ( bool)hashtable["Active"];
      
        if(hashtable.ContainsKey("ClientGuid"))
            client.ClientGuid = ( String)hashtable["ClientGuid"];
      

      return client;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Client]
    
      Where
      ClientId = @ClientId";
    public Client remove(Client client)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@ClientId", client.ClientId);
        
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

  


    private const String SqlUpdate = @"Update [Client] Set 
    ClientName = @ClientName,Active = @Active,ClientGuid = @ClientGuid
       Where 
      ClientId = @ClientId";

    public Client update(Client client)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@ClientId", client.ClientId);
              
                  sqlCommand.Parameters.AddWithValue("@ClientName", client.ClientName);
              
                  sqlCommand.Parameters.AddWithValue("@Active", client.Active);
              
                  sqlCommand.Parameters.AddWithValue("@ClientGuid", client.ClientGuid);
              

    
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
  

    public partial class RateDataMapper:TDataMapper<Rate,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "Rate";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Rate] (
    AssetId,ClientId,DateRate,MilageRate,RateGuid) Values (
    
      @AssetId,
      @ClientId,
      @DateRate,
      @MilageRate,
      @RateGuid);
    ";

    public override Rate create( Rate rate )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@AssetId", rate.AssetId);
              
                  sqlCommand.Parameters.Add("@ClientId", rate.ClientId);
              
                  sqlCommand.Parameters.Add("@DateRate", rate.DateRate);
              
                  sqlCommand.Parameters.Add("@MilageRate", rate.MilageRate);
              
                  sqlCommand.Parameters.Add("@RateGuid", rate.RateGuid);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return rate;
    }

  

    private const String SqlSelectAll = @"Select
    AssetId,ClientId,DateRate,MilageRate,RateGuid 
    From [Rate] ";
    
    public List<Rate> findAll(Object args)
    {
      List<Rate> rv = new List<Rate>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [Rate]
    
       Where 
      AssetId = @AssetId and ClientId = @ClientId
    ";

    public Rate findByPrimaryKey(
    int assetId,int clientId
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@AssetId", assetId);
        
          sqlCommand.Parameters.AddWithValue("@ClientId", clientId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("Rate not found, search by primary key");
    }

    }


    public bool exists(Rate rate)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@AssetId", rate.AssetId);
          
            sqlCommand.Parameters.AddWithValue("@ClientId", rate.ClientId);
          

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
            rate.AssetId = ( int)hashtable["AssetId"];
      
        if(hashtable.ContainsKey("ClientId"))
            rate.ClientId = ( int)hashtable["ClientId"];
      
        if(hashtable.ContainsKey("DateRate"))
            rate.DateRate = ( decimal)hashtable["DateRate"];
      
        if(hashtable.ContainsKey("MilageRate"))
            rate.MilageRate = ( decimal)hashtable["MilageRate"];
      
        if(hashtable.ContainsKey("RateGuid"))
            rate.RateGuid = ( String)hashtable["RateGuid"];
      

      return rate;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Rate]
    
      Where
      AssetId = @AssetId and ClientId = @ClientId";
    public Rate remove(Rate rate)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@AssetId", rate.AssetId);
        
          sqlCommand.Parameters.AddWithValue("@ClientId", rate.ClientId);
        
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

  


    private const String SqlUpdate = @"Update [Rate] Set 
    DateRate = @DateRate,MilageRate = @MilageRate,RateGuid = @RateGuid
       Where 
      AssetId = @AssetId and ClientId = @ClientId";

    public Rate update(Rate rate)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@AssetId", rate.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@ClientId", rate.ClientId);
              
                  sqlCommand.Parameters.AddWithValue("@DateRate", rate.DateRate);
              
                  sqlCommand.Parameters.AddWithValue("@MilageRate", rate.MilageRate);
              
                  sqlCommand.Parameters.AddWithValue("@RateGuid", rate.RateGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return rate;
    }

  
    }
    
  
    
    public partial class SubAfe
    {
    
      protected String _subAFE;
    
      protected String _aFE;
    
      protected String _subAFEStatus;
    
      protected String _subAfeGuid;
    

    public SubAfe(){}

    public SubAfe(
    String 
            subAFE
    )
    {
    
      _subAFE = subAFE;
    
    }

    

      public SubAfe(
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
  

    public partial class SubAfeDataMapper:TDataMapper<SubAfe,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "SubAfe";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [SubAfe] (
    SubAFE,AFE,SubAFEStatus,SubAfeGuid) Values (
    
      @SubAFE,
      @AFE,
      @SubAFEStatus,
      @SubAfeGuid);
    ";

    public override SubAfe create( SubAfe subAfe )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@SubAFE", subAfe.SubAFE);
              
                  sqlCommand.Parameters.Add("@AFE", subAfe.AFE);
              
                  sqlCommand.Parameters.Add("@SubAFEStatus", subAfe.SubAFEStatus);
              
                  sqlCommand.Parameters.Add("@SubAfeGuid", subAfe.SubAfeGuid);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return subAfe;
    }

  

    private const String SqlSelectAll = @"Select
    SubAFE,AFE,SubAFEStatus,SubAfeGuid 
    From [SubAfe] ";
    
    public List<SubAfe> findAll(Object args)
    {
      List<SubAfe> rv = new List<SubAfe>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [SubAfe]
    
       Where 
      SubAFE = @SubAFE
    ";

    public SubAfe findByPrimaryKey(
    String subAFE
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@SubAFE", subAFE);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("SubAfe not found, search by primary key");
    }

    }


    public bool exists(SubAfe subAfe)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@SubAFE", subAfe.SubAFE);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override SubAfe doLoad(IDataReader dataReader)
    {
    SubAfe subAfe = new SubAfe();

    subAfe.SubAFE = dataReader.GetString(0);
        subAfe.AFE = dataReader.GetString(1);
        subAfe.SubAFEStatus = dataReader.GetString(2);
        subAfe.SubAfeGuid = dataReader.GetString(3);
        

    return subAfe;
    }


    protected override SubAfe doLoad(Hashtable hashtable)
    {
      SubAfe subAfe = new SubAfe();

      
        if(hashtable.ContainsKey("SubAFE"))
            subAfe.SubAFE = ( String)hashtable["SubAFE"];
      
        if(hashtable.ContainsKey("AFE"))
            subAfe.AFE = ( String)hashtable["AFE"];
      
        if(hashtable.ContainsKey("SubAFEStatus"))
            subAfe.SubAFEStatus = ( String)hashtable["SubAFEStatus"];
      
        if(hashtable.ContainsKey("SubAfeGuid"))
            subAfe.SubAfeGuid = ( String)hashtable["SubAfeGuid"];
      

      return subAfe;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [SubAfe]
    
      Where
      SubAFE = @SubAFE";
    public SubAfe remove(SubAfe subAfe)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@SubAFE", subAfe.SubAFE);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subAfe;
    }

    #endregion
  

    public SubAfe save( SubAfe subAfe )
    {
      if(exists(subAfe))
        return update(subAfe);
        return create(subAfe);
    }

  


    private const String SqlUpdate = @"Update [SubAfe] Set 
    AFE = @AFE,SubAFEStatus = @SubAFEStatus,SubAfeGuid = @SubAfeGuid
       Where 
      SubAFE = @SubAFE";

    public SubAfe update(SubAfe subAfe)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@SubAFE", subAfe.SubAFE);
              
                  sqlCommand.Parameters.AddWithValue("@AFE", subAfe.AFE);
              
                  sqlCommand.Parameters.AddWithValue("@SubAFEStatus", subAfe.SubAFEStatus);
              
                  sqlCommand.Parameters.AddWithValue("@SubAfeGuid", subAfe.SubAfeGuid);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subAfe;
    }

  
    }
    
  
    
    public partial class SubAfeStatus
    {
    
      protected String _subAFEStatus;
    

    public SubAfeStatus(){}

    public SubAfeStatus(
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
  

    public partial class SubAfeStatusDataMapper:TDataMapper<SubAfeStatus,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "SubAfeStatus";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [SubAfeStatus] (
    SubAFEStatus) Values (
    
      @SubAFEStatus);
    ";

    public override SubAfeStatus create( SubAfeStatus subAfeStatus )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@SubAFEStatus", subAfeStatus.SubAFEStatus);
              
              sqlCommand.ExecuteNonQuery();
            
          }
      }

      return subAfeStatus;
    }

  

    private const String SqlSelectAll = @"Select
    SubAFEStatus 
    From [SubAfeStatus] ";
    
    public List<SubAfeStatus> findAll(Object args)
    {
      List<SubAfeStatus> rv = new List<SubAfeStatus>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [SubAfeStatus]
    
       Where 
      SubAFEStatus = @SubAFEStatus
    ";

    public SubAfeStatus findByPrimaryKey(
    String subAFEStatus
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@SubAFEStatus", subAFEStatus);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("SubAfeStatus not found, search by primary key");
    }

    }


    public bool exists(SubAfeStatus subAfeStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@SubAFEStatus", subAfeStatus.SubAFEStatus);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override SubAfeStatus doLoad(IDataReader dataReader)
    {
    SubAfeStatus subAfeStatus = new SubAfeStatus();

    subAfeStatus.SubAFEStatus = dataReader.GetString(0);
        

    return subAfeStatus;
    }


    protected override SubAfeStatus doLoad(Hashtable hashtable)
    {
      SubAfeStatus subAfeStatus = new SubAfeStatus();

      
        if(hashtable.ContainsKey("SubAFEStatus"))
            subAfeStatus.SubAFEStatus = ( String)hashtable["SubAFEStatus"];
      

      return subAfeStatus;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [SubAfeStatus]
    
      Where
      SubAFEStatus = @SubAFEStatus";
    public SubAfeStatus remove(SubAfeStatus subAfeStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@SubAFEStatus", subAfeStatus.SubAFEStatus);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subAfeStatus;
    }

    #endregion
  

    public SubAfeStatus save( SubAfeStatus subAfeStatus )
    {
      if(exists(subAfeStatus))
        return update(subAfeStatus);
        return create(subAfeStatus);
    }

  


    private const String SqlUpdate = @"Update [SubAfeStatus] Set 
    
       Where 
      SubAFEStatus = @SubAFEStatus";

    public SubAfeStatus update(SubAfeStatus subAfeStatus)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@SubAFEStatus", subAfeStatus.SubAFEStatus);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return subAfeStatus;
    }

  
    }
    
  
    
    public partial class SyncLog
    {
    
      protected int _syncLogId;
    
      protected int _assetId;
    
      protected String _deviceId;
    

    public SyncLog(){}

    public SyncLog(
    int 
            syncLogId
    )
    {
    
      _syncLogId = syncLogId;
    
    }

    

      public SyncLog(
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
  

    public partial class SyncLogDataMapper:TDataMapper<SyncLog,SqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "SyncLog";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [SyncLog] (
    AssetId,DeviceId) Values (
    
      @AssetId,
      @DeviceId);
    
      select scope_identity();
    ";

    public override SyncLog create( SyncLog syncLog )
    {
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand( SqlCreate , database.Connection ))
      {
      
                  sqlCommand.Parameters.Add("@AssetId", syncLog.AssetId);
              
                  sqlCommand.Parameters.Add("@DeviceId", syncLog.DeviceId);
              syncLog.SyncLogId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
          }
      }

      return syncLog;
    }

  

    private const String SqlSelectAll = @"Select
    SyncLogId,AssetId,DeviceId 
    From [SyncLog] ";
    
    public List<SyncLog> findAll(Object args)
    {
      List<SyncLog> rv = new List<SyncLog>();

      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectAll,database.Connection))
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
     From [SyncLog]
    
       Where 
      SyncLogId = @SyncLogId
    ";

    public SyncLog findByPrimaryKey(
    int syncLogId
    )
    {
    
    using(Database database = new Database())
    {
      using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.AddWithValue("@SyncLogId", syncLogId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("SyncLog not found, search by primary key");
    }

    }


    public bool exists(SyncLog syncLog)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.AddWithValue("@SyncLogId", syncLog.SyncLogId);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override SyncLog doLoad(IDataReader dataReader)
    {
    SyncLog syncLog = new SyncLog();

    syncLog.SyncLogId = dataReader.GetInt32(0);
        syncLog.AssetId = dataReader.GetInt32(1);
        syncLog.DeviceId = dataReader.GetString(2);
        

    return syncLog;
    }


    protected override SyncLog doLoad(Hashtable hashtable)
    {
      SyncLog syncLog = new SyncLog();

      
        if(hashtable.ContainsKey("SyncLogId"))
            syncLog.SyncLogId = ( int)hashtable["SyncLogId"];
      
        if(hashtable.ContainsKey("AssetId"))
            syncLog.AssetId = ( int)hashtable["AssetId"];
      
        if(hashtable.ContainsKey("DeviceId"))
            syncLog.DeviceId = ( String)hashtable["DeviceId"];
      

      return syncLog;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [SyncLog]
    
      Where
      SyncLogId = @SyncLogId";
    public SyncLog remove(SyncLog syncLog)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.AddWithValue("@SyncLogId", syncLog.SyncLogId);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return syncLog;
    }

    #endregion
  

    public SyncLog save( SyncLog syncLog )
    {
      if(exists(syncLog))
        return update(syncLog);
        return create(syncLog);
    }

  


    private const String SqlUpdate = @"Update [SyncLog] Set 
    AssetId = @AssetId,DeviceId = @DeviceId
       Where 
      SyncLogId = @SyncLogId";

    public SyncLog update(SyncLog syncLog)
    {
      using(Database database = new Database())
      {
        using(SqlCommand sqlCommand = new SqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.AddWithValue("@SyncLogId", syncLog.SyncLogId);
              
                  sqlCommand.Parameters.AddWithValue("@AssetId", syncLog.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@DeviceId", syncLog.DeviceId);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return syncLog;
    }

  
    }
    
  
      }
    