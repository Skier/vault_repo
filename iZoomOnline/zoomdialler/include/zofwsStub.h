/* zofwsStub.h
   Generated by gSOAP 2.7.9d from zofws.h
   Copyright(C) 2000-2006, Robert van Engelen, Genivia Inc. All Rights Reserved.
   This part of the software is released under one of the following licenses:
   GPL, the gSOAP public license, or Genivia's license for commercial use.
*/

#ifndef zofwsStub_H
#define zofwsStub_H
#ifndef WITH_NOGLOBAL
#define WITH_NOGLOBAL
#endif
#include "stdsoap2.h"

namespace zofws {

/******************************************************************************\
 *                                                                            *
 * Enumerations                                                               *
 *                                                                            *
\******************************************************************************/


/******************************************************************************\
 *                                                                            *
 * Classes and Structs                                                        *
 *                                                                            *
\******************************************************************************/


#ifndef SOAP_TYPE_zofws_zofws__GetNextUrlResponse
#define SOAP_TYPE_zofws_zofws__GetNextUrlResponse (8)
/* zofws:GetNextUrlResponse */
struct zofws__GetNextUrlResponse
{
public:
	char *return_;	/* RPC return element */	/* required element of type xsd:string */
};
#endif

#ifndef SOAP_TYPE_zofws_zofws__GetNextUrl
#define SOAP_TYPE_zofws_zofws__GetNextUrl (9)
/* zofws:GetNextUrl */
struct zofws__GetNextUrl
{
public:
	char *username;	/* optional element of type xsd:string */
};
#endif

#ifndef SOAP_TYPE_zofws_SOAP_ENV__Header
#define SOAP_TYPE_zofws_SOAP_ENV__Header (12)
/* SOAP Header: */
struct SOAP_ENV__Header
{
public:
	void *dummy;	/* transient */
};
#endif

#ifndef SOAP_TYPE_zofws_SOAP_ENV__Code
#define SOAP_TYPE_zofws_SOAP_ENV__Code (13)
/* SOAP Fault Code: */
struct SOAP_ENV__Code
{
public:
	char *SOAP_ENV__Value;	/* optional element of type xsd:QName */
	struct SOAP_ENV__Code *SOAP_ENV__Subcode;	/* optional element of type SOAP-ENV:Code */
};
#endif

#ifndef SOAP_TYPE_zofws_SOAP_ENV__Detail
#define SOAP_TYPE_zofws_SOAP_ENV__Detail (15)
/* SOAP-ENV:Detail */
struct SOAP_ENV__Detail
{
public:
	int __type;	/* any type of element <fault> (defined below) */
	void *fault;	/* transient */
	char *__any;
};
#endif

#ifndef SOAP_TYPE_zofws_SOAP_ENV__Reason
#define SOAP_TYPE_zofws_SOAP_ENV__Reason (16)
/* SOAP-ENV:Reason */
struct SOAP_ENV__Reason
{
public:
	char *SOAP_ENV__Text;	/* optional element of type xsd:string */
};
#endif

#ifndef SOAP_TYPE_zofws_SOAP_ENV__Fault
#define SOAP_TYPE_zofws_SOAP_ENV__Fault (17)
/* SOAP Fault: */
struct SOAP_ENV__Fault
{
public:
	char *faultcode;	/* optional element of type xsd:QName */
	char *faultstring;	/* optional element of type xsd:string */
	char *faultactor;	/* optional element of type xsd:string */
	struct SOAP_ENV__Detail *detail;	/* optional element of type SOAP-ENV:Detail */
	struct SOAP_ENV__Code *SOAP_ENV__Code;	/* optional element of type SOAP-ENV:Code */
	struct SOAP_ENV__Reason *SOAP_ENV__Reason;	/* optional element of type SOAP-ENV:Reason */
	char *SOAP_ENV__Node;	/* optional element of type xsd:string */
	char *SOAP_ENV__Role;	/* optional element of type xsd:string */
	struct SOAP_ENV__Detail *SOAP_ENV__Detail;	/* optional element of type SOAP-ENV:Detail */
};
#endif

/******************************************************************************\
 *                                                                            *
 * Types with Custom Serializers                                              *
 *                                                                            *
\******************************************************************************/


/******************************************************************************\
 *                                                                            *
 * Typedefs                                                                   *
 *                                                                            *
\******************************************************************************/

#ifndef SOAP_TYPE_zofws__XML
#define SOAP_TYPE_zofws__XML (4)
typedef char *_XML;
#endif

#ifndef SOAP_TYPE_zofws__QName
#define SOAP_TYPE_zofws__QName (5)
typedef char *_QName;
#endif


/******************************************************************************\
 *                                                                            *
 * Typedef Synonyms                                                           *
 *                                                                            *
\******************************************************************************/


/******************************************************************************\
 *                                                                            *
 * Externals                                                                  *
 *                                                                            *
\******************************************************************************/


/******************************************************************************\
 *                                                                            *
 * Stubs                                                                      *
 *                                                                            *
\******************************************************************************/


SOAP_FMAC5 int SOAP_FMAC6 soap_call_zofws__GetNextUrl(struct soap *soap, const char *soap_endpoint, const char *soap_action, char *username, char *&return_);

} // namespace zofws


#endif

/* End of zofwsStub.h */
