
      namespace TractInc.DocCapture.Domain
      {
      using System;
      
    using System.Data;
    using System.Collections;
    using System.Collections.Generic;
    using Weborb.Data.Management;
  
    
      using System.Data.SqlClient;
    
    public class Doc_captureDb:TDatabase<SqlConnection,SqlTransaction,SqlCommand>
    {
        public Doc_captureDb()
        {
          ConnectionString = "server=(local);user id=root;password=++Winston;database=doc-capture;connect timeout=30;";
        }
        
      

        public Addresstype create(Addresstype addresstype)
        {
          AddresstypeDataMapper dataMapper = new AddresstypeDataMapper(this);
          
          return dataMapper.create(addresstype);
        }

        public Addresstype update(Addresstype addresstype)
        {
          AddresstypeDataMapper dataMapper = new AddresstypeDataMapper(this);

          return dataMapper.update(addresstype);
        }

        public Addresstype remove(Addresstype addresstype)
        {
          AddresstypeDataMapper dataMapper = new AddresstypeDataMapper(this);

          return dataMapper.remove(addresstype);
        }
      

        public Countys create(Countys countys)
        {
          CountysDataMapper dataMapper = new CountysDataMapper(this);
          
          return dataMapper.create(countys);
        }

        public Countys update(Countys countys)
        {
          CountysDataMapper dataMapper = new CountysDataMapper(this);

          return dataMapper.update(countys);
        }

        public Countys remove(Countys countys)
        {
          CountysDataMapper dataMapper = new CountysDataMapper(this);

          return dataMapper.remove(countys);
        }
      

        public Document create(Document document)
        {
          DocumentDataMapper dataMapper = new DocumentDataMapper(this);
          
          return dataMapper.create(document);
        }

        public Document update(Document document)
        {
          DocumentDataMapper dataMapper = new DocumentDataMapper(this);

          return dataMapper.update(document);
        }

        public Document remove(Document document)
        {
          DocumentDataMapper dataMapper = new DocumentDataMapper(this);

          return dataMapper.remove(document);
        }
      

        public Documenttype create(Documenttype documenttype)
        {
          DocumenttypeDataMapper dataMapper = new DocumenttypeDataMapper(this);
          
          return dataMapper.create(documenttype);
        }

        public Documenttype update(Documenttype documenttype)
        {
          DocumenttypeDataMapper dataMapper = new DocumenttypeDataMapper(this);

          return dataMapper.update(documenttype);
        }

        public Documenttype remove(Documenttype documenttype)
        {
          DocumenttypeDataMapper dataMapper = new DocumenttypeDataMapper(this);

          return dataMapper.remove(documenttype);
        }
      

        public Participant create(Participant participant)
        {
          ParticipantDataMapper dataMapper = new ParticipantDataMapper(this);
          
          return dataMapper.create(participant);
        }

        public Participant update(Participant participant)
        {
          ParticipantDataMapper dataMapper = new ParticipantDataMapper(this);

          return dataMapper.update(participant);
        }

        public Participant remove(Participant participant)
        {
          ParticipantDataMapper dataMapper = new ParticipantDataMapper(this);

          return dataMapper.remove(participant);
        }
      

        public Participantaddress create(Participantaddress participantaddress)
        {
          ParticipantaddressDataMapper dataMapper = new ParticipantaddressDataMapper(this);
          
          return dataMapper.create(participantaddress);
        }

        public Participantaddress update(Participantaddress participantaddress)
        {
          ParticipantaddressDataMapper dataMapper = new ParticipantaddressDataMapper(this);

          return dataMapper.update(participantaddress);
        }

        public Participantaddress remove(Participantaddress participantaddress)
        {
          ParticipantaddressDataMapper dataMapper = new ParticipantaddressDataMapper(this);

          return dataMapper.remove(participantaddress);
        }
      

        public Participantentityparty create(Participantentityparty participantentityparty)
        {
          ParticipantentitypartyDataMapper dataMapper = new ParticipantentitypartyDataMapper(this);
          
          return dataMapper.create(participantentityparty);
        }

        public Participantentityparty update(Participantentityparty participantentityparty)
        {
          ParticipantentitypartyDataMapper dataMapper = new ParticipantentitypartyDataMapper(this);

          return dataMapper.update(participantentityparty);
        }

        public Participantentityparty remove(Participantentityparty participantentityparty)
        {
          ParticipantentitypartyDataMapper dataMapper = new ParticipantentitypartyDataMapper(this);

          return dataMapper.remove(participantentityparty);
        }
      

        public Participantreservation create(Participantreservation participantreservation)
        {
          ParticipantreservationDataMapper dataMapper = new ParticipantreservationDataMapper(this);
          
          return dataMapper.create(participantreservation);
        }

        public Participantreservation update(Participantreservation participantreservation)
        {
          ParticipantreservationDataMapper dataMapper = new ParticipantreservationDataMapper(this);

          return dataMapper.update(participantreservation);
        }

        public Participantreservation remove(Participantreservation participantreservation)
        {
          ParticipantreservationDataMapper dataMapper = new ParticipantreservationDataMapper(this);

          return dataMapper.remove(participantreservation);
        }
      

        public Participantrole create(Participantrole participantrole)
        {
          ParticipantroleDataMapper dataMapper = new ParticipantroleDataMapper(this);
          
          return dataMapper.create(participantrole);
        }

        public Participantrole update(Participantrole participantrole)
        {
          ParticipantroleDataMapper dataMapper = new ParticipantroleDataMapper(this);

          return dataMapper.update(participantrole);
        }

        public Participantrole remove(Participantrole participantrole)
        {
          ParticipantroleDataMapper dataMapper = new ParticipantroleDataMapper(this);

          return dataMapper.remove(participantrole);
        }
      

        public Participanttype create(Participanttype participanttype)
        {
          ParticipanttypeDataMapper dataMapper = new ParticipanttypeDataMapper(this);
          
          return dataMapper.create(participanttype);
        }

        public Participanttype update(Participanttype participanttype)
        {
          ParticipanttypeDataMapper dataMapper = new ParticipanttypeDataMapper(this);

          return dataMapper.update(participanttype);
        }

        public Participanttype remove(Participanttype participanttype)
        {
          ParticipanttypeDataMapper dataMapper = new ParticipanttypeDataMapper(this);

          return dataMapper.remove(participanttype);
        }
      

        public States create(States states)
        {
          StatesDataMapper dataMapper = new StatesDataMapper(this);
          
          return dataMapper.create(states);
        }

        public States update(States states)
        {
          StatesDataMapper dataMapper = new StatesDataMapper(this);

          return dataMapper.update(states);
        }

        public States remove(States states)
        {
          StatesDataMapper dataMapper = new StatesDataMapper(this);

          return dataMapper.remove(states);
        }
      

        public Tract create(Tract tract)
        {
          TractDataMapper dataMapper = new TractDataMapper(this);
          
          return dataMapper.create(tract);
        }

        public Tract update(Tract tract)
        {
          TractDataMapper dataMapper = new TractDataMapper(this);

          return dataMapper.update(tract);
        }

        public Tract remove(Tract tract)
        {
          TractDataMapper dataMapper = new TractDataMapper(this);

          return dataMapper.remove(tract);
        }
      

        public Tractexception create(Tractexception tractexception)
        {
          TractexceptionDataMapper dataMapper = new TractexceptionDataMapper(this);
          
          return dataMapper.create(tractexception);
        }

        public Tractexception update(Tractexception tractexception)
        {
          TractexceptionDataMapper dataMapper = new TractexceptionDataMapper(this);

          return dataMapper.update(tractexception);
        }

        public Tractexception remove(Tractexception tractexception)
        {
          TractexceptionDataMapper dataMapper = new TractexceptionDataMapper(this);

          return dataMapper.remove(tractexception);
        }
      
    }
  
    
    public partial class Addresstype: DomainObject
    {
    
      protected int _addressTypeID;
    
      protected String _types;
    

    public Addresstype(){}

    public Addresstype(
    int 
            addressTypeID,String 
            types
    )
    {
    AddressTypeID = addressTypeID;
    Types = types;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Addresstype."
    
      + AddressTypeID.ToString()
    ;
    
    return uri;
    }

    

      public int AddressTypeID
      {
        
            get { return _addressTypeID;}
            set 
            { 
                _addressTypeID = value;
            }
          
      }
    

      public String Types
      {
        
            get { return _types;}
            set 
            { 
                _types = value;
            }
          
      }
    


    }
  

    public partial class AddresstypeDataMapper:TDataMapper<Addresstype,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public AddresstypeDataMapper(){}
      public AddresstypeDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Addresstype";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Addresstype] (
    Types) Values (
    
      @Types);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Addresstype create( Addresstype addresstype )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(addresstype.Types != null)
                      sqlCommand.Parameters.AddWithValue("@Types", addresstype.Types);
                    else
                      sqlCommand.Parameters.AddWithValue("@Types", DBNull.Value);
                  addresstype.AddressTypeID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(addresstype,DataMapperOperation.create);

      return registerRecord(addresstype);
    }

  

    private const String SqlSelectAll = @"Select
    AddressTypeID,Types 
    From [Addresstype] ";
    
    public List<Addresstype> findAll(Object args)
    {
      List<Addresstype> rv = new List<Addresstype>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    AddressTypeID,Types
     From [Addresstype]
    
       Where 
      AddressTypeID = @AddressTypeID
    ";

    public Addresstype findByPrimaryKey(
    int addressTypeID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@AddressTypeID", addressTypeID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Addresstype not found, search by primary key");
 

    }


    public bool exists(Addresstype addresstype)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@AddressTypeID", addresstype.AddressTypeID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Addresstype doLoad(IDataReader dataReader)
    {
    Addresstype addresstype = new Addresstype();

    addresstype.AddressTypeID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          addresstype.Types = dataReader.GetString(1);
        addresstype.IsLoaded = true;
    
    return registerRecord(addresstype);
    }


    protected override Addresstype doLoad(Hashtable hashtable)
    {
      Addresstype addresstype = new Addresstype();

      
        if(hashtable.ContainsKey("AddressTypeID"))
            addresstype.AddressTypeID = ( int)hashtable["AddressTypeID"];
      
        if(hashtable.ContainsKey("Types"))
            addresstype.Types = ( String)hashtable["Types"];
      

      return addresstype;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Addresstype]
    
      Where
      AddressTypeID = @AddressTypeID";
    [Synchronized]
    public Addresstype remove(Addresstype addresstype)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@AddressTypeID", addresstype.AddressTypeID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(addresstype,DataMapperOperation.delete);

      return registerRecord(addresstype);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Addresstype save( Addresstype addresstype )
    {
      if(exists(addresstype))
        return update(addresstype);
        return create(addresstype);
    }

  

    const String SqlUpdate = @"Update [Addresstype] Set 
    Types = @Types
       Where 
      AddressTypeID = @AddressTypeID";
    
    
    [Synchronized]
    public Addresstype update(Addresstype addresstype)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@AddressTypeID", addresstype.AddressTypeID);
                  
                    if(addresstype.Types != null)
                      sqlCommand.Parameters.AddWithValue("@Types", addresstype.Types);
                    else
                      sqlCommand.Parameters.AddWithValue("@Types", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(addresstype,DataMapperOperation.update);

      return registerRecord(addresstype);
    }

  
    }
    
  
    
    public partial class Countys: DomainObject
    {
    
      protected int _oBJECTID;
    
      protected String _nAME;
    
      protected String _sTATE_NAME;
    
      protected String _sTATE_FIPS;
    
      protected String _cNTY_FIPS;
    
      protected String _fIPS;
    
      protected decimal? _pOP2000;
    
      protected decimal? _pOP2004;
    
      protected decimal? _pOP00_SQMI;
    
      protected decimal? _pOP04_SQMI;
    
      protected decimal? _wHITE;
    
      protected decimal? _bLACK;
    
      protected decimal? _aMERI_ES;
    
      protected decimal? _aSIAN;
    
      protected decimal? _hAWN_PI;
    
      protected decimal? _oTHER;
    
      protected decimal? _mULT_RACE;
    
      protected decimal? _hISPANIC;
    
      protected decimal? _mALES;
    
      protected decimal? _fEMALES;
    
      protected decimal? _aGE_UNDER5;
    
      protected decimal? _aGE_5_17;
    
      protected decimal? _aGE_18_21;
    
      protected decimal? _aGE_22_29;
    
      protected decimal? _aGE_30_39;
    
      protected decimal? _aGE_40_49;
    
      protected decimal? _aGE_50_64;
    
      protected decimal? _aGE_65_UP;
    
      protected decimal? _mED_AGE;
    
      protected decimal? _mED_AGE_M;
    
      protected decimal? _mED_AGE_F;
    
      protected decimal? _hOUSEHOLDS;
    
      protected decimal? _aVE_HH_SZ;
    
      protected decimal? _hSEHLD_1_M;
    
      protected decimal? _hSEHLD_1_F;
    
      protected decimal? _mARHH_CHD;
    
      protected decimal? _mARHH_NO_C;
    
      protected decimal? _mHH_CHILD;
    
      protected decimal? _fHH_CHILD;
    
      protected decimal? _fAMILIES;
    
      protected decimal? _aVE_FAM_SZ;
    
      protected decimal? _hSE_UNITS;
    
      protected decimal? _vACANT;
    
      protected decimal? _oWNER_OCC;
    
      protected decimal? _rENTER_OCC;
    
      protected decimal? _nO_FARMS97;
    
      protected decimal? _aVG_SIZE97;
    
      protected decimal? _cROP_ACR97;
    
      protected decimal? _aVG_SALE97;
    
      protected decimal? _sQMI;
    
      protected decimal? _shape_Leng;
    
      protected decimal? _shape_Area;
    
      protected String _icoMapAttr;
    
      // parent tables
      protected States _parentStates
        = new States()
      ;
    

    public Countys(){}

    public Countys(
    int 
            oBJECTID,int 
            sTATE_ID,String 
            nAME,String 
            sTATE_NAME,String 
            sTATE_FIPS,String 
            cNTY_FIPS,String 
            fIPS,decimal 
            pOP2000,decimal 
            pOP2004,decimal 
            pOP00_SQMI,decimal 
            pOP04_SQMI,decimal 
            wHITE,decimal 
            bLACK,decimal 
            aMERI_ES,decimal 
            aSIAN,decimal 
            hAWN_PI,decimal 
            oTHER,decimal 
            mULT_RACE,decimal 
            hISPANIC,decimal 
            mALES,decimal 
            fEMALES,decimal 
            aGE_UNDER5,decimal 
            aGE_5_17,decimal 
            aGE_18_21,decimal 
            aGE_22_29,decimal 
            aGE_30_39,decimal 
            aGE_40_49,decimal 
            aGE_50_64,decimal 
            aGE_65_UP,decimal 
            mED_AGE,decimal 
            mED_AGE_M,decimal 
            mED_AGE_F,decimal 
            hOUSEHOLDS,decimal 
            aVE_HH_SZ,decimal 
            hSEHLD_1_M,decimal 
            hSEHLD_1_F,decimal 
            mARHH_CHD,decimal 
            mARHH_NO_C,decimal 
            mHH_CHILD,decimal 
            fHH_CHILD,decimal 
            fAMILIES,decimal 
            aVE_FAM_SZ,decimal 
            hSE_UNITS,decimal 
            vACANT,decimal 
            oWNER_OCC,decimal 
            rENTER_OCC,decimal 
            nO_FARMS97,decimal 
            aVG_SIZE97,decimal 
            cROP_ACR97,decimal 
            aVG_SALE97,decimal 
            sQMI,decimal 
            shape_Leng,decimal 
            shape_Area,String 
            icoMapAttr
    )
    {
    OBJECTID = oBJECTID;
    STATE_ID = sTATE_ID;
    NAME = nAME;
    STATE_NAME = sTATE_NAME;
    STATE_FIPS = sTATE_FIPS;
    CNTY_FIPS = cNTY_FIPS;
    FIPS = fIPS;
    POP2000 = pOP2000;
    POP2004 = pOP2004;
    POP00_SQMI = pOP00_SQMI;
    POP04_SQMI = pOP04_SQMI;
    WHITE = wHITE;
    BLACK = bLACK;
    AMERI_ES = aMERI_ES;
    ASIAN = aSIAN;
    HAWN_PI = hAWN_PI;
    OTHER = oTHER;
    MULT_RACE = mULT_RACE;
    HISPANIC = hISPANIC;
    MALES = mALES;
    FEMALES = fEMALES;
    AGE_UNDER5 = aGE_UNDER5;
    AGE_5_17 = aGE_5_17;
    AGE_18_21 = aGE_18_21;
    AGE_22_29 = aGE_22_29;
    AGE_30_39 = aGE_30_39;
    AGE_40_49 = aGE_40_49;
    AGE_50_64 = aGE_50_64;
    AGE_65_UP = aGE_65_UP;
    MED_AGE = mED_AGE;
    MED_AGE_M = mED_AGE_M;
    MED_AGE_F = mED_AGE_F;
    HOUSEHOLDS = hOUSEHOLDS;
    AVE_HH_SZ = aVE_HH_SZ;
    HSEHLD_1_M = hSEHLD_1_M;
    HSEHLD_1_F = hSEHLD_1_F;
    MARHH_CHD = mARHH_CHD;
    MARHH_NO_C = mARHH_NO_C;
    MHH_CHILD = mHH_CHILD;
    FHH_CHILD = fHH_CHILD;
    FAMILIES = fAMILIES;
    AVE_FAM_SZ = aVE_FAM_SZ;
    HSE_UNITS = hSE_UNITS;
    VACANT = vACANT;
    OWNER_OCC = oWNER_OCC;
    RENTER_OCC = rENTER_OCC;
    NO_FARMS97 = nO_FARMS97;
    AVG_SIZE97 = aVG_SIZE97;
    CROP_ACR97 = cROP_ACR97;
    AVG_SALE97 = aVG_SALE97;
    SQMI = sQMI;
    Shape_Leng = shape_Leng;
    Shape_Area = shape_Area;
    IcoMapAttr = icoMapAttr;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Countys."
    
      + OBJECTID.ToString()
    ;
    
    return uri;
    }

    

      public int OBJECTID
      {
        
            get { return _oBJECTID;}
            set 
            { 
                _oBJECTID = value;
            }
          
      }
    

      public int STATE_ID
      {
        
            get
            {
            
                  if(_parentStates != null)
                    return _parentStates.STATE_ID;

                throw new NullReferenceException("Parent instance not initialized ");
            }
            set
            {
            
                      if(_parentStates == null)
                      _parentStates = new States();

                      _parentStates.STATE_ID = value;
                    
            }
          
      }
    

      public String NAME
      {
        
            get { return _nAME;}
            set 
            { 
                _nAME = value;
            }
          
      }
    

      public String STATE_NAME
      {
        
            get { return _sTATE_NAME;}
            set 
            { 
                _sTATE_NAME = value;
            }
          
      }
    

      public String STATE_FIPS
      {
        
            get { return _sTATE_FIPS;}
            set 
            { 
                _sTATE_FIPS = value;
            }
          
      }
    

      public String CNTY_FIPS
      {
        
            get { return _cNTY_FIPS;}
            set 
            { 
                _cNTY_FIPS = value;
            }
          
      }
    

      public String FIPS
      {
        
            get { return _fIPS;}
            set 
            { 
                _fIPS = value;
            }
          
      }
    

      public decimal? POP2000
      {
        
            get { return _pOP2000;}
            set 
            { 
                _pOP2000 = value;
            }
          
      }
    

      public decimal? POP2004
      {
        
            get { return _pOP2004;}
            set 
            { 
                _pOP2004 = value;
            }
          
      }
    

      public decimal? POP00_SQMI
      {
        
            get { return _pOP00_SQMI;}
            set 
            { 
                _pOP00_SQMI = value;
            }
          
      }
    

      public decimal? POP04_SQMI
      {
        
            get { return _pOP04_SQMI;}
            set 
            { 
                _pOP04_SQMI = value;
            }
          
      }
    

      public decimal? WHITE
      {
        
            get { return _wHITE;}
            set 
            { 
                _wHITE = value;
            }
          
      }
    

      public decimal? BLACK
      {
        
            get { return _bLACK;}
            set 
            { 
                _bLACK = value;
            }
          
      }
    

      public decimal? AMERI_ES
      {
        
            get { return _aMERI_ES;}
            set 
            { 
                _aMERI_ES = value;
            }
          
      }
    

      public decimal? ASIAN
      {
        
            get { return _aSIAN;}
            set 
            { 
                _aSIAN = value;
            }
          
      }
    

      public decimal? HAWN_PI
      {
        
            get { return _hAWN_PI;}
            set 
            { 
                _hAWN_PI = value;
            }
          
      }
    

      public decimal? OTHER
      {
        
            get { return _oTHER;}
            set 
            { 
                _oTHER = value;
            }
          
      }
    

      public decimal? MULT_RACE
      {
        
            get { return _mULT_RACE;}
            set 
            { 
                _mULT_RACE = value;
            }
          
      }
    

      public decimal? HISPANIC
      {
        
            get { return _hISPANIC;}
            set 
            { 
                _hISPANIC = value;
            }
          
      }
    

      public decimal? MALES
      {
        
            get { return _mALES;}
            set 
            { 
                _mALES = value;
            }
          
      }
    

      public decimal? FEMALES
      {
        
            get { return _fEMALES;}
            set 
            { 
                _fEMALES = value;
            }
          
      }
    

      public decimal? AGE_UNDER5
      {
        
            get { return _aGE_UNDER5;}
            set 
            { 
                _aGE_UNDER5 = value;
            }
          
      }
    

      public decimal? AGE_5_17
      {
        
            get { return _aGE_5_17;}
            set 
            { 
                _aGE_5_17 = value;
            }
          
      }
    

      public decimal? AGE_18_21
      {
        
            get { return _aGE_18_21;}
            set 
            { 
                _aGE_18_21 = value;
            }
          
      }
    

      public decimal? AGE_22_29
      {
        
            get { return _aGE_22_29;}
            set 
            { 
                _aGE_22_29 = value;
            }
          
      }
    

      public decimal? AGE_30_39
      {
        
            get { return _aGE_30_39;}
            set 
            { 
                _aGE_30_39 = value;
            }
          
      }
    

      public decimal? AGE_40_49
      {
        
            get { return _aGE_40_49;}
            set 
            { 
                _aGE_40_49 = value;
            }
          
      }
    

      public decimal? AGE_50_64
      {
        
            get { return _aGE_50_64;}
            set 
            { 
                _aGE_50_64 = value;
            }
          
      }
    

      public decimal? AGE_65_UP
      {
        
            get { return _aGE_65_UP;}
            set 
            { 
                _aGE_65_UP = value;
            }
          
      }
    

      public decimal? MED_AGE
      {
        
            get { return _mED_AGE;}
            set 
            { 
                _mED_AGE = value;
            }
          
      }
    

      public decimal? MED_AGE_M
      {
        
            get { return _mED_AGE_M;}
            set 
            { 
                _mED_AGE_M = value;
            }
          
      }
    

      public decimal? MED_AGE_F
      {
        
            get { return _mED_AGE_F;}
            set 
            { 
                _mED_AGE_F = value;
            }
          
      }
    

      public decimal? HOUSEHOLDS
      {
        
            get { return _hOUSEHOLDS;}
            set 
            { 
                _hOUSEHOLDS = value;
            }
          
      }
    

      public decimal? AVE_HH_SZ
      {
        
            get { return _aVE_HH_SZ;}
            set 
            { 
                _aVE_HH_SZ = value;
            }
          
      }
    

      public decimal? HSEHLD_1_M
      {
        
            get { return _hSEHLD_1_M;}
            set 
            { 
                _hSEHLD_1_M = value;
            }
          
      }
    

      public decimal? HSEHLD_1_F
      {
        
            get { return _hSEHLD_1_F;}
            set 
            { 
                _hSEHLD_1_F = value;
            }
          
      }
    

      public decimal? MARHH_CHD
      {
        
            get { return _mARHH_CHD;}
            set 
            { 
                _mARHH_CHD = value;
            }
          
      }
    

      public decimal? MARHH_NO_C
      {
        
            get { return _mARHH_NO_C;}
            set 
            { 
                _mARHH_NO_C = value;
            }
          
      }
    

      public decimal? MHH_CHILD
      {
        
            get { return _mHH_CHILD;}
            set 
            { 
                _mHH_CHILD = value;
            }
          
      }
    

      public decimal? FHH_CHILD
      {
        
            get { return _fHH_CHILD;}
            set 
            { 
                _fHH_CHILD = value;
            }
          
      }
    

      public decimal? FAMILIES
      {
        
            get { return _fAMILIES;}
            set 
            { 
                _fAMILIES = value;
            }
          
      }
    

      public decimal? AVE_FAM_SZ
      {
        
            get { return _aVE_FAM_SZ;}
            set 
            { 
                _aVE_FAM_SZ = value;
            }
          
      }
    

      public decimal? HSE_UNITS
      {
        
            get { return _hSE_UNITS;}
            set 
            { 
                _hSE_UNITS = value;
            }
          
      }
    

      public decimal? VACANT
      {
        
            get { return _vACANT;}
            set 
            { 
                _vACANT = value;
            }
          
      }
    

      public decimal? OWNER_OCC
      {
        
            get { return _oWNER_OCC;}
            set 
            { 
                _oWNER_OCC = value;
            }
          
      }
    

      public decimal? RENTER_OCC
      {
        
            get { return _rENTER_OCC;}
            set 
            { 
                _rENTER_OCC = value;
            }
          
      }
    

      public decimal? NO_FARMS97
      {
        
            get { return _nO_FARMS97;}
            set 
            { 
                _nO_FARMS97 = value;
            }
          
      }
    

      public decimal? AVG_SIZE97
      {
        
            get { return _aVG_SIZE97;}
            set 
            { 
                _aVG_SIZE97 = value;
            }
          
      }
    

      public decimal? CROP_ACR97
      {
        
            get { return _cROP_ACR97;}
            set 
            { 
                _cROP_ACR97 = value;
            }
          
      }
    

      public decimal? AVG_SALE97
      {
        
            get { return _aVG_SALE97;}
            set 
            { 
                _aVG_SALE97 = value;
            }
          
      }
    

      public decimal? SQMI
      {
        
            get { return _sQMI;}
            set 
            { 
                _sQMI = value;
            }
          
      }
    

      public decimal? Shape_Leng
      {
        
            get { return _shape_Leng;}
            set 
            { 
                _shape_Leng = value;
            }
          
      }
    

      public decimal? Shape_Area
      {
        
            get { return _shape_Area;}
            set 
            { 
                _shape_Area = value;
            }
          
      }
    

      public String IcoMapAttr
      {
        
            get { return _icoMapAttr;}
            set 
            { 
                _icoMapAttr = value;
            }
          
      }
    
      public States ParentStates
      {
      get { return _parentStates;}
      set { _parentStates = value; }
      }
    


    }
  

    public partial class CountysDataMapper:TDataMapper<Countys,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public CountysDataMapper(){}
      public CountysDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Countys";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Countys] (
    OBJECTID,STATE_ID,NAME,STATE_NAME,STATE_FIPS,CNTY_FIPS,FIPS,POP2000,POP2004,POP00_SQMI,POP04_SQMI,WHITE,BLACK,AMERI_ES,ASIAN,HAWN_PI,OTHER,MULT_RACE,HISPANIC,MALES,FEMALES,AGE_UNDER5,AGE_5_17,AGE_18_21,AGE_22_29,AGE_30_39,AGE_40_49,AGE_50_64,AGE_65_UP,MED_AGE,MED_AGE_M,MED_AGE_F,HOUSEHOLDS,AVE_HH_SZ,HSEHLD_1_M,HSEHLD_1_F,MARHH_CHD,MARHH_NO_C,MHH_CHILD,FHH_CHILD,FAMILIES,AVE_FAM_SZ,HSE_UNITS,VACANT,OWNER_OCC,RENTER_OCC,NO_FARMS97,AVG_SIZE97,CROP_ACR97,AVG_SALE97,SQMI,Shape_Leng,Shape_Area,IcoMapAttr) Values (
    
      @OBJECTID,
      @STATE_ID,
      @NAME,
      @STATE_NAME,
      @STATE_FIPS,
      @CNTY_FIPS,
      @FIPS,
      @POP2000,
      @POP2004,
      @POP00_SQMI,
      @POP04_SQMI,
      @WHITE,
      @BLACK,
      @AMERI_ES,
      @ASIAN,
      @HAWN_PI,
      @OTHER,
      @MULT_RACE,
      @HISPANIC,
      @MALES,
      @FEMALES,
      @AGE_UNDER5,
      @AGE_5_17,
      @AGE_18_21,
      @AGE_22_29,
      @AGE_30_39,
      @AGE_40_49,
      @AGE_50_64,
      @AGE_65_UP,
      @MED_AGE,
      @MED_AGE_M,
      @MED_AGE_F,
      @HOUSEHOLDS,
      @AVE_HH_SZ,
      @HSEHLD_1_M,
      @HSEHLD_1_F,
      @MARHH_CHD,
      @MARHH_NO_C,
      @MHH_CHILD,
      @FHH_CHILD,
      @FAMILIES,
      @AVE_FAM_SZ,
      @HSE_UNITS,
      @VACANT,
      @OWNER_OCC,
      @RENTER_OCC,
      @NO_FARMS97,
      @AVG_SIZE97,
      @CROP_ACR97,
      @AVG_SALE97,
      @SQMI,
      @Shape_Leng,
      @Shape_Area,
      @IcoMapAttr);
    ";
    
    
    [Synchronized]
    public override Countys create( Countys countys )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                      sqlCommand.Parameters.AddWithValue("@OBJECTID", countys.OBJECTID);
                  
                      sqlCommand.Parameters.AddWithValue("@STATE_ID", countys.STATE_ID);
                  
                    if(countys.NAME != null)
                      sqlCommand.Parameters.AddWithValue("@NAME", countys.NAME);
                    else
                      sqlCommand.Parameters.AddWithValue("@NAME", DBNull.Value);
                  
                    if(countys.STATE_NAME != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", countys.STATE_NAME);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", DBNull.Value);
                  
                    if(countys.STATE_FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", countys.STATE_FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", DBNull.Value);
                  
                    if(countys.CNTY_FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@CNTY_FIPS", countys.CNTY_FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@CNTY_FIPS", DBNull.Value);
                  
                    if(countys.FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@FIPS", countys.FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@FIPS", DBNull.Value);
                  
                    if(countys.POP2000 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2000", countys.POP2000);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2000", DBNull.Value);
                  
                    if(countys.POP2004 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2004", countys.POP2004);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2004", DBNull.Value);
                  
                    if(countys.POP00_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", countys.POP00_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", DBNull.Value);
                  
                    if(countys.POP04_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", countys.POP04_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", DBNull.Value);
                  
                    if(countys.WHITE != null)
                      sqlCommand.Parameters.AddWithValue("@WHITE", countys.WHITE);
                    else
                      sqlCommand.Parameters.AddWithValue("@WHITE", DBNull.Value);
                  
                    if(countys.BLACK != null)
                      sqlCommand.Parameters.AddWithValue("@BLACK", countys.BLACK);
                    else
                      sqlCommand.Parameters.AddWithValue("@BLACK", DBNull.Value);
                  
                    if(countys.AMERI_ES != null)
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", countys.AMERI_ES);
                    else
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", DBNull.Value);
                  
                    if(countys.ASIAN != null)
                      sqlCommand.Parameters.AddWithValue("@ASIAN", countys.ASIAN);
                    else
                      sqlCommand.Parameters.AddWithValue("@ASIAN", DBNull.Value);
                  
                    if(countys.HAWN_PI != null)
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", countys.HAWN_PI);
                    else
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", DBNull.Value);
                  
                    if(countys.OTHER != null)
                      sqlCommand.Parameters.AddWithValue("@OTHER", countys.OTHER);
                    else
                      sqlCommand.Parameters.AddWithValue("@OTHER", DBNull.Value);
                  
                    if(countys.MULT_RACE != null)
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", countys.MULT_RACE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", DBNull.Value);
                  
                    if(countys.HISPANIC != null)
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", countys.HISPANIC);
                    else
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", DBNull.Value);
                  
                    if(countys.MALES != null)
                      sqlCommand.Parameters.AddWithValue("@MALES", countys.MALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@MALES", DBNull.Value);
                  
                    if(countys.FEMALES != null)
                      sqlCommand.Parameters.AddWithValue("@FEMALES", countys.FEMALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FEMALES", DBNull.Value);
                  
                    if(countys.AGE_UNDER5 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", countys.AGE_UNDER5);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", DBNull.Value);
                  
                    if(countys.AGE_5_17 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", countys.AGE_5_17);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", DBNull.Value);
                  
                    if(countys.AGE_18_21 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", countys.AGE_18_21);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", DBNull.Value);
                  
                    if(countys.AGE_22_29 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", countys.AGE_22_29);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", DBNull.Value);
                  
                    if(countys.AGE_30_39 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", countys.AGE_30_39);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", DBNull.Value);
                  
                    if(countys.AGE_40_49 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", countys.AGE_40_49);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", DBNull.Value);
                  
                    if(countys.AGE_50_64 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", countys.AGE_50_64);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", DBNull.Value);
                  
                    if(countys.AGE_65_UP != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", countys.AGE_65_UP);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", DBNull.Value);
                  
                    if(countys.MED_AGE != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", countys.MED_AGE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", DBNull.Value);
                  
                    if(countys.MED_AGE_M != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", countys.MED_AGE_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", DBNull.Value);
                  
                    if(countys.MED_AGE_F != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", countys.MED_AGE_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", DBNull.Value);
                  
                    if(countys.HOUSEHOLDS != null)
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", countys.HOUSEHOLDS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", DBNull.Value);
                  
                    if(countys.AVE_HH_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", countys.AVE_HH_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", DBNull.Value);
                  
                    if(countys.HSEHLD_1_M != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", countys.HSEHLD_1_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", DBNull.Value);
                  
                    if(countys.HSEHLD_1_F != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", countys.HSEHLD_1_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", DBNull.Value);
                  
                    if(countys.MARHH_CHD != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", countys.MARHH_CHD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", DBNull.Value);
                  
                    if(countys.MARHH_NO_C != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", countys.MARHH_NO_C);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", DBNull.Value);
                  
                    if(countys.MHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", countys.MHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", DBNull.Value);
                  
                    if(countys.FHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", countys.FHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", DBNull.Value);
                  
                    if(countys.FAMILIES != null)
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", countys.FAMILIES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", DBNull.Value);
                  
                    if(countys.AVE_FAM_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", countys.AVE_FAM_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", DBNull.Value);
                  
                    if(countys.HSE_UNITS != null)
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", countys.HSE_UNITS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", DBNull.Value);
                  
                    if(countys.VACANT != null)
                      sqlCommand.Parameters.AddWithValue("@VACANT", countys.VACANT);
                    else
                      sqlCommand.Parameters.AddWithValue("@VACANT", DBNull.Value);
                  
                    if(countys.OWNER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", countys.OWNER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", DBNull.Value);
                  
                    if(countys.RENTER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", countys.RENTER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", DBNull.Value);
                  
                    if(countys.NO_FARMS97 != null)
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", countys.NO_FARMS97);
                    else
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", DBNull.Value);
                  
                    if(countys.AVG_SIZE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", countys.AVG_SIZE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", DBNull.Value);
                  
                    if(countys.CROP_ACR97 != null)
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", countys.CROP_ACR97);
                    else
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", DBNull.Value);
                  
                    if(countys.AVG_SALE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", countys.AVG_SALE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", DBNull.Value);
                  
                    if(countys.SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@SQMI", countys.SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@SQMI", DBNull.Value);
                  
                    if(countys.Shape_Leng != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", countys.Shape_Leng);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", DBNull.Value);
                  
                    if(countys.Shape_Area != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", countys.Shape_Area);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", DBNull.Value);
                  
                    if(countys.IcoMapAttr != null)
                      sqlCommand.Parameters.AddWithValue("@IcoMapAttr", countys.IcoMapAttr);
                    else
                      sqlCommand.Parameters.AddWithValue("@IcoMapAttr", DBNull.Value);
                  
                  sqlCommand.ExecuteNonQuery();
                
        }
      }
      
    
      
      raiseAffected(countys,DataMapperOperation.create);

      return registerRecord(countys);
    }

  

    private const String SqlSelectAll = @"Select
    OBJECTID,STATE_ID,NAME,STATE_NAME,STATE_FIPS,CNTY_FIPS,FIPS,POP2000,POP2004,POP00_SQMI,POP04_SQMI,WHITE,BLACK,AMERI_ES,ASIAN,HAWN_PI,OTHER,MULT_RACE,HISPANIC,MALES,FEMALES,AGE_UNDER5,AGE_5_17,AGE_18_21,AGE_22_29,AGE_30_39,AGE_40_49,AGE_50_64,AGE_65_UP,MED_AGE,MED_AGE_M,MED_AGE_F,HOUSEHOLDS,AVE_HH_SZ,HSEHLD_1_M,HSEHLD_1_F,MARHH_CHD,MARHH_NO_C,MHH_CHILD,FHH_CHILD,FAMILIES,AVE_FAM_SZ,HSE_UNITS,VACANT,OWNER_OCC,RENTER_OCC,NO_FARMS97,AVG_SIZE97,CROP_ACR97,AVG_SALE97,SQMI,Shape_Leng,Shape_Area,IcoMapAttr 
    From [Countys] ";
    
    public List<Countys> findAll(Object args)
    {
      List<Countys> rv = new List<Countys>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    OBJECTID,STATE_ID,NAME,STATE_NAME,STATE_FIPS,CNTY_FIPS,FIPS,POP2000,POP2004,POP00_SQMI,POP04_SQMI,WHITE,BLACK,AMERI_ES,ASIAN,HAWN_PI,OTHER,MULT_RACE,HISPANIC,MALES,FEMALES,AGE_UNDER5,AGE_5_17,AGE_18_21,AGE_22_29,AGE_30_39,AGE_40_49,AGE_50_64,AGE_65_UP,MED_AGE,MED_AGE_M,MED_AGE_F,HOUSEHOLDS,AVE_HH_SZ,HSEHLD_1_M,HSEHLD_1_F,MARHH_CHD,MARHH_NO_C,MHH_CHILD,FHH_CHILD,FAMILIES,AVE_FAM_SZ,HSE_UNITS,VACANT,OWNER_OCC,RENTER_OCC,NO_FARMS97,AVG_SIZE97,CROP_ACR97,AVG_SALE97,SQMI,Shape_Leng,Shape_Area,IcoMapAttr
     From [Countys]
    
       Where 
      OBJECTID = @OBJECTID
    ";

    public Countys findByPrimaryKey(
    int oBJECTID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@OBJECTID", oBJECTID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Countys not found, search by primary key");
 

    }


    public bool exists(Countys countys)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@OBJECTID", countys.OBJECTID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Countys doLoad(IDataReader dataReader)
    {
    Countys countys = new Countys();

    countys.OBJECTID = dataReader.GetInt32(0);
        countys.STATE_ID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          countys.NAME = dataReader.GetString(2);
        
          if(!dataReader.IsDBNull(3))
          countys.STATE_NAME = dataReader.GetString(3);
        
          if(!dataReader.IsDBNull(4))
          countys.STATE_FIPS = dataReader.GetString(4);
        
          if(!dataReader.IsDBNull(5))
          countys.CNTY_FIPS = dataReader.GetString(5);
        
          if(!dataReader.IsDBNull(6))
          countys.FIPS = dataReader.GetString(6);
        
          if(!dataReader.IsDBNull(7))
          countys.POP2000 = dataReader.GetDecimal(7);
        
          if(!dataReader.IsDBNull(8))
          countys.POP2004 = dataReader.GetDecimal(8);
        
          if(!dataReader.IsDBNull(9))
          countys.POP00_SQMI = dataReader.GetDecimal(9);
        
          if(!dataReader.IsDBNull(10))
          countys.POP04_SQMI = dataReader.GetDecimal(10);
        
          if(!dataReader.IsDBNull(11))
          countys.WHITE = dataReader.GetDecimal(11);
        
          if(!dataReader.IsDBNull(12))
          countys.BLACK = dataReader.GetDecimal(12);
        
          if(!dataReader.IsDBNull(13))
          countys.AMERI_ES = dataReader.GetDecimal(13);
        
          if(!dataReader.IsDBNull(14))
          countys.ASIAN = dataReader.GetDecimal(14);
        
          if(!dataReader.IsDBNull(15))
          countys.HAWN_PI = dataReader.GetDecimal(15);
        
          if(!dataReader.IsDBNull(16))
          countys.OTHER = dataReader.GetDecimal(16);
        
          if(!dataReader.IsDBNull(17))
          countys.MULT_RACE = dataReader.GetDecimal(17);
        
          if(!dataReader.IsDBNull(18))
          countys.HISPANIC = dataReader.GetDecimal(18);
        
          if(!dataReader.IsDBNull(19))
          countys.MALES = dataReader.GetDecimal(19);
        
          if(!dataReader.IsDBNull(20))
          countys.FEMALES = dataReader.GetDecimal(20);
        
          if(!dataReader.IsDBNull(21))
          countys.AGE_UNDER5 = dataReader.GetDecimal(21);
        
          if(!dataReader.IsDBNull(22))
          countys.AGE_5_17 = dataReader.GetDecimal(22);
        
          if(!dataReader.IsDBNull(23))
          countys.AGE_18_21 = dataReader.GetDecimal(23);
        
          if(!dataReader.IsDBNull(24))
          countys.AGE_22_29 = dataReader.GetDecimal(24);
        
          if(!dataReader.IsDBNull(25))
          countys.AGE_30_39 = dataReader.GetDecimal(25);
        
          if(!dataReader.IsDBNull(26))
          countys.AGE_40_49 = dataReader.GetDecimal(26);
        
          if(!dataReader.IsDBNull(27))
          countys.AGE_50_64 = dataReader.GetDecimal(27);
        
          if(!dataReader.IsDBNull(28))
          countys.AGE_65_UP = dataReader.GetDecimal(28);
        
          if(!dataReader.IsDBNull(29))
          countys.MED_AGE = dataReader.GetDecimal(29);
        
          if(!dataReader.IsDBNull(30))
          countys.MED_AGE_M = dataReader.GetDecimal(30);
        
          if(!dataReader.IsDBNull(31))
          countys.MED_AGE_F = dataReader.GetDecimal(31);
        
          if(!dataReader.IsDBNull(32))
          countys.HOUSEHOLDS = dataReader.GetDecimal(32);
        
          if(!dataReader.IsDBNull(33))
          countys.AVE_HH_SZ = dataReader.GetDecimal(33);
        
          if(!dataReader.IsDBNull(34))
          countys.HSEHLD_1_M = dataReader.GetDecimal(34);
        
          if(!dataReader.IsDBNull(35))
          countys.HSEHLD_1_F = dataReader.GetDecimal(35);
        
          if(!dataReader.IsDBNull(36))
          countys.MARHH_CHD = dataReader.GetDecimal(36);
        
          if(!dataReader.IsDBNull(37))
          countys.MARHH_NO_C = dataReader.GetDecimal(37);
        
          if(!dataReader.IsDBNull(38))
          countys.MHH_CHILD = dataReader.GetDecimal(38);
        
          if(!dataReader.IsDBNull(39))
          countys.FHH_CHILD = dataReader.GetDecimal(39);
        
          if(!dataReader.IsDBNull(40))
          countys.FAMILIES = dataReader.GetDecimal(40);
        
          if(!dataReader.IsDBNull(41))
          countys.AVE_FAM_SZ = dataReader.GetDecimal(41);
        
          if(!dataReader.IsDBNull(42))
          countys.HSE_UNITS = dataReader.GetDecimal(42);
        
          if(!dataReader.IsDBNull(43))
          countys.VACANT = dataReader.GetDecimal(43);
        
          if(!dataReader.IsDBNull(44))
          countys.OWNER_OCC = dataReader.GetDecimal(44);
        
          if(!dataReader.IsDBNull(45))
          countys.RENTER_OCC = dataReader.GetDecimal(45);
        
          if(!dataReader.IsDBNull(46))
          countys.NO_FARMS97 = dataReader.GetDecimal(46);
        
          if(!dataReader.IsDBNull(47))
          countys.AVG_SIZE97 = dataReader.GetDecimal(47);
        
          if(!dataReader.IsDBNull(48))
          countys.CROP_ACR97 = dataReader.GetDecimal(48);
        
          if(!dataReader.IsDBNull(49))
          countys.AVG_SALE97 = dataReader.GetDecimal(49);
        
          if(!dataReader.IsDBNull(50))
          countys.SQMI = dataReader.GetDecimal(50);
        
          if(!dataReader.IsDBNull(51))
          countys.Shape_Leng = dataReader.GetDecimal(51);
        
          if(!dataReader.IsDBNull(52))
          countys.Shape_Area = dataReader.GetDecimal(52);
        
          if(!dataReader.IsDBNull(53))
          countys.IcoMapAttr = dataReader.GetString(53);
        countys.IsLoaded = true;
    
    return registerRecord(countys);
    }


    protected override Countys doLoad(Hashtable hashtable)
    {
      Countys countys = new Countys();

      
        if(hashtable.ContainsKey("OBJECTID"))
            countys.OBJECTID = ( int)hashtable["OBJECTID"];
      
        if(hashtable.ContainsKey("STATE_ID"))
            countys.STATE_ID = ( int)hashtable["STATE_ID"];
      
        if(hashtable.ContainsKey("NAME"))
            countys.NAME = ( String)hashtable["NAME"];
      
        if(hashtable.ContainsKey("STATE_NAME"))
            countys.STATE_NAME = ( String)hashtable["STATE_NAME"];
      
        if(hashtable.ContainsKey("STATE_FIPS"))
            countys.STATE_FIPS = ( String)hashtable["STATE_FIPS"];
      
        if(hashtable.ContainsKey("CNTY_FIPS"))
            countys.CNTY_FIPS = ( String)hashtable["CNTY_FIPS"];
      
        if(hashtable.ContainsKey("FIPS"))
            countys.FIPS = ( String)hashtable["FIPS"];
      
        if(hashtable.ContainsKey("POP2000"))
            countys.POP2000 = ( decimal)hashtable["POP2000"];
      
        if(hashtable.ContainsKey("POP2004"))
            countys.POP2004 = ( decimal)hashtable["POP2004"];
      
        if(hashtable.ContainsKey("POP00_SQMI"))
            countys.POP00_SQMI = ( decimal)hashtable["POP00_SQMI"];
      
        if(hashtable.ContainsKey("POP04_SQMI"))
            countys.POP04_SQMI = ( decimal)hashtable["POP04_SQMI"];
      
        if(hashtable.ContainsKey("WHITE"))
            countys.WHITE = ( decimal)hashtable["WHITE"];
      
        if(hashtable.ContainsKey("BLACK"))
            countys.BLACK = ( decimal)hashtable["BLACK"];
      
        if(hashtable.ContainsKey("AMERI_ES"))
            countys.AMERI_ES = ( decimal)hashtable["AMERI_ES"];
      
        if(hashtable.ContainsKey("ASIAN"))
            countys.ASIAN = ( decimal)hashtable["ASIAN"];
      
        if(hashtable.ContainsKey("HAWN_PI"))
            countys.HAWN_PI = ( decimal)hashtable["HAWN_PI"];
      
        if(hashtable.ContainsKey("OTHER"))
            countys.OTHER = ( decimal)hashtable["OTHER"];
      
        if(hashtable.ContainsKey("MULT_RACE"))
            countys.MULT_RACE = ( decimal)hashtable["MULT_RACE"];
      
        if(hashtable.ContainsKey("HISPANIC"))
            countys.HISPANIC = ( decimal)hashtable["HISPANIC"];
      
        if(hashtable.ContainsKey("MALES"))
            countys.MALES = ( decimal)hashtable["MALES"];
      
        if(hashtable.ContainsKey("FEMALES"))
            countys.FEMALES = ( decimal)hashtable["FEMALES"];
      
        if(hashtable.ContainsKey("AGE_UNDER5"))
            countys.AGE_UNDER5 = ( decimal)hashtable["AGE_UNDER5"];
      
        if(hashtable.ContainsKey("AGE_5_17"))
            countys.AGE_5_17 = ( decimal)hashtable["AGE_5_17"];
      
        if(hashtable.ContainsKey("AGE_18_21"))
            countys.AGE_18_21 = ( decimal)hashtable["AGE_18_21"];
      
        if(hashtable.ContainsKey("AGE_22_29"))
            countys.AGE_22_29 = ( decimal)hashtable["AGE_22_29"];
      
        if(hashtable.ContainsKey("AGE_30_39"))
            countys.AGE_30_39 = ( decimal)hashtable["AGE_30_39"];
      
        if(hashtable.ContainsKey("AGE_40_49"))
            countys.AGE_40_49 = ( decimal)hashtable["AGE_40_49"];
      
        if(hashtable.ContainsKey("AGE_50_64"))
            countys.AGE_50_64 = ( decimal)hashtable["AGE_50_64"];
      
        if(hashtable.ContainsKey("AGE_65_UP"))
            countys.AGE_65_UP = ( decimal)hashtable["AGE_65_UP"];
      
        if(hashtable.ContainsKey("MED_AGE"))
            countys.MED_AGE = ( decimal)hashtable["MED_AGE"];
      
        if(hashtable.ContainsKey("MED_AGE_M"))
            countys.MED_AGE_M = ( decimal)hashtable["MED_AGE_M"];
      
        if(hashtable.ContainsKey("MED_AGE_F"))
            countys.MED_AGE_F = ( decimal)hashtable["MED_AGE_F"];
      
        if(hashtable.ContainsKey("HOUSEHOLDS"))
            countys.HOUSEHOLDS = ( decimal)hashtable["HOUSEHOLDS"];
      
        if(hashtable.ContainsKey("AVE_HH_SZ"))
            countys.AVE_HH_SZ = ( decimal)hashtable["AVE_HH_SZ"];
      
        if(hashtable.ContainsKey("HSEHLD_1_M"))
            countys.HSEHLD_1_M = ( decimal)hashtable["HSEHLD_1_M"];
      
        if(hashtable.ContainsKey("HSEHLD_1_F"))
            countys.HSEHLD_1_F = ( decimal)hashtable["HSEHLD_1_F"];
      
        if(hashtable.ContainsKey("MARHH_CHD"))
            countys.MARHH_CHD = ( decimal)hashtable["MARHH_CHD"];
      
        if(hashtable.ContainsKey("MARHH_NO_C"))
            countys.MARHH_NO_C = ( decimal)hashtable["MARHH_NO_C"];
      
        if(hashtable.ContainsKey("MHH_CHILD"))
            countys.MHH_CHILD = ( decimal)hashtable["MHH_CHILD"];
      
        if(hashtable.ContainsKey("FHH_CHILD"))
            countys.FHH_CHILD = ( decimal)hashtable["FHH_CHILD"];
      
        if(hashtable.ContainsKey("FAMILIES"))
            countys.FAMILIES = ( decimal)hashtable["FAMILIES"];
      
        if(hashtable.ContainsKey("AVE_FAM_SZ"))
            countys.AVE_FAM_SZ = ( decimal)hashtable["AVE_FAM_SZ"];
      
        if(hashtable.ContainsKey("HSE_UNITS"))
            countys.HSE_UNITS = ( decimal)hashtable["HSE_UNITS"];
      
        if(hashtable.ContainsKey("VACANT"))
            countys.VACANT = ( decimal)hashtable["VACANT"];
      
        if(hashtable.ContainsKey("OWNER_OCC"))
            countys.OWNER_OCC = ( decimal)hashtable["OWNER_OCC"];
      
        if(hashtable.ContainsKey("RENTER_OCC"))
            countys.RENTER_OCC = ( decimal)hashtable["RENTER_OCC"];
      
        if(hashtable.ContainsKey("NO_FARMS97"))
            countys.NO_FARMS97 = ( decimal)hashtable["NO_FARMS97"];
      
        if(hashtable.ContainsKey("AVG_SIZE97"))
            countys.AVG_SIZE97 = ( decimal)hashtable["AVG_SIZE97"];
      
        if(hashtable.ContainsKey("CROP_ACR97"))
            countys.CROP_ACR97 = ( decimal)hashtable["CROP_ACR97"];
      
        if(hashtable.ContainsKey("AVG_SALE97"))
            countys.AVG_SALE97 = ( decimal)hashtable["AVG_SALE97"];
      
        if(hashtable.ContainsKey("SQMI"))
            countys.SQMI = ( decimal)hashtable["SQMI"];
      
        if(hashtable.ContainsKey("Shape_Leng"))
            countys.Shape_Leng = ( decimal)hashtable["Shape_Leng"];
      
        if(hashtable.ContainsKey("Shape_Area"))
            countys.Shape_Area = ( decimal)hashtable["Shape_Area"];
      
        if(hashtable.ContainsKey("IcoMapAttr"))
            countys.IcoMapAttr = ( String)hashtable["IcoMapAttr"];
      

      return countys;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Countys]
    
      Where
      OBJECTID = @OBJECTID";
    [Synchronized]
    public Countys remove(Countys countys)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@OBJECTID", countys.OBJECTID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(countys,DataMapperOperation.delete);

      return registerRecord(countys);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Countys save( Countys countys )
    {
      if(exists(countys))
        return update(countys);
        return create(countys);
    }

  

    const String SqlUpdate = @"Update [Countys] Set 
    STATE_ID = @STATE_ID,NAME = @NAME,STATE_NAME = @STATE_NAME,STATE_FIPS = @STATE_FIPS,CNTY_FIPS = @CNTY_FIPS,FIPS = @FIPS,POP2000 = @POP2000,POP2004 = @POP2004,POP00_SQMI = @POP00_SQMI,POP04_SQMI = @POP04_SQMI,WHITE = @WHITE,BLACK = @BLACK,AMERI_ES = @AMERI_ES,ASIAN = @ASIAN,HAWN_PI = @HAWN_PI,OTHER = @OTHER,MULT_RACE = @MULT_RACE,HISPANIC = @HISPANIC,MALES = @MALES,FEMALES = @FEMALES,AGE_UNDER5 = @AGE_UNDER5,AGE_5_17 = @AGE_5_17,AGE_18_21 = @AGE_18_21,AGE_22_29 = @AGE_22_29,AGE_30_39 = @AGE_30_39,AGE_40_49 = @AGE_40_49,AGE_50_64 = @AGE_50_64,AGE_65_UP = @AGE_65_UP,MED_AGE = @MED_AGE,MED_AGE_M = @MED_AGE_M,MED_AGE_F = @MED_AGE_F,HOUSEHOLDS = @HOUSEHOLDS,AVE_HH_SZ = @AVE_HH_SZ,HSEHLD_1_M = @HSEHLD_1_M,HSEHLD_1_F = @HSEHLD_1_F,MARHH_CHD = @MARHH_CHD,MARHH_NO_C = @MARHH_NO_C,MHH_CHILD = @MHH_CHILD,FHH_CHILD = @FHH_CHILD,FAMILIES = @FAMILIES,AVE_FAM_SZ = @AVE_FAM_SZ,HSE_UNITS = @HSE_UNITS,VACANT = @VACANT,OWNER_OCC = @OWNER_OCC,RENTER_OCC = @RENTER_OCC,NO_FARMS97 = @NO_FARMS97,AVG_SIZE97 = @AVG_SIZE97,CROP_ACR97 = @CROP_ACR97,AVG_SALE97 = @AVG_SALE97,SQMI = @SQMI,Shape_Leng = @Shape_Leng,Shape_Area = @Shape_Area,IcoMapAttr = @IcoMapAttr
       Where 
      OBJECTID = @OBJECTID";
    
    
    [Synchronized]
    public Countys update(Countys countys)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@OBJECTID", countys.OBJECTID);
                  
                      sqlCommand.Parameters.AddWithValue("@STATE_ID", countys.STATE_ID);
                  
                    if(countys.NAME != null)
                      sqlCommand.Parameters.AddWithValue("@NAME", countys.NAME);
                    else
                      sqlCommand.Parameters.AddWithValue("@NAME", DBNull.Value);
                  
                    if(countys.STATE_NAME != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", countys.STATE_NAME);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", DBNull.Value);
                  
                    if(countys.STATE_FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", countys.STATE_FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", DBNull.Value);
                  
                    if(countys.CNTY_FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@CNTY_FIPS", countys.CNTY_FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@CNTY_FIPS", DBNull.Value);
                  
                    if(countys.FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@FIPS", countys.FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@FIPS", DBNull.Value);
                  
                    if(countys.POP2000 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2000", countys.POP2000);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2000", DBNull.Value);
                  
                    if(countys.POP2004 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2004", countys.POP2004);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2004", DBNull.Value);
                  
                    if(countys.POP00_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", countys.POP00_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", DBNull.Value);
                  
                    if(countys.POP04_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", countys.POP04_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", DBNull.Value);
                  
                    if(countys.WHITE != null)
                      sqlCommand.Parameters.AddWithValue("@WHITE", countys.WHITE);
                    else
                      sqlCommand.Parameters.AddWithValue("@WHITE", DBNull.Value);
                  
                    if(countys.BLACK != null)
                      sqlCommand.Parameters.AddWithValue("@BLACK", countys.BLACK);
                    else
                      sqlCommand.Parameters.AddWithValue("@BLACK", DBNull.Value);
                  
                    if(countys.AMERI_ES != null)
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", countys.AMERI_ES);
                    else
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", DBNull.Value);
                  
                    if(countys.ASIAN != null)
                      sqlCommand.Parameters.AddWithValue("@ASIAN", countys.ASIAN);
                    else
                      sqlCommand.Parameters.AddWithValue("@ASIAN", DBNull.Value);
                  
                    if(countys.HAWN_PI != null)
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", countys.HAWN_PI);
                    else
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", DBNull.Value);
                  
                    if(countys.OTHER != null)
                      sqlCommand.Parameters.AddWithValue("@OTHER", countys.OTHER);
                    else
                      sqlCommand.Parameters.AddWithValue("@OTHER", DBNull.Value);
                  
                    if(countys.MULT_RACE != null)
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", countys.MULT_RACE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", DBNull.Value);
                  
                    if(countys.HISPANIC != null)
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", countys.HISPANIC);
                    else
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", DBNull.Value);
                  
                    if(countys.MALES != null)
                      sqlCommand.Parameters.AddWithValue("@MALES", countys.MALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@MALES", DBNull.Value);
                  
                    if(countys.FEMALES != null)
                      sqlCommand.Parameters.AddWithValue("@FEMALES", countys.FEMALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FEMALES", DBNull.Value);
                  
                    if(countys.AGE_UNDER5 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", countys.AGE_UNDER5);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", DBNull.Value);
                  
                    if(countys.AGE_5_17 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", countys.AGE_5_17);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", DBNull.Value);
                  
                    if(countys.AGE_18_21 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", countys.AGE_18_21);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", DBNull.Value);
                  
                    if(countys.AGE_22_29 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", countys.AGE_22_29);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", DBNull.Value);
                  
                    if(countys.AGE_30_39 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", countys.AGE_30_39);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", DBNull.Value);
                  
                    if(countys.AGE_40_49 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", countys.AGE_40_49);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", DBNull.Value);
                  
                    if(countys.AGE_50_64 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", countys.AGE_50_64);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", DBNull.Value);
                  
                    if(countys.AGE_65_UP != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", countys.AGE_65_UP);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", DBNull.Value);
                  
                    if(countys.MED_AGE != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", countys.MED_AGE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", DBNull.Value);
                  
                    if(countys.MED_AGE_M != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", countys.MED_AGE_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", DBNull.Value);
                  
                    if(countys.MED_AGE_F != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", countys.MED_AGE_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", DBNull.Value);
                  
                    if(countys.HOUSEHOLDS != null)
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", countys.HOUSEHOLDS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", DBNull.Value);
                  
                    if(countys.AVE_HH_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", countys.AVE_HH_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", DBNull.Value);
                  
                    if(countys.HSEHLD_1_M != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", countys.HSEHLD_1_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", DBNull.Value);
                  
                    if(countys.HSEHLD_1_F != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", countys.HSEHLD_1_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", DBNull.Value);
                  
                    if(countys.MARHH_CHD != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", countys.MARHH_CHD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", DBNull.Value);
                  
                    if(countys.MARHH_NO_C != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", countys.MARHH_NO_C);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", DBNull.Value);
                  
                    if(countys.MHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", countys.MHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", DBNull.Value);
                  
                    if(countys.FHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", countys.FHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", DBNull.Value);
                  
                    if(countys.FAMILIES != null)
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", countys.FAMILIES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", DBNull.Value);
                  
                    if(countys.AVE_FAM_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", countys.AVE_FAM_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", DBNull.Value);
                  
                    if(countys.HSE_UNITS != null)
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", countys.HSE_UNITS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", DBNull.Value);
                  
                    if(countys.VACANT != null)
                      sqlCommand.Parameters.AddWithValue("@VACANT", countys.VACANT);
                    else
                      sqlCommand.Parameters.AddWithValue("@VACANT", DBNull.Value);
                  
                    if(countys.OWNER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", countys.OWNER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", DBNull.Value);
                  
                    if(countys.RENTER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", countys.RENTER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", DBNull.Value);
                  
                    if(countys.NO_FARMS97 != null)
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", countys.NO_FARMS97);
                    else
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", DBNull.Value);
                  
                    if(countys.AVG_SIZE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", countys.AVG_SIZE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", DBNull.Value);
                  
                    if(countys.CROP_ACR97 != null)
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", countys.CROP_ACR97);
                    else
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", DBNull.Value);
                  
                    if(countys.AVG_SALE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", countys.AVG_SALE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", DBNull.Value);
                  
                    if(countys.SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@SQMI", countys.SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@SQMI", DBNull.Value);
                  
                    if(countys.Shape_Leng != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", countys.Shape_Leng);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", DBNull.Value);
                  
                    if(countys.Shape_Area != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", countys.Shape_Area);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", DBNull.Value);
                  
                    if(countys.IcoMapAttr != null)
                      sqlCommand.Parameters.AddWithValue("@IcoMapAttr", countys.IcoMapAttr);
                    else
                      sqlCommand.Parameters.AddWithValue("@IcoMapAttr", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(countys,DataMapperOperation.update);

      return registerRecord(countys);
    }

  
    }
    
  
    
    public partial class Document: DomainObject
    {
    
      protected int _docID;
    
      protected bool _isPublic;
    
      protected int? _docTypeId;
    
      protected String _vol;
    
      protected String _pg;
    
      protected String _documentNo;
    
      protected String _county;
    
      protected String _state;
    
      protected DateTime? _dateFiled;
    
      protected DateTime? _dateSigned;
    
      protected String _researchNote;
    
      protected String _imageLink;
    

    public Document(){}

    public Document(
    int 
            docID,bool 
            isPublic,int 
            docTypeId,String 
            vol,String 
            pg,String 
            documentNo,String 
            county,String 
            state,DateTime 
            dateFiled,DateTime 
            dateSigned,String 
            researchNote,String 
            imageLink
    )
    {
    DocID = docID;
    IsPublic = isPublic;
    DocTypeId = docTypeId;
    Vol = vol;
    Pg = pg;
    DocumentNo = documentNo;
    County = county;
    State = state;
    DateFiled = dateFiled;
    DateSigned = dateSigned;
    ResearchNote = researchNote;
    ImageLink = imageLink;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Document."
    
      + DocID.ToString()
    ;
    
    return uri;
    }

    

      public int DocID
      {
        
            get { return _docID;}
            set 
            { 
                _docID = value;
            }
          
      }
    

      public bool IsPublic
      {
        
            get { return _isPublic;}
            set 
            { 
                _isPublic = value;
            }
          
      }
    

      public int? DocTypeId
      {
        
            get { return _docTypeId;}
            set 
            { 
                _docTypeId = value;
            }
          
      }
    

      public String Vol
      {
        
            get { return _vol;}
            set 
            { 
                _vol = value;
            }
          
      }
    

      public String Pg
      {
        
            get { return _pg;}
            set 
            { 
                _pg = value;
            }
          
      }
    

      public String DocumentNo
      {
        
            get { return _documentNo;}
            set 
            { 
                _documentNo = value;
            }
          
      }
    

      public String County
      {
        
            get { return _county;}
            set 
            { 
                _county = value;
            }
          
      }
    

      public String State
      {
        
            get { return _state;}
            set 
            { 
                _state = value;
            }
          
      }
    

      public DateTime? DateFiled
      {
        
            get { return _dateFiled;}
            set 
            { 
                _dateFiled = value;
            }
          
      }
    

      public DateTime? DateSigned
      {
        
            get { return _dateSigned;}
            set 
            { 
                _dateSigned = value;
            }
          
      }
    

      public String ResearchNote
      {
        
            get { return _researchNote;}
            set 
            { 
                _researchNote = value;
            }
          
      }
    

      public String ImageLink
      {
        
            get { return _imageLink;}
            set 
            { 
                _imageLink = value;
            }
          
      }
    


    }
  

    public partial class DocumentDataMapper:TDataMapper<Document,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public DocumentDataMapper(){}
      public DocumentDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Document";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Document] (
    IsPublic,DocTypeId,Vol,Pg,DocumentNo,County,State,DateFiled,DateSigned,ResearchNote,ImageLink) Values (
    
      @IsPublic,
      @DocTypeId,
      @Vol,
      @Pg,
      @DocumentNo,
      @County,
      @State,
      @DateFiled,
      @DateSigned,
      @ResearchNote,
      @ImageLink);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Document create( Document document )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                      sqlCommand.Parameters.AddWithValue("@IsPublic", document.IsPublic);
                  
                    if(document.DocTypeId != null)
                      sqlCommand.Parameters.AddWithValue("@DocTypeId", document.DocTypeId);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocTypeId", DBNull.Value);
                  
                    if(document.Vol != null)
                      sqlCommand.Parameters.AddWithValue("@Vol", document.Vol);
                    else
                      sqlCommand.Parameters.AddWithValue("@Vol", DBNull.Value);
                  
                    if(document.Pg != null)
                      sqlCommand.Parameters.AddWithValue("@Pg", document.Pg);
                    else
                      sqlCommand.Parameters.AddWithValue("@Pg", DBNull.Value);
                  
                    if(document.DocumentNo != null)
                      sqlCommand.Parameters.AddWithValue("@DocumentNo", document.DocumentNo);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocumentNo", DBNull.Value);
                  
                    if(document.County != null)
                      sqlCommand.Parameters.AddWithValue("@County", document.County);
                    else
                      sqlCommand.Parameters.AddWithValue("@County", DBNull.Value);
                  
                    if(document.State != null)
                      sqlCommand.Parameters.AddWithValue("@State", document.State);
                    else
                      sqlCommand.Parameters.AddWithValue("@State", DBNull.Value);
                  
                    if(document.DateFiled != null)
                      sqlCommand.Parameters.AddWithValue("@DateFiled", document.DateFiled);
                    else
                      sqlCommand.Parameters.AddWithValue("@DateFiled", DBNull.Value);
                  
                    if(document.DateSigned != null)
                      sqlCommand.Parameters.AddWithValue("@DateSigned", document.DateSigned);
                    else
                      sqlCommand.Parameters.AddWithValue("@DateSigned", DBNull.Value);
                  
                    if(document.ResearchNote != null)
                      sqlCommand.Parameters.AddWithValue("@ResearchNote", document.ResearchNote);
                    else
                      sqlCommand.Parameters.AddWithValue("@ResearchNote", DBNull.Value);
                  
                    if(document.ImageLink != null)
                      sqlCommand.Parameters.AddWithValue("@ImageLink", document.ImageLink);
                    else
                      sqlCommand.Parameters.AddWithValue("@ImageLink", DBNull.Value);
                  document.DocID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(document,DataMapperOperation.create);

      return registerRecord(document);
    }

  

    private const String SqlSelectAll = @"Select
    DocID,IsPublic,DocTypeId,Vol,Pg,DocumentNo,County,State,DateFiled,DateSigned,ResearchNote,ImageLink 
    From [Document] ";
    
    public List<Document> findAll(Object args)
    {
      List<Document> rv = new List<Document>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    DocID,IsPublic,DocTypeId,Vol,Pg,DocumentNo,County,State,DateFiled,DateSigned,ResearchNote,ImageLink
     From [Document]
    
       Where 
      DocID = @DocID
    ";

    public Document findByPrimaryKey(
    int docID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@DocID", docID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Document not found, search by primary key");
 

    }


    public bool exists(Document document)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@DocID", document.DocID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Document doLoad(IDataReader dataReader)
    {
    Document document = new Document();

    document.DocID = dataReader.GetInt32(0);
        document.IsPublic = dataReader.GetBoolean(1);
        
          if(!dataReader.IsDBNull(2))
          document.DocTypeId = dataReader.GetInt32(2);
        
          if(!dataReader.IsDBNull(3))
          document.Vol = dataReader.GetString(3);
        
          if(!dataReader.IsDBNull(4))
          document.Pg = dataReader.GetString(4);
        
          if(!dataReader.IsDBNull(5))
          document.DocumentNo = dataReader.GetString(5);
        
          if(!dataReader.IsDBNull(6))
          document.County = dataReader.GetString(6);
        
          if(!dataReader.IsDBNull(7))
          document.State = dataReader.GetString(7);
        
          if(!dataReader.IsDBNull(8))
          document.DateFiled = dataReader.GetDateTime(8);
        
          if(!dataReader.IsDBNull(9))
          document.DateSigned = dataReader.GetDateTime(9);
        
          if(!dataReader.IsDBNull(10))
          document.ResearchNote = dataReader.GetString(10);
        
          if(!dataReader.IsDBNull(11))
          document.ImageLink = dataReader.GetString(11);
        document.IsLoaded = true;
    
    return registerRecord(document);
    }


    protected override Document doLoad(Hashtable hashtable)
    {
      Document document = new Document();

      
        if(hashtable.ContainsKey("DocID"))
            document.DocID = ( int)hashtable["DocID"];
      
        if(hashtable.ContainsKey("IsPublic"))
            document.IsPublic = ( bool)hashtable["IsPublic"];
      
        if(hashtable.ContainsKey("DocTypeId"))
            document.DocTypeId = ( int)hashtable["DocTypeId"];
      
        if(hashtable.ContainsKey("Vol"))
            document.Vol = ( String)hashtable["Vol"];
      
        if(hashtable.ContainsKey("Pg"))
            document.Pg = ( String)hashtable["Pg"];
      
        if(hashtable.ContainsKey("DocumentNo"))
            document.DocumentNo = ( String)hashtable["DocumentNo"];
      
        if(hashtable.ContainsKey("County"))
            document.County = ( String)hashtable["County"];
      
        if(hashtable.ContainsKey("State"))
            document.State = ( String)hashtable["State"];
      
        if(hashtable.ContainsKey("DateFiled"))
            document.DateFiled = ( DateTime)hashtable["DateFiled"];
      
        if(hashtable.ContainsKey("DateSigned"))
            document.DateSigned = ( DateTime)hashtable["DateSigned"];
      
        if(hashtable.ContainsKey("ResearchNote"))
            document.ResearchNote = ( String)hashtable["ResearchNote"];
      
        if(hashtable.ContainsKey("ImageLink"))
            document.ImageLink = ( String)hashtable["ImageLink"];
      

      return document;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Document]
    
      Where
      DocID = @DocID";
    [Synchronized]
    public Document remove(Document document)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@DocID", document.DocID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(document,DataMapperOperation.delete);

      return registerRecord(document);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Document save( Document document )
    {
      if(exists(document))
        return update(document);
        return create(document);
    }

  

    const String SqlUpdate = @"Update [Document] Set 
    IsPublic = @IsPublic,DocTypeId = @DocTypeId,Vol = @Vol,Pg = @Pg,DocumentNo = @DocumentNo,County = @County,State = @State,DateFiled = @DateFiled,DateSigned = @DateSigned,ResearchNote = @ResearchNote,ImageLink = @ImageLink
       Where 
      DocID = @DocID";
    
    
    [Synchronized]
    public Document update(Document document)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@DocID", document.DocID);
                  
                      sqlCommand.Parameters.AddWithValue("@IsPublic", document.IsPublic);
                  
                    if(document.DocTypeId != null)
                      sqlCommand.Parameters.AddWithValue("@DocTypeId", document.DocTypeId);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocTypeId", DBNull.Value);
                  
                    if(document.Vol != null)
                      sqlCommand.Parameters.AddWithValue("@Vol", document.Vol);
                    else
                      sqlCommand.Parameters.AddWithValue("@Vol", DBNull.Value);
                  
                    if(document.Pg != null)
                      sqlCommand.Parameters.AddWithValue("@Pg", document.Pg);
                    else
                      sqlCommand.Parameters.AddWithValue("@Pg", DBNull.Value);
                  
                    if(document.DocumentNo != null)
                      sqlCommand.Parameters.AddWithValue("@DocumentNo", document.DocumentNo);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocumentNo", DBNull.Value);
                  
                    if(document.County != null)
                      sqlCommand.Parameters.AddWithValue("@County", document.County);
                    else
                      sqlCommand.Parameters.AddWithValue("@County", DBNull.Value);
                  
                    if(document.State != null)
                      sqlCommand.Parameters.AddWithValue("@State", document.State);
                    else
                      sqlCommand.Parameters.AddWithValue("@State", DBNull.Value);
                  
                    if(document.DateFiled != null)
                      sqlCommand.Parameters.AddWithValue("@DateFiled", document.DateFiled);
                    else
                      sqlCommand.Parameters.AddWithValue("@DateFiled", DBNull.Value);
                  
                    if(document.DateSigned != null)
                      sqlCommand.Parameters.AddWithValue("@DateSigned", document.DateSigned);
                    else
                      sqlCommand.Parameters.AddWithValue("@DateSigned", DBNull.Value);
                  
                    if(document.ResearchNote != null)
                      sqlCommand.Parameters.AddWithValue("@ResearchNote", document.ResearchNote);
                    else
                      sqlCommand.Parameters.AddWithValue("@ResearchNote", DBNull.Value);
                  
                    if(document.ImageLink != null)
                      sqlCommand.Parameters.AddWithValue("@ImageLink", document.ImageLink);
                    else
                      sqlCommand.Parameters.AddWithValue("@ImageLink", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(document,DataMapperOperation.update);

      return registerRecord(document);
    }

  
    }
    
  
    
    public partial class Documenttype: DomainObject
    {
    
      protected int _docTypeID;
    
      protected String _name;
    
      protected String _sellerRole;
    
      protected String _buyerRole;
    

    public Documenttype(){}

    public Documenttype(
    int 
            docTypeID,String 
            name,String 
            sellerRole,String 
            buyerRole
    )
    {
    DocTypeID = docTypeID;
    Name = name;
    SellerRole = sellerRole;
    BuyerRole = buyerRole;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Documenttype."
    
      + DocTypeID.ToString()
    ;
    
    return uri;
    }

    

      public int DocTypeID
      {
        
            get { return _docTypeID;}
            set 
            { 
                _docTypeID = value;
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
    

      public String SellerRole
      {
        
            get { return _sellerRole;}
            set 
            { 
                _sellerRole = value;
            }
          
      }
    

      public String BuyerRole
      {
        
            get { return _buyerRole;}
            set 
            { 
                _buyerRole = value;
            }
          
      }
    


    }
  

    public partial class DocumenttypeDataMapper:TDataMapper<Documenttype,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public DocumenttypeDataMapper(){}
      public DocumenttypeDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Documenttype";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Documenttype] (
    Name,SellerRole,BuyerRole) Values (
    
      @Name,
      @SellerRole,
      @BuyerRole);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Documenttype create( Documenttype documenttype )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(documenttype.Name != null)
                      sqlCommand.Parameters.AddWithValue("@Name", documenttype.Name);
                    else
                      sqlCommand.Parameters.AddWithValue("@Name", DBNull.Value);
                  
                    if(documenttype.SellerRole != null)
                      sqlCommand.Parameters.AddWithValue("@SellerRole", documenttype.SellerRole);
                    else
                      sqlCommand.Parameters.AddWithValue("@SellerRole", DBNull.Value);
                  
                    if(documenttype.BuyerRole != null)
                      sqlCommand.Parameters.AddWithValue("@BuyerRole", documenttype.BuyerRole);
                    else
                      sqlCommand.Parameters.AddWithValue("@BuyerRole", DBNull.Value);
                  documenttype.DocTypeID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(documenttype,DataMapperOperation.create);

      return registerRecord(documenttype);
    }

  

    private const String SqlSelectAll = @"Select
    DocTypeID,Name,SellerRole,BuyerRole 
    From [Documenttype] ";
    
    public List<Documenttype> findAll(Object args)
    {
      List<Documenttype> rv = new List<Documenttype>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    DocTypeID,Name,SellerRole,BuyerRole
     From [Documenttype]
    
       Where 
      DocTypeID = @DocTypeID
    ";

    public Documenttype findByPrimaryKey(
    int docTypeID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@DocTypeID", docTypeID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Documenttype not found, search by primary key");
 

    }


    public bool exists(Documenttype documenttype)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@DocTypeID", documenttype.DocTypeID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Documenttype doLoad(IDataReader dataReader)
    {
    Documenttype documenttype = new Documenttype();

    documenttype.DocTypeID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          documenttype.Name = dataReader.GetString(1);
        
          if(!dataReader.IsDBNull(2))
          documenttype.SellerRole = dataReader.GetString(2);
        
          if(!dataReader.IsDBNull(3))
          documenttype.BuyerRole = dataReader.GetString(3);
        documenttype.IsLoaded = true;
    
    return registerRecord(documenttype);
    }


    protected override Documenttype doLoad(Hashtable hashtable)
    {
      Documenttype documenttype = new Documenttype();

      
        if(hashtable.ContainsKey("DocTypeID"))
            documenttype.DocTypeID = ( int)hashtable["DocTypeID"];
      
        if(hashtable.ContainsKey("Name"))
            documenttype.Name = ( String)hashtable["Name"];
      
        if(hashtable.ContainsKey("SellerRole"))
            documenttype.SellerRole = ( String)hashtable["SellerRole"];
      
        if(hashtable.ContainsKey("BuyerRole"))
            documenttype.BuyerRole = ( String)hashtable["BuyerRole"];
      

      return documenttype;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Documenttype]
    
      Where
      DocTypeID = @DocTypeID";
    [Synchronized]
    public Documenttype remove(Documenttype documenttype)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@DocTypeID", documenttype.DocTypeID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(documenttype,DataMapperOperation.delete);

      return registerRecord(documenttype);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Documenttype save( Documenttype documenttype )
    {
      if(exists(documenttype))
        return update(documenttype);
        return create(documenttype);
    }

  

    const String SqlUpdate = @"Update [Documenttype] Set 
    Name = @Name,SellerRole = @SellerRole,BuyerRole = @BuyerRole
       Where 
      DocTypeID = @DocTypeID";
    
    
    [Synchronized]
    public Documenttype update(Documenttype documenttype)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@DocTypeID", documenttype.DocTypeID);
                  
                    if(documenttype.Name != null)
                      sqlCommand.Parameters.AddWithValue("@Name", documenttype.Name);
                    else
                      sqlCommand.Parameters.AddWithValue("@Name", DBNull.Value);
                  
                    if(documenttype.SellerRole != null)
                      sqlCommand.Parameters.AddWithValue("@SellerRole", documenttype.SellerRole);
                    else
                      sqlCommand.Parameters.AddWithValue("@SellerRole", DBNull.Value);
                  
                    if(documenttype.BuyerRole != null)
                      sqlCommand.Parameters.AddWithValue("@BuyerRole", documenttype.BuyerRole);
                    else
                      sqlCommand.Parameters.AddWithValue("@BuyerRole", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(documenttype,DataMapperOperation.update);

      return registerRecord(documenttype);
    }

  
    }
    
  
    
    public partial class Participant: DomainObject
    {
    
      protected int _participantID;
    
      protected int? _docID;
    
      protected int? _docRoleID;
    
      protected String _asNamed;
    
      protected String _phoneHome;
    
      protected String _phoneOffice;
    
      protected String _phoneCell;
    
      protected String _phoneAlt;
    
      protected String _entityName;
    
      protected String _firstName;
    
      protected String _middleName;
    
      protected String _lastName;
    
      protected String _contactPosition;
    
      protected String _tAXID;
    
      protected String _sSN;
    
      protected int _parentID;
    
      protected int? _typeId;
    

    public Participant(){}

    public Participant(
    int 
            participantID,int 
            docID,int 
            docRoleID,String 
            asNamed,String 
            phoneHome,String 
            phoneOffice,String 
            phoneCell,String 
            phoneAlt,String 
            entityName,String 
            firstName,String 
            middleName,String 
            lastName,String 
            contactPosition,String 
            tAXID,String 
            sSN,int 
            parentID,int 
            typeId
    )
    {
    ParticipantID = participantID;
    DocID = docID;
    DocRoleID = docRoleID;
    AsNamed = asNamed;
    PhoneHome = phoneHome;
    PhoneOffice = phoneOffice;
    PhoneCell = phoneCell;
    PhoneAlt = phoneAlt;
    EntityName = entityName;
    FirstName = firstName;
    MiddleName = middleName;
    LastName = lastName;
    ContactPosition = contactPosition;
    TAXID = tAXID;
    SSN = sSN;
    ParentID = parentID;
    TypeId = typeId;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Participant."
    
      + ParticipantID.ToString()
    ;
    
    return uri;
    }

    

      public int ParticipantID
      {
        
            get { return _participantID;}
            set 
            { 
                _participantID = value;
            }
          
      }
    

      public int? DocID
      {
        
            get { return _docID;}
            set 
            { 
                _docID = value;
            }
          
      }
    

      public int? DocRoleID
      {
        
            get { return _docRoleID;}
            set 
            { 
                _docRoleID = value;
            }
          
      }
    

      public String AsNamed
      {
        
            get { return _asNamed;}
            set 
            { 
                _asNamed = value;
            }
          
      }
    

      public String PhoneHome
      {
        
            get { return _phoneHome;}
            set 
            { 
                _phoneHome = value;
            }
          
      }
    

      public String PhoneOffice
      {
        
            get { return _phoneOffice;}
            set 
            { 
                _phoneOffice = value;
            }
          
      }
    

      public String PhoneCell
      {
        
            get { return _phoneCell;}
            set 
            { 
                _phoneCell = value;
            }
          
      }
    

      public String PhoneAlt
      {
        
            get { return _phoneAlt;}
            set 
            { 
                _phoneAlt = value;
            }
          
      }
    

      public String EntityName
      {
        
            get { return _entityName;}
            set 
            { 
                _entityName = value;
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
    

      public String ContactPosition
      {
        
            get { return _contactPosition;}
            set 
            { 
                _contactPosition = value;
            }
          
      }
    

      public String TAXID
      {
        
            get { return _tAXID;}
            set 
            { 
                _tAXID = value;
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
    

      public int ParentID
      {
        
            get { return _parentID;}
            set 
            { 
                _parentID = value;
            }
          
      }
    

      public int? TypeId
      {
        
            get { return _typeId;}
            set 
            { 
                _typeId = value;
            }
          
      }
    


    }
  

    public partial class ParticipantDataMapper:TDataMapper<Participant,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public ParticipantDataMapper(){}
      public ParticipantDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Participant";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Participant] (
    DocID,DocRoleID,AsNamed,PhoneHome,PhoneOffice,PhoneCell,PhoneAlt,EntityName,FirstName,MiddleName,LastName,ContactPosition,TAXID,SSN,ParentID,TypeId) Values (
    
      @DocID,
      @DocRoleID,
      @AsNamed,
      @PhoneHome,
      @PhoneOffice,
      @PhoneCell,
      @PhoneAlt,
      @EntityName,
      @FirstName,
      @MiddleName,
      @LastName,
      @ContactPosition,
      @TAXID,
      @SSN,
      @ParentID,
      @TypeId);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Participant create( Participant participant )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(participant.DocID != null)
                      sqlCommand.Parameters.AddWithValue("@DocID", participant.DocID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocID", DBNull.Value);
                  
                    if(participant.DocRoleID != null)
                      sqlCommand.Parameters.AddWithValue("@DocRoleID", participant.DocRoleID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocRoleID", DBNull.Value);
                  
                    if(participant.AsNamed != null)
                      sqlCommand.Parameters.AddWithValue("@AsNamed", participant.AsNamed);
                    else
                      sqlCommand.Parameters.AddWithValue("@AsNamed", DBNull.Value);
                  
                    if(participant.PhoneHome != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneHome", participant.PhoneHome);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneHome", DBNull.Value);
                  
                    if(participant.PhoneOffice != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneOffice", participant.PhoneOffice);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneOffice", DBNull.Value);
                  
                    if(participant.PhoneCell != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneCell", participant.PhoneCell);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneCell", DBNull.Value);
                  
                    if(participant.PhoneAlt != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneAlt", participant.PhoneAlt);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneAlt", DBNull.Value);
                  
                    if(participant.EntityName != null)
                      sqlCommand.Parameters.AddWithValue("@EntityName", participant.EntityName);
                    else
                      sqlCommand.Parameters.AddWithValue("@EntityName", DBNull.Value);
                  
                    if(participant.FirstName != null)
                      sqlCommand.Parameters.AddWithValue("@FirstName", participant.FirstName);
                    else
                      sqlCommand.Parameters.AddWithValue("@FirstName", DBNull.Value);
                  
                    if(participant.MiddleName != null)
                      sqlCommand.Parameters.AddWithValue("@MiddleName", participant.MiddleName);
                    else
                      sqlCommand.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                  
                    if(participant.LastName != null)
                      sqlCommand.Parameters.AddWithValue("@LastName", participant.LastName);
                    else
                      sqlCommand.Parameters.AddWithValue("@LastName", DBNull.Value);
                  
                    if(participant.ContactPosition != null)
                      sqlCommand.Parameters.AddWithValue("@ContactPosition", participant.ContactPosition);
                    else
                      sqlCommand.Parameters.AddWithValue("@ContactPosition", DBNull.Value);
                  
                    if(participant.TAXID != null)
                      sqlCommand.Parameters.AddWithValue("@TAXID", participant.TAXID);
                    else
                      sqlCommand.Parameters.AddWithValue("@TAXID", DBNull.Value);
                  
                    if(participant.SSN != null)
                      sqlCommand.Parameters.AddWithValue("@SSN", participant.SSN);
                    else
                      sqlCommand.Parameters.AddWithValue("@SSN", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@ParentID", participant.ParentID);
                  
                    if(participant.TypeId != null)
                      sqlCommand.Parameters.AddWithValue("@TypeId", participant.TypeId);
                    else
                      sqlCommand.Parameters.AddWithValue("@TypeId", DBNull.Value);
                  participant.ParticipantID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(participant,DataMapperOperation.create);

      return registerRecord(participant);
    }

  

    private const String SqlSelectAll = @"Select
    ParticipantID,DocID,DocRoleID,AsNamed,PhoneHome,PhoneOffice,PhoneCell,PhoneAlt,EntityName,FirstName,MiddleName,LastName,ContactPosition,TAXID,SSN,ParentID,TypeId 
    From [Participant] ";
    
    public List<Participant> findAll(Object args)
    {
      List<Participant> rv = new List<Participant>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    ParticipantID,DocID,DocRoleID,AsNamed,PhoneHome,PhoneOffice,PhoneCell,PhoneAlt,EntityName,FirstName,MiddleName,LastName,ContactPosition,TAXID,SSN,ParentID,TypeId
     From [Participant]
    
       Where 
      ParticipantID = @ParticipantID
    ";

    public Participant findByPrimaryKey(
    int participantID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@ParticipantID", participantID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Participant not found, search by primary key");
 

    }


    public bool exists(Participant participant)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@ParticipantID", participant.ParticipantID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Participant doLoad(IDataReader dataReader)
    {
    Participant participant = new Participant();

    participant.ParticipantID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          participant.DocID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          participant.DocRoleID = dataReader.GetInt32(2);
        
          if(!dataReader.IsDBNull(3))
          participant.AsNamed = dataReader.GetString(3);
        
          if(!dataReader.IsDBNull(4))
          participant.PhoneHome = dataReader.GetString(4);
        
          if(!dataReader.IsDBNull(5))
          participant.PhoneOffice = dataReader.GetString(5);
        
          if(!dataReader.IsDBNull(6))
          participant.PhoneCell = dataReader.GetString(6);
        
          if(!dataReader.IsDBNull(7))
          participant.PhoneAlt = dataReader.GetString(7);
        
          if(!dataReader.IsDBNull(8))
          participant.EntityName = dataReader.GetString(8);
        
          if(!dataReader.IsDBNull(9))
          participant.FirstName = dataReader.GetString(9);
        
          if(!dataReader.IsDBNull(10))
          participant.MiddleName = dataReader.GetString(10);
        
          if(!dataReader.IsDBNull(11))
          participant.LastName = dataReader.GetString(11);
        
          if(!dataReader.IsDBNull(12))
          participant.ContactPosition = dataReader.GetString(12);
        
          if(!dataReader.IsDBNull(13))
          participant.TAXID = dataReader.GetString(13);
        
          if(!dataReader.IsDBNull(14))
          participant.SSN = dataReader.GetString(14);
        participant.ParentID = dataReader.GetInt32(15);
        
          if(!dataReader.IsDBNull(16))
          participant.TypeId = dataReader.GetInt32(16);
        participant.IsLoaded = true;
    
    return registerRecord(participant);
    }


    protected override Participant doLoad(Hashtable hashtable)
    {
      Participant participant = new Participant();

      
        if(hashtable.ContainsKey("ParticipantID"))
            participant.ParticipantID = ( int)hashtable["ParticipantID"];
      
        if(hashtable.ContainsKey("DocID"))
            participant.DocID = ( int)hashtable["DocID"];
      
        if(hashtable.ContainsKey("DocRoleID"))
            participant.DocRoleID = ( int)hashtable["DocRoleID"];
      
        if(hashtable.ContainsKey("AsNamed"))
            participant.AsNamed = ( String)hashtable["AsNamed"];
      
        if(hashtable.ContainsKey("PhoneHome"))
            participant.PhoneHome = ( String)hashtable["PhoneHome"];
      
        if(hashtable.ContainsKey("PhoneOffice"))
            participant.PhoneOffice = ( String)hashtable["PhoneOffice"];
      
        if(hashtable.ContainsKey("PhoneCell"))
            participant.PhoneCell = ( String)hashtable["PhoneCell"];
      
        if(hashtable.ContainsKey("PhoneAlt"))
            participant.PhoneAlt = ( String)hashtable["PhoneAlt"];
      
        if(hashtable.ContainsKey("EntityName"))
            participant.EntityName = ( String)hashtable["EntityName"];
      
        if(hashtable.ContainsKey("FirstName"))
            participant.FirstName = ( String)hashtable["FirstName"];
      
        if(hashtable.ContainsKey("MiddleName"))
            participant.MiddleName = ( String)hashtable["MiddleName"];
      
        if(hashtable.ContainsKey("LastName"))
            participant.LastName = ( String)hashtable["LastName"];
      
        if(hashtable.ContainsKey("ContactPosition"))
            participant.ContactPosition = ( String)hashtable["ContactPosition"];
      
        if(hashtable.ContainsKey("TAXID"))
            participant.TAXID = ( String)hashtable["TAXID"];
      
        if(hashtable.ContainsKey("SSN"))
            participant.SSN = ( String)hashtable["SSN"];
      
        if(hashtable.ContainsKey("ParentID"))
            participant.ParentID = ( int)hashtable["ParentID"];
      
        if(hashtable.ContainsKey("TypeId"))
            participant.TypeId = ( int)hashtable["TypeId"];
      

      return participant;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Participant]
    
      Where
      ParticipantID = @ParticipantID";
    [Synchronized]
    public Participant remove(Participant participant)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@ParticipantID", participant.ParticipantID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(participant,DataMapperOperation.delete);

      return registerRecord(participant);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Participant save( Participant participant )
    {
      if(exists(participant))
        return update(participant);
        return create(participant);
    }

  

    const String SqlUpdate = @"Update [Participant] Set 
    DocID = @DocID,DocRoleID = @DocRoleID,AsNamed = @AsNamed,PhoneHome = @PhoneHome,PhoneOffice = @PhoneOffice,PhoneCell = @PhoneCell,PhoneAlt = @PhoneAlt,EntityName = @EntityName,FirstName = @FirstName,MiddleName = @MiddleName,LastName = @LastName,ContactPosition = @ContactPosition,TAXID = @TAXID,SSN = @SSN,ParentID = @ParentID,TypeId = @TypeId
       Where 
      ParticipantID = @ParticipantID";
    
    
    [Synchronized]
    public Participant update(Participant participant)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", participant.ParticipantID);
                  
                    if(participant.DocID != null)
                      sqlCommand.Parameters.AddWithValue("@DocID", participant.DocID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocID", DBNull.Value);
                  
                    if(participant.DocRoleID != null)
                      sqlCommand.Parameters.AddWithValue("@DocRoleID", participant.DocRoleID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocRoleID", DBNull.Value);
                  
                    if(participant.AsNamed != null)
                      sqlCommand.Parameters.AddWithValue("@AsNamed", participant.AsNamed);
                    else
                      sqlCommand.Parameters.AddWithValue("@AsNamed", DBNull.Value);
                  
                    if(participant.PhoneHome != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneHome", participant.PhoneHome);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneHome", DBNull.Value);
                  
                    if(participant.PhoneOffice != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneOffice", participant.PhoneOffice);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneOffice", DBNull.Value);
                  
                    if(participant.PhoneCell != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneCell", participant.PhoneCell);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneCell", DBNull.Value);
                  
                    if(participant.PhoneAlt != null)
                      sqlCommand.Parameters.AddWithValue("@PhoneAlt", participant.PhoneAlt);
                    else
                      sqlCommand.Parameters.AddWithValue("@PhoneAlt", DBNull.Value);
                  
                    if(participant.EntityName != null)
                      sqlCommand.Parameters.AddWithValue("@EntityName", participant.EntityName);
                    else
                      sqlCommand.Parameters.AddWithValue("@EntityName", DBNull.Value);
                  
                    if(participant.FirstName != null)
                      sqlCommand.Parameters.AddWithValue("@FirstName", participant.FirstName);
                    else
                      sqlCommand.Parameters.AddWithValue("@FirstName", DBNull.Value);
                  
                    if(participant.MiddleName != null)
                      sqlCommand.Parameters.AddWithValue("@MiddleName", participant.MiddleName);
                    else
                      sqlCommand.Parameters.AddWithValue("@MiddleName", DBNull.Value);
                  
                    if(participant.LastName != null)
                      sqlCommand.Parameters.AddWithValue("@LastName", participant.LastName);
                    else
                      sqlCommand.Parameters.AddWithValue("@LastName", DBNull.Value);
                  
                    if(participant.ContactPosition != null)
                      sqlCommand.Parameters.AddWithValue("@ContactPosition", participant.ContactPosition);
                    else
                      sqlCommand.Parameters.AddWithValue("@ContactPosition", DBNull.Value);
                  
                    if(participant.TAXID != null)
                      sqlCommand.Parameters.AddWithValue("@TAXID", participant.TAXID);
                    else
                      sqlCommand.Parameters.AddWithValue("@TAXID", DBNull.Value);
                  
                    if(participant.SSN != null)
                      sqlCommand.Parameters.AddWithValue("@SSN", participant.SSN);
                    else
                      sqlCommand.Parameters.AddWithValue("@SSN", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@ParentID", participant.ParentID);
                  
                    if(participant.TypeId != null)
                      sqlCommand.Parameters.AddWithValue("@TypeId", participant.TypeId);
                    else
                      sqlCommand.Parameters.AddWithValue("@TypeId", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(participant,DataMapperOperation.update);

      return registerRecord(participant);
    }

  
    }
    
  
    
    public partial class Participantaddress: DomainObject
    {
    
      protected int _addressID;
    
      protected int? _participantlID;
    
      protected int? _addressTypeID;
    
      protected String _line1;
    
      protected String _line2;
    
      protected String _city;
    
      protected String _state;
    
      protected String _zip;
    
      protected String _incareof;
    

    public Participantaddress(){}

    public Participantaddress(
    int 
            addressID,int 
            participantlID,int 
            addressTypeID,String 
            line1,String 
            line2,String 
            city,String 
            state,String 
            zip,String 
            incareof
    )
    {
    AddressID = addressID;
    ParticipantlID = participantlID;
    AddressTypeID = addressTypeID;
    Line1 = line1;
    Line2 = line2;
    City = city;
    State = state;
    Zip = zip;
    Incareof = incareof;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Participantaddress."
    
      + AddressID.ToString()
    ;
    
    return uri;
    }

    

      public int AddressID
      {
        
            get { return _addressID;}
            set 
            { 
                _addressID = value;
            }
          
      }
    

      public int? ParticipantlID
      {
        
            get { return _participantlID;}
            set 
            { 
                _participantlID = value;
            }
          
      }
    

      public int? AddressTypeID
      {
        
            get { return _addressTypeID;}
            set 
            { 
                _addressTypeID = value;
            }
          
      }
    

      public String Line1
      {
        
            get { return _line1;}
            set 
            { 
                _line1 = value;
            }
          
      }
    

      public String Line2
      {
        
            get { return _line2;}
            set 
            { 
                _line2 = value;
            }
          
      }
    

      public String City
      {
        
            get { return _city;}
            set 
            { 
                _city = value;
            }
          
      }
    

      public String State
      {
        
            get { return _state;}
            set 
            { 
                _state = value;
            }
          
      }
    

      public String Zip
      {
        
            get { return _zip;}
            set 
            { 
                _zip = value;
            }
          
      }
    

      public String Incareof
      {
        
            get { return _incareof;}
            set 
            { 
                _incareof = value;
            }
          
      }
    


    }
  

    public partial class ParticipantaddressDataMapper:TDataMapper<Participantaddress,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public ParticipantaddressDataMapper(){}
      public ParticipantaddressDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Participantaddress";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Participantaddress] (
    ParticipantlID,AddressTypeID,Line1,Line2,City,State,Zip,Incareof) Values (
    
      @ParticipantlID,
      @AddressTypeID,
      @Line1,
      @Line2,
      @City,
      @State,
      @Zip,
      @Incareof);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Participantaddress create( Participantaddress participantaddress )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(participantaddress.ParticipantlID != null)
                      sqlCommand.Parameters.AddWithValue("@ParticipantlID", participantaddress.ParticipantlID);
                    else
                      sqlCommand.Parameters.AddWithValue("@ParticipantlID", DBNull.Value);
                  
                    if(participantaddress.AddressTypeID != null)
                      sqlCommand.Parameters.AddWithValue("@AddressTypeID", participantaddress.AddressTypeID);
                    else
                      sqlCommand.Parameters.AddWithValue("@AddressTypeID", DBNull.Value);
                  
                    if(participantaddress.Line1 != null)
                      sqlCommand.Parameters.AddWithValue("@Line1", participantaddress.Line1);
                    else
                      sqlCommand.Parameters.AddWithValue("@Line1", DBNull.Value);
                  
                    if(participantaddress.Line2 != null)
                      sqlCommand.Parameters.AddWithValue("@Line2", participantaddress.Line2);
                    else
                      sqlCommand.Parameters.AddWithValue("@Line2", DBNull.Value);
                  
                    if(participantaddress.City != null)
                      sqlCommand.Parameters.AddWithValue("@City", participantaddress.City);
                    else
                      sqlCommand.Parameters.AddWithValue("@City", DBNull.Value);
                  
                    if(participantaddress.State != null)
                      sqlCommand.Parameters.AddWithValue("@State", participantaddress.State);
                    else
                      sqlCommand.Parameters.AddWithValue("@State", DBNull.Value);
                  
                    if(participantaddress.Zip != null)
                      sqlCommand.Parameters.AddWithValue("@Zip", participantaddress.Zip);
                    else
                      sqlCommand.Parameters.AddWithValue("@Zip", DBNull.Value);
                  
                    if(participantaddress.Incareof != null)
                      sqlCommand.Parameters.AddWithValue("@Incareof", participantaddress.Incareof);
                    else
                      sqlCommand.Parameters.AddWithValue("@Incareof", DBNull.Value);
                  participantaddress.AddressID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(participantaddress,DataMapperOperation.create);

      return registerRecord(participantaddress);
    }

  

    private const String SqlSelectAll = @"Select
    AddressID,ParticipantlID,AddressTypeID,Line1,Line2,City,State,Zip,Incareof 
    From [Participantaddress] ";
    
    public List<Participantaddress> findAll(Object args)
    {
      List<Participantaddress> rv = new List<Participantaddress>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    AddressID,ParticipantlID,AddressTypeID,Line1,Line2,City,State,Zip,Incareof
     From [Participantaddress]
    
       Where 
      AddressID = @AddressID
    ";

    public Participantaddress findByPrimaryKey(
    int addressID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@AddressID", addressID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Participantaddress not found, search by primary key");
 

    }


    public bool exists(Participantaddress participantaddress)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@AddressID", participantaddress.AddressID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Participantaddress doLoad(IDataReader dataReader)
    {
    Participantaddress participantaddress = new Participantaddress();

    participantaddress.AddressID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          participantaddress.ParticipantlID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          participantaddress.AddressTypeID = dataReader.GetInt32(2);
        
          if(!dataReader.IsDBNull(3))
          participantaddress.Line1 = dataReader.GetString(3);
        
          if(!dataReader.IsDBNull(4))
          participantaddress.Line2 = dataReader.GetString(4);
        
          if(!dataReader.IsDBNull(5))
          participantaddress.City = dataReader.GetString(5);
        
          if(!dataReader.IsDBNull(6))
          participantaddress.State = dataReader.GetString(6);
        
          if(!dataReader.IsDBNull(7))
          participantaddress.Zip = dataReader.GetString(7);
        
          if(!dataReader.IsDBNull(8))
          participantaddress.Incareof = dataReader.GetString(8);
        participantaddress.IsLoaded = true;
    
    return registerRecord(participantaddress);
    }


    protected override Participantaddress doLoad(Hashtable hashtable)
    {
      Participantaddress participantaddress = new Participantaddress();

      
        if(hashtable.ContainsKey("AddressID"))
            participantaddress.AddressID = ( int)hashtable["AddressID"];
      
        if(hashtable.ContainsKey("ParticipantlID"))
            participantaddress.ParticipantlID = ( int)hashtable["ParticipantlID"];
      
        if(hashtable.ContainsKey("AddressTypeID"))
            participantaddress.AddressTypeID = ( int)hashtable["AddressTypeID"];
      
        if(hashtable.ContainsKey("Line1"))
            participantaddress.Line1 = ( String)hashtable["Line1"];
      
        if(hashtable.ContainsKey("Line2"))
            participantaddress.Line2 = ( String)hashtable["Line2"];
      
        if(hashtable.ContainsKey("City"))
            participantaddress.City = ( String)hashtable["City"];
      
        if(hashtable.ContainsKey("State"))
            participantaddress.State = ( String)hashtable["State"];
      
        if(hashtable.ContainsKey("Zip"))
            participantaddress.Zip = ( String)hashtable["Zip"];
      
        if(hashtable.ContainsKey("Incareof"))
            participantaddress.Incareof = ( String)hashtable["Incareof"];
      

      return participantaddress;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Participantaddress]
    
      Where
      AddressID = @AddressID";
    [Synchronized]
    public Participantaddress remove(Participantaddress participantaddress)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@AddressID", participantaddress.AddressID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(participantaddress,DataMapperOperation.delete);

      return registerRecord(participantaddress);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Participantaddress save( Participantaddress participantaddress )
    {
      if(exists(participantaddress))
        return update(participantaddress);
        return create(participantaddress);
    }

  

    const String SqlUpdate = @"Update [Participantaddress] Set 
    ParticipantlID = @ParticipantlID,AddressTypeID = @AddressTypeID,Line1 = @Line1,Line2 = @Line2,City = @City,State = @State,Zip = @Zip,Incareof = @Incareof
       Where 
      AddressID = @AddressID";
    
    
    [Synchronized]
    public Participantaddress update(Participantaddress participantaddress)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@AddressID", participantaddress.AddressID);
                  
                    if(participantaddress.ParticipantlID != null)
                      sqlCommand.Parameters.AddWithValue("@ParticipantlID", participantaddress.ParticipantlID);
                    else
                      sqlCommand.Parameters.AddWithValue("@ParticipantlID", DBNull.Value);
                  
                    if(participantaddress.AddressTypeID != null)
                      sqlCommand.Parameters.AddWithValue("@AddressTypeID", participantaddress.AddressTypeID);
                    else
                      sqlCommand.Parameters.AddWithValue("@AddressTypeID", DBNull.Value);
                  
                    if(participantaddress.Line1 != null)
                      sqlCommand.Parameters.AddWithValue("@Line1", participantaddress.Line1);
                    else
                      sqlCommand.Parameters.AddWithValue("@Line1", DBNull.Value);
                  
                    if(participantaddress.Line2 != null)
                      sqlCommand.Parameters.AddWithValue("@Line2", participantaddress.Line2);
                    else
                      sqlCommand.Parameters.AddWithValue("@Line2", DBNull.Value);
                  
                    if(participantaddress.City != null)
                      sqlCommand.Parameters.AddWithValue("@City", participantaddress.City);
                    else
                      sqlCommand.Parameters.AddWithValue("@City", DBNull.Value);
                  
                    if(participantaddress.State != null)
                      sqlCommand.Parameters.AddWithValue("@State", participantaddress.State);
                    else
                      sqlCommand.Parameters.AddWithValue("@State", DBNull.Value);
                  
                    if(participantaddress.Zip != null)
                      sqlCommand.Parameters.AddWithValue("@Zip", participantaddress.Zip);
                    else
                      sqlCommand.Parameters.AddWithValue("@Zip", DBNull.Value);
                  
                    if(participantaddress.Incareof != null)
                      sqlCommand.Parameters.AddWithValue("@Incareof", participantaddress.Incareof);
                    else
                      sqlCommand.Parameters.AddWithValue("@Incareof", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(participantaddress,DataMapperOperation.update);

      return registerRecord(participantaddress);
    }

  
    }
    
  
    
    public partial class Participantentityparty: DomainObject
    {
    
      protected int _participantEntityPartyID;
    
      protected int? _participantID;
    
      protected String _fName;
    
      protected String _mName;
    
      protected String _lName;
    
      protected String _sSN;
    

    public Participantentityparty(){}

    public Participantentityparty(
    int 
            participantEntityPartyID,int 
            participantID,String 
            fName,String 
            mName,String 
            lName,String 
            sSN
    )
    {
    ParticipantEntityPartyID = participantEntityPartyID;
    ParticipantID = participantID;
    fName = fName;
    mName = mName;
    lName = lName;
    SSN = sSN;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Participantentityparty."
    
      + ParticipantEntityPartyID.ToString()
    ;
    
    return uri;
    }

    

      public int ParticipantEntityPartyID
      {
        
            get { return _participantEntityPartyID;}
            set 
            { 
                _participantEntityPartyID = value;
            }
          
      }
    

      public int? ParticipantID
      {
        
            get { return _participantID;}
            set 
            { 
                _participantID = value;
            }
          
      }
    

      public String fName
      {
        
            get { return _fName;}
            set 
            { 
                _fName = value;
            }
          
      }
    

      public String mName
      {
        
            get { return _mName;}
            set 
            { 
                _mName = value;
            }
          
      }
    

      public String lName
      {
        
            get { return _lName;}
            set 
            { 
                _lName = value;
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
    


    }
  

    public partial class ParticipantentitypartyDataMapper:TDataMapper<Participantentityparty,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public ParticipantentitypartyDataMapper(){}
      public ParticipantentitypartyDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Participantentityparty";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Participantentityparty] (
    ParticipantID,fName,mName,lName,SSN) Values (
    
      @ParticipantID,
      @fName,
      @mName,
      @lName,
      @SSN);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Participantentityparty create( Participantentityparty participantentityparty )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(participantentityparty.ParticipantID != null)
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", participantentityparty.ParticipantID);
                    else
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", DBNull.Value);
                  
                    if(participantentityparty.fName != null)
                      sqlCommand.Parameters.AddWithValue("@fName", participantentityparty.fName);
                    else
                      sqlCommand.Parameters.AddWithValue("@fName", DBNull.Value);
                  
                    if(participantentityparty.mName != null)
                      sqlCommand.Parameters.AddWithValue("@mName", participantentityparty.mName);
                    else
                      sqlCommand.Parameters.AddWithValue("@mName", DBNull.Value);
                  
                    if(participantentityparty.lName != null)
                      sqlCommand.Parameters.AddWithValue("@lName", participantentityparty.lName);
                    else
                      sqlCommand.Parameters.AddWithValue("@lName", DBNull.Value);
                  
                    if(participantentityparty.SSN != null)
                      sqlCommand.Parameters.AddWithValue("@SSN", participantentityparty.SSN);
                    else
                      sqlCommand.Parameters.AddWithValue("@SSN", DBNull.Value);
                  participantentityparty.ParticipantEntityPartyID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(participantentityparty,DataMapperOperation.create);

      return registerRecord(participantentityparty);
    }

  

    private const String SqlSelectAll = @"Select
    ParticipantEntityPartyID,ParticipantID,fName,mName,lName,SSN 
    From [Participantentityparty] ";
    
    public List<Participantentityparty> findAll(Object args)
    {
      List<Participantentityparty> rv = new List<Participantentityparty>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    ParticipantEntityPartyID,ParticipantID,fName,mName,lName,SSN
     From [Participantentityparty]
    
       Where 
      ParticipantEntityPartyID = @ParticipantEntityPartyID
    ";

    public Participantentityparty findByPrimaryKey(
    int participantEntityPartyID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@ParticipantEntityPartyID", participantEntityPartyID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Participantentityparty not found, search by primary key");
 

    }


    public bool exists(Participantentityparty participantentityparty)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@ParticipantEntityPartyID", participantentityparty.ParticipantEntityPartyID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Participantentityparty doLoad(IDataReader dataReader)
    {
    Participantentityparty participantentityparty = new Participantentityparty();

    participantentityparty.ParticipantEntityPartyID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          participantentityparty.ParticipantID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          participantentityparty.fName = dataReader.GetString(2);
        
          if(!dataReader.IsDBNull(3))
          participantentityparty.mName = dataReader.GetString(3);
        
          if(!dataReader.IsDBNull(4))
          participantentityparty.lName = dataReader.GetString(4);
        
          if(!dataReader.IsDBNull(5))
          participantentityparty.SSN = dataReader.GetString(5);
        participantentityparty.IsLoaded = true;
    
    return registerRecord(participantentityparty);
    }


    protected override Participantentityparty doLoad(Hashtable hashtable)
    {
      Participantentityparty participantentityparty = new Participantentityparty();

      
        if(hashtable.ContainsKey("ParticipantEntityPartyID"))
            participantentityparty.ParticipantEntityPartyID = ( int)hashtable["ParticipantEntityPartyID"];
      
        if(hashtable.ContainsKey("ParticipantID"))
            participantentityparty.ParticipantID = ( int)hashtable["ParticipantID"];
      
        if(hashtable.ContainsKey("fName"))
            participantentityparty.fName = ( String)hashtable["fName"];
      
        if(hashtable.ContainsKey("mName"))
            participantentityparty.mName = ( String)hashtable["mName"];
      
        if(hashtable.ContainsKey("lName"))
            participantentityparty.lName = ( String)hashtable["lName"];
      
        if(hashtable.ContainsKey("SSN"))
            participantentityparty.SSN = ( String)hashtable["SSN"];
      

      return participantentityparty;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Participantentityparty]
    
      Where
      ParticipantEntityPartyID = @ParticipantEntityPartyID";
    [Synchronized]
    public Participantentityparty remove(Participantentityparty participantentityparty)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@ParticipantEntityPartyID", participantentityparty.ParticipantEntityPartyID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(participantentityparty,DataMapperOperation.delete);

      return registerRecord(participantentityparty);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Participantentityparty save( Participantentityparty participantentityparty )
    {
      if(exists(participantentityparty))
        return update(participantentityparty);
        return create(participantentityparty);
    }

  

    const String SqlUpdate = @"Update [Participantentityparty] Set 
    ParticipantID = @ParticipantID,fName = @fName,mName = @mName,lName = @lName,SSN = @SSN
       Where 
      ParticipantEntityPartyID = @ParticipantEntityPartyID";
    
    
    [Synchronized]
    public Participantentityparty update(Participantentityparty participantentityparty)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@ParticipantEntityPartyID", participantentityparty.ParticipantEntityPartyID);
                  
                    if(participantentityparty.ParticipantID != null)
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", participantentityparty.ParticipantID);
                    else
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", DBNull.Value);
                  
                    if(participantentityparty.fName != null)
                      sqlCommand.Parameters.AddWithValue("@fName", participantentityparty.fName);
                    else
                      sqlCommand.Parameters.AddWithValue("@fName", DBNull.Value);
                  
                    if(participantentityparty.mName != null)
                      sqlCommand.Parameters.AddWithValue("@mName", participantentityparty.mName);
                    else
                      sqlCommand.Parameters.AddWithValue("@mName", DBNull.Value);
                  
                    if(participantentityparty.lName != null)
                      sqlCommand.Parameters.AddWithValue("@lName", participantentityparty.lName);
                    else
                      sqlCommand.Parameters.AddWithValue("@lName", DBNull.Value);
                  
                    if(participantentityparty.SSN != null)
                      sqlCommand.Parameters.AddWithValue("@SSN", participantentityparty.SSN);
                    else
                      sqlCommand.Parameters.AddWithValue("@SSN", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(participantentityparty,DataMapperOperation.update);

      return registerRecord(participantentityparty);
    }

  
    }
    
  
    
    public partial class Participantreservation: DomainObject
    {
    
      protected int _docReservationID;
    
      protected int? _participantID;
    
      protected String _details;
    

    public Participantreservation(){}

    public Participantreservation(
    int 
            docReservationID,int 
            participantID,String 
            details
    )
    {
    DocReservationID = docReservationID;
    ParticipantID = participantID;
    Details = details;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Participantreservation."
    
      + DocReservationID.ToString()
    ;
    
    return uri;
    }

    

      public int DocReservationID
      {
        
            get { return _docReservationID;}
            set 
            { 
                _docReservationID = value;
            }
          
      }
    

      public int? ParticipantID
      {
        
            get { return _participantID;}
            set 
            { 
                _participantID = value;
            }
          
      }
    

      public String Details
      {
        
            get { return _details;}
            set 
            { 
                _details = value;
            }
          
      }
    


    }
  

    public partial class ParticipantreservationDataMapper:TDataMapper<Participantreservation,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public ParticipantreservationDataMapper(){}
      public ParticipantreservationDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Participantreservation";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Participantreservation] (
    ParticipantID,Details) Values (
    
      @ParticipantID,
      @Details);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Participantreservation create( Participantreservation participantreservation )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(participantreservation.ParticipantID != null)
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", participantreservation.ParticipantID);
                    else
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", DBNull.Value);
                  
                    if(participantreservation.Details != null)
                      sqlCommand.Parameters.AddWithValue("@Details", participantreservation.Details);
                    else
                      sqlCommand.Parameters.AddWithValue("@Details", DBNull.Value);
                  participantreservation.DocReservationID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(participantreservation,DataMapperOperation.create);

      return registerRecord(participantreservation);
    }

  

    private const String SqlSelectAll = @"Select
    DocReservationID,ParticipantID,Details 
    From [Participantreservation] ";
    
    public List<Participantreservation> findAll(Object args)
    {
      List<Participantreservation> rv = new List<Participantreservation>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    DocReservationID,ParticipantID,Details
     From [Participantreservation]
    
       Where 
      DocReservationID = @DocReservationID
    ";

    public Participantreservation findByPrimaryKey(
    int docReservationID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@DocReservationID", docReservationID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Participantreservation not found, search by primary key");
 

    }


    public bool exists(Participantreservation participantreservation)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@DocReservationID", participantreservation.DocReservationID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Participantreservation doLoad(IDataReader dataReader)
    {
    Participantreservation participantreservation = new Participantreservation();

    participantreservation.DocReservationID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          participantreservation.ParticipantID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          participantreservation.Details = dataReader.GetString(2);
        participantreservation.IsLoaded = true;
    
    return registerRecord(participantreservation);
    }


    protected override Participantreservation doLoad(Hashtable hashtable)
    {
      Participantreservation participantreservation = new Participantreservation();

      
        if(hashtable.ContainsKey("DocReservationID"))
            participantreservation.DocReservationID = ( int)hashtable["DocReservationID"];
      
        if(hashtable.ContainsKey("ParticipantID"))
            participantreservation.ParticipantID = ( int)hashtable["ParticipantID"];
      
        if(hashtable.ContainsKey("Details"))
            participantreservation.Details = ( String)hashtable["Details"];
      

      return participantreservation;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Participantreservation]
    
      Where
      DocReservationID = @DocReservationID";
    [Synchronized]
    public Participantreservation remove(Participantreservation participantreservation)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@DocReservationID", participantreservation.DocReservationID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(participantreservation,DataMapperOperation.delete);

      return registerRecord(participantreservation);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Participantreservation save( Participantreservation participantreservation )
    {
      if(exists(participantreservation))
        return update(participantreservation);
        return create(participantreservation);
    }

  

    const String SqlUpdate = @"Update [Participantreservation] Set 
    ParticipantID = @ParticipantID,Details = @Details
       Where 
      DocReservationID = @DocReservationID";
    
    
    [Synchronized]
    public Participantreservation update(Participantreservation participantreservation)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@DocReservationID", participantreservation.DocReservationID);
                  
                    if(participantreservation.ParticipantID != null)
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", participantreservation.ParticipantID);
                    else
                      sqlCommand.Parameters.AddWithValue("@ParticipantID", DBNull.Value);
                  
                    if(participantreservation.Details != null)
                      sqlCommand.Parameters.AddWithValue("@Details", participantreservation.Details);
                    else
                      sqlCommand.Parameters.AddWithValue("@Details", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(participantreservation,DataMapperOperation.update);

      return registerRecord(participantreservation);
    }

  
    }
    
  
    
    public partial class Participantrole: DomainObject
    {
    
      protected int _docRoleID;
    
      protected String _roleName;
    
      protected int? _docTypeID;
    
      protected bool _isSeller;
    

    public Participantrole(){}

    public Participantrole(
    int 
            docRoleID,String 
            roleName,int 
            docTypeID,bool 
            isSeller
    )
    {
    DocRoleID = docRoleID;
    RoleName = roleName;
    DocTypeID = docTypeID;
    IsSeller = isSeller;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Participantrole."
    
      + DocRoleID.ToString()
    ;
    
    return uri;
    }

    

      public int DocRoleID
      {
        
            get { return _docRoleID;}
            set 
            { 
                _docRoleID = value;
            }
          
      }
    

      public String RoleName
      {
        
            get { return _roleName;}
            set 
            { 
                _roleName = value;
            }
          
      }
    

      public int? DocTypeID
      {
        
            get { return _docTypeID;}
            set 
            { 
                _docTypeID = value;
            }
          
      }
    

      public bool IsSeller
      {
        
            get { return _isSeller;}
            set 
            { 
                _isSeller = value;
            }
          
      }
    


    }
  

    public partial class ParticipantroleDataMapper:TDataMapper<Participantrole,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public ParticipantroleDataMapper(){}
      public ParticipantroleDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Participantrole";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Participantrole] (
    RoleName,DocTypeID,IsSeller) Values (
    
      @RoleName,
      @DocTypeID,
      @IsSeller);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Participantrole create( Participantrole participantrole )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(participantrole.RoleName != null)
                      sqlCommand.Parameters.AddWithValue("@RoleName", participantrole.RoleName);
                    else
                      sqlCommand.Parameters.AddWithValue("@RoleName", DBNull.Value);
                  
                    if(participantrole.DocTypeID != null)
                      sqlCommand.Parameters.AddWithValue("@DocTypeID", participantrole.DocTypeID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocTypeID", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@IsSeller", participantrole.IsSeller);
                  participantrole.DocRoleID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(participantrole,DataMapperOperation.create);

      return registerRecord(participantrole);
    }

  

    private const String SqlSelectAll = @"Select
    DocRoleID,RoleName,DocTypeID,IsSeller 
    From [Participantrole] ";
    
    public List<Participantrole> findAll(Object args)
    {
      List<Participantrole> rv = new List<Participantrole>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    DocRoleID,RoleName,DocTypeID,IsSeller
     From [Participantrole]
    
       Where 
      DocRoleID = @DocRoleID
    ";

    public Participantrole findByPrimaryKey(
    int docRoleID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@DocRoleID", docRoleID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Participantrole not found, search by primary key");
 

    }


    public bool exists(Participantrole participantrole)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@DocRoleID", participantrole.DocRoleID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Participantrole doLoad(IDataReader dataReader)
    {
    Participantrole participantrole = new Participantrole();

    participantrole.DocRoleID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          participantrole.RoleName = dataReader.GetString(1);
        
          if(!dataReader.IsDBNull(2))
          participantrole.DocTypeID = dataReader.GetInt32(2);
        participantrole.IsSeller = dataReader.GetBoolean(3);
        participantrole.IsLoaded = true;
    
    return registerRecord(participantrole);
    }


    protected override Participantrole doLoad(Hashtable hashtable)
    {
      Participantrole participantrole = new Participantrole();

      
        if(hashtable.ContainsKey("DocRoleID"))
            participantrole.DocRoleID = ( int)hashtable["DocRoleID"];
      
        if(hashtable.ContainsKey("RoleName"))
            participantrole.RoleName = ( String)hashtable["RoleName"];
      
        if(hashtable.ContainsKey("DocTypeID"))
            participantrole.DocTypeID = ( int)hashtable["DocTypeID"];
      
        if(hashtable.ContainsKey("IsSeller"))
            participantrole.IsSeller = ( bool)hashtable["IsSeller"];
      

      return participantrole;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Participantrole]
    
      Where
      DocRoleID = @DocRoleID";
    [Synchronized]
    public Participantrole remove(Participantrole participantrole)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@DocRoleID", participantrole.DocRoleID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(participantrole,DataMapperOperation.delete);

      return registerRecord(participantrole);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Participantrole save( Participantrole participantrole )
    {
      if(exists(participantrole))
        return update(participantrole);
        return create(participantrole);
    }

  

    const String SqlUpdate = @"Update [Participantrole] Set 
    RoleName = @RoleName,DocTypeID = @DocTypeID,IsSeller = @IsSeller
       Where 
      DocRoleID = @DocRoleID";
    
    
    [Synchronized]
    public Participantrole update(Participantrole participantrole)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@DocRoleID", participantrole.DocRoleID);
                  
                    if(participantrole.RoleName != null)
                      sqlCommand.Parameters.AddWithValue("@RoleName", participantrole.RoleName);
                    else
                      sqlCommand.Parameters.AddWithValue("@RoleName", DBNull.Value);
                  
                    if(participantrole.DocTypeID != null)
                      sqlCommand.Parameters.AddWithValue("@DocTypeID", participantrole.DocTypeID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocTypeID", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@IsSeller", participantrole.IsSeller);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(participantrole,DataMapperOperation.update);

      return registerRecord(participantrole);
    }

  
    }
    
  
    
    public partial class Participanttype: DomainObject
    {
    
      protected int _typeID;
    
      protected String _name;
    

    public Participanttype(){}

    public Participanttype(
    int 
            typeID,String 
            name
    )
    {
    TypeID = typeID;
    Name = name;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Participanttype."
    
      + TypeID.ToString()
    ;
    
    return uri;
    }

    

      public int TypeID
      {
        
            get { return _typeID;}
            set 
            { 
                _typeID = value;
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
    


    }
  

    public partial class ParticipanttypeDataMapper:TDataMapper<Participanttype,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public ParticipanttypeDataMapper(){}
      public ParticipanttypeDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Participanttype";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Participanttype] (
    Name) Values (
    
      @Name);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Participanttype create( Participanttype participanttype )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                      sqlCommand.Parameters.AddWithValue("@Name", participanttype.Name);
                  participanttype.TypeID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(participanttype,DataMapperOperation.create);

      return registerRecord(participanttype);
    }

  

    private const String SqlSelectAll = @"Select
    TypeID,Name 
    From [Participanttype] ";
    
    public List<Participanttype> findAll(Object args)
    {
      List<Participanttype> rv = new List<Participanttype>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    TypeID,Name
     From [Participanttype]
    
       Where 
      TypeID = @TypeID
    ";

    public Participanttype findByPrimaryKey(
    int typeID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@TypeID", typeID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Participanttype not found, search by primary key");
 

    }


    public bool exists(Participanttype participanttype)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@TypeID", participanttype.TypeID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Participanttype doLoad(IDataReader dataReader)
    {
    Participanttype participanttype = new Participanttype();

    participanttype.TypeID = dataReader.GetInt32(0);
        participanttype.Name = dataReader.GetString(1);
        participanttype.IsLoaded = true;
    
    return registerRecord(participanttype);
    }


    protected override Participanttype doLoad(Hashtable hashtable)
    {
      Participanttype participanttype = new Participanttype();

      
        if(hashtable.ContainsKey("TypeID"))
            participanttype.TypeID = ( int)hashtable["TypeID"];
      
        if(hashtable.ContainsKey("Name"))
            participanttype.Name = ( String)hashtable["Name"];
      

      return participanttype;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Participanttype]
    
      Where
      TypeID = @TypeID";
    [Synchronized]
    public Participanttype remove(Participanttype participanttype)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@TypeID", participanttype.TypeID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(participanttype,DataMapperOperation.delete);

      return registerRecord(participanttype);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Participanttype save( Participanttype participanttype )
    {
      if(exists(participanttype))
        return update(participanttype);
        return create(participanttype);
    }

  

    const String SqlUpdate = @"Update [Participanttype] Set 
    Name = @Name
       Where 
      TypeID = @TypeID";
    
    
    [Synchronized]
    public Participanttype update(Participanttype participanttype)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@TypeID", participanttype.TypeID);
                  
                      sqlCommand.Parameters.AddWithValue("@Name", participanttype.Name);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(participanttype,DataMapperOperation.update);

      return registerRecord(participanttype);
    }

  
    }
    
  
    
    public partial class States: DomainObject
    {
    
      protected int? _oBJECTID;
    
      protected int _sTATE_ID;
    
      protected String _sTATE_NAME;
    
      protected String _sTATE_FIPS;
    
      protected String _sUB_REGION;
    
      protected String _sTATE_ABBR;
    
      protected decimal? _pOP2000;
    
      protected decimal? _pOP2004;
    
      protected decimal? _pOP00_SQMI;
    
      protected decimal? _pOP04_SQMI;
    
      protected decimal? _wHITE;
    
      protected decimal? _bLACK;
    
      protected decimal? _aMERI_ES;
    
      protected decimal? _aSIAN;
    
      protected decimal? _hAWN_PI;
    
      protected decimal? _oTHER;
    
      protected decimal? _mULT_RACE;
    
      protected decimal? _hISPANIC;
    
      protected decimal? _mALES;
    
      protected decimal? _fEMALES;
    
      protected decimal? _aGE_UNDER5;
    
      protected decimal? _aGE_5_17;
    
      protected decimal? _aGE_18_21;
    
      protected decimal? _aGE_22_29;
    
      protected decimal? _aGE_30_39;
    
      protected decimal? _aGE_40_49;
    
      protected decimal? _aGE_50_64;
    
      protected decimal? _aGE_65_UP;
    
      protected decimal? _mED_AGE;
    
      protected decimal? _mED_AGE_M;
    
      protected decimal? _mED_AGE_F;
    
      protected decimal? _hOUSEHOLDS;
    
      protected decimal? _aVE_HH_SZ;
    
      protected decimal? _hSEHLD_1_M;
    
      protected decimal? _hSEHLD_1_F;
    
      protected decimal? _mARHH_CHD;
    
      protected decimal? _mARHH_NO_C;
    
      protected decimal? _mHH_CHILD;
    
      protected decimal? _fHH_CHILD;
    
      protected decimal? _fAMILIES;
    
      protected decimal? _aVE_FAM_SZ;
    
      protected decimal? _hSE_UNITS;
    
      protected decimal? _vACANT;
    
      protected decimal? _oWNER_OCC;
    
      protected decimal? _rENTER_OCC;
    
      protected decimal? _nO_FARMS97;
    
      protected decimal? _aVG_SIZE97;
    
      protected decimal? _cROP_ACR97;
    
      protected decimal? _aVG_SALE97;
    
      protected decimal? _sQMI;
    
      protected decimal? _shape_Leng;
    
      protected decimal? _shape_Area;
    

    public States(){}

    public States(
    int 
            oBJECTID,int 
            sTATE_ID,String 
            sTATE_NAME,String 
            sTATE_FIPS,String 
            sUB_REGION,String 
            sTATE_ABBR,decimal 
            pOP2000,decimal 
            pOP2004,decimal 
            pOP00_SQMI,decimal 
            pOP04_SQMI,decimal 
            wHITE,decimal 
            bLACK,decimal 
            aMERI_ES,decimal 
            aSIAN,decimal 
            hAWN_PI,decimal 
            oTHER,decimal 
            mULT_RACE,decimal 
            hISPANIC,decimal 
            mALES,decimal 
            fEMALES,decimal 
            aGE_UNDER5,decimal 
            aGE_5_17,decimal 
            aGE_18_21,decimal 
            aGE_22_29,decimal 
            aGE_30_39,decimal 
            aGE_40_49,decimal 
            aGE_50_64,decimal 
            aGE_65_UP,decimal 
            mED_AGE,decimal 
            mED_AGE_M,decimal 
            mED_AGE_F,decimal 
            hOUSEHOLDS,decimal 
            aVE_HH_SZ,decimal 
            hSEHLD_1_M,decimal 
            hSEHLD_1_F,decimal 
            mARHH_CHD,decimal 
            mARHH_NO_C,decimal 
            mHH_CHILD,decimal 
            fHH_CHILD,decimal 
            fAMILIES,decimal 
            aVE_FAM_SZ,decimal 
            hSE_UNITS,decimal 
            vACANT,decimal 
            oWNER_OCC,decimal 
            rENTER_OCC,decimal 
            nO_FARMS97,decimal 
            aVG_SIZE97,decimal 
            cROP_ACR97,decimal 
            aVG_SALE97,decimal 
            sQMI,decimal 
            shape_Leng,decimal 
            shape_Area
    )
    {
    OBJECTID = oBJECTID;
    STATE_ID = sTATE_ID;
    STATE_NAME = sTATE_NAME;
    STATE_FIPS = sTATE_FIPS;
    SUB_REGION = sUB_REGION;
    STATE_ABBR = sTATE_ABBR;
    POP2000 = pOP2000;
    POP2004 = pOP2004;
    POP00_SQMI = pOP00_SQMI;
    POP04_SQMI = pOP04_SQMI;
    WHITE = wHITE;
    BLACK = bLACK;
    AMERI_ES = aMERI_ES;
    ASIAN = aSIAN;
    HAWN_PI = hAWN_PI;
    OTHER = oTHER;
    MULT_RACE = mULT_RACE;
    HISPANIC = hISPANIC;
    MALES = mALES;
    FEMALES = fEMALES;
    AGE_UNDER5 = aGE_UNDER5;
    AGE_5_17 = aGE_5_17;
    AGE_18_21 = aGE_18_21;
    AGE_22_29 = aGE_22_29;
    AGE_30_39 = aGE_30_39;
    AGE_40_49 = aGE_40_49;
    AGE_50_64 = aGE_50_64;
    AGE_65_UP = aGE_65_UP;
    MED_AGE = mED_AGE;
    MED_AGE_M = mED_AGE_M;
    MED_AGE_F = mED_AGE_F;
    HOUSEHOLDS = hOUSEHOLDS;
    AVE_HH_SZ = aVE_HH_SZ;
    HSEHLD_1_M = hSEHLD_1_M;
    HSEHLD_1_F = hSEHLD_1_F;
    MARHH_CHD = mARHH_CHD;
    MARHH_NO_C = mARHH_NO_C;
    MHH_CHILD = mHH_CHILD;
    FHH_CHILD = fHH_CHILD;
    FAMILIES = fAMILIES;
    AVE_FAM_SZ = aVE_FAM_SZ;
    HSE_UNITS = hSE_UNITS;
    VACANT = vACANT;
    OWNER_OCC = oWNER_OCC;
    RENTER_OCC = rENTER_OCC;
    NO_FARMS97 = nO_FARMS97;
    AVG_SIZE97 = aVG_SIZE97;
    CROP_ACR97 = cROP_ACR97;
    AVG_SALE97 = aVG_SALE97;
    SQMI = sQMI;
    Shape_Leng = shape_Leng;
    Shape_Area = shape_Area;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.States."
    
      + STATE_ID.ToString()
    ;
    
    return uri;
    }

    

      public int? OBJECTID
      {
        
            get { return _oBJECTID;}
            set 
            { 
                _oBJECTID = value;
            }
          
      }
    

      public int STATE_ID
      {
        
            get { return _sTATE_ID;}
            set 
            { 
                _sTATE_ID = value;
            }
          
      }
    

      public String STATE_NAME
      {
        
            get { return _sTATE_NAME;}
            set 
            { 
                _sTATE_NAME = value;
            }
          
      }
    

      public String STATE_FIPS
      {
        
            get { return _sTATE_FIPS;}
            set 
            { 
                _sTATE_FIPS = value;
            }
          
      }
    

      public String SUB_REGION
      {
        
            get { return _sUB_REGION;}
            set 
            { 
                _sUB_REGION = value;
            }
          
      }
    

      public String STATE_ABBR
      {
        
            get { return _sTATE_ABBR;}
            set 
            { 
                _sTATE_ABBR = value;
            }
          
      }
    

      public decimal? POP2000
      {
        
            get { return _pOP2000;}
            set 
            { 
                _pOP2000 = value;
            }
          
      }
    

      public decimal? POP2004
      {
        
            get { return _pOP2004;}
            set 
            { 
                _pOP2004 = value;
            }
          
      }
    

      public decimal? POP00_SQMI
      {
        
            get { return _pOP00_SQMI;}
            set 
            { 
                _pOP00_SQMI = value;
            }
          
      }
    

      public decimal? POP04_SQMI
      {
        
            get { return _pOP04_SQMI;}
            set 
            { 
                _pOP04_SQMI = value;
            }
          
      }
    

      public decimal? WHITE
      {
        
            get { return _wHITE;}
            set 
            { 
                _wHITE = value;
            }
          
      }
    

      public decimal? BLACK
      {
        
            get { return _bLACK;}
            set 
            { 
                _bLACK = value;
            }
          
      }
    

      public decimal? AMERI_ES
      {
        
            get { return _aMERI_ES;}
            set 
            { 
                _aMERI_ES = value;
            }
          
      }
    

      public decimal? ASIAN
      {
        
            get { return _aSIAN;}
            set 
            { 
                _aSIAN = value;
            }
          
      }
    

      public decimal? HAWN_PI
      {
        
            get { return _hAWN_PI;}
            set 
            { 
                _hAWN_PI = value;
            }
          
      }
    

      public decimal? OTHER
      {
        
            get { return _oTHER;}
            set 
            { 
                _oTHER = value;
            }
          
      }
    

      public decimal? MULT_RACE
      {
        
            get { return _mULT_RACE;}
            set 
            { 
                _mULT_RACE = value;
            }
          
      }
    

      public decimal? HISPANIC
      {
        
            get { return _hISPANIC;}
            set 
            { 
                _hISPANIC = value;
            }
          
      }
    

      public decimal? MALES
      {
        
            get { return _mALES;}
            set 
            { 
                _mALES = value;
            }
          
      }
    

      public decimal? FEMALES
      {
        
            get { return _fEMALES;}
            set 
            { 
                _fEMALES = value;
            }
          
      }
    

      public decimal? AGE_UNDER5
      {
        
            get { return _aGE_UNDER5;}
            set 
            { 
                _aGE_UNDER5 = value;
            }
          
      }
    

      public decimal? AGE_5_17
      {
        
            get { return _aGE_5_17;}
            set 
            { 
                _aGE_5_17 = value;
            }
          
      }
    

      public decimal? AGE_18_21
      {
        
            get { return _aGE_18_21;}
            set 
            { 
                _aGE_18_21 = value;
            }
          
      }
    

      public decimal? AGE_22_29
      {
        
            get { return _aGE_22_29;}
            set 
            { 
                _aGE_22_29 = value;
            }
          
      }
    

      public decimal? AGE_30_39
      {
        
            get { return _aGE_30_39;}
            set 
            { 
                _aGE_30_39 = value;
            }
          
      }
    

      public decimal? AGE_40_49
      {
        
            get { return _aGE_40_49;}
            set 
            { 
                _aGE_40_49 = value;
            }
          
      }
    

      public decimal? AGE_50_64
      {
        
            get { return _aGE_50_64;}
            set 
            { 
                _aGE_50_64 = value;
            }
          
      }
    

      public decimal? AGE_65_UP
      {
        
            get { return _aGE_65_UP;}
            set 
            { 
                _aGE_65_UP = value;
            }
          
      }
    

      public decimal? MED_AGE
      {
        
            get { return _mED_AGE;}
            set 
            { 
                _mED_AGE = value;
            }
          
      }
    

      public decimal? MED_AGE_M
      {
        
            get { return _mED_AGE_M;}
            set 
            { 
                _mED_AGE_M = value;
            }
          
      }
    

      public decimal? MED_AGE_F
      {
        
            get { return _mED_AGE_F;}
            set 
            { 
                _mED_AGE_F = value;
            }
          
      }
    

      public decimal? HOUSEHOLDS
      {
        
            get { return _hOUSEHOLDS;}
            set 
            { 
                _hOUSEHOLDS = value;
            }
          
      }
    

      public decimal? AVE_HH_SZ
      {
        
            get { return _aVE_HH_SZ;}
            set 
            { 
                _aVE_HH_SZ = value;
            }
          
      }
    

      public decimal? HSEHLD_1_M
      {
        
            get { return _hSEHLD_1_M;}
            set 
            { 
                _hSEHLD_1_M = value;
            }
          
      }
    

      public decimal? HSEHLD_1_F
      {
        
            get { return _hSEHLD_1_F;}
            set 
            { 
                _hSEHLD_1_F = value;
            }
          
      }
    

      public decimal? MARHH_CHD
      {
        
            get { return _mARHH_CHD;}
            set 
            { 
                _mARHH_CHD = value;
            }
          
      }
    

      public decimal? MARHH_NO_C
      {
        
            get { return _mARHH_NO_C;}
            set 
            { 
                _mARHH_NO_C = value;
            }
          
      }
    

      public decimal? MHH_CHILD
      {
        
            get { return _mHH_CHILD;}
            set 
            { 
                _mHH_CHILD = value;
            }
          
      }
    

      public decimal? FHH_CHILD
      {
        
            get { return _fHH_CHILD;}
            set 
            { 
                _fHH_CHILD = value;
            }
          
      }
    

      public decimal? FAMILIES
      {
        
            get { return _fAMILIES;}
            set 
            { 
                _fAMILIES = value;
            }
          
      }
    

      public decimal? AVE_FAM_SZ
      {
        
            get { return _aVE_FAM_SZ;}
            set 
            { 
                _aVE_FAM_SZ = value;
            }
          
      }
    

      public decimal? HSE_UNITS
      {
        
            get { return _hSE_UNITS;}
            set 
            { 
                _hSE_UNITS = value;
            }
          
      }
    

      public decimal? VACANT
      {
        
            get { return _vACANT;}
            set 
            { 
                _vACANT = value;
            }
          
      }
    

      public decimal? OWNER_OCC
      {
        
            get { return _oWNER_OCC;}
            set 
            { 
                _oWNER_OCC = value;
            }
          
      }
    

      public decimal? RENTER_OCC
      {
        
            get { return _rENTER_OCC;}
            set 
            { 
                _rENTER_OCC = value;
            }
          
      }
    

      public decimal? NO_FARMS97
      {
        
            get { return _nO_FARMS97;}
            set 
            { 
                _nO_FARMS97 = value;
            }
          
      }
    

      public decimal? AVG_SIZE97
      {
        
            get { return _aVG_SIZE97;}
            set 
            { 
                _aVG_SIZE97 = value;
            }
          
      }
    

      public decimal? CROP_ACR97
      {
        
            get { return _cROP_ACR97;}
            set 
            { 
                _cROP_ACR97 = value;
            }
          
      }
    

      public decimal? AVG_SALE97
      {
        
            get { return _aVG_SALE97;}
            set 
            { 
                _aVG_SALE97 = value;
            }
          
      }
    

      public decimal? SQMI
      {
        
            get { return _sQMI;}
            set 
            { 
                _sQMI = value;
            }
          
      }
    

      public decimal? Shape_Leng
      {
        
            get { return _shape_Leng;}
            set 
            { 
                _shape_Leng = value;
            }
          
      }
    

      public decimal? Shape_Area
      {
        
            get { return _shape_Area;}
            set 
            { 
                _shape_Area = value;
            }
          
      }
    
          // one to many relation
          private List<Countys> _childCountysList;

          public List<Countys> childsCountys
          {
          get { return _childCountysList;}
          set { _childCountysList = value; }
          }
        


    }
  

    public partial class StatesDataMapper:TDataMapper<States,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public StatesDataMapper(){}
      public StatesDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "States";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [States] (
    OBJECTID,STATE_ID,STATE_NAME,STATE_FIPS,SUB_REGION,STATE_ABBR,POP2000,POP2004,POP00_SQMI,POP04_SQMI,WHITE,BLACK,AMERI_ES,ASIAN,HAWN_PI,OTHER,MULT_RACE,HISPANIC,MALES,FEMALES,AGE_UNDER5,AGE_5_17,AGE_18_21,AGE_22_29,AGE_30_39,AGE_40_49,AGE_50_64,AGE_65_UP,MED_AGE,MED_AGE_M,MED_AGE_F,HOUSEHOLDS,AVE_HH_SZ,HSEHLD_1_M,HSEHLD_1_F,MARHH_CHD,MARHH_NO_C,MHH_CHILD,FHH_CHILD,FAMILIES,AVE_FAM_SZ,HSE_UNITS,VACANT,OWNER_OCC,RENTER_OCC,NO_FARMS97,AVG_SIZE97,CROP_ACR97,AVG_SALE97,SQMI,Shape_Leng,Shape_Area) Values (
    
      @OBJECTID,
      @STATE_ID,
      @STATE_NAME,
      @STATE_FIPS,
      @SUB_REGION,
      @STATE_ABBR,
      @POP2000,
      @POP2004,
      @POP00_SQMI,
      @POP04_SQMI,
      @WHITE,
      @BLACK,
      @AMERI_ES,
      @ASIAN,
      @HAWN_PI,
      @OTHER,
      @MULT_RACE,
      @HISPANIC,
      @MALES,
      @FEMALES,
      @AGE_UNDER5,
      @AGE_5_17,
      @AGE_18_21,
      @AGE_22_29,
      @AGE_30_39,
      @AGE_40_49,
      @AGE_50_64,
      @AGE_65_UP,
      @MED_AGE,
      @MED_AGE_M,
      @MED_AGE_F,
      @HOUSEHOLDS,
      @AVE_HH_SZ,
      @HSEHLD_1_M,
      @HSEHLD_1_F,
      @MARHH_CHD,
      @MARHH_NO_C,
      @MHH_CHILD,
      @FHH_CHILD,
      @FAMILIES,
      @AVE_FAM_SZ,
      @HSE_UNITS,
      @VACANT,
      @OWNER_OCC,
      @RENTER_OCC,
      @NO_FARMS97,
      @AVG_SIZE97,
      @CROP_ACR97,
      @AVG_SALE97,
      @SQMI,
      @Shape_Leng,
      @Shape_Area);
    ";
    
    [TransactionRequired]
    [Synchronized]
    public override States create( States states )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(states.OBJECTID != null)
                      sqlCommand.Parameters.AddWithValue("@OBJECTID", states.OBJECTID);
                    else
                      sqlCommand.Parameters.AddWithValue("@OBJECTID", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@STATE_ID", states.STATE_ID);
                  
                    if(states.STATE_NAME != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", states.STATE_NAME);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", DBNull.Value);
                  
                    if(states.STATE_FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", states.STATE_FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", DBNull.Value);
                  
                    if(states.SUB_REGION != null)
                      sqlCommand.Parameters.AddWithValue("@SUB_REGION", states.SUB_REGION);
                    else
                      sqlCommand.Parameters.AddWithValue("@SUB_REGION", DBNull.Value);
                  
                    if(states.STATE_ABBR != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_ABBR", states.STATE_ABBR);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_ABBR", DBNull.Value);
                  
                    if(states.POP2000 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2000", states.POP2000);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2000", DBNull.Value);
                  
                    if(states.POP2004 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2004", states.POP2004);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2004", DBNull.Value);
                  
                    if(states.POP00_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", states.POP00_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", DBNull.Value);
                  
                    if(states.POP04_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", states.POP04_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", DBNull.Value);
                  
                    if(states.WHITE != null)
                      sqlCommand.Parameters.AddWithValue("@WHITE", states.WHITE);
                    else
                      sqlCommand.Parameters.AddWithValue("@WHITE", DBNull.Value);
                  
                    if(states.BLACK != null)
                      sqlCommand.Parameters.AddWithValue("@BLACK", states.BLACK);
                    else
                      sqlCommand.Parameters.AddWithValue("@BLACK", DBNull.Value);
                  
                    if(states.AMERI_ES != null)
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", states.AMERI_ES);
                    else
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", DBNull.Value);
                  
                    if(states.ASIAN != null)
                      sqlCommand.Parameters.AddWithValue("@ASIAN", states.ASIAN);
                    else
                      sqlCommand.Parameters.AddWithValue("@ASIAN", DBNull.Value);
                  
                    if(states.HAWN_PI != null)
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", states.HAWN_PI);
                    else
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", DBNull.Value);
                  
                    if(states.OTHER != null)
                      sqlCommand.Parameters.AddWithValue("@OTHER", states.OTHER);
                    else
                      sqlCommand.Parameters.AddWithValue("@OTHER", DBNull.Value);
                  
                    if(states.MULT_RACE != null)
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", states.MULT_RACE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", DBNull.Value);
                  
                    if(states.HISPANIC != null)
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", states.HISPANIC);
                    else
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", DBNull.Value);
                  
                    if(states.MALES != null)
                      sqlCommand.Parameters.AddWithValue("@MALES", states.MALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@MALES", DBNull.Value);
                  
                    if(states.FEMALES != null)
                      sqlCommand.Parameters.AddWithValue("@FEMALES", states.FEMALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FEMALES", DBNull.Value);
                  
                    if(states.AGE_UNDER5 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", states.AGE_UNDER5);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", DBNull.Value);
                  
                    if(states.AGE_5_17 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", states.AGE_5_17);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", DBNull.Value);
                  
                    if(states.AGE_18_21 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", states.AGE_18_21);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", DBNull.Value);
                  
                    if(states.AGE_22_29 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", states.AGE_22_29);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", DBNull.Value);
                  
                    if(states.AGE_30_39 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", states.AGE_30_39);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", DBNull.Value);
                  
                    if(states.AGE_40_49 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", states.AGE_40_49);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", DBNull.Value);
                  
                    if(states.AGE_50_64 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", states.AGE_50_64);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", DBNull.Value);
                  
                    if(states.AGE_65_UP != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", states.AGE_65_UP);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", DBNull.Value);
                  
                    if(states.MED_AGE != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", states.MED_AGE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", DBNull.Value);
                  
                    if(states.MED_AGE_M != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", states.MED_AGE_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", DBNull.Value);
                  
                    if(states.MED_AGE_F != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", states.MED_AGE_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", DBNull.Value);
                  
                    if(states.HOUSEHOLDS != null)
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", states.HOUSEHOLDS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", DBNull.Value);
                  
                    if(states.AVE_HH_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", states.AVE_HH_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", DBNull.Value);
                  
                    if(states.HSEHLD_1_M != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", states.HSEHLD_1_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", DBNull.Value);
                  
                    if(states.HSEHLD_1_F != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", states.HSEHLD_1_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", DBNull.Value);
                  
                    if(states.MARHH_CHD != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", states.MARHH_CHD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", DBNull.Value);
                  
                    if(states.MARHH_NO_C != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", states.MARHH_NO_C);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", DBNull.Value);
                  
                    if(states.MHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", states.MHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", DBNull.Value);
                  
                    if(states.FHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", states.FHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", DBNull.Value);
                  
                    if(states.FAMILIES != null)
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", states.FAMILIES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", DBNull.Value);
                  
                    if(states.AVE_FAM_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", states.AVE_FAM_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", DBNull.Value);
                  
                    if(states.HSE_UNITS != null)
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", states.HSE_UNITS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", DBNull.Value);
                  
                    if(states.VACANT != null)
                      sqlCommand.Parameters.AddWithValue("@VACANT", states.VACANT);
                    else
                      sqlCommand.Parameters.AddWithValue("@VACANT", DBNull.Value);
                  
                    if(states.OWNER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", states.OWNER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", DBNull.Value);
                  
                    if(states.RENTER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", states.RENTER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", DBNull.Value);
                  
                    if(states.NO_FARMS97 != null)
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", states.NO_FARMS97);
                    else
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", DBNull.Value);
                  
                    if(states.AVG_SIZE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", states.AVG_SIZE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", DBNull.Value);
                  
                    if(states.CROP_ACR97 != null)
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", states.CROP_ACR97);
                    else
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", DBNull.Value);
                  
                    if(states.AVG_SALE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", states.AVG_SALE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", DBNull.Value);
                  
                    if(states.SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@SQMI", states.SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@SQMI", DBNull.Value);
                  
                    if(states.Shape_Leng != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", states.Shape_Leng);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", DBNull.Value);
                  
                    if(states.Shape_Area != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", states.Shape_Area);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", DBNull.Value);
                  
                  sqlCommand.ExecuteNonQuery();
                
        }
      }
      
    
          if(states.childsCountys != null 
            && states.childsCountys.Count > 0)
          {
            CountysDataMapper dataMapper = new CountysDataMapper(Database);
            
            foreach(Countys item in states.childsCountys)
              dataMapper.create(item);
          }
        
      
      raiseAffected(states,DataMapperOperation.create);

      return registerRecord(states);
    }

  

    private const String SqlSelectAll = @"Select
    OBJECTID,STATE_ID,STATE_NAME,STATE_FIPS,SUB_REGION,STATE_ABBR,POP2000,POP2004,POP00_SQMI,POP04_SQMI,WHITE,BLACK,AMERI_ES,ASIAN,HAWN_PI,OTHER,MULT_RACE,HISPANIC,MALES,FEMALES,AGE_UNDER5,AGE_5_17,AGE_18_21,AGE_22_29,AGE_30_39,AGE_40_49,AGE_50_64,AGE_65_UP,MED_AGE,MED_AGE_M,MED_AGE_F,HOUSEHOLDS,AVE_HH_SZ,HSEHLD_1_M,HSEHLD_1_F,MARHH_CHD,MARHH_NO_C,MHH_CHILD,FHH_CHILD,FAMILIES,AVE_FAM_SZ,HSE_UNITS,VACANT,OWNER_OCC,RENTER_OCC,NO_FARMS97,AVG_SIZE97,CROP_ACR97,AVG_SALE97,SQMI,Shape_Leng,Shape_Area 
    From [States] ";
    
    public List<States> findAll(Object args)
    {
      List<States> rv = new List<States>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    OBJECTID,STATE_ID,STATE_NAME,STATE_FIPS,SUB_REGION,STATE_ABBR,POP2000,POP2004,POP00_SQMI,POP04_SQMI,WHITE,BLACK,AMERI_ES,ASIAN,HAWN_PI,OTHER,MULT_RACE,HISPANIC,MALES,FEMALES,AGE_UNDER5,AGE_5_17,AGE_18_21,AGE_22_29,AGE_30_39,AGE_40_49,AGE_50_64,AGE_65_UP,MED_AGE,MED_AGE_M,MED_AGE_F,HOUSEHOLDS,AVE_HH_SZ,HSEHLD_1_M,HSEHLD_1_F,MARHH_CHD,MARHH_NO_C,MHH_CHILD,FHH_CHILD,FAMILIES,AVE_FAM_SZ,HSE_UNITS,VACANT,OWNER_OCC,RENTER_OCC,NO_FARMS97,AVG_SIZE97,CROP_ACR97,AVG_SALE97,SQMI,Shape_Leng,Shape_Area
     From [States]
    
       Where 
      STATE_ID = @STATE_ID
    ";

    public States findByPrimaryKey(
    int sTATE_ID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@STATE_ID", sTATE_ID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("States not found, search by primary key");
 

    }


    public bool exists(States states)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@STATE_ID", states.STATE_ID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override States doLoad(IDataReader dataReader)
    {
    States states = new States();

    
          if(!dataReader.IsDBNull(0))
          states.OBJECTID = dataReader.GetInt32(0);
        states.STATE_ID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          states.STATE_NAME = dataReader.GetString(2);
        
          if(!dataReader.IsDBNull(3))
          states.STATE_FIPS = dataReader.GetString(3);
        
          if(!dataReader.IsDBNull(4))
          states.SUB_REGION = dataReader.GetString(4);
        
          if(!dataReader.IsDBNull(5))
          states.STATE_ABBR = dataReader.GetString(5);
        
          if(!dataReader.IsDBNull(6))
          states.POP2000 = dataReader.GetDecimal(6);
        
          if(!dataReader.IsDBNull(7))
          states.POP2004 = dataReader.GetDecimal(7);
        
          if(!dataReader.IsDBNull(8))
          states.POP00_SQMI = dataReader.GetDecimal(8);
        
          if(!dataReader.IsDBNull(9))
          states.POP04_SQMI = dataReader.GetDecimal(9);
        
          if(!dataReader.IsDBNull(10))
          states.WHITE = dataReader.GetDecimal(10);
        
          if(!dataReader.IsDBNull(11))
          states.BLACK = dataReader.GetDecimal(11);
        
          if(!dataReader.IsDBNull(12))
          states.AMERI_ES = dataReader.GetDecimal(12);
        
          if(!dataReader.IsDBNull(13))
          states.ASIAN = dataReader.GetDecimal(13);
        
          if(!dataReader.IsDBNull(14))
          states.HAWN_PI = dataReader.GetDecimal(14);
        
          if(!dataReader.IsDBNull(15))
          states.OTHER = dataReader.GetDecimal(15);
        
          if(!dataReader.IsDBNull(16))
          states.MULT_RACE = dataReader.GetDecimal(16);
        
          if(!dataReader.IsDBNull(17))
          states.HISPANIC = dataReader.GetDecimal(17);
        
          if(!dataReader.IsDBNull(18))
          states.MALES = dataReader.GetDecimal(18);
        
          if(!dataReader.IsDBNull(19))
          states.FEMALES = dataReader.GetDecimal(19);
        
          if(!dataReader.IsDBNull(20))
          states.AGE_UNDER5 = dataReader.GetDecimal(20);
        
          if(!dataReader.IsDBNull(21))
          states.AGE_5_17 = dataReader.GetDecimal(21);
        
          if(!dataReader.IsDBNull(22))
          states.AGE_18_21 = dataReader.GetDecimal(22);
        
          if(!dataReader.IsDBNull(23))
          states.AGE_22_29 = dataReader.GetDecimal(23);
        
          if(!dataReader.IsDBNull(24))
          states.AGE_30_39 = dataReader.GetDecimal(24);
        
          if(!dataReader.IsDBNull(25))
          states.AGE_40_49 = dataReader.GetDecimal(25);
        
          if(!dataReader.IsDBNull(26))
          states.AGE_50_64 = dataReader.GetDecimal(26);
        
          if(!dataReader.IsDBNull(27))
          states.AGE_65_UP = dataReader.GetDecimal(27);
        
          if(!dataReader.IsDBNull(28))
          states.MED_AGE = dataReader.GetDecimal(28);
        
          if(!dataReader.IsDBNull(29))
          states.MED_AGE_M = dataReader.GetDecimal(29);
        
          if(!dataReader.IsDBNull(30))
          states.MED_AGE_F = dataReader.GetDecimal(30);
        
          if(!dataReader.IsDBNull(31))
          states.HOUSEHOLDS = dataReader.GetDecimal(31);
        
          if(!dataReader.IsDBNull(32))
          states.AVE_HH_SZ = dataReader.GetDecimal(32);
        
          if(!dataReader.IsDBNull(33))
          states.HSEHLD_1_M = dataReader.GetDecimal(33);
        
          if(!dataReader.IsDBNull(34))
          states.HSEHLD_1_F = dataReader.GetDecimal(34);
        
          if(!dataReader.IsDBNull(35))
          states.MARHH_CHD = dataReader.GetDecimal(35);
        
          if(!dataReader.IsDBNull(36))
          states.MARHH_NO_C = dataReader.GetDecimal(36);
        
          if(!dataReader.IsDBNull(37))
          states.MHH_CHILD = dataReader.GetDecimal(37);
        
          if(!dataReader.IsDBNull(38))
          states.FHH_CHILD = dataReader.GetDecimal(38);
        
          if(!dataReader.IsDBNull(39))
          states.FAMILIES = dataReader.GetDecimal(39);
        
          if(!dataReader.IsDBNull(40))
          states.AVE_FAM_SZ = dataReader.GetDecimal(40);
        
          if(!dataReader.IsDBNull(41))
          states.HSE_UNITS = dataReader.GetDecimal(41);
        
          if(!dataReader.IsDBNull(42))
          states.VACANT = dataReader.GetDecimal(42);
        
          if(!dataReader.IsDBNull(43))
          states.OWNER_OCC = dataReader.GetDecimal(43);
        
          if(!dataReader.IsDBNull(44))
          states.RENTER_OCC = dataReader.GetDecimal(44);
        
          if(!dataReader.IsDBNull(45))
          states.NO_FARMS97 = dataReader.GetDecimal(45);
        
          if(!dataReader.IsDBNull(46))
          states.AVG_SIZE97 = dataReader.GetDecimal(46);
        
          if(!dataReader.IsDBNull(47))
          states.CROP_ACR97 = dataReader.GetDecimal(47);
        
          if(!dataReader.IsDBNull(48))
          states.AVG_SALE97 = dataReader.GetDecimal(48);
        
          if(!dataReader.IsDBNull(49))
          states.SQMI = dataReader.GetDecimal(49);
        
          if(!dataReader.IsDBNull(50))
          states.Shape_Leng = dataReader.GetDecimal(50);
        
          if(!dataReader.IsDBNull(51))
          states.Shape_Area = dataReader.GetDecimal(51);
        states.IsLoaded = true;
    
    return registerRecord(states);
    }


    protected override States doLoad(Hashtable hashtable)
    {
      States states = new States();

      
        if(hashtable.ContainsKey("OBJECTID"))
            states.OBJECTID = ( int)hashtable["OBJECTID"];
      
        if(hashtable.ContainsKey("STATE_ID"))
            states.STATE_ID = ( int)hashtable["STATE_ID"];
      
        if(hashtable.ContainsKey("STATE_NAME"))
            states.STATE_NAME = ( String)hashtable["STATE_NAME"];
      
        if(hashtable.ContainsKey("STATE_FIPS"))
            states.STATE_FIPS = ( String)hashtable["STATE_FIPS"];
      
        if(hashtable.ContainsKey("SUB_REGION"))
            states.SUB_REGION = ( String)hashtable["SUB_REGION"];
      
        if(hashtable.ContainsKey("STATE_ABBR"))
            states.STATE_ABBR = ( String)hashtable["STATE_ABBR"];
      
        if(hashtable.ContainsKey("POP2000"))
            states.POP2000 = ( decimal)hashtable["POP2000"];
      
        if(hashtable.ContainsKey("POP2004"))
            states.POP2004 = ( decimal)hashtable["POP2004"];
      
        if(hashtable.ContainsKey("POP00_SQMI"))
            states.POP00_SQMI = ( decimal)hashtable["POP00_SQMI"];
      
        if(hashtable.ContainsKey("POP04_SQMI"))
            states.POP04_SQMI = ( decimal)hashtable["POP04_SQMI"];
      
        if(hashtable.ContainsKey("WHITE"))
            states.WHITE = ( decimal)hashtable["WHITE"];
      
        if(hashtable.ContainsKey("BLACK"))
            states.BLACK = ( decimal)hashtable["BLACK"];
      
        if(hashtable.ContainsKey("AMERI_ES"))
            states.AMERI_ES = ( decimal)hashtable["AMERI_ES"];
      
        if(hashtable.ContainsKey("ASIAN"))
            states.ASIAN = ( decimal)hashtable["ASIAN"];
      
        if(hashtable.ContainsKey("HAWN_PI"))
            states.HAWN_PI = ( decimal)hashtable["HAWN_PI"];
      
        if(hashtable.ContainsKey("OTHER"))
            states.OTHER = ( decimal)hashtable["OTHER"];
      
        if(hashtable.ContainsKey("MULT_RACE"))
            states.MULT_RACE = ( decimal)hashtable["MULT_RACE"];
      
        if(hashtable.ContainsKey("HISPANIC"))
            states.HISPANIC = ( decimal)hashtable["HISPANIC"];
      
        if(hashtable.ContainsKey("MALES"))
            states.MALES = ( decimal)hashtable["MALES"];
      
        if(hashtable.ContainsKey("FEMALES"))
            states.FEMALES = ( decimal)hashtable["FEMALES"];
      
        if(hashtable.ContainsKey("AGE_UNDER5"))
            states.AGE_UNDER5 = ( decimal)hashtable["AGE_UNDER5"];
      
        if(hashtable.ContainsKey("AGE_5_17"))
            states.AGE_5_17 = ( decimal)hashtable["AGE_5_17"];
      
        if(hashtable.ContainsKey("AGE_18_21"))
            states.AGE_18_21 = ( decimal)hashtable["AGE_18_21"];
      
        if(hashtable.ContainsKey("AGE_22_29"))
            states.AGE_22_29 = ( decimal)hashtable["AGE_22_29"];
      
        if(hashtable.ContainsKey("AGE_30_39"))
            states.AGE_30_39 = ( decimal)hashtable["AGE_30_39"];
      
        if(hashtable.ContainsKey("AGE_40_49"))
            states.AGE_40_49 = ( decimal)hashtable["AGE_40_49"];
      
        if(hashtable.ContainsKey("AGE_50_64"))
            states.AGE_50_64 = ( decimal)hashtable["AGE_50_64"];
      
        if(hashtable.ContainsKey("AGE_65_UP"))
            states.AGE_65_UP = ( decimal)hashtable["AGE_65_UP"];
      
        if(hashtable.ContainsKey("MED_AGE"))
            states.MED_AGE = ( decimal)hashtable["MED_AGE"];
      
        if(hashtable.ContainsKey("MED_AGE_M"))
            states.MED_AGE_M = ( decimal)hashtable["MED_AGE_M"];
      
        if(hashtable.ContainsKey("MED_AGE_F"))
            states.MED_AGE_F = ( decimal)hashtable["MED_AGE_F"];
      
        if(hashtable.ContainsKey("HOUSEHOLDS"))
            states.HOUSEHOLDS = ( decimal)hashtable["HOUSEHOLDS"];
      
        if(hashtable.ContainsKey("AVE_HH_SZ"))
            states.AVE_HH_SZ = ( decimal)hashtable["AVE_HH_SZ"];
      
        if(hashtable.ContainsKey("HSEHLD_1_M"))
            states.HSEHLD_1_M = ( decimal)hashtable["HSEHLD_1_M"];
      
        if(hashtable.ContainsKey("HSEHLD_1_F"))
            states.HSEHLD_1_F = ( decimal)hashtable["HSEHLD_1_F"];
      
        if(hashtable.ContainsKey("MARHH_CHD"))
            states.MARHH_CHD = ( decimal)hashtable["MARHH_CHD"];
      
        if(hashtable.ContainsKey("MARHH_NO_C"))
            states.MARHH_NO_C = ( decimal)hashtable["MARHH_NO_C"];
      
        if(hashtable.ContainsKey("MHH_CHILD"))
            states.MHH_CHILD = ( decimal)hashtable["MHH_CHILD"];
      
        if(hashtable.ContainsKey("FHH_CHILD"))
            states.FHH_CHILD = ( decimal)hashtable["FHH_CHILD"];
      
        if(hashtable.ContainsKey("FAMILIES"))
            states.FAMILIES = ( decimal)hashtable["FAMILIES"];
      
        if(hashtable.ContainsKey("AVE_FAM_SZ"))
            states.AVE_FAM_SZ = ( decimal)hashtable["AVE_FAM_SZ"];
      
        if(hashtable.ContainsKey("HSE_UNITS"))
            states.HSE_UNITS = ( decimal)hashtable["HSE_UNITS"];
      
        if(hashtable.ContainsKey("VACANT"))
            states.VACANT = ( decimal)hashtable["VACANT"];
      
        if(hashtable.ContainsKey("OWNER_OCC"))
            states.OWNER_OCC = ( decimal)hashtable["OWNER_OCC"];
      
        if(hashtable.ContainsKey("RENTER_OCC"))
            states.RENTER_OCC = ( decimal)hashtable["RENTER_OCC"];
      
        if(hashtable.ContainsKey("NO_FARMS97"))
            states.NO_FARMS97 = ( decimal)hashtable["NO_FARMS97"];
      
        if(hashtable.ContainsKey("AVG_SIZE97"))
            states.AVG_SIZE97 = ( decimal)hashtable["AVG_SIZE97"];
      
        if(hashtable.ContainsKey("CROP_ACR97"))
            states.CROP_ACR97 = ( decimal)hashtable["CROP_ACR97"];
      
        if(hashtable.ContainsKey("AVG_SALE97"))
            states.AVG_SALE97 = ( decimal)hashtable["AVG_SALE97"];
      
        if(hashtable.ContainsKey("SQMI"))
            states.SQMI = ( decimal)hashtable["SQMI"];
      
        if(hashtable.ContainsKey("Shape_Leng"))
            states.Shape_Leng = ( decimal)hashtable["Shape_Leng"];
      
        if(hashtable.ContainsKey("Shape_Area"))
            states.Shape_Area = ( decimal)hashtable["Shape_Area"];
      

      return states;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [States]
    
      Where
      STATE_ID = @STATE_ID";
    [Synchronized]
    public States remove(States states)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@STATE_ID", states.STATE_ID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(states,DataMapperOperation.delete);

      return registerRecord(states);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public States save( States states )
    {
      if(exists(states))
        return update(states);
        return create(states);
    }

  

    const String SqlUpdate = @"Update [States] Set 
    OBJECTID = @OBJECTID,STATE_NAME = @STATE_NAME,STATE_FIPS = @STATE_FIPS,SUB_REGION = @SUB_REGION,STATE_ABBR = @STATE_ABBR,POP2000 = @POP2000,POP2004 = @POP2004,POP00_SQMI = @POP00_SQMI,POP04_SQMI = @POP04_SQMI,WHITE = @WHITE,BLACK = @BLACK,AMERI_ES = @AMERI_ES,ASIAN = @ASIAN,HAWN_PI = @HAWN_PI,OTHER = @OTHER,MULT_RACE = @MULT_RACE,HISPANIC = @HISPANIC,MALES = @MALES,FEMALES = @FEMALES,AGE_UNDER5 = @AGE_UNDER5,AGE_5_17 = @AGE_5_17,AGE_18_21 = @AGE_18_21,AGE_22_29 = @AGE_22_29,AGE_30_39 = @AGE_30_39,AGE_40_49 = @AGE_40_49,AGE_50_64 = @AGE_50_64,AGE_65_UP = @AGE_65_UP,MED_AGE = @MED_AGE,MED_AGE_M = @MED_AGE_M,MED_AGE_F = @MED_AGE_F,HOUSEHOLDS = @HOUSEHOLDS,AVE_HH_SZ = @AVE_HH_SZ,HSEHLD_1_M = @HSEHLD_1_M,HSEHLD_1_F = @HSEHLD_1_F,MARHH_CHD = @MARHH_CHD,MARHH_NO_C = @MARHH_NO_C,MHH_CHILD = @MHH_CHILD,FHH_CHILD = @FHH_CHILD,FAMILIES = @FAMILIES,AVE_FAM_SZ = @AVE_FAM_SZ,HSE_UNITS = @HSE_UNITS,VACANT = @VACANT,OWNER_OCC = @OWNER_OCC,RENTER_OCC = @RENTER_OCC,NO_FARMS97 = @NO_FARMS97,AVG_SIZE97 = @AVG_SIZE97,CROP_ACR97 = @CROP_ACR97,AVG_SALE97 = @AVG_SALE97,SQMI = @SQMI,Shape_Leng = @Shape_Leng,Shape_Area = @Shape_Area
       Where 
      STATE_ID = @STATE_ID";
    
    [TransactionRequired]
    [Synchronized]
    public States update(States states)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                    if(states.OBJECTID != null)
                      sqlCommand.Parameters.AddWithValue("@OBJECTID", states.OBJECTID);
                    else
                      sqlCommand.Parameters.AddWithValue("@OBJECTID", DBNull.Value);
                  
                      sqlCommand.Parameters.AddWithValue("@STATE_ID", states.STATE_ID);
                  
                    if(states.STATE_NAME != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", states.STATE_NAME);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_NAME", DBNull.Value);
                  
                    if(states.STATE_FIPS != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", states.STATE_FIPS);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_FIPS", DBNull.Value);
                  
                    if(states.SUB_REGION != null)
                      sqlCommand.Parameters.AddWithValue("@SUB_REGION", states.SUB_REGION);
                    else
                      sqlCommand.Parameters.AddWithValue("@SUB_REGION", DBNull.Value);
                  
                    if(states.STATE_ABBR != null)
                      sqlCommand.Parameters.AddWithValue("@STATE_ABBR", states.STATE_ABBR);
                    else
                      sqlCommand.Parameters.AddWithValue("@STATE_ABBR", DBNull.Value);
                  
                    if(states.POP2000 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2000", states.POP2000);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2000", DBNull.Value);
                  
                    if(states.POP2004 != null)
                      sqlCommand.Parameters.AddWithValue("@POP2004", states.POP2004);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP2004", DBNull.Value);
                  
                    if(states.POP00_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", states.POP00_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP00_SQMI", DBNull.Value);
                  
                    if(states.POP04_SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", states.POP04_SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@POP04_SQMI", DBNull.Value);
                  
                    if(states.WHITE != null)
                      sqlCommand.Parameters.AddWithValue("@WHITE", states.WHITE);
                    else
                      sqlCommand.Parameters.AddWithValue("@WHITE", DBNull.Value);
                  
                    if(states.BLACK != null)
                      sqlCommand.Parameters.AddWithValue("@BLACK", states.BLACK);
                    else
                      sqlCommand.Parameters.AddWithValue("@BLACK", DBNull.Value);
                  
                    if(states.AMERI_ES != null)
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", states.AMERI_ES);
                    else
                      sqlCommand.Parameters.AddWithValue("@AMERI_ES", DBNull.Value);
                  
                    if(states.ASIAN != null)
                      sqlCommand.Parameters.AddWithValue("@ASIAN", states.ASIAN);
                    else
                      sqlCommand.Parameters.AddWithValue("@ASIAN", DBNull.Value);
                  
                    if(states.HAWN_PI != null)
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", states.HAWN_PI);
                    else
                      sqlCommand.Parameters.AddWithValue("@HAWN_PI", DBNull.Value);
                  
                    if(states.OTHER != null)
                      sqlCommand.Parameters.AddWithValue("@OTHER", states.OTHER);
                    else
                      sqlCommand.Parameters.AddWithValue("@OTHER", DBNull.Value);
                  
                    if(states.MULT_RACE != null)
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", states.MULT_RACE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MULT_RACE", DBNull.Value);
                  
                    if(states.HISPANIC != null)
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", states.HISPANIC);
                    else
                      sqlCommand.Parameters.AddWithValue("@HISPANIC", DBNull.Value);
                  
                    if(states.MALES != null)
                      sqlCommand.Parameters.AddWithValue("@MALES", states.MALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@MALES", DBNull.Value);
                  
                    if(states.FEMALES != null)
                      sqlCommand.Parameters.AddWithValue("@FEMALES", states.FEMALES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FEMALES", DBNull.Value);
                  
                    if(states.AGE_UNDER5 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", states.AGE_UNDER5);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_UNDER5", DBNull.Value);
                  
                    if(states.AGE_5_17 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", states.AGE_5_17);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_5_17", DBNull.Value);
                  
                    if(states.AGE_18_21 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", states.AGE_18_21);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_18_21", DBNull.Value);
                  
                    if(states.AGE_22_29 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", states.AGE_22_29);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_22_29", DBNull.Value);
                  
                    if(states.AGE_30_39 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", states.AGE_30_39);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_30_39", DBNull.Value);
                  
                    if(states.AGE_40_49 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", states.AGE_40_49);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_40_49", DBNull.Value);
                  
                    if(states.AGE_50_64 != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", states.AGE_50_64);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_50_64", DBNull.Value);
                  
                    if(states.AGE_65_UP != null)
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", states.AGE_65_UP);
                    else
                      sqlCommand.Parameters.AddWithValue("@AGE_65_UP", DBNull.Value);
                  
                    if(states.MED_AGE != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", states.MED_AGE);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE", DBNull.Value);
                  
                    if(states.MED_AGE_M != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", states.MED_AGE_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_M", DBNull.Value);
                  
                    if(states.MED_AGE_F != null)
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", states.MED_AGE_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@MED_AGE_F", DBNull.Value);
                  
                    if(states.HOUSEHOLDS != null)
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", states.HOUSEHOLDS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HOUSEHOLDS", DBNull.Value);
                  
                    if(states.AVE_HH_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", states.AVE_HH_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_HH_SZ", DBNull.Value);
                  
                    if(states.HSEHLD_1_M != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", states.HSEHLD_1_M);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_M", DBNull.Value);
                  
                    if(states.HSEHLD_1_F != null)
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", states.HSEHLD_1_F);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSEHLD_1_F", DBNull.Value);
                  
                    if(states.MARHH_CHD != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", states.MARHH_CHD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_CHD", DBNull.Value);
                  
                    if(states.MARHH_NO_C != null)
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", states.MARHH_NO_C);
                    else
                      sqlCommand.Parameters.AddWithValue("@MARHH_NO_C", DBNull.Value);
                  
                    if(states.MHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", states.MHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@MHH_CHILD", DBNull.Value);
                  
                    if(states.FHH_CHILD != null)
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", states.FHH_CHILD);
                    else
                      sqlCommand.Parameters.AddWithValue("@FHH_CHILD", DBNull.Value);
                  
                    if(states.FAMILIES != null)
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", states.FAMILIES);
                    else
                      sqlCommand.Parameters.AddWithValue("@FAMILIES", DBNull.Value);
                  
                    if(states.AVE_FAM_SZ != null)
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", states.AVE_FAM_SZ);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVE_FAM_SZ", DBNull.Value);
                  
                    if(states.HSE_UNITS != null)
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", states.HSE_UNITS);
                    else
                      sqlCommand.Parameters.AddWithValue("@HSE_UNITS", DBNull.Value);
                  
                    if(states.VACANT != null)
                      sqlCommand.Parameters.AddWithValue("@VACANT", states.VACANT);
                    else
                      sqlCommand.Parameters.AddWithValue("@VACANT", DBNull.Value);
                  
                    if(states.OWNER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", states.OWNER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@OWNER_OCC", DBNull.Value);
                  
                    if(states.RENTER_OCC != null)
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", states.RENTER_OCC);
                    else
                      sqlCommand.Parameters.AddWithValue("@RENTER_OCC", DBNull.Value);
                  
                    if(states.NO_FARMS97 != null)
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", states.NO_FARMS97);
                    else
                      sqlCommand.Parameters.AddWithValue("@NO_FARMS97", DBNull.Value);
                  
                    if(states.AVG_SIZE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", states.AVG_SIZE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SIZE97", DBNull.Value);
                  
                    if(states.CROP_ACR97 != null)
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", states.CROP_ACR97);
                    else
                      sqlCommand.Parameters.AddWithValue("@CROP_ACR97", DBNull.Value);
                  
                    if(states.AVG_SALE97 != null)
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", states.AVG_SALE97);
                    else
                      sqlCommand.Parameters.AddWithValue("@AVG_SALE97", DBNull.Value);
                  
                    if(states.SQMI != null)
                      sqlCommand.Parameters.AddWithValue("@SQMI", states.SQMI);
                    else
                      sqlCommand.Parameters.AddWithValue("@SQMI", DBNull.Value);
                  
                    if(states.Shape_Leng != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", states.Shape_Leng);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Leng", DBNull.Value);
                  
                    if(states.Shape_Area != null)
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", states.Shape_Area);
                    else
                      sqlCommand.Parameters.AddWithValue("@Shape_Area", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    
          if(states.childsCountys != null
          && states.childsCountys.Count > 0)
          {
          CountysDataMapper dataMapper = new CountysDataMapper(Database);

          foreach(Countys item in states.childsCountys)
            dataMapper.save(item);
          }
        

      raiseAffected(states,DataMapperOperation.update);

      return registerRecord(states);
    }

  
    }
    
  
    
    public partial class Tract: DomainObject
    {
    
      protected int _tractID;
    
      protected int? _docID;
    
      protected String _refName;
    
      protected decimal? _calledAC;
    
      protected String _scopePlotUrl;
    

    public Tract(){}

    public Tract(
    int 
            tractID,int 
            docID,String 
            refName,decimal 
            calledAC,String 
            scopePlotUrl
    )
    {
    TractID = tractID;
    DocID = docID;
    RefName = refName;
    CalledAC = calledAC;
    ScopePlotUrl = scopePlotUrl;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Tract."
    
      + TractID.ToString()
    ;
    
    return uri;
    }

    

      public int TractID
      {
        
            get { return _tractID;}
            set 
            { 
                _tractID = value;
            }
          
      }
    

      public int? DocID
      {
        
            get { return _docID;}
            set 
            { 
                _docID = value;
            }
          
      }
    

      public String RefName
      {
        
            get { return _refName;}
            set 
            { 
                _refName = value;
            }
          
      }
    

      public decimal? CalledAC
      {
        
            get { return _calledAC;}
            set 
            { 
                _calledAC = value;
            }
          
      }
    

      public String ScopePlotUrl
      {
        
            get { return _scopePlotUrl;}
            set 
            { 
                _scopePlotUrl = value;
            }
          
      }
    


    }
  

    public partial class TractDataMapper:TDataMapper<Tract,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public TractDataMapper(){}
      public TractDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Tract";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Tract] (
    DocID,RefName,CalledAC,ScopePlotUrl) Values (
    
      @DocID,
      @RefName,
      @CalledAC,
      @ScopePlotUrl);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Tract create( Tract tract )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(tract.DocID != null)
                      sqlCommand.Parameters.AddWithValue("@DocID", tract.DocID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocID", DBNull.Value);
                  
                    if(tract.RefName != null)
                      sqlCommand.Parameters.AddWithValue("@RefName", tract.RefName);
                    else
                      sqlCommand.Parameters.AddWithValue("@RefName", DBNull.Value);
                  
                    if(tract.CalledAC != null)
                      sqlCommand.Parameters.AddWithValue("@CalledAC", tract.CalledAC);
                    else
                      sqlCommand.Parameters.AddWithValue("@CalledAC", DBNull.Value);
                  
                    if(tract.ScopePlotUrl != null)
                      sqlCommand.Parameters.AddWithValue("@ScopePlotUrl", tract.ScopePlotUrl);
                    else
                      sqlCommand.Parameters.AddWithValue("@ScopePlotUrl", DBNull.Value);
                  tract.TractID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(tract,DataMapperOperation.create);

      return registerRecord(tract);
    }

  

    private const String SqlSelectAll = @"Select
    TractID,DocID,RefName,CalledAC,ScopePlotUrl 
    From [Tract] ";
    
    public List<Tract> findAll(Object args)
    {
      List<Tract> rv = new List<Tract>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    TractID,DocID,RefName,CalledAC,ScopePlotUrl
     From [Tract]
    
       Where 
      TractID = @TractID
    ";

    public Tract findByPrimaryKey(
    int tractID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@TractID", tractID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Tract not found, search by primary key");
 

    }


    public bool exists(Tract tract)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@TractID", tract.TractID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Tract doLoad(IDataReader dataReader)
    {
    Tract tract = new Tract();

    tract.TractID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          tract.DocID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          tract.RefName = dataReader.GetString(2);
        
          if(!dataReader.IsDBNull(3))
          tract.CalledAC = dataReader.GetDecimal(3);
        
          if(!dataReader.IsDBNull(4))
          tract.ScopePlotUrl = dataReader.GetString(4);
        tract.IsLoaded = true;
    
    return registerRecord(tract);
    }


    protected override Tract doLoad(Hashtable hashtable)
    {
      Tract tract = new Tract();

      
        if(hashtable.ContainsKey("TractID"))
            tract.TractID = ( int)hashtable["TractID"];
      
        if(hashtable.ContainsKey("DocID"))
            tract.DocID = ( int)hashtable["DocID"];
      
        if(hashtable.ContainsKey("RefName"))
            tract.RefName = ( String)hashtable["RefName"];
      
        if(hashtable.ContainsKey("CalledAC"))
            tract.CalledAC = ( decimal)hashtable["CalledAC"];
      
        if(hashtable.ContainsKey("ScopePlotUrl"))
            tract.ScopePlotUrl = ( String)hashtable["ScopePlotUrl"];
      

      return tract;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Tract]
    
      Where
      TractID = @TractID";
    [Synchronized]
    public Tract remove(Tract tract)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@TractID", tract.TractID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(tract,DataMapperOperation.delete);

      return registerRecord(tract);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Tract save( Tract tract )
    {
      if(exists(tract))
        return update(tract);
        return create(tract);
    }

  

    const String SqlUpdate = @"Update [Tract] Set 
    DocID = @DocID,RefName = @RefName,CalledAC = @CalledAC,ScopePlotUrl = @ScopePlotUrl
       Where 
      TractID = @TractID";
    
    
    [Synchronized]
    public Tract update(Tract tract)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@TractID", tract.TractID);
                  
                    if(tract.DocID != null)
                      sqlCommand.Parameters.AddWithValue("@DocID", tract.DocID);
                    else
                      sqlCommand.Parameters.AddWithValue("@DocID", DBNull.Value);
                  
                    if(tract.RefName != null)
                      sqlCommand.Parameters.AddWithValue("@RefName", tract.RefName);
                    else
                      sqlCommand.Parameters.AddWithValue("@RefName", DBNull.Value);
                  
                    if(tract.CalledAC != null)
                      sqlCommand.Parameters.AddWithValue("@CalledAC", tract.CalledAC);
                    else
                      sqlCommand.Parameters.AddWithValue("@CalledAC", DBNull.Value);
                  
                    if(tract.ScopePlotUrl != null)
                      sqlCommand.Parameters.AddWithValue("@ScopePlotUrl", tract.ScopePlotUrl);
                    else
                      sqlCommand.Parameters.AddWithValue("@ScopePlotUrl", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(tract,DataMapperOperation.update);

      return registerRecord(tract);
    }

  
    }
    
  
    
    public partial class Tractexception: DomainObject
    {
    
      protected int _tractExceptionsID;
    
      protected int? _tractID;
    
      protected String _refName;
    
      protected String _calledAC;
    

    public Tractexception(){}

    public Tractexception(
    int 
            tractExceptionsID,int 
            tractID,String 
            refName,String 
            calledAC
    )
    {
    TractExceptionsID = tractExceptionsID;
    TractID = tractID;
    RefName = refName;
    CalledAC = calledAC;
    
    }

    public override String  getUri()
    {

    String uri = "doc-capture.Tractexception."
    
      + TractExceptionsID.ToString()
    ;
    
    return uri;
    }

    

      public int TractExceptionsID
      {
        
            get { return _tractExceptionsID;}
            set 
            { 
                _tractExceptionsID = value;
            }
          
      }
    

      public int? TractID
      {
        
            get { return _tractID;}
            set 
            { 
                _tractID = value;
            }
          
      }
    

      public String RefName
      {
        
            get { return _refName;}
            set 
            { 
                _refName = value;
            }
          
      }
    

      public String CalledAC
      {
        
            get { return _calledAC;}
            set 
            { 
                _calledAC = value;
            }
          
      }
    


    }
  

    public partial class TractexceptionDataMapper:TDataMapper<Tractexception,SqlConnection,Doc_captureDb, Weborb.Data.Management.MSSql.CommandBuilder, SqlTransaction, SqlCommand>
    {
      public TractexceptionDataMapper(){}
      public TractexceptionDataMapper(Doc_captureDb database):
      base(database){}
      public override String TableName
      {
        get
        {
          return "Tractexception";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into [Tractexception] (
    TractID,RefName,CalledAC) Values (
    
      @TractID,
      @RefName,
      @CalledAC);
    
      select scope_identity();
    ";
    
    
    [Synchronized]
    public override Tractexception create( Tractexception tractexception )
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand( SqlCreate ))
        {
        
                    if(tractexception.TractID != null)
                      sqlCommand.Parameters.AddWithValue("@TractID", tractexception.TractID);
                    else
                      sqlCommand.Parameters.AddWithValue("@TractID", DBNull.Value);
                  
                    if(tractexception.RefName != null)
                      sqlCommand.Parameters.AddWithValue("@RefName", tractexception.RefName);
                    else
                      sqlCommand.Parameters.AddWithValue("@RefName", DBNull.Value);
                  
                    if(tractexception.CalledAC != null)
                      sqlCommand.Parameters.AddWithValue("@CalledAC", tractexception.CalledAC);
                    else
                      sqlCommand.Parameters.AddWithValue("@CalledAC", DBNull.Value);
                  tractexception.TractExceptionsID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
                
        }
      }
      
    
      
      raiseAffected(tractexception,DataMapperOperation.create);

      return registerRecord(tractexception);
    }

  

    private const String SqlSelectAll = @"Select
    TractExceptionsID,TractID,RefName,CalledAC 
    From [Tractexception] ";
    
    public List<Tractexception> findAll(Object args)
    {
      List<Tractexception> rv = new List<Tractexception>();

      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectAll))
        {

          using(IDataReader dataReader = sqlCommand.ExecuteReader())
          {
            while(dataReader.Read())
              rv.Add(doLoad(dataReader));
          }
        }
        
       }

      return rv;
  
    }
  

    private const String SqlSelectByPk = @"Select
    TractExceptionsID,TractID,RefName,CalledAC
     From [Tractexception]
    
       Where 
      TractExceptionsID = @TractExceptionsID
    ";

    public Tractexception findByPrimaryKey(
    int tractExceptionsID
    )
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
    using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
    {
    
          sqlCommand.Parameters.AddWithValue("@TractExceptionsID", tractExceptionsID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
     }      
      throw new DataNotFoundException("Tractexception not found, search by primary key");
 

    }


    public bool exists(Tractexception tractexception)
    {
    using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
    {
      using(SqlCommand sqlCommand = Database.CreateCommand(SqlSelectByPk))
      {
      
            sqlCommand.Parameters.AddWithValue("@TractExceptionsID", tractexception.TractExceptionsID);
          

            using(IDataReader dataReader = sqlCommand.ExecuteReader())
            {
            return dataReader.Read();
            }
        }
      }
    }
  

    protected override Tractexception doLoad(IDataReader dataReader)
    {
    Tractexception tractexception = new Tractexception();

    tractexception.TractExceptionsID = dataReader.GetInt32(0);
        
          if(!dataReader.IsDBNull(1))
          tractexception.TractID = dataReader.GetInt32(1);
        
          if(!dataReader.IsDBNull(2))
          tractexception.RefName = dataReader.GetString(2);
        
          if(!dataReader.IsDBNull(3))
          tractexception.CalledAC = dataReader.GetString(3);
        tractexception.IsLoaded = true;
    
    return registerRecord(tractexception);
    }


    protected override Tractexception doLoad(Hashtable hashtable)
    {
      Tractexception tractexception = new Tractexception();

      
        if(hashtable.ContainsKey("TractExceptionsID"))
            tractexception.TractExceptionsID = ( int)hashtable["TractExceptionsID"];
      
        if(hashtable.ContainsKey("TractID"))
            tractexception.TractID = ( int)hashtable["TractID"];
      
        if(hashtable.ContainsKey("RefName"))
            tractexception.RefName = ( String)hashtable["RefName"];
      
        if(hashtable.ContainsKey("CalledAC"))
            tractexception.CalledAC = ( String)hashtable["CalledAC"];
      

      return tractexception;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From [Tractexception]
    
      Where
      TractExceptionsID = @TractExceptionsID";
    [Synchronized]
    public Tractexception remove(Tractexception tractexception)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
          using(SqlCommand sqlCommand = Database.CreateCommand(SqlDelete))
          {
          
            sqlCommand.Parameters.AddWithValue("@TractExceptionsID", tractexception.TractExceptionsID);
          
            sqlCommand.ExecuteNonQuery();
          }
       }
      raiseAffected(tractexception,DataMapperOperation.delete);

      return registerRecord(tractexception);
    }

    #endregion
  
    [Synchronized]
    [TransactionRequired]
    public Tractexception save( Tractexception tractexception )
    {
      if(exists(tractexception))
        return update(tractexception);
        return create(tractexception);
    }

  

    const String SqlUpdate = @"Update [Tractexception] Set 
    TractID = @TractID,RefName = @RefName,CalledAC = @CalledAC
       Where 
      TractExceptionsID = @TractExceptionsID";
    
    
    [Synchronized]
    public Tractexception update(Tractexception tractexception)
    {
      using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
      {
        using(SqlCommand sqlCommand = Database.CreateCommand(SqlUpdate))
        {
        
                      sqlCommand.Parameters.AddWithValue("@TractExceptionsID", tractexception.TractExceptionsID);
                  
                    if(tractexception.TractID != null)
                      sqlCommand.Parameters.AddWithValue("@TractID", tractexception.TractID);
                    else
                      sqlCommand.Parameters.AddWithValue("@TractID", DBNull.Value);
                  
                    if(tractexception.RefName != null)
                      sqlCommand.Parameters.AddWithValue("@RefName", tractexception.RefName);
                    else
                      sqlCommand.Parameters.AddWithValue("@RefName", DBNull.Value);
                  
                    if(tractexception.CalledAC != null)
                      sqlCommand.Parameters.AddWithValue("@CalledAC", tractexception.CalledAC);
                    else
                      sqlCommand.Parameters.AddWithValue("@CalledAC", DBNull.Value);
                  


        sqlCommand.ExecuteNonQuery();
        }
      }

    

      raiseAffected(tractexception,DataMapperOperation.update);

      return registerRecord(tractexception);
    }

  
    }
    
  
      
      

      }
    