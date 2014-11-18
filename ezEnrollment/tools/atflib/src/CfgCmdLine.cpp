/*
 *  $RCSfile: CfgCmdLine.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CfgCmdLine.cpp: implementation of the CCfgCmdLine class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/CfgCmdLine.h>
#include <atf/AssertException.h>
#include <string.h>
#include <stdlib.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CCfgCmdLine::CCfgCmdLine(int argc, char ** argv):m_data(NULL) {
    int i = 1;
    char* value;
    for(i=1;i<argc;i++){
        CfgEntry * current =  new CfgEntry();
        value = strchr(argv[i], '=');
        if(NULL == value) {
            current->key = argv[i];
        } else {
            value++;
            char *key = new char[value-argv[i]];
            strncpy(key,argv[i],value-argv[i]-1);
            key[value-argv[i]-1]=0;
            current->key = key;
            current->value=value;
            delete []key;
        }
        current->next = m_data;
        m_data = current;
    }
};

CCfgCmdLine::~CCfgCmdLine() {
    CfgEntry  * current = m_data;
    while ( NULL != current ) {
        m_data = m_data->next;
        delete current;
        current = m_data;
    }
};

CString CCfgCmdLine::GetName()const {
    return CString("");
};

void CCfgCmdLine::SetName(const char * name) {
    ATF_ASSERT("NOT_IMPLEMENTED");
};
    
ICfg * CCfgCmdLine::CreateChild( const char *name ) {
    ATF_ASSERT("NOT_IMPLEMENTED");
    return NULL;
};

ICfg * CCfgCmdLine::GetChildren() {
    ATF_ASSERT("NOT_IMPLEMENTED");
    return NULL;
};

ICfg * CCfgCmdLine::GetNext(){
    ATF_ASSERT("NOT_IMPLEMENTED");
    return NULL;
}; 


ICfg *CCfgCmdLine:: GetChild( const char *name) {
    ATF_ASSERT("NOT_IMPLEMENTED");
    return NULL;
};
    
bool CCfgCmdLine::HasParam(const char *name) const {
    ATF_ASSERT( NULL != name );
    return NULL != GetEntry(name);
};
    
CString CCfgCmdLine::GetParam(const char *name, const char * defaultValue) const {
    ATF_ASSERT( NULL != name );
    CfgEntry * current = GetEntry(name);
    if ( NULL == current ) {
        return CString(defaultValue);
    } else {
        return current->value;
    }
};

CString CCfgCmdLine::GetParam(const char *name) const {
    ATF_ASSERT( NULL != name );
    CfgEntry * current = GetEntry(name);
    if ( NULL == current ) {
        THROW_CFG_EXCEPTION(CString("Parameter [")+name+"] not found");
    } else {
        return current->value;
    }
};

void CCfgCmdLine::SetParam(const char *name, const char *value) {
    ATF_ASSERT("NOT_IMPLEMENTED");
};
    
long CCfgCmdLine::GetParamAsLong(const char *name, long defaultValue) const {
    ATF_ASSERT( NULL != name );
    CfgEntry * current = GetEntry(name);
    if ( NULL == current ) {
        return defaultValue;
    } else {
        return atol(current->value);
    }
};

long CCfgCmdLine::GetParamAsLong(const char *name) const {
    ATF_ASSERT( NULL != name );
    CfgEntry * current = GetEntry(name);
    if ( NULL == current ) {
        THROW_CFG_EXCEPTION(CString("Parameter [")+name+"] not found");
    } else {
        return atol(current->value);
    }
};

void CCfgCmdLine::SetParam(const char *name, long value) {
    ATF_ASSERT("NOT_IMPLEMENTED");
};
    
bool CCfgCmdLine::GetParamAsBool(const char *name, bool defaultValue) const {
    ATF_ASSERT( NULL != name );
    CfgEntry * current = GetEntry(name);
    if ( NULL == current ) {
        return defaultValue;
    } else {
        if ( 0 == current->value.CompareNoCase("T") || 
             0 == current->value.CompareNoCase("TRUE") ) 
        {
            return true;
        } else {
            if ( 0 == current->value.CompareNoCase("F") || 
                 0 == current->value.CompareNoCase("FALSE") ) 
            {
                return false;
            } else {
                return defaultValue;
            }
        }
    }
};

bool CCfgCmdLine::GetParamAsBool(const char *name) const {
    ATF_ASSERT( NULL != name );
    CfgEntry * current = GetEntry(name);
    if ( NULL == current ) {
        THROW_CFG_EXCEPTION(CString("Parameter [")+name+"] not found");
    } else {
        if ( 0 == current->value.CompareNoCase("T") || 
             0 == current->value.CompareNoCase("TRUE") ) 
        {
            return true;
        } else {
            if ( 0 == current->value.CompareNoCase("F") || 
                 0 == current->value.CompareNoCase("FALSE") ) 
            {
                return false;
            } else {
                THROW_CFG_EXCEPTION(CString("Invalid value [")+current->value+"] for parameter ["+name+"]");
            }
        }
    }
};


void CCfgCmdLine::SetParam(const char *name, bool value) {
    ATF_ASSERT("NOT_IMPLEMENTED");
};

CfgEntry * CCfgCmdLine::GetEntry( const char * name ) const {
    CfgEntry * current = m_data;
    while ( NULL != current ) {
        if ( 0 == current->key.CompareNoCase(name) ) {
            return current;
        }
        current = current->next;
    }
    return NULL;
};    

