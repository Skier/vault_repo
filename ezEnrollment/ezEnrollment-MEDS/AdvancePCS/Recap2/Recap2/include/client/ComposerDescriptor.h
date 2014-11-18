#ifndef ___COMPOSER_DESCRIPTOR___
#define ___COMPOSER_DESCRIPTOR___

#include <vector>
#include <wx/wx.h>
#include <atf/XmlNode.h>
#include <client/header.h>
#include <client/detail.h>

enum SectionFieldAlignment {FA_LEFT, FA_RIGHT};

enum SectionFieldSource {FS_UNUSED, FS_VALUE, FS_HEADER, FS_DETAIL};

class SectionFieldDescriptor {
public:
// Static
    // Defaults
    static SectionFieldAlignment GetDefaultAlignment();
    static wxChar GetDefaultFiller();
    static void SetDefaultAlignment(SectionFieldAlignment align);
    static void SetDefaultFiller(wxChar filler);

    // Parsing of enum
    static SectionFieldAlignment ParseAlignment(wxString& value);

public:
    SectionFieldDescriptor(CXmlNode& field);
    ~SectionFieldDescriptor();

    virtual size_t GetSize();

    virtual wxChar GetFiller();

    virtual SectionFieldAlignment GetAlignment();

    virtual SectionFieldSource GetFieldSource();

    virtual wxString& GetFieldSourceValue();

    virtual wxString Format(wxString& value);

    virtual wxString Encode(wxString& value, wxString& type);

private:
    CXmlNode*                m_data;
    size_t                   m_size;
    wxChar                   m_filler;
    SectionFieldAlignment    m_alignment;
    SectionFieldSource       m_fieldSource;
    wxString*                m_sourceValue;

    void Parse();
    
    CXmlNode* GetNode()
    {
        return m_data;
    }
};

class SectionDescriptor {
public:
    SectionDescriptor(CXmlNode& field);
    ~SectionDescriptor();

    virtual size_t GetSize();
    virtual wxString& GetType();

    virtual SectionFieldDescriptor* GetSectionFieldDescriptor(size_t index);

    virtual wxString Format(Header* header, Detail* detail, size_t row=0);

private:
    CXmlNode*                            m_data;
    size_t                               m_size;
    wxString*                            m_type;
    std::vector<SectionFieldDescriptor*> m_fields;

    void Parse();

    CXmlNode* GetNode()
    {
        return m_data;
    }
};

class ComposerDescriptor {
public:
    ComposerDescriptor(CXmlNode& field);
    ~ComposerDescriptor();

    virtual size_t GetSize();

    virtual SectionDescriptor* GetSectionDescriptor(size_t index);

private:
    CXmlNode*                            m_data;
    size_t                               m_size;
    std::vector<SectionDescriptor*>      m_sections;

    void Parse();

    CXmlNode* GetNode()
    {
        return m_data;
    }
};

#endif // ___COMPOSER_DESCRIPTOR___