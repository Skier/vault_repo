#include <client/FileMatrix.h>
#include <client/Messages.h>
#include <wx/tokenzr.h>
#include <wx/textfile.h>
#include <wx/progdlg.h>

FileMatrix::FileMatrix(wxString& filename, int number, wxString& lineSeparator, wxString& colSeparator)
{
    wxString str;
    wxArrayString strs;
    wxTextFile file(filename);
    if ( !file.Exists() ) {
        throw wxString(GRID_OPEN_ERROR_NOT_EXIST + filename);
    }
    if ( !file.Open() ) {
        throw wxString(GRID_OPEN_ERROR_CANT_OPEN + filename);
    }
    for (unsigned int i=0; (i<file.GetLineCount()) && ((i < number) || (number == -1)); i++) {
        str = file.GetLine(i);
        if (str.Last()=='\t') {
        	strs.Add(str.RemoveLast());
        } else {
        	strs.Add(str);
        }
    }
    file.Close();
    Load(strs, colSeparator);
}

