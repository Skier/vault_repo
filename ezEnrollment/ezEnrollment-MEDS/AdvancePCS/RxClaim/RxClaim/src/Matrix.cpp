#include <rxclaim/Matrix.h>
#include <rxclaim/Messages.h>
#include <wx/tokenzr.h>

SimpleVector::SimpleVector(wxString& str, wxString& colSeparator)
{
    m_storage = new wxArrayString();
    wxStringTokenizer st(str, colSeparator, wxTOKEN_RET_EMPTY_ALL);
    if ( str.IsEmpty() ) {
        GetStorage()->Add(str);
    } else {
        while ( st.HasMoreTokens() ) {
            GetStorage()->Add(st.GetNextToken());
        }
    }
}

SimpleVector::~SimpleVector()
{
    delete m_storage;
}

size_t SimpleVector::GetSize()
{
    return GetStorage()->GetCount();
}

wxString& SimpleVector::GetDataAt(size_t index)
{
    if ( index < GetSize() ) {
        return GetStorage()->Item(index);
    } else {
        throw wxString(DESCRIPTOR_INVALID_INDEX);
    }
}

//
// StringMatrix
//
StringMatrix::StringMatrix()
{
    m_vectors = new VectorArray();
}

StringMatrix::~StringMatrix()
{
    for (unsigned int i=0; i<GetVectors()->GetCount(); i++) {
        Vector *v = IGetVector(i);
        delete v;
    }
    delete m_vectors;
}

StringMatrix::Load(wxArrayString& strs, wxString& colSeparator)
{
    for (unsigned int i=0; i<strs.GetCount(); i++) {
        Vector *v = new SimpleVector(strs.Item(i), colSeparator);
        GetVectors()->Add(v);
    }
}

size_t StringMatrix::GetSize()
{
    return GetVectors()->GetCount();
}

Vector& StringMatrix::GetVector(size_t index){
    if ( index < GetSize() ) {
        return *(IGetVector(index));
    } else {
        throw wxString(DESCRIPTOR_INVALID_INDEX);
    }
}

