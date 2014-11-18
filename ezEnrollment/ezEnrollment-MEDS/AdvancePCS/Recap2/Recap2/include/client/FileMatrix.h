#ifndef ___FILE_MATRIX___
#define ___FILE_MATRIX___

#include "Matrix.h"

#define DEFAULT_LINE_SEPARATOR "\n"

class FileMatrix : public StringMatrix {
    public:
    FileMatrix(wxString& filename, 
    		   int number = -1,
               wxString& lineSeparator=wxString(DEFAULT_LINE_SEPARATOR),
               wxString& colSeparator=wxString(DEFAULT_COLUMN_SEPARATOR));
};

#endif // ___FILE_MATRIX___