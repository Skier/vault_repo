/*
 *  $RCSfile: XmlNode.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlNode.h: interface for the CXmlNode class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XMLNODE_H__BEB60A3A_2938_41A4_9081_B6B8FCEB05C9__INCLUDED_)
#define AFX_XMLNODE_H__BEB60A3A_2938_41A4_9081_B6B8FCEB05C9__INCLUDED_

#include <AFXWIN.H>
#include <atf/XmlProperty.h>
#include <stdio.h>

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


enum XmlNodeType {
    XML_ELEMENT_NODE       =  1,
    XML_ATTRIBUTE_NODE     =  2,
    XML_TEXT_NODE          =  3,
    XML_CDATA_SECTION_NODE =  4,
    XML_ENTITY_REF_NODE    =  5,
    XML_ENTITY_NODE        =  6,
    XML_PI_NODE            =  7,
    XML_COMMENT_NODE       =  8,
    XML_DOCUMENT_NODE      =  9,
    XML_DOCUMENT_TYPE_NODE = 10,
    XML_DOCUMENT_FRAG_NODE = 11,
    XML_NOTATION_NODE      = 12,
    XML_HTML_DOCUMENT_NODE = 13
};

class CXmlNode {
public:
    CXmlNode() : m_properties(NULL), m_parent(NULL),
                  m_children(NULL), m_next(NULL) {};

    CXmlNode(CXmlNode *parent, XmlNodeType type,
              const CString& name, const CString& content,
              CXmlProperty *props, CXmlNode *next);

    virtual ~CXmlNode();

    // copy ctor & operator=. Note that this does NOT copy syblings
    // and parent pointer, i.e. m_parent and m_next will be NULL
    // after using copy ctor and are never unmodified by operator=.
    // On the other hand, it DOES copy children and properties.
    CXmlNode(const CXmlNode& node);
    CXmlNode& operator=(const CXmlNode& node);

    // user-friendly creation:
    CXmlNode(XmlNodeType type, const CString& name,
              const CString& content = "");
    void AddChild(CXmlNode *child);
    void InsertChild(CXmlNode *child, CXmlNode *before_node);
    bool RemoveChild(CXmlNode *child);
    void AddProperty(const CString& name, const CString& value);
    bool DeleteProperty(const CString& name);

    // access methods:
    XmlNodeType GetType() const { return m_type; }
    CString GetName() const { return m_name; }
    CString GetContent() const { return m_content; }

    CString GetPath() const;
    CXmlNode *GetParent() const { return m_parent; }
    CXmlNode *GetNext() const { return m_next; }
    CXmlNode *GetChildren() const { return m_children; }
    CXmlNode *GetChild(const CString& name) const;
    CXmlNode *GetChild(const CString& name, 
                       const CString& propertyName, 
                       const CString& propertyValue) const;

    CXmlProperty *GetProperties() const { return m_properties; }
    bool GetPropVal(const CString& propName, CString *value) const;
    CString GetPropVal(const CString& propName,
                        const CString& defaultVal) const;
    bool HasProp(const CString& propName) const;

    void SetType(XmlNodeType type) { m_type = type; }
    void SetName(const CString& name) { m_name = name; }
    void SetContent(const CString& con) { m_content = con; }

    void SetParent(CXmlNode *parent) { m_parent = parent; }
    void SetNext(CXmlNode *next) { m_next = next; }
    void SetChildren(CXmlNode *child) { m_children = child; }

    void SetProperties(CXmlProperty *prop) { m_properties = prop; }
    void AddProperty(CXmlProperty *prop);

private:
    XmlNodeType m_type;
    CString m_name;
    CString m_content;
    CXmlProperty *m_properties;
    CXmlNode *m_parent, *m_children, *m_next;

    void DoCopy(const CXmlNode& node);
};

#endif // !defined(AFX_XMLNODE_H__BEB60A3A_2938_41A4_9081_B6B8FCEB05C9__INCLUDED_)
