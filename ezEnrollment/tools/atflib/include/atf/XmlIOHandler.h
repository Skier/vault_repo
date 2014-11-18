/*
 *  $RCSfile: XmlIOHandler.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlIOHandler.h: interface for the IXmlIOHandler class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XMLIOHANDLER_H__2A775D98_9373_4188_B227_A314E79E6B3A__INCLUDED_)
#define AFX_XMLIOHANDLER_H__2A775D98_9373_4188_B227_A314E79E6B3A__INCLUDED_


#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class IXmlIOHandler {
public:
    IXmlIOHandler(){};
    virtual ~IXmlIOHandler(){};
    virtual size_t LoadBuffer(char * buffer, size_t bufferSize) = 0;    
};

#endif // !defined(AFX_XMLIOHANDLER_H__2A775D98_9373_4188_B227_A314E79E6B3A__INCLUDED_)
