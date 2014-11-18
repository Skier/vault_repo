using System;
using System.Collections.Generic;
using System.Text;
using Weborb.Management.ServiceBrowser;
using System.Xml;
using System.Collections;

namespace Weborb.Management.CodeGen
{
    internal enum ServiceSerializerItemType
    {
        Service,
        Method,
        MethodArg,
        DataType
    }

    internal delegate void ServiceSerializeEvent(XmlTextWriter writer,ServiceNode serviceNode, ServiceSerializerItemType itemType);

    internal class ServiceSerializer
    {
        public event ServiceSerializeEvent WriteItem;
        Dictionary<String, ServiceDataType> dataTypeList = new Dictionary<String, ServiceDataType>();

        public void Serialize(ServiceBrowser.Service service, ArrayList args, String filePath)
        {

            

            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, Encoding.UTF8))
            {
                xmlTextWriter.WriteStartDocument();

                List<ServiceNode> namespaceList = new List<ServiceNode>();
                ServiceNode parentNamespace = service.Parent;

                while (parentNamespace != null)
                {
                    namespaceList.Add(parentNamespace);
                    parentNamespace = parentNamespace.Parent;
                }

                for (int i = namespaceList.Count-1; i >= 0; i--)
                {
                    xmlTextWriter.WriteStartElement("namespace");
                    xmlTextWriter.WriteAttributeString("name", namespaceList[i].Name);
                }

                xmlTextWriter.WriteStartElement("service");
                xmlTextWriter.WriteAttributeString("name", service.Name);
                xmlTextWriter.WriteAttributeString("fullname", service.getFullName());
                xmlTextWriter.WriteAttributeString("namespace", service.Parent.getFullName());

                if (WriteItem != null)
                    WriteItem(xmlTextWriter, service, ServiceSerializerItemType.Service);


                dataTypeList = new Dictionary<String, ServiceDataType>();

                foreach (ServiceMethod serviceMethod in service.Items)
                {
                    xmlTextWriter.WriteStartElement("method");
                    xmlTextWriter.WriteAttributeString("name", serviceMethod.Name);
                    xmlTextWriter.WriteAttributeString("type", serviceMethod.ReturnDataType.Name);
                    xmlTextWriter.WriteAttributeString("containsvalues", serviceMethod.Called ? "1" : "0");

                    dataTypeList[serviceMethod.ReturnDataType.Name] = serviceMethod.ReturnDataType;

                    if (WriteItem != null)
                        WriteItem(xmlTextWriter, serviceMethod, ServiceSerializerItemType.Service);

                    int argIndex = 0;

                    foreach (ServiceMethodArg serviceMethodArg in serviceMethod.Items)
                    {
                        xmlTextWriter.WriteStartElement("arg");
                        xmlTextWriter.WriteAttributeString("name", serviceMethodArg.Name);
                        xmlTextWriter.WriteAttributeString("type", serviceMethodArg.DataType.Name);

                        if (args != null && serviceMethod.Called && args[argIndex] != null)
                        {
                            if (serviceMethodArg.DataType.IsGeneric())
                            {
                                xmlTextWriter.WriteStartElement("value");
                                if (args[argIndex] is bool)
                                    xmlTextWriter.WriteString(args[argIndex].ToString().ToLower());
                                else
                                    xmlTextWriter.WriteCData(args[argIndex].ToString());
                                xmlTextWriter.WriteEndElement();
                            }
                            else if (serviceMethodArg.DataType.IsComplexType())
                                SerializeFields(serviceMethodArg.DataType, xmlTextWriter, args[argIndex]);
                            else if (serviceMethodArg.DataType.IsArray())
                                SerializeArray(serviceMethodArg.DataType, xmlTextWriter, args[argIndex]);
                        }

                        xmlTextWriter.WriteEndElement();

                        dataTypeList[serviceMethodArg.DataType.Name] = serviceMethodArg.DataType;

                        ++argIndex;
                    }
                    xmlTextWriter.WriteEndElement();

                    dataTypeList[serviceMethod.ReturnDataType.Name] = serviceMethod.ReturnDataType;
                }

                foreach (ServiceDataType dataType in dataTypeList.Values)
                {
                    if (dataType.Items.Count == 0)
                        continue;

                    xmlTextWriter.WriteStartElement("datatype");
                    xmlTextWriter.WriteAttributeString("name", dataType.Name);
                    xmlTextWriter.WriteAttributeString("fullname", dataType.getFullName());

                    foreach (ServiceDataTypeField field in dataType.Items)
                    {
                        xmlTextWriter.WriteStartElement("field");
                        xmlTextWriter.WriteAttributeString("name", field.Name);
                        xmlTextWriter.WriteAttributeString("type", field.DataType.Name);
                        xmlTextWriter.WriteEndElement();
                    }

                    xmlTextWriter.WriteEndElement();

                }

                xmlTextWriter.WriteEndElement();

                for (int i = 0; i < namespaceList.Count; i++ )
                    xmlTextWriter.WriteEndElement();

                    xmlTextWriter.WriteEndDocument();

                xmlTextWriter.Flush();
            }
        }

        private void SerializeFields(ServiceDataType dataType, XmlTextWriter xmlTextWriter, Object argsContext)
        {
            dataTypeList[dataType.Name] = dataType;

            if (argsContext == null)
                return;

            Hashtable hashtable = (Hashtable)argsContext;

            xmlTextWriter.WriteStartElement("complex-value");
            xmlTextWriter.WriteAttributeString("type", dataType.Name);

            foreach (ServiceDataTypeField field in dataType.Items)
            {
                xmlTextWriter.WriteStartElement("field");
                xmlTextWriter.WriteAttributeString("name", field.Name);
                xmlTextWriter.WriteAttributeString("type", field.DataType.Name);

                if (field.DataType.IsComplexType())
                    SerializeFields(field.DataType, xmlTextWriter, hashtable[field.Name]);
                else if (field.DataType.IsArray())
                    SerializeArray(field.DataType, xmlTextWriter, hashtable[field.Name]);
                else
                {
                    if (hashtable[field.Name] != null)
                    {
                        xmlTextWriter.WriteStartElement("value");

                        if (hashtable[field.Name] is bool)
                            xmlTextWriter.WriteString(hashtable[field.Name].ToString().ToLower());
                        else
                            xmlTextWriter.WriteCData(hashtable[field.Name].ToString());

                        xmlTextWriter.WriteEndElement();
                    }
                }
                
                xmlTextWriter.WriteEndElement();
            }

            xmlTextWriter.WriteEndElement();
        }

        private void SerializeArray(ServiceDataType dataType, XmlTextWriter xmlTextWriter, Object argsContext)
        {
            if (argsContext == null)
                return;

            xmlTextWriter.WriteStartElement("array-value");
            xmlTextWriter.WriteAttributeString("type", dataType.ElementType.Name);
            xmlTextWriter.WriteAttributeString("hashtable", dataType.IsHashTable ? "1" : "0");

            if (dataType.IsHashTable)
            {
                Hashtable hashTable = (Hashtable)argsContext;
                foreach (String key in hashTable.Keys)
                {
                    xmlTextWriter.WriteStartElement("item");
                    xmlTextWriter.WriteAttributeString("name", key);
                    xmlTextWriter.WriteAttributeString("type", dataType.ElementType.Name);

                    xmlTextWriter.WriteStartElement("value");
                    if (hashTable[key] != null)
                        xmlTextWriter.WriteCData(hashTable[key].ToString());
                    xmlTextWriter.WriteEndElement();

                    xmlTextWriter.WriteEndElement();
                }
            }
            else
            { 
                foreach (Object item in ((ICollection)argsContext))
                { 
                    xmlTextWriter.WriteStartElement("item");


                    if (dataType.ElementType.IsComplexType())
                    {
                        SerializeFields(dataType.ElementType, xmlTextWriter, item);
                    }
                    else if (dataType.ElementType.IsArray())
                    {
                        SerializeArray(dataType.ElementType, xmlTextWriter, item);
                    }
                    else if(item != null)
                    {
                        xmlTextWriter.WriteStartElement("value");
                            xmlTextWriter.WriteCData(item.ToString());
                        xmlTextWriter.WriteEndElement();
                    }
                    
                   
                    xmlTextWriter.WriteEndElement();
                }
            }
            

            xmlTextWriter.WriteEndElement();
        }

    }
}
