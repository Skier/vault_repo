/*
 *  $RCSfile: CfgXml.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CfgXml.cpp: implementation of the CCfgXml class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/CfgXml.h>
#include <atf/AssertException.h>
#include <stdlib.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
CCfgXml::CCfgXml(CXmlDocument & doc):m_child(NULL),m_next(NULL) {
    m_data = doc.GetRoot();
    ATF_ASSERT( NULL != m_data );
};

CCfgXml::~CCfgXml() {
    CCfgXml * current = m_child;
    while ( NULL != current ) {
        m_child = current->m_next;
        delete current;
        current = m_child;
    }
};
    
CString CCfgXml::GetName()const {
    ATF_ASSERT( NULL != m_data );
    return m_data->GetName();
};

void CCfgXml::SetName(const char * name) {
    ATF_ASSERT( NULL != name );
    ATF_ASSERT( NULL != m_data );
    m_data->SetName( name );
};
    
ICfg * CCfgXml::CreateChild( const char *name ) {
    ATF_ASSERT( NULL != name );
    ATF_ASSERT( NULL != m_data );
    ICfg * child = GetChild(name);
    if ( NULL != child ) {
        return child;
    } else {
        CXmlNode * node = new CXmlNode(XML_ELEMENT_NODE, name);
        m_data->AddChild(node);
        CCfgXml *cfg = new CCfgXml(node);
        cfg->m_next = m_child;
        m_child = cfg;
        return cfg;
    }
};

ICfg * CCfgXml::GetChild( const char *name) {
    ATF_ASSERT( NULL != name );
    ATF_ASSERT( NULL != m_data );
    CCfgXml * current = m_child;
    while ( NULL != current ) {
        if ( 0 == current->GetName().CompareNoCase(name) ) {
            return current;
        }
        current = current->m_next;
    }
    CXmlNode * node = m_data->GetChild( name );
    if ( NULL != node ) {
        CCfgXml *cfg = new CCfgXml( node );
        cfg->m_next = m_child;
        m_child = cfg;
        return cfg;
    }
    CString msg = "Can't find child configutation [";
    msg += name;
    msg += "]";
    THROW_CFG_EXCEPTION(msg);
    return NULL;
};

ICfg * CCfgXml::GetChildren() {
    ATF_ASSERT( NULL != m_data );
    if( NULL != m_child) {
        return m_child;
    };
    CXmlNode * node = m_data->GetChildren();
    if ( NULL == node ) {
        CString msg = "Can't find child configutation";
        THROW_CFG_EXCEPTION(msg);
    }
    m_child = new CCfgXml( node );
    return m_child;
};

ICfg * CCfgXml::GetNext() {
    ATF_ASSERT( NULL != m_data );
    if ( NULL != m_next ) {
        return m_next;
    }
    CXmlNode * node = m_data->GetNext();
    if ( NULL == node ) {
        return NULL;
    }
    m_next = new CCfgXml( node );
    return m_next;
}; 
    
bool CCfgXml::HasParam(const char *name) const {
    ATF_ASSERT( NULL != name );
    ATF_ASSERT( NULL != m_data );
    return m_data->HasProp(name);
};
    
CString CCfgXml::GetParam(const char *name, const char * defaultValue) const {
    if ( HasParam(name) ) {
        return m_data->GetPropVal(name, defaultValue);
    } else {
        return CString(defaultValue);
    }
};

CString CCfgXml::GetParam(const char *name) const {
    if ( HasParam(name) ) {
        return m_data->GetPropVal(name, "");
    } else {
        THROW_CFG_EXCEPTION(CString("Parameter [")+name+"] not found");
    }
};

void CCfgXml::SetParam(const char *name, const char *value) {
    ATF_ASSERT("NO_IMPLEMENTED");
};
    
long CCfgXml::GetParamAsLong(const char *name, long defaultValue) const {
    if ( !HasParam(name) ) {
        return defaultValue;
    } 
    CString value = GetParam( name );
    return atol(value);
};

long CCfgXml::GetParamAsLong(const char *name) const {
    if ( !HasParam(name) ) {
        THROW_CFG_EXCEPTION(CString("Parameter [")+name+"] not found");
    } 
    CString value = GetParam( name );
    return atol(value);
};

void CCfgXml::SetParam(const char *name, long value) {
    ATF_ASSERT("NO_IMPLEMENTED");
};
   
bool CCfgXml::GetParamAsBool(const char *name, bool defaultValue) const {
    if ( !HasParam(name) ) {
        return defaultValue;
    }
    CString value = GetParam(name);
    if ( 0 == value.CompareNoCase("F") || 0 == value.CompareNoCase("FALSE") ) {
        return false;
    } else if ( 0 == value.CompareNoCase("T") || 0 == value.CompareNoCase("TRUE") ) {
        return true;
    } else {
        return defaultValue;
    }
};
 
bool CCfgXml::GetParamAsBool(const char *name) const {
    if ( !HasParam(name) ) {
        THROW_CFG_EXCEPTION(CString("Parameter [")+name+"] not found");
    }
    CString value = GetParam(name);
    if ( 0 == value.CompareNoCase("F") || 0 == value.CompareNoCase("FALSE") ) {
        return false;
    } else if ( 0 == value.CompareNoCase("T") || 0 == value.CompareNoCase("TRUE") ) {
        return true;
    } else {
        THROW_CFG_EXCEPTION(CString("Parameter [")+name+"] invalid value ["+value+"]");
    }
};

void CCfgXml::SetParam(const char *name, bool value) {
    ATF_ASSERT("NO_IMPLEMENTED");
};

CCfgXml::CCfgXml(CXmlNode *node):m_child(NULL),m_next(NULL) {
    m_data = node;
    ATF_ASSERT( NULL != m_data );
};

