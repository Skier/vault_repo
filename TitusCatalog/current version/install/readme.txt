WebORB for .NET, version 3.5.0.0


What is new in 3.5
------------------------------------
* Integration with BlueDragon by New Atlanta Communications
  http://www.themidnightcoders.com/doc30?t=1101
  
* Command line remoting code generator
  http://www.themidnightcoders.com/doc30?t=800
  
* RTMP client support for server-to-server integration
  http://www.themidnightcoders.com/doc30?t=1706
  
* Improvements in support for .NET Generic collections



Additional Resources:
------------------------------------
Installing WebORB on Vista:
http://www.themidnightcoders.com/weborb/dotnet/vistainstall.shtm

Getting started with Flex and WebORB for .NET:
http://www.themidnightcoders.com/articles/flextodotnet.htm

Invoke .NET objects using RemoteObject API:
http://www.themidnightcoders.com/articles/remoteobjectfordotnet.htm

Getting started with WebORB Data Management:
http://www.themidnightcoders.com/weborb/dotnet/wdmfvideos.shtm

WebORB Data Management FAQ:
http://www.themidnightcoders.com/weborb/dotnet/wdmf-faq.shtm


PATCH RELEASES for 3.5
--------------------------------------
none


What's coming up in the future releases:
----------------------------------------
- WebORB Data Management API documentation
- WebORB Caching API
- Silverlight support


Release History:
------------------------------------

3.4.0.0
------------------------------------
* All WebORB editions (Standard, Professional and Enterprise) 
  are merged into one.. All licensing restrictions have been removed 
  and the product license has been changed to zero ($0.00). 
  WebORB for .NET is a FREE product offering the BEST integration 
  between rich clients and .NET
  
* Significant improvements in the AMF3 protocol compliance

* RTMPT support 
  http://www.themidnightcoders.com/doc30/?t=1704
  
* Support for the RTMP bandwidth detection
  http://www.themidnightcoders.com/doc30/?t=1705

* DataTable/DataSet row serialization configuration
  http://www.themidnightcoders.com/doc30/?t=1501044

* NHibernate proxy serialization support
  http://www.themidnightcoders.com/doc30/?t=1501045

* ArrayCollection serialization support
  http://www.themidnightcoders.com/doc30/?t=1501042

* Configurable support for Enum type serialization
  http://www.themidnightcoders.com/doc30/?t=1501046

* WebORB configuration hot deployment
  http://www.themidnightcoders.com/doc30/?t=553

* VideoChat example now has audio support
  http://www.themidnightcoders.com/weborb/dotnet/runexample.shtm?page=11
  
PATCH RELEASES:
3.4.0.2 - Significant AMF serialization improvements
3.4.0.1 - Fixes a bug in the compression serialization logic
  
  

3.3.0.0
------------------------------------
* WebORB management console includes real-time messaging information 
  for open client connections, shared objects and media streams
  
* Flex integration documentation is available in the user guide

* WDMF supports database views. A view can be added to a data model 
  and is represented by an ActiveRecord class
  
* WDMF no longer requires full closure of the related tables. Dependency 
  on the related tables can be turned of in the model designer.
  
* WebORB configuration file (weborb.config) is 'hot-deployable'. Any 
  change made to the file is automatically picked up by WebORB
  
* WebORB configuration file includes a setting controlling response 
  buffer (see the 'performance' section of the config file)
  
* Service browser has been redesigned to provide a per-assembly view 
  of the deployed services


3.2.0.0
------------------------------------
* WebORB Data Management Plugin for Flex Builder.
  Instructions available at: 
  http://www.themidnightcoders.com/weborb/dotnet/wdmf-flexbuilder.shtm  
  
* WebORB-based Implementation of Flex Producer/Consumer APIs
  Product distribution includes Flex component with an implementation
  of the Producer and Consumer API. If you are using Flex pub/sub with
  WebORB, make sure to add weborb.swc from [WEBORB INSTALL]/weborbassets/wdm
  to your Flex Builder project library.
  
* WebORB Data Management (WDMF) now generates unit test cases.
  WDMF code generator can now optionally add unit test cases for all the 
  generated code. The test cases can be executed from the Flex Builder project.
  
* WDMF Console improvements (menu/task-based layout)
  The new WDMF Designer console layout makes it easier to perform tasks like
  code generation, code retrieval and assembly deployment
  
* WDMF client auto-reconnect
  If the WDMF client disconnects from the server, it will attempt automatic reconnect.
  
