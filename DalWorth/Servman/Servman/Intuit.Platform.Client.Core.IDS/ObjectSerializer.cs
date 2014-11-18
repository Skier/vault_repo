/*
 * Copyright (c) 2010-2011 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 *
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Intuit.Platform.Client.Core.IDS
{
    /// <summary>
    /// Provides method to serialize and desrialize the object provided.
    /// </summary>
    public class ObjectSerializer
    {
        /// <summary>
        /// Provides XML for the object provided
        /// </summary>
        /// <param name="objectToSerialize">object whose XML is required</param>
        /// <param name="namespacesToAdd">List of namespaces to Add</param>
        /// <returns> stringBuilder which contains the XML of object</returns>
        public StringBuilder SerializeObject(object objectToSerialize, List<string> namespacesToAdd)
        {
            StringBuilder xmlGenerated = null;
            try
            {
                if (objectToSerialize != null)
                {
                    //Initialize serializer for QboUser
                    XmlSerializer serializer = new XmlSerializer(objectToSerialize.GetType());
                    MemoryStream ms = new MemoryStream();
                    using (XmlTextWriter writer = new XmlTextWriter(ms, new UTF8Encoding(false)))
                    {
                        //Add namespace to the Serializer...
                        XmlSerializerNamespaces XMLNamespaces = new XmlSerializerNamespaces();


                        if (namespacesToAdd != null)
                        {
                            int index = 1;
                            foreach (string xmlnamespace in namespacesToAdd)
                            {
                                string prefix = string.Empty;

                                if (index > 1)
                                    prefix = "ns" + index;

                                XMLNamespaces.Add(prefix, xmlnamespace);
                                index++;

                            }
                        }

                        //Serailize the Qbouser object with namespaces declared above
                        serializer.Serialize(writer, objectToSerialize, XMLNamespaces);
                        writer.Flush();
                        //Read Textwriter and get XML into string.
                        string xmlString = new UTF8Encoding(false, true).GetString(ms.ToArray());
                        xmlGenerated = new StringBuilder(xmlString);
                    }
                }
                else
                {
                    xmlGenerated = new StringBuilder();
                }
            }
            //If invalid byte sequence occured UFT8Encoding throws argumentexception
            catch (ArgumentException ae)
            {
                IDSException IDSEx = new IDSException(null,ae.Message,ae);
                throw IDSEx;
            }
            return xmlGenerated;
        }

        /// <summary>
        /// Provides object from the provided input
        /// </summary>
        /// <param name="responseXml">XML from which object to be deserialized</param>
        /// <param name="objectType"> Type of object</param>
        /// <returns>Object on Type provided</returns>
        public object DeserializeObject(string responseXml, Type objectType)
        {
            object deserializedObject = null;
            
            //Initialize serializer for object
            XmlSerializer serializer = new XmlSerializer(objectType);

            using (TextReader reader = new StringReader(responseXml))
            {
                //deserialization of input XML
                deserializedObject = serializer.Deserialize(reader);
            }

            return deserializedObject;
        }

    }
}
