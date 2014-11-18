#include <client/Matrix.h>
#include <client/Messages.h>
//#include <client/StdAfx.h>
//#include <client/StringParser.h>
#include <wx/tokenzr.h>

SimpleVector::SimpleVector(wxString& str, wxString& colSeparator)
{
    m_storage = new wxArrayString();
#if 1
    wxStringTokenizer st(str, colSeparator, wxTOKEN_RET_EMPTY_ALL);
	if ( str.IsEmpty() ) {
        GetStorage()->Add(str);
	} else {
    	while ( st.HasMoreTokens() ) {
        	GetStorage()->Add(st.GetNextToken());
    	}
    }
#else
    if ( wxString(COMMA_COLUMN_SEPARATOR).Cmp(colSeparator.c_str) == 0 ) {
    	LPCTSTR strLine = _T( str.c_str() );
    	CStringParser values;
    	values.Parse( strLine, poCsvLine );
    	for (int i=0; i<values.GetCount(); i++) {
            GetStorage()->Add( wxString( values.GetAt(i) ) );
    	}
    } else {
        wxStringTokenizer st(str, colSeparator, wxTOKEN_RET_EMPTY_ALL);
    	if ( str.IsEmpty() ) {
            GetStorage()->Add(str);
    	} else {
        	while ( st.HasMoreTokens() ) {
            	GetStorage()->Add(st.GetNextToken());
        	}
        }
    }
#endif
}

SimpleVector::~SimpleVector()
{
    delete m_storage;
}

long SimpleVector::GetSize()
{
    return GetStorage()->GetCount();
}

wxString& SimpleVector::GetDataAt(long index)
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

long StringMatrix::GetSize()
{
    return GetVectors()->GetCount();
}

Vector& StringMatrix::GetVector(long index){
    if ( index < GetSize() ) {
        return *(IGetVector(index));
    } else {
        throw wxString(DESCRIPTOR_INVALID_INDEX);
    }
}