* WDMF generated assembly can be automatically deployed
  The auto deployment option makes it easier to deploy the compiled assembly
  to the current virtual directory.
  
* Improved WDMF data model validation.
  Included checks for missing related tables and missing primary keys.
  
* Custom Flex channel implementation for RTMP messaging
  Weborb SWC component for Flex now includes a custom channel for the RTMP messaging.
  If you are getting a message WeborbMessagingChannel is not defined, make sure to 
  add weborb.swc from [WEBORB INSTALL]/weborbassets/wdm into your Flex Builder project
  
* Automatic GZIP response compression
  WebORB will automatically compress responses using GZIP or Deflate compression
  algorithms when the response size is over the pre-configured limit. See <compression>
  node in weborb.config for more details.
  
* Support for custom security roles provider
  WebORB can be configured with a custom roles provider. The role provider is 
  responsible to provide a list of available roles to the WebORB management console.
  Additionally it is used by the default principal implementation to check if a 
  user belongs to a role.

* Support for Windows Forms Authentication
  WebORB includes instructions and API to support Windows Forms Authentication. 
  See Logon.aspx and Global.asax for details.

* Changed IWebORBAuthenticationHandler to return System.Security.Principal.IPrincipal
  The CheckCredentials method has changed to return an instance of IPrincipal. The 
  principal is associated with the calling thread, thus individual method can be 
  secured using .NET's  standard code access security (CAS) mechanism.
  
* Added support for Flash Remoting style of DataSet object serialization
  WebORB serializes instances of DataSet class using the same data structure as 
  Flash Remoting. Previous serialization format can be turned on in weborb.config.
  See the <datasets> node for more details.
  
* Changed the WDMF base object (Weborb.Data.Management.DomainObject) to be serializable.
  This change allows persistence of the WDMF generated classes.
  
* Changed WDMF generated classes to use inheritance vs. partial class implementation
  This change provides greater flexibility when overriding default WDMF methods.
  
* Changed log time to use current locale (used to be GMT-0)
  All log entries now use local server time.


3.1.0.3
------------------------------------
* Flex Remoting (AMF3) is now available in the WebORB 
  Standard Edition
* Added support for MSMQ message selectors 
  (mx.messaging.Consumer.selector)
* Added capability to create new records in the testdrive 
  MXML/AS code generated by WDMF
* Added TimeSpan serialization. TimeSpan instances are 
  serialized as 'long' values. A value represents the 
  period of time expressed in milliseconds.
* Fixed compilation problems in the WDMF generated code 
  for MySQL databases
* Fixed compilation of VB WDMF projects using MySQL
* Static classes now show up in the service browser
* Improved WDMF data model validation - auto-correction 
  replaces illegal characters in the class and property 
  names with underscore ('_')
* Added support for the MySQL 'date', 'year' and 'blob' types
* Fixed a class cast exception problem with the SQL 
  Server float column type

3.1.0.2
------------------------------------
* Added support for VB.NET in the WDMF (WebORB Data Management
  for Flex) code generator. All the generated code for the 
  server-side can now be in C# or VB.NET
* Standalone WebORB Enterprise Edition process can now operate 
  as a Windows Service. Use -install, -uninstall, -start and 
  -stop arguments with weborbee.exe
* System.Data.DataSet is serialized as a collection of table 
  name to DataTable mappings. A DataTable is serialized as an
  array of complex types, where each complex type represents a
  row of data.
* Configuration file containing database and data model 
  information used by WebORB Data Management for Flex has been
  renamed to wdm.config. This change requires re-adding of 
  database and data model information in the console.
* Fixed bugs:
    o Flex consumers connection termination is now properly 
      handled
    o RTMP connection termination no longer causes thread 
      misallocation.
    o Microsoft SQL Server 2005 table names are properly prefixed
      with schema name. Previously schema name was omitted in the 
      generated SQL queries.
    o Double.NaN type adaptation. Values of NaN can now be adapted
      to any Nullable numeric type
    o ActionScript String to Guid type adaptation now works as
      expected

3.1.0.1
------------------------------------
* Added 'Cardinality' column in the Table/Column properties 
  in Code Generator;
* Added support for stored procedures
* Added support for ActiveRecords.XXX.findFirst and findLast 
  methods
* Console's Data Management tab immediately stores changes 
  in the column mappings
* Added data type mapping for the "float" data type

3.1.0.0:
------------------------------------
* WebORB Data Management for Flex - powerful, fully managed, 
  client-server data management system enabling creation of 
  Flex-based data-driven applications.
* Microsoft Windows Vista integration
* Improved real-time streaming and data messaging








