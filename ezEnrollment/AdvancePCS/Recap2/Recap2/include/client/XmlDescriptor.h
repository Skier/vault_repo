#ifndef ___XML_DESCRIPTOR___
#define ___XML_DESCRIPTOR___

#pragma warning(disable:4290)
#pragma warning(disable:4786)   

#include <map>
#include <wx/wx.h>
#include <atf/XmlNode.h>
#include <atf/XmlDocument.h>
#include <client/descriptor.h>

class XmlElementDependence : public ElementDependence {
public:
    XmlElementDependence(size_t field, size_t beginPos, size_t endPos)
        : m_field(field), m_beginPos(beginPos), m_endPos(endPos) {};

    virtual size_t GetFieldIndex()
    {
        return m_field;
    };

    virtual size_t GetBeginPosition()
    {
        return m_beginPos;
    };

    virtual size_t GetEndPosition()
    {
        return m_endPos;
    };

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
    virtual long GetMaxSize();
    virtual long GetMinSize();
    virtual bool IsRequired();
    virtual wxString& GetDefaultValue();
    virtual wxString& GetSeparator();
    virtual wxArrayString& GetChoices();
    virtual ElementDependence* GetDependence();

    virtual bool IsValid(wxString& value);
    wxString CutValue(wxString& candidate);
    wxString DependenceValue(wxString& candidate);

private:
    CXmlNode* m_data;
    long      m_maxSize;
    long      m_minSize;
    bool      m_required;
    wxString* m_type;
    wxString* m_name;
    wxString* m_shortName;
    wxString* m_defaultValue;
    wxString* m_separator;
    wxArrayString* m_choices;
    XmlElementDependence* m_dependence;

    void parseNode();
    void parseChoices();
    void parseDependence();
    long parseLongValue(CString& value);
    bool parseBoolValue(CString& value);
    
    CXmlNode* GetNode()
    {
        return m_data;
    }
};

class XmlDescriptor : public Descriptor {
public:
    XmlDescriptor(CXmlNode& node);
    ~XmlDescriptor();

    virtual long GetSize();
    virtual ElementDescriptor* GetElementDescriptor(long index);
    virtual long GetElementDescriptorIndex(wxString& name);

    // Check is descriptor is valid, throw wxString if not
    void CheckValid();
private:
    wxList *m_children;
    std::map<wxString, long> m_nameToIndex;
    CXmlNode* m_root;

    void parseDocument();

    wxList* GetChildren()
    {
        return m_children;
    }

    ElementDescriptor* IGetElementDescriptor(long index)
    {
        return (ElementDescriptor*)GetChildren()->Item(index)->GetData();
    }

    CXmlNode* GetRoot()
    {
        return m_root;
    }

};

#endif // ___XML_DESCRIPTOR___