#ifndef __RXCLAIM_XML_DESCRIPTOR_H__
#define __RXCLAIM_XML_DESCRIPTOR_H__

#include <wx/wx.h>
#include <atf/XmlNode.h>
#include <atf/XmlDocument.h>
#include <rxclaim/Descriptor.h>

#pragma warning( disable : 4290 )

class XmlElementDependence : public ElementDependence {
public:
    XmlElementDependence(size_t field, size_t beginPos, size_t endPos)
        : m_field(field), m_beginPos(beginPos), m_endPos(endPos) {};

    virtual size_t GetFieldIndex() { return m_field; };
    virtual size_t GetBeginPosition() { return m_beginPos; };
    virtual size_t GetEndPosition() { return m_endPos; };

private:
    size_t m_field;
    size_t m_beginPos;
    size_t m_endPos;

};

class XmlElementDescriptor : public ElementDescriptor {
public:
    XmlElementDescriptor(CXmlNode& field);
    ~XmlElementDescriptor();

    virtual wxString& GetType();
    virtual wxString& GetName();
    virtual wxString& GetShortName();
    virtual bool GetAlign();
    virtual size_t GetMaxSize();
    virtual size_t GetMinSize();
    virtual bool IsRequired();
    virtual bool IsEnabled();
    virtual bool IsValid(wxString& value);
    virtual wxString& GetDefaultValue();
    virtual wxString& GetSeparator();
    virtual wxArrayString& GetChoices();
    virtual ElementDependence* GetDependence();

    wxString CutValue(wxString& candidate);
    wxString DependenceValue(wxString& candidate);

private:
    void parseNode();
    void parseChoices();
    void parseDependence();
    long parseLongValue(CString& value);
    bool parseBoolValue(CString& value);
    
    CXmlNode* GetNode() { return m_data; }

private: 
    CXmlNode* m_data;
    long      m_maxSize;
    long      m_minSize;
    bool      m_align;
    bool      m_required;
    bool      m_enabled;
    wxString* m_type;
    wxString* m_name;
    wxString* m_shortName;
    wxString* m_defaultValue;
    wxString* m_separator;
    wxArrayString* m_choices;
    XmlElementDependence* m_dependence;

};

class XmlDescriptor : public Descriptor {
public:
    XmlDescriptor(CXmlNode& node);
    ~XmlDescriptor();

    virtual size_t GetSize();
    virtual ElementDescriptor* GetElementDescriptor(size_t index);

    // check is descriptor is valid, throw wxString if not
    void CheckValid();

private:

    void parseDocument();

    wxList* GetChildren() { return m_children; }

    ElementDescriptor* IGetElementDescriptor(size_t index) {
        return (ElementDescriptor*)GetChildren()->Item(index)->GetData();
    }

    CXmlNode* GetRoot() { return m_root; }

private:
    wxList *m_children;
    CXmlNode* m_root;
};

#endif // __RXCLAIM_XML_DESCRIPTOR_H__