#include <rxclaim/Composer.h>
#include <rxclaim/Messages.h>
#include <wx/datetime.h>

#define TEXT_TYPE       "text"
#define CHOICE_TYPE     "choice"
#define DATE_TYPE       "date"
#define LONG_DATE_TYPE  "longdate"
#define NUMERIC_TYPE    "numeric"
#define CARRIER_TYPE    "carrier"
#define CARRIER_NAME    "Carrier"

// Record types
#define ADDED           "2"
#define CHANGED         "3"
#define MOVED           "4"
#define REPLACED        "5"

DefaultComposer::DefaultComposer(size_t bufferSize,
                                 Descriptor* headerDescriptor,
                                 Descriptor* detailDescriptor,
                                 Matrix* dataMatrix,
                                 bool verifyOnly)
    : m_bufferSize(bufferSize),
      m_headerDescriptor(headerDescriptor),
      m_detailDescriptor(detailDescriptor),
      m_dataMatrix(dataMatrix),
      m_verify(verifyOnly)
{
    m_GlobalCarrier = new wxString("");
};


bool DefaultComposer::compose(wxString& outFilename)
{
    if ( !IsVerify() ) {
        m_outputFile = new wxFile(outFilename, wxFile::write);
        if ( !m_outputFile ) {
            LOG_MESSAGE(ERROR_MESSAGE_LEVEL, COMPOSE_ERROR_OPEN_EDI_FILE + outFilename + "]");
            return false;
        }
    }
    BufferAlloc();
    ResetTrailer();
    LOG_MESSAGE(INFO_MESSAGE_LEVEL, COMPOSE_CHECKING_HEADER);
    bool hr = ComposeHeader(GetHeaderDescriptor(), GetDataMatrix());
    LOG_MESSAGE(INFO_MESSAGE_LEVEL, COMPOSE_CHECKING_DETAIL);
    bool dr = ComposeDetail(GetDetailDescriptor(), GetDataMatrix());
    LOG_MESSAGE(INFO_MESSAGE_LEVEL, COMPOSE_ADDING_TRAILER);
    ComposeTrailer();
    BufferFree();
    if ( !IsVerify() ) {
        m_outputFile->Close();
    }
    return hr & dr;
};
    
// private
bool DefaultComposer::ComposeHeader(Descriptor* descriptor, Matrix* matrix)
{
    Vector& matrixHeader = matrix->GetVector(0);
    if ( matrixHeader.GetSize() != descriptor->GetSize() ) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, COMPOSE_WRONG_HEADER);
        return false;
    }
    
    Vector& row = matrix->GetVector(1);
    if ( row.GetSize() != descriptor->GetSize() ) {
        LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, COMPOSE_HEADER_SKIP);
        return false;
    }

    bool result = true;

    BufferClean();
    for (size_t j=0; j<descriptor->GetSize(); j++) {
        bool dataCheck = CheckAndWriteData(descriptor->GetElementDescriptor(j), row.GetDataAt(j));
        if ( !dataCheck ) {
            LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, wxString::Format("Row is [%d].\n", 1));
        } else {
            if ( descriptor->GetElementDescriptor(j)->GetName().Cmp(CARRIER_NAME) == 0 ) {
                SetCarrier(row.GetDataAt(j));
                GetTrailer().Carrier = GetCarrier()->c_str();
            }
        }
        result = result && dataCheck;
    }
    BufferFlush();
    return result;
};

bool DefaultComposer::ComposeDetail(Descriptor* descriptor, Matrix* matrix)
{
    Vector& matrixDetail = matrix->GetVector(2);
    if ( matrixDetail.GetSize() != descriptor->GetSize() ) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, COMPOSE_WRONG_DETAIL);
        return false;
    }
    bool result = true;
    GetTrailer().TotalRecords = matrix->GetSize() - 3;
    bool fillTrailer = true;
    for (size_t i=3; i<matrix->GetSize(); i++) {
        Vector& row = matrix->GetVector(i);
        if ( row.GetSize() != descriptor->GetSize() ) {
            LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, wxString::Format("Corrupt detail row [%d] was skipped.\n", i));
            break;
        }
        if ( fillTrailer )  {
            UpdateTrailer(row.GetDataAt(0));
        }
        BufferClean();
        for (size_t j=0; j<descriptor->GetSize(); j++) {
            bool dataCheck = CheckAndWriteData(descriptor->GetElementDescriptor(j), row.GetDataAt(j));
            if ( !dataCheck ) {
                LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, wxString::Format("Corrupt detail Row is [%d].\n", i));
            }
            result = result && dataCheck;
                
        }
        BufferFlush();
    }
    return result;
};

