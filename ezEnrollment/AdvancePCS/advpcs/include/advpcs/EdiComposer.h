/*
 *  $RCSfile: EdiComposer.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_EDI_COMPOSER_H__
#define __ADVPCS_EDI_COMPOSER_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/Composer.h>
/* -------------------------- implementation place -------------------------- */

struct Trailer {
    wxString RecordType;
    wxString Carrier;
    int      TotalRecords;
    int      TotalAdds;
    int      TotalChanges;
    int      TotalMoveHistory;
    int      TotalRollAccums;
    int      TotalReplacements;
    void Reset() {
        RecordType = "9";
        TotalRecords = 0;
        TotalAdds = 0;
        TotalChanges = 0;
        TotalMoveHistory = 0;
        TotalRollAccums = 0;
        TotalReplacements = 0;
    };
};

class EdiComposer : public Composer {
public:
    EdiComposer(ProcessIndicator& indicator);
    virtual bool Write(const Document& doc, const wxString& filename);
    virtual void Read(Document& doc);


protected:    
    bool Compose(const Document& doc, const wxString& outFilename);
    
    virtual bool ComposeHeader(const Document& doc);
    virtual bool ComposeDetail(const Document& doc);
    virtual bool ComposeTrailer();

    bool WriteData(const Field& field);
    bool WriteData(const FieldDescriptor& desc, const wxString& value);
    wxString ConvertDataForType(const wxString& data, const wxString& type);
    void BufferAlloc();
    void BufferFree();
    void* GetBuffer();
    void BufferClean();
    void BufferWriteRawData(const wxString& rawData, size_t size, bool align=LEFT_ALIGN);
    void BufferSkip(int count);
    void BufferFlush();
    void UpdateTrailer(const wxString& recordType);

    wxFile& GetOutputFile() { return *m_outputFile; };
    wxString GetCarrier() { return m_GlobalCarrier; };

    void SetCarrier(wxString& carrier) { m_GlobalCarrier = carrier; };
    wxString PrepareString(const wxString& data, int len);

    Trailer& GetTrailer() { return m_trailer; };

protected:    

    size_t m_bufferSize;

    void*    m_buffer;
    int      m_bufferOffset;
    wxFile*  m_outputFile;
    Trailer  m_trailer;
    wxString m_GlobalCarrier;
};

#endif /* __ADVPCS_EDI_COMPOSER_H__ */

