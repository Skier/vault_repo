#include <rxclaim/ClipboardMatrix.h>
#include <rxclaim/Messages.h>
#include <wx/tokenzr.h>
#include <wx/file.h>

ClipboardMatrix::ClipboardMatrix(wxString& lineSeparator, wxString& colSeparator)
{
    wxArrayString strs;
    GetDataFromClipboard();
    wxStringTokenizer st(m_textData.GetText(), lineSeparator);
    while ( st.HasMoreTokens() ) {
        wxString str = wxString();
        str = st.GetNextToken();
        if ( str.Last() == colSeparator ) {
            strs.Add(str.RemoveLast());
        } else {
            strs.Add(str);
        }
    }
    Load(strs, colSeparator);   
}

void ClipboardMatrix::GetDataFromClipboard()
{
    if ( wxTheClipboard->Open() ) {
        if ( wxTheClipboard->IsSupported(wxDF_TEXT) ) {
            wxTheClipboard->GetData(m_textData);
            wxTheClipboard->Close();
        } else {
            wxTheClipboard->Close();
            throw wxString(GRID_PASTE_WRONG_CONTENT);
        }  
    } else {
        throw wxString(GRID_WRONG_CLIPBOARD);
    }
}
