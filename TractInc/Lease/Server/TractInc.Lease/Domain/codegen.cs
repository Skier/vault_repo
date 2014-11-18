
      namespace TractInc.Lease.Domain
      {
      using System;
      
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using Weborb.Data.Management;
  
    
      using System.Data.SqlClient;
    
    public class TractIncDb:TDatabase<SqlConnection,SqlTransaction,SqlCommand>
    {
      public TractIncDb()
      {
        InitConnectionString("TractInc");
      }

    

        public County create(County county)
        {
          CountyDataMapper dataMapper = new CountyDataMapper(this);
          
          return dataMapper.create(county);
        }

        public County update(County county)
        {
          CountyDataMapper dataMapper = new CountyDataMapper(this);

          return dataMapper.update(county);
        }

        public County remove(County county)
        {
          CountyDataMapper dataMapper = new CountyDataMapper(this);

          return dataMapper.remove(county);
        }
      

        public Lease create(Lease lease)
        {
          LeaseDataMapper dataMapper = new LeaseDataMapper(this);
          
          return dataMapper.create(lease);
        }

        public Lease update(Lease lease)
        {
          LeaseDataMapper dataMapper = new LeaseDataMapper(this);

          return dataMapper.update(lease);
        }

        public Lease remove(Lease lease)
        {
          LeaseDataMapper dataMapper = new LeaseDataMapper(this);

          return dataMapper.remove(lease);
        }
      

        public User create(User user)
        {
          UserDataMapper dataMapper = new UserDataMapper(this);
          
          return dataMapper.create(user);
        }

        public User update(User user)
        {
          UserDataMapper dataMapper = new UserDataMapper(this);

          return dataMapper.update(user);
        }

        public User remove(User user)
        {
          UserDataMapper dataMapper = new UserDataMapper(this);

          return dataMapper.remove(user);
        }
      

        public LeaseEditHistory create(LeaseEditHistory leaseEditHistory)
        {
          LeaseEditHistoryDataMapper dataMapper = new LeaseEditHistoryDataMapper(this);
          
          return dataMapper.create(leaseEditHistory);
        }

        public LeaseEditHistory update(LeaseEditHistory leaseEditHistory)
        {
          LeaseEditHistoryDataMapper dataMapper = new LeaseEditHistoryDataMapper(this);

          return dataMapper.update(leaseEditHistory);
        }

        public LeaseEditHistory remove(LeaseEditHistory leaseEditHistory)
        {
          LeaseEditHistoryDataMapper dataMapper = new LeaseEditHistoryDataMapper(this);

          return dataMapper.remove(leaseEditHistory);
        }
      

        public State create(State state)
        {
          StateDataMapper dataMapper = new StateDataMapper(this);
          
          return dataMapper.create(state);
        }

        public State update(State state)
        {
          StateDataMapper dataMapper = new StateDataMapper(this);

          return dataMapper.update(state);
        }

        public State remove(State state)
        {
          StateDataMapper dataMapper = new StateDataMapper(this);

          return dataMapper.remove(state);
        }
      

        public TermUnit create(TermUnit termUnit)
        {
          TermUnitDataMapper dataMapper = new TermUnitDataMapper(this);
          
          return dataMapper.create(termUnit);
        }

        public TermUnit update(TermUnit termUnit)
        {
          TermUnitDataMapper dataMapper = new TermUnitDataMapper(this);

          return dataMapper.update(termUnit);
        }

        public TermUnit remove(TermUnit termUnit)
        {
          TermUnitDataMapper dataMapper = new TermUnitDataMapper(this);

          return dataMapper.remove(termUnit);
        }
      

        public UserRole create(UserRole userRole)
        {
          UserRoleDataMapper dataMapper = new UserRoleDataMapper(this);
          
          return dataMapper.create(userRole);
        }

        public UserRole update(UserRole userRole)
        {
          UserRoleDataMapper dataMapper = new UserRoleDataMapper(this);

          return dataMapper.update(userRole);
        }

        public UserRole remove(UserRole userRole)
        {
          UserRoleDataMapper dataMapper = new UserRoleDataMapper(this);

          return dataMapper.remove(userRole);
        }
      

        public Module create(Module module)
        {
          ModuleDataMapper dataMapper = new ModuleDataMapper(this);
          
          return dataMapper.create(module);
        }

        public Module update(Module module)
        {
          ModuleDataMapper dataMapper = new ModuleDataMapper(this);

          return dataMapper.update(module);
        }

        public Module remove(Module module)
        {
          ModuleDataMapper dataMapper = new ModuleDataMapper(this);

          return dataMapper.remove(module);
        }
      

        public Permission create(Permission permission)
        {
          PermissionDataMapper dataMapper = new PermissionDataMapper(this);
          
          return dataMapper.create(permission);
        }

        public Permission update(Permission permission)
        {
          PermissionDataMapper dataMapper = new PermissionDataMapper(this);

          return dataMapper.update(permission);
        }

        public Permission remove(Permission permission)
        {
          PermissionDataMapper dataMapper = new PermissionDataMapper(this);

          return dataMapper.remove(permission);
        }
      

        public PermissionAssignment create(PermissionAssignment permissionAssignment)
        {
          PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(this);
          
          return dataMapper.create(permissionAssignment);
        }

        public PermissionAssignment update(PermissionAssignment permissionAssignment)
        {
          PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(this);

          return dataMapper.update(permissionAssignment);
        }

        public PermissionAssignment remove(PermissionAssignment permissionAssignment)
        {
          PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(this);

          return dataMapper.remove(permissionAssignment);
        }
      

        public Role create(Role role)
        {
          RoleDataMapper dataMapper = new RoleDataMapper(this);
          
          return dataMapper.create(role);
        }

        public Role update(Role role)
        {
          RoleDataMapper dataMapper = new RoleDataMapper(this);

          return dataMapper.update(role);
        }

        public Role remove(Role role)
        {
          RoleDataMapper dataMapper = new RoleDataMapper(this);

          return dataMapper.remove(role);
        }
      
    }
  
    
    public partial class County: DomainObject
    {
    
      protected int _countyId;
    
      protected String _name;
    
      protected String _stateName;
    
      protected String _stateFips;
    
      protected String _countyFips;
    
      protected String _fips;
    

      // parent tables
      protected State _parentState
        = new State()
      ;
    

    public County(){}

    public County(
    int 
            countyId,String 
            name,int 
            stateId,String 
            stateName,String 
            stateFips,String 
            countyFips,String 
            fips
    )
    {
    CountyId = countyId;
    Name = name;
    StateId = stateId;
    StateName = stateName;
    StateFips = stateFips;
    CountyFips = countyFips;
    Fips = fips;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.County."
    
      + CountyId.ToString()
    ;
    
    return uri;
    }

    

      public int CountyId
      {
        
            get { return _countyId;}
            set 
            { 
                _countyId = value;
            }
          
      }
    

      public String Name
      {
        
            get { return _name;}
            set 
            { 
                _name = value;
            }
          
      }
    

      public int StateId
      {
        
            get
            {
            
                  if(_parentState != null)
                    return _parentState.StateId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentState == null)
                        _parentState = new State();

                      _parentState.StateId = value;
                    
            }
          
      }
    

      public String StateName
      {
        
            get { return _stateName;}
            set 
            { 
                _stateName = value;
            }
          
      }
    

      public String StateFips
      {
        
            get { return _stateFips;}
            set 
            { 
                _stateFips = value;
            }
          
      }
    

      public String CountyFips
      {
        
            get { return _countyFips;}
            set 
            { 
                _countyFips = value;
            }
          
      }
    

      public String Fips
      {
        
            get { return _fips;}
            set 
            { 
                _fips = value;
            }
          
      }
    

      public State ParentState
      {
      get { return _parentState;}
      set { _parentState = value; }
      }
    


    public override DomainObject extractSingleObject()
    {
      County county = new County();
      
      county.CountyId = this.CountyId;
      county.Name = this.Name;
      county.StateId = this.StateId;
      county.StateName = this.StateName;
      county.StateFips = this.StateFips;
      county.CountyFips = this.CountyFips;
      county.Fips = this.Fips;
      county.ActiveRecordId = this.ActiveRecordId; 
      return county;
    }

    
    }
  

    public partial class CountyDataMapper:TDataMapper<County,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public CountyDataMapper(){}
      public CountyDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "County";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [County] (
    Name,StateId,StateName,StateFips,CountyFips,Fips) Values (
    
      @Name,
      @StateId,
      @StateName,
      @StateFips,
      @CountyFips,
      @Fips);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override County create( County county )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                if(county.Name != null)
                  sqlCommand.Parameters.AddWithValue("@Name", county.Name);
                else
                  sqlCommand.Parameters.AddWithValue("@Name", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@StateId", county.StateId);
              
                if(county.StateName != null)
                  sqlCommand.Parameters.AddWithValue("@StateName", county.StateName);
                else
                  sqlCommand.Parameters.AddWithValue("@StateName", DBNull.Value);
              
                if(county.StateFips != null)
                  sqlCommand.Parameters.AddWithValue("@StateFips", county.StateFips);
                else
                  sqlCommand.Parameters.AddWithValue("@StateFips", DBNull.Value);
              
                if(county.CountyFips != null)
                  sqlCommand.Parameters.AddWithValue("@CountyFips", county.CountyFips);
                else
                  sqlCommand.Parameters.AddWithValue("@CountyFips", DBNull.Value);
              
                if(county.Fips != null)
                  sqlCommand.Parameters.AddWithValue("@Fips", county.Fips);
                else
                  sqlCommand.Parameters.AddWithValue("@Fips", DBNull.Value);
              county.CountyId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(county,DataMapperOperation.create);

      return registerRecord(county);
    }

  

    private const String SqlSelectAll = @"Select
    CountyId,Name,StateId,StateName,StateFips,CountyFips,Fips 
    From [County] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    CountyId,Name,StateId,StateName,StateFips,CountyFips,Fips
     From [County]
    
       Where 
      CountyId = @CountyId
    ";

    public County findByPrimaryKey(
    int countyId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@CountyId", countyId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("County not found, search by primary key");
 

    }


    public bool exists(County county)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@CountyId", county.CountyId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [County].[CountyId] = @CheckInCountyId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      County _County = (County)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInCountyId", _County.CountyId);
      

      return sqlCommand;
    }

  

    protected override County doLoad(IDataReader dataReader)
    {
    County county = new County();

    county.CountyId = dataReader.GetInt32(0);
            
          if(!dataReader.IsDBNull(1))        
          county.Name = dataReader.GetString(1);
            county.StateId = dataReader.GetInt32(2);
            
          if(!dataReader.IsDBNull(3))        
          county.StateName = dataReader.GetString(3);
            
          if(!dataReader.IsDBNull(4))        
          county.StateFips = dataReader.GetString(4);
            
          if(!dataReader.IsDBNull(5))        
          county.CountyFips = dataReader.GetString(5);
            
          if(!dataReader.IsDBNull(6))        
          county.Fips = dataReader.GetString(6);
            

    
    
    return registerRecord(county);
    }


    protected override County doLoad(Hashtable hashtable)
    {
      County county = new County();

      
        
        if(hashtable.ContainsKey("CountyId"))
            county.CountyId = ( int)hashtable["CountyId"];
      
        
        if(hashtable.ContainsKey("Name"))
            county.Name = ( String)hashtable["Name"];
      
        
        if(hashtable.ContainsKey("StateId"))
            county.StateId = ( int)hashtable["StateId"];
      
        
        if(hashtable.ContainsKey("StateName"))
            county.StateName = ( String)hashtable["StateName"];
      
        
        if(hashtable.ContainsKey("StateFips"))
            county.StateFips = ( String)hashtable["StateFips"];
      
        
        if(hashtable.ContainsKey("CountyFips"))
            county.CountyFips = ( String)hashtable["CountyFips"];
      
        
        if(hashtable.ContainsKey("Fips"))
            county.Fips = ( String)hashtable["Fips"];
      

      return county;
    }


    protected override List<County> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<County> resultList = new List<County>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              County item = new County();
              
              
                    item.CountyId = ( int)dataRow["CountyId"] ;
                  
                    if(!dataRow.IsNull("Name") &&  !(dataRow["Name"] is DBNull) )
                      item.Name = ( String)dataRow["Name"] ;
                  
                    item.StateId = ( int)dataRow["StateId"] ;
                  
                    if(!dataRow.IsNull("StateName") &&  !(dataRow["StateName"] is DBNull) )
                      item.StateName = ( String)dataRow["StateName"] ;
                  
                    if(!dataRow.IsNull("StateFips") &&  !(dataRow["StateFips"] is DBNull) )
                      item.StateFips = ( String)dataRow["StateFips"] ;
                  
                    if(!dataRow.IsNull("CountyFips") &&  !(dataRow["CountyFips"] is DBNull) )
                      item.CountyFips = ( String)dataRow["CountyFips"] ;
                  
                    if(!dataRow.IsNull("Fips") &&  !(dataRow["Fips"] is DBNull) )
                      item.Fips = ( String)dataRow["Fips"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [County]
    
      Where
      CountyId = @CountyId";
    [Synchronized]
    public County remove(County county)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@CountyId", county.CountyId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(county,DataMapperOperation.delete);

      return registerRecord(county);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public County save( County county )
    {
      if(exists(county))
        return update(county);
        return create(county);
    }

  

    const String SqlUpdate = @"Update [County] Set 
    Name = @Name,StateId = @StateId,StateName = @StateName,StateFips = @StateFips,CountyFips = @CountyFips,Fips = @Fips
       Where 
      CountyId = @CountyId";
    
    
    [Synchronized]
    public County update(County county)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@CountyId", county.CountyId);
                  
                    
                    if(county.Name != null)
                      sqlCommand.Parameters.AddWithValue("@Name", county.Name);
                    else
                      sqlCommand.Parameters.AddWithValue("@Name", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@StateId", county.StateId);
                  
                    
                    if(county.StateName != null)
                      sqlCommand.Parameters.AddWithValue("@StateName", county.StateName);
                    else
                      sqlCommand.Parameters.AddWithValue("@StateName", DBNull.Value);
                  
                    
                    if(county.StateFips != null)
                      sqlCommand.Parameters.AddWithValue("@StateFips", county.StateFips);
                    else
                      sqlCommand.Parameters.AddWithValue("@StateFips", DBNull.Value);
                  
                    
                    if(county.CountyFips != null)
                      sqlCommand.Parameters.AddWithValue("@CountyFips", county.CountyFips);
                    else
                      sqlCommand.Parameters.AddWithValue("@CountyFips", DBNull.Value);
                  
                    
                    if(county.Fips != null)
                      sqlCommand.Parameters.AddWithValue("@Fips", county.Fips);
                    else
                      sqlCommand.Parameters.AddWithValue("@Fips", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(county,DataMapperOperation.update);

      return registerRecord(county);
    }

  
    }
    
  
    
    public partial class Lease: DomainObject
    {
    
      protected int _leaseId;
    
      protected String _lCN;
    
      protected String _documentNumber;
    
      protected String _volume;
    
      protected String _pAGE;
    
      protected String _leaseeName;
    
      protected String _assigneeName;
    
      protected String _leassorName;
    
      protected String _assignorName;
    
      protected String _stateFips;
    
      protected String _countyFips;
    
      protected decimal? _unitDepth;
    
      protected decimal? _fromDepth;
    
      protected decimal? _fromFrom;
    
      protected decimal? _toDepth;
    
      protected decimal? _toFrom;
    
      protected String _workInt;
    
      protected String _orrInt;
    
      protected decimal? _netAcres;
    
      protected decimal? _grossAcres;
    
      protected String _nriAssign;
    
      protected DateTime? _rcdDate;
    
      protected decimal? _term;
    
      protected bool _hBR;
    
      protected bool _encumbrances;
    
      protected DateTime? _effDate;
    
      protected bool _pughClause;
    
      protected bool _depthLimitation;
    
      protected bool _shutInClau;
    
      protected bool _poolingClau;
    
      protected decimal? _minimumPmt;
    
      protected int? _author;
    
      protected String _status;
    

      // parent tables
      protected TermUnit _parentTermUnit;
    

    public Lease(){}

    public Lease(
    int 
            leaseId,String 
            lCN,String 
            documentNumber,String 
            volume,String 
            pAGE,String 
            leaseeName,String 
            assigneeName,String 
            leassorName,String 
            assignorName,String 
            stateFips,String 
            countyFips,decimal 
            unitDepth,decimal 
            fromDepth,decimal 
            fromFrom,decimal 
            toDepth,decimal 
            toFrom,String 
            workInt,String 
            orrInt,decimal 
            netAcres,decimal 
            grossAcres,String 
            nriAssign,DateTime 
            rcdDate,decimal 
            term,int 
            termUnitId,bool 
            hBR,bool 
            encumbrances,DateTime 
            effDate,bool 
            pughClause,bool 
            depthLimitation,bool 
            shutInClau,bool 
            poolingClau,decimal 
            minimumPmt,int 
            author,String 
            status
    )
    {
    LeaseId = leaseId;
    LCN = lCN;
    DocumentNumber = documentNumber;
    Volume = volume;
    PAGE = pAGE;
    LeaseeName = leaseeName;
    AssigneeName = assigneeName;
    LeassorName = leassorName;
    AssignorName = assignorName;
    StateFips = stateFips;
    CountyFips = countyFips;
    UnitDepth = unitDepth;
    FromDepth = fromDepth;
    FromFrom = fromFrom;
    ToDepth = toDepth;
    ToFrom = toFrom;
    WorkInt = workInt;
    OrrInt = orrInt;
    NetAcres = netAcres;
    GrossAcres = grossAcres;
    NriAssign = nriAssign;
    RcdDate = rcdDate;
    Term = term;
    TermUnitId = termUnitId;
    HBR = hBR;
    Encumbrances = encumbrances;
    EffDate = effDate;
    PughClause = pughClause;
    DepthLimitation = depthLimitation;
    ShutInClau = shutInClau;
    PoolingClau = poolingClau;
    MinimumPmt = minimumPmt;
    Author = author;
    Status = status;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.Lease."
    
      + LeaseId.ToString()
    ;
    
    return uri;
    }

    

      public int LeaseId
      {
        
            get { return _leaseId;}
            set 
            { 
                _leaseId = value;
            }
          
      }
    

      public String LCN
      {
        
            get { return _lCN;}
            set 
            { 
                _lCN = value;
            }
          
      }
    

      public String DocumentNumber
      {
        
            get { return _documentNumber;}
            set 
            { 
                _documentNumber = value;
            }
          
      }
    

      public String Volume
      {
        
            get { return _volume;}
            set 
            { 
                _volume = value;
            }
          
      }
    

      public String PAGE
      {
        
            get { return _pAGE;}
            set 
            { 
                _pAGE = value;
            }
          
      }
    

      public String LeaseeName
      {
        
            get { return _leaseeName;}
            set 
            { 
                _leaseeName = value;
            }
          
      }
    

      public String AssigneeName
      {
        
            get { return _assigneeName;}
            set 
            { 
                _assigneeName = value;
            }
          
      }
    

      public String LeassorName
      {
        
            get { return _leassorName;}
            set 
            { 
                _leassorName = value;
            }
          
      }
    

      public String AssignorName
      {
        
            get { return _assignorName;}
            set 
            { 
                _assignorName = value;
            }
          
      }
    

      public String StateFips
      {
        
            get { return _stateFips;}
            set 
            { 
                _stateFips = value;
            }
          
      }
    

      public String CountyFips
      {
        
            get { return _countyFips;}
            set 
            { 
                _countyFips = value;
            }
          
      }
    

      public decimal? UnitDepth
      {
        
            get { return _unitDepth;}
            set 
            { 
                _unitDepth = value;
            }
          
      }
    

      public decimal? FromDepth
      {
        
            get { return _fromDepth;}
            set 
            { 
                _fromDepth = value;
            }
          
      }
    

      public decimal? FromFrom
      {
        
            get { return _fromFrom;}
            set 
            { 
                _fromFrom = value;
            }
          
      }
    

      public decimal? ToDepth
      {
        
            get { return _toDepth;}
            set 
            { 
                _toDepth = value;
            }
          
      }
    

      public decimal? ToFrom
      {
        
            get { return _toFrom;}
            set 
            { 
                _toFrom = value;
            }
          
      }
    

      public String WorkInt
      {
        
            get { return _workInt;}
            set 
            { 
                _workInt = value;
            }
          
      }
    

      public String OrrInt
      {
        
            get { return _orrInt;}
            set 
            { 
                _orrInt = value;
            }
          
      }
    

      public decimal? NetAcres
      {
        
            get { return _netAcres;}
            set 
            { 
                _netAcres = value;
            }
          
      }
    

      public decimal? GrossAcres
      {
        
            get { return _grossAcres;}
            set 
            { 
                _grossAcres = value;
            }
          
      }
    

      public String NriAssign
      {
        
            get { return _nriAssign;}
            set 
            { 
                _nriAssign = value;
            }
          
      }
    

      public DateTime? RcdDate
      {
        
            get { return _rcdDate;}
            set 
            { 
                _rcdDate = value;
            }
          
      }
    

      public decimal? Term
      {
        
            get { return _term;}
            set 
            { 
                _term = value;
            }
          
      }
    

      public int? TermUnitId
      {
        
            get
            {
            
                  if(_parentTermUnit != null)
                    return _parentTermUnit.TermUnitId;

                return null;
            }
            set
            {
            
                      if(value == null)
                        _parentTermUnit = null;
                      else
                      {
                      if(_parentTermUnit == null)
                        _parentTermUnit = new TermUnit();

                        _parentTermUnit.TermUnitId = value.Value;
                      }
                    
            }
          
      }
    

      public bool HBR
      {
        
            get { return _hBR;}
            set 
            { 
                _hBR = value;
            }
          
      }
    

      public bool Encumbrances
      {
        
            get { return _encumbrances;}
            set 
            { 
                _encumbrances = value;
            }
          
      }
    

      public DateTime? EffDate
      {
        
            get { return _effDate;}
            set 
            { 
                _effDate = value;
            }
          
      }
    

      public bool PughClause
      {
        
            get { return _pughClause;}
            set 
            { 
                _pughClause = value;
            }
          
      }
    

      public bool DepthLimitation
      {
        
            get { return _depthLimitation;}
            set 
            { 
                _depthLimitation = value;
            }
          
      }
    

      public bool ShutInClau
      {
        
            get { return _shutInClau;}
            set 
            { 
                _shutInClau = value;
            }
          
      }
    

      public bool PoolingClau
      {
        
            get { return _poolingClau;}
            set 
            { 
                _poolingClau = value;
            }
          
      }
    

      public decimal? MinimumPmt
      {
        
            get { return _minimumPmt;}
            set 
            { 
                _minimumPmt = value;
            }
          
      }
    

      public int? Author
      {
        
            get { return _author;}
            set 
            { 
                _author = value;
            }
          
      }
    

      public String Status
      {
        
            get { return _status;}
            set 
            { 
                _status = value;
            }
          
      }
    

      public TermUnit ParentTermUnit
      {
      get { return _parentTermUnit;}
      set { _parentTermUnit = value; }
      }
    


    public override DomainObject extractSingleObject()
    {
      Lease lease = new Lease();
      
      lease.LeaseId = this.LeaseId;
      lease.LCN = this.LCN;
      lease.DocumentNumber = this.DocumentNumber;
      lease.Volume = this.Volume;
      lease.PAGE = this.PAGE;
      lease.LeaseeName = this.LeaseeName;
      lease.AssigneeName = this.AssigneeName;
      lease.LeassorName = this.LeassorName;
      lease.AssignorName = this.AssignorName;
      lease.StateFips = this.StateFips;
      lease.CountyFips = this.CountyFips;
      lease.UnitDepth = this.UnitDepth;
      lease.FromDepth = this.FromDepth;
      lease.FromFrom = this.FromFrom;
      lease.ToDepth = this.ToDepth;
      lease.ToFrom = this.ToFrom;
      lease.WorkInt = this.WorkInt;
      lease.OrrInt = this.OrrInt;
      lease.NetAcres = this.NetAcres;
      lease.GrossAcres = this.GrossAcres;
      lease.NriAssign = this.NriAssign;
      lease.RcdDate = this.RcdDate;
      lease.Term = this.Term;
      lease.TermUnitId = this.TermUnitId;
      lease.HBR = this.HBR;
      lease.Encumbrances = this.Encumbrances;
      lease.EffDate = this.EffDate;
      lease.PughClause = this.PughClause;
      lease.DepthLimitation = this.DepthLimitation;
      lease.ShutInClau = this.ShutInClau;
      lease.PoolingClau = this.PoolingClau;
      lease.MinimumPmt = this.MinimumPmt;
      lease.Author = this.Author;
      lease.Status = this.Status;
      lease.ActiveRecordId = this.ActiveRecordId; 
      return lease;
    }

    
          // one to many relation
          private List<LeaseEditHistory> _leaseEditHistorys;

          public List<LeaseEditHistory> leaseEditHistorys
          {
          get { return _leaseEditHistorys;}
          set { _leaseEditHistorys = value; }
          }
        
    }
  

    public partial class LeaseDataMapper:TDataMapper<Lease,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public LeaseDataMapper(){}
      public LeaseDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Lease";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Lease] (
    LCN,DocumentNumber,Volume,PAGE,LeaseeName,AssigneeName,LeassorName,AssignorName,StateFips,CountyFips,UnitDepth,FromDepth,FromFrom,ToDepth,ToFrom,WorkInt,OrrInt,NetAcres,GrossAcres,NriAssign,RcdDate,Term,TermUnitId,HBR,Encumbrances,EffDate,PughClause,DepthLimitation,ShutInClau,PoolingClau,MinimumPmt,Author,Status) Values (
    
      @LCN,
      @DocumentNumber,
      @Volume,
      @PAGE,
      @LeaseeName,
      @AssigneeName,
      @LeassorName,
      @AssignorName,
      @StateFips,
      @CountyFips,
      @UnitDepth,
      @FromDepth,
      @FromFrom,
      @ToDepth,
      @ToFrom,
      @WorkInt,
      @OrrInt,
      @NetAcres,
      @GrossAcres,
      @NriAssign,
      @RcdDate,
      @Term,
      @TermUnitId,
      @HBR,
      @Encumbrances,
      @EffDate,
      @PughClause,
      @DepthLimitation,
      @ShutInClau,
      @PoolingClau,
      @MinimumPmt,
      @Author,
      @Status);
    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Lease create( Lease lease )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                if(lease.LCN != null)
                  sqlCommand.Parameters.AddWithValue("@LCN", lease.LCN);
                else
                  sqlCommand.Parameters.AddWithValue("@LCN", DBNull.Value);
              
                if(lease.DocumentNumber != null)
                  sqlCommand.Parameters.AddWithValue("@DocumentNumber", lease.DocumentNumber);
                else
                  sqlCommand.Parameters.AddWithValue("@DocumentNumber", DBNull.Value);
              
                if(lease.Volume != null)
                  sqlCommand.Parameters.AddWithValue("@Volume", lease.Volume);
                else
                  sqlCommand.Parameters.AddWithValue("@Volume", DBNull.Value);
              
                if(lease.PAGE != null)
                  sqlCommand.Parameters.AddWithValue("@PAGE", lease.PAGE);
                else
                  sqlCommand.Parameters.AddWithValue("@PAGE", DBNull.Value);
              
                if(lease.LeaseeName != null)
                  sqlCommand.Parameters.AddWithValue("@LeaseeName", lease.LeaseeName);
                else
                  sqlCommand.Parameters.AddWithValue("@LeaseeName", DBNull.Value);
              
                if(lease.AssigneeName != null)
                  sqlCommand.Parameters.AddWithValue("@AssigneeName", lease.AssigneeName);
                else
                  sqlCommand.Parameters.AddWithValue("@AssigneeName", DBNull.Value);
              
                if(lease.LeassorName != null)
                  sqlCommand.Parameters.AddWithValue("@LeassorName", lease.LeassorName);
                else
                  sqlCommand.Parameters.AddWithValue("@LeassorName", DBNull.Value);
              
                if(lease.AssignorName != null)
                  sqlCommand.Parameters.AddWithValue("@AssignorName", lease.AssignorName);
                else
                  sqlCommand.Parameters.AddWithValue("@AssignorName", DBNull.Value);
              
                if(lease.StateFips != null)
                  sqlCommand.Parameters.AddWithValue("@StateFips", lease.StateFips);
                else
                  sqlCommand.Parameters.AddWithValue("@StateFips", DBNull.Value);
              
                if(lease.CountyFips != null)
                  sqlCommand.Parameters.AddWithValue("@CountyFips", lease.CountyFips);
                else
                  sqlCommand.Parameters.AddWithValue("@CountyFips", DBNull.Value);
              
                if(lease.UnitDepth != null)
                  sqlCommand.Parameters.AddWithValue("@UnitDepth", lease.UnitDepth);
                else
                  sqlCommand.Parameters.AddWithValue("@UnitDepth", DBNull.Value);
              
                if(lease.FromDepth != null)
                  sqlCommand.Parameters.AddWithValue("@FromDepth", lease.FromDepth);
                else
                  sqlCommand.Parameters.AddWithValue("@FromDepth", DBNull.Value);
              
                if(lease.FromFrom != null)
                  sqlCommand.Parameters.AddWithValue("@FromFrom", lease.FromFrom);
                else
                  sqlCommand.Parameters.AddWithValue("@FromFrom", DBNull.Value);
              
                if(lease.ToDepth != null)
                  sqlCommand.Parameters.AddWithValue("@ToDepth", lease.ToDepth);
                else
                  sqlCommand.Parameters.AddWithValue("@ToDepth", DBNull.Value);
              
                if(lease.ToFrom != null)
                  sqlCommand.Parameters.AddWithValue("@ToFrom", lease.ToFrom);
                else
                  sqlCommand.Parameters.AddWithValue("@ToFrom", DBNull.Value);
              
                if(lease.WorkInt != null)
                  sqlCommand.Parameters.AddWithValue("@WorkInt", lease.WorkInt);
                else
                  sqlCommand.Parameters.AddWithValue("@WorkInt", DBNull.Value);
              
                if(lease.OrrInt != null)
                  sqlCommand.Parameters.AddWithValue("@OrrInt", lease.OrrInt);
                else
                  sqlCommand.Parameters.AddWithValue("@OrrInt", DBNull.Value);
              
                if(lease.NetAcres != null)
                  sqlCommand.Parameters.AddWithValue("@NetAcres", lease.NetAcres);
                else
                  sqlCommand.Parameters.AddWithValue("@NetAcres", DBNull.Value);
              
                if(lease.GrossAcres != null)
                  sqlCommand.Parameters.AddWithValue("@GrossAcres", lease.GrossAcres);
                else
                  sqlCommand.Parameters.AddWithValue("@GrossAcres", DBNull.Value);
              
                if(lease.NriAssign != null)
                  sqlCommand.Parameters.AddWithValue("@NriAssign", lease.NriAssign);
                else
                  sqlCommand.Parameters.AddWithValue("@NriAssign", DBNull.Value);
              
                if(lease.RcdDate != null)
                  sqlCommand.Parameters.AddWithValue("@RcdDate", lease.RcdDate);
                else
                  sqlCommand.Parameters.AddWithValue("@RcdDate", DBNull.Value);
              
                if(lease.Term != null)
                  sqlCommand.Parameters.AddWithValue("@Term", lease.Term);
                else
                  sqlCommand.Parameters.AddWithValue("@Term", DBNull.Value);
              
                if(lease.TermUnitId != null)
                  sqlCommand.Parameters.AddWithValue("@TermUnitId", lease.TermUnitId);
                else
                  sqlCommand.Parameters.AddWithValue("@TermUnitId", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@HBR", lease.HBR);
              
                  sqlCommand.Parameters.AddWithValue("@Encumbrances", lease.Encumbrances);
              
                if(lease.EffDate != null)
                  sqlCommand.Parameters.AddWithValue("@EffDate", lease.EffDate);
                else
                  sqlCommand.Parameters.AddWithValue("@EffDate", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@PughClause", lease.PughClause);
              
                  sqlCommand.Parameters.AddWithValue("@DepthLimitation", lease.DepthLimitation);
              
                  sqlCommand.Parameters.AddWithValue("@ShutInClau", lease.ShutInClau);
              
                  sqlCommand.Parameters.AddWithValue("@PoolingClau", lease.PoolingClau);
              
                if(lease.MinimumPmt != null)
                  sqlCommand.Parameters.AddWithValue("@MinimumPmt", lease.MinimumPmt);
                else
                  sqlCommand.Parameters.AddWithValue("@MinimumPmt", DBNull.Value);
              
                if(lease.Author != null)
                  sqlCommand.Parameters.AddWithValue("@Author", lease.Author);
                else
                  sqlCommand.Parameters.AddWithValue("@Author", DBNull.Value);
              
                if(lease.Status != null)
                  sqlCommand.Parameters.AddWithValue("@Status", lease.Status);
                else
                  sqlCommand.Parameters.AddWithValue("@Status", DBNull.Value);
              lease.LeaseId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(lease.leaseEditHistorys != null 
            && lease.leaseEditHistorys.Count > 0)
          {
            LeaseEditHistoryDataMapper dataMapper = new LeaseEditHistoryDataMapper(Database);
            
            foreach(LeaseEditHistory item in lease.leaseEditHistorys)
              dataMapper.create(item);
          }
        
      
      raiseAffected(lease,DataMapperOperation.create);

      return registerRecord(lease);
    }

  

    private const String SqlSelectAll = @"Select
    LeaseId,LCN,DocumentNumber,Volume,PAGE,LeaseeName,AssigneeName,LeassorName,AssignorName,StateFips,CountyFips,UnitDepth,FromDepth,FromFrom,ToDepth,ToFrom,WorkInt,OrrInt,NetAcres,GrossAcres,NriAssign,RcdDate,Term,TermUnitId,HBR,Encumbrances,EffDate,PughClause,DepthLimitation,ShutInClau,PoolingClau,MinimumPmt,Author,Status 
    From [Lease] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    LeaseId,LCN,DocumentNumber,Volume,PAGE,LeaseeName,AssigneeName,LeassorName,AssignorName,StateFips,CountyFips,UnitDepth,FromDepth,FromFrom,ToDepth,ToFrom,WorkInt,OrrInt,NetAcres,GrossAcres,NriAssign,RcdDate,Term,TermUnitId,HBR,Encumbrances,EffDate,PughClause,DepthLimitation,ShutInClau,PoolingClau,MinimumPmt,Author,Status
     From [Lease]
    
       Where 
      LeaseId = @LeaseId
    ";

    public Lease findByPrimaryKey(
    int leaseId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@LeaseId", leaseId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Lease not found, search by primary key");
 

    }


    public bool exists(Lease lease)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@LeaseId", lease.LeaseId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Lease].[LeaseId] = @CheckInLeaseId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Lease _Lease = (Lease)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInLeaseId", _Lease.LeaseId);
      

      return sqlCommand;
    }

  

    protected override Lease doLoad(IDataReader dataReader)
    {
    Lease lease = new Lease();

    lease.LeaseId = dataReader.GetInt32(0);
            
          if(!dataReader.IsDBNull(1))        
          lease.LCN = dataReader.GetString(1);
            
          if(!dataReader.IsDBNull(2))        
          lease.DocumentNumber = dataReader.GetString(2);
            
          if(!dataReader.IsDBNull(3))        
          lease.Volume = dataReader.GetString(3);
            
          if(!dataReader.IsDBNull(4))        
          lease.PAGE = dataReader.GetString(4);
            
          if(!dataReader.IsDBNull(5))        
          lease.LeaseeName = dataReader.GetString(5);
            
          if(!dataReader.IsDBNull(6))        
          lease.AssigneeName = dataReader.GetString(6);
            
          if(!dataReader.IsDBNull(7))        
          lease.LeassorName = dataReader.GetString(7);
            
          if(!dataReader.IsDBNull(8))        
          lease.AssignorName = dataReader.GetString(8);
            
          if(!dataReader.IsDBNull(9))        
          lease.StateFips = dataReader.GetString(9);
            
          if(!dataReader.IsDBNull(10))        
          lease.CountyFips = dataReader.GetString(10);
            
          if(!dataReader.IsDBNull(11))        
          lease.UnitDepth = dataReader.GetDecimal(11);
            
          if(!dataReader.IsDBNull(12))        
          lease.FromDepth = dataReader.GetDecimal(12);
            
          if(!dataReader.IsDBNull(13))        
          lease.FromFrom = dataReader.GetDecimal(13);
            
          if(!dataReader.IsDBNull(14))        
          lease.ToDepth = dataReader.GetDecimal(14);
            
          if(!dataReader.IsDBNull(15))        
          lease.ToFrom = dataReader.GetDecimal(15);
            
          if(!dataReader.IsDBNull(16))        
          lease.WorkInt = dataReader.GetString(16);
            
          if(!dataReader.IsDBNull(17))        
          lease.OrrInt = dataReader.GetString(17);
            
          if(!dataReader.IsDBNull(18))        
          lease.NetAcres = dataReader.GetDecimal(18);
            
          if(!dataReader.IsDBNull(19))        
          lease.GrossAcres = dataReader.GetDecimal(19);
            
          if(!dataReader.IsDBNull(20))        
          lease.NriAssign = dataReader.GetString(20);
            
          if(!dataReader.IsDBNull(21))        
          lease.RcdDate = dataReader.GetDateTime(21);
            
          if(!dataReader.IsDBNull(22))        
          lease.Term = dataReader.GetDecimal(22);
            
          if(!dataReader.IsDBNull(23))        
          lease.TermUnitId = dataReader.GetInt32(23);
            lease.HBR = dataReader.GetBoolean(24);
            lease.Encumbrances = dataReader.GetBoolean(25);
            
          if(!dataReader.IsDBNull(26))        
          lease.EffDate = dataReader.GetDateTime(26);
            lease.PughClause = dataReader.GetBoolean(27);
            lease.DepthLimitation = dataReader.GetBoolean(28);
            lease.ShutInClau = dataReader.GetBoolean(29);
            lease.PoolingClau = dataReader.GetBoolean(30);
            
          if(!dataReader.IsDBNull(31))        
          lease.MinimumPmt = dataReader.GetDecimal(31);
            
          if(!dataReader.IsDBNull(32))        
          lease.Author = dataReader.GetInt32(32);
            
          if(!dataReader.IsDBNull(33))        
          lease.Status = dataReader.GetString(33);
            

    
    
    return registerRecord(lease);
    }


    protected override Lease doLoad(Hashtable hashtable)
    {
      Lease lease = new Lease();

      
        
        if(hashtable.ContainsKey("LeaseId"))
            lease.LeaseId = ( int)hashtable["LeaseId"];
      
        
        if(hashtable.ContainsKey("LCN"))
            lease.LCN = ( String)hashtable["LCN"];
      
        
        if(hashtable.ContainsKey("DocumentNumber"))
            lease.DocumentNumber = ( String)hashtable["DocumentNumber"];
      
        
        if(hashtable.ContainsKey("Volume"))
            lease.Volume = ( String)hashtable["Volume"];
      
        
        if(hashtable.ContainsKey("PAGE"))
            lease.PAGE = ( String)hashtable["PAGE"];
      
        
        if(hashtable.ContainsKey("LeaseeName"))
            lease.LeaseeName = ( String)hashtable["LeaseeName"];
      
        
        if(hashtable.ContainsKey("AssigneeName"))
            lease.AssigneeName = ( String)hashtable["AssigneeName"];
      
        
        if(hashtable.ContainsKey("LeassorName"))
            lease.LeassorName = ( String)hashtable["LeassorName"];
      
        
        if(hashtable.ContainsKey("AssignorName"))
            lease.AssignorName = ( String)hashtable["AssignorName"];
      
        
        if(hashtable.ContainsKey("StateFips"))
            lease.StateFips = ( String)hashtable["StateFips"];
      
        
        if(hashtable.ContainsKey("CountyFips"))
            lease.CountyFips = ( String)hashtable["CountyFips"];
      
        
        if(hashtable.ContainsKey("UnitDepth"))
            lease.UnitDepth = ( decimal)hashtable["UnitDepth"];
      
        
        if(hashtable.ContainsKey("FromDepth"))
            lease.FromDepth = ( decimal)hashtable["FromDepth"];
      
        
        if(hashtable.ContainsKey("FromFrom"))
            lease.FromFrom = ( decimal)hashtable["FromFrom"];
      
        
        if(hashtable.ContainsKey("ToDepth"))
            lease.ToDepth = ( decimal)hashtable["ToDepth"];
      
        
        if(hashtable.ContainsKey("ToFrom"))
            lease.ToFrom = ( decimal)hashtable["ToFrom"];
      
        
        if(hashtable.ContainsKey("WorkInt"))
            lease.WorkInt = ( String)hashtable["WorkInt"];
      
        
        if(hashtable.ContainsKey("OrrInt"))
            lease.OrrInt = ( String)hashtable["OrrInt"];
      
        
        if(hashtable.ContainsKey("NetAcres"))
            lease.NetAcres = ( decimal)hashtable["NetAcres"];
      
        
        if(hashtable.ContainsKey("GrossAcres"))
            lease.GrossAcres = ( decimal)hashtable["GrossAcres"];
      
        
        if(hashtable.ContainsKey("NriAssign"))
            lease.NriAssign = ( String)hashtable["NriAssign"];
      
        
        if(hashtable.ContainsKey("RcdDate"))
            lease.RcdDate = ( DateTime)hashtable["RcdDate"];
      
        
        if(hashtable.ContainsKey("Term"))
            lease.Term = ( decimal)hashtable["Term"];
      
        
        if(hashtable.ContainsKey("TermUnitId"))
            lease.TermUnitId = ( int)hashtable["TermUnitId"];
      
        
        if(hashtable.ContainsKey("HBR"))
            lease.HBR = ( bool)hashtable["HBR"];
      
        
        if(hashtable.ContainsKey("Encumbrances"))
            lease.Encumbrances = ( bool)hashtable["Encumbrances"];
      
        
        if(hashtable.ContainsKey("EffDate"))
            lease.EffDate = ( DateTime)hashtable["EffDate"];
      
        
        if(hashtable.ContainsKey("PughClause"))
            lease.PughClause = ( bool)hashtable["PughClause"];
      
        
        if(hashtable.ContainsKey("DepthLimitation"))
            lease.DepthLimitation = ( bool)hashtable["DepthLimitation"];
      
        
        if(hashtable.ContainsKey("ShutInClau"))
            lease.ShutInClau = ( bool)hashtable["ShutInClau"];
      
        
        if(hashtable.ContainsKey("PoolingClau"))
            lease.PoolingClau = ( bool)hashtable["PoolingClau"];
      
        
        if(hashtable.ContainsKey("MinimumPmt"))
            lease.MinimumPmt = ( decimal)hashtable["MinimumPmt"];
      
        
        if(hashtable.ContainsKey("Author"))
            lease.Author = ( int)hashtable["Author"];
      
        
        if(hashtable.ContainsKey("Status"))
            lease.Status = ( String)hashtable["Status"];
      

      return lease;
    }


    protected override List<Lease> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Lease> resultList = new List<Lease>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Lease item = new Lease();
              
              
                    item.LeaseId = ( int)dataRow["LeaseId"] ;
                  
                    if(!dataRow.IsNull("LCN") &&  !(dataRow["LCN"] is DBNull) )
                      item.LCN = ( String)dataRow["LCN"] ;
                  
                    if(!dataRow.IsNull("DocumentNumber") &&  !(dataRow["DocumentNumber"] is DBNull) )
                      item.DocumentNumber = ( String)dataRow["DocumentNumber"] ;
                  
                    if(!dataRow.IsNull("Volume") &&  !(dataRow["Volume"] is DBNull) )
                      item.Volume = ( String)dataRow["Volume"] ;
                  
                    if(!dataRow.IsNull("PAGE") &&  !(dataRow["PAGE"] is DBNull) )
                      item.PAGE = ( String)dataRow["PAGE"] ;
                  
                    if(!dataRow.IsNull("LeaseeName") &&  !(dataRow["LeaseeName"] is DBNull) )
                      item.LeaseeName = ( String)dataRow["LeaseeName"] ;
                  
                    if(!dataRow.IsNull("AssigneeName") &&  !(dataRow["AssigneeName"] is DBNull) )
                      item.AssigneeName = ( String)dataRow["AssigneeName"] ;
                  
                    if(!dataRow.IsNull("LeassorName") &&  !(dataRow["LeassorName"] is DBNull) )
                      item.LeassorName = ( String)dataRow["LeassorName"] ;
                  
                    if(!dataRow.IsNull("AssignorName") &&  !(dataRow["AssignorName"] is DBNull) )
                      item.AssignorName = ( String)dataRow["AssignorName"] ;
                  
                    if(!dataRow.IsNull("StateFips") &&  !(dataRow["StateFips"] is DBNull) )
                      item.StateFips = ( String)dataRow["StateFips"] ;
                  
                    if(!dataRow.IsNull("CountyFips") &&  !(dataRow["CountyFips"] is DBNull) )
                      item.CountyFips = ( String)dataRow["CountyFips"] ;
                  
                    if(!dataRow.IsNull("UnitDepth") &&  !(dataRow["UnitDepth"] is DBNull) )
                      item.UnitDepth = ( decimal)dataRow["UnitDepth"] ;
                  
                    if(!dataRow.IsNull("FromDepth") &&  !(dataRow["FromDepth"] is DBNull) )
                      item.FromDepth = ( decimal)dataRow["FromDepth"] ;
                  
                    if(!dataRow.IsNull("FromFrom") &&  !(dataRow["FromFrom"] is DBNull) )
                      item.FromFrom = ( decimal)dataRow["FromFrom"] ;
                  
                    if(!dataRow.IsNull("ToDepth") &&  !(dataRow["ToDepth"] is DBNull) )
                      item.ToDepth = ( decimal)dataRow["ToDepth"] ;
                  
                    if(!dataRow.IsNull("ToFrom") &&  !(dataRow["ToFrom"] is DBNull) )
                      item.ToFrom = ( decimal)dataRow["ToFrom"] ;
                  
                    if(!dataRow.IsNull("WorkInt") &&  !(dataRow["WorkInt"] is DBNull) )
                      item.WorkInt = ( String)dataRow["WorkInt"] ;
                  
                    if(!dataRow.IsNull("OrrInt") &&  !(dataRow["OrrInt"] is DBNull) )
                      item.OrrInt = ( String)dataRow["OrrInt"] ;
                  
                    if(!dataRow.IsNull("NetAcres") &&  !(dataRow["NetAcres"] is DBNull) )
                      item.NetAcres = ( decimal)dataRow["NetAcres"] ;
                  
                    if(!dataRow.IsNull("GrossAcres") &&  !(dataRow["GrossAcres"] is DBNull) )
                      item.GrossAcres = ( decimal)dataRow["GrossAcres"] ;
                  
                    if(!dataRow.IsNull("NriAssign") &&  !(dataRow["NriAssign"] is DBNull) )
                      item.NriAssign = ( String)dataRow["NriAssign"] ;
                  
                    if(!dataRow.IsNull("RcdDate") &&  !(dataRow["RcdDate"] is DBNull) )
                      item.RcdDate = ( DateTime)dataRow["RcdDate"] ;
                  
                    if(!dataRow.IsNull("Term") &&  !(dataRow["Term"] is DBNull) )
                      item.Term = ( decimal)dataRow["Term"] ;
                  
                    if(!dataRow.IsNull("TermUnitId") &&  !(dataRow["TermUnitId"] is DBNull) )
                      item.TermUnitId = ( int)dataRow["TermUnitId"] ;
                  
                    item.HBR = ( bool)dataRow["HBR"] ;
                  
                    item.Encumbrances = ( bool)dataRow["Encumbrances"] ;
                  
                    if(!dataRow.IsNull("EffDate") &&  !(dataRow["EffDate"] is DBNull) )
                      item.EffDate = ( DateTime)dataRow["EffDate"] ;
                  
                    item.PughClause = ( bool)dataRow["PughClause"] ;
                  
                    item.DepthLimitation = ( bool)dataRow["DepthLimitation"] ;
                  
                    item.ShutInClau = ( bool)dataRow["ShutInClau"] ;
                  
                    item.PoolingClau = ( bool)dataRow["PoolingClau"] ;
                  
                    if(!dataRow.IsNull("MinimumPmt") &&  !(dataRow["MinimumPmt"] is DBNull) )
                      item.MinimumPmt = ( decimal)dataRow["MinimumPmt"] ;
                  
                    if(!dataRow.IsNull("Author") &&  !(dataRow["Author"] is DBNull) )
                      item.Author = ( int)dataRow["Author"] ;
                  
                    if(!dataRow.IsNull("Status") &&  !(dataRow["Status"] is DBNull) )
                      item.Status = ( String)dataRow["Status"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [Lease]
    
      Where
      LeaseId = @LeaseId";
    [Synchronized]
    public Lease remove(Lease lease)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@LeaseId", lease.LeaseId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(lease,DataMapperOperation.delete);

      return registerRecord(lease);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Lease save( Lease lease )
    {
      if(exists(lease))
        return update(lease);
        return create(lease);
    }

  

    const String SqlUpdate = @"Update [Lease] Set 
    LCN = @LCN,DocumentNumber = @DocumentNumber,Volume = @Volume,PAGE = @PAGE,LeaseeName = @LeaseeName,AssigneeName = @AssigneeName,LeassorName = @LeassorName,AssignorName = @AssignorName,StateFips = @StateFips,CountyFips = @CountyFips,UnitDepth = @UnitDepth,FromDepth = @FromDepth,FromFrom = @FromFrom,ToDepth = @ToDepth,ToFrom = @ToFrom,WorkInt = @WorkInt,OrrInt = @OrrInt,NetAcres = @NetAcres,GrossAcres = @GrossAcres,NriAssign = @NriAssign,RcdDate = @RcdDate,Term = @Term,TermUnitId = @TermUnitId,HBR = @HBR,Encumbrances = @Encumbrances,EffDate = @EffDate,PughClause = @PughClause,DepthLimitation = @DepthLimitation,ShutInClau = @ShutInClau,PoolingClau = @PoolingClau,MinimumPmt = @MinimumPmt,Author = @Author,Status = @Status
       Where 
      LeaseId = @LeaseId";
    
    [TransactionRequired]
    [Synchronized]
    public Lease update(Lease lease)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@LeaseId", lease.LeaseId);
                  
                    
                    if(lease.LCN != null)
                      sqlCommand.Parameters.AddWithValue("@LCN", lease.LCN);
                    else
                      sqlCommand.Parameters.AddWithValue("@LCN", DBNull.Value);
                  
                    
                    if(lease.DocumentNumber != null)
                      sqlCommand.Parameters.AddWithValue("@DocumentNumber", lease.DocumentNumber);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocumentNumber", DBNull.Value);
                  
                    
                    if(lease.Volume != null)
                      sqlCommand.Parameters.AddWithValue("@Volume", lease.Volume);
                    else
                      sqlCommand.Parameters.AddWithValue("@Volume", DBNull.Value);
                  
                    
                    if(lease.PAGE != null)
                      sqlCommand.Parameters.AddWithValue("@PAGE", lease.PAGE);
                    else
                      sqlCommand.Parameters.AddWithValue("@PAGE", DBNull.Value);
                  
                    
                    if(lease.LeaseeName != null)
                      sqlCommand.Parameters.AddWithValue("@LeaseeName", lease.LeaseeName);
                    else
                      sqlCommand.Parameters.AddWithValue("@LeaseeName", DBNull.Value);
                  
                    
                    if(lease.AssigneeName != null)
                      sqlCommand.Parameters.AddWithValue("@AssigneeName", lease.AssigneeName);
                    else
                      sqlCommand.Parameters.AddWithValue("@AssigneeName", DBNull.Value);
                  
                    
                    if(lease.LeassorName != null)
                      sqlCommand.Parameters.AddWithValue("@LeassorName", lease.LeassorName);
                    else
                      sqlCommand.Parameters.AddWithValue("@LeassorName", DBNull.Value);
                  
                    
                    if(lease.AssignorName != null)
                      sqlCommand.Parameters.AddWithValue("@AssignorName", lease.AssignorName);
                    else
                      sqlCommand.Parameters.AddWithValue("@AssignorName", DBNull.Value);
                  
                    
                    if(lease.StateFips != null)
                      sqlCommand.Parameters.AddWithValue("@StateFips", lease.StateFips);
                    else
                      sqlCommand.Parameters.AddWithValue("@StateFips", DBNull.Value);
                  
                    
                    if(lease.CountyFips != null)
                      sqlCommand.Parameters.AddWithValue("@CountyFips", lease.CountyFips);
                    else
                      sqlCommand.Parameters.AddWithValue("@CountyFips", DBNull.Value);
                  
                    
                    if(lease.UnitDepth != null)
                      sqlCommand.Parameters.AddWithValue("@UnitDepth", lease.UnitDepth);
                    else
                      sqlCommand.Parameters.AddWithValue("@UnitDepth", DBNull.Value);
                  
                    
                    if(lease.FromDepth != null)
                      sqlCommand.Parameters.AddWithValue("@FromDepth", lease.FromDepth);
                    else
                      sqlCommand.Parameters.AddWithValue("@FromDepth", DBNull.Value);
                  
                    
                    if(lease.FromFrom != null)
                      sqlCommand.Parameters.AddWithValue("@FromFrom", lease.FromFrom);
                    else
                      sqlCommand.Parameters.AddWithValue("@FromFrom", DBNull.Value);
                  
                    
                    if(lease.ToDepth != null)
                      sqlCommand.Parameters.AddWithValue("@ToDepth", lease.ToDepth);
                    else
                      sqlCommand.Parameters.AddWithValue("@ToDepth", DBNull.Value);
                  
                    
                    if(lease.ToFrom != null)
                      sqlCommand.Parameters.AddWithValue("@ToFrom", lease.ToFrom);
                    else
                      sqlCommand.Parameters.AddWithValue("@ToFrom", DBNull.Value);
                  
                    
                    if(lease.WorkInt != null)
                      sqlCommand.Parameters.AddWithValue("@WorkInt", lease.WorkInt);
                    else
                      sqlCommand.Parameters.AddWithValue("@WorkInt", DBNull.Value);
                  
                    
                    if(lease.OrrInt != null)
                      sqlCommand.Parameters.AddWithValue("@OrrInt", lease.OrrInt);
                    else
                      sqlCommand.Parameters.AddWithValue("@OrrInt", DBNull.Value);
                  
                    
                    if(lease.NetAcres != null)
                      sqlCommand.Parameters.AddWithValue("@NetAcres", lease.NetAcres);
                    else
                      sqlCommand.Parameters.AddWithValue("@NetAcres", DBNull.Value);
                  
                    
                    if(lease.GrossAcres != null)
                      sqlCommand.Parameters.AddWithValue("@GrossAcres", lease.GrossAcres);
                    else
                      sqlCommand.Parameters.AddWithValue("@GrossAcres", DBNull.Value);
                  
                    
                    if(lease.NriAssign != null)
                      sqlCommand.Parameters.AddWithValue("@NriAssign", lease.NriAssign);
                    else
                      sqlCommand.Parameters.AddWithValue("@NriAssign", DBNull.Value);
                  
                    
                    if(lease.RcdDate != null)
                      sqlCommand.Parameters.AddWithValue("@RcdDate", lease.RcdDate);
                    else
                      sqlCommand.Parameters.AddWithValue("@RcdDate", DBNull.Value);
                  
                    
                    if(lease.Term != null)
                      sqlCommand.Parameters.AddWithValue("@Term", lease.Term);
                    else
                      sqlCommand.Parameters.AddWithValue("@Term", DBNull.Value);
                  
                    
                    if(lease.TermUnitId != null)
                      sqlCommand.Parameters.AddWithValue("@TermUnitId", lease.TermUnitId);
                    else
                      sqlCommand.Parameters.AddWithValue("@TermUnitId", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@HBR", lease.HBR);
                  
                      sqlCommand.Parameters.AddWithValue("@Encumbrances", lease.Encumbrances);
                  
                    
                    if(lease.EffDate != null)
                      sqlCommand.Parameters.AddWithValue("@EffDate", lease.EffDate);
                    else
                      sqlCommand.Parameters.AddWithValue("@EffDate", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@PughClause", lease.PughClause);
                  
                      sqlCommand.Parameters.AddWithValue("@DepthLimitation", lease.DepthLimitation);
                  
                      sqlCommand.Parameters.AddWithValue("@ShutInClau", lease.ShutInClau);
                  
                      sqlCommand.Parameters.AddWithValue("@PoolingClau", lease.PoolingClau);
                  
                    
                    if(lease.MinimumPmt != null)
                      sqlCommand.Parameters.AddWithValue("@MinimumPmt", lease.MinimumPmt);
                    else
                      sqlCommand.Parameters.AddWithValue("@MinimumPmt", DBNull.Value);
                  
                    
                    if(lease.Author != null)
                      sqlCommand.Parameters.AddWithValue("@Author", lease.Author);
                    else
                      sqlCommand.Parameters.AddWithValue("@Author", DBNull.Value);
                  
                    
                    if(lease.Status != null)
                      sqlCommand.Parameters.AddWithValue("@Status", lease.Status);
                    else
                      sqlCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(lease.leaseEditHistorys != null
          && lease.leaseEditHistorys.Count > 0)
          {
          LeaseEditHistoryDataMapper dataMapper = new LeaseEditHistoryDataMapper(Database);

          foreach(LeaseEditHistory item in lease.leaseEditHistorys)
            dataMapper.save(item);
          }
        

      raiseAffected(lease,DataMapperOperation.update);

      return registerRecord(lease);
    }

  
    }
    
  
    
    public partial class User: DomainObject
    {
    
      protected int _userId;
    
      protected String _login;
    
      protected String _password;
    
      protected String _email;
    
      protected bool _isActive;
    
      protected int _hackingAttempts;
    
      protected int _newTracts;
    

    public User(){}

    public User(
    int 
            userId,String 
            login,String 
            password,String 
            email,bool 
            isActive,int 
            hackingAttempts,int 
            newTracts
    )
    {
    UserId = userId;
    Login = login;
    Password = password;
    Email = email;
    IsActive = isActive;
    HackingAttempts = hackingAttempts;
    NewTracts = newTracts;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.User."
    
      + UserId.ToString()
    ;
    
    return uri;
    }

    

      public int UserId
      {
        
            get { return _userId;}
            set 
            { 
                _userId = value;
            }
          
      }
    

      public String Login
      {
        
            get { return _login;}
            set 
            { 
                _login = value;
            }
          
      }
    

      public String Password
      {
        
            get { return _password;}
            set 
            { 
                _password = value;
            }
          
      }
    

      public String Email
      {
        
            get { return _email;}
            set 
            { 
                _email = value;
            }
          
      }
    

      public bool IsActive
      {
        
            get { return _isActive;}
            set 
            { 
                _isActive = value;
            }
          
      }
    

      public int HackingAttempts
      {
        
            get { return _hackingAttempts;}
            set 
            { 
                _hackingAttempts = value;
            }
          
      }
    

      public int NewTracts
      {
        
            get { return _newTracts;}
            set 
            { 
                _newTracts = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      User user = new User();
      
      user.UserId = this.UserId;
      user.Login = this.Login;
      user.Password = this.Password;
      user.Email = this.Email;
      user.IsActive = this.IsActive;
      user.HackingAttempts = this.HackingAttempts;
      user.NewTracts = this.NewTracts;
      user.ActiveRecordId = this.ActiveRecordId; 
      return user;
    }

    
          // one to many relation
          private List<LeaseEditHistory> _leaseEditHistorys;

          public List<LeaseEditHistory> leaseEditHistorys
          {
          get { return _leaseEditHistorys;}
          set { _leaseEditHistorys = value; }
          }
        
          // one to many relation
          private List<UserRole> _userRoles;

          public List<UserRole> userRoles
          {
          get { return _userRoles;}
          set { _userRoles = value; }
          }
        
    }
  

    public partial class UserDataMapper:TDataMapper<User,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public UserDataMapper(){}
      public UserDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "User";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [User] (
    Login,Password,Email,IsActive,HackingAttempts,NewTracts) Values (
    
      @Login,
      @Password,
      @Email,
      @IsActive,
      @HackingAttempts,
      @NewTracts);
    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override User create( User user )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@Login", user.Login);
              
                  sqlCommand.Parameters.AddWithValue("@Password", user.Password);
              
                  sqlCommand.Parameters.AddWithValue("@Email", user.Email);
              
                  sqlCommand.Parameters.AddWithValue("@IsActive", user.IsActive);
              
                  sqlCommand.Parameters.AddWithValue("@HackingAttempts", user.HackingAttempts);
              
                  sqlCommand.Parameters.AddWithValue("@NewTracts", user.NewTracts);
              user.UserId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(user.leaseEditHistorys != null 
            && user.leaseEditHistorys.Count > 0)
          {
            LeaseEditHistoryDataMapper dataMapper = new LeaseEditHistoryDataMapper(Database);
            
            foreach(LeaseEditHistory item in user.leaseEditHistorys)
              dataMapper.create(item);
          }
        
          
          if(user.userRoles != null 
            && user.userRoles.Count > 0)
          {
            UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);
            
            foreach(UserRole item in user.userRoles)
              dataMapper.create(item);
          }
        
      
      raiseAffected(user,DataMapperOperation.create);

      return registerRecord(user);
    }

  

    private const String SqlSelectAll = @"Select
    UserId,Login,Password,Email,IsActive,HackingAttempts,NewTracts 
    From [User] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    UserId,Login,Password,Email,IsActive,HackingAttempts,NewTracts
     From [User]
    
       Where 
      UserId = @UserId
    ";

    public User findByPrimaryKey(
    int userId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@UserId", userId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("User not found, search by primary key");
 

    }


    public bool exists(User user)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [User].[UserId] = @CheckInUserId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      User _User = (User)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInUserId", _User.UserId);
      

      return sqlCommand;
    }

  

    protected override User doLoad(IDataReader dataReader)
    {
    User user = new User();

    user.UserId = dataReader.GetInt32(0);
            user.Login = dataReader.GetString(1);
            user.Password = dataReader.GetString(2);
            user.Email = dataReader.GetString(3);
            user.IsActive = dataReader.GetBoolean(4);
            user.HackingAttempts = dataReader.GetInt32(5);
            user.NewTracts = dataReader.GetInt32(6);
            

    
    
    return registerRecord(user);
    }


    protected override User doLoad(Hashtable hashtable)
    {
      User user = new User();

      
        
        if(hashtable.ContainsKey("UserId"))
            user.UserId = ( int)hashtable["UserId"];
      
        
        if(hashtable.ContainsKey("Login"))
            user.Login = ( String)hashtable["Login"];
      
        
        if(hashtable.ContainsKey("Password"))
            user.Password = ( String)hashtable["Password"];
      
        
        if(hashtable.ContainsKey("Email"))
            user.Email = ( String)hashtable["Email"];
      
        
        if(hashtable.ContainsKey("IsActive"))
            user.IsActive = ( bool)hashtable["IsActive"];
      
        
        if(hashtable.ContainsKey("HackingAttempts"))
            user.HackingAttempts = ( int)hashtable["HackingAttempts"];
      
        
        if(hashtable.ContainsKey("NewTracts"))
            user.NewTracts = ( int)hashtable["NewTracts"];
      

      return user;
    }


    protected override List<User> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<User> resultList = new List<User>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              User item = new User();
              
              
                    item.UserId = ( int)dataRow["UserId"] ;
                  
                    item.Login = ( String)dataRow["Login"] ;
                  
                    item.Password = ( String)dataRow["Password"] ;
                  
                    item.Email = ( String)dataRow["Email"] ;
                  
                    item.IsActive = ( bool)dataRow["IsActive"] ;
                  
                    item.HackingAttempts = ( int)dataRow["HackingAttempts"] ;
                  
                    item.NewTracts = ( int)dataRow["NewTracts"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [User]
    
      Where
      UserId = @UserId";
    [Synchronized]
    public User remove(User user)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(user,DataMapperOperation.delete);

      return registerRecord(user);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public User save( User user )
    {
      if(exists(user))
        return update(user);
        return create(user);
    }

  

    const String SqlUpdate = @"Update [User] Set 
    Login = @Login,Password = @Password,Email = @Email,IsActive = @IsActive,HackingAttempts = @HackingAttempts,NewTracts = @NewTracts
       Where 
      UserId = @UserId";
    
    [TransactionRequired]
    [Synchronized]
    public User update(User user)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@UserId", user.UserId);
                  
                      sqlCommand.Parameters.AddWithValue("@Login", user.Login);
                  
                      sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                  
                      sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                  
                      sqlCommand.Parameters.AddWithValue("@IsActive", user.IsActive);
                  
                      sqlCommand.Parameters.AddWithValue("@HackingAttempts", user.HackingAttempts);
                  
                      sqlCommand.Parameters.AddWithValue("@NewTracts", user.NewTracts);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(user.leaseEditHistorys != null
          && user.leaseEditHistorys.Count > 0)
          {
          LeaseEditHistoryDataMapper dataMapper = new LeaseEditHistoryDataMapper(Database);

          foreach(LeaseEditHistory item in user.leaseEditHistorys)
            dataMapper.save(item);
          }
        
          if(user.userRoles != null
          && user.userRoles.Count > 0)
          {
          UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);

          foreach(UserRole item in user.userRoles)
            dataMapper.save(item);
          }
        

      raiseAffected(user,DataMapperOperation.update);

      return registerRecord(user);
    }

  
    }
    
  
    
    public partial class LeaseEditHistory: DomainObject
    {
    
      protected int _editHistoryId;
    
      protected DateTime _dateEdited;
    
      protected String _status;
    

      // parent tables
      protected Lease _parentLease
        = new Lease()
      ;
    

      // parent tables
      protected User _parentUser
        = new User()
      ;
    

    public LeaseEditHistory(){}

    public LeaseEditHistory(
    int 
            editHistoryId,int 
            userId,int 
            leaseId,DateTime 
            dateEdited,String 
            status
    )
    {
    EditHistoryId = editHistoryId;
    UserId = userId;
    LeaseId = leaseId;
    DateEdited = dateEdited;
    Status = status;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.LeaseEditHistory."
    
      + EditHistoryId.ToString()
    ;
    
    return uri;
    }

    

      public int EditHistoryId
      {
        
            get { return _editHistoryId;}
            set 
            { 
                _editHistoryId = value;
            }
          
      }
    

      public int UserId
      {
        
            get
            {
            
                  if(_parentUser != null)
                    return _parentUser.UserId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentUser == null)
                        _parentUser = new User();

                      _parentUser.UserId = value;
                    
            }
          
      }
    

      public int LeaseId
      {
        
            get
            {
            
                  if(_parentLease != null)
                    return _parentLease.LeaseId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentLease == null)
                        _parentLease = new Lease();

                      _parentLease.LeaseId = value;
                    
            }
          
      }
    

      public DateTime DateEdited
      {
        
            get { return _dateEdited;}
            set 
            { 
                _dateEdited = value;
            }
          
      }
    

      public String Status
      {
        
            get { return _status;}
            set 
            { 
                _status = value;
            }
          
      }
    

      public Lease ParentLease
      {
      get { return _parentLease;}
      set { _parentLease = value; }
      }
    

      public User ParentUser
      {
      get { return _parentUser;}
      set { _parentUser = value; }
      }
    


    public override DomainObject extractSingleObject()
    {
      LeaseEditHistory leaseEditHistory = new LeaseEditHistory();
      
      leaseEditHistory.EditHistoryId = this.EditHistoryId;
      leaseEditHistory.UserId = this.UserId;
      leaseEditHistory.LeaseId = this.LeaseId;
      leaseEditHistory.DateEdited = this.DateEdited;
      leaseEditHistory.Status = this.Status;
      leaseEditHistory.ActiveRecordId = this.ActiveRecordId; 
      return leaseEditHistory;
    }

    
    }
  

    public partial class LeaseEditHistoryDataMapper:TDataMapper<LeaseEditHistory,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public LeaseEditHistoryDataMapper(){}
      public LeaseEditHistoryDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "LeaseEditHistory";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [LeaseEditHistory] (
    UserId,LeaseId,DateEdited,Status) Values (
    
      @UserId,
      @LeaseId,
      @DateEdited,
      @Status);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override LeaseEditHistory create( LeaseEditHistory leaseEditHistory )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@UserId", leaseEditHistory.UserId);
              
                  sqlCommand.Parameters.AddWithValue("@LeaseId", leaseEditHistory.LeaseId);
              
                  sqlCommand.Parameters.AddWithValue("@DateEdited", leaseEditHistory.DateEdited);
              
                if(leaseEditHistory.Status != null)
                  sqlCommand.Parameters.AddWithValue("@Status", leaseEditHistory.Status);
                else
                  sqlCommand.Parameters.AddWithValue("@Status", DBNull.Value);
              leaseEditHistory.EditHistoryId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(leaseEditHistory,DataMapperOperation.create);

      return registerRecord(leaseEditHistory);
    }

  

    private const String SqlSelectAll = @"Select
    EditHistoryId,UserId,LeaseId,DateEdited,Status 
    From [LeaseEditHistory] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    EditHistoryId,UserId,LeaseId,DateEdited,Status
     From [LeaseEditHistory]
    
       Where 
      EditHistoryId = @EditHistoryId
    ";

    public LeaseEditHistory findByPrimaryKey(
    int editHistoryId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@EditHistoryId", editHistoryId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("LeaseEditHistory not found, search by primary key");
 

    }


    public bool exists(LeaseEditHistory leaseEditHistory)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@EditHistoryId", leaseEditHistory.EditHistoryId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [LeaseEditHistory].[EditHistoryId] = @CheckInEditHistoryId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      LeaseEditHistory _LeaseEditHistory = (LeaseEditHistory)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInEditHistoryId", _LeaseEditHistory.EditHistoryId);
      

      return sqlCommand;
    }

  

    protected override LeaseEditHistory doLoad(IDataReader dataReader)
    {
    LeaseEditHistory leaseEditHistory = new LeaseEditHistory();

    leaseEditHistory.EditHistoryId = dataReader.GetInt32(0);
            leaseEditHistory.UserId = dataReader.GetInt32(1);
            leaseEditHistory.LeaseId = dataReader.GetInt32(2);
            leaseEditHistory.DateEdited = dataReader.GetDateTime(3);
            
          if(!dataReader.IsDBNull(4))        
          leaseEditHistory.Status = dataReader.GetString(4);
            

    
    
    return registerRecord(leaseEditHistory);
    }


    protected override LeaseEditHistory doLoad(Hashtable hashtable)
    {
      LeaseEditHistory leaseEditHistory = new LeaseEditHistory();

      
        
        if(hashtable.ContainsKey("EditHistoryId"))
            leaseEditHistory.EditHistoryId = ( int)hashtable["EditHistoryId"];
      
        
        if(hashtable.ContainsKey("UserId"))
            leaseEditHistory.UserId = ( int)hashtable["UserId"];
      
        
        if(hashtable.ContainsKey("LeaseId"))
            leaseEditHistory.LeaseId = ( int)hashtable["LeaseId"];
      
        
        if(hashtable.ContainsKey("DateEdited"))
            leaseEditHistory.DateEdited = ( DateTime)hashtable["DateEdited"];
      
        
        if(hashtable.ContainsKey("Status"))
            leaseEditHistory.Status = ( String)hashtable["Status"];
      

      return leaseEditHistory;
    }


    protected override List<LeaseEditHistory> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<LeaseEditHistory> resultList = new List<LeaseEditHistory>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              LeaseEditHistory item = new LeaseEditHistory();
              
              
                    item.EditHistoryId = ( int)dataRow["EditHistoryId"] ;
                  
                    item.UserId = ( int)dataRow["UserId"] ;
                  
                    item.LeaseId = ( int)dataRow["LeaseId"] ;
                  
                    item.DateEdited = ( DateTime)dataRow["DateEdited"] ;
                  
                    if(!dataRow.IsNull("Status") &&  !(dataRow["Status"] is DBNull) )
                      item.Status = ( String)dataRow["Status"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [LeaseEditHistory]
    
      Where
      EditHistoryId = @EditHistoryId";
    [Synchronized]
    public LeaseEditHistory remove(LeaseEditHistory leaseEditHistory)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@EditHistoryId", leaseEditHistory.EditHistoryId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(leaseEditHistory,DataMapperOperation.delete);

      return registerRecord(leaseEditHistory);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public LeaseEditHistory save( LeaseEditHistory leaseEditHistory )
    {
      if(exists(leaseEditHistory))
        return update(leaseEditHistory);
        return create(leaseEditHistory);
    }

  

    const String SqlUpdate = @"Update [LeaseEditHistory] Set 
    UserId = @UserId,LeaseId = @LeaseId,DateEdited = @DateEdited,Status = @Status
       Where 
      EditHistoryId = @EditHistoryId";
    
    
    [Synchronized]
    public LeaseEditHistory update(LeaseEditHistory leaseEditHistory)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@EditHistoryId", leaseEditHistory.EditHistoryId);
                  
                      sqlCommand.Parameters.AddWithValue("@UserId", leaseEditHistory.UserId);
                  
                      sqlCommand.Parameters.AddWithValue("@LeaseId", leaseEditHistory.LeaseId);
                  
                      sqlCommand.Parameters.AddWithValue("@DateEdited", leaseEditHistory.DateEdited);
                  
                    
                    if(leaseEditHistory.Status != null)
                      sqlCommand.Parameters.AddWithValue("@Status", leaseEditHistory.Status);
                    else
                      sqlCommand.Parameters.AddWithValue("@Status", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(leaseEditHistory,DataMapperOperation.update);

      return registerRecord(leaseEditHistory);
    }

  
    }
    
  
    
    public partial class State: DomainObject
    {
    
      protected int _stateId;
    
      protected String _name;
    
      protected String _stateFips;
    
      protected String _stateAbbr;
    

    public State(){}

    public State(
    int 
            stateId,String 
            name,String 
            stateFips,String 
            stateAbbr
    )
    {
    StateId = stateId;
    Name = name;
    StateFips = stateFips;
    StateAbbr = stateAbbr;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.State."
    
      + StateId.ToString()
    ;
    
    return uri;
    }

    

      public int StateId
      {
        
            get { return _stateId;}
            set 
            { 
                _stateId = value;
            }
          
      }
    

      public String Name
      {
        
            get { return _name;}
            set 
            { 
                _name = value;
            }
          
      }
    

      public String StateFips
      {
        
            get { return _stateFips;}
            set 
            { 
                _stateFips = value;
            }
          
      }
    

      public String StateAbbr
      {
        
            get { return _stateAbbr;}
            set 
            { 
                _stateAbbr = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      State state = new State();
      
      state.StateId = this.StateId;
      state.Name = this.Name;
      state.StateFips = this.StateFips;
      state.StateAbbr = this.StateAbbr;
      state.ActiveRecordId = this.ActiveRecordId; 
      return state;
    }

    
          // one to many relation
          private List<County> _countys;

          public List<County> countys
          {
          get { return _countys;}
          set { _countys = value; }
          }
        
    }
  

    public partial class StateDataMapper:TDataMapper<State,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public StateDataMapper(){}
      public StateDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "State";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [State] (
    StateId,Name,StateFips,StateAbbr) Values (
    
      @StateId,
      @Name,
      @StateFips,
      @StateAbbr);
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override State create( State state )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@StateId", state.StateId);
              
                if(state.Name != null)
                  sqlCommand.Parameters.AddWithValue("@Name", state.Name);
                else
                  sqlCommand.Parameters.AddWithValue("@Name", DBNull.Value);
              
                if(state.StateFips != null)
                  sqlCommand.Parameters.AddWithValue("@StateFips", state.StateFips);
                else
                  sqlCommand.Parameters.AddWithValue("@StateFips", DBNull.Value);
              
                if(state.StateAbbr != null)
                  sqlCommand.Parameters.AddWithValue("@StateAbbr", state.StateAbbr);
                else
                  sqlCommand.Parameters.AddWithValue("@StateAbbr", DBNull.Value);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(state.countys != null 
            && state.countys.Count > 0)
          {
            CountyDataMapper dataMapper = new CountyDataMapper(Database);
            
            foreach(County item in state.countys)
              dataMapper.create(item);
          }
        
      
      raiseAffected(state,DataMapperOperation.create);

      return registerRecord(state);
    }

  

    private const String SqlSelectAll = @"Select
    StateId,Name,StateFips,StateAbbr 
    From [State] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    StateId,Name,StateFips,StateAbbr
     From [State]
    
       Where 
      StateId = @StateId
    ";

    public State findByPrimaryKey(
    int stateId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@StateId", stateId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("State not found, search by primary key");
 

    }


    public bool exists(State state)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@StateId", state.StateId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [State].[StateId] = @CheckInStateId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      State _State = (State)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInStateId", _State.StateId);
      

      return sqlCommand;
    }

  

    protected override State doLoad(IDataReader dataReader)
    {
    State state = new State();

    state.StateId = dataReader.GetInt32(0);
            
          if(!dataReader.IsDBNull(1))        
          state.Name = dataReader.GetString(1);
            
          if(!dataReader.IsDBNull(2))        
          state.StateFips = dataReader.GetString(2);
            
          if(!dataReader.IsDBNull(3))        
          state.StateAbbr = dataReader.GetString(3);
            

    
    
    return registerRecord(state);
    }


    protected override State doLoad(Hashtable hashtable)
    {
      State state = new State();

      
        
        if(hashtable.ContainsKey("StateId"))
            state.StateId = ( int)hashtable["StateId"];
      
        
        if(hashtable.ContainsKey("Name"))
            state.Name = ( String)hashtable["Name"];
      
        
        if(hashtable.ContainsKey("StateFips"))
            state.StateFips = ( String)hashtable["StateFips"];
      
        
        if(hashtable.ContainsKey("StateAbbr"))
            state.StateAbbr = ( String)hashtable["StateAbbr"];
      

      return state;
    }


    protected override List<State> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<State> resultList = new List<State>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              State item = new State();
              
              
                    item.StateId = ( int)dataRow["StateId"] ;
                  
                    if(!dataRow.IsNull("Name") &&  !(dataRow["Name"] is DBNull) )
                      item.Name = ( String)dataRow["Name"] ;
                  
                    if(!dataRow.IsNull("StateFips") &&  !(dataRow["StateFips"] is DBNull) )
                      item.StateFips = ( String)dataRow["StateFips"] ;
                  
                    if(!dataRow.IsNull("StateAbbr") &&  !(dataRow["StateAbbr"] is DBNull) )
                      item.StateAbbr = ( String)dataRow["StateAbbr"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [State]
    
      Where
      StateId = @StateId";
    [Synchronized]
    public State remove(State state)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@StateId", state.StateId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(state,DataMapperOperation.delete);

      return registerRecord(state);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public State save( State state )
    {
      if(exists(state))
        return update(state);
        return create(state);
    }

  

    const String SqlUpdate = @"Update [State] Set 
    Name = @Name,StateFips = @StateFips,StateAbbr = @StateAbbr
       Where 
      StateId = @StateId";
    
    [TransactionRequired]
    [Synchronized]
    public State update(State state)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@StateId", state.StateId);
                  
                    
                    if(state.Name != null)
                      sqlCommand.Parameters.AddWithValue("@Name", state.Name);
                    else
                      sqlCommand.Parameters.AddWithValue("@Name", DBNull.Value);
                  
                    
                    if(state.StateFips != null)
                      sqlCommand.Parameters.AddWithValue("@StateFips", state.StateFips);
                    else
                      sqlCommand.Parameters.AddWithValue("@StateFips", DBNull.Value);
                  
                    
                    if(state.StateAbbr != null)
                      sqlCommand.Parameters.AddWithValue("@StateAbbr", state.StateAbbr);
                    else
                      sqlCommand.Parameters.AddWithValue("@StateAbbr", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(state.countys != null
          && state.countys.Count > 0)
          {
          CountyDataMapper dataMapper = new CountyDataMapper(Database);

          foreach(County item in state.countys)
            dataMapper.save(item);
          }
        

      raiseAffected(state,DataMapperOperation.update);

      return registerRecord(state);
    }

  
    }
    
  
    
    public partial class TermUnit: DomainObject
    {
    
      protected int _termUnitId;
    
      protected String _name;
    

    public TermUnit(){}

    public TermUnit(
    int 
            termUnitId,String 
            name
    )
    {
    TermUnitId = termUnitId;
    Name = name;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.TermUnit."
    
      + TermUnitId.ToString()
    ;
    
    return uri;
    }

    

      public int TermUnitId
      {
        
            get { return _termUnitId;}
            set 
            { 
                _termUnitId = value;
            }
          
      }
    

      public String Name
      {
        
            get { return _name;}
            set 
            { 
                _name = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      TermUnit termUnit = new TermUnit();
      
      termUnit.TermUnitId = this.TermUnitId;
      termUnit.Name = this.Name;
      termUnit.ActiveRecordId = this.ActiveRecordId; 
      return termUnit;
    }

    
          // one to many relation
          private List<Lease> _leases;

          public List<Lease> leases
          {
          get { return _leases;}
          set { _leases = value; }
          }
        
    }
  

    public partial class TermUnitDataMapper:TDataMapper<TermUnit,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public TermUnitDataMapper(){}
      public TermUnitDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "TermUnit";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [TermUnit] (
    Name) Values (
    
      @Name);
    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override TermUnit create( TermUnit termUnit )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@Name", termUnit.Name);
              termUnit.TermUnitId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(termUnit.leases != null 
            && termUnit.leases.Count > 0)
          {
            LeaseDataMapper dataMapper = new LeaseDataMapper(Database);
            
            foreach(Lease item in termUnit.leases)
              dataMapper.create(item);
          }
        
      
      raiseAffected(termUnit,DataMapperOperation.create);

      return registerRecord(termUnit);
    }

  

    private const String SqlSelectAll = @"Select
    TermUnitId,Name 
    From [TermUnit] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    TermUnitId,Name
     From [TermUnit]
    
       Where 
      TermUnitId = @TermUnitId
    ";

    public TermUnit findByPrimaryKey(
    int termUnitId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@TermUnitId", termUnitId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("TermUnit not found, search by primary key");
 

    }


    public bool exists(TermUnit termUnit)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@TermUnitId", termUnit.TermUnitId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [TermUnit].[TermUnitId] = @CheckInTermUnitId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      TermUnit _TermUnit = (TermUnit)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInTermUnitId", _TermUnit.TermUnitId);
      

      return sqlCommand;
    }

  

    protected override TermUnit doLoad(IDataReader dataReader)
    {
    TermUnit termUnit = new TermUnit();

    termUnit.TermUnitId = dataReader.GetInt32(0);
            termUnit.Name = dataReader.GetString(1);
            

    
    
    return registerRecord(termUnit);
    }


    protected override TermUnit doLoad(Hashtable hashtable)
    {
      TermUnit termUnit = new TermUnit();

      
        
        if(hashtable.ContainsKey("TermUnitId"))
            termUnit.TermUnitId = ( int)hashtable["TermUnitId"];
      
        
        if(hashtable.ContainsKey("Name"))
            termUnit.Name = ( String)hashtable["Name"];
      

      return termUnit;
    }


    protected override List<TermUnit> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<TermUnit> resultList = new List<TermUnit>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              TermUnit item = new TermUnit();
              
              
                    item.TermUnitId = ( int)dataRow["TermUnitId"] ;
                  
                    item.Name = ( String)dataRow["Name"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [TermUnit]
    
      Where
      TermUnitId = @TermUnitId";
    [Synchronized]
    public TermUnit remove(TermUnit termUnit)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@TermUnitId", termUnit.TermUnitId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(termUnit,DataMapperOperation.delete);

      return registerRecord(termUnit);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public TermUnit save( TermUnit termUnit )
    {
      if(exists(termUnit))
        return update(termUnit);
        return create(termUnit);
    }

  

    const String SqlUpdate = @"Update [TermUnit] Set 
    Name = @Name
       Where 
      TermUnitId = @TermUnitId";
    
    [TransactionRequired]
    [Synchronized]
    public TermUnit update(TermUnit termUnit)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@TermUnitId", termUnit.TermUnitId);
                  
                      sqlCommand.Parameters.AddWithValue("@Name", termUnit.Name);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(termUnit.leases != null
          && termUnit.leases.Count > 0)
          {
          LeaseDataMapper dataMapper = new LeaseDataMapper(Database);

          foreach(Lease item in termUnit.leases)
            dataMapper.save(item);
          }
        

      raiseAffected(termUnit,DataMapperOperation.update);

      return registerRecord(termUnit);
    }

  
    }
    
  
    
    public partial class UserRole: DomainObject
    {
    
      protected int _userRoleId;
    

      // parent tables
      protected Role _parentRole
        = new Role()
      ;
    

      // parent tables
      protected User _parentUser
        = new User()
      ;
    

    public UserRole(){}

    public UserRole(
    int 
            userRoleId,int 
            userId,int 
            roleId
    )
    {
    UserRoleId = userRoleId;
    UserId = userId;
    RoleId = roleId;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.UserRole."
    
      + UserRoleId.ToString()
    ;
    
    return uri;
    }

    

      public int UserRoleId
      {
        
            get { return _userRoleId;}
            set 
            { 
                _userRoleId = value;
            }
          
      }
    

      public int UserId
      {
        
            get
            {
            
                  if(_parentUser != null)
                    return _parentUser.UserId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentUser == null)
                        _parentUser = new User();

                      _parentUser.UserId = value;
                    
            }
          
      }
    

      public int RoleId
      {
        
            get
            {
            
                  if(_parentRole != null)
                    return _parentRole.RoleId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentRole == null)
                        _parentRole = new Role();

                      _parentRole.RoleId = value;
                    
            }
          
      }
    

      public Role ParentRole
      {
      get { return _parentRole;}
      set { _parentRole = value; }
      }
    

      public User ParentUser
      {
      get { return _parentUser;}
      set { _parentUser = value; }
      }
    


    public override DomainObject extractSingleObject()
    {
      UserRole userRole = new UserRole();
      
      userRole.UserRoleId = this.UserRoleId;
      userRole.UserId = this.UserId;
      userRole.RoleId = this.RoleId;
      userRole.ActiveRecordId = this.ActiveRecordId; 
      return userRole;
    }

    
    }
  

    public partial class UserRoleDataMapper:TDataMapper<UserRole,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public UserRoleDataMapper(){}
      public UserRoleDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "UserRole";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [UserRole] (
    UserId,RoleId) Values (
    
      @UserId,
      @RoleId);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override UserRole create( UserRole userRole )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@UserId", userRole.UserId);
              
                  sqlCommand.Parameters.AddWithValue("@RoleId", userRole.RoleId);
              userRole.UserRoleId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(userRole,DataMapperOperation.create);

      return registerRecord(userRole);
    }

  

    private const String SqlSelectAll = @"Select
    UserRoleId,UserId,RoleId 
    From [UserRole] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    UserRoleId,UserId,RoleId
     From [UserRole]
    
       Where 
      UserRoleId = @UserRoleId
    ";

    public UserRole findByPrimaryKey(
    int userRoleId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@UserRoleId", userRoleId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("UserRole not found, search by primary key");
 

    }


    public bool exists(UserRole userRole)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [UserRole].[UserRoleId] = @CheckInUserRoleId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      UserRole _UserRole = (UserRole)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInUserRoleId", _UserRole.UserRoleId);
      

      return sqlCommand;
    }

  

    protected override UserRole doLoad(IDataReader dataReader)
    {
    UserRole userRole = new UserRole();

    userRole.UserRoleId = dataReader.GetInt32(0);
            userRole.UserId = dataReader.GetInt32(1);
            userRole.RoleId = dataReader.GetInt32(2);
            

    
    
    return registerRecord(userRole);
    }


    protected override UserRole doLoad(Hashtable hashtable)
    {
      UserRole userRole = new UserRole();

      
        
        if(hashtable.ContainsKey("UserRoleId"))
            userRole.UserRoleId = ( int)hashtable["UserRoleId"];
      
        
        if(hashtable.ContainsKey("UserId"))
            userRole.UserId = ( int)hashtable["UserId"];
      
        
        if(hashtable.ContainsKey("RoleId"))
            userRole.RoleId = ( int)hashtable["RoleId"];
      

      return userRole;
    }


    protected override List<UserRole> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<UserRole> resultList = new List<UserRole>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              UserRole item = new UserRole();
              
              
                    item.UserRoleId = ( int)dataRow["UserRoleId"] ;
                  
                    item.UserId = ( int)dataRow["UserId"] ;
                  
                    item.RoleId = ( int)dataRow["RoleId"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [UserRole]
    
      Where
      UserRoleId = @UserRoleId";
    [Synchronized]
    public UserRole remove(UserRole userRole)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(userRole,DataMapperOperation.delete);

      return registerRecord(userRole);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public UserRole save( UserRole userRole )
    {
      if(exists(userRole))
        return update(userRole);
        return create(userRole);
    }

  

    const String SqlUpdate = @"Update [UserRole] Set 
    UserId = @UserId,RoleId = @RoleId
       Where 
      UserRoleId = @UserRoleId";
    
    
    [Synchronized]
    public UserRole update(UserRole userRole)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);
                  
                      sqlCommand.Parameters.AddWithValue("@UserId", userRole.UserId);
                  
                      sqlCommand.Parameters.AddWithValue("@RoleId", userRole.RoleId);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(userRole,DataMapperOperation.update);

      return registerRecord(userRole);
    }

  
    }
    
  
    
    public partial class Module: DomainObject
    {
    
      protected int _moduleId;
    
      protected String _description;
    

    public Module(){}

    public Module(
    int 
            moduleId,String 
            description
    )
    {
    ModuleId = moduleId;
    Description = description;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.Module."
    
      + ModuleId.ToString()
    ;
    
    return uri;
    }

    

      public int ModuleId
      {
        
            get { return _moduleId;}
            set 
            { 
                _moduleId = value;
            }
          
      }
    

      public String Description
      {
        
            get { return _description;}
            set 
            { 
                _description = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      Module module = new Module();
      
      module.ModuleId = this.ModuleId;
      module.Description = this.Description;
      module.ActiveRecordId = this.ActiveRecordId; 
      return module;
    }

    
          // one to many relation
          private List<Permission> _permissions;

          public List<Permission> permissions
          {
          get { return _permissions;}
          set { _permissions = value; }
          }
        
    }
  

    public partial class ModuleDataMapper:TDataMapper<Module,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public ModuleDataMapper(){}
      public ModuleDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Module";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Module] (
    Description) Values (
    
      @Description);
    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Module create( Module module )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@Description", module.Description);
              module.ModuleId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(module.permissions != null 
            && module.permissions.Count > 0)
          {
            PermissionDataMapper dataMapper = new PermissionDataMapper(Database);
            
            foreach(Permission item in module.permissions)
              dataMapper.create(item);
          }
        
      
      raiseAffected(module,DataMapperOperation.create);

      return registerRecord(module);
    }

  

    private const String SqlSelectAll = @"Select
    ModuleId,Description 
    From [Module] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    ModuleId,Description
     From [Module]
    
       Where 
      ModuleId = @ModuleId
    ";

    public Module findByPrimaryKey(
    int moduleId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@ModuleId", moduleId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Module not found, search by primary key");
 

    }


    public bool exists(Module module)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@ModuleId", module.ModuleId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Module].[ModuleId] = @CheckInModuleId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Module _Module = (Module)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInModuleId", _Module.ModuleId);
      

      return sqlCommand;
    }

  

    protected override Module doLoad(IDataReader dataReader)
    {
    Module module = new Module();

    module.ModuleId = dataReader.GetInt32(0);
            module.Description = dataReader.GetString(1);
            

    
    
    return registerRecord(module);
    }


    protected override Module doLoad(Hashtable hashtable)
    {
      Module module = new Module();

      
        
        if(hashtable.ContainsKey("ModuleId"))
            module.ModuleId = ( int)hashtable["ModuleId"];
      
        
        if(hashtable.ContainsKey("Description"))
            module.Description = ( String)hashtable["Description"];
      

      return module;
    }


    protected override List<Module> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Module> resultList = new List<Module>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Module item = new Module();
              
              
                    item.ModuleId = ( int)dataRow["ModuleId"] ;
                  
                    item.Description = ( String)dataRow["Description"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [Module]
    
      Where
      ModuleId = @ModuleId";
    [Synchronized]
    public Module remove(Module module)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@ModuleId", module.ModuleId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(module,DataMapperOperation.delete);

      return registerRecord(module);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Module save( Module module )
    {
      if(exists(module))
        return update(module);
        return create(module);
    }

  

    const String SqlUpdate = @"Update [Module] Set 
    Description = @Description
       Where 
      ModuleId = @ModuleId";
    
    [TransactionRequired]
    [Synchronized]
    public Module update(Module module)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@ModuleId", module.ModuleId);
                  
                      sqlCommand.Parameters.AddWithValue("@Description", module.Description);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(module.permissions != null
          && module.permissions.Count > 0)
          {
          PermissionDataMapper dataMapper = new PermissionDataMapper(Database);

          foreach(Permission item in module.permissions)
            dataMapper.save(item);
          }
        

      raiseAffected(module,DataMapperOperation.update);

      return registerRecord(module);
    }

  
    }
    
  
    
    public partial class Permission: DomainObject
    {
    
      protected int _permissionId;
    
      protected String _description;
    
      protected String _code;
    

      // parent tables
      protected Module _parentModule
        = new Module()
      ;
    

    public Permission(){}

    public Permission(
    int 
            permissionId,int 
            moduleId,String 
            description,String 
            code
    )
    {
    PermissionId = permissionId;
    ModuleId = moduleId;
    Description = description;
    Code = code;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.Permission."
    
      + PermissionId.ToString()
    ;
    
    return uri;
    }

    

      public int PermissionId
      {
        
            get { return _permissionId;}
            set 
            { 
                _permissionId = value;
            }
          
      }
    

      public int ModuleId
      {
        
            get
            {
            
                  if(_parentModule != null)
                    return _parentModule.ModuleId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentModule == null)
                        _parentModule = new Module();

                      _parentModule.ModuleId = value;
                    
            }
          
      }
    

      public String Description
      {
        
            get { return _description;}
            set 
            { 
                _description = value;
            }
          
      }
    

      public String Code
      {
        
            get { return _code;}
            set 
            { 
                _code = value;
            }
          
      }
    

      public Module ParentModule
      {
      get { return _parentModule;}
      set { _parentModule = value; }
      }
    


    public override DomainObject extractSingleObject()
    {
      Permission permission = new Permission();
      
      permission.PermissionId = this.PermissionId;
      permission.ModuleId = this.ModuleId;
      permission.Description = this.Description;
      permission.Code = this.Code;
      permission.ActiveRecordId = this.ActiveRecordId; 
      return permission;
    }

    
          // one to many relation
          private List<PermissionAssignment> _permissionAssignments;

          public List<PermissionAssignment> permissionAssignments
          {
          get { return _permissionAssignments;}
          set { _permissionAssignments = value; }
          }
        
    }
  

    public partial class PermissionDataMapper:TDataMapper<Permission,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public PermissionDataMapper(){}
      public PermissionDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Permission";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Permission] (
    ModuleId,Description,Code) Values (
    
      @ModuleId,
      @Description,
      @Code);
    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Permission create( Permission permission )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@ModuleId", permission.ModuleId);
              
                if(permission.Description != null)
                  sqlCommand.Parameters.AddWithValue("@Description", permission.Description);
                else
                  sqlCommand.Parameters.AddWithValue("@Description", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@Code", permission.Code);
              permission.PermissionId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(permission.permissionAssignments != null 
            && permission.permissionAssignments.Count > 0)
          {
            PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);
            
            foreach(PermissionAssignment item in permission.permissionAssignments)
              dataMapper.create(item);
          }
        
      
      raiseAffected(permission,DataMapperOperation.create);

      return registerRecord(permission);
    }

  

    private const String SqlSelectAll = @"Select
    PermissionId,ModuleId,Description,Code 
    From [Permission] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    PermissionId,ModuleId,Description,Code
     From [Permission]
    
       Where 
      PermissionId = @PermissionId
    ";

    public Permission findByPrimaryKey(
    int permissionId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@PermissionId", permissionId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Permission not found, search by primary key");
 

    }


    public bool exists(Permission permission)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@PermissionId", permission.PermissionId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Permission].[PermissionId] = @CheckInPermissionId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Permission _Permission = (Permission)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInPermissionId", _Permission.PermissionId);
      

      return sqlCommand;
    }

  

    protected override Permission doLoad(IDataReader dataReader)
    {
    Permission permission = new Permission();

    permission.PermissionId = dataReader.GetInt32(0);
            permission.ModuleId = dataReader.GetInt32(1);
            
          if(!dataReader.IsDBNull(2))        
          permission.Description = dataReader.GetString(2);
            permission.Code = dataReader.GetString(3);
            

    
    
    return registerRecord(permission);
    }


    protected override Permission doLoad(Hashtable hashtable)
    {
      Permission permission = new Permission();

      
        
        if(hashtable.ContainsKey("PermissionId"))
            permission.PermissionId = ( int)hashtable["PermissionId"];
      
        
        if(hashtable.ContainsKey("ModuleId"))
            permission.ModuleId = ( int)hashtable["ModuleId"];
      
        
        if(hashtable.ContainsKey("Description"))
            permission.Description = ( String)hashtable["Description"];
      
        
        if(hashtable.ContainsKey("Code"))
            permission.Code = ( String)hashtable["Code"];
      

      return permission;
    }


    protected override List<Permission> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Permission> resultList = new List<Permission>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Permission item = new Permission();
              
              
                    item.PermissionId = ( int)dataRow["PermissionId"] ;
                  
                    item.ModuleId = ( int)dataRow["ModuleId"] ;
                  
                    if(!dataRow.IsNull("Description") &&  !(dataRow["Description"] is DBNull) )
                      item.Description = ( String)dataRow["Description"] ;
                  
                    item.Code = ( String)dataRow["Code"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [Permission]
    
      Where
      PermissionId = @PermissionId";
    [Synchronized]
    public Permission remove(Permission permission)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@PermissionId", permission.PermissionId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(permission,DataMapperOperation.delete);

      return registerRecord(permission);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Permission save( Permission permission )
    {
      if(exists(permission))
        return update(permission);
        return create(permission);
    }

  

    const String SqlUpdate = @"Update [Permission] Set 
    ModuleId = @ModuleId,Description = @Description,Code = @Code
       Where 
      PermissionId = @PermissionId";
    
    [TransactionRequired]
    [Synchronized]
    public Permission update(Permission permission)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@PermissionId", permission.PermissionId);
                  
                      sqlCommand.Parameters.AddWithValue("@ModuleId", permission.ModuleId);
                  
                    
                    if(permission.Description != null)
                      sqlCommand.Parameters.AddWithValue("@Description", permission.Description);
                    else
                      sqlCommand.Parameters.AddWithValue("@Description", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@Code", permission.Code);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(permission.permissionAssignments != null
          && permission.permissionAssignments.Count > 0)
          {
          PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);

          foreach(PermissionAssignment item in permission.permissionAssignments)
            dataMapper.save(item);
          }
        

      raiseAffected(permission,DataMapperOperation.update);

      return registerRecord(permission);
    }

  
    }
    
  
    
    public partial class PermissionAssignment: DomainObject
    {
    
      protected int _permissionAssignmentId;
    

      // parent tables
      protected Permission _parentPermission
        = new Permission()
      ;
    

      // parent tables
      protected Role _parentRole
        = new Role()
      ;
    

    public PermissionAssignment(){}

    public PermissionAssignment(
    int 
            permissionAssignmentId,int 
            permissionId,int 
            roleId
    )
    {
    PermissionAssignmentId = permissionAssignmentId;
    PermissionId = permissionId;
    RoleId = roleId;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.PermissionAssignment."
    
      + PermissionAssignmentId.ToString()
    ;
    
    return uri;
    }

    

      public int PermissionAssignmentId
      {
        
            get { return _permissionAssignmentId;}
            set 
            { 
                _permissionAssignmentId = value;
            }
          
      }
    

      public int PermissionId
      {
        
            get
            {
            
                  if(_parentPermission != null)
                    return _parentPermission.PermissionId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentPermission == null)
                        _parentPermission = new Permission();

                      _parentPermission.PermissionId = value;
                    
            }
          
      }
    

      public int RoleId
      {
        
            get
            {
            
                  if(_parentRole != null)
                    return _parentRole.RoleId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentRole == null)
                        _parentRole = new Role();

                      _parentRole.RoleId = value;
                    
            }
          
      }
    

      public Permission ParentPermission
      {
      get { return _parentPermission;}
      set { _parentPermission = value; }
      }
    

      public Role ParentRole
      {
      get { return _parentRole;}
      set { _parentRole = value; }
      }
    


    public override DomainObject extractSingleObject()
    {
      PermissionAssignment permissionAssignment = new PermissionAssignment();
      
      permissionAssignment.PermissionAssignmentId = this.PermissionAssignmentId;
      permissionAssignment.PermissionId = this.PermissionId;
      permissionAssignment.RoleId = this.RoleId;
      permissionAssignment.ActiveRecordId = this.ActiveRecordId; 
      return permissionAssignment;
    }

    
    }
  

    public partial class PermissionAssignmentDataMapper:TDataMapper<PermissionAssignment,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public PermissionAssignmentDataMapper(){}
      public PermissionAssignmentDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "PermissionAssignment";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [PermissionAssignment] (
    PermissionId,RoleId) Values (
    
      @PermissionId,
      @RoleId);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override PermissionAssignment create( PermissionAssignment permissionAssignment )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@PermissionId", permissionAssignment.PermissionId);
              
                  sqlCommand.Parameters.AddWithValue("@RoleId", permissionAssignment.RoleId);
              permissionAssignment.PermissionAssignmentId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(permissionAssignment,DataMapperOperation.create);

      return registerRecord(permissionAssignment);
    }

  

    private const String SqlSelectAll = @"Select
    PermissionAssignmentId,PermissionId,RoleId 
    From [PermissionAssignment] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    PermissionAssignmentId,PermissionId,RoleId
     From [PermissionAssignment]
    
       Where 
      PermissionAssignmentId = @PermissionAssignmentId
    ";

    public PermissionAssignment findByPrimaryKey(
    int permissionAssignmentId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@PermissionAssignmentId", permissionAssignmentId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("PermissionAssignment not found, search by primary key");
 

    }


    public bool exists(PermissionAssignment permissionAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@PermissionAssignmentId", permissionAssignment.PermissionAssignmentId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [PermissionAssignment].[PermissionAssignmentId] = @CheckInPermissionAssignmentId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      PermissionAssignment _PermissionAssignment = (PermissionAssignment)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInPermissionAssignmentId", _PermissionAssignment.PermissionAssignmentId);
      

      return sqlCommand;
    }

  

    protected override PermissionAssignment doLoad(IDataReader dataReader)
    {
    PermissionAssignment permissionAssignment = new PermissionAssignment();

    permissionAssignment.PermissionAssignmentId = dataReader.GetInt32(0);
            permissionAssignment.PermissionId = dataReader.GetInt32(1);
            permissionAssignment.RoleId = dataReader.GetInt32(2);
            

    
    
    return registerRecord(permissionAssignment);
    }


    protected override PermissionAssignment doLoad(Hashtable hashtable)
    {
      PermissionAssignment permissionAssignment = new PermissionAssignment();

      
        
        if(hashtable.ContainsKey("PermissionAssignmentId"))
            permissionAssignment.PermissionAssignmentId = ( int)hashtable["PermissionAssignmentId"];
      
        
        if(hashtable.ContainsKey("PermissionId"))
            permissionAssignment.PermissionId = ( int)hashtable["PermissionId"];
      
        
        if(hashtable.ContainsKey("RoleId"))
            permissionAssignment.RoleId = ( int)hashtable["RoleId"];
      

      return permissionAssignment;
    }


    protected override List<PermissionAssignment> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<PermissionAssignment> resultList = new List<PermissionAssignment>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              PermissionAssignment item = new PermissionAssignment();
              
              
                    item.PermissionAssignmentId = ( int)dataRow["PermissionAssignmentId"] ;
                  
                    item.PermissionId = ( int)dataRow["PermissionId"] ;
                  
                    item.RoleId = ( int)dataRow["RoleId"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [PermissionAssignment]
    
      Where
      PermissionAssignmentId = @PermissionAssignmentId";
    [Synchronized]
    public PermissionAssignment remove(PermissionAssignment permissionAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@PermissionAssignmentId", permissionAssignment.PermissionAssignmentId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(permissionAssignment,DataMapperOperation.delete);

      return registerRecord(permissionAssignment);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public PermissionAssignment save( PermissionAssignment permissionAssignment )
    {
      if(exists(permissionAssignment))
        return update(permissionAssignment);
        return create(permissionAssignment);
    }

  

    const String SqlUpdate = @"Update [PermissionAssignment] Set 
    PermissionId = @PermissionId,RoleId = @RoleId
       Where 
      PermissionAssignmentId = @PermissionAssignmentId";
    
    
    [Synchronized]
    public PermissionAssignment update(PermissionAssignment permissionAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@PermissionAssignmentId", permissionAssignment.PermissionAssignmentId);
                  
                      sqlCommand.Parameters.AddWithValue("@PermissionId", permissionAssignment.PermissionId);
                  
                      sqlCommand.Parameters.AddWithValue("@RoleId", permissionAssignment.RoleId);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(permissionAssignment,DataMapperOperation.update);

      return registerRecord(permissionAssignment);
    }

  
    }
    
  
    
    public partial class Role: DomainObject
    {
    
      protected int _roleId;
    
      protected String _name;
    

    public Role(){}

    public Role(
    int 
            roleId,String 
            name
    )
    {
    RoleId = roleId;
    Name = name;
    
    }

    public override String  getUri()
    {

    String uri = "TractInc.Role."
    
      + RoleId.ToString()
    ;
    
    return uri;
    }

    

      public int RoleId
      {
        
            get { return _roleId;}
            set 
            { 
                _roleId = value;
            }
          
      }
    

      public String Name
      {
        
            get { return _name;}
            set 
            { 
                _name = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      Role role = new Role();
      
      role.RoleId = this.RoleId;
      role.Name = this.Name;
      role.ActiveRecordId = this.ActiveRecordId; 
      return role;
    }

    
          // one to many relation
          private List<UserRole> _userRoles;

          public List<UserRole> userRoles
          {
          get { return _userRoles;}
          set { _userRoles = value; }
          }
        
          // one to many relation
          private List<PermissionAssignment> _permissionAssignments;

          public List<PermissionAssignment> permissionAssignments
          {
          get { return _permissionAssignments;}
          set { _permissionAssignments = value; }
          }
        
    }
  

    public partial class RoleDataMapper:TDataMapper<Role,SqlConnection,TractIncDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public RoleDataMapper(){}
      public RoleDataMapper(TractIncDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Role";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Role] (
    Name) Values (
    
      @Name);
    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Role create( Role role )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@Name", role.Name);
              role.RoleId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(role.userRoles != null 
            && role.userRoles.Count > 0)
          {
            UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);
            
            foreach(UserRole item in role.userRoles)
              dataMapper.create(item);
          }
        
          
          if(role.permissionAssignments != null 
            && role.permissionAssignments.Count > 0)
          {
            PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);
            
            foreach(PermissionAssignment item in role.permissionAssignments)
              dataMapper.create(item);
          }
        
      
      raiseAffected(role,DataMapperOperation.create);

      return registerRecord(role);
    }

  

    private const String SqlSelectAll = @"Select
    RoleId,Name 
    From [Role] ";

    public QueryResult findAll(Hashtable options)
    {
    QueryOptions queryOptions = new QueryOptions(options);
    String queryId = Guid.NewGuid().ToString();

    if(queryOptions.IsPaged || queryOptions.IsMonitored)
    registerCollection(SqlSelectAll,queryId,queryOptions);

    if(queryOptions.IsPaged)
    return getQueryPage(queryId,1);

    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
    {
    return new QueryResult(queryId, queryOptions.IsMonitored, fill(sqlCommand,queryOptions.Offset,queryOptions.Limit));

    }

    }
    }
  

    private const String SqlSelectByPk = @"Select
    RoleId,Name
     From [Role]
    
       Where 
      RoleId = @RoleId
    ";

    public Role findByPrimaryKey(
    int roleId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@RoleId", roleId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Role not found, search by primary key");
 

    }


    public bool exists(Role role)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@RoleId", role.RoleId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Role].[RoleId] = @CheckInRoleId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Role _Role = (Role)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInRoleId", _Role.RoleId);
      

      return sqlCommand;
    }

  

    protected override Role doLoad(IDataReader dataReader)
    {
    Role role = new Role();

    role.RoleId = dataReader.GetInt32(0);
            role.Name = dataReader.GetString(1);
            

    
    
    return registerRecord(role);
    }


    protected override Role doLoad(Hashtable hashtable)
    {
      Role role = new Role();

      
        
        if(hashtable.ContainsKey("RoleId"))
            role.RoleId = ( int)hashtable["RoleId"];
      
        
        if(hashtable.ContainsKey("Name"))
            role.Name = ( String)hashtable["Name"];
      

      return role;
    }


    protected override List<Role> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Role> resultList = new List<Role>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Role item = new Role();
              
              
                    item.RoleId = ( int)dataRow["RoleId"] ;
                  
                    item.Name = ( String)dataRow["Name"] ;
                  
               // updated
               
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [Role]
    
      Where
      RoleId = @RoleId";
    [Synchronized]
    public Role remove(Role role)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@RoleId", role.RoleId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(role,DataMapperOperation.delete);

      return registerRecord(role);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Role save( Role role )
    {
      if(exists(role))
        return update(role);
        return create(role);
    }

  

    const String SqlUpdate = @"Update [Role] Set 
    Name = @Name
       Where 
      RoleId = @RoleId";
    
    [TransactionRequired]
    [Synchronized]
    public Role update(Role role)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@RoleId", role.RoleId);
                  
                      sqlCommand.Parameters.AddWithValue("@Name", role.Name);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(role.userRoles != null
          && role.userRoles.Count > 0)
          {
          UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);

          foreach(UserRole item in role.userRoles)
            dataMapper.save(item);
          }
        
          if(role.permissionAssignments != null
          && role.permissionAssignments.Count > 0)
          {
          PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);

          foreach(PermissionAssignment item in role.permissionAssignments)
            dataMapper.save(item);
          }
        

      raiseAffected(role,DataMapperOperation.update);

      return registerRecord(role);
    }

  
    }
    
  
      }
    