
#include <client/Messages.h>
#include <client/ComposerDescriptor.h>
#include <wx/datetime.h>

#define TYPE_ATTR                "type"
#define SIZE_ATTR                "size"
#define VALUE_ATTR               "value"
#define JUSTIFY_ATTR             "justify"
#define FILLER_ATTR              "filler"
#define HEADER_ATTR              "headerField"
#define GRID_ATTR                "gridField"

#define COMPOSER_FIELD_ATTR      "compField"
#define SECTION_ATTR             "section"

#define DEFAULT_JUSTIFY_ATTR             "default_justify"
#define DEFAULT_FILLER_ATTR              "default_filler"

//------------------------------------------------------------------------------
// SectionFieldDescriptor static
//------------------------------------------------------------------------------
SectionFieldAlignment c_alignment;
wxChar c_filler;

SectionFieldAlignment SectionFieldDescriptor::GetDefaultAlignment()
{
    return c_alignment;
}

wxChar SectionFieldDescriptor::GetDefaultFiller()
{
    return c_filler;
}

void SectionFieldDescriptor::SetDefaultAlignment(SectionFieldAlignment align)
{
    c_alignment = align;
}

void SectionFieldDescriptor::SetDefaultFiller(wxChar filler)
{
    c_filler = filler;
}

SectionFieldAlignment SectionFieldDescriptor::ParseAlignment(wxString& value)
{
    if ( value.Cmp(_("L")) == 0 ) {
        return FA_LEFT;
    } else if ( value.Cmp(_("R")) == 0 ) {
        return FA_RIGHT;
    } else {
        throw wxString::Format(DESCRIPTOR_WRONG_ALIGNMENT + wxString(" %s"), value);
    }
}

//------------------------------------------------------------------------------
// SectionFieldDescriptor
//------------------------------------------------------------------------------
SectionFieldDescriptor::SectionFieldDescriptor(CXmlNode& node)
    : m_data(&node)
{
    Parse();
};

SectionFieldDescriptor::~SectionFieldDescriptor() 
{
    delete m_sourceValue;
}
    
size_t SectionFieldDescriptor::GetSize()
{
    return m_size;
}

wxChar SectionFieldDescriptor::GetFiller()
{
    return m_filler;
}

SectionFieldAlignment SectionFieldDescriptor::GetAlignment()
{
    return m_alignment;
}

SectionFieldSource SectionFieldDescriptor::GetFieldSource()
{
    return m_fieldSource;
}

wxString& SectionFieldDescriptor::GetFieldSourceValue()
{
    return *m_sourceValue;
}

// Private 
void SectionFieldDescriptor::Parse()
{
    if ( !GetNode()->HasProp(SIZE_ATTR) ) { 
        throw wxString::Format(DESCRIPTOR_WRONG_XML + wxString("  %s"), wxString(GetNode()->GetName()));
    }
    m_size = atol(GetNode()->GetPropVal(SIZE_ATTR, ""));

    bool hasValue = GetNode()->HasProp(VALUE_ATTR);
    bool hasHeader = GetNode()->HasProp(HEADER_ATTR);
    bool hasDetail = GetNode()->HasProp(GRID_ATTR);
    if ( (hasValue && (hasHeader || hasDetail))
         || (hasHeader && (hasDetail || hasValue))
         || (hasDetail && (hasValue || hasHeader)) ) {
        throw wxString::Format(DESCRIPTOR_WRONG_XML + wxString("  %s"), wxString(GetNode()->GetName()));
    }

    m_sourceValue = new wxString();
    if ( hasValue ) {
        m_fieldSource = FS_VALUE;
        *m_sourceValue << wxString(GetNode()->GetPropVal(VALUE_ATTR, ""));
    } else if (hasHeader) {
        m_fieldSource = FS_HEADER;
        *m_sourceValue << wxString(GetNode()->GetPropVal(HEADER_ATTR, ""));
    } else if (hasDetail) {
        m_fieldSource = FS_DETAIL;
        *m_sourceValue << wxString(GetNode()->GetPropVal(GRID_ATTR, ""));
    } else {
        m_fieldSource = FS_UNUSED;
    }
    m_alignment = ParseAlignment(wxString(GetNode()->GetPropVal(JUSTIFY_ATTR, "L")));
    m_filler = wxString(GetNode()->GetPropVal(FILLER_ATTR, GetDefaultFiller()))[0];
}
    
wxString SectionFieldDescriptor::Format(wxString& value)
{
    if ( value.Length() > GetSize() ) {
        return value.Truncate(GetSize());
    } else {
        wxString addon(GetFiller(), GetSize() - value.Length());
        if ( GetAlignment() == FA_LEFT ) {
            return value + addon;
        } else {
            return addon + value;
        }
    }
}

wxString SectionFieldDescriptor::Encode(wxString& value, wxString& type)
{
   wxDateTime date;
   if ( type.Cmp("date") == 0 
        && value.Length() != 0 ) {
       if ( date.ParseFormat(value, "%Y/%m/%d") ) {
           return date.Format("%Y%m%d");
       } else {
           return value;
       }
   } else {
       return value;
   }
}

