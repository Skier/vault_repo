#ifndef ___CLIPBOARD_MATRIX___
#define ___CLIPBOARD_MATRIX___

#include "Matrix.h"
#include "wx/clipbrd.h"

#define DEFAULT_LINE_SEPARATOR "\n"

class ClipboardMatrix : public StringMatrix {
    private:
        wxTextDataObject m_textData;

    public:
    ClipboardMatrix(wxString& lineSeparator=wxString(DEFAULT_LINE_SEPARATOR),
                    wxString& colSeparator=wxString(DEFAULT_COLUMN_SEPARATOR));

    private:
    void GetDataFromClipboard();
};

#endif // ___CLIPBOARD_MATRIX___
