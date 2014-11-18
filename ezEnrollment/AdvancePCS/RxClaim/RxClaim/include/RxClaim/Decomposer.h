#ifndef __RXCLAIM_DECOMPOSER_H__
#define __RXCLAIM_DECOMPOSER_H__

#include <wx/textfile.h>
#include <rxclaim/Descriptor.h>

class Decomposer {
public:
    Decomposer(Descriptor* headerDesciptor, Descriptor* detailDescriptor);
    virtual bool decompose(wxString& inFile, wxString& outFile);
    virtual bool decompose_row(Descriptor* descriptor, wxString& data, bool withTitle=false);

protected:
    wxTextFile* m_in;
    wxTextFile* m_out;
    Descriptor* m_headerDescriptor;
    Descriptor* m_detailDescriptor;

    size_t calculate_size(ElementDescriptor* ed);
    wxString decompose_value(ElementDescriptor* ed, wxString& ev);
};

#endif /* __RXCLAIM_DECOMPOSER_H__ */