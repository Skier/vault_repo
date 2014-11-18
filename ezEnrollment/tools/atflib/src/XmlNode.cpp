/*
 *  $RCSfile: XmlNode.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlNode.cpp: implementation of the CXmlNode class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/XmlNode.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CXmlNode::CXmlNode(CXmlNode *parent,XmlNodeType type,
                     const CString& name, const CString& content,
                     CXmlProperty *props, CXmlNode *next)
    : m_type(type), m_name(name), m_content(content),
      m_properties(props), m_parent(parent),
      m_children(NULL), m_next(next)
{
    if (m_parent)
    {
        if (m_parent->m_children)
        {
            m_next = m_parent->m_children;
            m_parent->m_children = this;
        }
        else
            m_parent->m_children = this;
    }
}



CXmlNode::CXmlNode(XmlNodeType type, const CString& name,
                     const CString& content)
    : m_type(type), m_name(name), m_content(content),
      m_properties(NULL), m_parent(NULL),
      m_children(NULL), m_next(NULL)
{}



CXmlNode::CXmlNode(const CXmlNode& node) {
    m_next = NULL;
    m_parent = NULL;
    DoCopy(node);
}



CXmlNode::~CXmlNode() {
    CXmlNode *c, *c2;
    for (c = m_children; c; c = c2) {
        c2 = c->m_next;
        delete c;
    }

    CXmlProperty *p, *p2;
    for (p = m_properties; p; p = p2) {
        p2 = p->GetNext();
        delete p;
    }
}



CXmlNode& CXmlNode::operator=(const CXmlNode& node) {
    delete m_properties;
    delete m_children;
    DoCopy(node);
    return *this;
}



void CXmlNode::DoCopy(const CXmlNode& node) {

    m_type = node.m_type;
    m_name = node.m_name;
    m_content = node.m_content;
    m_children = NULL;

    CXmlNode *n = node.m_children;
    while (n) {
        AddChild(new CXmlNode(*n));
        n = n->GetNext();
    }

    m_properties = NULL;
    CXmlProperty *p = node.m_properties;
    while (p) {
       AddProperty(p->GetName(), p->GetValue());
       p = p->GetNext();
    }
}


bool CXmlNode::HasProp(const CString& propName) const {

    CXmlProperty *prop = GetProperties();

    while (prop) {
        if (0 == prop->GetName().CompareNoCase(propName)) return true;
        prop = prop->GetNext();
    }

    return false;
}



bool CXmlNode::GetPropVal(const CString& propName, CString *value) const {
    CXmlProperty *prop = GetProperties();

    while (prop) {
        if ( 0 == prop->GetName().CompareNoCase(propName) ) {
            *value = prop->GetValue();
            return true;
        }
        prop = prop->GetNext();
    }

    return false;
}


CString CXmlNode::GetPath() const {
    CXmlNode *parent = GetParent();
    if ( NULL == parent ) {
        return GetName();
    } else {
        CString result = parent->GetPath();
        result += "/";
        result += GetName();
        return result;
    }
};


CXmlNode *CXmlNode::GetChild(const CString& name) const {
    for (CXmlNode *node = GetChildren(); NULL != node; node = node->GetNext() ) {
        if ( 0 == node->GetName().CompareNoCase(name) ) {
            return node;
        }
    }
    return NULL;
};


CXmlNode *CXmlNode::GetChild(const CString& name, 
                             const CString& propertyName, 
                             const CString& propertyValue) const {

    CString value;
    for (CXmlNode *node = GetChildren(); NULL != node; node = node->GetNext() ) {
        if ( 0 == node->GetName().CompareNoCase(name) ) {
            if ( node->GetPropVal(propertyName, &value) ) {
                if ( 0 == value.CompareNoCase(propertyValue) ) {
                    return node;
                }
            }
        }
    }
    return NULL;
};

CString CXmlNode::GetPropVal(const CString& propName, const CString& defaultVal) const {

    CString tmp;
    if (GetPropVal(propName, &tmp))
        return tmp;
    else
        return defaultVal;
}



void CXmlNode::AddChild(CXmlNode *child) {

    if (m_children == NULL) {
        m_children = child;
    } else {
        CXmlNode *ch = m_children;
        while (ch->m_next) ch = ch->m_next;
        ch->m_next = child;
    }
    child->m_next = NULL;
    child->m_parent = this;
}



void CXmlNode::InsertChild(CXmlNode *child, CXmlNode *before_node) {

    if (m_children == before_node) {
       m_children = child;
    } else {
        CXmlNode *ch = m_children;
        while (ch->m_next != before_node) ch = ch->m_next;
        ch->m_next = child;
    }

    child->m_parent = this;
    child->m_next = before_node;
}



bool CXmlNode::RemoveChild(CXmlNode *child) {
    if (m_children == NULL)
        return false;
    else if (m_children == child) {
        m_children = child->m_next;
        child->m_parent = NULL;
        child->m_next = NULL;
        return true;
    } else {
        CXmlNode *ch = m_children;
        while (ch->m_next) {
            if (ch->m_next == child) {
                ch->m_next = child->m_next;
                child->m_parent = NULL;
                child->m_next = NULL;
                return true;
            }
            ch = ch->m_next;
        }
        return false;
    }
}



void CXmlNode::AddProperty(const CString& name, const CString& value) {
    AddProperty(new CXmlProperty(name, value, NULL));
}

void CXmlNode::AddProperty(CXmlProperty *prop){
    if (m_properties == NULL) {
        m_properties = prop;
    } else {
        CXmlProperty *p = m_properties;
        while (p->GetNext()) p = p->GetNext();
        p->SetNext(prop);
    }
}



bool CXmlNode::DeleteProperty(const CString& name) {
    if (m_properties == NULL) {
        return false;

    } else if ( 0 == m_properties->GetName().CompareNoCase(name)) {
        CXmlProperty *prop = m_properties;
        m_properties = prop->GetNext();
        prop->SetNext(NULL);
        delete prop;
        return true;

    } else {
        CXmlProperty *p = m_properties;
        while (p->GetNext()) {
            if (0 == p->GetNext()->GetName().CompareNoCase(name) ) {
                CXmlProperty *prop = p->GetNext();
                p->SetNext(prop->GetNext());
                prop->SetNext(NULL);
                delete prop;
                return true;
            }
            p = p->GetNext();
        }
        return false;
    }
}

