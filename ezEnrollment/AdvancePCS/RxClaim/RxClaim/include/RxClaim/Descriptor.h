#ifndef __RXCLAIM_DESCRIPTOR_H__
#define __RXCLAIM_DESCRIPTOR_H__

#include <wx/wx.h>

#define RIGHT_ALIGN true
#define LEFT_ALIGN false

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

    virtual bool GetAlign() = 0;

    virtual size_t GetMaxSize() = 0;
    virtual size_t GetMinSize() = 0;

    virtual bool IsRequired() = 0;
    virtual bool IsEnabled() = 0;
    virtual bool IsValid( wxString& value ) = 0;

    virtual wxString& GetDefaultValue() = 0;

    virtual wxString& GetSeparator() = 0;

    virtual wxArrayString& GetChoices() = 0;

    virtual wxString CutValue(wxString& candidate) = 0;

    // may be NULL if not dependence
    virtual ElementDependence* GetDependence() = 0;
};

class Descriptor {
public:
    virtual size_t GetSize() = 0;

    virtual ElementDescriptor* GetElementDescriptor(size_t index) = 0;
};

#endif // __RXCLAIM_DESCRIPTOR_H__
