
      namespace TractInc.DocCapture.Domain
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
        ConnectionString = "server=localhost;port=3306;user id=root;password=++Winston;database=doc-capture;";
      }
    }
  
    
    public partial class Addresstype
    {
    
      protected int _addressTypeID;
    
      protected String _types;
    

    public Addresstype(){}

    public Addresstype(
    int 
            addressTypeID
    )
    {
    
      _addressTypeID = addressTypeID;
    
    }

    

      public Addresstype(
      int 
        addressTypeID,String 
        types
      )
      {
      
        _addressTypeID = addressTypeID;
      
        _types = types;
      
      }

    
      public int AddressTypeID
      {
      get { return _addressTypeID;}
      set { _addressTypeID = value; }
      }
    
      public String Types
      {
      get { return _types;}
      set { _types = value; }
      }
    
    
    }
  

    public partial class AddresstypeDataMapper:TDataMapper<Addresstype,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "addresstype";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into addresstype (
    Types) Values (
    
      ?Types);";

    public override Addresstype create( Addresstype addresstype )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(addresstype.Types != null)
                    sqlCommand.Parameters.Add("?Types", addresstype.Types);
                  else
                    sqlCommand.Parameters.Add("?Types", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                addresstype.AddressTypeID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return addresstype;
    }

  

    private const String SqlSelectAll = @"Select
    AddressTypeID,Types 
    From addresstype ";
    
    public List<Addresstype> findAll(Object args)
    {
      List<Addresstype> rv = new List<Addresstype>();

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
    AddressTypeID,Types
     From addresstype
       Where 
      AddressTypeID = ?AddressTypeID
    ";

    public Addresstype findByPrimaryKey(
    int addressTypeID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AddressTypeID", addressTypeID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("addresstype not found, search by primary key");
    }

    }


    public bool exists(Addresstype addresstype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AddressTypeID", addresstype.AddressTypeID);
          

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
        

    return addresstype;
    }


    protected override Addresstype doLoad(Hashtable hashtable)
    {
      Addresstype addresstype = new Addresstype();

      
        if(hashtable.ContainsKey("AddressTypeID"))
        addresstype.AddressTypeID = (int) Convert.ChangeType(hashtable["AddressTypeID"], typeof(int));
          
        if(hashtable.ContainsKey("Types"))
        addresstype.Types = ( String )hashtable["Types"];
          

      return addresstype;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From addresstype
      Where
      AddressTypeID = ?AddressTypeID";
    public Addresstype remove(Addresstype addresstype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AddressTypeID", addresstype.AddressTypeID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return addresstype;
    }

    #endregion
  

    public Addresstype save( Addresstype addresstype )
    {
      if(exists(addresstype))
        return update(addresstype);
        return create(addresstype);
    }

  


    private const String SqlUpdate = @"Update addresstype Set 
    Types = ?Types
       Where 
      AddressTypeID = ?AddressTypeID";

    public Addresstype update(Addresstype addresstype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AddressTypeID", addresstype.AddressTypeID);
              
                if(addresstype.Types != null)
                  sqlCommand.Parameters.Add("?Types", addresstype.Types);
                else
                  sqlCommand.Parameters.Add("?Types", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return addresstype;
    }

  
    }
    
  
    
    public partial class Document
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
            docID
    )
    {
    
      _docID = docID;
    
    }

    

      public Document(
      int 
        docID,bool 
        isPublic,int? 
        docTypeId,String 
        vol,String 
        pg,String 
        documentNo,String 
        county,String 
        state,DateTime? 
        dateFiled,DateTime? 
        dateSigned,String 
        researchNote,String 
        imageLink
      )
      {
      
        _docID = docID;
      
        _isPublic = isPublic;
      
        _docTypeId = docTypeId;
      
        _vol = vol;
      
        _pg = pg;
      
        _documentNo = documentNo;
      
        _county = county;
      
        _state = state;
      
        _dateFiled = dateFiled;
      
        _dateSigned = dateSigned;
      
        _researchNote = researchNote;
      
        _imageLink = imageLink;
      
      }

    
      public int DocID
      {
      get { return _docID;}
      set { _docID = value; }
      }
    
      public bool IsPublic
      {
      get { return _isPublic;}
      set { _isPublic = value; }
      }
    
      public int? DocTypeId
      {
      get { return _docTypeId;}
      set { _docTypeId = value; }
      }
    
      public String Vol
      {
      get { return _vol;}
      set { _vol = value; }
      }
    
      public String Pg
      {
      get { return _pg;}
      set { _pg = value; }
      }
    
      public String DocumentNo
      {
      get { return _documentNo;}
      set { _documentNo = value; }
      }
    
      public String County
      {
      get { return _county;}
      set { _county = value; }
      }
    
      public String State
      {
      get { return _state;}
      set { _state = value; }
      }
    
      public DateTime? DateFiled
      {
      get { return _dateFiled;}
      set { _dateFiled = value; }
      }
    
      public DateTime? DateSigned
      {
      get { return _dateSigned;}
      set { _dateSigned = value; }
      }
    
      public String ResearchNote
      {
      get { return _researchNote;}
      set { _researchNote = value; }
      }
    
      public String ImageLink
      {
      get { return _imageLink;}
      set { _imageLink = value; }
      }
    
    
    }
  

    public partial class DocumentDataMapper:TDataMapper<Document,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "document";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into document (
    IsPublic,DocTypeId,Vol,Pg,DocumentNo,County,State,DateFiled,DateSigned,ResearchNote,ImageLink) Values (
    
      ?IsPublic,
      ?DocTypeId,
      ?Vol,
      ?Pg,
      ?DocumentNo,
      ?County,
      ?State,
      ?DateFiled,
      ?DateSigned,
      ?ResearchNote,
      ?ImageLink);";

    public override Document create( Document document )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?IsPublic", document.IsPublic);
                
                  if(document.DocTypeId != null)
                    sqlCommand.Parameters.Add("?DocTypeId", document.DocTypeId);
                  else
                    sqlCommand.Parameters.Add("?DocTypeId", DBNull.Value);
                
                  if(document.Vol != null)
                    sqlCommand.Parameters.Add("?Vol", document.Vol);
                  else
                    sqlCommand.Parameters.Add("?Vol", DBNull.Value);
                
                  if(document.Pg != null)
                    sqlCommand.Parameters.Add("?Pg", document.Pg);
                  else
                    sqlCommand.Parameters.Add("?Pg", DBNull.Value);
                
                  if(document.DocumentNo != null)
                    sqlCommand.Parameters.Add("?DocumentNo", document.DocumentNo);
                  else
                    sqlCommand.Parameters.Add("?DocumentNo", DBNull.Value);
                
                  if(document.County != null)
                    sqlCommand.Parameters.Add("?County", document.County);
                  else
                    sqlCommand.Parameters.Add("?County", DBNull.Value);
                
                  if(document.State != null)
                    sqlCommand.Parameters.Add("?State", document.State);
                  else
                    sqlCommand.Parameters.Add("?State", DBNull.Value);
                
                  if(document.DateFiled != null)
                    sqlCommand.Parameters.Add("?DateFiled", document.DateFiled);
                  else
                    sqlCommand.Parameters.Add("?DateFiled", DBNull.Value);
                
                  if(document.DateSigned != null)
                    sqlCommand.Parameters.Add("?DateSigned", document.DateSigned);
                  else
                    sqlCommand.Parameters.Add("?DateSigned", DBNull.Value);
                
                  if(document.ResearchNote != null)
                    sqlCommand.Parameters.Add("?ResearchNote", document.ResearchNote);
                  else
                    sqlCommand.Parameters.Add("?ResearchNote", DBNull.Value);
                
                  if(document.ImageLink != null)
                    sqlCommand.Parameters.Add("?ImageLink", document.ImageLink);
                  else
                    sqlCommand.Parameters.Add("?ImageLink", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                document.DocID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return document;
    }

  

    private const String SqlSelectAll = @"Select
    DocID,IsPublic,DocTypeId,Vol,Pg,DocumentNo,County,State,DateFiled,DateSigned,ResearchNote,ImageLink 
    From document ";
    
    public List<Document> findAll(Object args)
    {
      List<Document> rv = new List<Document>();

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
    DocID,IsPublic,DocTypeId,Vol,Pg,DocumentNo,County,State,DateFiled,DateSigned,ResearchNote,ImageLink
     From document
       Where 
      DocID = ?DocID
    ";

    public Document findByPrimaryKey(
    int docID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?DocID", docID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("document not found, search by primary key");
    }

    }


    public bool exists(Document document)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?DocID", document.DocID);
          

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
        

    return document;
    }


    protected override Document doLoad(Hashtable hashtable)
    {
      Document document = new Document();

      
        if(hashtable.ContainsKey("DocID"))
        document.DocID = (int) Convert.ChangeType(hashtable["DocID"], typeof(int));
          
        if(hashtable.ContainsKey("IsPublic"))
        document.IsPublic = (bool) Convert.ChangeType(hashtable["IsPublic"], typeof(bool));
          
        if(hashtable.ContainsKey("DocTypeId"))
        document.DocTypeId = (int) Convert.ChangeType(hashtable["DocTypeId"], typeof(int));
          
        if(hashtable.ContainsKey("Vol"))
        document.Vol = ( String )hashtable["Vol"];
          
        if(hashtable.ContainsKey("Pg"))
        document.Pg = ( String )hashtable["Pg"];
          
        if(hashtable.ContainsKey("DocumentNo"))
        document.DocumentNo = ( String )hashtable["DocumentNo"];
          
        if(hashtable.ContainsKey("County"))
        document.County = ( String )hashtable["County"];
          
        if(hashtable.ContainsKey("State"))
        document.State = ( String )hashtable["State"];
          
        if(hashtable.ContainsKey("DateFiled"))
        document.DateFiled = (DateTime) Convert.ChangeType(hashtable["DateFiled"], typeof(DateTime));
          
        if(hashtable.ContainsKey("DateSigned"))
        document.DateSigned = (DateTime) Convert.ChangeType(hashtable["DateSigned"], typeof(DateTime));
          
        if(hashtable.ContainsKey("ResearchNote"))
        document.ResearchNote = ( String )hashtable["ResearchNote"];
          
        if(hashtable.ContainsKey("ImageLink"))
        document.ImageLink = ( String )hashtable["ImageLink"];
          

      return document;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From document
      Where
      DocID = ?DocID";
    public Document remove(Document document)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?DocID", document.DocID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return document;
    }

    #endregion
  

    public Document save( Document document )
    {
      if(exists(document))
        return update(document);
        return create(document);
    }

  


    private const String SqlUpdate = @"Update document Set 
    IsPublic = ?IsPublic,DocTypeId = ?DocTypeId,Vol = ?Vol,Pg = ?Pg,DocumentNo = ?DocumentNo,County = ?County,State = ?State,DateFiled = ?DateFiled,DateSigned = ?DateSigned,ResearchNote = ?ResearchNote,ImageLink = ?ImageLink
       Where 
      DocID = ?DocID";

    public Document update(Document document)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?DocID", document.DocID);
              
                  sqlCommand.Parameters.Add("?IsPublic", document.IsPublic);
              
                if(document.DocTypeId != null)
                  sqlCommand.Parameters.Add("?DocTypeId", document.DocTypeId);
                else
                  sqlCommand.Parameters.Add("?DocTypeId", DBNull.Value);
              
                if(document.Vol != null)
                  sqlCommand.Parameters.Add("?Vol", document.Vol);
                else
                  sqlCommand.Parameters.Add("?Vol", DBNull.Value);
              
                if(document.Pg != null)
                  sqlCommand.Parameters.Add("?Pg", document.Pg);
                else
                  sqlCommand.Parameters.Add("?Pg", DBNull.Value);
              
                if(document.DocumentNo != null)
                  sqlCommand.Parameters.Add("?DocumentNo", document.DocumentNo);
                else
                  sqlCommand.Parameters.Add("?DocumentNo", DBNull.Value);
              
                if(document.County != null)
                  sqlCommand.Parameters.Add("?County", document.County);
                else
                  sqlCommand.Parameters.Add("?County", DBNull.Value);
              
                if(document.State != null)
                  sqlCommand.Parameters.Add("?State", document.State);
                else
                  sqlCommand.Parameters.Add("?State", DBNull.Value);
              
                if(document.DateFiled != null)
                  sqlCommand.Parameters.Add("?DateFiled", document.DateFiled);
                else
                  sqlCommand.Parameters.Add("?DateFiled", DBNull.Value);
              
                if(document.DateSigned != null)
                  sqlCommand.Parameters.Add("?DateSigned", document.DateSigned);
                else
                  sqlCommand.Parameters.Add("?DateSigned", DBNull.Value);
              
                if(document.ResearchNote != null)
                  sqlCommand.Parameters.Add("?ResearchNote", document.ResearchNote);
                else
                  sqlCommand.Parameters.Add("?ResearchNote", DBNull.Value);
              
                if(document.ImageLink != null)
                  sqlCommand.Parameters.Add("?ImageLink", document.ImageLink);
                else
                  sqlCommand.Parameters.Add("?ImageLink", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return document;
    }

  
    }
    
  
    
    public partial class Documenttype
    {
    
      protected int _docTypeID;
    
      protected String _name;
    

    public Documenttype(){}

    public Documenttype(
    int 
            docTypeID
    )
    {
    
      _docTypeID = docTypeID;
    
    }

    

      public Documenttype(
      int 
        docTypeID,String 
        name
      )
      {
      
        _docTypeID = docTypeID;
      
        _name = name;
      
      }

    
      public int DocTypeID
      {
      get { return _docTypeID;}
      set { _docTypeID = value; }
      }
    
      public String Name
      {
      get { return _name;}
      set { _name = value; }
      }
    
    
    }
  

    public partial class DocumenttypeDataMapper:TDataMapper<Documenttype,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "documenttype";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into documenttype (
    Name) Values (
    
      ?Name);";

    public override Documenttype create( Documenttype documenttype )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(documenttype.Name != null)
                    sqlCommand.Parameters.Add("?Name", documenttype.Name);
                  else
                    sqlCommand.Parameters.Add("?Name", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                documenttype.DocTypeID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return documenttype;
    }

  

    private const String SqlSelectAll = @"Select
    DocTypeID,Name 
    From documenttype ";
    
    public List<Documenttype> findAll(Object args)
    {
      List<Documenttype> rv = new List<Documenttype>();

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
    DocTypeID,Name
     From documenttype
       Where 
      DocTypeID = ?DocTypeID
    ";

    public Documenttype findByPrimaryKey(
    int docTypeID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?DocTypeID", docTypeID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("documenttype not found, search by primary key");
    }

    }


    public bool exists(Documenttype documenttype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?DocTypeID", documenttype.DocTypeID);
          

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
        

    return documenttype;
    }


    protected override Documenttype doLoad(Hashtable hashtable)
    {
      Documenttype documenttype = new Documenttype();

      
        if(hashtable.ContainsKey("DocTypeID"))
        documenttype.DocTypeID = (int) Convert.ChangeType(hashtable["DocTypeID"], typeof(int));
          
        if(hashtable.ContainsKey("Name"))
        documenttype.Name = ( String )hashtable["Name"];
          

      return documenttype;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From documenttype
      Where
      DocTypeID = ?DocTypeID";
    public Documenttype remove(Documenttype documenttype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?DocTypeID", documenttype.DocTypeID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return documenttype;
    }

    #endregion
  

    public Documenttype save( Documenttype documenttype )
    {
      if(exists(documenttype))
        return update(documenttype);
        return create(documenttype);
    }

  


    private const String SqlUpdate = @"Update documenttype Set 
    Name = ?Name
       Where 
      DocTypeID = ?DocTypeID";

    public Documenttype update(Documenttype documenttype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?DocTypeID", documenttype.DocTypeID);
              
                if(documenttype.Name != null)
                  sqlCommand.Parameters.Add("?Name", documenttype.Name);
                else
                  sqlCommand.Parameters.Add("?Name", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return documenttype;
    }

  
    }
    
  
    
    public partial class Participant
    {
    
      protected int _participantID;
    
      protected int? _docID;
    
      protected int? _docRoleID;
    
      protected String _asNamed;
    
      protected int? _phoneHome;
    
      protected int? _phoneOffice;
    
      protected int? _phoneCell;
    
      protected int? _phoneAlt;
    
      protected String _entityName;
    
      protected String _firstName;
    
      protected String _middleName;
    
      protected String _lastName;
    
      protected String _contactPosition;
    
      protected String _tAXID;
    
      protected String _sSN;
    
      protected int _parentID;
    
      protected int _typeID;
    

    public Participant(){}

    public Participant(
    int 
            participantID
    )
    {
    
      _participantID = participantID;
    
    }

    

      public Participant(
      int 
        participantID,int? 
        docID,int? 
        docRoleID,String 
        asNamed,int? 
        phoneHome,int? 
        phoneOffice,int? 
        phoneCell,int? 
        phoneAlt,String 
        entityName,String 
        firstName,String 
        middleName,String 
        lastName,String 
        contactPosition,String 
        tAXID,String 
        sSN,int 
        parentID,int 
        typeID
      )
      {
      
        _participantID = participantID;
      
        _docID = docID;
      
        _docRoleID = docRoleID;
      
        _asNamed = asNamed;
      
        _phoneHome = phoneHome;
      
        _phoneOffice = phoneOffice;
      
        _phoneCell = phoneCell;
      
        _phoneAlt = phoneAlt;
      
        _entityName = entityName;
      
        _firstName = firstName;
      
        _middleName = middleName;
      
        _lastName = lastName;
      
        _contactPosition = contactPosition;
      
        _tAXID = tAXID;
      
        _sSN = sSN;
      
        _parentID = parentID;
      
        _typeID = typeID;
      
      }

    
      public int ParticipantID
      {
      get { return _participantID;}
      set { _participantID = value; }
      }
    
      public int? DocID
      {
      get { return _docID;}
      set { _docID = value; }
      }
    
      public int? DocRoleID
      {
      get { return _docRoleID;}
      set { _docRoleID = value; }
      }
    
      public String AsNamed
      {
      get { return _asNamed;}
      set { _asNamed = value; }
      }
    
      public int? PhoneHome
      {
      get { return _phoneHome;}
      set { _phoneHome = value; }
      }
    
      public int? PhoneOffice
      {
      get { return _phoneOffice;}
      set { _phoneOffice = value; }
      }
    
      public int? PhoneCell
      {
      get { return _phoneCell;}
      set { _phoneCell = value; }
      }
    
      public int? PhoneAlt
      {
      get { return _phoneAlt;}
      set { _phoneAlt = value; }
      }
    
      public String EntityName
      {
      get { return _entityName;}
      set { _entityName = value; }
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
    
      public String ContactPosition
      {
      get { return _contactPosition;}
      set { _contactPosition = value; }
      }
    
      public String TAXID
      {
      get { return _tAXID;}
      set { _tAXID = value; }
      }
    
      public String SSN
      {
      get { return _sSN;}
      set { _sSN = value; }
      }
    
      public int ParentID
      {
      get { return _parentID;}
      set { _parentID = value; }
      }
    
      public int TypeID
      {
      get { return _typeID;}
      set { _typeID = value; }
      }
    
    
    }
  

    public partial class ParticipantDataMapper:TDataMapper<Participant,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "participant";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into participant (
    DocID,DocRoleID,AsNamed,PhoneHome,PhoneOffice,PhoneCell,PhoneAlt,EntityName,FirstName,MiddleName,LastName,ContactPosition,TAXID,SSN,ParentID,TypeID) Values (
    
      ?DocID,
      ?DocRoleID,
      ?AsNamed,
      ?PhoneHome,
      ?PhoneOffice,
      ?PhoneCell,
      ?PhoneAlt,
      ?EntityName,
      ?FirstName,
      ?MiddleName,
      ?LastName,
      ?ContactPosition,
      ?TAXID,
      ?SSN,
      ?ParentID,
      ?TypeID);";

    public override Participant create( Participant participant )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(participant.DocID != null)
                    sqlCommand.Parameters.Add("?DocID", participant.DocID);
                  else
                    sqlCommand.Parameters.Add("?DocID", DBNull.Value);
                
                  if(participant.DocRoleID != null)
                    sqlCommand.Parameters.Add("?DocRoleID", participant.DocRoleID);
                  else
                    sqlCommand.Parameters.Add("?DocRoleID", DBNull.Value);
                
                  if(participant.AsNamed != null)
                    sqlCommand.Parameters.Add("?AsNamed", participant.AsNamed);
                  else
                    sqlCommand.Parameters.Add("?AsNamed", DBNull.Value);
                
                  if(participant.PhoneHome != null)
                    sqlCommand.Parameters.Add("?PhoneHome", participant.PhoneHome);
                  else
                    sqlCommand.Parameters.Add("?PhoneHome", DBNull.Value);
                
                  if(participant.PhoneOffice != null)
                    sqlCommand.Parameters.Add("?PhoneOffice", participant.PhoneOffice);
                  else
                    sqlCommand.Parameters.Add("?PhoneOffice", DBNull.Value);
                
                  if(participant.PhoneCell != null)
                    sqlCommand.Parameters.Add("?PhoneCell", participant.PhoneCell);
                  else
                    sqlCommand.Parameters.Add("?PhoneCell", DBNull.Value);
                
                  if(participant.PhoneAlt != null)
                    sqlCommand.Parameters.Add("?PhoneAlt", participant.PhoneAlt);
                  else
                    sqlCommand.Parameters.Add("?PhoneAlt", DBNull.Value);
                
                  if(participant.EntityName != null)
                    sqlCommand.Parameters.Add("?EntityName", participant.EntityName);
                  else
                    sqlCommand.Parameters.Add("?EntityName", DBNull.Value);
                
                  if(participant.FirstName != null)
                    sqlCommand.Parameters.Add("?FirstName", participant.FirstName);
                  else
                    sqlCommand.Parameters.Add("?FirstName", DBNull.Value);
                
                  if(participant.MiddleName != null)
                    sqlCommand.Parameters.Add("?MiddleName", participant.MiddleName);
                  else
                    sqlCommand.Parameters.Add("?MiddleName", DBNull.Value);
                
                  if(participant.LastName != null)
                    sqlCommand.Parameters.Add("?LastName", participant.LastName);
                  else
                    sqlCommand.Parameters.Add("?LastName", DBNull.Value);
                
                  if(participant.ContactPosition != null)
                    sqlCommand.Parameters.Add("?ContactPosition", participant.ContactPosition);
                  else
                    sqlCommand.Parameters.Add("?ContactPosition", DBNull.Value);
                
                  if(participant.TAXID != null)
                    sqlCommand.Parameters.Add("?TAXID", participant.TAXID);
                  else
                    sqlCommand.Parameters.Add("?TAXID", DBNull.Value);
                
                  if(participant.SSN != null)
                    sqlCommand.Parameters.Add("?SSN", participant.SSN);
                  else
                    sqlCommand.Parameters.Add("?SSN", DBNull.Value);
                
                    sqlCommand.Parameters.Add("?ParentID", participant.ParentID);
                
                    sqlCommand.Parameters.Add("?TypeID", participant.TypeID);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                participant.ParticipantID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return participant;
    }

  

    private const String SqlSelectAll = @"Select
    ParticipantID,DocID,DocRoleID,AsNamed,PhoneHome,PhoneOffice,PhoneCell,PhoneAlt,EntityName,FirstName,MiddleName,LastName,ContactPosition,TAXID,SSN,ParentID,TypeID 
    From participant ";
    
    public List<Participant> findAll(Object args)
    {
      List<Participant> rv = new List<Participant>();

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
    ParticipantID,DocID,DocRoleID,AsNamed,PhoneHome,PhoneOffice,PhoneCell,PhoneAlt,EntityName,FirstName,MiddleName,LastName,ContactPosition,TAXID,SSN,ParentID,TypeID
     From participant
       Where 
      ParticipantID = ?ParticipantID
    ";

    public Participant findByPrimaryKey(
    int participantID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?ParticipantID", participantID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("participant not found, search by primary key");
    }

    }


    public bool exists(Participant participant)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?ParticipantID", participant.ParticipantID);
          

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
          participant.PhoneHome = dataReader.GetInt32(4);
        
          if(!dataReader.IsDBNull(5))
          participant.PhoneOffice = dataReader.GetInt32(5);
        
          if(!dataReader.IsDBNull(6))
          participant.PhoneCell = dataReader.GetInt32(6);
        
          if(!dataReader.IsDBNull(7))
          participant.PhoneAlt = dataReader.GetInt32(7);
        
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
        participant.TypeID = dataReader.GetInt32(16);
        

    return participant;
    }


    protected override Participant doLoad(Hashtable hashtable)
    {
      Participant participant = new Participant();

      
        if(hashtable.ContainsKey("ParticipantID"))
        participant.ParticipantID = (int) Convert.ChangeType(hashtable["ParticipantID"], typeof(int));
          
        if(hashtable.ContainsKey("DocID"))
        participant.DocID = (int) Convert.ChangeType(hashtable["DocID"], typeof(int));
          
        if(hashtable.ContainsKey("DocRoleID"))
        participant.DocRoleID = (int) Convert.ChangeType(hashtable["DocRoleID"], typeof(int));
          
        if(hashtable.ContainsKey("AsNamed"))
        participant.AsNamed = ( String )hashtable["AsNamed"];
          
        if(hashtable.ContainsKey("PhoneHome"))
        participant.PhoneHome = (int) Convert.ChangeType(hashtable["PhoneHome"], typeof(int));
          
        if(hashtable.ContainsKey("PhoneOffice"))
        participant.PhoneOffice = (int) Convert.ChangeType(hashtable["PhoneOffice"], typeof(int));
          
        if(hashtable.ContainsKey("PhoneCell"))
        participant.PhoneCell = (int) Convert.ChangeType(hashtable["PhoneCell"], typeof(int));
          
        if(hashtable.ContainsKey("PhoneAlt"))
        participant.PhoneAlt = (int) Convert.ChangeType(hashtable["PhoneAlt"], typeof(int));
          
        if(hashtable.ContainsKey("EntityName"))
        participant.EntityName = ( String )hashtable["EntityName"];
          
        if(hashtable.ContainsKey("FirstName"))
        participant.FirstName = ( String )hashtable["FirstName"];
          
        if(hashtable.ContainsKey("MiddleName"))
        participant.MiddleName = ( String )hashtable["MiddleName"];
          
        if(hashtable.ContainsKey("LastName"))
        participant.LastName = ( String )hashtable["LastName"];
          
        if(hashtable.ContainsKey("ContactPosition"))
        participant.ContactPosition = ( String )hashtable["ContactPosition"];
          
        if(hashtable.ContainsKey("TAXID"))
        participant.TAXID = ( String )hashtable["TAXID"];
          
        if(hashtable.ContainsKey("SSN"))
        participant.SSN = ( String )hashtable["SSN"];
          
        if(hashtable.ContainsKey("ParentID"))
        participant.ParentID = (int) Convert.ChangeType(hashtable["ParentID"], typeof(int));
          
        if(hashtable.ContainsKey("TypeID"))
        participant.TypeID = (int) Convert.ChangeType(hashtable["TypeID"], typeof(int));
          

      return participant;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From participant
      Where
      ParticipantID = ?ParticipantID";
    public Participant remove(Participant participant)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?ParticipantID", participant.ParticipantID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participant;
    }

    #endregion
  

    public Participant save( Participant participant )
    {
      if(exists(participant))
        return update(participant);
        return create(participant);
    }

  


    private const String SqlUpdate = @"Update participant Set 
    DocID = ?DocID,DocRoleID = ?DocRoleID,AsNamed = ?AsNamed,PhoneHome = ?PhoneHome,PhoneOffice = ?PhoneOffice,PhoneCell = ?PhoneCell,PhoneAlt = ?PhoneAlt,EntityName = ?EntityName,FirstName = ?FirstName,MiddleName = ?MiddleName,LastName = ?LastName,ContactPosition = ?ContactPosition,TAXID = ?TAXID,SSN = ?SSN,ParentID = ?ParentID,TypeID = ?TypeID
       Where 
      ParticipantID = ?ParticipantID";

    public Participant update(Participant participant)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?ParticipantID", participant.ParticipantID);
              
                if(participant.DocID != null)
                  sqlCommand.Parameters.Add("?DocID", participant.DocID);
                else
                  sqlCommand.Parameters.Add("?DocID", DBNull.Value);
              
                if(participant.DocRoleID != null)
                  sqlCommand.Parameters.Add("?DocRoleID", participant.DocRoleID);
                else
                  sqlCommand.Parameters.Add("?DocRoleID", DBNull.Value);
              
                if(participant.AsNamed != null)
                  sqlCommand.Parameters.Add("?AsNamed", participant.AsNamed);
                else
                  sqlCommand.Parameters.Add("?AsNamed", DBNull.Value);
              
                if(participant.PhoneHome != null)
                  sqlCommand.Parameters.Add("?PhoneHome", participant.PhoneHome);
                else
                  sqlCommand.Parameters.Add("?PhoneHome", DBNull.Value);
              
                if(participant.PhoneOffice != null)
                  sqlCommand.Parameters.Add("?PhoneOffice", participant.PhoneOffice);
                else
                  sqlCommand.Parameters.Add("?PhoneOffice", DBNull.Value);
              
                if(participant.PhoneCell != null)
                  sqlCommand.Parameters.Add("?PhoneCell", participant.PhoneCell);
                else
                  sqlCommand.Parameters.Add("?PhoneCell", DBNull.Value);
              
                if(participant.PhoneAlt != null)
                  sqlCommand.Parameters.Add("?PhoneAlt", participant.PhoneAlt);
                else
                  sqlCommand.Parameters.Add("?PhoneAlt", DBNull.Value);
              
                if(participant.EntityName != null)
                  sqlCommand.Parameters.Add("?EntityName", participant.EntityName);
                else
                  sqlCommand.Parameters.Add("?EntityName", DBNull.Value);
              
                if(participant.FirstName != null)
                  sqlCommand.Parameters.Add("?FirstName", participant.FirstName);
                else
                  sqlCommand.Parameters.Add("?FirstName", DBNull.Value);
              
                if(participant.MiddleName != null)
                  sqlCommand.Parameters.Add("?MiddleName", participant.MiddleName);
                else
                  sqlCommand.Parameters.Add("?MiddleName", DBNull.Value);
              
                if(participant.LastName != null)
                  sqlCommand.Parameters.Add("?LastName", participant.LastName);
                else
                  sqlCommand.Parameters.Add("?LastName", DBNull.Value);
              
                if(participant.ContactPosition != null)
                  sqlCommand.Parameters.Add("?ContactPosition", participant.ContactPosition);
                else
                  sqlCommand.Parameters.Add("?ContactPosition", DBNull.Value);
              
                if(participant.TAXID != null)
                  sqlCommand.Parameters.Add("?TAXID", participant.TAXID);
                else
                  sqlCommand.Parameters.Add("?TAXID", DBNull.Value);
              
                if(participant.SSN != null)
                  sqlCommand.Parameters.Add("?SSN", participant.SSN);
                else
                  sqlCommand.Parameters.Add("?SSN", DBNull.Value);
              
                  sqlCommand.Parameters.Add("?ParentID", participant.ParentID);
              
                  sqlCommand.Parameters.Add("?TypeID", participant.TypeID);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participant;
    }

  
    }
    
  
    
    public partial class Participantaddress
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
            addressID
    )
    {
    
      _addressID = addressID;
    
    }

    

      public Participantaddress(
      int 
        addressID,int? 
        participantlID,int? 
        addressTypeID,String 
        line1,String 
        line2,String 
        city,String 
        state,String 
        zip,String 
        incareof
      )
      {
      
        _addressID = addressID;
      
        _participantlID = participantlID;
      
        _addressTypeID = addressTypeID;
      
        _line1 = line1;
      
        _line2 = line2;
      
        _city = city;
      
        _state = state;
      
        _zip = zip;
      
        _incareof = incareof;
      
      }

    
      public int AddressID
      {
      get { return _addressID;}
      set { _addressID = value; }
      }
    
      public int? ParticipantlID
      {
      get { return _participantlID;}
      set { _participantlID = value; }
      }
    
      public int? AddressTypeID
      {
      get { return _addressTypeID;}
      set { _addressTypeID = value; }
      }
    
      public String Line1
      {
      get { return _line1;}
      set { _line1 = value; }
      }
    
      public String Line2
      {
      get { return _line2;}
      set { _line2 = value; }
      }
    
      public String City
      {
      get { return _city;}
      set { _city = value; }
      }
    
      public String State
      {
      get { return _state;}
      set { _state = value; }
      }
    
      public String Zip
      {
      get { return _zip;}
      set { _zip = value; }
      }
    
      public String Incareof
      {
      get { return _incareof;}
      set { _incareof = value; }
      }
    
    
    }
  

    public partial class ParticipantaddressDataMapper:TDataMapper<Participantaddress,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "participantaddress";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into participantaddress (
    ParticipantlID,AddressTypeID,Line1,Line2,City,State,Zip,Incareof) Values (
    
      ?ParticipantlID,
      ?AddressTypeID,
      ?Line1,
      ?Line2,
      ?City,
      ?State,
      ?Zip,
      ?Incareof);";

    public override Participantaddress create( Participantaddress participantaddress )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(participantaddress.ParticipantlID != null)
                    sqlCommand.Parameters.Add("?ParticipantlID", participantaddress.ParticipantlID);
                  else
                    sqlCommand.Parameters.Add("?ParticipantlID", DBNull.Value);
                
                  if(participantaddress.AddressTypeID != null)
                    sqlCommand.Parameters.Add("?AddressTypeID", participantaddress.AddressTypeID);
                  else
                    sqlCommand.Parameters.Add("?AddressTypeID", DBNull.Value);
                
                  if(participantaddress.Line1 != null)
                    sqlCommand.Parameters.Add("?Line1", participantaddress.Line1);
                  else
                    sqlCommand.Parameters.Add("?Line1", DBNull.Value);
                
                  if(participantaddress.Line2 != null)
                    sqlCommand.Parameters.Add("?Line2", participantaddress.Line2);
                  else
                    sqlCommand.Parameters.Add("?Line2", DBNull.Value);
                
                  if(participantaddress.City != null)
                    sqlCommand.Parameters.Add("?City", participantaddress.City);
                  else
                    sqlCommand.Parameters.Add("?City", DBNull.Value);
                
                  if(participantaddress.State != null)
                    sqlCommand.Parameters.Add("?State", participantaddress.State);
                  else
                    sqlCommand.Parameters.Add("?State", DBNull.Value);
                
                  if(participantaddress.Zip != null)
                    sqlCommand.Parameters.Add("?Zip", participantaddress.Zip);
                  else
                    sqlCommand.Parameters.Add("?Zip", DBNull.Value);
                
                  if(participantaddress.Incareof != null)
                    sqlCommand.Parameters.Add("?Incareof", participantaddress.Incareof);
                  else
                    sqlCommand.Parameters.Add("?Incareof", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                participantaddress.AddressID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return participantaddress;
    }

  

    private const String SqlSelectAll = @"Select
    AddressID,ParticipantlID,AddressTypeID,Line1,Line2,City,State,Zip,Incareof 
    From participantaddress ";
    
    public List<Participantaddress> findAll(Object args)
    {
      List<Participantaddress> rv = new List<Participantaddress>();

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
    AddressID,ParticipantlID,AddressTypeID,Line1,Line2,City,State,Zip,Incareof
     From participantaddress
       Where 
      AddressID = ?AddressID
    ";

    public Participantaddress findByPrimaryKey(
    int addressID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?AddressID", addressID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("participantaddress not found, search by primary key");
    }

    }


    public bool exists(Participantaddress participantaddress)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?AddressID", participantaddress.AddressID);
          

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
        

    return participantaddress;
    }


    protected override Participantaddress doLoad(Hashtable hashtable)
    {
      Participantaddress participantaddress = new Participantaddress();

      
        if(hashtable.ContainsKey("AddressID"))
        participantaddress.AddressID = (int) Convert.ChangeType(hashtable["AddressID"], typeof(int));
          
        if(hashtable.ContainsKey("ParticipantlID"))
        participantaddress.ParticipantlID = (int) Convert.ChangeType(hashtable["ParticipantlID"], typeof(int));
          
        if(hashtable.ContainsKey("AddressTypeID"))
        participantaddress.AddressTypeID = (int) Convert.ChangeType(hashtable["AddressTypeID"], typeof(int));
          
        if(hashtable.ContainsKey("Line1"))
        participantaddress.Line1 = ( String )hashtable["Line1"];
          
        if(hashtable.ContainsKey("Line2"))
        participantaddress.Line2 = ( String )hashtable["Line2"];
          
        if(hashtable.ContainsKey("City"))
        participantaddress.City = ( String )hashtable["City"];
          
        if(hashtable.ContainsKey("State"))
        participantaddress.State = ( String )hashtable["State"];
          
        if(hashtable.ContainsKey("Zip"))
        participantaddress.Zip = ( String )hashtable["Zip"];
          
        if(hashtable.ContainsKey("Incareof"))
        participantaddress.Incareof = ( String )hashtable["Incareof"];
          

      return participantaddress;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From participantaddress
      Where
      AddressID = ?AddressID";
    public Participantaddress remove(Participantaddress participantaddress)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?AddressID", participantaddress.AddressID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantaddress;
    }

    #endregion
  

    public Participantaddress save( Participantaddress participantaddress )
    {
      if(exists(participantaddress))
        return update(participantaddress);
        return create(participantaddress);
    }

  


    private const String SqlUpdate = @"Update participantaddress Set 
    ParticipantlID = ?ParticipantlID,AddressTypeID = ?AddressTypeID,Line1 = ?Line1,Line2 = ?Line2,City = ?City,State = ?State,Zip = ?Zip,Incareof = ?Incareof
       Where 
      AddressID = ?AddressID";

    public Participantaddress update(Participantaddress participantaddress)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?AddressID", participantaddress.AddressID);
              
                if(participantaddress.ParticipantlID != null)
                  sqlCommand.Parameters.Add("?ParticipantlID", participantaddress.ParticipantlID);
                else
                  sqlCommand.Parameters.Add("?ParticipantlID", DBNull.Value);
              
                if(participantaddress.AddressTypeID != null)
                  sqlCommand.Parameters.Add("?AddressTypeID", participantaddress.AddressTypeID);
                else
                  sqlCommand.Parameters.Add("?AddressTypeID", DBNull.Value);
              
                if(participantaddress.Line1 != null)
                  sqlCommand.Parameters.Add("?Line1", participantaddress.Line1);
                else
                  sqlCommand.Parameters.Add("?Line1", DBNull.Value);
              
                if(participantaddress.Line2 != null)
                  sqlCommand.Parameters.Add("?Line2", participantaddress.Line2);
                else
                  sqlCommand.Parameters.Add("?Line2", DBNull.Value);
              
                if(participantaddress.City != null)
                  sqlCommand.Parameters.Add("?City", participantaddress.City);
                else
                  sqlCommand.Parameters.Add("?City", DBNull.Value);
              
                if(participantaddress.State != null)
                  sqlCommand.Parameters.Add("?State", participantaddress.State);
                else
                  sqlCommand.Parameters.Add("?State", DBNull.Value);
              
                if(participantaddress.Zip != null)
                  sqlCommand.Parameters.Add("?Zip", participantaddress.Zip);
                else
                  sqlCommand.Parameters.Add("?Zip", DBNull.Value);
              
                if(participantaddress.Incareof != null)
                  sqlCommand.Parameters.Add("?Incareof", participantaddress.Incareof);
                else
                  sqlCommand.Parameters.Add("?Incareof", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantaddress;
    }

  
    }
    
  
    
    public partial class Participantentityparty
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
            participantEntityPartyID
    )
    {
    
      _participantEntityPartyID = participantEntityPartyID;
    
    }

    

      public Participantentityparty(
      int 
        participantEntityPartyID,int? 
        participantID,String 
        fName,String 
        mName,String 
        lName,String 
        sSN
      )
      {
      
        _participantEntityPartyID = participantEntityPartyID;
      
        _participantID = participantID;
      
        _fName = fName;
      
        _mName = mName;
      
        _lName = lName;
      
        _sSN = sSN;
      
      }

    
      public int ParticipantEntityPartyID
      {
      get { return _participantEntityPartyID;}
      set { _participantEntityPartyID = value; }
      }
    
      public int? ParticipantID
      {
      get { return _participantID;}
      set { _participantID = value; }
      }
    
      public String fName
      {
      get { return _fName;}
      set { _fName = value; }
      }
    
      public String mName
      {
      get { return _mName;}
      set { _mName = value; }
      }
    
      public String lName
      {
      get { return _lName;}
      set { _lName = value; }
      }
    
      public String SSN
      {
      get { return _sSN;}
      set { _sSN = value; }
      }
    
    
    }
  

    public partial class ParticipantentitypartyDataMapper:TDataMapper<Participantentityparty,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "participantentityparty";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into participantentityparty (
    ParticipantID,fName,mName,lName,SSN) Values (
    
      ?ParticipantID,
      ?fName,
      ?mName,
      ?lName,
      ?SSN);";

    public override Participantentityparty create( Participantentityparty participantentityparty )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(participantentityparty.ParticipantID != null)
                    sqlCommand.Parameters.Add("?ParticipantID", participantentityparty.ParticipantID);
                  else
                    sqlCommand.Parameters.Add("?ParticipantID", DBNull.Value);
                
                  if(participantentityparty.fName != null)
                    sqlCommand.Parameters.Add("?fName", participantentityparty.fName);
                  else
                    sqlCommand.Parameters.Add("?fName", DBNull.Value);
                
                  if(participantentityparty.mName != null)
                    sqlCommand.Parameters.Add("?mName", participantentityparty.mName);
                  else
                    sqlCommand.Parameters.Add("?mName", DBNull.Value);
                
                  if(participantentityparty.lName != null)
                    sqlCommand.Parameters.Add("?lName", participantentityparty.lName);
                  else
                    sqlCommand.Parameters.Add("?lName", DBNull.Value);
                
                  if(participantentityparty.SSN != null)
                    sqlCommand.Parameters.Add("?SSN", participantentityparty.SSN);
                  else
                    sqlCommand.Parameters.Add("?SSN", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                participantentityparty.ParticipantEntityPartyID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return participantentityparty;
    }

  

    private const String SqlSelectAll = @"Select
    ParticipantEntityPartyID,ParticipantID,fName,mName,lName,SSN 
    From participantentityparty ";
    
    public List<Participantentityparty> findAll(Object args)
    {
      List<Participantentityparty> rv = new List<Participantentityparty>();

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
    ParticipantEntityPartyID,ParticipantID,fName,mName,lName,SSN
     From participantentityparty
       Where 
      ParticipantEntityPartyID = ?ParticipantEntityPartyID
    ";

    public Participantentityparty findByPrimaryKey(
    int participantEntityPartyID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?ParticipantEntityPartyID", participantEntityPartyID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("participantentityparty not found, search by primary key");
    }

    }


    public bool exists(Participantentityparty participantentityparty)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?ParticipantEntityPartyID", participantentityparty.ParticipantEntityPartyID);
          

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
        

    return participantentityparty;
    }


    protected override Participantentityparty doLoad(Hashtable hashtable)
    {
      Participantentityparty participantentityparty = new Participantentityparty();

      
        if(hashtable.ContainsKey("ParticipantEntityPartyID"))
        participantentityparty.ParticipantEntityPartyID = (int) Convert.ChangeType(hashtable["ParticipantEntityPartyID"], typeof(int));
          
        if(hashtable.ContainsKey("ParticipantID"))
        participantentityparty.ParticipantID = (int) Convert.ChangeType(hashtable["ParticipantID"], typeof(int));
          
        if(hashtable.ContainsKey("fName"))
        participantentityparty.fName = ( String )hashtable["fName"];
          
        if(hashtable.ContainsKey("mName"))
        participantentityparty.mName = ( String )hashtable["mName"];
          
        if(hashtable.ContainsKey("lName"))
        participantentityparty.lName = ( String )hashtable["lName"];
          
        if(hashtable.ContainsKey("SSN"))
        participantentityparty.SSN = ( String )hashtable["SSN"];
          

      return participantentityparty;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From participantentityparty
      Where
      ParticipantEntityPartyID = ?ParticipantEntityPartyID";
    public Participantentityparty remove(Participantentityparty participantentityparty)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?ParticipantEntityPartyID", participantentityparty.ParticipantEntityPartyID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantentityparty;
    }

    #endregion
  

    public Participantentityparty save( Participantentityparty participantentityparty )
    {
      if(exists(participantentityparty))
        return update(participantentityparty);
        return create(participantentityparty);
    }

  


    private const String SqlUpdate = @"Update participantentityparty Set 
    ParticipantID = ?ParticipantID,fName = ?fName,mName = ?mName,lName = ?lName,SSN = ?SSN
       Where 
      ParticipantEntityPartyID = ?ParticipantEntityPartyID";

    public Participantentityparty update(Participantentityparty participantentityparty)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?ParticipantEntityPartyID", participantentityparty.ParticipantEntityPartyID);
              
                if(participantentityparty.ParticipantID != null)
                  sqlCommand.Parameters.Add("?ParticipantID", participantentityparty.ParticipantID);
                else
                  sqlCommand.Parameters.Add("?ParticipantID", DBNull.Value);
              
                if(participantentityparty.fName != null)
                  sqlCommand.Parameters.Add("?fName", participantentityparty.fName);
                else
                  sqlCommand.Parameters.Add("?fName", DBNull.Value);
              
                if(participantentityparty.mName != null)
                  sqlCommand.Parameters.Add("?mName", participantentityparty.mName);
                else
                  sqlCommand.Parameters.Add("?mName", DBNull.Value);
              
                if(participantentityparty.lName != null)
                  sqlCommand.Parameters.Add("?lName", participantentityparty.lName);
                else
                  sqlCommand.Parameters.Add("?lName", DBNull.Value);
              
                if(participantentityparty.SSN != null)
                  sqlCommand.Parameters.Add("?SSN", participantentityparty.SSN);
                else
                  sqlCommand.Parameters.Add("?SSN", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantentityparty;
    }

  
    }
    
  
    
    public partial class Participantreservation
    {
    
      protected int _docReservationID;
    
      protected int? _participantID;
    
      protected String _details;
    

    public Participantreservation(){}

    public Participantreservation(
    int 
            docReservationID
    )
    {
    
      _docReservationID = docReservationID;
    
    }

    

      public Participantreservation(
      int 
        docReservationID,int? 
        participantID,String 
        details
      )
      {
      
        _docReservationID = docReservationID;
      
        _participantID = participantID;
      
        _details = details;
      
      }

    
      public int DocReservationID
      {
      get { return _docReservationID;}
      set { _docReservationID = value; }
      }
    
      public int? ParticipantID
      {
      get { return _participantID;}
      set { _participantID = value; }
      }
    
      public String Details
      {
      get { return _details;}
      set { _details = value; }
      }
    
    
    }
  

    public partial class ParticipantreservationDataMapper:TDataMapper<Participantreservation,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "participantreservation";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into participantreservation (
    ParticipantID,Details) Values (
    
      ?ParticipantID,
      ?Details);";

    public override Participantreservation create( Participantreservation participantreservation )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(participantreservation.ParticipantID != null)
                    sqlCommand.Parameters.Add("?ParticipantID", participantreservation.ParticipantID);
                  else
                    sqlCommand.Parameters.Add("?ParticipantID", DBNull.Value);
                
                  if(participantreservation.Details != null)
                    sqlCommand.Parameters.Add("?Details", participantreservation.Details);
                  else
                    sqlCommand.Parameters.Add("?Details", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                participantreservation.DocReservationID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return participantreservation;
    }

  

    private const String SqlSelectAll = @"Select
    DocReservationID,ParticipantID,Details 
    From participantreservation ";
    
    public List<Participantreservation> findAll(Object args)
    {
      List<Participantreservation> rv = new List<Participantreservation>();

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
    DocReservationID,ParticipantID,Details
     From participantreservation
       Where 
      DocReservationID = ?DocReservationID
    ";

    public Participantreservation findByPrimaryKey(
    int docReservationID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?DocReservationID", docReservationID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("participantreservation not found, search by primary key");
    }

    }


    public bool exists(Participantreservation participantreservation)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?DocReservationID", participantreservation.DocReservationID);
          

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
        

    return participantreservation;
    }


    protected override Participantreservation doLoad(Hashtable hashtable)
    {
      Participantreservation participantreservation = new Participantreservation();

      
        if(hashtable.ContainsKey("DocReservationID"))
        participantreservation.DocReservationID = (int) Convert.ChangeType(hashtable["DocReservationID"], typeof(int));
          
        if(hashtable.ContainsKey("ParticipantID"))
        participantreservation.ParticipantID = (int) Convert.ChangeType(hashtable["ParticipantID"], typeof(int));
          
        if(hashtable.ContainsKey("Details"))
        participantreservation.Details = ( String )hashtable["Details"];
          

      return participantreservation;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From participantreservation
      Where
      DocReservationID = ?DocReservationID";
    public Participantreservation remove(Participantreservation participantreservation)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?DocReservationID", participantreservation.DocReservationID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantreservation;
    }

    #endregion
  

    public Participantreservation save( Participantreservation participantreservation )
    {
      if(exists(participantreservation))
        return update(participantreservation);
        return create(participantreservation);
    }

  


    private const String SqlUpdate = @"Update participantreservation Set 
    ParticipantID = ?ParticipantID,Details = ?Details
       Where 
      DocReservationID = ?DocReservationID";

    public Participantreservation update(Participantreservation participantreservation)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?DocReservationID", participantreservation.DocReservationID);
              
                if(participantreservation.ParticipantID != null)
                  sqlCommand.Parameters.Add("?ParticipantID", participantreservation.ParticipantID);
                else
                  sqlCommand.Parameters.Add("?ParticipantID", DBNull.Value);
              
                if(participantreservation.Details != null)
                  sqlCommand.Parameters.Add("?Details", participantreservation.Details);
                else
                  sqlCommand.Parameters.Add("?Details", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantreservation;
    }

  
    }
    
  
    
    public partial class Participantrole
    {
    
      protected int _docRoleID;
    
      protected String _roleName;
    
      protected int _docTypeID;
    
      protected bool _isSeller;
    

    public Participantrole(){}

    public Participantrole(
    int 
            docRoleID
    )
    {
    
      _docRoleID = docRoleID;
    
    }

    

      public Participantrole(
      int 
        docRoleID,String 
        roleName,int 
        docTypeID,bool 
        isSeller
      )
      {
      
        _docRoleID = docRoleID;
      
        _roleName = roleName;
      
        _docTypeID = docTypeID;
      
        _isSeller = isSeller;
      
      }

    
      public int DocRoleID
      {
      get { return _docRoleID;}
      set { _docRoleID = value; }
      }
    
      public String RoleName
      {
      get { return _roleName;}
      set { _roleName = value; }
      }
    
      public int DocTypeID
      {
      get { return _docTypeID;}
      set { _docTypeID = value; }
      }
    
      public bool IsSeller
      {
      get { return _isSeller;}
      set { _isSeller = value; }
      }
    
    
    }
  

    public partial class ParticipantroleDataMapper:TDataMapper<Participantrole,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "participantrole";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into participantrole (
    RoleName,DocTypeID,IsSeller) Values (
    
      ?RoleName,
      ?DocTypeID,
      ?IsSeller);";

    public override Participantrole create( Participantrole participantrole )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(participantrole.RoleName != null)
                    sqlCommand.Parameters.Add("?RoleName", participantrole.RoleName);
                  else
                    sqlCommand.Parameters.Add("?RoleName", DBNull.Value);
                
                    sqlCommand.Parameters.Add("?DocTypeID", participantrole.DocTypeID);
                
                    sqlCommand.Parameters.Add("?IsSeller", participantrole.IsSeller);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                participantrole.DocRoleID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return participantrole;
    }

  

    private const String SqlSelectAll = @"Select
    DocRoleID,RoleName,DocTypeID,IsSeller 
    From participantrole ";
    
    public List<Participantrole> findAll(Object args)
    {
      List<Participantrole> rv = new List<Participantrole>();

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
    DocRoleID,RoleName,DocTypeID,IsSeller
     From participantrole
       Where 
      DocRoleID = ?DocRoleID
    ";

    public Participantrole findByPrimaryKey(
    int docRoleID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?DocRoleID", docRoleID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("participantrole not found, search by primary key");
    }

    }


    public bool exists(Participantrole participantrole)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?DocRoleID", participantrole.DocRoleID);
          

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
        participantrole.DocTypeID = dataReader.GetInt32(2);
        participantrole.IsSeller = dataReader.GetBoolean(3);
        

    return participantrole;
    }


    protected override Participantrole doLoad(Hashtable hashtable)
    {
      Participantrole participantrole = new Participantrole();

      
        if(hashtable.ContainsKey("DocRoleID"))
        participantrole.DocRoleID = (int) Convert.ChangeType(hashtable["DocRoleID"], typeof(int));
          
        if(hashtable.ContainsKey("RoleName"))
        participantrole.RoleName = ( String )hashtable["RoleName"];
          
        if(hashtable.ContainsKey("DocTypeID"))
        participantrole.DocTypeID = (int) Convert.ChangeType(hashtable["DocTypeID"], typeof(int));
          
        if(hashtable.ContainsKey("IsSeller"))
        participantrole.IsSeller = (bool) Convert.ChangeType(hashtable["IsSeller"], typeof(bool));
          

      return participantrole;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From participantrole
      Where
      DocRoleID = ?DocRoleID";
    public Participantrole remove(Participantrole participantrole)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?DocRoleID", participantrole.DocRoleID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantrole;
    }

    #endregion
  

    public Participantrole save( Participantrole participantrole )
    {
      if(exists(participantrole))
        return update(participantrole);
        return create(participantrole);
    }

  


    private const String SqlUpdate = @"Update participantrole Set 
    RoleName = ?RoleName,DocTypeID = ?DocTypeID,IsSeller = ?IsSeller
       Where 
      DocRoleID = ?DocRoleID";

    public Participantrole update(Participantrole participantrole)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?DocRoleID", participantrole.DocRoleID);
              
                if(participantrole.RoleName != null)
                  sqlCommand.Parameters.Add("?RoleName", participantrole.RoleName);
                else
                  sqlCommand.Parameters.Add("?RoleName", DBNull.Value);
              
                  sqlCommand.Parameters.Add("?DocTypeID", participantrole.DocTypeID);
              
                  sqlCommand.Parameters.Add("?IsSeller", participantrole.IsSeller);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participantrole;
    }

  
    }
    
  
    
    public partial class Participanttype
    {
    
      protected int _typeID;
    
      protected String _name;
    

    public Participanttype(){}

    public Participanttype(
    int 
            typeID
    )
    {
    
      _typeID = typeID;
    
    }

    

      public Participanttype(
      int 
        typeID,String 
        name
      )
      {
      
        _typeID = typeID;
      
        _name = name;
      
      }

    
      public int TypeID
      {
      get { return _typeID;}
      set { _typeID = value; }
      }
    
      public String Name
      {
      get { return _name;}
      set { _name = value; }
      }
    
    
    }
  

    public partial class ParticipanttypeDataMapper:TDataMapper<Participanttype,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "participanttype";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into participanttype (
    Name) Values (
    
      ?Name);";

    public override Participanttype create( Participanttype participanttype )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                    sqlCommand.Parameters.Add("?Name", participanttype.Name);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                participanttype.TypeID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return participanttype;
    }

  

    private const String SqlSelectAll = @"Select
    TypeID,Name 
    From participanttype ";
    
    public List<Participanttype> findAll(Object args)
    {
      List<Participanttype> rv = new List<Participanttype>();

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
    TypeID,Name
     From participanttype
       Where 
      TypeID = ?TypeID
    ";

    public Participanttype findByPrimaryKey(
    int typeID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?TypeID", typeID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("participanttype not found, search by primary key");
    }

    }


    public bool exists(Participanttype participanttype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?TypeID", participanttype.TypeID);
          

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
        

    return participanttype;
    }


    protected override Participanttype doLoad(Hashtable hashtable)
    {
      Participanttype participanttype = new Participanttype();

      
        if(hashtable.ContainsKey("TypeID"))
        participanttype.TypeID = (int) Convert.ChangeType(hashtable["TypeID"], typeof(int));
          
        if(hashtable.ContainsKey("Name"))
        participanttype.Name = ( String )hashtable["Name"];
          

      return participanttype;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From participanttype
      Where
      TypeID = ?TypeID";
    public Participanttype remove(Participanttype participanttype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?TypeID", participanttype.TypeID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participanttype;
    }

    #endregion
  

    public Participanttype save( Participanttype participanttype )
    {
      if(exists(participanttype))
        return update(participanttype);
        return create(participanttype);
    }

  


    private const String SqlUpdate = @"Update participanttype Set 
    Name = ?Name
       Where 
      TypeID = ?TypeID";

    public Participanttype update(Participanttype participanttype)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?TypeID", participanttype.TypeID);
              
                  sqlCommand.Parameters.Add("?Name", participanttype.Name);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return participanttype;
    }

  
    }
    
  
    
    public partial class Tract
    {
    
      protected int _tractID;
    
      protected int? _docID;
    
      protected String _refName;
    
      protected decimal? _calledAC;
    
      protected String _scopePlotUrl;
    

    public Tract(){}

    public Tract(
    int 
            tractID
    )
    {
    
      _tractID = tractID;
    
    }

    

      public Tract(
      int 
        tractID,int? 
        docID,String 
        refName,decimal 
        calledAC,String 
        scopePlotUrl
      )
      {
      
        _tractID = tractID;
      
        _docID = docID;
      
        _refName = refName;
      
        _calledAC = calledAC;
      
        _scopePlotUrl = scopePlotUrl;
      
      }

    
      public int TractID
      {
      get { return _tractID;}
      set { _tractID = value; }
      }
    
      public int? DocID
      {
      get { return _docID;}
      set { _docID = value; }
      }
    
      public String RefName
      {
      get { return _refName;}
      set { _refName = value; }
      }
    
      public decimal? CalledAC
      {
      get { return _calledAC;}
      set { _calledAC = value; }
      }
    
      public String ScopePlotUrl
      {
      get { return _scopePlotUrl;}
      set { _scopePlotUrl = value; }
      }
    
    
    }
  

    public partial class TractDataMapper:TDataMapper<Tract,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "tract";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into tract (
    DocID,RefName,CalledAC,ScopePlotUrl) Values (
    
      ?DocID,
      ?RefName,
      ?CalledAC,
      ?ScopePlotUrl);";

    public override Tract create( Tract tract )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(tract.DocID != null)
                    sqlCommand.Parameters.Add("?DocID", tract.DocID);
                  else
                    sqlCommand.Parameters.Add("?DocID", DBNull.Value);
                
                  if(tract.RefName != null)
                    sqlCommand.Parameters.Add("?RefName", tract.RefName);
                  else
                    sqlCommand.Parameters.Add("?RefName", DBNull.Value);
                
                  if(tract.CalledAC != null)
                    sqlCommand.Parameters.Add("?CalledAC", tract.CalledAC);
                  else
                    sqlCommand.Parameters.Add("?CalledAC", DBNull.Value);
                
                  if(tract.ScopePlotUrl != null)
                    sqlCommand.Parameters.Add("?ScopePlotUrl", tract.ScopePlotUrl);
                  else
                    sqlCommand.Parameters.Add("?ScopePlotUrl", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                tract.TractID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return tract;
    }

  

    private const String SqlSelectAll = @"Select
    TractID,DocID,RefName,CalledAC,ScopePlotUrl 
    From tract ";
    
    public List<Tract> findAll(Object args)
    {
      List<Tract> rv = new List<Tract>();

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
    TractID,DocID,RefName,CalledAC,ScopePlotUrl
     From tract
       Where 
      TractID = ?TractID
    ";

    public Tract findByPrimaryKey(
    int tractID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?TractID", tractID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("tract not found, search by primary key");
    }

    }


    public bool exists(Tract tract)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?TractID", tract.TractID);
          

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
        

    return tract;
    }


    protected override Tract doLoad(Hashtable hashtable)
    {
      Tract tract = new Tract();

      
        if(hashtable.ContainsKey("TractID"))
        tract.TractID = (int) Convert.ChangeType(hashtable["TractID"], typeof(int));
          
        if(hashtable.ContainsKey("DocID"))
        tract.DocID = (int) Convert.ChangeType(hashtable["DocID"], typeof(int));
          
        if(hashtable.ContainsKey("RefName"))
        tract.RefName = ( String )hashtable["RefName"];
          
        if(hashtable.ContainsKey("CalledAC"))
        tract.CalledAC = (decimal) Convert.ChangeType(hashtable["CalledAC"], typeof(decimal));
          
        if(hashtable.ContainsKey("ScopePlotUrl"))
        tract.ScopePlotUrl = ( String )hashtable["ScopePlotUrl"];
          

      return tract;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From tract
      Where
      TractID = ?TractID";
    public Tract remove(Tract tract)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?TractID", tract.TractID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return tract;
    }

    #endregion
  

    public Tract save( Tract tract )
    {
      if(exists(tract))
        return update(tract);
        return create(tract);
    }

  


    private const String SqlUpdate = @"Update tract Set 
    DocID = ?DocID,RefName = ?RefName,CalledAC = ?CalledAC,ScopePlotUrl = ?ScopePlotUrl
       Where 
      TractID = ?TractID";

    public Tract update(Tract tract)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?TractID", tract.TractID);
              
                if(tract.DocID != null)
                  sqlCommand.Parameters.Add("?DocID", tract.DocID);
                else
                  sqlCommand.Parameters.Add("?DocID", DBNull.Value);
              
                if(tract.RefName != null)
                  sqlCommand.Parameters.Add("?RefName", tract.RefName);
                else
                  sqlCommand.Parameters.Add("?RefName", DBNull.Value);
              
                if(tract.CalledAC != null)
                  sqlCommand.Parameters.Add("?CalledAC", tract.CalledAC);
                else
                  sqlCommand.Parameters.Add("?CalledAC", DBNull.Value);
              
                if(tract.ScopePlotUrl != null)
                  sqlCommand.Parameters.Add("?ScopePlotUrl", tract.ScopePlotUrl);
                else
                  sqlCommand.Parameters.Add("?ScopePlotUrl", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return tract;
    }

  
    }
    
  
    
    public partial class Tractexception
    {
    
      protected int _tractExceptionsID;
    
      protected int? _tractID;
    
      protected String _refName;
    
      protected String _calledAC;
    

    public Tractexception(){}

    public Tractexception(
    int 
            tractExceptionsID
    )
    {
    
      _tractExceptionsID = tractExceptionsID;
    
    }

    

      public Tractexception(
      int 
        tractExceptionsID,int? 
        tractID,String 
        refName,String 
        calledAC
      )
      {
      
        _tractExceptionsID = tractExceptionsID;
      
        _tractID = tractID;
      
        _refName = refName;
      
        _calledAC = calledAC;
      
      }

    
      public int TractExceptionsID
      {
      get { return _tractExceptionsID;}
      set { _tractExceptionsID = value; }
      }
    
      public int? TractID
      {
      get { return _tractID;}
      set { _tractID = value; }
      }
    
      public String RefName
      {
      get { return _refName;}
      set { _refName = value; }
      }
    
      public String CalledAC
      {
      get { return _calledAC;}
      set { _calledAC = value; }
      }
    
    
    }
  

    public partial class TractexceptionDataMapper:TDataMapper<Tractexception,MySqlConnection, CommandBuilder>
    {
      public override String TableName
      {
        get
        {
          return "tractexception";
        }
      }

      public override Hashtable getRelation(string tableName)
      {
        throw new Exception("Not yet implemented");
      }

        

    private const String SqlCreate = @"Insert Into tractexception (
    TractID,RefName,CalledAC) Values (
    
      ?TractID,
      ?RefName,
      ?CalledAC);";

    public override Tractexception create( Tractexception tractexception )
    {
      using(Database database = new Database())
      {
          using(MySqlCommand sqlCommand = new MySqlCommand( SqlCreate , database.Connection ))
          {
            
                  if(tractexception.TractID != null)
                    sqlCommand.Parameters.Add("?TractID", tractexception.TractID);
                  else
                    sqlCommand.Parameters.Add("?TractID", DBNull.Value);
                
                  if(tractexception.RefName != null)
                    sqlCommand.Parameters.Add("?RefName", tractexception.RefName);
                  else
                    sqlCommand.Parameters.Add("?RefName", DBNull.Value);
                
                  if(tractexception.CalledAC != null)
                    sqlCommand.Parameters.Add("?CalledAC", tractexception.CalledAC);
                  else
                    sqlCommand.Parameters.Add("?CalledAC", DBNull.Value);
                
    
            sqlCommand.ExecuteNonQuery();

              
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select @@Identity  as NewId";
                
                tractexception.TractExceptionsID = int.Parse( sqlCommand.ExecuteScalar().ToString()) ;
              
            }
        }

      return tractexception;
    }

  

    private const String SqlSelectAll = @"Select
    TractExceptionsID,TractID,RefName,CalledAC 
    From tractexception ";
    
    public List<Tractexception> findAll(Object args)
    {
      List<Tractexception> rv = new List<Tractexception>();

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
    TractExceptionsID,TractID,RefName,CalledAC
     From tractexception
       Where 
      TractExceptionsID = ?TractExceptionsID
    ";

    public Tractexception findByPrimaryKey(
    int tractExceptionsID
    )
    {
    
    using(Database database = new Database())
    {
      using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
      {
        
          sqlCommand.Parameters.Add("?TractExceptionsID", tractExceptionsID);
        

        using(IDataReader dataReader = sqlCommand.ExecuteReader())
        {
          if(dataReader.Read())
            return doLoad(dataReader);
        }
      }
      
      throw new DataNotFoundException("tractexception not found, search by primary key");
    }

    }


    public bool exists(Tractexception tractexception)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlSelectByPk,database.Connection))
        {
          
            sqlCommand.Parameters.Add("?TractExceptionsID", tractexception.TractExceptionsID);
          

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
        

    return tractexception;
    }


    protected override Tractexception doLoad(Hashtable hashtable)
    {
      Tractexception tractexception = new Tractexception();

      
        if(hashtable.ContainsKey("TractExceptionsID"))
        tractexception.TractExceptionsID = (int) Convert.ChangeType(hashtable["TractExceptionsID"], typeof(int));
          
        if(hashtable.ContainsKey("TractID"))
        tractexception.TractID = (int) Convert.ChangeType(hashtable["TractID"], typeof(int));
          
        if(hashtable.ContainsKey("RefName"))
        tractexception.RefName = ( String )hashtable["RefName"];
          
        if(hashtable.ContainsKey("CalledAC"))
        tractexception.CalledAC = ( String )hashtable["CalledAC"];
          

      return tractexception;
    }
  

    #region Delete
    private const String SqlDelete = @"Delete From tractexception
      Where
      TractExceptionsID = ?TractExceptionsID";
    public Tractexception remove(Tractexception tractexception)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlDelete,database.Connection))
        {
        
          sqlCommand.Parameters.Add("?TractExceptionsID", tractexception.TractExceptionsID);
        
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return tractexception;
    }

    #endregion
  

    public Tractexception save( Tractexception tractexception )
    {
      if(exists(tractexception))
        return update(tractexception);
        return create(tractexception);
    }

  


    private const String SqlUpdate = @"Update tractexception Set 
    TractID = ?TractID,RefName = ?RefName,CalledAC = ?CalledAC
       Where 
      TractExceptionsID = ?TractExceptionsID";

    public Tractexception update(Tractexception tractexception)
    {
      using(Database database = new Database())
      {
        using(MySqlCommand sqlCommand = new MySqlCommand(SqlUpdate, database.Connection))
        {
          
                  sqlCommand.Parameters.Add("?TractExceptionsID", tractexception.TractExceptionsID);
              
                if(tractexception.TractID != null)
                  sqlCommand.Parameters.Add("?TractID", tractexception.TractID);
                else
                  sqlCommand.Parameters.Add("?TractID", DBNull.Value);
              
                if(tractexception.RefName != null)
                  sqlCommand.Parameters.Add("?RefName", tractexception.RefName);
                else
                  sqlCommand.Parameters.Add("?RefName", DBNull.Value);
              
                if(tractexception.CalledAC != null)
                  sqlCommand.Parameters.Add("?CalledAC", tractexception.CalledAC);
                else
                  sqlCommand.Parameters.Add("?CalledAC", DBNull.Value);
              

    
          sqlCommand.ExecuteNonQuery();
        }
      }
      
      return tractexception;
    }

  
    }
    
  
      }
    