#include <client/composer.h>
#include <client/Messages.h>

DefaultComposer::DefaultComposer(ComposerDescriptor* descriptor,
                                 Header* header,
                                 Detail* detail)
    : m_descriptor(descriptor),
      m_header(header),
      m_detail(detail)
{
}

bool DefaultComposer::compose(wxString& outFilename)
{
    wxFile* file = new wxFile(outFilename, wxFile::write);
    SectionDescriptor* r00 = GetDescriptor()->GetSectionDescriptor(0);
    file->Write(r00->Format(GetHeader(), GetDetail()) + wxT("\n"));
    size_t rows = 0;
    for (int i=0; i<GetDetail()->GetNumberRows(); i++) {
        for (int j=1; j<GetDescriptor()->GetSize()-1; j++, rows++) {
            SectionDescriptor* rxx = GetDescriptor()->GetSectionDescriptor(j);
            file->Write(rxx->Format(GetHeader(), GetDetail(), i) + wxT("\n"));
        }
    }
    SectionDescriptor* r99 = GetDescriptor()->GetSectionDescriptor(GetDescriptor()->GetSize()-1);
    file->Write(r99->Format(GetHeader(), GetDetail(), rows+2) + wxT("\n"));
    file->Close();
    return true;
}
