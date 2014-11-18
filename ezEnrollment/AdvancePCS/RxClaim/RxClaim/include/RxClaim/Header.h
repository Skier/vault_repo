#ifndef __RXCLAIM_HEADER_H__
#define __RXCLAIM_HEADER_H__

#include <wx/datetime.h>
#include <rxclaim/Descriptor.h>

class HeaderField {
public:
    virtual ElementDescriptor* GetDescriptor() = 0;
    virtual wxString& GetValue() = 0;
    virtual void SetValue(wxString& value) = 0;
};

class Header {
public:
    virtual Descriptor* GetDescriptor() = 0;
    virtual bool IsValid(wxString& reason = wxString()) = 0;
    virtual long GetSize() = 0;
    virtual HeaderField* GetField(long index) = 0;

    virtual void Clear() = 0;
    virtual void Load(wxString& finename) = 0;
    virtual void Store(wxString& finename) = 0;
};

//
// Defaults
//
class DefaultHeaderField : public HeaderField {
public:
    DefaultHeaderField(ElementDescriptor* elementDescriptor);
    
    ElementDescriptor* GetDescriptor();
    wxString& GetValue();
    void SetValue(wxString& value);
    
private:
    wxString m_value;    
    ElementDescriptor* m_elementDescriptor;
    
};

class DefaultHeader : public Header {
public:
    DefaultHeader(Descriptor* descriptor);
    
    Descriptor* GetDescriptor();
    bool IsValid(wxString& reason = wxString());
    long GetSize();
    HeaderField* GetField(long index);

    void Clear();
    
    void Load(wxString& finename);
    void Store(wxString& finename);
private:    
    wxList* m_fields;
    Descriptor* m_descriptor;
    
    wxList* GetFields()
    {
        return m_fields;
    };
};

#endif /* __RXCLAIM_HEADER_H__ */