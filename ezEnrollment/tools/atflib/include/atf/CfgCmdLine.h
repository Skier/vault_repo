/*
 *  $RCSfile: CfgCmdLine.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CfgCmdLine.h: interface for the CCfgCmdLine class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CFGCMDLINE_H__4E5ABAD1_4134_4C1A_8CC5_D818035E2D4A__INCLUDED_)
#define AFX_CFGCMDLINE_H__4E5ABAD1_4134_4C1A_8CC5_D818035E2D4A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Cfg.h>

struct CfgEntry {
    CString key;
    CString value;
    CfgEntry * next;
    CfgEntry():next(NULL),key(""),value(""){};
};

class CCfgCmdLine : public ICfg {
public:
    CCfgCmdLine(int argc, char ** argv);
    virtual ~CCfgCmdLine();

    CString GetName()const;
    void SetName(const char * name);
    
    ICfg * CreateChild( const char *name );
    ICfg * GetChild( const char *name);

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
    
private:
    CfgEntry * GetEntry( const char * name ) const;    
private:
    CfgEntry * m_data;
};

#endif // !defined(AFX_CFGCMDLINE_H__4E5ABAD1_4134_4C1A_8CC5_D818035E2D4A__INCLUDED_)
