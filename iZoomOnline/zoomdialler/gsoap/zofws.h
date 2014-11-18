namespace zofws {
/* zofws.h
   Generated by wsdl2h 1.2.9d from http://moritur/zofws.php?wsdl and typemap.dat
   2007-03-30 12:54:31 GMT
   Copyright (C) 2001-2006 Robert van Engelen, Genivia Inc. All Rights Reserved.
   This part of the software is released under one of the following licenses:
   GPL or Genivia's license for commercial use.
*/

/* NOTE:

 - Compile this file with soapcpp2 to complete the code generation process.
 - Use soapcpp2 option -I to specify paths for #import
   To build with STL, 'stlvector.h' is imported from 'import' dir in package.
 - Use wsdl2h options -c and -s to generate pure C code or C++ code without STL.
 - Use 'typemap.dat' to control schema namespace bindings and type mappings.
   It is strongly recommended to customize the names of the namespace prefixes
   generated by wsdl2h. To do so, modify the prefix bindings in the Namespaces
   section below and add the modified lines to 'typemap.dat' to rerun wsdl2h.
 - Use Doxygen (www.doxygen.org) to browse this file.
 - Use wsdl2h option -l to view the software license terms.

   DO NOT include this file directly into your project.
   Include only the soapcpp2-generated headers and source code files.
*/

//gsoapopt w

/******************************************************************************\
 *                                                                            *
 * http://zoomonline.com/zofws                                                *
 *                                                                            *
\******************************************************************************/


/******************************************************************************\
 *                                                                            *
 * Import                                                                     *
 *                                                                            *
\******************************************************************************/


/******************************************************************************\
 *                                                                            *
 * Schema Namespaces                                                          *
 *                                                                            *
\******************************************************************************/


/* NOTE:

It is strongly recommended to customize the names of the namespace prefixes
generated by wsdl2h. To do so, modify the prefix bindings below and add the
modified lines to typemap.dat to rerun wsdl2h:

zofws = "http://zoomonline.com/zofws"

*/

//gsoap zofws schema namespace:	http://zoomonline.com/zofws
//gsoap zofws schema form:	unqualified

/******************************************************************************\
 *                                                                            *
 * Schema Types                                                               *
 *                                                                            *
\******************************************************************************/



/******************************************************************************\
 *                                                                            *
 * Services                                                                   *
 *                                                                            *
\******************************************************************************/


//gsoap zofws service name:	ZoomOnlineFrontendBinding 
//gsoap zofws service type:	ZoomOnlineFrontendPortType 
//gsoap zofws service port:	http://moritur/zofws.php 
//gsoap zofws service namespace:	http://zoomonline.com/zofws 
//gsoap zofws service transport:	http://schemas.xmlsoap.org/soap/http 

/** @mainpage Service Definitions

@section Service_bindings Bindings
  - @ref ZoomOnlineFrontendBinding

*/

/**

@page ZoomOnlineFrontendBinding Binding "ZoomOnlineFrontendBinding"

@section ZoomOnlineFrontendBinding_operations Operations of Binding  "ZoomOnlineFrontendBinding"
  - @ref zofws__GetNextUrl

@section ZoomOnlineFrontendBinding_ports Endpoints of Binding  "ZoomOnlineFrontendBinding"
  - http://moritur/zofws.php

*/

/******************************************************************************\
 *                                                                            *
 * ZoomOnlineFrontendBinding                                                  *
 *                                                                            *
\******************************************************************************/


/******************************************************************************\
 *                                                                            *
 * zofws__GetNextUrl                                                          *
 *                                                                            *
\******************************************************************************/


/// Operation "zofws__GetNextUrl" of service binding "ZoomOnlineFrontendBinding"

/**

Operation details:

  - SOAP RPC encodingStyle="http://schemas.xmlsoap.org/soap/encoding/"
  - SOAP action="http://moritur/zofws.php/GetNextUrl"

C stub function (defined in soapClient.c[pp] generated by soapcpp2):
@code
  int soap_call_zofws__GetNextUrl(
    struct soap *soap,
    NULL, // char *endpoint = NULL selects default endpoint for this operation
    NULL, // char *action = NULL selects default action for this operation
    // request parameters:
    char*                               username,
    // response parameters:
    char*                              &return_
  );
@endcode

C server function (called from the service dispatcher defined in soapServer.c[pp]):
@code
  int zofws__GetNextUrl(
    struct soap *soap,
    // request parameters:
    char*                               username,
    // response parameters:
    char*                              &return_
  );
@endcode

C++ proxy class (defined in soapZoomOnlineFrontendBindingProxy.h):
  class ZoomOnlineFrontendBinding;

*/

//gsoap zofws service method-style:	GetNextUrl rpc
//gsoap zofws service method-encoding:	GetNextUrl http://schemas.xmlsoap.org/soap/encoding/
//gsoap zofws service method-action:	GetNextUrl http://moritur/zofws.php/GetNextUrl
int zofws__GetNextUrl(
    char*                               username,	///< Request parameter
    char*                              &return_	///< Response parameter
);

} // namespace zofws

/* End of zofws.h */