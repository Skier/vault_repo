#ifndef ___MATRIX___
#define ___MATRIX___

#include <wx/wx.h>
#define DEFAULT_COLUMN_SEPARATOR "\t"
#define COMMA_COLUMN_SEPARATOR ","

class Vector {
    public:
    virtual long GetSize() = 0;
    virtual wxString& GetDataAt(long index) = 0;
};

class Matrix {
    public:
    virtual long GetSize() = 0;
    virtual Vector& GetVector(long index) = 0;
};

class SimpleVector : public Vector {
    private:
        wxArrayString* m_storage;

    public:
    SimpleVector(wxString& str, wxString& colSeparator=wxString(DEFAULT_COLUMN_SEPARATOR));
    ~SimpleVector();
    virtual long GetSize();
    virtual wxString& GetDataAt(long index);

    private:
    wxArrayString* GetStorage() 
    {
        return m_storage;
    }
};

WX_DEFINE_ARRAY(Vector*, VectorArray);

class StringMatrix : public Matrix {
    private:
        VectorArray* m_vectors;

    public:
    StringMatrix();
    ~StringMatrix();
    Load(wxArrayString& strs, wxString& colSeparator=wxString(DEFAULT_COLUMN_SEPARATOR));
    
    virtual long GetSize();
    virtual Vector& GetVector(long index);

    private:
    VectorArray* GetVectors()
    {
        return m_vectors;
    }

    Vector* IGetVector(long index)
    {
        return (Vector*)GetVectors()->Item(index);
    }
};

#endif // ___MATRIX___