bool DefaultComposer::ComposeTrailer()
{
    BufferClean();  

    BufferWriteRawData(&(GetTrailer().RecordType), 1);
    BufferWriteRawData(&(GetTrailer().Carrier), 9);
    wxString total;
    total << GetTrailer().TotalRecords;
    BufferWriteRawData(PrepareString(total, 9), 9);
    wxString added;
    added << GetTrailer().TotalAdds;
    BufferWriteRawData(PrepareString(added, 9), 9);
    wxString changed;
    changed << GetTrailer().TotalChanges;
    BufferWriteRawData(PrepareString(changed, 9), 9);
    wxString moved;
    moved << GetTrailer().TotalMoveHistory;
    BufferWriteRawData(PrepareString(moved, 9), 9);
    wxString rolled;
    BufferWriteRawData(PrepareString(rolled, 9), 9);
    wxString replaced;
    replaced << GetTrailer().TotalReplacements;
    BufferWriteRawData(PrepareString(replaced, 9), 9);

    BufferFlush();
    return true;
};

bool DefaultComposer::CheckAndWriteData(ElementDescriptor* fieldDescriptor,
                                 wxString& data)
{
    wxString* rawData = ConvertDataForType(data, 
        fieldDescriptor->GetType());
    if ( !rawData ) {
        LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, "Incorrect value for column [" + 
            fieldDescriptor->GetName() + "].");
        return false;
    } else if ( fieldDescriptor->IsRequired()
         && rawData->IsEmpty() ) {
        LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, "Required value for column [" + 
            fieldDescriptor->GetName() + "].");
        delete rawData;
        return false;
    } else if ( rawData->Length() > fieldDescriptor->GetMaxSize() ) {
        LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, "Value to long for column [" + 
            fieldDescriptor->GetName() + "].");
        delete rawData;
        return false;
    } else if ( (rawData->Length() < fieldDescriptor->GetMinSize()) && (!rawData->IsEmpty()) && (!fieldDescriptor->IsRequired())) {
        LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, "Value to short for column [" + 
            fieldDescriptor->GetName() + "].");
        delete rawData;
        return false;
    } else if ( fieldDescriptor->GetType().Cmp(NUMERIC_TYPE) == 0 ) {
        BufferWriteRawData(new wxString(rawData->Pad(fieldDescriptor->GetMaxSize() - rawData->Len(), '0', FALSE)),
            fieldDescriptor->GetMaxSize(), fieldDescriptor->GetAlign());
        delete rawData;
        return true;
    } else if ( fieldDescriptor->GetType().Cmp(CHOICE_TYPE) == 0 ) {
        if ( fieldDescriptor->IsValid(*rawData) ) {
            BufferWriteRawData(rawData, 
                fieldDescriptor->GetMaxSize(), fieldDescriptor->GetAlign());
            delete rawData;
            return true;
        } else {
            LOG_MESSAGE(VERIFY_MESSAGE_LEVEL, "Illegal choice for column [" + 
                fieldDescriptor->GetName() + "].");
            delete rawData;
            return false;
        }
    } else {
        BufferWriteRawData(rawData, 
            fieldDescriptor->GetMaxSize(), fieldDescriptor->GetAlign());
        delete rawData;
        return true;
    }
    
};

