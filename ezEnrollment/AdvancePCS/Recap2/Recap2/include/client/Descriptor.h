#ifndef ___DESCRIPTOR___
#define ___DESCRIPTOR___

#include <wx/wx.h>

class ElementDependence {
public:
    virtual size_t GetFieldIndex() = 0;

    virtual size_t GetBeginPosition() = 0;

    virtual size_t GetEndPosition() = 0;
};

class ElementDescriptor {
public:
    virtual wxString& GetName() = 0;

    virtual wxString& GetShortName() = 0;

    virtual wxString& GetType() = 0;

    virtual long GetMaxSize() = 0;

    virtual long GetMinSize() = 0;

    virtual bool IsRequired() = 0;

    virtual wxString& GetDefaultValue() = 0;

    virtual wxString& GetSeparator() = 0;

    virtual wxArrayString& GetChoices() = 0;

    // may be NULL if not dependence
    virtual ElementDependence* GetDependence() = 0;

    // Validation & preparing methods
    virtual bool IsValid(wxString& value) = 0;

    virtual wxString CutValue(wxString& candidate) = 0;

};

class Descriptor {
public:
    virtual long GetSize() = 0;

    virtual ElementDescriptor* GetElementDescriptor(long index) = 0;

    virtual long GetElementDescriptorIndex(wxString& name) = 0;
};

#endif // ___DESCRIPTOR___