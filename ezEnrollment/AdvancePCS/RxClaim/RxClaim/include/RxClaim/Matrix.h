#ifndef __RXCLAIM_MATRIX_H__
#define __RXCLAIM_MATRIX_H__

#include <wx/wx.h>
#define DEFAULT_COLUMN_SEPARATOR "\t"

class Vector {
    public:
    virtual size_t GetSize() = 0;
    virtual wxString& GetDataAt(size_t index) = 0;
};

class Matrix {
    public:
    virtual size_t GetSize() = 0;
    virtual Vector& GetVector(size_t index) = 0;
};

class SimpleVector : public Vector {
    private:
        wxArrayString* m_storage;

    public:
    SimpleVector(wxString& str, wxString& colSeparator=wxString(DEFAULT_COLUMN_SEPARATOR));
    ~SimpleVector();
    virtual size_t GetSize();
    virtual wxString& GetDataAt(size_t index);

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
    
    virtual size_t GetSize();
    virtual Vector& GetVector(size_t index);

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

#endif /* __RXCLAIM_MATRIX_H__ */