wxString* DefaultComposer::ConvertDataForType(wxString& data, wxString& type)
{
    if ( type.Cmp(TEXT_TYPE) == 0 ) {
        return new wxString(data);    
    } else if (type.Cmp(CARRIER_TYPE) == 0) {
        return new wxString(GetCarrier()->c_str()); 
    } else if (type.Cmp(CHOICE_TYPE) == 0) {
        return new wxString(data);
    } else if (type.Cmp(NUMERIC_TYPE) == 0) {
        if ( ! data.IsNumber() ) {
            return NULL;
        } else {
            return new wxString(data);
        }
    } else if (type.Cmp(DATE_TYPE) == 0) {
        if ( data.Cmp("99/99/9999") == 0 ) {
            return new wxString("9999999");
        }
        if ( data.Cmp("00/00/0000") == 0 ) {
            return new wxString("0000000");
        }
        if ( data == "" ) {
            return new wxString("");
        }
        wxDateTime date;
        if ( date.ParseFormat(data, "%m/%d/%Y") ) {
            if ( date.GetYear() < 1000 ) {
                return NULL;
            }
            wxString *str = new wxString("");
            int c = date.GetYear()/100 - 19;
            *str << ( c > 9 ? ( c - ( ( c/10 ) * 10) ) : c);
            *str << date.Format("%y%m%d"); 
            return str; 
        } else {
            return NULL;
        }
        return new wxString(data);
    } else if (type.Cmp(LONG_DATE_TYPE) == 0) {
        if ( data.Cmp("99/99/9999") == 0 ) {
            return new wxString("99999999");
        }
        if ( data.Cmp("00/00/0000") == 0 ) {
            return new wxString("0000000");
        }
        if ( data == "" ) {
            return new wxString("");
        }
        wxDateTime date;
        if ( date.ParseFormat(data, "%m/%d/%Y") ) {
            if ( date.GetYear() < 1000 ) {
                return NULL;
            } else {
                return new wxString(date.Format("%Y%m%d")); 
            }
        } else {
            return NULL;
        }
        return new wxString(data);
    } else {
        return NULL;
    }
};

wxString* DefaultComposer::PrepareString(wxString& data, int len)
{
    wxString* result = new wxString();
    *result << wxString(data.Pad(len - data.Len(), '0', FALSE)).c_str();
    return result;
};

void DefaultComposer::BufferAlloc()
{
    m_buffer = malloc(m_bufferSize);
};

void DefaultComposer::BufferFree()
{
    free(GetBuffer());
};

void* DefaultComposer::GetBuffer()
{
    return m_buffer;
};

void DefaultComposer::BufferClean()
{
    memset(GetBuffer(), FILLER_CHARACTER, m_bufferSize);
    m_bufferOffset = 0;
};


void DefaultComposer::BufferWriteRawData(wxString* rawData, size_t size, bool align)
{
    int realSize = rawData->Length();
#if 0
    LOG_MESSAGE("DEBUG: RawData=%s, RealSize=%d, Offset=%d\n", 
        rawData->c_str(), realSize, m_bufferOffset);
#endif
    if ( m_bufferOffset + size < m_bufferSize ) {
        int seek = (align == LEFT_ALIGN) ? 0 : size - realSize;
        memcpy(((unsigned char*)GetBuffer() + m_bufferOffset + seek), 
               (void*)rawData->c_str(), 
               realSize);
        m_bufferOffset += size;
    } else {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, "Buffer overflow!!!");
        //exit(1);
    }
};

void DefaultComposer::BufferFlush()
{
    if ( !IsVerify() ) {
        GetOutputFile()->Write(GetBuffer(), m_bufferSize);
        GetOutputFile()->Write((void*)"\r\n", 2);
    }
};

void DefaultComposer::BufferSkip(int count)
{
    m_bufferOffset += count;
};

void DefaultComposer::UpdateTrailer(wxString& recordType)
{
    if ( recordType.Cmp(ADDED) == 0 ) {
        GetTrailer().TotalAdds++;
    } else if( recordType.Cmp(CHANGED) == 0 ) {
        GetTrailer().TotalChanges++;
    } else if( recordType.Cmp(MOVED) == 0 ) {
        GetTrailer().TotalMoveHistory++;
    } else if( recordType.Cmp(REPLACED) == 0 ) {
        GetTrailer().TotalReplacements++;
    }
};
