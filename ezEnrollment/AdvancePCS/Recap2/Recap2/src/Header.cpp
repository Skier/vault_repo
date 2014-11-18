#include "wx/file.h"
#include "client/header.h"
#include "client/filematrix.h"

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
    m_value = value;
};


//
// DefaultHeader
//
DefaultHeader::DefaultHeader(Descriptor* descriptor)
    : m_descriptor(descriptor)
{
    m_fields = new wxList();
    for (int i=0; i<descriptor->GetSize(); i++) {
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
        field->SetValue(wxString());
    }
};

bool DefaultHeader::IsValid(wxString& reason)
{
    for (int i=0; i<GetSize(); i++) {
        HeaderField* field = GetField(i);
        if ( !field->GetDescriptor()->IsValid(field->GetValue()) ) {
            return false;
        }
    }
    return true;
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
//    FileMatrix fm(filename, 3, DEFAULT_LINE_SEPARATOR, COMMA_COLUMN_SEPARATOR);
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
        field->SetValue(values.GetDataAt(i));
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