//------------------------------------------------------------------------------
// SectionDescriptor
//------------------------------------------------------------------------------
SectionDescriptor::SectionDescriptor(CXmlNode& node)
    : m_data(&node)
{
    Parse();
}

SectionDescriptor::~SectionDescriptor() 
{
    for (std::vector<SectionFieldDescriptor*>::iterator i=m_fields.begin(); 
         i != m_fields.end(); ++i ) {
        delete *i;
    }
}
    
size_t SectionDescriptor::GetSize()
{
    return m_fields.size();
}

wxString& SectionDescriptor::GetType()
{
    return *m_type;
}

SectionFieldDescriptor* SectionDescriptor::GetSectionFieldDescriptor(size_t index)
{
    if ( index < GetSize() ) {
        return m_fields[index];
    } else {
        throw wxString(DESCRIPTOR_INVALID_INDEX);
    }
}

void SectionDescriptor::Parse()
{
    if ( !GetNode()->HasProp(TYPE_ATTR) ) {
        throw wxString::Format(DESCRIPTOR_WRONG_XML + wxString("  %s"), wxString(GetNode()->GetName()));
    }
    m_type = new wxString(GetNode()->GetPropVal(TYPE_ATTR, ""));

    CXmlNode* current = GetNode()->GetChildren();
    while ( NULL != current ) {
        if ( wxString(current->GetName()).Cmp(COMPOSER_FIELD_ATTR) == 0 ) {
            SectionFieldDescriptor* elem = new SectionFieldDescriptor(*current);
            m_fields.push_back(elem);
        }
        current = current->GetNext();
    }
}

wxString SectionDescriptor::Format(Header* header, Detail* detail, size_t row)
{
    wxString section;
    for (size_t i=0; i<GetSize(); i++) {
        SectionFieldDescriptor* sfd = GetSectionFieldDescriptor(i);
        wxString value;
        wxString type;
        switch ( sfd->GetFieldSource() ) {
        case FS_VALUE:
            value = sfd->GetFieldSourceValue();
            break;    
        case FS_HEADER:
            value = header->GetField(
                header->GetDescriptor()->GetElementDescriptorIndex(
                    sfd->GetFieldSourceValue()))->GetValue();
            type = header->GetDescriptor()->GetElementDescriptor(
                       header->GetDescriptor()->GetElementDescriptorIndex(
                           sfd->GetFieldSourceValue()))->GetType();
            break;    
        case FS_DETAIL:
            value = detail->GetValue(row,
                detail->GetDescriptor()->GetElementDescriptorIndex(
                    sfd->GetFieldSourceValue()));
            type = detail->GetDescriptor()->GetElementDescriptor(
                       detail->GetDescriptor()->GetElementDescriptorIndex(
                           sfd->GetFieldSourceValue()))->GetType();
            break;    
        case FS_UNUSED:
            if ( GetType().Cmp("R99") == 0 
                 && i == 1 ) {
                value = wxString::Format(wxT("%d"), row);
            }
            break;
        default:
            break;
        }
        section << sfd->Format(sfd->Encode(value, type));
    }
    return section;
}

//------------------------------------------------------------------------------
// ComposerDescriptor
//------------------------------------------------------------------------------
ComposerDescriptor::ComposerDescriptor(CXmlNode& node)
    : m_data(&node)
{
    Parse();
}

ComposerDescriptor::~ComposerDescriptor() 
{
    for (std::vector<SectionDescriptor*>::iterator i=m_sections.begin(); 
         i != m_sections.end(); ++i ) {
        delete *i;
    }
}
    
size_t ComposerDescriptor::GetSize()
{
    return m_sections.size();
}

SectionDescriptor* ComposerDescriptor::GetSectionDescriptor(size_t index)
{
    if ( index < GetSize() ) {
        return m_sections[index];
    } else {
        throw wxString(DESCRIPTOR_INVALID_INDEX);
    }
}

void ComposerDescriptor::Parse()
{
    if ( !GetNode()->HasProp(DEFAULT_JUSTIFY_ATTR) 
         || !GetNode()->HasProp(DEFAULT_FILLER_ATTR) ) {
        throw wxString::Format(DESCRIPTOR_WRONG_XML + wxString(" %s"), wxString(GetNode()->GetName()));
    }
    SectionFieldDescriptor::SetDefaultAlignment(SectionFieldDescriptor::ParseAlignment(wxString(GetNode()->GetPropVal(DEFAULT_JUSTIFY_ATTR, "L"))));
    SectionFieldDescriptor::SetDefaultFiller(wxString(GetNode()->GetPropVal(DEFAULT_FILLER_ATTR, " "))[0]);

    CXmlNode* current = GetNode()->GetChildren();
    while ( NULL != current ) {
        if ( wxString(current->GetName()).Cmp(SECTION_ATTR) == 0 ) {
            SectionDescriptor* elem = new SectionDescriptor(*current);
            m_sections.push_back(elem);
        }
        current = current->GetNext();
    }
}

