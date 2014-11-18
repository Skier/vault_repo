using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dalworth.Server.Codegen
{
    public class XsltExtention
    {
        CodeGenerator m_codegen;

        Hashtable m_dataReaderFunctions;
        Hashtable m_csDataType;
        Hashtable m_dataConversionFunctions;

        public XsltExtention(CodeGenerator codegen)
        {
            m_codegen = codegen;

            m_dataReaderFunctions = new Hashtable();

            m_dataReaderFunctions["xs:string"] = "GetString";
            m_dataReaderFunctions["xs:int"] = "GetInt32";
            m_dataReaderFunctions["xs:long"] = "GetInt64";
            m_dataReaderFunctions["xs:boolean"] = "GetBoolean";
            m_dataReaderFunctions["xs:date"] = "GetDateTime";
            m_dataReaderFunctions["xs:short"] = "GetInt16";
            m_dataReaderFunctions["xs:byte"] = "GetByte";
            m_dataReaderFunctions["xs:decimal"] = "GetDecimal";
            m_dataReaderFunctions["xs:normalizedString"] = "GetString";
            m_dataReaderFunctions["xs:float"] = "GetFloat";

            m_csDataType = new Hashtable();

            m_csDataType["xs:string"] = "String";
            m_csDataType["xs:int"] = "int";
            m_csDataType["xs:long"] = "long";
            m_csDataType["xs:boolean"] = "bool";
            m_csDataType["xs:date"] = "DateTime";
            m_csDataType["xs:short"] = "int";
            m_csDataType["xs:byte"] = "byte";
            m_csDataType["xs:decimal"] = "decimal";
            m_csDataType["xs:normalizedString"] = "String";
            m_csDataType["xs:float"] = "float";

            m_dataConversionFunctions = new Hashtable();
            m_dataConversionFunctions["xs:int"] = "ToInt32";
            m_dataConversionFunctions["xs:long"] = "ToInt64";
            m_dataConversionFunctions["xs:byte"] = "ToByte";
        }

        public String Progress(String message)
        {
            m_codegen.AddMessage(message);

            return String.Empty;
        }

        public String FunctionParameter(String columnName)
        {
            return columnName.Substring(0, 1).ToLower() + columnName.Substring(1);
        }


        public String DataReaderFunctionName(String xsDataType)
        {
            return m_dataReaderFunctions[xsDataType].ToString();
        }

        public String DataConversionFunctionName(String xsDataType)
        {
            return m_dataConversionFunctions[xsDataType].ToString();
        }

        public String CSharpDataType(String xsDataType)
        {
            return m_csDataType[xsDataType].ToString();
        }

        public bool IsGenEmptyClass()
        {
            return m_codegen.GenerateEmptyClass;
        }
    }
}
