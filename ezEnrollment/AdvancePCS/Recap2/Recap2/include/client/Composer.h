#ifndef ___COMPOSER___
#define ___COMPOSER___

#include <wx/wx.h>
#include <wx/file.h>
#include <client/ComposerDescriptor.h>
#include <client/header.h>
#include <client/detail.h>

class Composer {
public:
    virtual bool compose(wxString& outFilename) = 0;
};

//
// Defaults
//
class DefaultComposer : public Composer {
public:
    DefaultComposer(ComposerDescriptor* descriptor,
                    Header* header,
                    Detail* detail);

    bool compose(wxString& outFilename);
    
protected:    
    ComposerDescriptor*    m_descriptor;
    Header*                m_header;
    Detail*                m_detail;

//    virtual bool ComposeHeader();
//    virtual bool ComposeDetail();
//    virtual bool ComposeTrailer();

    Header* GetHeader()
    {
        return m_header;
    }

    Detail* GetDetail()
    {
        return m_detail;
    }

    ComposerDescriptor* GetDescriptor()
    {
        return m_descriptor;
    }
};

#endif // ___COMPOSER___