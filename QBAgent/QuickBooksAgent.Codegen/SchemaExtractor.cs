using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Schema;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;

namespace QuickBooksAgent.Codegen
{
    public class SchemaExtractor
    {
        #region SqlStatements

        const String SqlTableMeta = "SELECT Sysobjects.name, syscolumns.Id as Id, syscolumns.name, systypes.name, syscolumns.isnullable " +
            "FROM syscolumns INNER JOIN systypes ON syscolumns.xtype = systypes.xtype " +
            "LEFT JOIN sysobjects ON syscolumns.id = sysobjects.id " +
                                "LEFT OUTER JOIN sysproperties ON (sysproperties.smallid = syscolumns.colid AND sysproperties.id = syscolumns.id) " +
                                    "LEFT OUTER JOIN	syscomments ON syscolumns.cdefault = syscomments.id " +
            "WHERE syscolumns.id IN (SELECT id FROM SYSOBJECTS WHERE xtype = 'U') AND (systypes.name <> 'sysname') AND Sysobjects.name = @TableName " +
            "ORDER BY syscolumns.colid";

        const String SqlDbMeta = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.Tables WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME != 'dtproperties'";

        #endregion

        Hashtable m_dataTypeMapping;

        public SchemaExtractor()
        {
            m_dataTypeMapping = new Hashtable();

            m_dataTypeMapping["int"] = "int";
            m_dataTypeMapping["double"] = "double";
            m_dataTypeMapping["datetime"] = "date";
            m_dataTypeMapping["nvarchar"] = "string";
            m_dataTypeMapping["varchar"] = "string";
            m_dataTypeMapping["bit"] = "boolean";
            m_dataTypeMapping["bigint"] = "long";
            m_dataTypeMapping["smallint"] = "short";
            m_dataTypeMapping["tinyint"] = "byte";
            m_dataTypeMapping["money"] = "decimal";
            m_dataTypeMapping["decimal"] = "decimal";
            m_dataTypeMapping["numeric"] = "decimal";
        }





        public void Extract(String connectionString, String outputPath)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                List<String> tables = new List<string>();

                using (SqlCommand sqlCommand = new SqlCommand(SqlDbMeta, sqlConnection))
                {
                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            tables.Add(dataReader.GetString(0));
                        }
                    }
                }

                XmlSchema xmlSchema = new XmlSchema();
				xmlSchema.Namespaces.Add("mstns", "http://tempuri.org/XMLSchema.xsd");

                DatabaseMeta databaseMeta = new DatabaseMeta();

                foreach (String tableName in tables)
                {
                    databaseMeta.LoadTableMeta(connectionString, tableName);
                }


                databaseMeta.Sort();

                int elementId = 0;

                foreach (TableMeta tableMeta in databaseMeta.Tables)
                {
                    XmlSchemaElement element = new XmlSchemaElement();
                    element.Name = tableMeta.Name;
                    element.Id = "E" + (++elementId).ToString();

                    XmlSchemaComplexType complexType = new XmlSchemaComplexType();

                    using (SqlCommand sqlCommand = new SqlCommand(SqlTableMeta, sqlConnection))
                    {
                        sqlCommand.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = tableMeta.Name;

                        using(IDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                XmlSchemaAttribute attribute = new XmlSchemaAttribute();

                                attribute.Name = dataReader.GetString(2);

                                if(!m_dataTypeMapping.ContainsKey( dataReader.GetString(3) ))
                                    throw new Exception("Datatype " + dataReader.GetString(3)+ " mapping not found");

                                attribute.SchemaTypeName = new XmlQualifiedName(m_dataTypeMapping[dataReader.GetString(3)].ToString(), "http://www.w3.org/2001/XMLSchema");

                                // Is nullable
                                if (dataReader.GetInt32(4) == 1)
                                    attribute.Use = XmlSchemaUse.Optional;
                                else
                                    attribute.Use = XmlSchemaUse.Required;


                                complexType.Attributes.Add(attribute);

                            }
                        }
                    }

                    using (SqlCommand sqlCommand = new SqlCommand("exec sp_pkeys '" + tableMeta.Name + "'", sqlConnection))
                    {
                        using(IDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            XmlSchemaKey primaryKey = new XmlSchemaKey();

                            while (dataReader.Read())
                            {
                                if (primaryKey.Selector == null)
                                {
                                    primaryKey.Selector = new XmlSchemaXPath();
                                    primaryKey.Selector.XPath = ".";
                                    primaryKey.Name = dataReader.GetString(5);
                                }

                                XmlSchemaXPath field = new XmlSchemaXPath();

                                field.XPath = "@" + dataReader.GetString(3);

                                primaryKey.Fields.Add(field);
                            }

                            element.Constraints.Add(primaryKey);
                        }

                    }


					/*
							<xs:keyref msdata:DeleteRule="Cascade" name="fk_role_person" refer="mstns:pk_role">
								<xs:selector xpath="." />
								<xs:field xpath="@roleid" />
							</xs:keyref>
					 */

					int relayIndex = 0;

					foreach (TableMeta relTable in tableMeta.DependsTables.Values)
					{
						using (SqlCommand sqlCommand = new SqlCommand("exec sp_pkeys '" + relTable.Name + "'", sqlConnection))
						{
							using (IDataReader dataReader = sqlCommand.ExecuteReader())
							{
								XmlSchemaKeyref xmlSchemaKeyref = new XmlSchemaKeyref();


								while (dataReader.Read())
								{
									if (xmlSchemaKeyref.Selector == null)
									{
										xmlSchemaKeyref.Selector = new XmlSchemaXPath();
										xmlSchemaKeyref.Selector.XPath = ".";
										xmlSchemaKeyref.Name = "FK_" + tableMeta.Name + "_" + (relayIndex++).ToString() + "_" + dataReader.GetString(5);
										xmlSchemaKeyref.Refer = new XmlQualifiedName(dataReader.GetString(5));

									}

									XmlSchemaXPath field = new XmlSchemaXPath();

									field.XPath = "@" + dataReader.GetString(3);

									xmlSchemaKeyref.Fields.Add(field);
								}

								element.Constraints.Add(xmlSchemaKeyref);
							}

						}
					}


					element.SchemaType = complexType;

                    xmlSchema.Items.Add(element);
                }

                xmlSchema.Compile(new ValidationEventHandler(ValidationCallbackOne));


                using (FileStream fileStream = File.OpenWrite(outputPath))
                {
                    xmlSchema.Write(fileStream);
                }

            }

        }

        public static void ValidationCallbackOne(object sender, ValidationEventArgs args)
        {
            Program.AddMessage(args.Message);
        }


    }
}
