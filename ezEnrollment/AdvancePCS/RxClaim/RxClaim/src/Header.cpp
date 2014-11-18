#include "wx/file.h"
#include "rxclaim/header.h"
#include "rxclaim/filematrix.h"

//
// DefaultHeaderField
//
DefaultHeaderField::DefaultHeaderField(ElementDescriptor* elementDescriptor)
   : m_elementDescriptor(elementDescriptor)
{
    m_value = elementDescriptor->GetDefaultValue();
};

ElementDescriptor* DefaultHeaderField::GetDescriptor()
{
    return m_elementDescriptor;
};

wxString& DefaultHeaderField::GetValue()
{
    return m_value;
};

void DefaultHeaderField::SetValue(wxString& value)
{
    if ( GetDescriptor()->IsEnabled() ) {
    m_value = value;
    } else {
        throw wxString("Element [" + GetDescriptor()->GetName() + "] not enabled.");
    }
};


//
// DefaultHeader
//
DefaultHeader::DefaultHeader(Descriptor* descriptor)
    : m_descriptor(descriptor)
{
    m_fields = new wxList();
    for (size_t i=0; i<descriptor->GetSize(); i++) {
        GetFields()->Append(
            (wxObject*) new DefaultHeaderField(
            GetDescriptor()->GetElementDescriptor(i)));
    }
};

Descriptor* DefaultHeader::GetDescriptor()
{
    return m_descriptor;
};

void DefaultHeader::Clear()
{
    for (int i=0; i<GetSize(); i++) {
        HeaderField* field = GetField(i);
        if (field->GetDescriptor()->IsEnabled()) {
            field->SetValue(wxString(""));
        }
    }
};

bool DefaultHeader::IsValid(wxString& reason)
{
    bool m_valid = true;

    for (int i=0; i<GetSize(); i++) {
        HeaderField* field = GetField(i);
        if (field->GetDescriptor()->IsEnabled()) {

// ----- convert to Upper Case
            if ( field->GetDescriptor()->GetType().Cmp("text") == 0 ) {
                field->SetValue( field->GetValue().Upper());
            }
//-------------
            
            if ( field->GetValue().IsEmpty()
                && field->GetDescriptor()->IsRequired()) {
                reason << field->GetDescriptor()->GetName() + ": cannot be empty\n"; 
                m_valid = false;
            } else if ((field->GetDescriptor()->GetType().Cmp("date")==0)||(field->GetDescriptor()->GetType().Cmp("longdate")==0)) {
                if ( field->GetValue().IsEmpty() || field->GetValue().Cmp("00/00/0000") == 0 || field->GetValue().Cmp("99/99/9999") == 0) {
                    continue;
                }
                wxDateTime date;
                if ( !date.ParseFormat(field->GetValue(), "%m/%d/%Y") ) {
                    reason << field->GetDescriptor()->GetName() + ": incorrect date \"" + field->GetValue() + "\"\n"; 
                    m_valid = false;
                } else {
                    date.ParseFormat(field->GetValue(), "%m/%d/%Y");
                    if ( date.GetYear() < 1000 ) {
                    reason << field->GetDescriptor()->GetName() + ": year must more then 1000 \n"; 
                        m_valid = false;
                    } else {
                        field->SetValue(wxString(date.Format("%m/%d/%Y")));
                    }
                }
            } else if ( field->GetValue().Len() > field->GetDescriptor()->GetMaxSize()) {
                    reason << field->GetDescriptor()->GetName() + ": value too long " + field->GetValue() +"\n";
                m_valid = false;
            } else if ((field->GetDescriptor()->GetType().Cmp("numeric")==0)
                    &&(!field->GetValue().IsNumber())) {
                    reason << field->GetDescriptor()->GetName() + ": value too big " + field->GetValue() + "\n"; 
                m_valid = false;
            } 

        }
    }
    return m_valid;
};

long DefaultHeader::GetSize()
{
    return GetFields()->GetCount();
};

HeaderField* DefaultHeader::GetField(long index)
{
    if ( index < GetSize() ) {
        return (HeaderField*)(GetFields()->Item(index)->GetData());
    } else {
        throw wxString("Invalid index.");
    }
};

void DefaultHeader::Load(wxString& filename)
{
    FileMatrix fm(filename, 3);
    if ( fm.GetSize() < 3 ) {
        throw wxString("Invalid data file [" + filename + "] header section. Must have minimum three lines.");
    }
    Vector& names = fm.GetVector(0);
    Vector& values = fm.GetVector(1);
    if ( names.GetSize() != GetDescriptor()->GetSize() ) {
        throw wxString("Invalid data file [" + filename + "] header section. Data & config mismatch.");
    }
    if ( names.GetSize() != values.GetSize() ) {
        throw wxString("Invalid data file [" + filename + "] header section. Names & values mismatch.");
    }
    // Simple algorithm, do not check names
    int max = names.GetSize();
    for (int i=0; i<max; i++) {
        HeaderField* field = GetField(i);
        if ( field->GetDescriptor()->IsEnabled() ) {
            field->SetValue(values.GetDataAt(i));
        }
    }
}

void DefaultHeader::Store(wxString& filename)
{
    wxFile file(filename, wxFile::write);
    wxString names, values;
    
    for (int i=0; i<GetSize(); i++) {
        HeaderField* field = GetField(i);
        names += field->GetDescriptor()->GetName();
        values += field->GetValue();
        names += DEFAULT_COLUMN_SEPARATOR;
        values += DEFAULT_COLUMN_SEPARATOR;
    }
    file.Write(names + DEFAULT_LINE_SEPARATOR);
    file.Write(values + DEFAULT_LINE_SEPARATOR);
    file.Close();
}
