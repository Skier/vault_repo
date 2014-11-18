#ifndef __RXCLAIM_COMPOSER_H__
#define __RXCLAIM_COMPOSER_H__

#define FILLER_CHARACTER 32

#include <wx/wx.h>
#include <wx/file.h>
#include <rxclaim/Descriptor.h>
#include <rxclaim/Matrix.h>
#include <rxclaim/Header.h>
#include <rxclaim/Detail.h>

class Composer {
public:
    virtual bool compose(wxString& outFilename) = 0;
};

struct Trailer {
    wxString RecordType;
    wxString Carrier;
    int      TotalRecords;
    int      TotalAdds;
    int      TotalChanges;
    int      TotalMoveHistory;
    int      TotalRollAccums;
    int      TotalReplacements;
};

//
// Defaults
//
class DefaultComposer : public Composer {
public:
    DefaultComposer(size_t bufferSize,
                    Descriptor* headerDescriptor,
                    Descriptor* detailDescriptor,
                    Matrix* dataMatrix,
                    bool verifyOnly=false);

    bool compose(wxString& outFilename);
    
protected:    
    virtual bool ComposeHeader(Descriptor* descriptor, Matrix* matrix);
    virtual bool ComposeDetail(Descriptor* descriptor, Matrix* matrix);
    virtual bool ComposeTrailer();

    bool CheckAndWriteData(ElementDescriptor* fieldDesciptor, wxString& data);
    wxString* ConvertDataForType(wxString& data, wxString& type);
    void BufferAlloc();
    void BufferFree();
    void* GetBuffer();
    void BufferClean();
    void BufferWriteRawData(wxString* rawData, size_t size, bool align=LEFT_ALIGN);
    void BufferSkip(int count);
    void BufferFlush();
    void UpdateTrailer(wxString& recordType);

    Trailer& GetTrailer() { return m_trailer; };

    void ResetTrailer() {
        GetTrailer().RecordType = "9";
        GetTrailer().TotalRecords = 0;
        GetTrailer().TotalAdds = 0;
        GetTrailer().TotalChanges = 0;
        GetTrailer().TotalMoveHistory = 0;
        GetTrailer().TotalRollAccums = 0;
        GetTrailer().TotalReplacements = 0;
    };

    Descriptor* GetHeaderDescriptor() { return m_headerDescriptor; };
    Descriptor* GetDetailDescriptor() { return m_detailDescriptor; };

    Matrix* GetDataMatrix() { return m_dataMatrix; };

    bool IsVerify() { return m_verify; };

    wxFile* GetOutputFile() { return m_outputFile; };
    wxString* GetCarrier() { return m_GlobalCarrier; };

    void SetCarrier(wxString& carrier) { *m_GlobalCarrier = carrier; };
    wxString* PrepareString(wxString& data, int len);

protected:    
    size_t m_bufferSize;
    Descriptor* m_headerDescriptor;
    Descriptor* m_detailDescriptor;
    Matrix* m_dataMatrix;
    bool m_verify;
    void* m_buffer;
    int m_bufferOffset;
    wxFile* m_outputFile;
    Trailer m_trailer;
    wxString* m_GlobalCarrier;
};

//
// InternalComposer
//
class InternalComposer : public DefaultComposer {
public:
    InternalComposer(size_t bufferSize,
                     Header* header,
                     Detail* detail,
                     bool verifyOnly=false);

    void SetRowSize(size_t bufferSize) { m_bufferSize = bufferSize; };

protected:    

    bool ComposeHeader(Descriptor* descriptor, Matrix* matrix);
    bool ComposeDetail(Descriptor* descriptor, Matrix* matrix);

    Header* GetHeader() { return m_header; };
    Detail* GetDetail() { return m_detail; };

protected:
    Header* m_header;
    Detail* m_detail;
};

#endif // ___COMPOSER___