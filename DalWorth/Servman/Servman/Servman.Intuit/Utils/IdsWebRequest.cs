//<!-- 
//Copyright Intuit, Inc 2009

//  Intuit Data Services Sample Application
//  This sample demonstrates how to query Intuit Data Services for Customer, Employee and Invoice data from a sample Quickbooks Company file.
//  This sample is for learning purposes only.

//
// Copyright (c) 2009 Intuit Inc. All rights reserved.

// Redistribution and use in source and binary forms, with or without modification, are permitted in conjunction 
// with Intuit Partner Platform. 

// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, 
// BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY, NON-INFRINGEMENT AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED.  

// IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT 
// OF THE USE OF THIS SOFTWARE, WHETHER OR NOT SUCH DAMAGES WERE FORESEEABLE AND EVEN IF THE AUTHOR IS ADVISED 
// OF THE POSSIBILITY OF SUCH DAMAGES.
// -->


using System;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Servman.Intuit.Utils
{
    public class IdsWebRequest
    {

        public enum IdsObjectType
        {
            Account     = 10,
            Customer    = 20,
            Employee    = 30,
            Estimates   = 40,
            Invoice     = 50,
            Item        = 60,
            Jobs        = 70,
            Payment     = 80,
            SalesOrder  = 90,
            SalesRep    = 100,
            Vendor      = 110
        }

        public static XmlDocument GetRequest(XmlDocument postDocument, String intuitToken, String intuitAppToken, String url)
        {
            try
            {
                var xmlResponse = new XmlDocument();
                byte[] buffer = Encoding.ASCII.GetBytes(postDocument.OuterXml);
                var webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = "POST";
                webReq.ContentType = "text/xml";
                webReq.ContentLength = buffer.Length;
                webReq.Headers.Set("Authorization", BuildHeader(intuitToken, intuitAppToken));
                var postData = webReq.GetRequestStream();
                postData.Write(buffer, 0, buffer.Length);
                postData.Close();
                var webResp = (HttpWebResponse)webReq.GetResponse();
                var answer = webResp.GetResponseStream();
                var answerStream = new StreamReader(answer);
                xmlResponse.LoadXml(answerStream.ReadToEnd());
                return xmlResponse;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static String BuildHeader(String intuitToken, String intuitAppToken)
        {
            var authHeader = String.Concat("INTUITAUTH ", "intuit-app-token=\"", intuitAppToken, "\"", ",intuit-token=\"", intuitToken, "\"");
            return authHeader;
        }

        public static XmlElement MakeSimpleElement(XmlDocument xmlDoc, String tagname, String tagval)
        {
            try
            {
                XmlElement myElement = xmlDoc.CreateElement(tagname);
                myElement.InnerText = tagval;
                return myElement;
            }
            catch (Exception exception)
            {
                Trace.Write(exception.Message);
                throw exception;
            }

        }

        public static XmlAttribute MakeAttribute(XmlDocument xmlDoc, String tagname, String tagval)
        {
            try
            {
                XmlAttribute myAttribute = xmlDoc.CreateAttribute(tagname);
                myAttribute.Value = tagval;
                return myAttribute;
            }
            catch (Exception exception)
            {
                Trace.Write(exception.Message);
                throw exception;
            }

        }

        public static XmlNode AddNode(XmlDocument xmlDoc, XmlNode parentNode, String nodeName)
        {
            try
            {
                XmlNode myNode = xmlDoc.CreateElement(nodeName);
                parentNode.AppendChild(myNode);
                return myNode;
            }
            catch (Exception exception)
            {
                Trace.Write(exception.Message);
                throw exception;
            }

        }

    }
}
