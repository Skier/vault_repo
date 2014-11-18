#ifndef __RXCLAIM_FILE_MATRIX_H__
#define __RXCLAIM_FILE_MATRIX_H__

#include <rxclaim/Matrix.h>

#define DEFAULT_LINE_SEPARATOR "\n"

class FileMatrix : public StringMatrix {
    public:
    FileMatrix(wxString& filename, 
               int number = -1,
               wxString& lineSeparator=wxString(DEFAULT_LINE_SEPARATOR),
               wxString& colSeparator=wxString(DEFAULT_COLUMN_SEPARATOR));
};

#endif // __RXCLAIM_FILE_MATRIX_H__
