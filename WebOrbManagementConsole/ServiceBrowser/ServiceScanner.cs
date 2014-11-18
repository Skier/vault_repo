using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Collections;

namespace Weborb.Management.ServiceBrowser
{
    internal class ServiceScanner
    {
        ServiceNodeMap m_namespaceMap;

        static List<String> s_skipFileList;
        static Dictionary<Type, String> s_typeMap;

        private Dictionary<String, ServiceDataType> m_typeCash;

        private const String DEFAULT_TYPE = "Object";

        static ServiceScanner()
        {
            s_skipFileList = new List<String>();
            //s_skipFileList.Add( "weborb.dll" );
            s_skipFileList.Add( "cpuinfo.dll" );
            s_skipFileList.Add( "log4net.dll" );
            s_skipFileList.Add( "OpenUrl20.dll" );
            s_skipFileList.Add( "SharpHsql.dll" );
            s_skipFileList.Add( "Mono.Security.dll" );
            s_skipFileList.Add( "Indy.Sockets.dll" );

            s_typeMap = new Dictionary<Type, string>();
            s_typeMap.Add(typeof(int), "int");
            s_typeMap.Add(typeof(DateTime), "Date");
            s_typeMap.Add(typeof(String), "String");
            s_typeMap.Add(typeof(StringBuilder), "String");
            s_typeMap.Add(typeof(Array), "Array");
            s_typeMap.Add(typeof(void), "void");
            s_typeMap.Add(typeof(float), "Number");
            s_typeMap.Add(typeof(double), "Number");
            s_typeMap.Add(typeof(decimal), "Number");
            s_typeMap.Add(typeof(short), "Number");
            s_typeMap.Add(typeof(long), "Number");
            s_typeMap.Add(typeof(byte), "Number");
            s_typeMap.Add(typeof(bool), "Boolean");

        }

        public ServiceScanner()
        {
            m_namespaceMap = new ServiceNodeMap();
            m_typeCash = new Dictionary<String, ServiceDataType>();
        }

        internal static String RootPath
        {
            get
            {
                return Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "bin" );
            }
        }

        internal List<FileInfo> getFiles( DirectoryInfo directoryInfo )
        {
            FileInfo[] files = directoryInfo.GetFiles( "*.dll" );

            List<FileInfo> rv = new List<FileInfo>();

            foreach( FileInfo fileInfo in files )
                if( !s_skipFileList.Contains( fileInfo.Name ) )
                    rv.Add( fileInfo );

            return rv;
        }

        internal Service loadService( String className )
        {
            foreach( FileInfo fileInfo in getFiles( new DirectoryInfo( RootPath ) ) )
            {
                Assembly assembly = loadAssembly(fileInfo.Name);

                foreach( Type type in assembly.GetTypes() )
                {
                    if( type.FullName == className )
                        return loadService( type );
                }
            }

            return null;
        }


        private static Assembly loadAssembly(String assemblyFileName)
        {
            String assemblyName = assemblyFileName.Substring(0, assemblyFileName.LastIndexOf('.'));

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name == assemblyName)
                    return assembly;
            }

            throw new Exception("Assembly " + assemblyName + " not loaded in application domain");

        }

        private Service loadService( Type type )
        {
            Service service = new Service( m_namespaceMap.getNamespace( type.Namespace ),
                type.Name );

            foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly | BindingFlags.Static))
            {
                if( methodInfo.IsSpecialName )
                    continue;

                ServiceMethod serviceMethod = new ServiceMethod( methodInfo.Name );
                serviceMethod.Parent = service;
                serviceMethod.ReturnDataType = getMappedType(methodInfo.ReturnType);

                foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
                {
                    ServiceMethodArg serviceMethodArg = new ServiceMethodArg(parameterInfo.Name);

                    serviceMethodArg.DataType = getMappedType(parameterInfo.ParameterType);

                    serviceMethodArg.Parent = serviceMethod;


                    serviceMethod.Items.Add(serviceMethodArg);

                }

                service.Items.Add( serviceMethod );
            }

            return service;
        }

        private ServiceDataType getMappedType(Type type)
        {
            if(m_typeCash.ContainsKey(type.FullName))
                return m_typeCash[type.FullName];
            
            if (s_typeMap.ContainsKey(type))
                m_typeCash[type.FullName] = new ServiceDataType(s_typeMap[type], null);
            else if (type.IsArray || type.GetInterface("IEnumerable") != null)
            {
                ServiceDataType arrayType = new ServiceDataType(s_typeMap[typeof(Array)], null);

                m_typeCash[type.FullName] = arrayType;

                if (type.HasElementType)
                    arrayType.ElementType = getMappedType(type.GetElementType());
                else
                    arrayType.ElementType = getMappedType(typeof(Object));

                arrayType.IsHashTable = type.Equals(typeof(IDictionary)) || type.GetInterface("IDictionary") != null;

            }
            else if (type.IsClass && !type.Namespace.StartsWith("System.") && type.Namespace != "System")
            {
                m_typeCash[type.FullName] = new ServiceDataType(type.Name, m_namespaceMap.getNamespace(type.Namespace));

                ServiceDataType mappedType = m_typeCash[type.FullName];

                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    mappedType.Items.Add(new ServiceDataTypeField(
                        propertyInfo.Name,
                        getMappedType(propertyInfo.PropertyType),
                        mappedType));
                }

                foreach (FieldInfo fieldInfo in type.GetFields())
                {
                    mappedType.Items.Add(new ServiceDataTypeField(
                        fieldInfo.Name,
                        getMappedType(fieldInfo.FieldType),
                        mappedType));
                }
            }
            else
                m_typeCash[type.FullName] = new ServiceDataType(DEFAULT_TYPE, null);


            return m_typeCash[type.FullName];

        }

        internal List<ServiceNode> getServices()
        {
            m_namespaceMap = new ServiceNodeMap();
            processDir( RootPath );

            List<ServiceNode> returnList = new List<ServiceNode>();

            foreach (ServiceNode serviceNode in m_namespaceMap.Items)
                if (serviceNode.Items.Count > 0 || serviceNode.IsService())
                    returnList.Add(serviceNode);

            return returnList;
        }

        private void processDir( String path )
        {
            DirectoryInfo directoryInfo = new DirectoryInfo( path );

            foreach( FileInfo fileInfo in getFiles( directoryInfo ) )
            {
                if( s_skipFileList.Contains( fileInfo.Name ) )
                    continue;

                try
                {
                    Assembly assembly = loadAssembly(fileInfo.Name);

                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!type.IsClass || type.IsAbstract || !type.IsPublic)
                            continue;

                        if (fileInfo.Name.Equals("weborb.dll") && !type.Namespace.StartsWith("Weborb.Examples"))
                            continue;

                        processType(type);
                    }
                }
                catch( Exception )
                {
                }
            }
        }

        private void processType( Type type )
        {
            Service service = loadService( type );

            if( service.Items.Count == 0 )
                return;

            if( service.Parent == null )
                m_namespaceMap.Items.Add( service );
            else
                service.Parent.Items.Add( service );
        }
    }
}
