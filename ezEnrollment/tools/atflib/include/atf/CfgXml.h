/*
 *  $RCSfile: CfgXml.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CfgXml.h: interface for the CCfgXml class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CFGXML_H__1B0F3CC9_03C1_4776_8ABB_E46E4CF7B1A8__INCLUDED_)
#define AFX_CFGXML_H__1B0F3CC9_03C1_4776_8ABB_E46E4CF7B1A8__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Cfg.h>
#include <atf/XmlNode.h>
#include <atf/XmlDocument.h>

#pragma warning( disable : 4290 ) 

class CCfgXml : public ICfg {
public:
    CCfgXml(CXmlDocument & doc);
    virtual ~CCfgXml();
    
    CString GetName()const;
    void SetName(const char * name);
    
    ICfg * CreateChild( const char *name );
    ICfg * GetChild( const char *name) throw (CCfgException);

    ICfg * GetChildren() throw (CCfgException);
    ICfg * GetNext(); 
    
    bool HasParam(const char *name) const;
    
    CString GetParam(const char *name, const char * defaultValue) const;
    CString GetParam(const char *name) const throw (CCfgException);
    void SetParam(const char *name, const char *value);
    
    long GetParamAsLong(const char *name, long defaultValue) const;
    long GetParamAsLong(const char *name) const throw (CCfgException);
    void SetParam(const char *name, long value);
    
    bool GetParamAsBool(const char *name, bool defaultValue) const;
    bool GetParamAsBool(const char *name) const throw (CCfgException);
    void SetParam(const char *name, bool value = false);
protected:    
    CCfgXml(CXmlNode *node);
private:
    CXmlNode *m_data;
    
    CCfgXml *m_next;
    CCfgXml *m_child;
};

#endif // !defined(AFX_CFGXML_H__1B0F3CC9_03C1_4776_8ABB_E46E4CF7B1A8__INCLUDED_)
