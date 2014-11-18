<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" 
  xmlns:codegen="urn:cogegen-xslt-lib:xslt"
  xmlns:xs="http://www.w3.org/2001/XMLSchema" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" encoding="UTF-8" />

  <xsl:key match="/xs:schema/xs:element/xs:key/xs:field" name="pk" use="../../@name"/>


  <xsl:template match="/">
    <codegen>
      <xsl:for-each select="/xs:schema/xs:element">
        <xsl:call-template name="files" />
      </xsl:for-each>

      <file name="codegen\DalworthDB.html" overwrite="true" type="index">
        &lt;html&gt;
        <xsl:for-each select="/xs:schema/xs:element">
          <xsl:sort select="@name"/>
          <xsl:variable name="table" select="@name" />
          &lt;table width="600" border="1" style="border-collapse:collapse;" &gt;
            &lt;tr&gt;
          &lt;td colspan="3" bgcolor="#CFCFCF" &gt;
          &lt;b&gt;<xsl:value-of select="@name"/>&lt;/b&gt;
          &lt;/td&gt;
          &lt;/tr&gt;
          <xsl:for-each select="xs:complexType/xs:attribute">
            &lt;tr&gt;
            &lt;td width="300"&gt;
                <xsl:value-of select="@name" />
                <xsl:if test="concat('@',@name) = key('pk',$table)/@xpath">
                  (PK)
                </xsl:if>
            &lt;/td&gt;
          &lt;td width="100"&gt;
          <xsl:value-of select="@type" />
          &lt;/td&gt;
          &lt;td align="center" &gt; - &lt;/td&gt;
          &lt;/tr&gt;
          </xsl:for-each>
          &lt;/table&gt;
          &lt;br /&gt;
          </xsl:for-each>
        &lt;/html&gt;
      </file>

      <file name="codegen\Import.Codegen.cs" overwrite="true" type="index">
        namespace Dalworth.Server.Domain
        {
        <xsl:call-template name="using.store" />
        using System.IO;

        public class Import:Task
        {

        public Import(){}
        public Import(String path)
        {
        importFolder = path;
        }


        private String importFolder;

        public String ImportFolder
        {
        get { return importFolder; }
        set { importFolder = value; }
        }

        private int insertedRows;

        public int InsertedRows
        {
        get { return insertedRows; }
        set { insertedRows = value; }
        }

        private bool clear;
        public bool Clear
        {
        get {return clear;}
        set {clear = value;}

        }

        protected override void Main()
        {
        Database.Begin();
        try
        {

        String filePath = String.Empty;
        insertedRows = 0;

        #region Cleaning
        if(Clear)
        {
        <xsl:for-each select="/xs:schema/xs:element">
          <xsl:sort order="descending" data-type="number" select="number(substring(@id,2))"/>
          #region <xsl:value-of select="@name" />
          AddMessage("Removing <xsl:value-of select="@name" />", <xsl:value-of select="format-number(position() div count(../xs:element) * 100,'#')" />);
          <xsl:value-of select="@name" />.Clear();
          #endregion
        </xsl:for-each>
        }
        #endregion

        <xsl:for-each select="/xs:schema/xs:element">
          #region <xsl:value-of select="@name" />

          filePath = String.Format(@"{0}\<xsl:value-of select="@name" />.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing <xsl:value-of select="@name" />", <xsl:value-of select="format-number(position() div count(../xs:element) * 100,'#')" />);

          insertedRows += <xsl:value-of select="@name" />.Import(filePath);
          }
          #endregion
        </xsl:for-each>

        Database.Commit();

        AddMessage("Complete",100);

        }catch(Exception e)
        {
        Database.Rollback();
        AddMessage("Complete with errors",100);
        throw e;
        }
        }
        }
        }
      </file>

      <file name="codegen\Export.Codegen.cs" overwrite="true" type="index">
        namespace Dalworth.Server.Domain
        {
        <xsl:call-template name="using.store" />
        using System.IO;

        public class Export:Task
        {

        public Export(){}
        public Export(String path)
        {
        exportFolder = path;
        }


        private String exportFolder;

        public String ExportFolder
        {
        get { return exportFolder; }
        set { exportFolder = value; }
        }

        private int exportedRows;

        public int ExportedRows
        {
        get { return exportedRows; }
        set { exportedRows = value; }
        }

        protected override void Main()
        {

        try
        {

        Database.Begin();

        String filePath = String.Empty;

        exportedRows = 0;

        <xsl:for-each select="/xs:schema/xs:element">
          #region <xsl:value-of select="@name" />

          filePath = String.Format(@"{0}\<xsl:value-of select="@name" />.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting <xsl:value-of select="@name" />", <xsl:value-of select="format-number(position() div count(../xs:element) * 100,'#')" />);

          exportedRows += <xsl:value-of select="@name" />.Export(filePath);

          #endregion
        </xsl:for-each>

        Database.Commit();

        AddMessage("Complete",100);


        }catch(Exception e)
        {
        Database.Rollback();
        AddMessage("Complete with errors",100);

        throw e;
        }
        }
        }
        }
      </file>
    </codegen>
  </xsl:template>

  <xsl:template name="using.biz">
    using System;
  </xsl:template>

  <xsl:template name="using.store">
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  </xsl:template>


  <xsl:template name="files">


    <xsl:variable name="biz-name" select="@name"   />
    <xsl:variable name="store-name" select="@name"   />
    <xsl:variable name="table" select="@name"   />
    <xsl:variable name="functionParam" select="codegen:FunctionParameter(@name)" />

    <file name="codegen\{@name}.Generator.cs" type="store" overwrite="true">

      <xsl:value-of select="codegen:Progress(concat('Generating store class from ', @name))" />

      <xsl:call-template name="using.store" />
      namespace Dalworth.Server.Domain
      {


      public partial class <xsl:value-of select="@name"/> : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into <xsl:value-of select="@name" /> ( " +
      <xsl:for-each select="xs:complexType/xs:attribute[@default!='identity']">
        " <xsl:value-of select="@name" /> <xsl:if test="position() != last()">,</xsl:if> " +
      </xsl:for-each>
      ") Values (" +
      <xsl:for-each select="xs:complexType/xs:attribute[@default!='identity']">
        " ?<xsl:value-of select="@name" /> <xsl:if test="position() != last()">,</xsl:if> " +
      </xsl:for-each>
      ")";

      public static void Insert(<xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" />, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      <xsl:for-each select="xs:complexType/xs:attribute[@default!='identity']">
        Database.PutParameter(dbCommand,"?<xsl:value-of select="@name"/>", <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/>);
      </xsl:for-each>

      dbCommand.ExecuteNonQuery();

      <xsl:for-each select="xs:complexType/xs:attribute[@default='identity']">
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/> = Convert.<xsl:value-of select="codegen:DataConversionFunctionName(@type)"/>(dbIdentityCommand.ExecuteScalar());
        }
      </xsl:for-each>

      }
      }

      public static void Insert(<xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" />)
      {
        Insert(<xsl:value-of select="$functionParam" />, null);
      }


      public static void Insert(List&lt;<xsl:value-of select="$biz-name" />&gt; <xsl:text> </xsl:text><xsl:value-of select="$functionParam" />List, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(<xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" /> in <xsl:text> </xsl:text><xsl:value-of select="$functionParam" />List)
      {
      if(!parametersAdded)
      {
      <xsl:for-each select="xs:complexType/xs:attribute[@default!='identity']">
        Database.PutParameter(dbCommand,"?<xsl:value-of select="@name"/>", <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/>);
      </xsl:for-each>
      parametersAdded = true;
      }
      else
      {

      <xsl:for-each select="xs:complexType/xs:attribute[@default!='identity']">
        Database.UpdateParameter(dbCommand,"?<xsl:value-of select="@name"/>",<xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/>);
      </xsl:for-each>
      }

      dbCommand.ExecuteNonQuery();

      <xsl:for-each select="xs:complexType/xs:attribute[@default='identity']">
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/> = Convert.<xsl:value-of select="codegen:DataConversionFunctionName(@type)"/>(dbIdentityCommand.ExecuteScalar());
        }
      </xsl:for-each>

      }
      }
      }

      public static void Insert(List&lt;<xsl:value-of select="$biz-name" />&gt; <xsl:text> </xsl:text><xsl:value-of select="$functionParam" />List)
      {
        Insert(<xsl:value-of select="$functionParam" />List, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update <xsl:value-of select="@name" /> Set "
      <xsl:for-each select="xs:complexType/xs:attribute[not(concat('@',@name) = key('pk',$table)/@xpath)]">
        + " <xsl:value-of select="@name" /> = ?<xsl:value-of select="@name" /> <xsl:if test="position() != last()">,</xsl:if> "
      </xsl:for-each>

      <xsl:if test="count(xs:key) != 0">
        + " Where "
        <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
          + " <xsl:value-of select="@name" /> = ?<xsl:value-of select="@name" /> <xsl:if test="position() != last()"> and </xsl:if> "
        </xsl:for-each>
      </xsl:if>
      ;

      public static void Update(<xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" />, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      <xsl:for-each select="xs:complexType/xs:attribute">
        Database.PutParameter(dbCommand,"?<xsl:value-of select="@name"/>", <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/>);
      </xsl:for-each>

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(<xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" />)
      {
        Update(<xsl:value-of select="$functionParam" />, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      <xsl:for-each select="xs:complexType/xs:attribute">
        + " <xsl:value-of select="@name" /> <xsl:if test="position() != last()">,</xsl:if> "
      </xsl:for-each>

      + " From <xsl:value-of select="$table" /> "

      <xsl:if test="count(xs:key) != 0">
        + " Where "
        <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
          + " <xsl:value-of select="@name" /> = ?<xsl:value-of select="@name" /> <xsl:if test="position() != last()"> and </xsl:if> "
        </xsl:for-each>
      </xsl:if>
      ;

      public static <xsl:value-of select="$biz-name" /> FindByPrimaryKey(
      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        <xsl:value-of select="codegen:CSharpDataType(@type)" />
        <xsl:text> </xsl:text>
        <xsl:value-of select="codegen:FunctionParameter(@name)" />
        <xsl:if test="position() != last()">,</xsl:if>
      </xsl:for-each>, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        Database.PutParameter(dbCommand,"?<xsl:value-of select="@name"/>", <xsl:value-of select="codegen:FunctionParameter(@name)" />);
      </xsl:for-each>

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("<xsl:value-of select="$table" /> not found, search by primary key");

      }

      public static <xsl:value-of select="$biz-name" /> FindByPrimaryKey(
      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        <xsl:value-of select="codegen:CSharpDataType(@type)" />
        <xsl:text> </xsl:text>
        <xsl:value-of select="codegen:FunctionParameter(@name)" />
        <xsl:if test="position() != last()">,</xsl:if>
      </xsl:for-each>
      )
      {
      return FindByPrimaryKey(
      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        <xsl:value-of select="codegen:FunctionParameter(@name)" />
        <xsl:if test="position() != last()">,</xsl:if>
      </xsl:for-each>, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(<xsl:value-of select="$biz-name" /> <xsl:text> </xsl:text><xsl:value-of select="$functionParam" />, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        Database.PutParameter(dbCommand,"?<xsl:value-of select="@name"/>",<xsl:value-of select="$functionParam" />.<xsl:value-of select="@name" />);
      </xsl:for-each>

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(<xsl:value-of select="$biz-name" /> <xsl:text> </xsl:text><xsl:value-of select="$functionParam" />)
      {
      return Exists(<xsl:value-of select="$functionParam" />, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from <xsl:value-of select="$table"/> limit 1";

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

      public static <xsl:value-of select="$biz-name" /> Load(IDataReader dataReader, int offset)
      {
      <xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" /> = new<xsl:text> </xsl:text><xsl:value-of select="$biz-name" />();

      <xsl:for-each select="xs:complexType/xs:attribute">
        <xsl:choose>
          <xsl:when test="@use = 'optional'">
            if(!dataReader.IsDBNull(<xsl:value-of select="position()-1" /> + offset))
            <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/> = dataReader.<xsl:value-of select="codegen:DataReaderFunctionName(@type)"/>(<xsl:value-of select="position()-1" /> + offset);
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/> = dataReader.<xsl:value-of select="codegen:DataReaderFunctionName(@type)"/>(<xsl:value-of select="position()-1" /> + offset);
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each>

      return <xsl:value-of select="$functionParam" />;
      }

      public static <xsl:value-of select="$biz-name" /> Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From <xsl:value-of select="$table" /> "

      <xsl:if test="count(xs:key) != 0">
        + " Where "
        <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
          + " <xsl:value-of select="@name" /> = ?<xsl:value-of select="@name" /> <xsl:if test="position() != last()"> and </xsl:if> "
        </xsl:for-each>
      </xsl:if>
      ;
      public static void Delete(<xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" />, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        Database.PutParameter(dbCommand,"?<xsl:value-of select="@name"/>", <xsl:value-of select="$functionParam" />.<xsl:value-of select="@name"/>);
      </xsl:for-each>


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(<xsl:value-of select="$biz-name" /><xsl:text> </xsl:text><xsl:value-of select="$functionParam" />)
      {
        Delete(<xsl:value-of select="$functionParam" />, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From <xsl:value-of select="$table" /> ";

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

      <xsl:for-each select="xs:complexType/xs:attribute">
        + " <xsl:value-of select="@name" /> <xsl:if test="position() != last()">,</xsl:if> "
      </xsl:for-each>

      + " From <xsl:value-of select="$table" /> ";
      public static List&lt;<xsl:value-of select="$biz-name" />&gt; Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List&lt;<xsl:value-of select="$biz-name" />&gt; rv = new List&lt;<xsl:value-of select="$biz-name" />&gt;();

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

      public static List&lt;<xsl:value-of select="$biz-name" />&gt; Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List&lt;<xsl:value-of select="$biz-name" />&gt; itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (<xsl:value-of select="@name" /> obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return <xsl:for-each select="xs:complexType/xs:attribute">
               <xsl:value-of select="@name" /> == obj.<xsl:value-of select="@name" /><xsl:if test="position() != last()"> &amp;&amp; </xsl:if><xsl:if test="position() = last()">;</xsl:if>
             </xsl:for-each>
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List&lt;<xsl:value-of select="$biz-name" />&gt; itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(<xsl:value-of select="$biz-name" />));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(<xsl:value-of select="$biz-name" /> item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List&lt;<xsl:value-of select="$biz-name" />&gt;
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(<xsl:value-of select="$biz-name" />));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List&lt;<xsl:value-of select="$biz-name" />&gt; itemsList
      = new List&lt;<xsl:value-of select="$biz-name" />&gt;();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is <xsl:value-of select="$biz-name" />)
      itemsList.Add(deserializedObject as <xsl:value-of select="$biz-name" />);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      <xsl:value-of select="codegen:Progress(concat('Generating business class from ', @name))" />

      #region Fields
      <xsl:for-each select="xs:complexType/xs:attribute">
        protected <xsl:value-of select="codegen:CSharpDataType(@type)"/><xsl:if test="@use = 'optional' and (@type='xs:int' or @type='xs:date' or @type='xs:byte')">?</xsl:if><xsl:text> </xsl:text>m_<xsl:value-of select="codegen:FunctionParameter(@name)" />;
      </xsl:for-each>
      #endregion

      #region Constructors
      public <xsl:value-of select="$biz-name"/>(
      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        <xsl:value-of select="codegen:CSharpDataType(@type)" />
        <xsl:text> 
          </xsl:text>
        <xsl:value-of select="codegen:FunctionParameter(@name)" />
        <xsl:if test="position() != last()">,</xsl:if>
      </xsl:for-each>
      ) : this()
      {
      <xsl:for-each select="xs:complexType/xs:attribute[concat('@',@name) = key('pk',$table)/@xpath]">
        m_<xsl:value-of select="codegen:FunctionParameter(@name)" /> = <xsl:value-of select="codegen:FunctionParameter(@name)" />;
      </xsl:for-each>
      }

      <xsl:if test="count(xs:complexType/xs:attribute) != count(key('pk',$table))">


        public <xsl:value-of select="$biz-name"/>(
        <xsl:for-each select="xs:complexType/xs:attribute">
          <xsl:value-of select="codegen:CSharpDataType(@type)" />
          <xsl:if test="@use = 'optional' and (@type='xs:int' or @type='xs:date' or @type='xs:byte')">?</xsl:if>
          <xsl:text> 
          </xsl:text>
          <xsl:value-of select="codegen:FunctionParameter(@name)" />
          <xsl:if test="position() != last()">,</xsl:if>
        </xsl:for-each>
        ) : this()
        {
        <xsl:for-each select="xs:complexType/xs:attribute">
          m_<xsl:value-of select="codegen:FunctionParameter(@name)" /> = <xsl:value-of select="codegen:FunctionParameter(@name)" />;
        </xsl:for-each>
        }


      </xsl:if>
      #endregion

      <xsl:for-each select="xs:complexType/xs:attribute">
        [XmlElement]
        public <xsl:value-of select="codegen:CSharpDataType(@type)" /><xsl:if test="@use = 'optional' and (@type='xs:int' or @type='xs:date' or @type='xs:byte')">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="@name" />
        {
        get { return m_<xsl:value-of select="codegen:FunctionParameter(@name)" />;}
        set { m_<xsl:value-of select="codegen:FunctionParameter(@name)" /> = value; }
        }
      </xsl:for-each>

      public static int FieldsCount
      {
      get { return <xsl:value-of select="count(xs:complexType/xs:attribute)" />; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    </file>

    <xsl:if test="codegen:IsGenEmptyClass()">

      <file name="{$biz-name}.cs" type="empty-biz" overwrite="false">
        <xsl:call-template name="using.biz" />

        namespace Dalworth.Server.Domain
        {
        public partial class <xsl:value-of select="$biz-name"/>
        {
        public <xsl:value-of select="$biz-name"/>()
        {

        }
        }
        }
      </file>
    </xsl:if>

  </xsl:template>

</xsl:stylesheet>