/* fscwsFreeSideConnectorBindingProxy.h
   Generated by gSOAP 2.7.9d from fscws.h
   Copyright(C) 2000-2006, Robert van Engelen, Genivia Inc. All Rights Reserved.
   This part of the software is released under one of the following licenses:
   GPL, the gSOAP public license, or Genivia's license for commercial use.
*/

#ifndef fscwsFreeSideConnectorBinding_H
#define fscwsFreeSideConnectorBinding_H
#include "fscwsH.h"

namespace fscws {
class FreeSideConnectorBinding
{   public:
	struct soap *soap;
	const char *endpoint;
	FreeSideConnectorBinding()
	{ soap = soap_new(); endpoint = "http://moritur/nusoap/lib/fscws.php"; if (soap && !soap->namespaces) { static const struct Namespace namespaces[] = 
{
	{"SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/", "http://www.w3.org/*/soap-envelope", NULL},
	{"SOAP-ENC", "http://schemas.xmlsoap.org/soap/encoding/", "http://www.w3.org/*/soap-encoding", NULL},
	{"xsi", "http://www.w3.org/2001/XMLSchema-instance", "http://www.w3.org/*/XMLSchema-instance", NULL},
	{"xsd", "http://www.w3.org/2001/XMLSchema", "http://www.w3.org/*/XMLSchema", NULL},
	{"fscws", "http://zoomonline.com/fscws", NULL, NULL},
	{NULL, NULL, NULL, NULL}
};
	soap->namespaces = namespaces; } };
	virtual ~FreeSideConnectorBinding() { if (soap) { soap_destroy(soap); soap_end(soap); soap_free(soap); } };
	virtual int fscws__SignOn(char *username, char *password, char *&return_) { return soap ? soap_call_fscws__SignOn(soap, endpoint, NULL, username, password, return_) : SOAP_EOM; };
};

} // namespace fscws

#endif
