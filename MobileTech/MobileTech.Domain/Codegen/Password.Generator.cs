
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Password
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Password ( " +
      
        " Functionality, " +
      
        " PasswordId, " +
      
        " PasswordValue " +
      
      ") Values (" +
      
        " @Functionality, " +
      
        " @PasswordId, " +
      
        " @PasswordValue " +
      
      ")";

      public static void Insert(Password password)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@Functionality", password.Functionality);
      
        Database.PutParameter(dbCommand,"@PasswordId", password.PasswordId);
      
        Database.PutParameter(dbCommand,"@PasswordValue", password.PasswordValue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Password>  passwordList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Password password in  passwordList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@Functionality", password.Functionality);
      
        Database.PutParameter(dbCommand,"@PasswordId", password.PasswordId);
      
        Database.PutParameter(dbCommand,"@PasswordValue", password.PasswordValue);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@Functionality",password.Functionality);
      
        Database.UpdateParameter(dbCommand,"@PasswordId",password.PasswordId);
      
        Database.UpdateParameter(dbCommand,"@PasswordValue",password.PasswordValue);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Password Set "
      
        + " PasswordValue = @PasswordValue "
      
        + " Where "
        
          + " Functionality = @Functionality and  "
        
          + " PasswordId = @PasswordId "
        
      ;

      public static void Update(Password password)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@Functionality", password.Functionality);
      
        Database.PutParameter(dbCommand,"@PasswordId", password.PasswordId);
      
        Database.PutParameter(dbCommand,"@PasswordValue", password.PasswordValue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Functionality, "
      
        + " PasswordId, "
      
        + " PasswordValue "
      

      + " From Password "

      
        + " Where "
        
          + " Functionality = @Functionality and  "
        
          + " PasswordId = @PasswordId "
        
      ;

      public static Password FindByPrimaryKey(
      String functionality,int passwordId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@Functionality", functionality);
      
        Database.PutParameter(dbCommand,"@PasswordId", passwordId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Password not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Password password)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@Functionality",password.Functionality);
      
        Database.PutParameter(dbCommand,"@PasswordId",password.PasswordId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from Password";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static Password Load(IDataReader dataReader)
      {
      Password password = new Password();

      password.Functionality = dataReader.GetString(0);
          password.PasswordId = dataReader.GetInt32(1);
          password.PasswordValue = dataReader.GetString(2);
          

      return password;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Password "

      
        + " Where "
        
          + " Functionality = @Functionality and  "
        
          + " PasswordId = @PasswordId "
        
      ;
      public static void Delete(Password password)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@Functionality", password.Functionality);
      
        Database.PutParameter(dbCommand,"@PasswordId", password.PasswordId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Password ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " Functionality, "
      
        + " PasswordId, "
      
        + " PasswordValue "
      

      + " From Password ";
      public static List<Password> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<Password> rv = new List<Password>();

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
      #endregion

      #region Import from file
      
      public static int Import(String xmlFilePath)
      {
        List<Password> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<Password> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Password));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(Password item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Password>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Password));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<Password> itemsList
      = new List<Password>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Password)
        itemsList.Add(deserializedObject as Password);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected String m_functionality;
        
          protected int m_passwordId;
        
          protected String m_passwordValue;
        
        #endregion
        
        #region Constructors
        public Password(
        String 
          functionality,int 
          passwordId
         )
        {
        
          m_functionality = functionality;
        
          m_passwordId = passwordId;
        
        }
        
        


        public Password(
        String 
          functionality,int 
          passwordId,String 
          passwordValue
        )
        {
        
          m_functionality = functionality;
        
          m_passwordId = passwordId;
        
          m_passwordValue = passwordValue;
        
          }


        
      #endregion

      
        [XmlElement]
        public String Functionality
        {
          get { return m_functionality;}
          set { m_functionality = value; }
        }
      
        [XmlElement]
        public int PasswordId
        {
          get { return m_passwordId;}
          set { m_passwordId = value; }
        }
      
        [XmlElement]
        public String PasswordValue
        {
          get { return m_passwordValue;}
          set { m_passwordValue = value; }
        }
      
      }
      #endregion
      }

    