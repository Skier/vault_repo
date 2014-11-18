
      namespace TractInc.Expense.Domain
      {
      using System;
      
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using Weborb.Data.Management;
  
    
      using System.Data.SqlClient;
    
    public partial class TractIncRAIDDb:TDatabase<SqlConnection,SqlTransaction,SqlCommand>
    {
      public TractIncRAIDDb()
      {
        InitConnectionString("TractIncRAID");
      }

    

        public AfeStatus create(AfeStatus afeStatus)
        {
          AfeStatusDataMapper dataMapper = new AfeStatusDataMapper(this);
          
          return dataMapper.create(afeStatus);
        }

        public AfeStatus update(AfeStatus afeStatus)
        {
          AfeStatusDataMapper dataMapper = new AfeStatusDataMapper(this);

          return dataMapper.update(afeStatus);
        }

        public AfeStatus remove(AfeStatus afeStatus)
        {
          AfeStatusDataMapper dataMapper = new AfeStatusDataMapper(this);

          return dataMapper.remove(afeStatus);
        }
      

        public Client create(Client client)
        {
          ClientDataMapper dataMapper = new ClientDataMapper(this);
          
          return dataMapper.create(client);
        }

        public Client update(Client client)
        {
          ClientDataMapper dataMapper = new ClientDataMapper(this);

          return dataMapper.update(client);
        }

        public Client remove(Client client)
        {
          ClientDataMapper dataMapper = new ClientDataMapper(this);

          return dataMapper.remove(client);
        }
      

        public AssetType create(AssetType assetType)
        {
          AssetTypeDataMapper dataMapper = new AssetTypeDataMapper(this);
          
          return dataMapper.create(assetType);
        }

        public AssetType update(AssetType assetType)
        {
          AssetTypeDataMapper dataMapper = new AssetTypeDataMapper(this);

          return dataMapper.update(assetType);
        }

        public AssetType remove(AssetType assetType)
        {
          AssetTypeDataMapper dataMapper = new AssetTypeDataMapper(this);

          return dataMapper.remove(assetType);
        }
      

        public Asset create(Asset asset)
        {
          AssetDataMapper dataMapper = new AssetDataMapper(this);
          
          return dataMapper.create(asset);
        }

        public Asset update(Asset asset)
        {
          AssetDataMapper dataMapper = new AssetDataMapper(this);

          return dataMapper.update(asset);
        }

        public Asset remove(Asset asset)
        {
          AssetDataMapper dataMapper = new AssetDataMapper(this);

          return dataMapper.remove(asset);
        }
      

        public Afe create(Afe afe)
        {
          AfeDataMapper dataMapper = new AfeDataMapper(this);
          
          return dataMapper.create(afe);
        }

        public Afe update(Afe afe)
        {
          AfeDataMapper dataMapper = new AfeDataMapper(this);

          return dataMapper.update(afe);
        }

        public Afe remove(Afe afe)
        {
          AfeDataMapper dataMapper = new AfeDataMapper(this);

          return dataMapper.remove(afe);
        }
      

        public BillStatus create(BillStatus billStatus)
        {
          BillStatusDataMapper dataMapper = new BillStatusDataMapper(this);
          
          return dataMapper.create(billStatus);
        }

        public BillStatus update(BillStatus billStatus)
        {
          BillStatusDataMapper dataMapper = new BillStatusDataMapper(this);

          return dataMapper.update(billStatus);
        }

        public BillStatus remove(BillStatus billStatus)
        {
          BillStatusDataMapper dataMapper = new BillStatusDataMapper(this);

          return dataMapper.remove(billStatus);
        }
      

        public BillItemStatus create(BillItemStatus billItemStatus)
        {
          BillItemStatusDataMapper dataMapper = new BillItemStatusDataMapper(this);
          
          return dataMapper.create(billItemStatus);
        }

        public BillItemStatus update(BillItemStatus billItemStatus)
        {
          BillItemStatusDataMapper dataMapper = new BillItemStatusDataMapper(this);

          return dataMapper.update(billItemStatus);
        }

        public BillItemStatus remove(BillItemStatus billItemStatus)
        {
          BillItemStatusDataMapper dataMapper = new BillItemStatusDataMapper(this);

          return dataMapper.remove(billItemStatus);
        }
      

        public InvoiceItemType create(InvoiceItemType invoiceItemType)
        {
          InvoiceItemTypeDataMapper dataMapper = new InvoiceItemTypeDataMapper(this);
          
          return dataMapper.create(invoiceItemType);
        }

        public InvoiceItemType update(InvoiceItemType invoiceItemType)
        {
          InvoiceItemTypeDataMapper dataMapper = new InvoiceItemTypeDataMapper(this);

          return dataMapper.update(invoiceItemType);
        }

        public InvoiceItemType remove(InvoiceItemType invoiceItemType)
        {
          InvoiceItemTypeDataMapper dataMapper = new InvoiceItemTypeDataMapper(this);

          return dataMapper.remove(invoiceItemType);
        }
      

        public Bill create(Bill bill)
        {
          BillDataMapper dataMapper = new BillDataMapper(this);
          
          return dataMapper.create(bill);
        }

        public Bill update(Bill bill)
        {
          BillDataMapper dataMapper = new BillDataMapper(this);

          return dataMapper.update(bill);
        }

        public Bill remove(Bill bill)
        {
          BillDataMapper dataMapper = new BillDataMapper(this);

          return dataMapper.remove(bill);
        }
      

        public SubAfeStatus create(SubAfeStatus subAfeStatus)
        {
          SubAfeStatusDataMapper dataMapper = new SubAfeStatusDataMapper(this);
          
          return dataMapper.create(subAfeStatus);
        }

        public SubAfeStatus update(SubAfeStatus subAfeStatus)
        {
          SubAfeStatusDataMapper dataMapper = new SubAfeStatusDataMapper(this);

          return dataMapper.update(subAfeStatus);
        }

        public SubAfeStatus remove(SubAfeStatus subAfeStatus)
        {
          SubAfeStatusDataMapper dataMapper = new SubAfeStatusDataMapper(this);

          return dataMapper.remove(subAfeStatus);
        }
      

        public BillItemType create(BillItemType billItemType)
        {
          BillItemTypeDataMapper dataMapper = new BillItemTypeDataMapper(this);
          
          return dataMapper.create(billItemType);
        }

        public BillItemType update(BillItemType billItemType)
        {
          BillItemTypeDataMapper dataMapper = new BillItemTypeDataMapper(this);

          return dataMapper.update(billItemType);
        }

        public BillItemType remove(BillItemType billItemType)
        {
          BillItemTypeDataMapper dataMapper = new BillItemTypeDataMapper(this);

          return dataMapper.remove(billItemType);
        }
      

        public SubAfe create(SubAfe subAfe)
        {
          SubAfeDataMapper dataMapper = new SubAfeDataMapper(this);
          
          return dataMapper.create(subAfe);
        }

        public SubAfe update(SubAfe subAfe)
        {
          SubAfeDataMapper dataMapper = new SubAfeDataMapper(this);

          return dataMapper.update(subAfe);
        }

        public SubAfe remove(SubAfe subAfe)
        {
          SubAfeDataMapper dataMapper = new SubAfeDataMapper(this);

          return dataMapper.remove(subAfe);
        }
      

        public InvoiceStatus create(InvoiceStatus invoiceStatus)
        {
          InvoiceStatusDataMapper dataMapper = new InvoiceStatusDataMapper(this);
          
          return dataMapper.create(invoiceStatus);
        }

        public InvoiceStatus update(InvoiceStatus invoiceStatus)
        {
          InvoiceStatusDataMapper dataMapper = new InvoiceStatusDataMapper(this);

          return dataMapper.update(invoiceStatus);
        }

        public InvoiceStatus remove(InvoiceStatus invoiceStatus)
        {
          InvoiceStatusDataMapper dataMapper = new InvoiceStatusDataMapper(this);

          return dataMapper.remove(invoiceStatus);
        }
      

        public InvoiceItemStatus create(InvoiceItemStatus invoiceItemStatus)
        {
          InvoiceItemStatusDataMapper dataMapper = new InvoiceItemStatusDataMapper(this);
          
          return dataMapper.create(invoiceItemStatus);
        }

        public InvoiceItemStatus update(InvoiceItemStatus invoiceItemStatus)
        {
          InvoiceItemStatusDataMapper dataMapper = new InvoiceItemStatusDataMapper(this);

          return dataMapper.update(invoiceItemStatus);
        }

        public InvoiceItemStatus remove(InvoiceItemStatus invoiceItemStatus)
        {
          InvoiceItemStatusDataMapper dataMapper = new InvoiceItemStatusDataMapper(this);

          return dataMapper.remove(invoiceItemStatus);
        }
      

        public Invoice create(Invoice invoice)
        {
          InvoiceDataMapper dataMapper = new InvoiceDataMapper(this);
          
          return dataMapper.create(invoice);
        }

        public Invoice update(Invoice invoice)
        {
          InvoiceDataMapper dataMapper = new InvoiceDataMapper(this);

          return dataMapper.update(invoice);
        }

        public Invoice remove(Invoice invoice)
        {
          InvoiceDataMapper dataMapper = new InvoiceDataMapper(this);

          return dataMapper.remove(invoice);
        }
      

        public AssetAssignment create(AssetAssignment assetAssignment)
        {
          AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(this);
          
          return dataMapper.create(assetAssignment);
        }

        public AssetAssignment update(AssetAssignment assetAssignment)
        {
          AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(this);

          return dataMapper.update(assetAssignment);
        }

        public AssetAssignment remove(AssetAssignment assetAssignment)
        {
          AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(this);

          return dataMapper.remove(assetAssignment);
        }
      

        public BillItem create(BillItem billItem)
        {
          BillItemDataMapper dataMapper = new BillItemDataMapper(this);
          
          return dataMapper.create(billItem);
        }

        public BillItem update(BillItem billItem)
        {
          BillItemDataMapper dataMapper = new BillItemDataMapper(this);

          return dataMapper.update(billItem);
        }

        public BillItem remove(BillItem billItem)
        {
          BillItemDataMapper dataMapper = new BillItemDataMapper(this);

          return dataMapper.remove(billItem);
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
      

        public Note create(Note note)
        {
          NoteDataMapper dataMapper = new NoteDataMapper(this);
          
          return dataMapper.create(note);
        }

        public Note update(Note note)
        {
          NoteDataMapper dataMapper = new NoteDataMapper(this);

          return dataMapper.update(note);
        }

        public Note remove(Note note)
        {
          NoteDataMapper dataMapper = new NoteDataMapper(this);

          return dataMapper.remove(note);
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
      

        public RateByAssignment create(RateByAssignment rateByAssignment)
        {
          RateByAssignmentDataMapper dataMapper = new RateByAssignmentDataMapper(this);
          
          return dataMapper.create(rateByAssignment);
        }

        public RateByAssignment update(RateByAssignment rateByAssignment)
        {
          RateByAssignmentDataMapper dataMapper = new RateByAssignmentDataMapper(this);

          return dataMapper.update(rateByAssignment);
        }

        public RateByAssignment remove(RateByAssignment rateByAssignment)
        {
          RateByAssignmentDataMapper dataMapper = new RateByAssignmentDataMapper(this);

          return dataMapper.remove(rateByAssignment);
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
      

        public BillItemAttachment create(BillItemAttachment billItemAttachment)
        {
          BillItemAttachmentDataMapper dataMapper = new BillItemAttachmentDataMapper(this);
          
          return dataMapper.create(billItemAttachment);
        }

        public BillItemAttachment update(BillItemAttachment billItemAttachment)
        {
          BillItemAttachmentDataMapper dataMapper = new BillItemAttachmentDataMapper(this);

          return dataMapper.update(billItemAttachment);
        }

        public BillItemAttachment remove(BillItemAttachment billItemAttachment)
        {
          BillItemAttachmentDataMapper dataMapper = new BillItemAttachmentDataMapper(this);

          return dataMapper.remove(billItemAttachment);
        }
      

        public InvoiceItem create(InvoiceItem invoiceItem)
        {
          InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(this);
          
          return dataMapper.create(invoiceItem);
        }

        public InvoiceItem update(InvoiceItem invoiceItem)
        {
          InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(this);

          return dataMapper.update(invoiceItem);
        }

        public InvoiceItem remove(InvoiceItem invoiceItem)
        {
          InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(this);

          return dataMapper.remove(invoiceItem);
        }
      

        public SyncLog create(SyncLog syncLog)
        {
          SyncLogDataMapper dataMapper = new SyncLogDataMapper(this);
          
          return dataMapper.create(syncLog);
        }

        public SyncLog update(SyncLog syncLog)
        {
          SyncLogDataMapper dataMapper = new SyncLogDataMapper(this);

          return dataMapper.update(syncLog);
        }

        public SyncLog remove(SyncLog syncLog)
        {
          SyncLogDataMapper dataMapper = new SyncLogDataMapper(this);

          return dataMapper.remove(syncLog);
        }
      

        public Message create(Message message)
        {
          MessageDataMapper dataMapper = new MessageDataMapper(this);
          
          return dataMapper.create(message);
        }

        public Message update(Message message)
        {
          MessageDataMapper dataMapper = new MessageDataMapper(this);

          return dataMapper.update(message);
        }

        public Message remove(Message message)
        {
          MessageDataMapper dataMapper = new MessageDataMapper(this);

          return dataMapper.remove(message);
        }
      

        public UserAsset create(UserAsset userAsset)
        {
          UserAssetDataMapper dataMapper = new UserAssetDataMapper(this);
          
          return dataMapper.create(userAsset);
        }

        public UserAsset update(UserAsset userAsset)
        {
          UserAssetDataMapper dataMapper = new UserAssetDataMapper(this);

          return dataMapper.update(userAsset);
        }

        public UserAsset remove(UserAsset userAsset)
        {
          UserAssetDataMapper dataMapper = new UserAssetDataMapper(this);

          return dataMapper.remove(userAsset);
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
      
    }
  
    
    public partial class AfeStatus: DomainObject
    {
    
      protected String _aFEStatus;
    

    public AfeStatus(){}

    public AfeStatus(
    String 
            aFEStatus
    )
    {
    
      this.AFEStatus = aFEStatus;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.AfeStatus."
    
      + AFEStatus.ToString()
    ;
    
    return uri;
    }

    

      public String AFEStatus
      {
        
            get { return _aFEStatus;}
            set 
            { 
                _aFEStatus = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      AfeStatus afeStatus = new AfeStatus();
      
      afeStatus.AFEStatus = this.AFEStatus;
      afeStatus.ActiveRecordId = this.ActiveRecordId; 
      return afeStatus;
    }

    
          // one to many relation
          private List<Afe> _relatedAfe;

          public List<Afe> relatedAfe
          {
          get { return _relatedAfe;}
          set { _relatedAfe = value; }
          }
          
          
          public Afe addRelatedAfeItem(
          Afe afe)
          {
            afe.RelatedAfeStatus = this;
            
            _relatedAfe.Add(afe);
            
            return afe;
          }
            
        
    }
  

    public abstract class _AfeStatusDataMapper:TDataMapper<AfeStatus,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _AfeStatusDataMapper(){}
      public _AfeStatusDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[AfeStatus]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[AfeStatus] (
    
      [AFEStatus]
      ) Values (
    
      @aFEStatus);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override AfeStatus create( AfeStatus afeStatus )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@aFEStatus", afeStatus.AFEStatus);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(afeStatus.relatedAfe != null 
            && afeStatus.relatedAfe.Count > 0)
          {
            AfeDataMapper dataMapper = new AfeDataMapper(Database);
            
            foreach(Afe item in afeStatus.relatedAfe)
              dataMapper.create(item);
          }
        
      
      raiseAffected(afeStatus,DataMapperOperation.create);

      return registerRecord(afeStatus);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [AFEStatus] 
     From [dbo].[AfeStatus]
    
       Where 
      
         [AFEStatus] = @aFEStatus
    ";

    public AfeStatus findByPrimaryKey(
    String aFEStatus
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@aFEStatus", aFEStatus);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("AfeStatus not found, search by primary key");
 

    }


    public bool exists(AfeStatus afeStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@AFEStatus", afeStatus.AFEStatus);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [AfeStatus].[AFEStatus] = @CheckInAFEStatus";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      AfeStatus _AfeStatus = (AfeStatus)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInAFEStatus", _AfeStatus.AFEStatus);
      

      return sqlCommand;
    }

  

    protected override AfeStatus doLoad(IDataReader dataReader)
    {
    AfeStatus afeStatus = new AfeStatus();

    afeStatus.AFEStatus = (String) dataReader.GetValue(0);
            

    
    
    return registerRecord(afeStatus);
    }


    protected override AfeStatus doLoad(Hashtable hashtable)
    {
      AfeStatus afeStatus = new AfeStatus();

      
        
        if(hashtable.ContainsKey("AFEStatus"))
        afeStatus.AFEStatus = ( String)hashtable["AFEStatus"];
          

      return afeStatus;
    }


    protected override List<AfeStatus> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<AfeStatus> resultList = new List<AfeStatus>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              AfeStatus item = new AfeStatus();
              
              
                    item.AFEStatus = ( String)dataRow["AFEStatus"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[AfeStatus]
    
      Where
      
        [AFEStatus] = @aFEStatus";
    [Synchronized]
    public override AfeStatus remove(AfeStatus afeStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@aFEStatus", afeStatus.AFEStatus);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(afeStatus,DataMapperOperation.delete);

      return registerRecord(afeStatus);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override AfeStatus save( AfeStatus afeStatus )
    {
      if(exists(afeStatus))
        return update(afeStatus);
        return create(afeStatus);
    }

  [TransactionRequired]
    [Synchronized]
    public override AfeStatus update(AfeStatus afeStatus)
    {
      
    
        return registerRecord(afeStatus);

    }

  
    }
    
  
    
    public partial class Client: DomainObject
    {
    
      protected int _clientId;
    
      protected String _clientName;
    
      protected String _clientAddress;
    
      protected bool _active;
    
      protected bool _deleted;
    

    public Client(){}

    public Client(
    int 
            clientId,String 
            clientName,String 
            clientAddress,bool 
            active,bool 
            deleted
    )
    {
    
      this.ClientId = clientId;
    
      this.ClientName = clientName;
    
      this.ClientAddress = clientAddress;
    
      this.Active = active;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Client."
    
      + ClientId.ToString()
    ;
    
    return uri;
    }

    

      public int ClientId
      {
        
            get { return _clientId;}
            set 
            { 
                _clientId = value;
            }
          
      }
    

      public String ClientName
      {
        
            get { return _clientName;}
            set 
            { 
                _clientName = value;
            }
          
      }
    

      public String ClientAddress
      {
        
            get { return _clientAddress;}
            set 
            { 
                _clientAddress = value;
            }
          
      }
    

      public bool Active
      {
        
            get { return _active;}
            set 
            { 
                _active = value;
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      Client client = new Client();
      
      client.ClientId = this.ClientId;
      client.ClientName = this.ClientName;
      client.ClientAddress = this.ClientAddress;
      client.Active = this.Active;
      client.Deleted = this.Deleted;
      client.ActiveRecordId = this.ActiveRecordId; 
      return client;
    }

    
          // one to many relation
          private List<Afe> _relatedAfe;

          public List<Afe> relatedAfe
          {
          get { return _relatedAfe;}
          set { _relatedAfe = value; }
          }
          
          
          public Afe addRelatedAfeItem(
          Afe afe)
          {
            afe.RelatedClient = this;
            
            _relatedAfe.Add(afe);
            
            return afe;
          }
            
        
          // one to many relation
          private List<Invoice> _relatedInvoice;

          public List<Invoice> relatedInvoice
          {
          get { return _relatedInvoice;}
          set { _relatedInvoice = value; }
          }
          
          
          public Invoice addRelatedInvoiceItem(
          Invoice invoice)
          {
            invoice.RelatedClient = this;
            
            _relatedInvoice.Add(invoice);
            
            return invoice;
          }
            
        
    }
  

    public abstract class _ClientDataMapper:TDataMapper<Client,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _ClientDataMapper(){}
      public _ClientDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Client]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Client] (
    
      [ClientName]
      ,
      [ClientAddress]
      ,
      [Active]
      ,
      [Deleted]
      ) Values (
    
      @clientName,
      @clientAddress,
      @active,
      @deleted);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Client create( Client client )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@clientName", client.ClientName);
              
                  sqlCommand.Parameters.AddWithValue("@clientAddress", client.ClientAddress);
              
                  sqlCommand.Parameters.AddWithValue("@active", client.Active);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", client.Deleted);
              client.ClientId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(client.relatedAfe != null 
            && client.relatedAfe.Count > 0)
          {
            AfeDataMapper dataMapper = new AfeDataMapper(Database);
            
            foreach(Afe item in client.relatedAfe)
              dataMapper.create(item);
          }
        
          
          if(client.relatedInvoice != null 
            && client.relatedInvoice.Count > 0)
          {
            InvoiceDataMapper dataMapper = new InvoiceDataMapper(Database);
            
            foreach(Invoice item in client.relatedInvoice)
              dataMapper.create(item);
          }
        
      
      raiseAffected(client,DataMapperOperation.create);

      return registerRecord(client);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [ClientId] ,
      [ClientName] ,
      [ClientAddress] ,
      [Active] ,
      [Deleted] 
     From [dbo].[Client]
    
       Where 
      
         [ClientId] = @clientId
    ";

    public Client findByPrimaryKey(
    int clientId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@clientId", clientId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Client not found, search by primary key");
 

    }


    public bool exists(Client client)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@ClientId", client.ClientId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Client].[ClientId] = @CheckInClientId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Client _Client = (Client)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInClientId", _Client.ClientId);
      

      return sqlCommand;
    }

  

    protected override Client doLoad(IDataReader dataReader)
    {
    Client client = new Client();

    client.ClientId = (int) dataReader.GetValue(0);
            client.ClientName = (String) dataReader.GetValue(1);
            client.ClientAddress = (String) dataReader.GetValue(2);
            client.Active = (bool) dataReader.GetValue(3);
            client.Deleted = (bool) dataReader.GetValue(4);
            

    
    
    return registerRecord(client);
    }


    protected override Client doLoad(Hashtable hashtable)
    {
      Client client = new Client();

      
        
        if(hashtable.ContainsKey("ClientId"))
        client.ClientId = ( int)hashtable["ClientId"];
          
        
        if(hashtable.ContainsKey("ClientName"))
        client.ClientName = ( String)hashtable["ClientName"];
          
        
        if(hashtable.ContainsKey("ClientAddress"))
        client.ClientAddress = ( String)hashtable["ClientAddress"];
          
        
        if(hashtable.ContainsKey("Active"))
        client.Active = ( bool)hashtable["Active"];
          
        
        if(hashtable.ContainsKey("Deleted"))
        client.Deleted = ( bool)hashtable["Deleted"];
          

      return client;
    }


    protected override List<Client> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Client> resultList = new List<Client>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Client item = new Client();
              
              
                    item.ClientId = ( int)dataRow["ClientId"] ;
                  
                    item.ClientName = ( String)dataRow["ClientName"] ;
                  
                    item.ClientAddress = ( String)dataRow["ClientAddress"] ;
                  
                    item.Active = ( bool)dataRow["Active"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Client]
    
      Where
      
        [ClientId] = @clientId";
    [Synchronized]
    public override Client remove(Client client)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@clientId", client.ClientId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(client,DataMapperOperation.delete);

      return registerRecord(client);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Client save( Client client )
    {
      if(exists(client))
        return update(client);
        return create(client);
    }

  
      const String SqlUpdate = @"Update [dbo].[Client] Set
      
        [ClientName] = @clientName,
        [ClientAddress] = @clientAddress,
        [Active] = @active,
        [Deleted] = @deleted
        Where
        
          [ClientId] = @clientId";
    [TransactionRequired]
    [Synchronized]
    public override Client update(Client client)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@clientId", client.ClientId);
            
              sqlCommand.Parameters.AddWithValue("@clientName", client.ClientName);
            
              sqlCommand.Parameters.AddWithValue("@clientAddress", client.ClientAddress);
            
              sqlCommand.Parameters.AddWithValue("@active", client.Active);
            
              sqlCommand.Parameters.AddWithValue("@deleted", client.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(client.relatedAfe != null
              && client.relatedAfe.Count > 0)
              {
              AfeDataMapper dataMapper = new AfeDataMapper(Database);

              foreach(Afe item in client.relatedAfe)
              dataMapper.save(item);
              }
            
              if(client.relatedInvoice != null
              && client.relatedInvoice.Count > 0)
              {
              InvoiceDataMapper dataMapper = new InvoiceDataMapper(Database);

              foreach(Invoice item in client.relatedInvoice)
              dataMapper.save(item);
              }
            

        raiseAffected(client,DataMapperOperation.update);
        
    
        return registerRecord(client);

    }

  
    }
    
  
    
    public partial class AssetType: DomainObject
    {
    
      protected String __Type;
    

    public AssetType(){}

    public AssetType(
    String 
            _Type
    )
    {
    
      this._Type = _Type;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.AssetType."
    
      + _Type.ToString()
    ;
    
    return uri;
    }

    

      public String _Type
      {
        
            get { return __Type;}
            set 
            { 
                __Type = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      AssetType assetType = new AssetType();
      
      assetType._Type = this._Type;
      assetType.ActiveRecordId = this.ActiveRecordId; 
      return assetType;
    }

    
          // one to many relation
          private List<Asset> _relatedAsset;

          public List<Asset> relatedAsset
          {
          get { return _relatedAsset;}
          set { _relatedAsset = value; }
          }
          
          
          public Asset addRelatedAssetItem(
          Asset asset)
          {
            asset.RelatedAssetType = this;
            
            _relatedAsset.Add(asset);
            
            return asset;
          }
            
        
    }
  

    public abstract class _AssetTypeDataMapper:TDataMapper<AssetType,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _AssetTypeDataMapper(){}
      public _AssetTypeDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[AssetType]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[AssetType] (
    
      [Type]
      ) Values (
    
      @type);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override AssetType create( AssetType assetType )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@type", assetType._Type);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(assetType.relatedAsset != null 
            && assetType.relatedAsset.Count > 0)
          {
            AssetDataMapper dataMapper = new AssetDataMapper(Database);
            
            foreach(Asset item in assetType.relatedAsset)
              dataMapper.create(item);
          }
        
      
      raiseAffected(assetType,DataMapperOperation.create);

      return registerRecord(assetType);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [Type] 
     From [dbo].[AssetType]
    
       Where 
      
         [Type] = @type
    ";

    public AssetType findByPrimaryKey(
    String type
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@type", type);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("AssetType not found, search by primary key");
 

    }


    public bool exists(AssetType assetType)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@Type", assetType._Type);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [AssetType].[Type] = @CheckInType";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      AssetType _AssetType = (AssetType)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInType", _AssetType._Type);
      

      return sqlCommand;
    }

  

    protected override AssetType doLoad(IDataReader dataReader)
    {
    AssetType assetType = new AssetType();

    assetType._Type = (String) dataReader.GetValue(0);
            

    
    
    return registerRecord(assetType);
    }


    protected override AssetType doLoad(Hashtable hashtable)
    {
      AssetType assetType = new AssetType();

      
        
        if(hashtable.ContainsKey("Type"))
        assetType._Type = ( String)hashtable["_Type"];
          

      return assetType;
    }


    protected override List<AssetType> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<AssetType> resultList = new List<AssetType>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              AssetType item = new AssetType();
              
              
                    item._Type = ( String)dataRow["Type"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[AssetType]
    
      Where
      
        [Type] = @type";
    [Synchronized]
    public override AssetType remove(AssetType assetType)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@type", assetType._Type);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(assetType,DataMapperOperation.delete);

      return registerRecord(assetType);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override AssetType save( AssetType assetType )
    {
      if(exists(assetType))
        return update(assetType);
        return create(assetType);
    }

  [TransactionRequired]
    [Synchronized]
    public override AssetType update(AssetType assetType)
    {
      
    
        return registerRecord(assetType);

    }

  
    }
    
  
    
    public partial class Asset: DomainObject
    {
    
      protected int _assetId;
    
      protected int _chiefAssetId;
    
      protected String _businessName;
    
      protected String _firstName;
    
      protected String _middleName;
    
      protected String _lastName;
    
      protected String _sSN;
    
      protected bool _deleted;
    

      // parent tables
      protected AssetType _relatedAssetType
        = new AssetType()
      ;
    

    public Asset(){}

    public Asset(
    int 
            assetId,String 
            _Type,int 
            chiefAssetId,String 
            businessName,String 
            firstName,String 
            middleName,String 
            lastName,String 
            sSN,bool 
            deleted
    )
    {
    
      this.AssetId = assetId;
    
      this._Type = _Type;
    
      this.ChiefAssetId = chiefAssetId;
    
      this.BusinessName = businessName;
    
      this.FirstName = firstName;
    
      this.MiddleName = middleName;
    
      this.LastName = lastName;
    
      this.SSN = sSN;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Asset."
    
      + AssetId.ToString()
    ;
    
    return uri;
    }

    

      public int AssetId
      {
        
            get { return _assetId;}
            set 
            { 
                _assetId = value;
            }
          
      }
    

      public String _Type
      {
        
            get
            {
            
                  if(_relatedAssetType != null)
                    return _relatedAssetType._Type;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAssetType == null)
                        _relatedAssetType = new AssetType();

                      _relatedAssetType._Type = value;
                    
            }
          
      }
    

      public int ChiefAssetId
      {
        
            get { return _chiefAssetId;}
            set 
            { 
                _chiefAssetId = value;
            }
          
      }
    

      public String BusinessName
      {
        
            get { return _businessName;}
            set 
            { 
                _businessName = value;
            }
          
      }
    

      public String FirstName
      {
        
            get { return _firstName;}
            set 
            { 
                _firstName = value;
            }
          
      }
    

      public String MiddleName
      {
        
            get { return _middleName;}
            set 
            { 
                _middleName = value;
            }
          
      }
    

      public String LastName
      {
        
            get { return _lastName;}
            set 
            { 
                _lastName = value;
            }
          
      }
    

      public String SSN
      {
        
            get { return _sSN;}
            set 
            { 
                _sSN = value;
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    

      public AssetType RelatedAssetType
      {
      get { return _relatedAssetType;}
      set { _relatedAssetType = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      Asset asset = new Asset();
      
      asset.AssetId = this.AssetId;
      asset._Type = this._Type;
      asset.ChiefAssetId = this.ChiefAssetId;
      asset.BusinessName = this.BusinessName;
      asset.FirstName = this.FirstName;
      asset.MiddleName = this.MiddleName;
      asset.LastName = this.LastName;
      asset.SSN = this.SSN;
      asset.Deleted = this.Deleted;
      asset.ActiveRecordId = this.ActiveRecordId; 
      return asset;
    }

    
          // one to many relation
          private List<Bill> _relatedBill;

          public List<Bill> relatedBill
          {
          get { return _relatedBill;}
          set { _relatedBill = value; }
          }
          
          
          public Bill addRelatedBillItem(
          Bill bill)
          {
            bill.RelatedAsset = this;
            
            _relatedBill.Add(bill);
            
            return bill;
          }
            
        
          // one to many relation
          private List<AssetAssignment> _relatedAssetAssignment;

          public List<AssetAssignment> relatedAssetAssignment
          {
          get { return _relatedAssetAssignment;}
          set { _relatedAssetAssignment = value; }
          }
          
          
          public AssetAssignment addRelatedAssetAssignmentItem(
          AssetAssignment assetAssignment)
          {
            assetAssignment.RelatedAsset = this;
            
            _relatedAssetAssignment.Add(assetAssignment);
            
            return assetAssignment;
          }
            
        
          // one to many relation
          private List<UserAsset> _relatedUserAsset;

          public List<UserAsset> relatedUserAsset
          {
          get { return _relatedUserAsset;}
          set { _relatedUserAsset = value; }
          }
          
          
          public UserAsset addRelatedUserAssetItem(
          UserAsset userAsset)
          {
            userAsset.RelatedAsset = this;
            
            _relatedUserAsset.Add(userAsset);
            
            return userAsset;
          }
            
        
    }
  

    public abstract class _AssetDataMapper:TDataMapper<Asset,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _AssetDataMapper(){}
      public _AssetDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Asset]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Asset] (
    
      [Type]
      ,
      [ChiefAssetId]
      ,
      [BusinessName]
      ,
      [FirstName]
      ,
      [MiddleName]
      ,
      [LastName]
      ,
      [SSN]
      ,
      [Deleted]
      ) Values (
    
      @type,
      @chiefAssetId,
      @businessName,
      @firstName,
      @middleName,
      @lastName,
      @sSN,
      @deleted);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Asset create( Asset asset )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@type", asset._Type);
              
                  sqlCommand.Parameters.AddWithValue("@chiefAssetId", asset.ChiefAssetId);
              
                  sqlCommand.Parameters.AddWithValue("@businessName", asset.BusinessName);
              
                  sqlCommand.Parameters.AddWithValue("@firstName", asset.FirstName);
              
                  sqlCommand.Parameters.AddWithValue("@middleName", asset.MiddleName);
              
                  sqlCommand.Parameters.AddWithValue("@lastName", asset.LastName);
              
                  sqlCommand.Parameters.AddWithValue("@sSN", asset.SSN);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", asset.Deleted);
              asset.AssetId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(asset.relatedBill != null 
            && asset.relatedBill.Count > 0)
          {
            BillDataMapper dataMapper = new BillDataMapper(Database);
            
            foreach(Bill item in asset.relatedBill)
              dataMapper.create(item);
          }
        
          
          if(asset.relatedAssetAssignment != null 
            && asset.relatedAssetAssignment.Count > 0)
          {
            AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(Database);
            
            foreach(AssetAssignment item in asset.relatedAssetAssignment)
              dataMapper.create(item);
          }
        
          
          if(asset.relatedUserAsset != null 
            && asset.relatedUserAsset.Count > 0)
          {
            UserAssetDataMapper dataMapper = new UserAssetDataMapper(Database);
            
            foreach(UserAsset item in asset.relatedUserAsset)
              dataMapper.create(item);
          }
        
      
      raiseAffected(asset,DataMapperOperation.create);

      return registerRecord(asset);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [AssetId] ,
      [Type] ,
      [ChiefAssetId] ,
      [BusinessName] ,
      [FirstName] ,
      [MiddleName] ,
      [LastName] ,
      [SSN] ,
      [Deleted] 
     From [dbo].[Asset]
    
       Where 
      
         [AssetId] = @assetId
    ";

    public Asset findByPrimaryKey(
    int assetId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@assetId", assetId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Asset not found, search by primary key");
 

    }


    public bool exists(Asset asset)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@AssetId", asset.AssetId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Asset].[AssetId] = @CheckInAssetId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Asset _Asset = (Asset)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInAssetId", _Asset.AssetId);
      

      return sqlCommand;
    }

  

    protected override Asset doLoad(IDataReader dataReader)
    {
    Asset asset = new Asset();

    asset.AssetId = (int) dataReader.GetValue(0);
            asset._Type = (String) dataReader.GetValue(1);
            asset.ChiefAssetId = (int) dataReader.GetValue(2);
            asset.BusinessName = (String) dataReader.GetValue(3);
            asset.FirstName = (String) dataReader.GetValue(4);
            asset.MiddleName = (String) dataReader.GetValue(5);
            asset.LastName = (String) dataReader.GetValue(6);
            asset.SSN = (String) dataReader.GetValue(7);
            asset.Deleted = (bool) dataReader.GetValue(8);
            

    
    
    return registerRecord(asset);
    }


    protected override Asset doLoad(Hashtable hashtable)
    {
      Asset asset = new Asset();

      
        
        if(hashtable.ContainsKey("AssetId"))
        asset.AssetId = ( int)hashtable["AssetId"];
          
        
        if(hashtable.ContainsKey("Type"))
        asset._Type = ( String)hashtable["_Type"];
          
        
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
          
        
        if(hashtable.ContainsKey("Deleted"))
        asset.Deleted = ( bool)hashtable["Deleted"];
          

      return asset;
    }


    protected override List<Asset> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Asset> resultList = new List<Asset>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Asset item = new Asset();
              
              
                    item.AssetId = ( int)dataRow["AssetId"] ;
                  
                    item._Type = ( String)dataRow["Type"] ;
                  
                    item.ChiefAssetId = ( int)dataRow["ChiefAssetId"] ;
                  
                    item.BusinessName = ( String)dataRow["BusinessName"] ;
                  
                    item.FirstName = ( String)dataRow["FirstName"] ;
                  
                    item.MiddleName = ( String)dataRow["MiddleName"] ;
                  
                    item.LastName = ( String)dataRow["LastName"] ;
                  
                    item.SSN = ( String)dataRow["SSN"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Asset]
    
      Where
      
        [AssetId] = @assetId";
    [Synchronized]
    public override Asset remove(Asset asset)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@assetId", asset.AssetId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(asset,DataMapperOperation.delete);

      return registerRecord(asset);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Asset save( Asset asset )
    {
      if(exists(asset))
        return update(asset);
        return create(asset);
    }

  
      const String SqlUpdate = @"Update [dbo].[Asset] Set
      
        [Type] = @type,
        [ChiefAssetId] = @chiefAssetId,
        [BusinessName] = @businessName,
        [FirstName] = @firstName,
        [MiddleName] = @middleName,
        [LastName] = @lastName,
        [SSN] = @sSN,
        [Deleted] = @deleted
        Where
        
          [AssetId] = @assetId";
    [TransactionRequired]
    [Synchronized]
    public override Asset update(Asset asset)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@assetId", asset.AssetId);
            
              sqlCommand.Parameters.AddWithValue("@type", asset._Type);
            
              sqlCommand.Parameters.AddWithValue("@chiefAssetId", asset.ChiefAssetId);
            
              sqlCommand.Parameters.AddWithValue("@businessName", asset.BusinessName);
            
              sqlCommand.Parameters.AddWithValue("@firstName", asset.FirstName);
            
              sqlCommand.Parameters.AddWithValue("@middleName", asset.MiddleName);
            
              sqlCommand.Parameters.AddWithValue("@lastName", asset.LastName);
            
              sqlCommand.Parameters.AddWithValue("@sSN", asset.SSN);
            
              sqlCommand.Parameters.AddWithValue("@deleted", asset.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(asset.relatedBill != null
              && asset.relatedBill.Count > 0)
              {
              BillDataMapper dataMapper = new BillDataMapper(Database);

              foreach(Bill item in asset.relatedBill)
              dataMapper.save(item);
              }
            
              if(asset.relatedAssetAssignment != null
              && asset.relatedAssetAssignment.Count > 0)
              {
              AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(Database);

              foreach(AssetAssignment item in asset.relatedAssetAssignment)
              dataMapper.save(item);
              }
            
              if(asset.relatedUserAsset != null
              && asset.relatedUserAsset.Count > 0)
              {
              UserAssetDataMapper dataMapper = new UserAssetDataMapper(Database);

              foreach(UserAsset item in asset.relatedUserAsset)
              dataMapper.save(item);
              }
            

        raiseAffected(asset,DataMapperOperation.update);
        
    
        return registerRecord(asset);

    }

  
    }
    
  
    
    public partial class Afe: DomainObject
    {
    
      protected String _aFE;
    
      protected String _aFEName;
    
      protected bool _deleted;
    

      // parent tables
      protected AfeStatus _relatedAfeStatus
        = new AfeStatus()
      ;
    

      // parent tables
      protected Client _relatedClient
        = new Client()
      ;
    

    public Afe(){}

    public Afe(
    String 
            aFE,int 
            clientId,String 
            aFEName,String 
            aFEStatus,bool 
            deleted
    )
    {
    
      this.AFE = aFE;
    
      this.ClientId = clientId;
    
      this.AFEName = aFEName;
    
      this.AFEStatus = aFEStatus;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Afe."
    
      + AFE.ToString()
    ;
    
    return uri;
    }

    

      public String AFE
      {
        
            get { return _aFE;}
            set 
            { 
                _aFE = value;
            }
          
      }
    

      public int ClientId
      {
        
            get
            {
            
                  if(_relatedClient != null)
                    return _relatedClient.ClientId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedClient == null)
                        _relatedClient = new Client();

                      _relatedClient.ClientId = value;
                    
            }
          
      }
    

      public String AFEName
      {
        
            get { return _aFEName;}
            set 
            { 
                _aFEName = value;
            }
          
      }
    

      public String AFEStatus
      {
        
            get
            {
            
                  if(_relatedAfeStatus != null)
                    return _relatedAfeStatus.AFEStatus;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAfeStatus == null)
                        _relatedAfeStatus = new AfeStatus();

                      _relatedAfeStatus.AFEStatus = value;
                    
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    

      public AfeStatus RelatedAfeStatus
      {
      get { return _relatedAfeStatus;}
      set { _relatedAfeStatus = value; }
      }
      
    

      public Client RelatedClient
      {
      get { return _relatedClient;}
      set { _relatedClient = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      Afe afe = new Afe();
      
      afe.AFE = this.AFE;
      afe.ClientId = this.ClientId;
      afe.AFEName = this.AFEName;
      afe.AFEStatus = this.AFEStatus;
      afe.Deleted = this.Deleted;
      afe.ActiveRecordId = this.ActiveRecordId; 
      return afe;
    }

    
          // one to many relation
          private List<SubAfe> _relatedSubAfe;

          public List<SubAfe> relatedSubAfe
          {
          get { return _relatedSubAfe;}
          set { _relatedSubAfe = value; }
          }
          
          
          public SubAfe addRelatedSubAfeItem(
          SubAfe subAfe)
          {
            subAfe.RelatedAfe = this;
            
            _relatedSubAfe.Add(subAfe);
            
            return subAfe;
          }
            
        
          // one to many relation
          private List<AssetAssignment> _relatedAssetAssignment;

          public List<AssetAssignment> relatedAssetAssignment
          {
          get { return _relatedAssetAssignment;}
          set { _relatedAssetAssignment = value; }
          }
          
          
          public AssetAssignment addRelatedAssetAssignmentItem(
          AssetAssignment assetAssignment)
          {
            assetAssignment.RelatedAfe = this;
            
            _relatedAssetAssignment.Add(assetAssignment);
            
            return assetAssignment;
          }
            
        
    }
  

    public abstract class _AfeDataMapper:TDataMapper<Afe,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _AfeDataMapper(){}
      public _AfeDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Afe]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Afe] (
    
      [AFE]
      ,
      [ClientId]
      ,
      [AFEName]
      ,
      [AFEStatus]
      ,
      [Deleted]
      ) Values (
    
      @aFE,
      @clientId,
      @aFEName,
      @aFEStatus,
      @deleted);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Afe create( Afe afe )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@aFE", afe.AFE);
              
                  sqlCommand.Parameters.AddWithValue("@clientId", afe.ClientId);
              
                  sqlCommand.Parameters.AddWithValue("@aFEName", afe.AFEName);
              
                  sqlCommand.Parameters.AddWithValue("@aFEStatus", afe.AFEStatus);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", afe.Deleted);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(afe.relatedSubAfe != null 
            && afe.relatedSubAfe.Count > 0)
          {
            SubAfeDataMapper dataMapper = new SubAfeDataMapper(Database);
            
            foreach(SubAfe item in afe.relatedSubAfe)
              dataMapper.create(item);
          }
        
          
          if(afe.relatedAssetAssignment != null 
            && afe.relatedAssetAssignment.Count > 0)
          {
            AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(Database);
            
            foreach(AssetAssignment item in afe.relatedAssetAssignment)
              dataMapper.create(item);
          }
        
      
      raiseAffected(afe,DataMapperOperation.create);

      return registerRecord(afe);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [AFE] ,
      [ClientId] ,
      [AFEName] ,
      [AFEStatus] ,
      [Deleted] 
     From [dbo].[Afe]
    
       Where 
      
         [AFE] = @aFE
    ";

    public Afe findByPrimaryKey(
    String aFE
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@aFE", aFE);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Afe not found, search by primary key");
 

    }


    public bool exists(Afe afe)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@AFE", afe.AFE);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Afe].[AFE] = @CheckInAFE";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Afe _Afe = (Afe)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInAFE", _Afe.AFE);
      

      return sqlCommand;
    }

  

    protected override Afe doLoad(IDataReader dataReader)
    {
    Afe afe = new Afe();

    afe.AFE = (String) dataReader.GetValue(0);
            afe.ClientId = (int) dataReader.GetValue(1);
            afe.AFEName = (String) dataReader.GetValue(2);
            afe.AFEStatus = (String) dataReader.GetValue(3);
            afe.Deleted = (bool) dataReader.GetValue(4);
            

    
    
    return registerRecord(afe);
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
          
        
        if(hashtable.ContainsKey("Deleted"))
        afe.Deleted = ( bool)hashtable["Deleted"];
          

      return afe;
    }


    protected override List<Afe> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Afe> resultList = new List<Afe>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Afe item = new Afe();
              
              
                    item.AFE = ( String)dataRow["AFE"] ;
                  
                    item.ClientId = ( int)dataRow["ClientId"] ;
                  
                    item.AFEName = ( String)dataRow["AFEName"] ;
                  
                    item.AFEStatus = ( String)dataRow["AFEStatus"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Afe]
    
      Where
      
        [AFE] = @aFE";
    [Synchronized]
    public override Afe remove(Afe afe)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@aFE", afe.AFE);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(afe,DataMapperOperation.delete);

      return registerRecord(afe);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Afe save( Afe afe )
    {
      if(exists(afe))
        return update(afe);
        return create(afe);
    }

  
      const String SqlUpdate = @"Update [dbo].[Afe] Set
      
        [ClientId] = @clientId,
        [AFEName] = @aFEName,
        [AFEStatus] = @aFEStatus,
        [Deleted] = @deleted
        Where
        
          [AFE] = @aFE";
    [TransactionRequired]
    [Synchronized]
    public override Afe update(Afe afe)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@aFE", afe.AFE);
            
              sqlCommand.Parameters.AddWithValue("@clientId", afe.ClientId);
            
              sqlCommand.Parameters.AddWithValue("@aFEName", afe.AFEName);
            
              sqlCommand.Parameters.AddWithValue("@aFEStatus", afe.AFEStatus);
            
              sqlCommand.Parameters.AddWithValue("@deleted", afe.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(afe.relatedSubAfe != null
              && afe.relatedSubAfe.Count > 0)
              {
              SubAfeDataMapper dataMapper = new SubAfeDataMapper(Database);

              foreach(SubAfe item in afe.relatedSubAfe)
              dataMapper.save(item);
              }
            
              if(afe.relatedAssetAssignment != null
              && afe.relatedAssetAssignment.Count > 0)
              {
              AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(Database);

              foreach(AssetAssignment item in afe.relatedAssetAssignment)
              dataMapper.save(item);
              }
            

        raiseAffected(afe,DataMapperOperation.update);
        
    
        return registerRecord(afe);

    }

  
    }
    
  
    
    public partial class BillStatus: DomainObject
    {
    
      protected String _status;
    

    public BillStatus(){}

    public BillStatus(
    String 
            status
    )
    {
    
      this.Status = status;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.BillStatus."
    
      + Status.ToString()
    ;
    
    return uri;
    }

    

      public String Status
      {
        
            get { return _status;}
            set 
            { 
                _status = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      BillStatus billStatus = new BillStatus();
      
      billStatus.Status = this.Status;
      billStatus.ActiveRecordId = this.ActiveRecordId; 
      return billStatus;
    }

    
          // one to many relation
          private List<Bill> _relatedBill;

          public List<Bill> relatedBill
          {
          get { return _relatedBill;}
          set { _relatedBill = value; }
          }
          
          
          public Bill addRelatedBillItem(
          Bill bill)
          {
            bill.RelatedBillStatus = this;
            
            _relatedBill.Add(bill);
            
            return bill;
          }
            
        
    }
  

    public abstract class _BillStatusDataMapper:TDataMapper<BillStatus,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _BillStatusDataMapper(){}
      public _BillStatusDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[BillStatus]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[BillStatus] (
    
      [Status]
      ) Values (
    
      @status);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override BillStatus create( BillStatus billStatus )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@status", billStatus.Status);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(billStatus.relatedBill != null 
            && billStatus.relatedBill.Count > 0)
          {
            BillDataMapper dataMapper = new BillDataMapper(Database);
            
            foreach(Bill item in billStatus.relatedBill)
              dataMapper.create(item);
          }
        
      
      raiseAffected(billStatus,DataMapperOperation.create);

      return registerRecord(billStatus);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [Status] 
     From [dbo].[BillStatus]
    
       Where 
      
         [Status] = @status
    ";

    public BillStatus findByPrimaryKey(
    String status
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@status", status);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("BillStatus not found, search by primary key");
 

    }


    public bool exists(BillStatus billStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@Status", billStatus.Status);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [BillStatus].[Status] = @CheckInStatus";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      BillStatus _BillStatus = (BillStatus)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInStatus", _BillStatus.Status);
      

      return sqlCommand;
    }

  

    protected override BillStatus doLoad(IDataReader dataReader)
    {
    BillStatus billStatus = new BillStatus();

    billStatus.Status = (String) dataReader.GetValue(0);
            

    
    
    return registerRecord(billStatus);
    }


    protected override BillStatus doLoad(Hashtable hashtable)
    {
      BillStatus billStatus = new BillStatus();

      
        
        if(hashtable.ContainsKey("Status"))
        billStatus.Status = ( String)hashtable["Status"];
          

      return billStatus;
    }


    protected override List<BillStatus> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<BillStatus> resultList = new List<BillStatus>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              BillStatus item = new BillStatus();
              
              
                    item.Status = ( String)dataRow["Status"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[BillStatus]
    
      Where
      
        [Status] = @status";
    [Synchronized]
    public override BillStatus remove(BillStatus billStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@status", billStatus.Status);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(billStatus,DataMapperOperation.delete);

      return registerRecord(billStatus);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override BillStatus save( BillStatus billStatus )
    {
      if(exists(billStatus))
        return update(billStatus);
        return create(billStatus);
    }

  [TransactionRequired]
    [Synchronized]
    public override BillStatus update(BillStatus billStatus)
    {
      
    
        return registerRecord(billStatus);

    }

  
    }
    
  
    
    public partial class BillItemStatus: DomainObject
    {
    
      protected String _status;
    

    public BillItemStatus(){}

    public BillItemStatus(
    String 
            status
    )
    {
    
      this.Status = status;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.BillItemStatus."
    
      + Status.ToString()
    ;
    
    return uri;
    }

    

      public String Status
      {
        
            get { return _status;}
            set 
            { 
                _status = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      BillItemStatus billItemStatus = new BillItemStatus();
      
      billItemStatus.Status = this.Status;
      billItemStatus.ActiveRecordId = this.ActiveRecordId; 
      return billItemStatus;
    }

    
          // one to many relation
          private List<BillItem> _relatedBillItem;

          public List<BillItem> relatedBillItem
          {
          get { return _relatedBillItem;}
          set { _relatedBillItem = value; }
          }
          
          
          public BillItem addRelatedBillItemItem(
          BillItem billItem)
          {
            billItem.RelatedBillItemStatus = this;
            
            _relatedBillItem.Add(billItem);
            
            return billItem;
          }
            
        
    }
  

    public abstract class _BillItemStatusDataMapper:TDataMapper<BillItemStatus,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _BillItemStatusDataMapper(){}
      public _BillItemStatusDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[BillItemStatus]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[BillItemStatus] (
    
      [Status]
      ) Values (
    
      @status);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override BillItemStatus create( BillItemStatus billItemStatus )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@status", billItemStatus.Status);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(billItemStatus.relatedBillItem != null 
            && billItemStatus.relatedBillItem.Count > 0)
          {
            BillItemDataMapper dataMapper = new BillItemDataMapper(Database);
            
            foreach(BillItem item in billItemStatus.relatedBillItem)
              dataMapper.create(item);
          }
        
      
      raiseAffected(billItemStatus,DataMapperOperation.create);

      return registerRecord(billItemStatus);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [Status] 
     From [dbo].[BillItemStatus]
    
       Where 
      
         [Status] = @status
    ";

    public BillItemStatus findByPrimaryKey(
    String status
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@status", status);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("BillItemStatus not found, search by primary key");
 

    }


    public bool exists(BillItemStatus billItemStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@Status", billItemStatus.Status);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [BillItemStatus].[Status] = @CheckInStatus";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      BillItemStatus _BillItemStatus = (BillItemStatus)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInStatus", _BillItemStatus.Status);
      

      return sqlCommand;
    }

  

    protected override BillItemStatus doLoad(IDataReader dataReader)
    {
    BillItemStatus billItemStatus = new BillItemStatus();

    billItemStatus.Status = (String) dataReader.GetValue(0);
            

    
    
    return registerRecord(billItemStatus);
    }


    protected override BillItemStatus doLoad(Hashtable hashtable)
    {
      BillItemStatus billItemStatus = new BillItemStatus();

      
        
        if(hashtable.ContainsKey("Status"))
        billItemStatus.Status = ( String)hashtable["Status"];
          

      return billItemStatus;
    }


    protected override List<BillItemStatus> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<BillItemStatus> resultList = new List<BillItemStatus>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              BillItemStatus item = new BillItemStatus();
              
              
                    item.Status = ( String)dataRow["Status"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[BillItemStatus]
    
      Where
      
        [Status] = @status";
    [Synchronized]
    public override BillItemStatus remove(BillItemStatus billItemStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@status", billItemStatus.Status);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(billItemStatus,DataMapperOperation.delete);

      return registerRecord(billItemStatus);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override BillItemStatus save( BillItemStatus billItemStatus )
    {
      if(exists(billItemStatus))
        return update(billItemStatus);
        return create(billItemStatus);
    }

  [TransactionRequired]
    [Synchronized]
    public override BillItemStatus update(BillItemStatus billItemStatus)
    {
      
    
        return registerRecord(billItemStatus);

    }

  
    }
    
  
    
    public partial class InvoiceItemType: DomainObject
    {
    
      protected int _invoiceItemTypeId;
    
      protected String _name;
    
      protected bool? _isCountable;
    
      protected bool? _isPresetRate;
    
      protected bool? _isSingle;
    
      protected bool _deleted;
    

    public InvoiceItemType(){}

    public InvoiceItemType(
    int 
            invoiceItemTypeId,String 
            name,bool 
            isCountable,bool 
            isPresetRate,bool 
            isSingle,bool 
            deleted
    )
    {
    
      this.InvoiceItemTypeId = invoiceItemTypeId;
    
      this.Name = name;
    
      this.IsCountable = isCountable;
    
      this.IsPresetRate = isPresetRate;
    
      this.IsSingle = isSingle;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.InvoiceItemType."
    
      + InvoiceItemTypeId.ToString()
    ;
    
    return uri;
    }

    

      public int InvoiceItemTypeId
      {
        
            get { return _invoiceItemTypeId;}
            set 
            { 
                _invoiceItemTypeId = value;
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
    

      public bool? IsCountable
      {
        
            get { return _isCountable;}
            set 
            { 
                _isCountable = value;
            }
          
      }
    

      public bool? IsPresetRate
      {
        
            get { return _isPresetRate;}
            set 
            { 
                _isPresetRate = value;
            }
          
      }
    

      public bool? IsSingle
      {
        
            get { return _isSingle;}
            set 
            { 
                _isSingle = value;
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      InvoiceItemType invoiceItemType = new InvoiceItemType();
      
      invoiceItemType.InvoiceItemTypeId = this.InvoiceItemTypeId;
      invoiceItemType.Name = this.Name;
      invoiceItemType.IsCountable = this.IsCountable;
      invoiceItemType.IsPresetRate = this.IsPresetRate;
      invoiceItemType.IsSingle = this.IsSingle;
      invoiceItemType.Deleted = this.Deleted;
      invoiceItemType.ActiveRecordId = this.ActiveRecordId; 
      return invoiceItemType;
    }

    
          // one to many relation
          private List<BillItemType> _relatedBillItemType;

          public List<BillItemType> relatedBillItemType
          {
          get { return _relatedBillItemType;}
          set { _relatedBillItemType = value; }
          }
          
          
          public BillItemType addRelatedBillItemTypeItem(
          BillItemType billItemType)
          {
            billItemType.RelatedInvoiceItemType = this;
            
            _relatedBillItemType.Add(billItemType);
            
            return billItemType;
          }
            
        
          // one to many relation
          private List<InvoiceItem> _relatedInvoiceItem;

          public List<InvoiceItem> relatedInvoiceItem
          {
          get { return _relatedInvoiceItem;}
          set { _relatedInvoiceItem = value; }
          }
          
          
          public InvoiceItem addRelatedInvoiceItemItem(
          InvoiceItem invoiceItem)
          {
            invoiceItem.RelatedInvoiceItemType = this;
            
            _relatedInvoiceItem.Add(invoiceItem);
            
            return invoiceItem;
          }
            
        
    }
  

    public abstract class _InvoiceItemTypeDataMapper:TDataMapper<InvoiceItemType,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _InvoiceItemTypeDataMapper(){}
      public _InvoiceItemTypeDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[InvoiceItemType]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[InvoiceItemType] (
    
      [Name]
      ,
      [IsCountable]
      ,
      [IsPresetRate]
      ,
      [IsSingle]
      ,
      [Deleted]
      ) Values (
    
      @name,
      @isCountable,
      @isPresetRate,
      @isSingle,
      @deleted);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override InvoiceItemType create( InvoiceItemType invoiceItemType )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(invoiceItemType.Name != null)
                  
                  sqlCommand.Parameters.AddWithValue("@name", invoiceItemType.Name);
                else
                  sqlCommand.Parameters.AddWithValue("@name", DBNull.Value);
              
                    if(invoiceItemType.IsCountable.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@isCountable", invoiceItemType.IsCountable);
                else
                  sqlCommand.Parameters.AddWithValue("@isCountable", DBNull.Value);
              
                    if(invoiceItemType.IsPresetRate.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@isPresetRate", invoiceItemType.IsPresetRate);
                else
                  sqlCommand.Parameters.AddWithValue("@isPresetRate", DBNull.Value);
              
                    if(invoiceItemType.IsSingle.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@isSingle", invoiceItemType.IsSingle);
                else
                  sqlCommand.Parameters.AddWithValue("@isSingle", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", invoiceItemType.Deleted);
              invoiceItemType.InvoiceItemTypeId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(invoiceItemType.relatedBillItemType != null 
            && invoiceItemType.relatedBillItemType.Count > 0)
          {
            BillItemTypeDataMapper dataMapper = new BillItemTypeDataMapper(Database);
            
            foreach(BillItemType item in invoiceItemType.relatedBillItemType)
              dataMapper.create(item);
          }
        
          
          if(invoiceItemType.relatedInvoiceItem != null 
            && invoiceItemType.relatedInvoiceItem.Count > 0)
          {
            InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);
            
            foreach(InvoiceItem item in invoiceItemType.relatedInvoiceItem)
              dataMapper.create(item);
          }
        
      
      raiseAffected(invoiceItemType,DataMapperOperation.create);

      return registerRecord(invoiceItemType);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [InvoiceItemTypeId] ,
      [Name] ,
      [IsCountable] ,
      [IsPresetRate] ,
      [IsSingle] ,
      [Deleted] 
     From [dbo].[InvoiceItemType]
    
       Where 
      
         [InvoiceItemTypeId] = @invoiceItemTypeId
    ";

    public InvoiceItemType findByPrimaryKey(
    int invoiceItemTypeId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@invoiceItemTypeId", invoiceItemTypeId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("InvoiceItemType not found, search by primary key");
 

    }


    public bool exists(InvoiceItemType invoiceItemType)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@InvoiceItemTypeId", invoiceItemType.InvoiceItemTypeId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [InvoiceItemType].[InvoiceItemTypeId] = @CheckInInvoiceItemTypeId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      InvoiceItemType _InvoiceItemType = (InvoiceItemType)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInInvoiceItemTypeId", _InvoiceItemType.InvoiceItemTypeId);
      

      return sqlCommand;
    }

  

    protected override InvoiceItemType doLoad(IDataReader dataReader)
    {
    InvoiceItemType invoiceItemType = new InvoiceItemType();

    invoiceItemType.InvoiceItemTypeId = (int) dataReader.GetValue(0);
            
          if(!dataReader.IsDBNull(1))        
          invoiceItemType.Name = (String) dataReader.GetValue(1);
            
          if(!dataReader.IsDBNull(2))        
          invoiceItemType.IsCountable = (bool) dataReader.GetValue(2);
            
          if(!dataReader.IsDBNull(3))        
          invoiceItemType.IsPresetRate = (bool) dataReader.GetValue(3);
            
          if(!dataReader.IsDBNull(4))        
          invoiceItemType.IsSingle = (bool) dataReader.GetValue(4);
            invoiceItemType.Deleted = (bool) dataReader.GetValue(5);
            

    
    
    return registerRecord(invoiceItemType);
    }


    protected override InvoiceItemType doLoad(Hashtable hashtable)
    {
      InvoiceItemType invoiceItemType = new InvoiceItemType();

      
        
        if(hashtable.ContainsKey("InvoiceItemTypeId"))
        invoiceItemType.InvoiceItemTypeId = ( int)hashtable["InvoiceItemTypeId"];
          
        
        if(hashtable.ContainsKey("Name"))
        invoiceItemType.Name = ( String)hashtable["Name"];
          
        
        if(hashtable.ContainsKey("IsCountable"))
        invoiceItemType.IsCountable = ( bool)hashtable["IsCountable"];
          
        
        if(hashtable.ContainsKey("IsPresetRate"))
        invoiceItemType.IsPresetRate = ( bool)hashtable["IsPresetRate"];
          
        
        if(hashtable.ContainsKey("IsSingle"))
        invoiceItemType.IsSingle = ( bool)hashtable["IsSingle"];
          
        
        if(hashtable.ContainsKey("Deleted"))
        invoiceItemType.Deleted = ( bool)hashtable["Deleted"];
          

      return invoiceItemType;
    }


    protected override List<InvoiceItemType> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<InvoiceItemType> resultList = new List<InvoiceItemType>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              InvoiceItemType item = new InvoiceItemType();
              
              
                    item.InvoiceItemTypeId = ( int)dataRow["InvoiceItemTypeId"] ;
                  
                  if(!dataRow.IsNull("Name"))
                
                    item.Name = ( String)dataRow["Name"] ;
                  
                  if(!dataRow.IsNull("IsCountable"))
                
                    item.IsCountable = ( bool)dataRow["IsCountable"] ;
                  
                  if(!dataRow.IsNull("IsPresetRate"))
                
                    item.IsPresetRate = ( bool)dataRow["IsPresetRate"] ;
                  
                  if(!dataRow.IsNull("IsSingle"))
                
                    item.IsSingle = ( bool)dataRow["IsSingle"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[InvoiceItemType]
    
      Where
      
        [InvoiceItemTypeId] = @invoiceItemTypeId";
    [Synchronized]
    public override InvoiceItemType remove(InvoiceItemType invoiceItemType)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@invoiceItemTypeId", invoiceItemType.InvoiceItemTypeId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(invoiceItemType,DataMapperOperation.delete);

      return registerRecord(invoiceItemType);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override InvoiceItemType save( InvoiceItemType invoiceItemType )
    {
      if(exists(invoiceItemType))
        return update(invoiceItemType);
        return create(invoiceItemType);
    }

  
      const String SqlUpdate = @"Update [dbo].[InvoiceItemType] Set
      
        [Name] = @name,
        [IsCountable] = @isCountable,
        [IsPresetRate] = @isPresetRate,
        [IsSingle] = @isSingle,
        [Deleted] = @deleted
        Where
        
          [InvoiceItemTypeId] = @invoiceItemTypeId";
    [TransactionRequired]
    [Synchronized]
    public override InvoiceItemType update(InvoiceItemType invoiceItemType)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@invoiceItemTypeId", invoiceItemType.InvoiceItemTypeId);
            
                  if(invoiceItemType.Name != null)
                
              sqlCommand.Parameters.AddWithValue("@name", invoiceItemType.Name);
              else
              sqlCommand.Parameters.AddWithValue("@name", DBNull.Value);
            
                  if(invoiceItemType.IsCountable.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@isCountable", invoiceItemType.IsCountable);
              else
              sqlCommand.Parameters.AddWithValue("@isCountable", DBNull.Value);
            
                  if(invoiceItemType.IsPresetRate.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@isPresetRate", invoiceItemType.IsPresetRate);
              else
              sqlCommand.Parameters.AddWithValue("@isPresetRate", DBNull.Value);
            
                  if(invoiceItemType.IsSingle.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@isSingle", invoiceItemType.IsSingle);
              else
              sqlCommand.Parameters.AddWithValue("@isSingle", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@deleted", invoiceItemType.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(invoiceItemType.relatedBillItemType != null
              && invoiceItemType.relatedBillItemType.Count > 0)
              {
              BillItemTypeDataMapper dataMapper = new BillItemTypeDataMapper(Database);

              foreach(BillItemType item in invoiceItemType.relatedBillItemType)
              dataMapper.save(item);
              }
            
              if(invoiceItemType.relatedInvoiceItem != null
              && invoiceItemType.relatedInvoiceItem.Count > 0)
              {
              InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);

              foreach(InvoiceItem item in invoiceItemType.relatedInvoiceItem)
              dataMapper.save(item);
              }
            

        raiseAffected(invoiceItemType,DataMapperOperation.update);
        
    
        return registerRecord(invoiceItemType);

    }

  
    }
    
  
    
    public partial class Bill: DomainObject
    {
    
      protected int _billId;
    
      protected String _notes;
    
      protected String _startDate;
    
      protected int _totalDailyBill;
    
      protected decimal _dailyBillAmt;
    
      protected decimal _otherBillAmt;
    
      protected decimal _totalBillAmt;
    

      // parent tables
      protected Asset _relatedAsset
        = new Asset()
      ;
    

      // parent tables
      protected BillStatus _relatedBillStatus
        = new BillStatus()
      ;
    

    public Bill(){}

    public Bill(
    int 
            billId,String 
            status,String 
            notes,String 
            startDate,int 
            assetId,int 
            totalDailyBill,decimal 
            dailyBillAmt,decimal 
            otherBillAmt,decimal 
            totalBillAmt
    )
    {
    
      this.BillId = billId;
    
      this.Status = status;
    
      this.Notes = notes;
    
      this.StartDate = startDate;
    
      this.AssetId = assetId;
    
      this.TotalDailyBill = totalDailyBill;
    
      this.DailyBillAmt = dailyBillAmt;
    
      this.OtherBillAmt = otherBillAmt;
    
      this.TotalBillAmt = totalBillAmt;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Bill."
    
      + BillId.ToString()
    ;
    
    return uri;
    }

    

      public int BillId
      {
        
            get { return _billId;}
            set 
            { 
                _billId = value;
            }
          
      }
    

      public String Status
      {
        
            get
            {
            
                  if(_relatedBillStatus != null)
                    return _relatedBillStatus.Status;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedBillStatus == null)
                        _relatedBillStatus = new BillStatus();

                      _relatedBillStatus.Status = value;
                    
            }
          
      }
    

      public String Notes
      {
        
            get { return _notes;}
            set 
            { 
                _notes = value;
            }
          
      }
    

      public String StartDate
      {
        
            get { return _startDate;}
            set 
            { 
                _startDate = value;
            }
          
      }
    

      public int AssetId
      {
        
            get
            {
            
                  if(_relatedAsset != null)
                    return _relatedAsset.AssetId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAsset == null)
                        _relatedAsset = new Asset();

                      _relatedAsset.AssetId = value;
                    
            }
          
      }
    

      public int TotalDailyBill
      {
        
            get { return _totalDailyBill;}
            set 
            { 
                _totalDailyBill = value;
            }
          
      }
    

      public decimal DailyBillAmt
      {
        
            get { return _dailyBillAmt;}
            set 
            { 
                _dailyBillAmt = value;
            }
          
      }
    

      public decimal OtherBillAmt
      {
        
            get { return _otherBillAmt;}
            set 
            { 
                _otherBillAmt = value;
            }
          
      }
    

      public decimal TotalBillAmt
      {
        
            get { return _totalBillAmt;}
            set 
            { 
                _totalBillAmt = value;
            }
          
      }
    

      public Asset RelatedAsset
      {
      get { return _relatedAsset;}
      set { _relatedAsset = value; }
      }
      
    

      public BillStatus RelatedBillStatus
      {
      get { return _relatedBillStatus;}
      set { _relatedBillStatus = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      Bill bill = new Bill();
      
      bill.BillId = this.BillId;
      bill.Status = this.Status;
      bill.Notes = this.Notes;
      bill.StartDate = this.StartDate;
      bill.AssetId = this.AssetId;
      bill.TotalDailyBill = this.TotalDailyBill;
      bill.DailyBillAmt = this.DailyBillAmt;
      bill.OtherBillAmt = this.OtherBillAmt;
      bill.TotalBillAmt = this.TotalBillAmt;
      bill.ActiveRecordId = this.ActiveRecordId; 
      return bill;
    }

    
          // one to many relation
          private List<BillItem> _relatedBillItem;

          public List<BillItem> relatedBillItem
          {
          get { return _relatedBillItem;}
          set { _relatedBillItem = value; }
          }
          
          
          public BillItem addRelatedBillItemItem(
          BillItem billItem)
          {
            billItem.RelatedBill = this;
            
            _relatedBillItem.Add(billItem);
            
            return billItem;
          }
            
        
    }
  

    public abstract class _BillDataMapper:TDataMapper<Bill,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _BillDataMapper(){}
      public _BillDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Bill]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Bill] (
    
      [Status]
      ,
      [Notes]
      ,
      [StartDate]
      ,
      [AssetId]
      ,
      [TotalDailyBill]
      ,
      [DailyBillAmt]
      ,
      [OtherBillAmt]
      ,
      [TotalBillAmt]
      ) Values (
    
      @status,
      @notes,
      @startDate,
      @assetId,
      @totalDailyBill,
      @dailyBillAmt,
      @otherBillAmt,
      @totalBillAmt);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Bill create( Bill bill )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@status", bill.Status);
              
                  sqlCommand.Parameters.AddWithValue("@notes", bill.Notes);
              
                  sqlCommand.Parameters.AddWithValue("@startDate", bill.StartDate);
              
                  sqlCommand.Parameters.AddWithValue("@assetId", bill.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@totalDailyBill", bill.TotalDailyBill);
              
                  sqlCommand.Parameters.AddWithValue("@dailyBillAmt", bill.DailyBillAmt);
              
                  sqlCommand.Parameters.AddWithValue("@otherBillAmt", bill.OtherBillAmt);
              
                  sqlCommand.Parameters.AddWithValue("@totalBillAmt", bill.TotalBillAmt);
              bill.BillId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(bill.relatedBillItem != null 
            && bill.relatedBillItem.Count > 0)
          {
            BillItemDataMapper dataMapper = new BillItemDataMapper(Database);
            
            foreach(BillItem item in bill.relatedBillItem)
              dataMapper.create(item);
          }
        
      
      raiseAffected(bill,DataMapperOperation.create);

      return registerRecord(bill);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [BillId] ,
      [Status] ,
      [Notes] ,
      [StartDate] ,
      [AssetId] ,
      [TotalDailyBill] ,
      [DailyBillAmt] ,
      [OtherBillAmt] ,
      [TotalBillAmt] 
     From [dbo].[Bill]
    
       Where 
      
         [BillId] = @billId
    ";

    public Bill findByPrimaryKey(
    int billId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@billId", billId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Bill not found, search by primary key");
 

    }


    public bool exists(Bill bill)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@BillId", bill.BillId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Bill].[BillId] = @CheckInBillId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Bill _Bill = (Bill)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInBillId", _Bill.BillId);
      

      return sqlCommand;
    }

  

    protected override Bill doLoad(IDataReader dataReader)
    {
    Bill bill = new Bill();

    bill.BillId = (int) dataReader.GetValue(0);
            bill.Status = (String) dataReader.GetValue(1);
            bill.Notes = (String) dataReader.GetValue(2);
            bill.StartDate = (String) dataReader.GetValue(3);
            bill.AssetId = (int) dataReader.GetValue(4);
            bill.TotalDailyBill = (int) dataReader.GetValue(5);
            bill.DailyBillAmt = (decimal) dataReader.GetValue(6);
            bill.OtherBillAmt = (decimal) dataReader.GetValue(7);
            bill.TotalBillAmt = (decimal) dataReader.GetValue(8);
            

    
    
    return registerRecord(bill);
    }


    protected override Bill doLoad(Hashtable hashtable)
    {
      Bill bill = new Bill();

      
        
        if(hashtable.ContainsKey("BillId"))
        bill.BillId = ( int)hashtable["BillId"];
          
        
        if(hashtable.ContainsKey("Status"))
        bill.Status = ( String)hashtable["Status"];
          
        
        if(hashtable.ContainsKey("Notes"))
        bill.Notes = ( String)hashtable["Notes"];
          
        
        if(hashtable.ContainsKey("StartDate"))
        bill.StartDate = ( String)hashtable["StartDate"];
          
        
        if(hashtable.ContainsKey("AssetId"))
        bill.AssetId = ( int)hashtable["AssetId"];
          
        
        if(hashtable.ContainsKey("TotalDailyBill"))
        bill.TotalDailyBill = ( int)hashtable["TotalDailyBill"];
          
        
        if(hashtable.ContainsKey("DailyBillAmt"))
        bill.DailyBillAmt = ( decimal)hashtable["DailyBillAmt"];
          
        
        if(hashtable.ContainsKey("OtherBillAmt"))
        bill.OtherBillAmt = ( decimal)hashtable["OtherBillAmt"];
          
        
        if(hashtable.ContainsKey("TotalBillAmt"))
        bill.TotalBillAmt = ( decimal)hashtable["TotalBillAmt"];
          

      return bill;
    }


    protected override List<Bill> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Bill> resultList = new List<Bill>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Bill item = new Bill();
              
              
                    item.BillId = ( int)dataRow["BillId"] ;
                  
                    item.Status = ( String)dataRow["Status"] ;
                  
                    item.Notes = ( String)dataRow["Notes"] ;
                  
                    item.StartDate = ( String)dataRow["StartDate"] ;
                  
                    item.AssetId = ( int)dataRow["AssetId"] ;
                  
                    item.TotalDailyBill = ( int)dataRow["TotalDailyBill"] ;
                  
                    item.DailyBillAmt = ( decimal)dataRow["DailyBillAmt"] ;
                  
                    item.OtherBillAmt = ( decimal)dataRow["OtherBillAmt"] ;
                  
                    item.TotalBillAmt = ( decimal)dataRow["TotalBillAmt"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Bill]
    
      Where
      
        [BillId] = @billId";
    [Synchronized]
    public override Bill remove(Bill bill)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@billId", bill.BillId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(bill,DataMapperOperation.delete);

      return registerRecord(bill);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Bill save( Bill bill )
    {
      if(exists(bill))
        return update(bill);
        return create(bill);
    }

  
      const String SqlUpdate = @"Update [dbo].[Bill] Set
      
        [Status] = @status,
        [Notes] = @notes,
        [StartDate] = @startDate,
        [AssetId] = @assetId,
        [TotalDailyBill] = @totalDailyBill,
        [DailyBillAmt] = @dailyBillAmt,
        [OtherBillAmt] = @otherBillAmt,
        [TotalBillAmt] = @totalBillAmt
        Where
        
          [BillId] = @billId";
    [TransactionRequired]
    [Synchronized]
    public override Bill update(Bill bill)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@billId", bill.BillId);
            
              sqlCommand.Parameters.AddWithValue("@status", bill.Status);
            
              sqlCommand.Parameters.AddWithValue("@notes", bill.Notes);
            
              sqlCommand.Parameters.AddWithValue("@startDate", bill.StartDate);
            
              sqlCommand.Parameters.AddWithValue("@assetId", bill.AssetId);
            
              sqlCommand.Parameters.AddWithValue("@totalDailyBill", bill.TotalDailyBill);
            
              sqlCommand.Parameters.AddWithValue("@dailyBillAmt", bill.DailyBillAmt);
            
              sqlCommand.Parameters.AddWithValue("@otherBillAmt", bill.OtherBillAmt);
            
              sqlCommand.Parameters.AddWithValue("@totalBillAmt", bill.TotalBillAmt);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(bill.relatedBillItem != null
              && bill.relatedBillItem.Count > 0)
              {
              BillItemDataMapper dataMapper = new BillItemDataMapper(Database);

              foreach(BillItem item in bill.relatedBillItem)
              dataMapper.save(item);
              }
            

        raiseAffected(bill,DataMapperOperation.update);
        
    
        return registerRecord(bill);

    }

  
    }
    
  
    
    public partial class SubAfeStatus: DomainObject
    {
    
      protected String _subAFEStatus;
    

    public SubAfeStatus(){}

    public SubAfeStatus(
    String 
            subAFEStatus
    )
    {
    
      this.SubAFEStatus = subAFEStatus;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.SubAfeStatus."
    
      + SubAFEStatus.ToString()
    ;
    
    return uri;
    }

    

      public String SubAFEStatus
      {
        
            get { return _subAFEStatus;}
            set 
            { 
                _subAFEStatus = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      SubAfeStatus subAfeStatus = new SubAfeStatus();
      
      subAfeStatus.SubAFEStatus = this.SubAFEStatus;
      subAfeStatus.ActiveRecordId = this.ActiveRecordId; 
      return subAfeStatus;
    }

    
          // one to many relation
          private List<SubAfe> _relatedSubAfe;

          public List<SubAfe> relatedSubAfe
          {
          get { return _relatedSubAfe;}
          set { _relatedSubAfe = value; }
          }
          
          
          public SubAfe addRelatedSubAfeItem(
          SubAfe subAfe)
          {
            subAfe.RelatedSubAfeStatus = this;
            
            _relatedSubAfe.Add(subAfe);
            
            return subAfe;
          }
            
        
    }
  

    public abstract class _SubAfeStatusDataMapper:TDataMapper<SubAfeStatus,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _SubAfeStatusDataMapper(){}
      public _SubAfeStatusDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[SubAfeStatus]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[SubAfeStatus] (
    
      [SubAFEStatus]
      ) Values (
    
      @subAFEStatus);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override SubAfeStatus create( SubAfeStatus subAfeStatus )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@subAFEStatus", subAfeStatus.SubAFEStatus);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(subAfeStatus.relatedSubAfe != null 
            && subAfeStatus.relatedSubAfe.Count > 0)
          {
            SubAfeDataMapper dataMapper = new SubAfeDataMapper(Database);
            
            foreach(SubAfe item in subAfeStatus.relatedSubAfe)
              dataMapper.create(item);
          }
        
      
      raiseAffected(subAfeStatus,DataMapperOperation.create);

      return registerRecord(subAfeStatus);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [SubAFEStatus] 
     From [dbo].[SubAfeStatus]
    
       Where 
      
         [SubAFEStatus] = @subAFEStatus
    ";

    public SubAfeStatus findByPrimaryKey(
    String subAFEStatus
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@subAFEStatus", subAFEStatus);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("SubAfeStatus not found, search by primary key");
 

    }


    public bool exists(SubAfeStatus subAfeStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@SubAFEStatus", subAfeStatus.SubAFEStatus);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [SubAfeStatus].[SubAFEStatus] = @CheckInSubAFEStatus";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      SubAfeStatus _SubAfeStatus = (SubAfeStatus)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInSubAFEStatus", _SubAfeStatus.SubAFEStatus);
      

      return sqlCommand;
    }

  

    protected override SubAfeStatus doLoad(IDataReader dataReader)
    {
    SubAfeStatus subAfeStatus = new SubAfeStatus();

    subAfeStatus.SubAFEStatus = (String) dataReader.GetValue(0);
            

    
    
    return registerRecord(subAfeStatus);
    }


    protected override SubAfeStatus doLoad(Hashtable hashtable)
    {
      SubAfeStatus subAfeStatus = new SubAfeStatus();

      
        
        if(hashtable.ContainsKey("SubAFEStatus"))
        subAfeStatus.SubAFEStatus = ( String)hashtable["SubAFEStatus"];
          

      return subAfeStatus;
    }


    protected override List<SubAfeStatus> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<SubAfeStatus> resultList = new List<SubAfeStatus>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              SubAfeStatus item = new SubAfeStatus();
              
              
                    item.SubAFEStatus = ( String)dataRow["SubAFEStatus"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[SubAfeStatus]
    
      Where
      
        [SubAFEStatus] = @subAFEStatus";
    [Synchronized]
    public override SubAfeStatus remove(SubAfeStatus subAfeStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@subAFEStatus", subAfeStatus.SubAFEStatus);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(subAfeStatus,DataMapperOperation.delete);

      return registerRecord(subAfeStatus);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override SubAfeStatus save( SubAfeStatus subAfeStatus )
    {
      if(exists(subAfeStatus))
        return update(subAfeStatus);
        return create(subAfeStatus);
    }

  [TransactionRequired]
    [Synchronized]
    public override SubAfeStatus update(SubAfeStatus subAfeStatus)
    {
      
    
        return registerRecord(subAfeStatus);

    }

  
    }
    
  
    
    public partial class BillItemType: DomainObject
    {
    
      protected int _billItemTypeId;
    
      protected String _name;
    
      protected bool? _isCountable;
    
      protected bool? _isPresetRate;
    
      protected bool? _isSingle;
    
      protected bool? _isAttachRequired;
    
      protected bool _deleted;
    

      // parent tables
      protected InvoiceItemType _relatedInvoiceItemType
        = new InvoiceItemType()
      ;
    

    public BillItemType(){}

    public BillItemType(
    int 
            billItemTypeId,int 
            invoiceItemTypeId,String 
            name,bool 
            isCountable,bool 
            isPresetRate,bool 
            isSingle,bool 
            isAttachRequired,bool 
            deleted
    )
    {
    
      this.BillItemTypeId = billItemTypeId;
    
      this.InvoiceItemTypeId = invoiceItemTypeId;
    
      this.Name = name;
    
      this.IsCountable = isCountable;
    
      this.IsPresetRate = isPresetRate;
    
      this.IsSingle = isSingle;
    
      this.IsAttachRequired = isAttachRequired;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.BillItemType."
    
      + BillItemTypeId.ToString()
    ;
    
    return uri;
    }

    

      public int BillItemTypeId
      {
        
            get { return _billItemTypeId;}
            set 
            { 
                _billItemTypeId = value;
            }
          
      }
    

      public int InvoiceItemTypeId
      {
        
            get
            {
            
                  if(_relatedInvoiceItemType != null)
                    return _relatedInvoiceItemType.InvoiceItemTypeId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedInvoiceItemType == null)
                        _relatedInvoiceItemType = new InvoiceItemType();

                      _relatedInvoiceItemType.InvoiceItemTypeId = value;
                    
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
    

      public bool? IsCountable
      {
        
            get { return _isCountable;}
            set 
            { 
                _isCountable = value;
            }
          
      }
    

      public bool? IsPresetRate
      {
        
            get { return _isPresetRate;}
            set 
            { 
                _isPresetRate = value;
            }
          
      }
    

      public bool? IsSingle
      {
        
            get { return _isSingle;}
            set 
            { 
                _isSingle = value;
            }
          
      }
    

      public bool? IsAttachRequired
      {
        
            get { return _isAttachRequired;}
            set 
            { 
                _isAttachRequired = value;
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    

      public InvoiceItemType RelatedInvoiceItemType
      {
      get { return _relatedInvoiceItemType;}
      set { _relatedInvoiceItemType = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      BillItemType billItemType = new BillItemType();
      
      billItemType.BillItemTypeId = this.BillItemTypeId;
      billItemType.InvoiceItemTypeId = this.InvoiceItemTypeId;
      billItemType.Name = this.Name;
      billItemType.IsCountable = this.IsCountable;
      billItemType.IsPresetRate = this.IsPresetRate;
      billItemType.IsSingle = this.IsSingle;
      billItemType.IsAttachRequired = this.IsAttachRequired;
      billItemType.Deleted = this.Deleted;
      billItemType.ActiveRecordId = this.ActiveRecordId; 
      return billItemType;
    }

    
          // one to many relation
          private List<BillItem> _relatedBillItem;

          public List<BillItem> relatedBillItem
          {
          get { return _relatedBillItem;}
          set { _relatedBillItem = value; }
          }
          
          
          public BillItem addRelatedBillItemItem(
          BillItem billItem)
          {
            billItem.RelatedBillItemType = this;
            
            _relatedBillItem.Add(billItem);
            
            return billItem;
          }
            
        
          // one to many relation
          private List<RateByAssignment> _relatedRateByAssignment;

          public List<RateByAssignment> relatedRateByAssignment
          {
          get { return _relatedRateByAssignment;}
          set { _relatedRateByAssignment = value; }
          }
          
          
          public RateByAssignment addRelatedRateByAssignmentItem(
          RateByAssignment rateByAssignment)
          {
            rateByAssignment.RelatedBillItemType = this;
            
            _relatedRateByAssignment.Add(rateByAssignment);
            
            return rateByAssignment;
          }
            
        
    }
  

    public abstract class _BillItemTypeDataMapper:TDataMapper<BillItemType,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _BillItemTypeDataMapper(){}
      public _BillItemTypeDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[BillItemType]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[BillItemType] (
    
      [InvoiceItemTypeId]
      ,
      [Name]
      ,
      [IsCountable]
      ,
      [IsPresetRate]
      ,
      [IsSingle]
      ,
      [IsAttachRequired]
      ,
      [Deleted]
      ) Values (
    
      @invoiceItemTypeId,
      @name,
      @isCountable,
      @isPresetRate,
      @isSingle,
      @isAttachRequired,
      @deleted);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override BillItemType create( BillItemType billItemType )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@invoiceItemTypeId", billItemType.InvoiceItemTypeId);
              
                    if(billItemType.Name != null)
                  
                  sqlCommand.Parameters.AddWithValue("@name", billItemType.Name);
                else
                  sqlCommand.Parameters.AddWithValue("@name", DBNull.Value);
              
                    if(billItemType.IsCountable.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@isCountable", billItemType.IsCountable);
                else
                  sqlCommand.Parameters.AddWithValue("@isCountable", DBNull.Value);
              
                    if(billItemType.IsPresetRate.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@isPresetRate", billItemType.IsPresetRate);
                else
                  sqlCommand.Parameters.AddWithValue("@isPresetRate", DBNull.Value);
              
                    if(billItemType.IsSingle.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@isSingle", billItemType.IsSingle);
                else
                  sqlCommand.Parameters.AddWithValue("@isSingle", DBNull.Value);
              
                    if(billItemType.IsAttachRequired.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@isAttachRequired", billItemType.IsAttachRequired);
                else
                  sqlCommand.Parameters.AddWithValue("@isAttachRequired", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", billItemType.Deleted);
              billItemType.BillItemTypeId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(billItemType.relatedBillItem != null 
            && billItemType.relatedBillItem.Count > 0)
          {
            BillItemDataMapper dataMapper = new BillItemDataMapper(Database);
            
            foreach(BillItem item in billItemType.relatedBillItem)
              dataMapper.create(item);
          }
        
          
          if(billItemType.relatedRateByAssignment != null 
            && billItemType.relatedRateByAssignment.Count > 0)
          {
            RateByAssignmentDataMapper dataMapper = new RateByAssignmentDataMapper(Database);
            
            foreach(RateByAssignment item in billItemType.relatedRateByAssignment)
              dataMapper.create(item);
          }
        
      
      raiseAffected(billItemType,DataMapperOperation.create);

      return registerRecord(billItemType);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [BillItemTypeId] ,
      [InvoiceItemTypeId] ,
      [Name] ,
      [IsCountable] ,
      [IsPresetRate] ,
      [IsSingle] ,
      [IsAttachRequired] ,
      [Deleted] 
     From [dbo].[BillItemType]
    
       Where 
      
         [BillItemTypeId] = @billItemTypeId
    ";

    public BillItemType findByPrimaryKey(
    int billItemTypeId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@billItemTypeId", billItemTypeId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("BillItemType not found, search by primary key");
 

    }


    public bool exists(BillItemType billItemType)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@BillItemTypeId", billItemType.BillItemTypeId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [BillItemType].[BillItemTypeId] = @CheckInBillItemTypeId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      BillItemType _BillItemType = (BillItemType)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInBillItemTypeId", _BillItemType.BillItemTypeId);
      

      return sqlCommand;
    }

  

    protected override BillItemType doLoad(IDataReader dataReader)
    {
    BillItemType billItemType = new BillItemType();

    billItemType.BillItemTypeId = (int) dataReader.GetValue(0);
            billItemType.InvoiceItemTypeId = (int) dataReader.GetValue(1);
            
          if(!dataReader.IsDBNull(2))        
          billItemType.Name = (String) dataReader.GetValue(2);
            
          if(!dataReader.IsDBNull(3))        
          billItemType.IsCountable = (bool) dataReader.GetValue(3);
            
          if(!dataReader.IsDBNull(4))        
          billItemType.IsPresetRate = (bool) dataReader.GetValue(4);
            
          if(!dataReader.IsDBNull(5))        
          billItemType.IsSingle = (bool) dataReader.GetValue(5);
            
          if(!dataReader.IsDBNull(6))        
          billItemType.IsAttachRequired = (bool) dataReader.GetValue(6);
            billItemType.Deleted = (bool) dataReader.GetValue(7);
            

    
    
    return registerRecord(billItemType);
    }


    protected override BillItemType doLoad(Hashtable hashtable)
    {
      BillItemType billItemType = new BillItemType();

      
        
        if(hashtable.ContainsKey("BillItemTypeId"))
        billItemType.BillItemTypeId = ( int)hashtable["BillItemTypeId"];
          
        
        if(hashtable.ContainsKey("InvoiceItemTypeId"))
        billItemType.InvoiceItemTypeId = ( int)hashtable["InvoiceItemTypeId"];
          
        
        if(hashtable.ContainsKey("Name"))
        billItemType.Name = ( String)hashtable["Name"];
          
        
        if(hashtable.ContainsKey("IsCountable"))
        billItemType.IsCountable = ( bool)hashtable["IsCountable"];
          
        
        if(hashtable.ContainsKey("IsPresetRate"))
        billItemType.IsPresetRate = ( bool)hashtable["IsPresetRate"];
          
        
        if(hashtable.ContainsKey("IsSingle"))
        billItemType.IsSingle = ( bool)hashtable["IsSingle"];
          
        
        if(hashtable.ContainsKey("IsAttachRequired"))
        billItemType.IsAttachRequired = ( bool)hashtable["IsAttachRequired"];
          
        
        if(hashtable.ContainsKey("Deleted"))
        billItemType.Deleted = ( bool)hashtable["Deleted"];
          

      return billItemType;
    }


    protected override List<BillItemType> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<BillItemType> resultList = new List<BillItemType>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              BillItemType item = new BillItemType();
              
              
                    item.BillItemTypeId = ( int)dataRow["BillItemTypeId"] ;
                  
                    item.InvoiceItemTypeId = ( int)dataRow["InvoiceItemTypeId"] ;
                  
                  if(!dataRow.IsNull("Name"))
                
                    item.Name = ( String)dataRow["Name"] ;
                  
                  if(!dataRow.IsNull("IsCountable"))
                
                    item.IsCountable = ( bool)dataRow["IsCountable"] ;
                  
                  if(!dataRow.IsNull("IsPresetRate"))
                
                    item.IsPresetRate = ( bool)dataRow["IsPresetRate"] ;
                  
                  if(!dataRow.IsNull("IsSingle"))
                
                    item.IsSingle = ( bool)dataRow["IsSingle"] ;
                  
                  if(!dataRow.IsNull("IsAttachRequired"))
                
                    item.IsAttachRequired = ( bool)dataRow["IsAttachRequired"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[BillItemType]
    
      Where
      
        [BillItemTypeId] = @billItemTypeId";
    [Synchronized]
    public override BillItemType remove(BillItemType billItemType)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@billItemTypeId", billItemType.BillItemTypeId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(billItemType,DataMapperOperation.delete);

      return registerRecord(billItemType);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override BillItemType save( BillItemType billItemType )
    {
      if(exists(billItemType))
        return update(billItemType);
        return create(billItemType);
    }

  
      const String SqlUpdate = @"Update [dbo].[BillItemType] Set
      
        [InvoiceItemTypeId] = @invoiceItemTypeId,
        [Name] = @name,
        [IsCountable] = @isCountable,
        [IsPresetRate] = @isPresetRate,
        [IsSingle] = @isSingle,
        [IsAttachRequired] = @isAttachRequired,
        [Deleted] = @deleted
        Where
        
          [BillItemTypeId] = @billItemTypeId";
    [TransactionRequired]
    [Synchronized]
    public override BillItemType update(BillItemType billItemType)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@billItemTypeId", billItemType.BillItemTypeId);
            
              sqlCommand.Parameters.AddWithValue("@invoiceItemTypeId", billItemType.InvoiceItemTypeId);
            
                  if(billItemType.Name != null)
                
              sqlCommand.Parameters.AddWithValue("@name", billItemType.Name);
              else
              sqlCommand.Parameters.AddWithValue("@name", DBNull.Value);
            
                  if(billItemType.IsCountable.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@isCountable", billItemType.IsCountable);
              else
              sqlCommand.Parameters.AddWithValue("@isCountable", DBNull.Value);
            
                  if(billItemType.IsPresetRate.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@isPresetRate", billItemType.IsPresetRate);
              else
              sqlCommand.Parameters.AddWithValue("@isPresetRate", DBNull.Value);
            
                  if(billItemType.IsSingle.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@isSingle", billItemType.IsSingle);
              else
              sqlCommand.Parameters.AddWithValue("@isSingle", DBNull.Value);
            
                  if(billItemType.IsAttachRequired.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@isAttachRequired", billItemType.IsAttachRequired);
              else
              sqlCommand.Parameters.AddWithValue("@isAttachRequired", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@deleted", billItemType.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(billItemType.relatedBillItem != null
              && billItemType.relatedBillItem.Count > 0)
              {
              BillItemDataMapper dataMapper = new BillItemDataMapper(Database);

              foreach(BillItem item in billItemType.relatedBillItem)
              dataMapper.save(item);
              }
            
              if(billItemType.relatedRateByAssignment != null
              && billItemType.relatedRateByAssignment.Count > 0)
              {
              RateByAssignmentDataMapper dataMapper = new RateByAssignmentDataMapper(Database);

              foreach(RateByAssignment item in billItemType.relatedRateByAssignment)
              dataMapper.save(item);
              }
            

        raiseAffected(billItemType,DataMapperOperation.update);
        
    
        return registerRecord(billItemType);

    }

  
    }
    
  
    
    public partial class SubAfe: DomainObject
    {
    
      protected String _subAFE;
    
      protected String _shortName;
    
      protected bool _deleted;
    
      protected bool _temporary;
    

      // parent tables
      protected Afe _relatedAfe
        = new Afe()
      ;
    

      // parent tables
      protected SubAfeStatus _relatedSubAfeStatus
        = new SubAfeStatus()
      ;
    

    public SubAfe(){}

    public SubAfe(
    String 
            subAFE,String 
            aFE,String 
            subAFEStatus,String 
            shortName,bool 
            deleted,bool 
            temporary
    )
    {
    
      this.SubAFE = subAFE;
    
      this.AFE = aFE;
    
      this.SubAFEStatus = subAFEStatus;
    
      this.ShortName = shortName;
    
      this.Deleted = deleted;
    
      this.Temporary = temporary;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.SubAfe."
    
      + SubAFE.ToString()
    ;
    
    return uri;
    }

    

      public String SubAFE
      {
        
            get { return _subAFE;}
            set 
            { 
                _subAFE = value;
            }
          
      }
    

      public String AFE
      {
        
            get
            {
            
                  if(_relatedAfe != null)
                    return _relatedAfe.AFE;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAfe == null)
                        _relatedAfe = new Afe();

                      _relatedAfe.AFE = value;
                    
            }
          
      }
    

      public String SubAFEStatus
      {
        
            get
            {
            
                  if(_relatedSubAfeStatus != null)
                    return _relatedSubAfeStatus.SubAFEStatus;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedSubAfeStatus == null)
                        _relatedSubAfeStatus = new SubAfeStatus();

                      _relatedSubAfeStatus.SubAFEStatus = value;
                    
            }
          
      }
    

      public String ShortName
      {
        
            get { return _shortName;}
            set 
            { 
                _shortName = value;
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    

      public bool Temporary
      {
        
            get { return _temporary;}
            set 
            { 
                _temporary = value;
            }
          
      }
    

      public Afe RelatedAfe
      {
      get { return _relatedAfe;}
      set { _relatedAfe = value; }
      }
      
    

      public SubAfeStatus RelatedSubAfeStatus
      {
      get { return _relatedSubAfeStatus;}
      set { _relatedSubAfeStatus = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      SubAfe subAfe = new SubAfe();
      
      subAfe.SubAFE = this.SubAFE;
      subAfe.AFE = this.AFE;
      subAfe.SubAFEStatus = this.SubAFEStatus;
      subAfe.ShortName = this.ShortName;
      subAfe.Deleted = this.Deleted;
      subAfe.Temporary = this.Temporary;
      subAfe.ActiveRecordId = this.ActiveRecordId; 
      return subAfe;
    }

    
          // one to many relation
          private List<AssetAssignment> _relatedAssetAssignment;

          public List<AssetAssignment> relatedAssetAssignment
          {
          get { return _relatedAssetAssignment;}
          set { _relatedAssetAssignment = value; }
          }
          
          
          public AssetAssignment addRelatedAssetAssignmentItem(
          AssetAssignment assetAssignment)
          {
            assetAssignment.RelatedSubAfe = this;
            
            _relatedAssetAssignment.Add(assetAssignment);
            
            return assetAssignment;
          }
            
        
    }
  

    public abstract class _SubAfeDataMapper:TDataMapper<SubAfe,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _SubAfeDataMapper(){}
      public _SubAfeDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[SubAfe]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[SubAfe] (
    
      [SubAFE]
      ,
      [AFE]
      ,
      [SubAFEStatus]
      ,
      [ShortName]
      ,
      [Deleted]
      ,
      [Temporary]
      ) Values (
    
      @subAFE,
      @aFE,
      @subAFEStatus,
      @shortName,
      @deleted,
      @temporary);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override SubAfe create( SubAfe subAfe )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@subAFE", subAfe.SubAFE);
              
                  sqlCommand.Parameters.AddWithValue("@aFE", subAfe.AFE);
              
                  sqlCommand.Parameters.AddWithValue("@subAFEStatus", subAfe.SubAFEStatus);
              
                  sqlCommand.Parameters.AddWithValue("@shortName", subAfe.ShortName);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", subAfe.Deleted);
              
                  sqlCommand.Parameters.AddWithValue("@temporary", subAfe.Temporary);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(subAfe.relatedAssetAssignment != null 
            && subAfe.relatedAssetAssignment.Count > 0)
          {
            AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(Database);
            
            foreach(AssetAssignment item in subAfe.relatedAssetAssignment)
              dataMapper.create(item);
          }
        
      
      raiseAffected(subAfe,DataMapperOperation.create);

      return registerRecord(subAfe);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [SubAFE] ,
      [AFE] ,
      [SubAFEStatus] ,
      [ShortName] ,
      [Deleted] ,
      [Temporary] 
     From [dbo].[SubAfe]
    
       Where 
      
         [SubAFE] = @subAFE
    ";

    public SubAfe findByPrimaryKey(
    String subAFE
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@subAFE", subAFE);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("SubAfe not found, search by primary key");
 

    }


    public bool exists(SubAfe subAfe)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@SubAFE", subAfe.SubAFE);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [SubAfe].[SubAFE] = @CheckInSubAFE";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      SubAfe _SubAfe = (SubAfe)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInSubAFE", _SubAfe.SubAFE);
      

      return sqlCommand;
    }

  

    protected override SubAfe doLoad(IDataReader dataReader)
    {
    SubAfe subAfe = new SubAfe();

    subAfe.SubAFE = (String) dataReader.GetValue(0);
            subAfe.AFE = (String) dataReader.GetValue(1);
            subAfe.SubAFEStatus = (String) dataReader.GetValue(2);
            subAfe.ShortName = (String) dataReader.GetValue(3);
            subAfe.Deleted = (bool) dataReader.GetValue(4);
            subAfe.Temporary = (bool) dataReader.GetValue(5);
            

    
    
    return registerRecord(subAfe);
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
          
        
        if(hashtable.ContainsKey("ShortName"))
        subAfe.ShortName = ( String)hashtable["ShortName"];
          
        
        if(hashtable.ContainsKey("Deleted"))
        subAfe.Deleted = ( bool)hashtable["Deleted"];
          
        
        if(hashtable.ContainsKey("Temporary"))
        subAfe.Temporary = ( bool)hashtable["Temporary"];
          

      return subAfe;
    }


    protected override List<SubAfe> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<SubAfe> resultList = new List<SubAfe>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              SubAfe item = new SubAfe();
              
              
                    item.SubAFE = ( String)dataRow["SubAFE"] ;
                  
                    item.AFE = ( String)dataRow["AFE"] ;
                  
                    item.SubAFEStatus = ( String)dataRow["SubAFEStatus"] ;
                  
                    item.ShortName = ( String)dataRow["ShortName"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
                    item.Temporary = ( bool)dataRow["Temporary"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[SubAfe]
    
      Where
      
        [SubAFE] = @subAFE";
    [Synchronized]
    public override SubAfe remove(SubAfe subAfe)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@subAFE", subAfe.SubAFE);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(subAfe,DataMapperOperation.delete);

      return registerRecord(subAfe);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override SubAfe save( SubAfe subAfe )
    {
      if(exists(subAfe))
        return update(subAfe);
        return create(subAfe);
    }

  
      const String SqlUpdate = @"Update [dbo].[SubAfe] Set
      
        [AFE] = @aFE,
        [SubAFEStatus] = @subAFEStatus,
        [ShortName] = @shortName,
        [Deleted] = @deleted,
        [Temporary] = @temporary
        Where
        
          [SubAFE] = @subAFE";
    [TransactionRequired]
    [Synchronized]
    public override SubAfe update(SubAfe subAfe)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@subAFE", subAfe.SubAFE);
            
              sqlCommand.Parameters.AddWithValue("@aFE", subAfe.AFE);
            
              sqlCommand.Parameters.AddWithValue("@subAFEStatus", subAfe.SubAFEStatus);
            
              sqlCommand.Parameters.AddWithValue("@shortName", subAfe.ShortName);
            
              sqlCommand.Parameters.AddWithValue("@deleted", subAfe.Deleted);
            
              sqlCommand.Parameters.AddWithValue("@temporary", subAfe.Temporary);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(subAfe.relatedAssetAssignment != null
              && subAfe.relatedAssetAssignment.Count > 0)
              {
              AssetAssignmentDataMapper dataMapper = new AssetAssignmentDataMapper(Database);

              foreach(AssetAssignment item in subAfe.relatedAssetAssignment)
              dataMapper.save(item);
              }
            

        raiseAffected(subAfe,DataMapperOperation.update);
        
    
        return registerRecord(subAfe);

    }

  
    }
    
  
    
    public partial class InvoiceStatus: DomainObject
    {
    
      protected String _status;
    

    public InvoiceStatus(){}

    public InvoiceStatus(
    String 
            status
    )
    {
    
      this.Status = status;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.InvoiceStatus."
    
      + Status.ToString()
    ;
    
    return uri;
    }

    

      public String Status
      {
        
            get { return _status;}
            set 
            { 
                _status = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      InvoiceStatus invoiceStatus = new InvoiceStatus();
      
      invoiceStatus.Status = this.Status;
      invoiceStatus.ActiveRecordId = this.ActiveRecordId; 
      return invoiceStatus;
    }

    
          // one to many relation
          private List<Invoice> _relatedInvoice;

          public List<Invoice> relatedInvoice
          {
          get { return _relatedInvoice;}
          set { _relatedInvoice = value; }
          }
          
          
          public Invoice addRelatedInvoiceItem(
          Invoice invoice)
          {
            invoice.RelatedInvoiceStatus = this;
            
            _relatedInvoice.Add(invoice);
            
            return invoice;
          }
            
        
    }
  

    public abstract class _InvoiceStatusDataMapper:TDataMapper<InvoiceStatus,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _InvoiceStatusDataMapper(){}
      public _InvoiceStatusDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[InvoiceStatus]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[InvoiceStatus] (
    
      [Status]
      ) Values (
    
      @status);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override InvoiceStatus create( InvoiceStatus invoiceStatus )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@status", invoiceStatus.Status);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(invoiceStatus.relatedInvoice != null 
            && invoiceStatus.relatedInvoice.Count > 0)
          {
            InvoiceDataMapper dataMapper = new InvoiceDataMapper(Database);
            
            foreach(Invoice item in invoiceStatus.relatedInvoice)
              dataMapper.create(item);
          }
        
      
      raiseAffected(invoiceStatus,DataMapperOperation.create);

      return registerRecord(invoiceStatus);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [Status] 
     From [dbo].[InvoiceStatus]
    
       Where 
      
         [Status] = @status
    ";

    public InvoiceStatus findByPrimaryKey(
    String status
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@status", status);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("InvoiceStatus not found, search by primary key");
 

    }


    public bool exists(InvoiceStatus invoiceStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@Status", invoiceStatus.Status);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [InvoiceStatus].[Status] = @CheckInStatus";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      InvoiceStatus _InvoiceStatus = (InvoiceStatus)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInStatus", _InvoiceStatus.Status);
      

      return sqlCommand;
    }

  

    protected override InvoiceStatus doLoad(IDataReader dataReader)
    {
    InvoiceStatus invoiceStatus = new InvoiceStatus();

    invoiceStatus.Status = (String) dataReader.GetValue(0);
            

    
    
    return registerRecord(invoiceStatus);
    }


    protected override InvoiceStatus doLoad(Hashtable hashtable)
    {
      InvoiceStatus invoiceStatus = new InvoiceStatus();

      
        
        if(hashtable.ContainsKey("Status"))
        invoiceStatus.Status = ( String)hashtable["Status"];
          

      return invoiceStatus;
    }


    protected override List<InvoiceStatus> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<InvoiceStatus> resultList = new List<InvoiceStatus>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              InvoiceStatus item = new InvoiceStatus();
              
              
                    item.Status = ( String)dataRow["Status"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[InvoiceStatus]
    
      Where
      
        [Status] = @status";
    [Synchronized]
    public override InvoiceStatus remove(InvoiceStatus invoiceStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@status", invoiceStatus.Status);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(invoiceStatus,DataMapperOperation.delete);

      return registerRecord(invoiceStatus);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override InvoiceStatus save( InvoiceStatus invoiceStatus )
    {
      if(exists(invoiceStatus))
        return update(invoiceStatus);
        return create(invoiceStatus);
    }

  [TransactionRequired]
    [Synchronized]
    public override InvoiceStatus update(InvoiceStatus invoiceStatus)
    {
      
    
        return registerRecord(invoiceStatus);

    }

  
    }
    
  
    
    public partial class InvoiceItemStatus: DomainObject
    {
    
      protected String _status;
    

    public InvoiceItemStatus(){}

    public InvoiceItemStatus(
    String 
            status
    )
    {
    
      this.Status = status;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.InvoiceItemStatus."
    
      + Status.ToString()
    ;
    
    return uri;
    }

    

      public String Status
      {
        
            get { return _status;}
            set 
            { 
                _status = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      InvoiceItemStatus invoiceItemStatus = new InvoiceItemStatus();
      
      invoiceItemStatus.Status = this.Status;
      invoiceItemStatus.ActiveRecordId = this.ActiveRecordId; 
      return invoiceItemStatus;
    }

    
          // one to many relation
          private List<InvoiceItem> _relatedInvoiceItem;

          public List<InvoiceItem> relatedInvoiceItem
          {
          get { return _relatedInvoiceItem;}
          set { _relatedInvoiceItem = value; }
          }
          
          
          public InvoiceItem addRelatedInvoiceItemItem(
          InvoiceItem invoiceItem)
          {
            invoiceItem.RelatedInvoiceItemStatus = this;
            
            _relatedInvoiceItem.Add(invoiceItem);
            
            return invoiceItem;
          }
            
        
    }
  

    public abstract class _InvoiceItemStatusDataMapper:TDataMapper<InvoiceItemStatus,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _InvoiceItemStatusDataMapper(){}
      public _InvoiceItemStatusDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[InvoiceItemStatus]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[InvoiceItemStatus] (
    
      [Status]
      ) Values (
    
      @status);

    ";
    
    [TransactionRequired]
    [Synchronized]
    public override InvoiceItemStatus create( InvoiceItemStatus invoiceItemStatus )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@status", invoiceItemStatus.Status);
              
              sqlCommand.ExecuteNonQuery();
            
        }
      }
      
    
          
          if(invoiceItemStatus.relatedInvoiceItem != null 
            && invoiceItemStatus.relatedInvoiceItem.Count > 0)
          {
            InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);
            
            foreach(InvoiceItem item in invoiceItemStatus.relatedInvoiceItem)
              dataMapper.create(item);
          }
        
      
      raiseAffected(invoiceItemStatus,DataMapperOperation.create);

      return registerRecord(invoiceItemStatus);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [Status] 
     From [dbo].[InvoiceItemStatus]
    
       Where 
      
         [Status] = @status
    ";

    public InvoiceItemStatus findByPrimaryKey(
    String status
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@status", status);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("InvoiceItemStatus not found, search by primary key");
 

    }


    public bool exists(InvoiceItemStatus invoiceItemStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@Status", invoiceItemStatus.Status);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [InvoiceItemStatus].[Status] = @CheckInStatus";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      InvoiceItemStatus _InvoiceItemStatus = (InvoiceItemStatus)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInStatus", _InvoiceItemStatus.Status);
      

      return sqlCommand;
    }

  

    protected override InvoiceItemStatus doLoad(IDataReader dataReader)
    {
    InvoiceItemStatus invoiceItemStatus = new InvoiceItemStatus();

    invoiceItemStatus.Status = (String) dataReader.GetValue(0);
            

    
    
    return registerRecord(invoiceItemStatus);
    }


    protected override InvoiceItemStatus doLoad(Hashtable hashtable)
    {
      InvoiceItemStatus invoiceItemStatus = new InvoiceItemStatus();

      
        
        if(hashtable.ContainsKey("Status"))
        invoiceItemStatus.Status = ( String)hashtable["Status"];
          

      return invoiceItemStatus;
    }


    protected override List<InvoiceItemStatus> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<InvoiceItemStatus> resultList = new List<InvoiceItemStatus>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              InvoiceItemStatus item = new InvoiceItemStatus();
              
              
                    item.Status = ( String)dataRow["Status"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[InvoiceItemStatus]
    
      Where
      
        [Status] = @status";
    [Synchronized]
    public override InvoiceItemStatus remove(InvoiceItemStatus invoiceItemStatus)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@status", invoiceItemStatus.Status);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(invoiceItemStatus,DataMapperOperation.delete);

      return registerRecord(invoiceItemStatus);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override InvoiceItemStatus save( InvoiceItemStatus invoiceItemStatus )
    {
      if(exists(invoiceItemStatus))
        return update(invoiceItemStatus);
        return create(invoiceItemStatus);
    }

  [TransactionRequired]
    [Synchronized]
    public override InvoiceItemStatus update(InvoiceItemStatus invoiceItemStatus)
    {
      
    
        return registerRecord(invoiceItemStatus);

    }

  
    }
    
  
    
    public partial class Invoice: DomainObject
    {
    
      protected int _invoiceId;
    
      protected String _invoiceNumber;
    
      protected String _clientName;
    
      protected String _clientAddress;
    
      protected bool _clientActive;
    
      protected String _notes;
    
      protected String _startDate;
    
      protected int _totalDailyAmt;
    
      protected decimal _dailyInvoiceAmt;
    
      protected decimal _otherInvoiceAmt;
    
      protected decimal _totalInvoiceAmt;
    

      // parent tables
      protected Client _relatedClient
        = new Client()
      ;
    

      // parent tables
      protected InvoiceStatus _relatedInvoiceStatus
        = new InvoiceStatus()
      ;
    

    public Invoice(){}

    public Invoice(
    int 
            invoiceId,String 
            invoiceNumber,int 
            clientId,String 
            clientName,String 
            clientAddress,bool 
            clientActive,String 
            status,String 
            notes,String 
            startDate,int 
            totalDailyAmt,decimal 
            dailyInvoiceAmt,decimal 
            otherInvoiceAmt,decimal 
            totalInvoiceAmt
    )
    {
    
      this.InvoiceId = invoiceId;
    
      this.InvoiceNumber = invoiceNumber;
    
      this.ClientId = clientId;
    
      this.ClientName = clientName;
    
      this.ClientAddress = clientAddress;
    
      this.ClientActive = clientActive;
    
      this.Status = status;
    
      this.Notes = notes;
    
      this.StartDate = startDate;
    
      this.TotalDailyAmt = totalDailyAmt;
    
      this.DailyInvoiceAmt = dailyInvoiceAmt;
    
      this.OtherInvoiceAmt = otherInvoiceAmt;
    
      this.TotalInvoiceAmt = totalInvoiceAmt;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Invoice."
    
      + InvoiceId.ToString()
    ;
    
    return uri;
    }

    

      public int InvoiceId
      {
        
            get { return _invoiceId;}
            set 
            { 
                _invoiceId = value;
            }
          
      }
    

      public String InvoiceNumber
      {
        
            get { return _invoiceNumber;}
            set 
            { 
                _invoiceNumber = value;
            }
          
      }
    

      public int ClientId
      {
        
            get
            {
            
                  if(_relatedClient != null)
                    return _relatedClient.ClientId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedClient == null)
                        _relatedClient = new Client();

                      _relatedClient.ClientId = value;
                    
            }
          
      }
    

      public String ClientName
      {
        
            get { return _clientName;}
            set 
            { 
                _clientName = value;
            }
          
      }
    

      public String ClientAddress
      {
        
            get { return _clientAddress;}
            set 
            { 
                _clientAddress = value;
            }
          
      }
    

      public bool ClientActive
      {
        
            get { return _clientActive;}
            set 
            { 
                _clientActive = value;
            }
          
      }
    

      public String Status
      {
        
            get
            {
            
                  if(_relatedInvoiceStatus != null)
                    return _relatedInvoiceStatus.Status;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedInvoiceStatus == null)
                        _relatedInvoiceStatus = new InvoiceStatus();

                      _relatedInvoiceStatus.Status = value;
                    
            }
          
      }
    

      public String Notes
      {
        
            get { return _notes;}
            set 
            { 
                _notes = value;
            }
          
      }
    

      public String StartDate
      {
        
            get { return _startDate;}
            set 
            { 
                _startDate = value;
            }
          
      }
    

      public int TotalDailyAmt
      {
        
            get { return _totalDailyAmt;}
            set 
            { 
                _totalDailyAmt = value;
            }
          
      }
    

      public decimal DailyInvoiceAmt
      {
        
            get { return _dailyInvoiceAmt;}
            set 
            { 
                _dailyInvoiceAmt = value;
            }
          
      }
    

      public decimal OtherInvoiceAmt
      {
        
            get { return _otherInvoiceAmt;}
            set 
            { 
                _otherInvoiceAmt = value;
            }
          
      }
    

      public decimal TotalInvoiceAmt
      {
        
            get { return _totalInvoiceAmt;}
            set 
            { 
                _totalInvoiceAmt = value;
            }
          
      }
    

      public Client RelatedClient
      {
      get { return _relatedClient;}
      set { _relatedClient = value; }
      }
      
    

      public InvoiceStatus RelatedInvoiceStatus
      {
      get { return _relatedInvoiceStatus;}
      set { _relatedInvoiceStatus = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      Invoice invoice = new Invoice();
      
      invoice.InvoiceId = this.InvoiceId;
      invoice.InvoiceNumber = this.InvoiceNumber;
      invoice.ClientId = this.ClientId;
      invoice.ClientName = this.ClientName;
      invoice.ClientAddress = this.ClientAddress;
      invoice.ClientActive = this.ClientActive;
      invoice.Status = this.Status;
      invoice.Notes = this.Notes;
      invoice.StartDate = this.StartDate;
      invoice.TotalDailyAmt = this.TotalDailyAmt;
      invoice.DailyInvoiceAmt = this.DailyInvoiceAmt;
      invoice.OtherInvoiceAmt = this.OtherInvoiceAmt;
      invoice.TotalInvoiceAmt = this.TotalInvoiceAmt;
      invoice.ActiveRecordId = this.ActiveRecordId; 
      return invoice;
    }

    
          // one to many relation
          private List<InvoiceItem> _relatedInvoiceItem;

          public List<InvoiceItem> relatedInvoiceItem
          {
          get { return _relatedInvoiceItem;}
          set { _relatedInvoiceItem = value; }
          }
          
          
          public InvoiceItem addRelatedInvoiceItemItem(
          InvoiceItem invoiceItem)
          {
            invoiceItem.RelatedInvoice = this;
            
            _relatedInvoiceItem.Add(invoiceItem);
            
            return invoiceItem;
          }
            
        
    }
  

    public abstract class _InvoiceDataMapper:TDataMapper<Invoice,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _InvoiceDataMapper(){}
      public _InvoiceDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Invoice]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Invoice] (
    
      [InvoiceNumber]
      ,
      [ClientId]
      ,
      [ClientName]
      ,
      [ClientAddress]
      ,
      [ClientActive]
      ,
      [Status]
      ,
      [Notes]
      ,
      [StartDate]
      ,
      [TotalDailyAmt]
      ,
      [DailyInvoiceAmt]
      ,
      [OtherInvoiceAmt]
      ,
      [TotalInvoiceAmt]
      ) Values (
    
      @invoiceNumber,
      @clientId,
      @clientName,
      @clientAddress,
      @clientActive,
      @status,
      @notes,
      @startDate,
      @totalDailyAmt,
      @dailyInvoiceAmt,
      @otherInvoiceAmt,
      @totalInvoiceAmt);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override Invoice create( Invoice invoice )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(invoice.InvoiceNumber != null)
                  
                  sqlCommand.Parameters.AddWithValue("@invoiceNumber", invoice.InvoiceNumber);
                else
                  sqlCommand.Parameters.AddWithValue("@invoiceNumber", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@clientId", invoice.ClientId);
              
                  sqlCommand.Parameters.AddWithValue("@clientName", invoice.ClientName);
              
                  sqlCommand.Parameters.AddWithValue("@clientAddress", invoice.ClientAddress);
              
                  sqlCommand.Parameters.AddWithValue("@clientActive", invoice.ClientActive);
              
                  sqlCommand.Parameters.AddWithValue("@status", invoice.Status);
              
                    if(invoice.Notes != null)
                  
                  sqlCommand.Parameters.AddWithValue("@notes", invoice.Notes);
                else
                  sqlCommand.Parameters.AddWithValue("@notes", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@startDate", invoice.StartDate);
              
                  sqlCommand.Parameters.AddWithValue("@totalDailyAmt", invoice.TotalDailyAmt);
              
                  sqlCommand.Parameters.AddWithValue("@dailyInvoiceAmt", invoice.DailyInvoiceAmt);
              
                  sqlCommand.Parameters.AddWithValue("@otherInvoiceAmt", invoice.OtherInvoiceAmt);
              
                  sqlCommand.Parameters.AddWithValue("@totalInvoiceAmt", invoice.TotalInvoiceAmt);
              invoice.InvoiceId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(invoice.relatedInvoiceItem != null 
            && invoice.relatedInvoiceItem.Count > 0)
          {
            InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);
            
            foreach(InvoiceItem item in invoice.relatedInvoiceItem)
              dataMapper.create(item);
          }
        
      
      raiseAffected(invoice,DataMapperOperation.create);

      return registerRecord(invoice);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [InvoiceId] ,
      [InvoiceNumber] ,
      [ClientId] ,
      [ClientName] ,
      [ClientAddress] ,
      [ClientActive] ,
      [Status] ,
      [Notes] ,
      [StartDate] ,
      [TotalDailyAmt] ,
      [DailyInvoiceAmt] ,
      [OtherInvoiceAmt] ,
      [TotalInvoiceAmt] 
     From [dbo].[Invoice]
    
       Where 
      
         [InvoiceId] = @invoiceId
    ";

    public Invoice findByPrimaryKey(
    int invoiceId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@invoiceId", invoiceId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Invoice not found, search by primary key");
 

    }


    public bool exists(Invoice invoice)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@InvoiceId", invoice.InvoiceId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Invoice].[InvoiceId] = @CheckInInvoiceId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Invoice _Invoice = (Invoice)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInInvoiceId", _Invoice.InvoiceId);
      

      return sqlCommand;
    }

  

    protected override Invoice doLoad(IDataReader dataReader)
    {
    Invoice invoice = new Invoice();

    invoice.InvoiceId = (int) dataReader.GetValue(0);
            
          if(!dataReader.IsDBNull(1))        
          invoice.InvoiceNumber = (String) dataReader.GetValue(1);
            invoice.ClientId = (int) dataReader.GetValue(2);
            invoice.ClientName = (String) dataReader.GetValue(3);
            invoice.ClientAddress = (String) dataReader.GetValue(4);
            invoice.ClientActive = (bool) dataReader.GetValue(5);
            invoice.Status = (String) dataReader.GetValue(6);
            
          if(!dataReader.IsDBNull(7))        
          invoice.Notes = (String) dataReader.GetValue(7);
            invoice.StartDate = (String) dataReader.GetValue(8);
            invoice.TotalDailyAmt = (int) dataReader.GetValue(9);
            invoice.DailyInvoiceAmt = (decimal) dataReader.GetValue(10);
            invoice.OtherInvoiceAmt = (decimal) dataReader.GetValue(11);
            invoice.TotalInvoiceAmt = (decimal) dataReader.GetValue(12);
            

    
    
    return registerRecord(invoice);
    }


    protected override Invoice doLoad(Hashtable hashtable)
    {
      Invoice invoice = new Invoice();

      
        
        if(hashtable.ContainsKey("InvoiceId"))
        invoice.InvoiceId = ( int)hashtable["InvoiceId"];
          
        
        if(hashtable.ContainsKey("InvoiceNumber"))
        invoice.InvoiceNumber = ( String)hashtable["InvoiceNumber"];
          
        
        if(hashtable.ContainsKey("ClientId"))
        invoice.ClientId = ( int)hashtable["ClientId"];
          
        
        if(hashtable.ContainsKey("ClientName"))
        invoice.ClientName = ( String)hashtable["ClientName"];
          
        
        if(hashtable.ContainsKey("ClientAddress"))
        invoice.ClientAddress = ( String)hashtable["ClientAddress"];
          
        
        if(hashtable.ContainsKey("ClientActive"))
        invoice.ClientActive = ( bool)hashtable["ClientActive"];
          
        
        if(hashtable.ContainsKey("Status"))
        invoice.Status = ( String)hashtable["Status"];
          
        
        if(hashtable.ContainsKey("Notes"))
        invoice.Notes = ( String)hashtable["Notes"];
          
        
        if(hashtable.ContainsKey("StartDate"))
        invoice.StartDate = ( String)hashtable["StartDate"];
          
        
        if(hashtable.ContainsKey("TotalDailyAmt"))
        invoice.TotalDailyAmt = ( int)hashtable["TotalDailyAmt"];
          
        
        if(hashtable.ContainsKey("DailyInvoiceAmt"))
        invoice.DailyInvoiceAmt = ( decimal)hashtable["DailyInvoiceAmt"];
          
        
        if(hashtable.ContainsKey("OtherInvoiceAmt"))
        invoice.OtherInvoiceAmt = ( decimal)hashtable["OtherInvoiceAmt"];
          
        
        if(hashtable.ContainsKey("TotalInvoiceAmt"))
        invoice.TotalInvoiceAmt = ( decimal)hashtable["TotalInvoiceAmt"];
          

      return invoice;
    }


    protected override List<Invoice> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Invoice> resultList = new List<Invoice>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Invoice item = new Invoice();
              
              
                    item.InvoiceId = ( int)dataRow["InvoiceId"] ;
                  
                  if(!dataRow.IsNull("InvoiceNumber"))
                
                    item.InvoiceNumber = ( String)dataRow["InvoiceNumber"] ;
                  
                    item.ClientId = ( int)dataRow["ClientId"] ;
                  
                    item.ClientName = ( String)dataRow["ClientName"] ;
                  
                    item.ClientAddress = ( String)dataRow["ClientAddress"] ;
                  
                    item.ClientActive = ( bool)dataRow["ClientActive"] ;
                  
                    item.Status = ( String)dataRow["Status"] ;
                  
                  if(!dataRow.IsNull("Notes"))
                
                    item.Notes = ( String)dataRow["Notes"] ;
                  
                    item.StartDate = ( String)dataRow["StartDate"] ;
                  
                    item.TotalDailyAmt = ( int)dataRow["TotalDailyAmt"] ;
                  
                    item.DailyInvoiceAmt = ( decimal)dataRow["DailyInvoiceAmt"] ;
                  
                    item.OtherInvoiceAmt = ( decimal)dataRow["OtherInvoiceAmt"] ;
                  
                    item.TotalInvoiceAmt = ( decimal)dataRow["TotalInvoiceAmt"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Invoice]
    
      Where
      
        [InvoiceId] = @invoiceId";
    [Synchronized]
    public override Invoice remove(Invoice invoice)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@invoiceId", invoice.InvoiceId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(invoice,DataMapperOperation.delete);

      return registerRecord(invoice);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Invoice save( Invoice invoice )
    {
      if(exists(invoice))
        return update(invoice);
        return create(invoice);
    }

  
      const String SqlUpdate = @"Update [dbo].[Invoice] Set
      
        [InvoiceNumber] = @invoiceNumber,
        [ClientId] = @clientId,
        [ClientName] = @clientName,
        [ClientAddress] = @clientAddress,
        [ClientActive] = @clientActive,
        [Status] = @status,
        [Notes] = @notes,
        [StartDate] = @startDate,
        [TotalDailyAmt] = @totalDailyAmt,
        [DailyInvoiceAmt] = @dailyInvoiceAmt,
        [OtherInvoiceAmt] = @otherInvoiceAmt,
        [TotalInvoiceAmt] = @totalInvoiceAmt
        Where
        
          [InvoiceId] = @invoiceId";
    [TransactionRequired]
    [Synchronized]
    public override Invoice update(Invoice invoice)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@invoiceId", invoice.InvoiceId);
            
                  if(invoice.InvoiceNumber != null)
                
              sqlCommand.Parameters.AddWithValue("@invoiceNumber", invoice.InvoiceNumber);
              else
              sqlCommand.Parameters.AddWithValue("@invoiceNumber", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@clientId", invoice.ClientId);
            
              sqlCommand.Parameters.AddWithValue("@clientName", invoice.ClientName);
            
              sqlCommand.Parameters.AddWithValue("@clientAddress", invoice.ClientAddress);
            
              sqlCommand.Parameters.AddWithValue("@clientActive", invoice.ClientActive);
            
              sqlCommand.Parameters.AddWithValue("@status", invoice.Status);
            
                  if(invoice.Notes != null)
                
              sqlCommand.Parameters.AddWithValue("@notes", invoice.Notes);
              else
              sqlCommand.Parameters.AddWithValue("@notes", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@startDate", invoice.StartDate);
            
              sqlCommand.Parameters.AddWithValue("@totalDailyAmt", invoice.TotalDailyAmt);
            
              sqlCommand.Parameters.AddWithValue("@dailyInvoiceAmt", invoice.DailyInvoiceAmt);
            
              sqlCommand.Parameters.AddWithValue("@otherInvoiceAmt", invoice.OtherInvoiceAmt);
            
              sqlCommand.Parameters.AddWithValue("@totalInvoiceAmt", invoice.TotalInvoiceAmt);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(invoice.relatedInvoiceItem != null
              && invoice.relatedInvoiceItem.Count > 0)
              {
              InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);

              foreach(InvoiceItem item in invoice.relatedInvoiceItem)
              dataMapper.save(item);
              }
            

        raiseAffected(invoice,DataMapperOperation.update);
        
    
        return registerRecord(invoice);

    }

  
    }
    
  
    
    public partial class AssetAssignment: DomainObject
    {
    
      protected int _assetAssignmentId;
    
      protected bool _deleted;
    

      // parent tables
      protected Afe _relatedAfe
        = new Afe()
      ;
    

      // parent tables
      protected Asset _relatedAsset
        = new Asset()
      ;
    

      // parent tables
      protected SubAfe _relatedSubAfe
        = new SubAfe()
      ;
    

    public AssetAssignment(){}

    public AssetAssignment(
    int 
            assetAssignmentId,String 
            aFE,String 
            subAFE,int 
            assetId,bool 
            deleted
    )
    {
    
      this.AssetAssignmentId = assetAssignmentId;
    
      this.AFE = aFE;
    
      this.SubAFE = subAFE;
    
      this.AssetId = assetId;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.AssetAssignment."
    
      + AssetAssignmentId.ToString()
    ;
    
    return uri;
    }

    

      public int AssetAssignmentId
      {
        
            get { return _assetAssignmentId;}
            set 
            { 
                _assetAssignmentId = value;
            }
          
      }
    

      public String AFE
      {
        
            get
            {
            
                  if(_relatedAfe != null)
                    return _relatedAfe.AFE;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAfe == null)
                        _relatedAfe = new Afe();

                      _relatedAfe.AFE = value;
                    
            }
          
      }
    

      public String SubAFE
      {
        
            get
            {
            
                  if(_relatedSubAfe != null)
                    return _relatedSubAfe.SubAFE;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedSubAfe == null)
                        _relatedSubAfe = new SubAfe();

                      _relatedSubAfe.SubAFE = value;
                    
            }
          
      }
    

      public int AssetId
      {
        
            get
            {
            
                  if(_relatedAsset != null)
                    return _relatedAsset.AssetId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAsset == null)
                        _relatedAsset = new Asset();

                      _relatedAsset.AssetId = value;
                    
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    

      public Afe RelatedAfe
      {
      get { return _relatedAfe;}
      set { _relatedAfe = value; }
      }
      
    

      public Asset RelatedAsset
      {
      get { return _relatedAsset;}
      set { _relatedAsset = value; }
      }
      
    

      public SubAfe RelatedSubAfe
      {
      get { return _relatedSubAfe;}
      set { _relatedSubAfe = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      AssetAssignment assetAssignment = new AssetAssignment();
      
      assetAssignment.AssetAssignmentId = this.AssetAssignmentId;
      assetAssignment.AFE = this.AFE;
      assetAssignment.SubAFE = this.SubAFE;
      assetAssignment.AssetId = this.AssetId;
      assetAssignment.Deleted = this.Deleted;
      assetAssignment.ActiveRecordId = this.ActiveRecordId; 
      return assetAssignment;
    }

    
          // one to many relation
          private List<BillItem> _relatedBillItem;

          public List<BillItem> relatedBillItem
          {
          get { return _relatedBillItem;}
          set { _relatedBillItem = value; }
          }
          
          
          public BillItem addRelatedBillItemItem(
          BillItem billItem)
          {
            billItem.RelatedAssetAssignment = this;
            
            _relatedBillItem.Add(billItem);
            
            return billItem;
          }
            
        
          // one to many relation
          private List<RateByAssignment> _relatedRateByAssignment;

          public List<RateByAssignment> relatedRateByAssignment
          {
          get { return _relatedRateByAssignment;}
          set { _relatedRateByAssignment = value; }
          }
          
          
          public RateByAssignment addRelatedRateByAssignmentItem(
          RateByAssignment rateByAssignment)
          {
            rateByAssignment.RelatedAssetAssignment = this;
            
            _relatedRateByAssignment.Add(rateByAssignment);
            
            return rateByAssignment;
          }
            
        
          // one to many relation
          private List<InvoiceItem> _relatedInvoiceItem;

          public List<InvoiceItem> relatedInvoiceItem
          {
          get { return _relatedInvoiceItem;}
          set { _relatedInvoiceItem = value; }
          }
          
          
          public InvoiceItem addRelatedInvoiceItemItem(
          InvoiceItem invoiceItem)
          {
            invoiceItem.RelatedAssetAssignment = this;
            
            _relatedInvoiceItem.Add(invoiceItem);
            
            return invoiceItem;
          }
            
        
    }
  

    public abstract class _AssetAssignmentDataMapper:TDataMapper<AssetAssignment,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _AssetAssignmentDataMapper(){}
      public _AssetAssignmentDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[AssetAssignment]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[AssetAssignment] (
    
      [AFE]
      ,
      [SubAFE]
      ,
      [AssetId]
      ,
      [Deleted]
      ) Values (
    
      @aFE,
      @subAFE,
      @assetId,
      @deleted);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override AssetAssignment create( AssetAssignment assetAssignment )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@aFE", assetAssignment.AFE);
              
                  sqlCommand.Parameters.AddWithValue("@subAFE", assetAssignment.SubAFE);
              
                  sqlCommand.Parameters.AddWithValue("@assetId", assetAssignment.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", assetAssignment.Deleted);
              assetAssignment.AssetAssignmentId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(assetAssignment.relatedBillItem != null 
            && assetAssignment.relatedBillItem.Count > 0)
          {
            BillItemDataMapper dataMapper = new BillItemDataMapper(Database);
            
            foreach(BillItem item in assetAssignment.relatedBillItem)
              dataMapper.create(item);
          }
        
          
          if(assetAssignment.relatedRateByAssignment != null 
            && assetAssignment.relatedRateByAssignment.Count > 0)
          {
            RateByAssignmentDataMapper dataMapper = new RateByAssignmentDataMapper(Database);
            
            foreach(RateByAssignment item in assetAssignment.relatedRateByAssignment)
              dataMapper.create(item);
          }
        
          
          if(assetAssignment.relatedInvoiceItem != null 
            && assetAssignment.relatedInvoiceItem.Count > 0)
          {
            InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);
            
            foreach(InvoiceItem item in assetAssignment.relatedInvoiceItem)
              dataMapper.create(item);
          }
        
      
      raiseAffected(assetAssignment,DataMapperOperation.create);

      return registerRecord(assetAssignment);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [AssetAssignmentId] ,
      [AFE] ,
      [SubAFE] ,
      [AssetId] ,
      [Deleted] 
     From [dbo].[AssetAssignment]
    
       Where 
      
         [AssetAssignmentId] = @assetAssignmentId
    ";

    public AssetAssignment findByPrimaryKey(
    int assetAssignmentId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@assetAssignmentId", assetAssignmentId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("AssetAssignment not found, search by primary key");
 

    }


    public bool exists(AssetAssignment assetAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@AssetAssignmentId", assetAssignment.AssetAssignmentId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [AssetAssignment].[AssetAssignmentId] = @CheckInAssetAssignmentId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      AssetAssignment _AssetAssignment = (AssetAssignment)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInAssetAssignmentId", _AssetAssignment.AssetAssignmentId);
      

      return sqlCommand;
    }

  

    protected override AssetAssignment doLoad(IDataReader dataReader)
    {
    AssetAssignment assetAssignment = new AssetAssignment();

    assetAssignment.AssetAssignmentId = (int) dataReader.GetValue(0);
            assetAssignment.AFE = (String) dataReader.GetValue(1);
            assetAssignment.SubAFE = (String) dataReader.GetValue(2);
            assetAssignment.AssetId = (int) dataReader.GetValue(3);
            assetAssignment.Deleted = (bool) dataReader.GetValue(4);
            

    
    
    return registerRecord(assetAssignment);
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
          
        
        if(hashtable.ContainsKey("Deleted"))
        assetAssignment.Deleted = ( bool)hashtable["Deleted"];
          

      return assetAssignment;
    }


    protected override List<AssetAssignment> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<AssetAssignment> resultList = new List<AssetAssignment>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              AssetAssignment item = new AssetAssignment();
              
              
                    item.AssetAssignmentId = ( int)dataRow["AssetAssignmentId"] ;
                  
                    item.AFE = ( String)dataRow["AFE"] ;
                  
                    item.SubAFE = ( String)dataRow["SubAFE"] ;
                  
                    item.AssetId = ( int)dataRow["AssetId"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[AssetAssignment]
    
      Where
      
        [AssetAssignmentId] = @assetAssignmentId";
    [Synchronized]
    public override AssetAssignment remove(AssetAssignment assetAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@assetAssignmentId", assetAssignment.AssetAssignmentId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(assetAssignment,DataMapperOperation.delete);

      return registerRecord(assetAssignment);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override AssetAssignment save( AssetAssignment assetAssignment )
    {
      if(exists(assetAssignment))
        return update(assetAssignment);
        return create(assetAssignment);
    }

  
      const String SqlUpdate = @"Update [dbo].[AssetAssignment] Set
      
        [AFE] = @aFE,
        [SubAFE] = @subAFE,
        [AssetId] = @assetId,
        [Deleted] = @deleted
        Where
        
          [AssetAssignmentId] = @assetAssignmentId";
    [TransactionRequired]
    [Synchronized]
    public override AssetAssignment update(AssetAssignment assetAssignment)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@assetAssignmentId", assetAssignment.AssetAssignmentId);
            
              sqlCommand.Parameters.AddWithValue("@aFE", assetAssignment.AFE);
            
              sqlCommand.Parameters.AddWithValue("@subAFE", assetAssignment.SubAFE);
            
              sqlCommand.Parameters.AddWithValue("@assetId", assetAssignment.AssetId);
            
              sqlCommand.Parameters.AddWithValue("@deleted", assetAssignment.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(assetAssignment.relatedBillItem != null
              && assetAssignment.relatedBillItem.Count > 0)
              {
              BillItemDataMapper dataMapper = new BillItemDataMapper(Database);

              foreach(BillItem item in assetAssignment.relatedBillItem)
              dataMapper.save(item);
              }
            
              if(assetAssignment.relatedRateByAssignment != null
              && assetAssignment.relatedRateByAssignment.Count > 0)
              {
              RateByAssignmentDataMapper dataMapper = new RateByAssignmentDataMapper(Database);

              foreach(RateByAssignment item in assetAssignment.relatedRateByAssignment)
              dataMapper.save(item);
              }
            
              if(assetAssignment.relatedInvoiceItem != null
              && assetAssignment.relatedInvoiceItem.Count > 0)
              {
              InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);

              foreach(InvoiceItem item in assetAssignment.relatedInvoiceItem)
              dataMapper.save(item);
              }
            

        raiseAffected(assetAssignment,DataMapperOperation.update);
        
    
        return registerRecord(assetAssignment);

    }

  
    }
    
  
    
    public partial class BillItem: DomainObject
    {
    
      protected int _billItemId;
    
      protected String _billingDate;
    
      protected int? _qty;
    
      protected decimal? _billRate;
    
      protected String _notes;
    

      // parent tables
      protected AssetAssignment _relatedAssetAssignment
        = new AssetAssignment()
      ;
    

      // parent tables
      protected Bill _relatedBill
        = new Bill()
      ;
    

      // parent tables
      protected BillItemStatus _relatedBillItemStatus
        = new BillItemStatus()
      ;
    

      // parent tables
      protected BillItemType _relatedBillItemType
        = new BillItemType()
      ;
    

    public BillItem(){}

    public BillItem(
    int 
            billItemId,int 
            billItemTypeId,int 
            billId,int 
            assetAssignmentId,String 
            billingDate,int 
            qty,decimal 
            billRate,String 
            status,String 
            notes
    )
    {
    
      this.BillItemId = billItemId;
    
      this.BillItemTypeId = billItemTypeId;
    
      this.BillId = billId;
    
      this.AssetAssignmentId = assetAssignmentId;
    
      this.BillingDate = billingDate;
    
      this.Qty = qty;
    
      this.BillRate = billRate;
    
      this.Status = status;
    
      this.Notes = notes;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.BillItem."
    
      + BillItemId.ToString()
    ;
    
    return uri;
    }

    

      public int BillItemId
      {
        
            get { return _billItemId;}
            set 
            { 
                _billItemId = value;
            }
          
      }
    

      public int BillItemTypeId
      {
        
            get
            {
            
                  if(_relatedBillItemType != null)
                    return _relatedBillItemType.BillItemTypeId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedBillItemType == null)
                        _relatedBillItemType = new BillItemType();

                      _relatedBillItemType.BillItemTypeId = value;
                    
            }
          
      }
    

      public int BillId
      {
        
            get
            {
            
                  if(_relatedBill != null)
                    return _relatedBill.BillId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedBill == null)
                        _relatedBill = new Bill();

                      _relatedBill.BillId = value;
                    
            }
          
      }
    

      public int AssetAssignmentId
      {
        
            get
            {
            
                  if(_relatedAssetAssignment != null)
                    return _relatedAssetAssignment.AssetAssignmentId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAssetAssignment == null)
                        _relatedAssetAssignment = new AssetAssignment();

                      _relatedAssetAssignment.AssetAssignmentId = value;
                    
            }
          
      }
    

      public String BillingDate
      {
        
            get { return _billingDate;}
            set 
            { 
                _billingDate = value;
            }
          
      }
    

      public int? Qty
      {
        
            get { return _qty;}
            set 
            { 
                _qty = value;
            }
          
      }
    

      public decimal? BillRate
      {
        
            get { return _billRate;}
            set 
            { 
                _billRate = value;
            }
          
      }
    

      public String Status
      {
        
            get
            {
            
                  if(_relatedBillItemStatus != null)
                    return _relatedBillItemStatus.Status;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedBillItemStatus == null)
                        _relatedBillItemStatus = new BillItemStatus();

                      _relatedBillItemStatus.Status = value;
                    
            }
          
      }
    

      public String Notes
      {
        
            get { return _notes;}
            set 
            { 
                _notes = value;
            }
          
      }
    

      public AssetAssignment RelatedAssetAssignment
      {
      get { return _relatedAssetAssignment;}
      set { _relatedAssetAssignment = value; }
      }
      
    

      public Bill RelatedBill
      {
      get { return _relatedBill;}
      set { _relatedBill = value; }
      }
      
    

      public BillItemStatus RelatedBillItemStatus
      {
      get { return _relatedBillItemStatus;}
      set { _relatedBillItemStatus = value; }
      }
      
    

      public BillItemType RelatedBillItemType
      {
      get { return _relatedBillItemType;}
      set { _relatedBillItemType = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      BillItem billItem = new BillItem();
      
      billItem.BillItemId = this.BillItemId;
      billItem.BillItemTypeId = this.BillItemTypeId;
      billItem.BillId = this.BillId;
      billItem.AssetAssignmentId = this.AssetAssignmentId;
      billItem.BillingDate = this.BillingDate;
      billItem.Qty = this.Qty;
      billItem.BillRate = this.BillRate;
      billItem.Status = this.Status;
      billItem.Notes = this.Notes;
      billItem.ActiveRecordId = this.ActiveRecordId; 
      return billItem;
    }

    
          // one to many relation
          private List<BillItemAttachment> _relatedBillItemAttachment;

          public List<BillItemAttachment> relatedBillItemAttachment
          {
          get { return _relatedBillItemAttachment;}
          set { _relatedBillItemAttachment = value; }
          }
          
          
          public BillItemAttachment addRelatedBillItemAttachmentItem(
          BillItemAttachment billItemAttachment)
          {
            billItemAttachment.RelatedBillItem = this;
            
            _relatedBillItemAttachment.Add(billItemAttachment);
            
            return billItemAttachment;
          }
            
        
          // one to many relation
          private List<InvoiceItem> _relatedInvoiceItem;

          public List<InvoiceItem> relatedInvoiceItem
          {
          get { return _relatedInvoiceItem;}
          set { _relatedInvoiceItem = value; }
          }
          
          
          public InvoiceItem addRelatedInvoiceItemItem(
          InvoiceItem invoiceItem)
          {
            invoiceItem.RelatedBillItem = this;
            
            _relatedInvoiceItem.Add(invoiceItem);
            
            return invoiceItem;
          }
            
        
    }
  

    public abstract class _BillItemDataMapper:TDataMapper<BillItem,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _BillItemDataMapper(){}
      public _BillItemDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[BillItem]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[BillItem] (
    
      [BillItemTypeId]
      ,
      [BillId]
      ,
      [AssetAssignmentId]
      ,
      [BillingDate]
      ,
      [Qty]
      ,
      [BillRate]
      ,
      [Status]
      ,
      [Notes]
      ) Values (
    
      @billItemTypeId,
      @billId,
      @assetAssignmentId,
      @billingDate,
      @qty,
      @billRate,
      @status,
      @notes);

    
      select scope_identity();
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override BillItem create( BillItem billItem )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@billItemTypeId", billItem.BillItemTypeId);
              
                  sqlCommand.Parameters.AddWithValue("@billId", billItem.BillId);
              
                  sqlCommand.Parameters.AddWithValue("@assetAssignmentId", billItem.AssetAssignmentId);
              
                  sqlCommand.Parameters.AddWithValue("@billingDate", billItem.BillingDate);
              
                    if(billItem.Qty.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@qty", billItem.Qty);
                else
                  sqlCommand.Parameters.AddWithValue("@qty", DBNull.Value);
              
                    if(billItem.BillRate.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@billRate", billItem.BillRate);
                else
                  sqlCommand.Parameters.AddWithValue("@billRate", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@status", billItem.Status);
              
                    if(billItem.Notes != null)
                  
                  sqlCommand.Parameters.AddWithValue("@notes", billItem.Notes);
                else
                  sqlCommand.Parameters.AddWithValue("@notes", DBNull.Value);
              billItem.BillItemId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(billItem.relatedBillItemAttachment != null 
            && billItem.relatedBillItemAttachment.Count > 0)
          {
            BillItemAttachmentDataMapper dataMapper = new BillItemAttachmentDataMapper(Database);
            
            foreach(BillItemAttachment item in billItem.relatedBillItemAttachment)
              dataMapper.create(item);
          }
        
          
          if(billItem.relatedInvoiceItem != null 
            && billItem.relatedInvoiceItem.Count > 0)
          {
            InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);
            
            foreach(InvoiceItem item in billItem.relatedInvoiceItem)
              dataMapper.create(item);
          }
        
      
      raiseAffected(billItem,DataMapperOperation.create);

      return registerRecord(billItem);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [BillItemId] ,
      [BillItemTypeId] ,
      [BillId] ,
      [AssetAssignmentId] ,
      [BillingDate] ,
      [Qty] ,
      [BillRate] ,
      [Status] ,
      [Notes] 
     From [dbo].[BillItem]
    
       Where 
      
         [BillItemId] = @billItemId
    ";

    public BillItem findByPrimaryKey(
    int billItemId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@billItemId", billItemId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("BillItem not found, search by primary key");
 

    }


    public bool exists(BillItem billItem)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@BillItemId", billItem.BillItemId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [BillItem].[BillItemId] = @CheckInBillItemId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      BillItem _BillItem = (BillItem)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInBillItemId", _BillItem.BillItemId);
      

      return sqlCommand;
    }

  

    protected override BillItem doLoad(IDataReader dataReader)
    {
    BillItem billItem = new BillItem();

    billItem.BillItemId = (int) dataReader.GetValue(0);
            billItem.BillItemTypeId = (int) dataReader.GetValue(1);
            billItem.BillId = (int) dataReader.GetValue(2);
            billItem.AssetAssignmentId = (int) dataReader.GetValue(3);
            billItem.BillingDate = (String) dataReader.GetValue(4);
            
          if(!dataReader.IsDBNull(5))        
          billItem.Qty = (int) dataReader.GetValue(5);
            
          if(!dataReader.IsDBNull(6))        
          billItem.BillRate = (decimal) dataReader.GetValue(6);
            billItem.Status = (String) dataReader.GetValue(7);
            
          if(!dataReader.IsDBNull(8))        
          billItem.Notes = (String) dataReader.GetValue(8);
            

    
    
    return registerRecord(billItem);
    }


    protected override BillItem doLoad(Hashtable hashtable)
    {
      BillItem billItem = new BillItem();

      
        
        if(hashtable.ContainsKey("BillItemId"))
        billItem.BillItemId = ( int)hashtable["BillItemId"];
          
        
        if(hashtable.ContainsKey("BillItemTypeId"))
        billItem.BillItemTypeId = ( int)hashtable["BillItemTypeId"];
          
        
        if(hashtable.ContainsKey("BillId"))
        billItem.BillId = ( int)hashtable["BillId"];
          
        
        if(hashtable.ContainsKey("AssetAssignmentId"))
        billItem.AssetAssignmentId = ( int)hashtable["AssetAssignmentId"];
          
        
        if(hashtable.ContainsKey("BillingDate"))
        billItem.BillingDate = ( String)hashtable["BillingDate"];
          
        
        if(hashtable.ContainsKey("Qty"))
        billItem.Qty = ( int)hashtable["Qty"];
          
        
        if(hashtable.ContainsKey("BillRate"))
        billItem.BillRate = ( decimal)hashtable["BillRate"];
          
        
        if(hashtable.ContainsKey("Status"))
        billItem.Status = ( String)hashtable["Status"];
          
        
        if(hashtable.ContainsKey("Notes"))
        billItem.Notes = ( String)hashtable["Notes"];
          

      return billItem;
    }


    protected override List<BillItem> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<BillItem> resultList = new List<BillItem>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              BillItem item = new BillItem();
              
              
                    item.BillItemId = ( int)dataRow["BillItemId"] ;
                  
                    item.BillItemTypeId = ( int)dataRow["BillItemTypeId"] ;
                  
                    item.BillId = ( int)dataRow["BillId"] ;
                  
                    item.AssetAssignmentId = ( int)dataRow["AssetAssignmentId"] ;
                  
                    item.BillingDate = ( String)dataRow["BillingDate"] ;
                  
                  if(!dataRow.IsNull("Qty"))
                
                    item.Qty = ( int)dataRow["Qty"] ;
                  
                  if(!dataRow.IsNull("BillRate"))
                
                    item.BillRate = ( decimal)dataRow["BillRate"] ;
                  
                    item.Status = ( String)dataRow["Status"] ;
                  
                  if(!dataRow.IsNull("Notes"))
                
                    item.Notes = ( String)dataRow["Notes"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[BillItem]
    
      Where
      
        [BillItemId] = @billItemId";
    [Synchronized]
    public override BillItem remove(BillItem billItem)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@billItemId", billItem.BillItemId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(billItem,DataMapperOperation.delete);

      return registerRecord(billItem);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override BillItem save( BillItem billItem )
    {
      if(exists(billItem))
        return update(billItem);
        return create(billItem);
    }

  
      const String SqlUpdate = @"Update [dbo].[BillItem] Set
      
        [BillItemTypeId] = @billItemTypeId,
        [BillId] = @billId,
        [AssetAssignmentId] = @assetAssignmentId,
        [BillingDate] = @billingDate,
        [Qty] = @qty,
        [BillRate] = @billRate,
        [Status] = @status,
        [Notes] = @notes
        Where
        
          [BillItemId] = @billItemId";
    [TransactionRequired]
    [Synchronized]
    public override BillItem update(BillItem billItem)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@billItemId", billItem.BillItemId);
            
              sqlCommand.Parameters.AddWithValue("@billItemTypeId", billItem.BillItemTypeId);
            
              sqlCommand.Parameters.AddWithValue("@billId", billItem.BillId);
            
              sqlCommand.Parameters.AddWithValue("@assetAssignmentId", billItem.AssetAssignmentId);
            
              sqlCommand.Parameters.AddWithValue("@billingDate", billItem.BillingDate);
            
                  if(billItem.Qty.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@qty", billItem.Qty);
              else
              sqlCommand.Parameters.AddWithValue("@qty", DBNull.Value);
            
                  if(billItem.BillRate.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@billRate", billItem.BillRate);
              else
              sqlCommand.Parameters.AddWithValue("@billRate", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@status", billItem.Status);
            
                  if(billItem.Notes != null)
                
              sqlCommand.Parameters.AddWithValue("@notes", billItem.Notes);
              else
              sqlCommand.Parameters.AddWithValue("@notes", DBNull.Value);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(billItem.relatedBillItemAttachment != null
              && billItem.relatedBillItemAttachment.Count > 0)
              {
              BillItemAttachmentDataMapper dataMapper = new BillItemAttachmentDataMapper(Database);

              foreach(BillItemAttachment item in billItem.relatedBillItemAttachment)
              dataMapper.save(item);
              }
            
              if(billItem.relatedInvoiceItem != null
              && billItem.relatedInvoiceItem.Count > 0)
              {
              InvoiceItemDataMapper dataMapper = new InvoiceItemDataMapper(Database);

              foreach(InvoiceItem item in billItem.relatedInvoiceItem)
              dataMapper.save(item);
              }
            

        raiseAffected(billItem,DataMapperOperation.update);
        
    
        return registerRecord(billItem);

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
    
      protected bool _deleted;
    

    public User(){}

    public User(
    int 
            userId,String 
            login,String 
            password,String 
            email,bool 
            isActive,int 
            hackingAttempts,bool 
            deleted
    )
    {
    
      this.UserId = userId;
    
      this.Login = login;
    
      this.Password = password;
    
      this.Email = email;
    
      this.IsActive = isActive;
    
      this.HackingAttempts = hackingAttempts;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.User."
    
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
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
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
      user.Deleted = this.Deleted;
      user.ActiveRecordId = this.ActiveRecordId; 
      return user;
    }

    
          // one to many relation
          private List<Note> _relatedNote;

          public List<Note> relatedNote
          {
          get { return _relatedNote;}
          set { _relatedNote = value; }
          }
          
          
          public Note addRelatedNoteItem(
          Note note)
          {
            note.RelatedUser = this;
            
            _relatedNote.Add(note);
            
            return note;
          }
            
        
          // one to many relation
          private List<Message> _relatedMessage;

          public List<Message> relatedMessage
          {
          get { return _relatedMessage;}
          set { _relatedMessage = value; }
          }
          
          
          public Message addRelatedMessageItem(
          Message message)
          {
            message.RelatedUser = this;
            
            _relatedMessage.Add(message);
            
            return message;
          }
            
        
          // one to many relation
          private List<UserAsset> _relatedUserAsset;

          public List<UserAsset> relatedUserAsset
          {
          get { return _relatedUserAsset;}
          set { _relatedUserAsset = value; }
          }
          
          
          public UserAsset addRelatedUserAssetItem(
          UserAsset userAsset)
          {
            userAsset.RelatedUser = this;
            
            _relatedUserAsset.Add(userAsset);
            
            return userAsset;
          }
            
        
          // one to many relation
          private List<UserRole> _relatedUserRole;

          public List<UserRole> relatedUserRole
          {
          get { return _relatedUserRole;}
          set { _relatedUserRole = value; }
          }
          
          
          public UserRole addRelatedUserRoleItem(
          UserRole userRole)
          {
            userRole.RelatedUser = this;
            
            _relatedUserRole.Add(userRole);
            
            return userRole;
          }
            
        
    }
  

    public abstract class _UserDataMapper:TDataMapper<User,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _UserDataMapper(){}
      public _UserDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[User]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[User] (
    
      [Login]
      ,
      [Password]
      ,
      [Email]
      ,
      [IsActive]
      ,
      [HackingAttempts]
      ,
      [Deleted]
      ) Values (
    
      @login,
      @password,
      @email,
      @isActive,
      @hackingAttempts,
      @deleted);

    
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
        
                  sqlCommand.Parameters.AddWithValue("@login", user.Login);
              
                  sqlCommand.Parameters.AddWithValue("@password", user.Password);
              
                  sqlCommand.Parameters.AddWithValue("@email", user.Email);
              
                  sqlCommand.Parameters.AddWithValue("@isActive", user.IsActive);
              
                  sqlCommand.Parameters.AddWithValue("@hackingAttempts", user.HackingAttempts);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", user.Deleted);
              user.UserId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(user.relatedNote != null 
            && user.relatedNote.Count > 0)
          {
            NoteDataMapper dataMapper = new NoteDataMapper(Database);
            
            foreach(Note item in user.relatedNote)
              dataMapper.create(item);
          }
        
          
          if(user.relatedMessage != null 
            && user.relatedMessage.Count > 0)
          {
            MessageDataMapper dataMapper = new MessageDataMapper(Database);
            
            foreach(Message item in user.relatedMessage)
              dataMapper.create(item);
          }
        
          
          if(user.relatedUserAsset != null 
            && user.relatedUserAsset.Count > 0)
          {
            UserAssetDataMapper dataMapper = new UserAssetDataMapper(Database);
            
            foreach(UserAsset item in user.relatedUserAsset)
              dataMapper.create(item);
          }
        
          
          if(user.relatedUserRole != null 
            && user.relatedUserRole.Count > 0)
          {
            UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);
            
            foreach(UserRole item in user.relatedUserRole)
              dataMapper.create(item);
          }
        
      
      raiseAffected(user,DataMapperOperation.create);

      return registerRecord(user);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [UserId] ,
      [Login] ,
      [Password] ,
      [Email] ,
      [IsActive] ,
      [HackingAttempts] ,
      [Deleted] 
     From [dbo].[User]
    
       Where 
      
         [UserId] = @userId
    ";

    public User findByPrimaryKey(
    int userId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@userId", userId);
        

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

    user.UserId = (int) dataReader.GetValue(0);
            user.Login = (String) dataReader.GetValue(1);
            user.Password = (String) dataReader.GetValue(2);
            user.Email = (String) dataReader.GetValue(3);
            user.IsActive = (bool) dataReader.GetValue(4);
            user.HackingAttempts = (int) dataReader.GetValue(5);
            user.Deleted = (bool) dataReader.GetValue(6);
            

    
    
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
          
        
        if(hashtable.ContainsKey("Deleted"))
        user.Deleted = ( bool)hashtable["Deleted"];
          

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
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[User]
    
      Where
      
        [UserId] = @userId";
    [Synchronized]
    public override User remove(User user)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@userId", user.UserId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(user,DataMapperOperation.delete);

      return registerRecord(user);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override User save( User user )
    {
      if(exists(user))
        return update(user);
        return create(user);
    }

  
      const String SqlUpdate = @"Update [dbo].[User] Set
      
        [Login] = @login,
        [Password] = @password,
        [Email] = @email,
        [IsActive] = @isActive,
        [HackingAttempts] = @hackingAttempts,
        [Deleted] = @deleted
        Where
        
          [UserId] = @userId";
    [TransactionRequired]
    [Synchronized]
    public override User update(User user)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@userId", user.UserId);
            
              sqlCommand.Parameters.AddWithValue("@login", user.Login);
            
              sqlCommand.Parameters.AddWithValue("@password", user.Password);
            
              sqlCommand.Parameters.AddWithValue("@email", user.Email);
            
              sqlCommand.Parameters.AddWithValue("@isActive", user.IsActive);
            
              sqlCommand.Parameters.AddWithValue("@hackingAttempts", user.HackingAttempts);
            
              sqlCommand.Parameters.AddWithValue("@deleted", user.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(user.relatedNote != null
              && user.relatedNote.Count > 0)
              {
              NoteDataMapper dataMapper = new NoteDataMapper(Database);

              foreach(Note item in user.relatedNote)
              dataMapper.save(item);
              }
            
              if(user.relatedMessage != null
              && user.relatedMessage.Count > 0)
              {
              MessageDataMapper dataMapper = new MessageDataMapper(Database);

              foreach(Message item in user.relatedMessage)
              dataMapper.save(item);
              }
            
              if(user.relatedUserAsset != null
              && user.relatedUserAsset.Count > 0)
              {
              UserAssetDataMapper dataMapper = new UserAssetDataMapper(Database);

              foreach(UserAsset item in user.relatedUserAsset)
              dataMapper.save(item);
              }
            
              if(user.relatedUserRole != null
              && user.relatedUserRole.Count > 0)
              {
              UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);

              foreach(UserRole item in user.relatedUserRole)
              dataMapper.save(item);
              }
            

        raiseAffected(user,DataMapperOperation.update);
        
    
        return registerRecord(user);

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
    
      this.ModuleId = moduleId;
    
      this.Description = description;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Module."
    
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
          private List<Permission> _relatedPermission;

          public List<Permission> relatedPermission
          {
          get { return _relatedPermission;}
          set { _relatedPermission = value; }
          }
          
          
          public Permission addRelatedPermissionItem(
          Permission permission)
          {
            permission.RelatedModule = this;
            
            _relatedPermission.Add(permission);
            
            return permission;
          }
            
        
    }
  

    public abstract class _ModuleDataMapper:TDataMapper<Module,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _ModuleDataMapper(){}
      public _ModuleDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Module]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Module] (
    
      [Description]
      ) Values (
    
      @description);

    
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
        
                  sqlCommand.Parameters.AddWithValue("@description", module.Description);
              module.ModuleId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(module.relatedPermission != null 
            && module.relatedPermission.Count > 0)
          {
            PermissionDataMapper dataMapper = new PermissionDataMapper(Database);
            
            foreach(Permission item in module.relatedPermission)
              dataMapper.create(item);
          }
        
      
      raiseAffected(module,DataMapperOperation.create);

      return registerRecord(module);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [ModuleId] ,
      [Description] 
     From [dbo].[Module]
    
       Where 
      
         [ModuleId] = @moduleId
    ";

    public Module findByPrimaryKey(
    int moduleId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@moduleId", moduleId);
        

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

    module.ModuleId = (int) dataReader.GetValue(0);
            module.Description = (String) dataReader.GetValue(1);
            

    
    
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
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Module]
    
      Where
      
        [ModuleId] = @moduleId";
    [Synchronized]
    public override Module remove(Module module)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@moduleId", module.ModuleId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(module,DataMapperOperation.delete);

      return registerRecord(module);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Module save( Module module )
    {
      if(exists(module))
        return update(module);
        return create(module);
    }

  
      const String SqlUpdate = @"Update [dbo].[Module] Set
      
        [Description] = @description
        Where
        
          [ModuleId] = @moduleId";
    [TransactionRequired]
    [Synchronized]
    public override Module update(Module module)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@moduleId", module.ModuleId);
            
              sqlCommand.Parameters.AddWithValue("@description", module.Description);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(module.relatedPermission != null
              && module.relatedPermission.Count > 0)
              {
              PermissionDataMapper dataMapper = new PermissionDataMapper(Database);

              foreach(Permission item in module.relatedPermission)
              dataMapper.save(item);
              }
            

        raiseAffected(module,DataMapperOperation.update);
        
    
        return registerRecord(module);

    }

  
    }
    
  
    
    public partial class Note: DomainObject
    {
    
      protected int _noteId;
    
      protected int _relatedItemId;
    
      protected String _itemType;
    
      protected DateTime _posted;
    
      protected String _noteText;
    

      // parent tables
      protected User _relatedUser
        = new User()
      ;
    

    public Note(){}

    public Note(
    int 
            noteId,int 
            relatedItemId,String 
            itemType,int 
            senderId,DateTime 
            posted,String 
            noteText
    )
    {
    
      this.NoteId = noteId;
    
      this.RelatedItemId = relatedItemId;
    
      this.ItemType = itemType;
    
      this.SenderId = senderId;
    
      this.Posted = posted;
    
      this.NoteText = noteText;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Note."
    
      + NoteId.ToString()
    ;
    
    return uri;
    }

    

      public int NoteId
      {
        
            get { return _noteId;}
            set 
            { 
                _noteId = value;
            }
          
      }
    

      public int RelatedItemId
      {
        
            get { return _relatedItemId;}
            set 
            { 
                _relatedItemId = value;
            }
          
      }
    

      public String ItemType
      {
        
            get { return _itemType;}
            set 
            { 
                _itemType = value;
            }
          
      }
    

      public int SenderId
      {
        
            get
            {
            
                  if(_relatedUser != null)
                    return _relatedUser.UserId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedUser == null)
                        _relatedUser = new User();

                      _relatedUser.UserId = value;
                    
            }
          
      }
    

      public DateTime Posted
      {
        
            get { return _posted;}
            set 
            { 
                _posted = value;
            }
          
      }
    

      public String NoteText
      {
        
            get { return _noteText;}
            set 
            { 
                _noteText = value;
            }
          
      }
    

      public User RelatedUser
      {
      get { return _relatedUser;}
      set { _relatedUser = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      Note note = new Note();
      
      note.NoteId = this.NoteId;
      note.RelatedItemId = this.RelatedItemId;
      note.ItemType = this.ItemType;
      note.SenderId = this.SenderId;
      note.Posted = this.Posted;
      note.NoteText = this.NoteText;
      note.ActiveRecordId = this.ActiveRecordId; 
      return note;
    }

    
    }
  

    public abstract class _NoteDataMapper:TDataMapper<Note,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _NoteDataMapper(){}
      public _NoteDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Note]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Note] (
    
      [RelatedItemId]
      ,
      [ItemType]
      ,
      [SenderId]
      ,
      [Posted]
      ,
      [NoteText]
      ) Values (
    
      @relatedItemId,
      @itemType,
      @senderId,
      @posted,
      @noteText);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Note create( Note note )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@relatedItemId", note.RelatedItemId);
              
                  sqlCommand.Parameters.AddWithValue("@itemType", note.ItemType);
              
                  sqlCommand.Parameters.AddWithValue("@senderId", note.SenderId);
              
                  sqlCommand.Parameters.AddWithValue("@posted", note.Posted);
              
                  sqlCommand.Parameters.AddWithValue("@noteText", note.NoteText);
              note.NoteId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(note,DataMapperOperation.create);

      return registerRecord(note);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [NoteId] ,
      [RelatedItemId] ,
      [ItemType] ,
      [SenderId] ,
      [Posted] ,
      [NoteText] 
     From [dbo].[Note]
    
       Where 
      
         [NoteId] = @noteId
    ";

    public Note findByPrimaryKey(
    int noteId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@noteId", noteId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Note not found, search by primary key");
 

    }


    public bool exists(Note note)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@NoteId", note.NoteId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Note].[NoteId] = @CheckInNoteId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Note _Note = (Note)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInNoteId", _Note.NoteId);
      

      return sqlCommand;
    }

  

    protected override Note doLoad(IDataReader dataReader)
    {
    Note note = new Note();

    note.NoteId = (int) dataReader.GetValue(0);
            note.RelatedItemId = (int) dataReader.GetValue(1);
            note.ItemType = (String) dataReader.GetValue(2);
            note.SenderId = (int) dataReader.GetValue(3);
            note.Posted = (DateTime) dataReader.GetValue(4);
            note.NoteText = (String) dataReader.GetValue(5);
            

    
    
    return registerRecord(note);
    }


    protected override Note doLoad(Hashtable hashtable)
    {
      Note note = new Note();

      
        
        if(hashtable.ContainsKey("NoteId"))
        note.NoteId = ( int)hashtable["NoteId"];
          
        
        if(hashtable.ContainsKey("RelatedItemId"))
        note.RelatedItemId = ( int)hashtable["RelatedItemId"];
          
        
        if(hashtable.ContainsKey("ItemType"))
        note.ItemType = ( String)hashtable["ItemType"];
          
        
        if(hashtable.ContainsKey("SenderId"))
        note.SenderId = ( int)hashtable["SenderId"];
          
        
        if(hashtable.ContainsKey("Posted"))
        note.Posted = ( DateTime)hashtable["Posted"];
          
        
        if(hashtable.ContainsKey("NoteText"))
        note.NoteText = ( String)hashtable["NoteText"];
          

      return note;
    }


    protected override List<Note> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Note> resultList = new List<Note>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Note item = new Note();
              
              
                    item.NoteId = ( int)dataRow["NoteId"] ;
                  
                    item.RelatedItemId = ( int)dataRow["RelatedItemId"] ;
                  
                    item.ItemType = ( String)dataRow["ItemType"] ;
                  
                    item.SenderId = ( int)dataRow["SenderId"] ;
                  
                    item.Posted = ( DateTime)dataRow["Posted"] ;
                  
                    item.NoteText = ( String)dataRow["NoteText"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Note]
    
      Where
      
        [NoteId] = @noteId";
    [Synchronized]
    public override Note remove(Note note)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@noteId", note.NoteId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(note,DataMapperOperation.delete);

      return registerRecord(note);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Note save( Note note )
    {
      if(exists(note))
        return update(note);
        return create(note);
    }

  
      const String SqlUpdate = @"Update [dbo].[Note] Set
      
        [RelatedItemId] = @relatedItemId,
        [ItemType] = @itemType,
        [SenderId] = @senderId,
        [Posted] = @posted,
        [NoteText] = @noteText
        Where
        
          [NoteId] = @noteId";
    
    [Synchronized]
    public override Note update(Note note)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@noteId", note.NoteId);
            
              sqlCommand.Parameters.AddWithValue("@relatedItemId", note.RelatedItemId);
            
              sqlCommand.Parameters.AddWithValue("@itemType", note.ItemType);
            
              sqlCommand.Parameters.AddWithValue("@senderId", note.SenderId);
            
              sqlCommand.Parameters.AddWithValue("@posted", note.Posted);
            
              sqlCommand.Parameters.AddWithValue("@noteText", note.NoteText);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(note,DataMapperOperation.update);
        
    
        return registerRecord(note);

    }

  
    }
    
  
    
    public partial class Permission: DomainObject
    {
    
      protected int _permissionId;
    
      protected String _description;
    
      protected String _code;
    

      // parent tables
      protected Module _relatedModule
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
    
      this.PermissionId = permissionId;
    
      this.ModuleId = moduleId;
    
      this.Description = description;
    
      this.Code = code;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Permission."
    
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
            
                  if(_relatedModule != null)
                    return _relatedModule.ModuleId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedModule == null)
                        _relatedModule = new Module();

                      _relatedModule.ModuleId = value;
                    
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
    

      public Module RelatedModule
      {
      get { return _relatedModule;}
      set { _relatedModule = value; }
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
          private List<PermissionAssignment> _relatedPermissionAssignment;

          public List<PermissionAssignment> relatedPermissionAssignment
          {
          get { return _relatedPermissionAssignment;}
          set { _relatedPermissionAssignment = value; }
          }
          
          
          public PermissionAssignment addRelatedPermissionAssignmentItem(
          PermissionAssignment permissionAssignment)
          {
            permissionAssignment.RelatedPermission = this;
            
            _relatedPermissionAssignment.Add(permissionAssignment);
            
            return permissionAssignment;
          }
            
        
    }
  

    public abstract class _PermissionDataMapper:TDataMapper<Permission,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _PermissionDataMapper(){}
      public _PermissionDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Permission]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Permission] (
    
      [ModuleId]
      ,
      [Description]
      ,
      [Code]
      ) Values (
    
      @moduleId,
      @description,
      @code);

    
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
        
                  sqlCommand.Parameters.AddWithValue("@moduleId", permission.ModuleId);
              
                    if(permission.Description != null)
                  
                  sqlCommand.Parameters.AddWithValue("@description", permission.Description);
                else
                  sqlCommand.Parameters.AddWithValue("@description", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@code", permission.Code);
              permission.PermissionId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(permission.relatedPermissionAssignment != null 
            && permission.relatedPermissionAssignment.Count > 0)
          {
            PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);
            
            foreach(PermissionAssignment item in permission.relatedPermissionAssignment)
              dataMapper.create(item);
          }
        
      
      raiseAffected(permission,DataMapperOperation.create);

      return registerRecord(permission);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [PermissionId] ,
      [ModuleId] ,
      [Description] ,
      [Code] 
     From [dbo].[Permission]
    
       Where 
      
         [PermissionId] = @permissionId
    ";

    public Permission findByPrimaryKey(
    int permissionId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@permissionId", permissionId);
        

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

    permission.PermissionId = (int) dataReader.GetValue(0);
            permission.ModuleId = (int) dataReader.GetValue(1);
            
          if(!dataReader.IsDBNull(2))        
          permission.Description = (String) dataReader.GetValue(2);
            permission.Code = (String) dataReader.GetValue(3);
            

    
    
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
                  
                  if(!dataRow.IsNull("Description"))
                
                    item.Description = ( String)dataRow["Description"] ;
                  
                    item.Code = ( String)dataRow["Code"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Permission]
    
      Where
      
        [PermissionId] = @permissionId";
    [Synchronized]
    public override Permission remove(Permission permission)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@permissionId", permission.PermissionId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(permission,DataMapperOperation.delete);

      return registerRecord(permission);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Permission save( Permission permission )
    {
      if(exists(permission))
        return update(permission);
        return create(permission);
    }

  
      const String SqlUpdate = @"Update [dbo].[Permission] Set
      
        [ModuleId] = @moduleId,
        [Description] = @description,
        [Code] = @code
        Where
        
          [PermissionId] = @permissionId";
    [TransactionRequired]
    [Synchronized]
    public override Permission update(Permission permission)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@permissionId", permission.PermissionId);
            
              sqlCommand.Parameters.AddWithValue("@moduleId", permission.ModuleId);
            
                  if(permission.Description != null)
                
              sqlCommand.Parameters.AddWithValue("@description", permission.Description);
              else
              sqlCommand.Parameters.AddWithValue("@description", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@code", permission.Code);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(permission.relatedPermissionAssignment != null
              && permission.relatedPermissionAssignment.Count > 0)
              {
              PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);

              foreach(PermissionAssignment item in permission.relatedPermissionAssignment)
              dataMapper.save(item);
              }
            

        raiseAffected(permission,DataMapperOperation.update);
        
    
        return registerRecord(permission);

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
    
      this.RoleId = roleId;
    
      this.Name = name;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Role."
    
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
          private List<PermissionAssignment> _relatedPermissionAssignment;

          public List<PermissionAssignment> relatedPermissionAssignment
          {
          get { return _relatedPermissionAssignment;}
          set { _relatedPermissionAssignment = value; }
          }
          
          
          public PermissionAssignment addRelatedPermissionAssignmentItem(
          PermissionAssignment permissionAssignment)
          {
            permissionAssignment.RelatedRole = this;
            
            _relatedPermissionAssignment.Add(permissionAssignment);
            
            return permissionAssignment;
          }
            
        
          // one to many relation
          private List<UserRole> _relatedUserRole;

          public List<UserRole> relatedUserRole
          {
          get { return _relatedUserRole;}
          set { _relatedUserRole = value; }
          }
          
          
          public UserRole addRelatedUserRoleItem(
          UserRole userRole)
          {
            userRole.RelatedRole = this;
            
            _relatedUserRole.Add(userRole);
            
            return userRole;
          }
            
        
    }
  

    public abstract class _RoleDataMapper:TDataMapper<Role,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _RoleDataMapper(){}
      public _RoleDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Role]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Role] (
    
      [Name]
      ) Values (
    
      @name);

    
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
        
                  sqlCommand.Parameters.AddWithValue("@name", role.Name);
              role.RoleId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
          
          if(role.relatedPermissionAssignment != null 
            && role.relatedPermissionAssignment.Count > 0)
          {
            PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);
            
            foreach(PermissionAssignment item in role.relatedPermissionAssignment)
              dataMapper.create(item);
          }
        
          
          if(role.relatedUserRole != null 
            && role.relatedUserRole.Count > 0)
          {
            UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);
            
            foreach(UserRole item in role.relatedUserRole)
              dataMapper.create(item);
          }
        
      
      raiseAffected(role,DataMapperOperation.create);

      return registerRecord(role);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [RoleId] ,
      [Name] 
     From [dbo].[Role]
    
       Where 
      
         [RoleId] = @roleId
    ";

    public Role findByPrimaryKey(
    int roleId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@roleId", roleId);
        

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

    role.RoleId = (int) dataReader.GetValue(0);
            role.Name = (String) dataReader.GetValue(1);
            

    
    
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
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Role]
    
      Where
      
        [RoleId] = @roleId";
    [Synchronized]
    public override Role remove(Role role)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@roleId", role.RoleId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(role,DataMapperOperation.delete);

      return registerRecord(role);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Role save( Role role )
    {
      if(exists(role))
        return update(role);
        return create(role);
    }

  
      const String SqlUpdate = @"Update [dbo].[Role] Set
      
        [Name] = @name
        Where
        
          [RoleId] = @roleId";
    [TransactionRequired]
    [Synchronized]
    public override Role update(Role role)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@roleId", role.RoleId);
            
              sqlCommand.Parameters.AddWithValue("@name", role.Name);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        
              if(role.relatedPermissionAssignment != null
              && role.relatedPermissionAssignment.Count > 0)
              {
              PermissionAssignmentDataMapper dataMapper = new PermissionAssignmentDataMapper(Database);

              foreach(PermissionAssignment item in role.relatedPermissionAssignment)
              dataMapper.save(item);
              }
            
              if(role.relatedUserRole != null
              && role.relatedUserRole.Count > 0)
              {
              UserRoleDataMapper dataMapper = new UserRoleDataMapper(Database);

              foreach(UserRole item in role.relatedUserRole)
              dataMapper.save(item);
              }
            

        raiseAffected(role,DataMapperOperation.update);
        
    
        return registerRecord(role);

    }

  
    }
    
  
    
    public partial class RateByAssignment: DomainObject
    {
    
      protected int _rateByAssignmentId;
    
      protected decimal _billRate;
    
      protected decimal _invoiceRate;
    
      protected bool _shouldNotExceedRate;
    
      protected bool _deleted;
    

      // parent tables
      protected AssetAssignment _relatedAssetAssignment
        = new AssetAssignment()
      ;
    

      // parent tables
      protected BillItemType _relatedBillItemType
        = new BillItemType()
      ;
    

    public RateByAssignment(){}

    public RateByAssignment(
    int 
            rateByAssignmentId,int 
            assetAssignmentId,int 
            billItemTypeId,decimal 
            billRate,decimal 
            invoiceRate,bool 
            shouldNotExceedRate,bool 
            deleted
    )
    {
    
      this.RateByAssignmentId = rateByAssignmentId;
    
      this.AssetAssignmentId = assetAssignmentId;
    
      this.BillItemTypeId = billItemTypeId;
    
      this.BillRate = billRate;
    
      this.InvoiceRate = invoiceRate;
    
      this.ShouldNotExceedRate = shouldNotExceedRate;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.RateByAssignment."
    
      + RateByAssignmentId.ToString()
    ;
    
    return uri;
    }

    

      public int RateByAssignmentId
      {
        
            get { return _rateByAssignmentId;}
            set 
            { 
                _rateByAssignmentId = value;
            }
          
      }
    

      public int AssetAssignmentId
      {
        
            get
            {
            
                  if(_relatedAssetAssignment != null)
                    return _relatedAssetAssignment.AssetAssignmentId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAssetAssignment == null)
                        _relatedAssetAssignment = new AssetAssignment();

                      _relatedAssetAssignment.AssetAssignmentId = value;
                    
            }
          
      }
    

      public int BillItemTypeId
      {
        
            get
            {
            
                  if(_relatedBillItemType != null)
                    return _relatedBillItemType.BillItemTypeId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedBillItemType == null)
                        _relatedBillItemType = new BillItemType();

                      _relatedBillItemType.BillItemTypeId = value;
                    
            }
          
      }
    

      public decimal BillRate
      {
        
            get { return _billRate;}
            set 
            { 
                _billRate = value;
            }
          
      }
    

      public decimal InvoiceRate
      {
        
            get { return _invoiceRate;}
            set 
            { 
                _invoiceRate = value;
            }
          
      }
    

      public bool ShouldNotExceedRate
      {
        
            get { return _shouldNotExceedRate;}
            set 
            { 
                _shouldNotExceedRate = value;
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    

      public AssetAssignment RelatedAssetAssignment
      {
      get { return _relatedAssetAssignment;}
      set { _relatedAssetAssignment = value; }
      }
      
    

      public BillItemType RelatedBillItemType
      {
      get { return _relatedBillItemType;}
      set { _relatedBillItemType = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      RateByAssignment rateByAssignment = new RateByAssignment();
      
      rateByAssignment.RateByAssignmentId = this.RateByAssignmentId;
      rateByAssignment.AssetAssignmentId = this.AssetAssignmentId;
      rateByAssignment.BillItemTypeId = this.BillItemTypeId;
      rateByAssignment.BillRate = this.BillRate;
      rateByAssignment.InvoiceRate = this.InvoiceRate;
      rateByAssignment.ShouldNotExceedRate = this.ShouldNotExceedRate;
      rateByAssignment.Deleted = this.Deleted;
      rateByAssignment.ActiveRecordId = this.ActiveRecordId; 
      return rateByAssignment;
    }

    
    }
  

    public abstract class _RateByAssignmentDataMapper:TDataMapper<RateByAssignment,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _RateByAssignmentDataMapper(){}
      public _RateByAssignmentDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[RateByAssignment]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[RateByAssignment] (
    
      [AssetAssignmentId]
      ,
      [BillItemTypeId]
      ,
      [BillRate]
      ,
      [InvoiceRate]
      ,
      [ShouldNotExceedRate]
      ,
      [Deleted]
      ) Values (
    
      @assetAssignmentId,
      @billItemTypeId,
      @billRate,
      @invoiceRate,
      @shouldNotExceedRate,
      @deleted);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override RateByAssignment create( RateByAssignment rateByAssignment )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@assetAssignmentId", rateByAssignment.AssetAssignmentId);
              
                  sqlCommand.Parameters.AddWithValue("@billItemTypeId", rateByAssignment.BillItemTypeId);
              
                  sqlCommand.Parameters.AddWithValue("@billRate", rateByAssignment.BillRate);
              
                  sqlCommand.Parameters.AddWithValue("@invoiceRate", rateByAssignment.InvoiceRate);
              
                  sqlCommand.Parameters.AddWithValue("@shouldNotExceedRate", rateByAssignment.ShouldNotExceedRate);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", rateByAssignment.Deleted);
              rateByAssignment.RateByAssignmentId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(rateByAssignment,DataMapperOperation.create);

      return registerRecord(rateByAssignment);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [RateByAssignmentId] ,
      [AssetAssignmentId] ,
      [BillItemTypeId] ,
      [BillRate] ,
      [InvoiceRate] ,
      [ShouldNotExceedRate] ,
      [Deleted] 
     From [dbo].[RateByAssignment]
    
       Where 
      
         [RateByAssignmentId] = @rateByAssignmentId
    ";

    public RateByAssignment findByPrimaryKey(
    int rateByAssignmentId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@rateByAssignmentId", rateByAssignmentId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("RateByAssignment not found, search by primary key");
 

    }


    public bool exists(RateByAssignment rateByAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@RateByAssignmentId", rateByAssignment.RateByAssignmentId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [RateByAssignment].[RateByAssignmentId] = @CheckInRateByAssignmentId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      RateByAssignment _RateByAssignment = (RateByAssignment)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInRateByAssignmentId", _RateByAssignment.RateByAssignmentId);
      

      return sqlCommand;
    }

  

    protected override RateByAssignment doLoad(IDataReader dataReader)
    {
    RateByAssignment rateByAssignment = new RateByAssignment();

    rateByAssignment.RateByAssignmentId = (int) dataReader.GetValue(0);
            rateByAssignment.AssetAssignmentId = (int) dataReader.GetValue(1);
            rateByAssignment.BillItemTypeId = (int) dataReader.GetValue(2);
            rateByAssignment.BillRate = (decimal) dataReader.GetValue(3);
            rateByAssignment.InvoiceRate = (decimal) dataReader.GetValue(4);
            rateByAssignment.ShouldNotExceedRate = (bool) dataReader.GetValue(5);
            rateByAssignment.Deleted = (bool) dataReader.GetValue(6);
            

    
    
    return registerRecord(rateByAssignment);
    }


    protected override RateByAssignment doLoad(Hashtable hashtable)
    {
      RateByAssignment rateByAssignment = new RateByAssignment();

      
        
        if(hashtable.ContainsKey("RateByAssignmentId"))
        rateByAssignment.RateByAssignmentId = ( int)hashtable["RateByAssignmentId"];
          
        
        if(hashtable.ContainsKey("AssetAssignmentId"))
        rateByAssignment.AssetAssignmentId = ( int)hashtable["AssetAssignmentId"];
          
        
        if(hashtable.ContainsKey("BillItemTypeId"))
        rateByAssignment.BillItemTypeId = ( int)hashtable["BillItemTypeId"];
          
        
        if(hashtable.ContainsKey("BillRate"))
        rateByAssignment.BillRate = ( decimal)hashtable["BillRate"];
          
        
        if(hashtable.ContainsKey("InvoiceRate"))
        rateByAssignment.InvoiceRate = ( decimal)hashtable["InvoiceRate"];
          
        
        if(hashtable.ContainsKey("ShouldNotExceedRate"))
        rateByAssignment.ShouldNotExceedRate = ( bool)hashtable["ShouldNotExceedRate"];
          
        
        if(hashtable.ContainsKey("Deleted"))
        rateByAssignment.Deleted = ( bool)hashtable["Deleted"];
          

      return rateByAssignment;
    }


    protected override List<RateByAssignment> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<RateByAssignment> resultList = new List<RateByAssignment>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              RateByAssignment item = new RateByAssignment();
              
              
                    item.RateByAssignmentId = ( int)dataRow["RateByAssignmentId"] ;
                  
                    item.AssetAssignmentId = ( int)dataRow["AssetAssignmentId"] ;
                  
                    item.BillItemTypeId = ( int)dataRow["BillItemTypeId"] ;
                  
                    item.BillRate = ( decimal)dataRow["BillRate"] ;
                  
                    item.InvoiceRate = ( decimal)dataRow["InvoiceRate"] ;
                  
                    item.ShouldNotExceedRate = ( bool)dataRow["ShouldNotExceedRate"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[RateByAssignment]
    
      Where
      
        [RateByAssignmentId] = @rateByAssignmentId";
    [Synchronized]
    public override RateByAssignment remove(RateByAssignment rateByAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@rateByAssignmentId", rateByAssignment.RateByAssignmentId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(rateByAssignment,DataMapperOperation.delete);

      return registerRecord(rateByAssignment);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override RateByAssignment save( RateByAssignment rateByAssignment )
    {
      if(exists(rateByAssignment))
        return update(rateByAssignment);
        return create(rateByAssignment);
    }

  
      const String SqlUpdate = @"Update [dbo].[RateByAssignment] Set
      
        [AssetAssignmentId] = @assetAssignmentId,
        [BillItemTypeId] = @billItemTypeId,
        [BillRate] = @billRate,
        [InvoiceRate] = @invoiceRate,
        [ShouldNotExceedRate] = @shouldNotExceedRate,
        [Deleted] = @deleted
        Where
        
          [RateByAssignmentId] = @rateByAssignmentId";
    
    [Synchronized]
    public override RateByAssignment update(RateByAssignment rateByAssignment)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@rateByAssignmentId", rateByAssignment.RateByAssignmentId);
            
              sqlCommand.Parameters.AddWithValue("@assetAssignmentId", rateByAssignment.AssetAssignmentId);
            
              sqlCommand.Parameters.AddWithValue("@billItemTypeId", rateByAssignment.BillItemTypeId);
            
              sqlCommand.Parameters.AddWithValue("@billRate", rateByAssignment.BillRate);
            
              sqlCommand.Parameters.AddWithValue("@invoiceRate", rateByAssignment.InvoiceRate);
            
              sqlCommand.Parameters.AddWithValue("@shouldNotExceedRate", rateByAssignment.ShouldNotExceedRate);
            
              sqlCommand.Parameters.AddWithValue("@deleted", rateByAssignment.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(rateByAssignment,DataMapperOperation.update);
        
    
        return registerRecord(rateByAssignment);

    }

  
    }
    
  
    
    public partial class PermissionAssignment: DomainObject
    {
    
      protected int _permissionAssignmentId;
    

      // parent tables
      protected Permission _relatedPermission
        = new Permission()
      ;
    

      // parent tables
      protected Role _relatedRole
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
    
      this.PermissionAssignmentId = permissionAssignmentId;
    
      this.PermissionId = permissionId;
    
      this.RoleId = roleId;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.PermissionAssignment."
    
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
            
                  if(_relatedPermission != null)
                    return _relatedPermission.PermissionId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedPermission == null)
                        _relatedPermission = new Permission();

                      _relatedPermission.PermissionId = value;
                    
            }
          
      }
    

      public int RoleId
      {
        
            get
            {
            
                  if(_relatedRole != null)
                    return _relatedRole.RoleId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedRole == null)
                        _relatedRole = new Role();

                      _relatedRole.RoleId = value;
                    
            }
          
      }
    

      public Permission RelatedPermission
      {
      get { return _relatedPermission;}
      set { _relatedPermission = value; }
      }
      
    

      public Role RelatedRole
      {
      get { return _relatedRole;}
      set { _relatedRole = value; }
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
  

    public abstract class _PermissionAssignmentDataMapper:TDataMapper<PermissionAssignment,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _PermissionAssignmentDataMapper(){}
      public _PermissionAssignmentDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[PermissionAssignment]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[PermissionAssignment] (
    
      [PermissionId]
      ,
      [RoleId]
      ) Values (
    
      @permissionId,
      @roleId);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override PermissionAssignment create( PermissionAssignment permissionAssignment )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@permissionId", permissionAssignment.PermissionId);
              
                  sqlCommand.Parameters.AddWithValue("@roleId", permissionAssignment.RoleId);
              permissionAssignment.PermissionAssignmentId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(permissionAssignment,DataMapperOperation.create);

      return registerRecord(permissionAssignment);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [PermissionAssignmentId] ,
      [PermissionId] ,
      [RoleId] 
     From [dbo].[PermissionAssignment]
    
       Where 
      
         [PermissionAssignmentId] = @permissionAssignmentId
    ";

    public PermissionAssignment findByPrimaryKey(
    int permissionAssignmentId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@permissionAssignmentId", permissionAssignmentId);
        

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

    permissionAssignment.PermissionAssignmentId = (int) dataReader.GetValue(0);
            permissionAssignment.PermissionId = (int) dataReader.GetValue(1);
            permissionAssignment.RoleId = (int) dataReader.GetValue(2);
            

    
    
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
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[PermissionAssignment]
    
      Where
      
        [PermissionAssignmentId] = @permissionAssignmentId";
    [Synchronized]
    public override PermissionAssignment remove(PermissionAssignment permissionAssignment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@permissionAssignmentId", permissionAssignment.PermissionAssignmentId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(permissionAssignment,DataMapperOperation.delete);

      return registerRecord(permissionAssignment);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override PermissionAssignment save( PermissionAssignment permissionAssignment )
    {
      if(exists(permissionAssignment))
        return update(permissionAssignment);
        return create(permissionAssignment);
    }

  
      const String SqlUpdate = @"Update [dbo].[PermissionAssignment] Set
      
        [PermissionId] = @permissionId,
        [RoleId] = @roleId
        Where
        
          [PermissionAssignmentId] = @permissionAssignmentId";
    
    [Synchronized]
    public override PermissionAssignment update(PermissionAssignment permissionAssignment)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@permissionAssignmentId", permissionAssignment.PermissionAssignmentId);
            
              sqlCommand.Parameters.AddWithValue("@permissionId", permissionAssignment.PermissionId);
            
              sqlCommand.Parameters.AddWithValue("@roleId", permissionAssignment.RoleId);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(permissionAssignment,DataMapperOperation.update);
        
    
        return registerRecord(permissionAssignment);

    }

  
    }
    
  
    
    public partial class BillItemAttachment: DomainObject
    {
    
      protected int _billItemAttachmentId;
    
      protected String _fileName;
    
      protected String _originalFileName;
    

      // parent tables
      protected BillItem _relatedBillItem
        = new BillItem()
      ;
    

    public BillItemAttachment(){}

    public BillItemAttachment(
    int 
            billItemAttachmentId,int 
            billItemId,String 
            fileName,String 
            originalFileName
    )
    {
    
      this.BillItemAttachmentId = billItemAttachmentId;
    
      this.BillItemId = billItemId;
    
      this.FileName = fileName;
    
      this.OriginalFileName = originalFileName;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.BillItemAttachment."
    
      + BillItemAttachmentId.ToString()
    ;
    
    return uri;
    }

    

      public int BillItemAttachmentId
      {
        
            get { return _billItemAttachmentId;}
            set 
            { 
                _billItemAttachmentId = value;
            }
          
      }
    

      public int BillItemId
      {
        
            get
            {
            
                  if(_relatedBillItem != null)
                    return _relatedBillItem.BillItemId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedBillItem == null)
                        _relatedBillItem = new BillItem();

                      _relatedBillItem.BillItemId = value;
                    
            }
          
      }
    

      public String FileName
      {
        
            get { return _fileName;}
            set 
            { 
                _fileName = value;
            }
          
      }
    

      public String OriginalFileName
      {
        
            get { return _originalFileName;}
            set 
            { 
                _originalFileName = value;
            }
          
      }
    

      public BillItem RelatedBillItem
      {
      get { return _relatedBillItem;}
      set { _relatedBillItem = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      BillItemAttachment billItemAttachment = new BillItemAttachment();
      
      billItemAttachment.BillItemAttachmentId = this.BillItemAttachmentId;
      billItemAttachment.BillItemId = this.BillItemId;
      billItemAttachment.FileName = this.FileName;
      billItemAttachment.OriginalFileName = this.OriginalFileName;
      billItemAttachment.ActiveRecordId = this.ActiveRecordId; 
      return billItemAttachment;
    }

    
    }
  

    public abstract class _BillItemAttachmentDataMapper:TDataMapper<BillItemAttachment,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _BillItemAttachmentDataMapper(){}
      public _BillItemAttachmentDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[BillItemAttachment]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[BillItemAttachment] (
    
      [BillItemId]
      ,
      [FileName]
      ,
      [OriginalFileName]
      ) Values (
    
      @billItemId,
      @fileName,
      @originalFileName);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override BillItemAttachment create( BillItemAttachment billItemAttachment )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@billItemId", billItemAttachment.BillItemId);
              
                  sqlCommand.Parameters.AddWithValue("@fileName", billItemAttachment.FileName);
              
                  sqlCommand.Parameters.AddWithValue("@originalFileName", billItemAttachment.OriginalFileName);
              billItemAttachment.BillItemAttachmentId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(billItemAttachment,DataMapperOperation.create);

      return registerRecord(billItemAttachment);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [BillItemAttachmentId] ,
      [BillItemId] ,
      [FileName] ,
      [OriginalFileName] 
     From [dbo].[BillItemAttachment]
    
       Where 
      
         [BillItemAttachmentId] = @billItemAttachmentId
    ";

    public BillItemAttachment findByPrimaryKey(
    int billItemAttachmentId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@billItemAttachmentId", billItemAttachmentId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("BillItemAttachment not found, search by primary key");
 

    }


    public bool exists(BillItemAttachment billItemAttachment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@BillItemAttachmentId", billItemAttachment.BillItemAttachmentId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [BillItemAttachment].[BillItemAttachmentId] = @CheckInBillItemAttachmentId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      BillItemAttachment _BillItemAttachment = (BillItemAttachment)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInBillItemAttachmentId", _BillItemAttachment.BillItemAttachmentId);
      

      return sqlCommand;
    }

  

    protected override BillItemAttachment doLoad(IDataReader dataReader)
    {
    BillItemAttachment billItemAttachment = new BillItemAttachment();

    billItemAttachment.BillItemAttachmentId = (int) dataReader.GetValue(0);
            billItemAttachment.BillItemId = (int) dataReader.GetValue(1);
            billItemAttachment.FileName = (String) dataReader.GetValue(2);
            billItemAttachment.OriginalFileName = (String) dataReader.GetValue(3);
            

    
    
    return registerRecord(billItemAttachment);
    }


    protected override BillItemAttachment doLoad(Hashtable hashtable)
    {
      BillItemAttachment billItemAttachment = new BillItemAttachment();

      
        
        if(hashtable.ContainsKey("BillItemAttachmentId"))
        billItemAttachment.BillItemAttachmentId = ( int)hashtable["BillItemAttachmentId"];
          
        
        if(hashtable.ContainsKey("BillItemId"))
        billItemAttachment.BillItemId = ( int)hashtable["BillItemId"];
          
        
        if(hashtable.ContainsKey("FileName"))
        billItemAttachment.FileName = ( String)hashtable["FileName"];
          
        
        if(hashtable.ContainsKey("OriginalFileName"))
        billItemAttachment.OriginalFileName = ( String)hashtable["OriginalFileName"];
          

      return billItemAttachment;
    }


    protected override List<BillItemAttachment> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<BillItemAttachment> resultList = new List<BillItemAttachment>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              BillItemAttachment item = new BillItemAttachment();
              
              
                    item.BillItemAttachmentId = ( int)dataRow["BillItemAttachmentId"] ;
                  
                    item.BillItemId = ( int)dataRow["BillItemId"] ;
                  
                    item.FileName = ( String)dataRow["FileName"] ;
                  
                    item.OriginalFileName = ( String)dataRow["OriginalFileName"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[BillItemAttachment]
    
      Where
      
        [BillItemAttachmentId] = @billItemAttachmentId";
    [Synchronized]
    public override BillItemAttachment remove(BillItemAttachment billItemAttachment)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@billItemAttachmentId", billItemAttachment.BillItemAttachmentId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(billItemAttachment,DataMapperOperation.delete);

      return registerRecord(billItemAttachment);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override BillItemAttachment save( BillItemAttachment billItemAttachment )
    {
      if(exists(billItemAttachment))
        return update(billItemAttachment);
        return create(billItemAttachment);
    }

  
      const String SqlUpdate = @"Update [dbo].[BillItemAttachment] Set
      
        [BillItemId] = @billItemId,
        [FileName] = @fileName,
        [OriginalFileName] = @originalFileName
        Where
        
          [BillItemAttachmentId] = @billItemAttachmentId";
    
    [Synchronized]
    public override BillItemAttachment update(BillItemAttachment billItemAttachment)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@billItemAttachmentId", billItemAttachment.BillItemAttachmentId);
            
              sqlCommand.Parameters.AddWithValue("@billItemId", billItemAttachment.BillItemId);
            
              sqlCommand.Parameters.AddWithValue("@fileName", billItemAttachment.FileName);
            
              sqlCommand.Parameters.AddWithValue("@originalFileName", billItemAttachment.OriginalFileName);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(billItemAttachment,DataMapperOperation.update);
        
    
        return registerRecord(billItemAttachment);

    }

  
    }
    
  
    
    public partial class InvoiceItem: DomainObject
    {
    
      protected int _invoiceItemId;
    
      protected String _invoiceDate;
    
      protected int? _qty;
    
      protected decimal? _invoiceRate;
    
      protected String _notes;
    
      protected bool _isSelected;
    

      // parent tables
      protected AssetAssignment _relatedAssetAssignment
        = new AssetAssignment()
      ;
    

      // parent tables
      protected BillItem _relatedBillItem;
    

      // parent tables
      protected Invoice _relatedInvoice
        = new Invoice()
      ;
    

      // parent tables
      protected InvoiceItemStatus _relatedInvoiceItemStatus
        = new InvoiceItemStatus()
      ;
    

      // parent tables
      protected InvoiceItemType _relatedInvoiceItemType
        = new InvoiceItemType()
      ;
    

    public InvoiceItem(){}

    public InvoiceItem(
    int 
            invoiceItemId,int 
            invoiceItemTypeId,int 
            invoiceId,int 
            billItemId,int 
            assetAssignmentId,String 
            invoiceDate,int 
            qty,decimal 
            invoiceRate,String 
            status,String 
            notes,bool 
            isSelected
    )
    {
    
      this.InvoiceItemId = invoiceItemId;
    
      this.InvoiceItemTypeId = invoiceItemTypeId;
    
      this.InvoiceId = invoiceId;
    
      this.BillItemId = billItemId;
    
      this.AssetAssignmentId = assetAssignmentId;
    
      this.InvoiceDate = invoiceDate;
    
      this.Qty = qty;
    
      this.InvoiceRate = invoiceRate;
    
      this.Status = status;
    
      this.Notes = notes;
    
      this.IsSelected = isSelected;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.InvoiceItem."
    
      + InvoiceItemId.ToString()
    ;
    
    return uri;
    }

    

      public int InvoiceItemId
      {
        
            get { return _invoiceItemId;}
            set 
            { 
                _invoiceItemId = value;
            }
          
      }
    

      public int InvoiceItemTypeId
      {
        
            get
            {
            
                  if(_relatedInvoiceItemType != null)
                    return _relatedInvoiceItemType.InvoiceItemTypeId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedInvoiceItemType == null)
                        _relatedInvoiceItemType = new InvoiceItemType();

                      _relatedInvoiceItemType.InvoiceItemTypeId = value;
                    
            }
          
      }
    

      public int InvoiceId
      {
        
            get
            {
            
                  if(_relatedInvoice != null)
                    return _relatedInvoice.InvoiceId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedInvoice == null)
                        _relatedInvoice = new Invoice();

                      _relatedInvoice.InvoiceId = value;
                    
            }
          
      }
    

      public int? BillItemId
      {
        
            get
            {
            
                  if(_relatedBillItem != null)
                    return _relatedBillItem.BillItemId;

                return null;
            }
            set
            {
            
                      if(!value.HasValue)
                        _relatedBillItem = null;
                      else
                      {
                      if(_relatedBillItem == null)
                        _relatedBillItem = new BillItem();

                        _relatedBillItem.BillItemId = value.Value;
                      }
                    
            }
          
      }
    

      public int AssetAssignmentId
      {
        
            get
            {
            
                  if(_relatedAssetAssignment != null)
                    return _relatedAssetAssignment.AssetAssignmentId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAssetAssignment == null)
                        _relatedAssetAssignment = new AssetAssignment();

                      _relatedAssetAssignment.AssetAssignmentId = value;
                    
            }
          
      }
    

      public String InvoiceDate
      {
        
            get { return _invoiceDate;}
            set 
            { 
                _invoiceDate = value;
            }
          
      }
    

      public int? Qty
      {
        
            get { return _qty;}
            set 
            { 
                _qty = value;
            }
          
      }
    

      public decimal? InvoiceRate
      {
        
            get { return _invoiceRate;}
            set 
            { 
                _invoiceRate = value;
            }
          
      }
    

      public String Status
      {
        
            get
            {
            
                  if(_relatedInvoiceItemStatus != null)
                    return _relatedInvoiceItemStatus.Status;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedInvoiceItemStatus == null)
                        _relatedInvoiceItemStatus = new InvoiceItemStatus();

                      _relatedInvoiceItemStatus.Status = value;
                    
            }
          
      }
    

      public String Notes
      {
        
            get { return _notes;}
            set 
            { 
                _notes = value;
            }
          
      }
    

      public bool IsSelected
      {
        
            get { return _isSelected;}
            set 
            { 
                _isSelected = value;
            }
          
      }
    

      public AssetAssignment RelatedAssetAssignment
      {
      get { return _relatedAssetAssignment;}
      set { _relatedAssetAssignment = value; }
      }
      
    

      public BillItem RelatedBillItem
      {
      get { return _relatedBillItem;}
      set { _relatedBillItem = value; }
      }
      
    

      public Invoice RelatedInvoice
      {
      get { return _relatedInvoice;}
      set { _relatedInvoice = value; }
      }
      
    

      public InvoiceItemStatus RelatedInvoiceItemStatus
      {
      get { return _relatedInvoiceItemStatus;}
      set { _relatedInvoiceItemStatus = value; }
      }
      
    

      public InvoiceItemType RelatedInvoiceItemType
      {
      get { return _relatedInvoiceItemType;}
      set { _relatedInvoiceItemType = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      InvoiceItem invoiceItem = new InvoiceItem();
      
      invoiceItem.InvoiceItemId = this.InvoiceItemId;
      invoiceItem.InvoiceItemTypeId = this.InvoiceItemTypeId;
      invoiceItem.InvoiceId = this.InvoiceId;
      invoiceItem.BillItemId = this.BillItemId;
      invoiceItem.AssetAssignmentId = this.AssetAssignmentId;
      invoiceItem.InvoiceDate = this.InvoiceDate;
      invoiceItem.Qty = this.Qty;
      invoiceItem.InvoiceRate = this.InvoiceRate;
      invoiceItem.Status = this.Status;
      invoiceItem.Notes = this.Notes;
      invoiceItem.IsSelected = this.IsSelected;
      invoiceItem.ActiveRecordId = this.ActiveRecordId; 
      return invoiceItem;
    }

    
    }
  

    public abstract class _InvoiceItemDataMapper:TDataMapper<InvoiceItem,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _InvoiceItemDataMapper(){}
      public _InvoiceItemDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[InvoiceItem]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[InvoiceItem] (
    
      [InvoiceItemTypeId]
      ,
      [InvoiceId]
      ,
      [BillItemId]
      ,
      [AssetAssignmentId]
      ,
      [InvoiceDate]
      ,
      [Qty]
      ,
      [InvoiceRate]
      ,
      [Status]
      ,
      [Notes]
      ,
      [IsSelected]
      ) Values (
    
      @invoiceItemTypeId,
      @invoiceId,
      @billItemId,
      @assetAssignmentId,
      @invoiceDate,
      @qty,
      @invoiceRate,
      @status,
      @notes,
      @isSelected);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override InvoiceItem create( InvoiceItem invoiceItem )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@invoiceItemTypeId", invoiceItem.InvoiceItemTypeId);
              
                  sqlCommand.Parameters.AddWithValue("@invoiceId", invoiceItem.InvoiceId);
              
                    if(invoiceItem.BillItemId.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@billItemId", invoiceItem.BillItemId);
                else
                  sqlCommand.Parameters.AddWithValue("@billItemId", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@assetAssignmentId", invoiceItem.AssetAssignmentId);
              
                  sqlCommand.Parameters.AddWithValue("@invoiceDate", invoiceItem.InvoiceDate);
              
                    if(invoiceItem.Qty.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@qty", invoiceItem.Qty);
                else
                  sqlCommand.Parameters.AddWithValue("@qty", DBNull.Value);
              
                    if(invoiceItem.InvoiceRate.HasValue)
                  
                  sqlCommand.Parameters.AddWithValue("@invoiceRate", invoiceItem.InvoiceRate);
                else
                  sqlCommand.Parameters.AddWithValue("@invoiceRate", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@status", invoiceItem.Status);
              
                    if(invoiceItem.Notes != null)
                  
                  sqlCommand.Parameters.AddWithValue("@notes", invoiceItem.Notes);
                else
                  sqlCommand.Parameters.AddWithValue("@notes", DBNull.Value);
              
                  sqlCommand.Parameters.AddWithValue("@isSelected", invoiceItem.IsSelected);
              invoiceItem.InvoiceItemId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(invoiceItem,DataMapperOperation.create);

      return registerRecord(invoiceItem);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [InvoiceItemId] ,
      [InvoiceItemTypeId] ,
      [InvoiceId] ,
      [BillItemId] ,
      [AssetAssignmentId] ,
      [InvoiceDate] ,
      [Qty] ,
      [InvoiceRate] ,
      [Status] ,
      [Notes] ,
      [IsSelected] 
     From [dbo].[InvoiceItem]
    
       Where 
      
         [InvoiceItemId] = @invoiceItemId
    ";

    public InvoiceItem findByPrimaryKey(
    int invoiceItemId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@invoiceItemId", invoiceItemId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("InvoiceItem not found, search by primary key");
 

    }


    public bool exists(InvoiceItem invoiceItem)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@InvoiceItemId", invoiceItem.InvoiceItemId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [InvoiceItem].[InvoiceItemId] = @CheckInInvoiceItemId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      InvoiceItem _InvoiceItem = (InvoiceItem)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInInvoiceItemId", _InvoiceItem.InvoiceItemId);
      

      return sqlCommand;
    }

  

    protected override InvoiceItem doLoad(IDataReader dataReader)
    {
    InvoiceItem invoiceItem = new InvoiceItem();

    invoiceItem.InvoiceItemId = (int) dataReader.GetValue(0);
            invoiceItem.InvoiceItemTypeId = (int) dataReader.GetValue(1);
            invoiceItem.InvoiceId = (int) dataReader.GetValue(2);
            
          if(!dataReader.IsDBNull(3))        
          invoiceItem.BillItemId = (int) dataReader.GetValue(3);
            invoiceItem.AssetAssignmentId = (int) dataReader.GetValue(4);
            invoiceItem.InvoiceDate = (String) dataReader.GetValue(5);
            
          if(!dataReader.IsDBNull(6))        
          invoiceItem.Qty = (int) dataReader.GetValue(6);
            
          if(!dataReader.IsDBNull(7))        
          invoiceItem.InvoiceRate = (decimal) dataReader.GetValue(7);
            invoiceItem.Status = (String) dataReader.GetValue(8);
            
          if(!dataReader.IsDBNull(9))        
          invoiceItem.Notes = (String) dataReader.GetValue(9);
            invoiceItem.IsSelected = (bool) dataReader.GetValue(10);
            

    
    
    return registerRecord(invoiceItem);
    }


    protected override InvoiceItem doLoad(Hashtable hashtable)
    {
      InvoiceItem invoiceItem = new InvoiceItem();

      
        
        if(hashtable.ContainsKey("InvoiceItemId"))
        invoiceItem.InvoiceItemId = ( int)hashtable["InvoiceItemId"];
          
        
        if(hashtable.ContainsKey("InvoiceItemTypeId"))
        invoiceItem.InvoiceItemTypeId = ( int)hashtable["InvoiceItemTypeId"];
          
        
        if(hashtable.ContainsKey("InvoiceId"))
        invoiceItem.InvoiceId = ( int)hashtable["InvoiceId"];
          
        
        if(hashtable.ContainsKey("BillItemId"))
        invoiceItem.BillItemId = ( int)hashtable["BillItemId"];
          
        
        if(hashtable.ContainsKey("AssetAssignmentId"))
        invoiceItem.AssetAssignmentId = ( int)hashtable["AssetAssignmentId"];
          
        
        if(hashtable.ContainsKey("InvoiceDate"))
        invoiceItem.InvoiceDate = ( String)hashtable["InvoiceDate"];
          
        
        if(hashtable.ContainsKey("Qty"))
        invoiceItem.Qty = ( int)hashtable["Qty"];
          
        
        if(hashtable.ContainsKey("InvoiceRate"))
        invoiceItem.InvoiceRate = ( decimal)hashtable["InvoiceRate"];
          
        
        if(hashtable.ContainsKey("Status"))
        invoiceItem.Status = ( String)hashtable["Status"];
          
        
        if(hashtable.ContainsKey("Notes"))
        invoiceItem.Notes = ( String)hashtable["Notes"];
          
        
        if(hashtable.ContainsKey("IsSelected"))
        invoiceItem.IsSelected = ( bool)hashtable["IsSelected"];
          

      return invoiceItem;
    }


    protected override List<InvoiceItem> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<InvoiceItem> resultList = new List<InvoiceItem>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              InvoiceItem item = new InvoiceItem();
              
              
                    item.InvoiceItemId = ( int)dataRow["InvoiceItemId"] ;
                  
                    item.InvoiceItemTypeId = ( int)dataRow["InvoiceItemTypeId"] ;
                  
                    item.InvoiceId = ( int)dataRow["InvoiceId"] ;
                  
                  if(!dataRow.IsNull("BillItemId"))
                
                    item.BillItemId = ( int)dataRow["BillItemId"] ;
                  
                    item.AssetAssignmentId = ( int)dataRow["AssetAssignmentId"] ;
                  
                    item.InvoiceDate = ( String)dataRow["InvoiceDate"] ;
                  
                  if(!dataRow.IsNull("Qty"))
                
                    item.Qty = ( int)dataRow["Qty"] ;
                  
                  if(!dataRow.IsNull("InvoiceRate"))
                
                    item.InvoiceRate = ( decimal)dataRow["InvoiceRate"] ;
                  
                    item.Status = ( String)dataRow["Status"] ;
                  
                  if(!dataRow.IsNull("Notes"))
                
                    item.Notes = ( String)dataRow["Notes"] ;
                  
                    item.IsSelected = ( bool)dataRow["IsSelected"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[InvoiceItem]
    
      Where
      
        [InvoiceItemId] = @invoiceItemId";
    [Synchronized]
    public override InvoiceItem remove(InvoiceItem invoiceItem)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@invoiceItemId", invoiceItem.InvoiceItemId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(invoiceItem,DataMapperOperation.delete);

      return registerRecord(invoiceItem);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override InvoiceItem save( InvoiceItem invoiceItem )
    {
      if(exists(invoiceItem))
        return update(invoiceItem);
        return create(invoiceItem);
    }

  
      const String SqlUpdate = @"Update [dbo].[InvoiceItem] Set
      
        [InvoiceItemTypeId] = @invoiceItemTypeId,
        [InvoiceId] = @invoiceId,
        [BillItemId] = @billItemId,
        [AssetAssignmentId] = @assetAssignmentId,
        [InvoiceDate] = @invoiceDate,
        [Qty] = @qty,
        [InvoiceRate] = @invoiceRate,
        [Status] = @status,
        [Notes] = @notes,
        [IsSelected] = @isSelected
        Where
        
          [InvoiceItemId] = @invoiceItemId";
    
    [Synchronized]
    public override InvoiceItem update(InvoiceItem invoiceItem)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@invoiceItemId", invoiceItem.InvoiceItemId);
            
              sqlCommand.Parameters.AddWithValue("@invoiceItemTypeId", invoiceItem.InvoiceItemTypeId);
            
              sqlCommand.Parameters.AddWithValue("@invoiceId", invoiceItem.InvoiceId);
            
                  if(invoiceItem.BillItemId.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@billItemId", invoiceItem.BillItemId);
              else
              sqlCommand.Parameters.AddWithValue("@billItemId", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@assetAssignmentId", invoiceItem.AssetAssignmentId);
            
              sqlCommand.Parameters.AddWithValue("@invoiceDate", invoiceItem.InvoiceDate);
            
                  if(invoiceItem.Qty.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@qty", invoiceItem.Qty);
              else
              sqlCommand.Parameters.AddWithValue("@qty", DBNull.Value);
            
                  if(invoiceItem.InvoiceRate.HasValue)
                
              sqlCommand.Parameters.AddWithValue("@invoiceRate", invoiceItem.InvoiceRate);
              else
              sqlCommand.Parameters.AddWithValue("@invoiceRate", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@status", invoiceItem.Status);
            
                  if(invoiceItem.Notes != null)
                
              sqlCommand.Parameters.AddWithValue("@notes", invoiceItem.Notes);
              else
              sqlCommand.Parameters.AddWithValue("@notes", DBNull.Value);
            
              sqlCommand.Parameters.AddWithValue("@isSelected", invoiceItem.IsSelected);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(invoiceItem,DataMapperOperation.update);
        
    
        return registerRecord(invoiceItem);

    }

  
    }
    
  
    
    public partial class SyncLog: DomainObject
    {
    
      protected int _syncLogId;
    
      protected int _assetId;
    
      protected String _deviceId;
    

    public SyncLog(){}

    public SyncLog(
    int 
            syncLogId,int 
            assetId,String 
            deviceId
    )
    {
    
      this.SyncLogId = syncLogId;
    
      this.AssetId = assetId;
    
      this.DeviceId = deviceId;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.SyncLog."
    
      + SyncLogId.ToString()
    ;
    
    return uri;
    }

    

      public int SyncLogId
      {
        
            get { return _syncLogId;}
            set 
            { 
                _syncLogId = value;
            }
          
      }
    

      public int AssetId
      {
        
            get { return _assetId;}
            set 
            { 
                _assetId = value;
            }
          
      }
    

      public String DeviceId
      {
        
            get { return _deviceId;}
            set 
            { 
                _deviceId = value;
            }
          
      }
    


    public override DomainObject extractSingleObject()
    {
      SyncLog syncLog = new SyncLog();
      
      syncLog.SyncLogId = this.SyncLogId;
      syncLog.AssetId = this.AssetId;
      syncLog.DeviceId = this.DeviceId;
      syncLog.ActiveRecordId = this.ActiveRecordId; 
      return syncLog;
    }

    
    }
  

    public abstract class _SyncLogDataMapper:TDataMapper<SyncLog,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _SyncLogDataMapper(){}
      public _SyncLogDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[SyncLog]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[SyncLog] (
    
      [AssetId]
      ,
      [DeviceId]
      ) Values (
    
      @assetId,
      @deviceId);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override SyncLog create( SyncLog syncLog )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@assetId", syncLog.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@deviceId", syncLog.DeviceId);
              syncLog.SyncLogId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(syncLog,DataMapperOperation.create);

      return registerRecord(syncLog);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [SyncLogId] ,
      [AssetId] ,
      [DeviceId] 
     From [dbo].[SyncLog]
    
       Where 
      
         [SyncLogId] = @syncLogId
    ";

    public SyncLog findByPrimaryKey(
    int syncLogId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@syncLogId", syncLogId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("SyncLog not found, search by primary key");
 

    }


    public bool exists(SyncLog syncLog)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@SyncLogId", syncLog.SyncLogId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [SyncLog].[SyncLogId] = @CheckInSyncLogId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      SyncLog _SyncLog = (SyncLog)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInSyncLogId", _SyncLog.SyncLogId);
      

      return sqlCommand;
    }

  

    protected override SyncLog doLoad(IDataReader dataReader)
    {
    SyncLog syncLog = new SyncLog();

    syncLog.SyncLogId = (int) dataReader.GetValue(0);
            syncLog.AssetId = (int) dataReader.GetValue(1);
            syncLog.DeviceId = (String) dataReader.GetValue(2);
            

    
    
    return registerRecord(syncLog);
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


    protected override List<SyncLog> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<SyncLog> resultList = new List<SyncLog>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              SyncLog item = new SyncLog();
              
              
                    item.SyncLogId = ( int)dataRow["SyncLogId"] ;
                  
                    item.AssetId = ( int)dataRow["AssetId"] ;
                  
                    item.DeviceId = ( String)dataRow["DeviceId"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[SyncLog]
    
      Where
      
        [SyncLogId] = @syncLogId";
    [Synchronized]
    public override SyncLog remove(SyncLog syncLog)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@syncLogId", syncLog.SyncLogId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(syncLog,DataMapperOperation.delete);

      return registerRecord(syncLog);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override SyncLog save( SyncLog syncLog )
    {
      if(exists(syncLog))
        return update(syncLog);
        return create(syncLog);
    }

  
      const String SqlUpdate = @"Update [dbo].[SyncLog] Set
      
        [AssetId] = @assetId,
        [DeviceId] = @deviceId
        Where
        
          [SyncLogId] = @syncLogId";
    
    [Synchronized]
    public override SyncLog update(SyncLog syncLog)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@syncLogId", syncLog.SyncLogId);
            
              sqlCommand.Parameters.AddWithValue("@assetId", syncLog.AssetId);
            
              sqlCommand.Parameters.AddWithValue("@deviceId", syncLog.DeviceId);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(syncLog,DataMapperOperation.update);
        
    
        return registerRecord(syncLog);

    }

  
    }
    
  
    
    public partial class Message: DomainObject
    {
    
      protected int _messageId;
    
      protected DateTime _posted;
    
      protected String _subject;
    
      protected String _body;
    
      protected bool _isRead;
    

      // parent tables
      protected User _relatedUser
        = new User()
      ;
    

      // parent tables
      protected User _relatedUser1
        = new User()
      ;
    

    public Message(){}

    public Message(
    int 
            messageId,int 
            senderUserId,int 
            receiverUserId,DateTime 
            posted,String 
            subject,String 
            body,bool 
            isRead
    )
    {
    
      this.MessageId = messageId;
    
      this.SenderUserId = senderUserId;
    
      this.ReceiverUserId = receiverUserId;
    
      this.Posted = posted;
    
      this.Subject = subject;
    
      this.Body = body;
    
      this.IsRead = isRead;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.Message."
    
      + MessageId.ToString()
    ;
    
    return uri;
    }

    

      public int MessageId
      {
        
            get { return _messageId;}
            set 
            { 
                _messageId = value;
            }
          
      }
    

      public int SenderUserId
      {
        
            get
            {
            
                  if(_relatedUser1 != null)
                    return _relatedUser1.UserId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedUser1 == null)
                        _relatedUser1 = new User();

                      _relatedUser1.UserId = value;
                    
            }
          
      }
    

      public int ReceiverUserId
      {
        
            get
            {
            
                  if(_relatedUser != null)
                    return _relatedUser.UserId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedUser == null)
                        _relatedUser = new User();

                      _relatedUser.UserId = value;
                    
            }
          
      }
    

      public DateTime Posted
      {
        
            get { return _posted;}
            set 
            { 
                _posted = value;
            }
          
      }
    

      public String Subject
      {
        
            get { return _subject;}
            set 
            { 
                _subject = value;
            }
          
      }
    

      public String Body
      {
        
            get { return _body;}
            set 
            { 
                _body = value;
            }
          
      }
    

      public bool IsRead
      {
        
            get { return _isRead;}
            set 
            { 
                _isRead = value;
            }
          
      }
    

      public User RelatedUser
      {
      get { return _relatedUser;}
      set { _relatedUser = value; }
      }
      
    

      public User RelatedUser1
      {
      get { return _relatedUser1;}
      set { _relatedUser1 = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      Message message = new Message();
      
      message.MessageId = this.MessageId;
      message.SenderUserId = this.SenderUserId;
      message.ReceiverUserId = this.ReceiverUserId;
      message.Posted = this.Posted;
      message.Subject = this.Subject;
      message.Body = this.Body;
      message.IsRead = this.IsRead;
      message.ActiveRecordId = this.ActiveRecordId; 
      return message;
    }

    
    }
  

    public abstract class _MessageDataMapper:TDataMapper<Message,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _MessageDataMapper(){}
      public _MessageDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[Message]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[Message] (
    
      [SenderUserId]
      ,
      [ReceiverUserId]
      ,
      [Posted]
      ,
      [Subject]
      ,
      [Body]
      ,
      [IsRead]
      ) Values (
    
      @senderUserId,
      @receiverUserId,
      @posted,
      @subject,
      @body,
      @isRead);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Message create( Message message )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@senderUserId", message.SenderUserId);
              
                  sqlCommand.Parameters.AddWithValue("@receiverUserId", message.ReceiverUserId);
              
                  sqlCommand.Parameters.AddWithValue("@posted", message.Posted);
              
                  sqlCommand.Parameters.AddWithValue("@subject", message.Subject);
              
                  sqlCommand.Parameters.AddWithValue("@body", message.Body);
              
                  sqlCommand.Parameters.AddWithValue("@isRead", message.IsRead);
              message.MessageId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(message,DataMapperOperation.create);

      return registerRecord(message);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [MessageId] ,
      [SenderUserId] ,
      [ReceiverUserId] ,
      [Posted] ,
      [Subject] ,
      [Body] ,
      [IsRead] 
     From [dbo].[Message]
    
       Where 
      
         [MessageId] = @messageId
    ";

    public Message findByPrimaryKey(
    int messageId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@messageId", messageId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Message not found, search by primary key");
 

    }


    public bool exists(Message message)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@MessageId", message.MessageId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [Message].[MessageId] = @CheckInMessageId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      Message _Message = (Message)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInMessageId", _Message.MessageId);
      

      return sqlCommand;
    }

  

    protected override Message doLoad(IDataReader dataReader)
    {
    Message message = new Message();

    message.MessageId = (int) dataReader.GetValue(0);
            message.SenderUserId = (int) dataReader.GetValue(1);
            message.ReceiverUserId = (int) dataReader.GetValue(2);
            message.Posted = (DateTime) dataReader.GetValue(3);
            message.Subject = (String) dataReader.GetValue(4);
            message.Body = (String) dataReader.GetValue(5);
            message.IsRead = (bool) dataReader.GetValue(6);
            

    
    
    return registerRecord(message);
    }


    protected override Message doLoad(Hashtable hashtable)
    {
      Message message = new Message();

      
        
        if(hashtable.ContainsKey("MessageId"))
        message.MessageId = ( int)hashtable["MessageId"];
          
        
        if(hashtable.ContainsKey("SenderUserId"))
        message.SenderUserId = ( int)hashtable["SenderUserId"];
          
        
        if(hashtable.ContainsKey("ReceiverUserId"))
        message.ReceiverUserId = ( int)hashtable["ReceiverUserId"];
          
        
        if(hashtable.ContainsKey("Posted"))
        message.Posted = ( DateTime)hashtable["Posted"];
          
        
        if(hashtable.ContainsKey("Subject"))
        message.Subject = ( String)hashtable["Subject"];
          
        
        if(hashtable.ContainsKey("Body"))
        message.Body = ( String)hashtable["Body"];
          
        
        if(hashtable.ContainsKey("IsRead"))
        message.IsRead = ( bool)hashtable["IsRead"];
          

      return message;
    }


    protected override List<Message> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<Message> resultList = new List<Message>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              Message item = new Message();
              
              
                    item.MessageId = ( int)dataRow["MessageId"] ;
                  
                    item.SenderUserId = ( int)dataRow["SenderUserId"] ;
                  
                    item.ReceiverUserId = ( int)dataRow["ReceiverUserId"] ;
                  
                    item.Posted = ( DateTime)dataRow["Posted"] ;
                  
                    item.Subject = ( String)dataRow["Subject"] ;
                  
                    item.Body = ( String)dataRow["Body"] ;
                  
                    item.IsRead = ( bool)dataRow["IsRead"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[Message]
    
      Where
      
        [MessageId] = @messageId";
    [Synchronized]
    public override Message remove(Message message)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@messageId", message.MessageId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(message,DataMapperOperation.delete);

      return registerRecord(message);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override Message save( Message message )
    {
      if(exists(message))
        return update(message);
        return create(message);
    }

  
      const String SqlUpdate = @"Update [dbo].[Message] Set
      
        [SenderUserId] = @senderUserId,
        [ReceiverUserId] = @receiverUserId,
        [Posted] = @posted,
        [Subject] = @subject,
        [Body] = @body,
        [IsRead] = @isRead
        Where
        
          [MessageId] = @messageId";
    
    [Synchronized]
    public override Message update(Message message)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@messageId", message.MessageId);
            
              sqlCommand.Parameters.AddWithValue("@senderUserId", message.SenderUserId);
            
              sqlCommand.Parameters.AddWithValue("@receiverUserId", message.ReceiverUserId);
            
              sqlCommand.Parameters.AddWithValue("@posted", message.Posted);
            
              sqlCommand.Parameters.AddWithValue("@subject", message.Subject);
            
              sqlCommand.Parameters.AddWithValue("@body", message.Body);
            
              sqlCommand.Parameters.AddWithValue("@isRead", message.IsRead);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(message,DataMapperOperation.update);
        
    
        return registerRecord(message);

    }

  
    }
    
  
    
    public partial class UserAsset: DomainObject
    {
    
      protected int _userAssetId;
    
      protected bool _deleted;
    

      // parent tables
      protected Asset _relatedAsset
        = new Asset()
      ;
    

      // parent tables
      protected User _relatedUser
        = new User()
      ;
    

    public UserAsset(){}

    public UserAsset(
    int 
            userAssetId,int 
            userId,int 
            assetId,bool 
            deleted
    )
    {
    
      this.UserAssetId = userAssetId;
    
      this.UserId = userId;
    
      this.AssetId = assetId;
    
      this.Deleted = deleted;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.UserAsset."
    
      + UserAssetId.ToString()
    ;
    
    return uri;
    }

    

      public int UserAssetId
      {
        
            get { return _userAssetId;}
            set 
            { 
                _userAssetId = value;
            }
          
      }
    

      public int UserId
      {
        
            get
            {
            
                  if(_relatedUser != null)
                    return _relatedUser.UserId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedUser == null)
                        _relatedUser = new User();

                      _relatedUser.UserId = value;
                    
            }
          
      }
    

      public int AssetId
      {
        
            get
            {
            
                  if(_relatedAsset != null)
                    return _relatedAsset.AssetId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedAsset == null)
                        _relatedAsset = new Asset();

                      _relatedAsset.AssetId = value;
                    
            }
          
      }
    

      public bool Deleted
      {
        
            get { return _deleted;}
            set 
            { 
                _deleted = value;
            }
          
      }
    

      public Asset RelatedAsset
      {
      get { return _relatedAsset;}
      set { _relatedAsset = value; }
      }
      
    

      public User RelatedUser
      {
      get { return _relatedUser;}
      set { _relatedUser = value; }
      }
      
    


    public override DomainObject extractSingleObject()
    {
      UserAsset userAsset = new UserAsset();
      
      userAsset.UserAssetId = this.UserAssetId;
      userAsset.UserId = this.UserId;
      userAsset.AssetId = this.AssetId;
      userAsset.Deleted = this.Deleted;
      userAsset.ActiveRecordId = this.ActiveRecordId; 
      return userAsset;
    }

    
    }
  

    public abstract class _UserAssetDataMapper:TDataMapper<UserAsset,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _UserAssetDataMapper(){}
      public _UserAssetDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[UserAsset]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[UserAsset] (
    
      [UserId]
      ,
      [AssetId]
      ,
      [Deleted]
      ) Values (
    
      @userId,
      @assetId,
      @deleted);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override UserAsset create( UserAsset userAsset )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@userId", userAsset.UserId);
              
                  sqlCommand.Parameters.AddWithValue("@assetId", userAsset.AssetId);
              
                  sqlCommand.Parameters.AddWithValue("@deleted", userAsset.Deleted);
              userAsset.UserAssetId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(userAsset,DataMapperOperation.create);

      return registerRecord(userAsset);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [UserAssetId] ,
      [UserId] ,
      [AssetId] ,
      [Deleted] 
     From [dbo].[UserAsset]
    
       Where 
      
         [UserAssetId] = @userAssetId
    ";

    public UserAsset findByPrimaryKey(
    int userAssetId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@userAssetId", userAssetId);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("UserAsset not found, search by primary key");
 

    }


    public bool exists(UserAsset userAsset)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
          {
              
                sqlCommand.Parameters.AddWithValue("@UserAssetId", userAsset.UserAssetId);
              

              using(IDataReader dataReader = sqlCommand.ExecuteReader())
              {
              return dataReader.Read();
              }
          }
      }
    }

    private const string CheckInSql = @"
    
      [UserAsset].[UserAssetId] = @CheckInUserAssetId";

    protected override IDbCommand PrepareCheckInCommand(DomainObject domainObject, string sqlQuery)
    {
      UserAsset _UserAsset = (UserAsset)domainObject;

       SqlCommand sqlCommand = Database.CreateCommand(ModifyQueryForCheckIn(sqlQuery,CheckInSql));

      
        sqlCommand.Parameters.AddWithValue("@CheckInUserAssetId", _UserAsset.UserAssetId);
      

      return sqlCommand;
    }

  

    protected override UserAsset doLoad(IDataReader dataReader)
    {
    UserAsset userAsset = new UserAsset();

    userAsset.UserAssetId = (int) dataReader.GetValue(0);
            userAsset.UserId = (int) dataReader.GetValue(1);
            userAsset.AssetId = (int) dataReader.GetValue(2);
            userAsset.Deleted = (bool) dataReader.GetValue(3);
            

    
    
    return registerRecord(userAsset);
    }


    protected override UserAsset doLoad(Hashtable hashtable)
    {
      UserAsset userAsset = new UserAsset();

      
        
        if(hashtable.ContainsKey("UserAssetId"))
        userAsset.UserAssetId = ( int)hashtable["UserAssetId"];
          
        
        if(hashtable.ContainsKey("UserId"))
        userAsset.UserId = ( int)hashtable["UserId"];
          
        
        if(hashtable.ContainsKey("AssetId"))
        userAsset.AssetId = ( int)hashtable["AssetId"];
          
        
        if(hashtable.ContainsKey("Deleted"))
        userAsset.Deleted = ( bool)hashtable["Deleted"];
          

      return userAsset;
    }


    protected override List<UserAsset> fill(SqlCommand sqlCommand, int offset, int limit)
    {
        List<UserAsset> resultList = new List<UserAsset>();
    
         using(SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
         {
            DataTable dataTable = new DataTable();
            
            sqlDataAdapter.Fill(offset,limit,dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
              UserAsset item = new UserAsset();
              
              
                    item.UserAssetId = ( int)dataRow["UserAssetId"] ;
                  
                    item.UserId = ( int)dataRow["UserId"] ;
                  
                    item.AssetId = ( int)dataRow["AssetId"] ;
                  
                    item.Deleted = ( bool)dataRow["Deleted"] ;
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[UserAsset]
    
      Where
      
        [UserAssetId] = @userAssetId";
    [Synchronized]
    public override UserAsset remove(UserAsset userAsset)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@userAssetId", userAsset.UserAssetId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(userAsset,DataMapperOperation.delete);

      return registerRecord(userAsset);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override UserAsset save( UserAsset userAsset )
    {
      if(exists(userAsset))
        return update(userAsset);
        return create(userAsset);
    }

  
      const String SqlUpdate = @"Update [dbo].[UserAsset] Set
      
        [UserId] = @userId,
        [AssetId] = @assetId,
        [Deleted] = @deleted
        Where
        
          [UserAssetId] = @userAssetId";
    
    [Synchronized]
    public override UserAsset update(UserAsset userAsset)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@userAssetId", userAsset.UserAssetId);
            
              sqlCommand.Parameters.AddWithValue("@userId", userAsset.UserId);
            
              sqlCommand.Parameters.AddWithValue("@assetId", userAsset.AssetId);
            
              sqlCommand.Parameters.AddWithValue("@deleted", userAsset.Deleted);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(userAsset,DataMapperOperation.update);
        
    
        return registerRecord(userAsset);

    }

  
    }
    
  
    
    public partial class UserRole: DomainObject
    {
    
      protected int _userRoleId;
    

      // parent tables
      protected Role _relatedRole
        = new Role()
      ;
    

      // parent tables
      protected User _relatedUser
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
    
      this.UserRoleId = userRoleId;
    
      this.UserId = userId;
    
      this.RoleId = roleId;
    
    }

    public override String  getUri()
    {

    String uri = "TractIncRAID.UserRole."
    
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
            
                  if(_relatedUser != null)
                    return _relatedUser.UserId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedUser == null)
                        _relatedUser = new User();

                      _relatedUser.UserId = value;
                    
            }
          
      }
    

      public int RoleId
      {
        
            get
            {
            
                  if(_relatedRole != null)
                    return _relatedRole.RoleId;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_relatedRole == null)
                        _relatedRole = new Role();

                      _relatedRole.RoleId = value;
                    
            }
          
      }
    

      public Role RelatedRole
      {
      get { return _relatedRole;}
      set { _relatedRole = value; }
      }
      
    

      public User RelatedUser
      {
      get { return _relatedUser;}
      set { _relatedUser = value; }
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
  

    public abstract class _UserRoleDataMapper:TDataMapper<UserRole,SqlConnection,TractIncRAIDDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public _UserRoleDataMapper(){}
      public _UserRoleDataMapper(TractIncRAIDDb database):
      base(database){}
        public override String TableName
        {
          get
          {
            return "[dbo].[UserRole]";
          }
        }

        public override Hashtable getRelation(string tableName)
        {
          throw new Exception("Not yet implemented");
        }

        
    
    private const String SqlCreate = @"Insert Into [dbo].[UserRole] (
    
      [UserId]
      ,
      [RoleId]
      ) Values (
    
      @userId,
      @roleId);

    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override UserRole create( UserRole userRole )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                  sqlCommand.Parameters.AddWithValue("@userId", userRole.UserId);
              
                  sqlCommand.Parameters.AddWithValue("@roleId", userRole.RoleId);
              userRole.UserRoleId = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
            
        }
      }
      
    
      
      raiseAffected(userRole,DataMapperOperation.create);

      return registerRecord(userRole);
    }

  

    private const String SqlSelectByPk = @"Select
    
      [UserRoleId] ,
      [UserId] ,
      [RoleId] 
     From [dbo].[UserRole]
    
       Where 
      
         [UserRoleId] = @userRoleId
    ";

    public UserRole findByPrimaryKey(
    int userRoleId
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
        
          sqlCommand.Parameters.AddWithValue("@userRoleId", userRoleId);
        

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

    userRole.UserRoleId = (int) dataReader.GetValue(0);
            userRole.UserId = (int) dataReader.GetValue(1);
            userRole.RoleId = (int) dataReader.GetValue(2);
            

    
    
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
                  
               registerRecord(item);
               
               resultList.Add(item);
            }
         }
         
         return resultList;
    }
    
    
  

    #region Delete
    private const String SqlDelete = @"Delete From [dbo].[UserRole]
    
      Where
      
        [UserRoleId] = @userRoleId";
    [Synchronized]
    public override UserRole remove(UserRole userRole)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            
            sqlCommand.Parameters.AddWithValue("@userRoleId", userRole.UserRoleId);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(userRole,DataMapperOperation.delete);

      return registerRecord(userRole);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public override UserRole save( UserRole userRole )
    {
      if(exists(userRole))
        return update(userRole);
        return create(userRole);
    }

  
      const String SqlUpdate = @"Update [dbo].[UserRole] Set
      
        [UserId] = @userId,
        [RoleId] = @roleId
        Where
        
          [UserRoleId] = @userRoleId";
    
    [Synchronized]
    public override UserRole update(UserRole userRole)
    {
      


        using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
        {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
              sqlCommand.Parameters.AddWithValue("@userRoleId", userRole.UserRoleId);
            
              sqlCommand.Parameters.AddWithValue("@userId", userRole.UserId);
            
              sqlCommand.Parameters.AddWithValue("@roleId", userRole.RoleId);
            


        sqlCommand.ExecuteNonQuery();
        }
        }

        

        raiseAffected(userRole,DataMapperOperation.update);
        
    
        return registerRecord(userRole);

    }

  
    }
    
  
      }
    