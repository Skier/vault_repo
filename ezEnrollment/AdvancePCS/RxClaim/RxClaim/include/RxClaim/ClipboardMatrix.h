#ifndef __RXCLAIM_CLIPBOARD_MATRIX_H__
#define __RXCLAIM_CLIPBOARD_MATRIX_H__

#include <rxclaim/Matrix.h>
#include <wx/clipbrd.h>

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

#endif /* __RXCLAIM_CLIPBOARD_MATRIX_H__ */

