/*
 *  $RCSfile: EdiComposer.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/10/13 17:12:08 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <wx/file.h>
#include <wx/datetime.h>
#include <atf/SystemException.h>
#include <atf/Logger.h>
#include <advpcs/Document.h>
#include <advpcs/Descriptor.h>
#include <advpcs/EdiComposer.h>
#include <advpcs/ProcessIndicator.h>
#include <advpcs/App.h>
#include <advpcs/Resources.h>
/* -------------------------- implementation place -------------------------- */
static const wxString TEXT_TYPE(wxT(       "text"     ));
static const wxString REGEXP_TYPE(wxT(     "regexp"   ));
static const wxString CHOICE_TYPE(wxT(     "choice"   ));
static const wxString DATE_TYPE(wxT(       "date"     ));
static const wxString DATE_TYPE_0(wxT(     "date0"    ));
static const wxString DATE_TYPE_9(wxT(     "date9"    ));
static const wxString DATE_TYPE_09(wxT(    "date09"   ));
static const wxString LONG_DATE_TYPE(wxT(  "longdate" ));
static const wxString LONG_DATE_TYPE_0(wxT( "longdate0" ));
static const wxString LONG_DATE_TYPE_9(wxT( "longdate9" ));
static const wxString LONG_DATE_TYPE_09(wxT("longdate09"));
static const wxString NUMERIC_TYPE(wxT(    "numeric"  ));
static const wxString MONEY_TYPE(wxT(      "money"  ));
static const wxString ALPHA_NUMERIC_TYPE(wxT("alphanumeric"));
static const wxString CARRIER_TYPE(wxT(    "carrier"  ));
static const wxString CARRIER_NAME(wxT(    "Carrier"  ));
static const wxString FILE_COUNTER_TYPE(wxT("file_counter" ));
static const wxString ROW_COUNTER_TYPE(wxT( "row_counter"  ));

#define FILLER_CHARACTER 32

// Record types
#define ADDED           "2"
#define CHANGED         "3"
#define MOVED           "4"
#define REPLACED        "5"


EdiComposer::EdiComposer(ProcessIndicator& indicator) 
   : Composer(indicator), m_outputFile(NULL), m_buffer(NULL), m_bufferOffset(0), m_bufferSize(0)
{
    m_bufferSize = wxGetApp().GetEdiRowLength();
}; 

bool EdiComposer::Write(const Document& doc, const wxString& filename) {
//    if ( !doc.IsValid(true) ) {
    if ( !doc.IsValid() ) {
        return false;
    }

    return Compose(doc, filename);
};

void EdiComposer::Read(Document& doc) {
    wxFAIL;
};



bool EdiComposer::Compose(const Document& doc, const wxString& outFilename) {

    m_outputFile = new wxFile(outFilename, wxFile::write);
    if ( !m_outputFile ) {
        LOG_ERROR(wxGetApp().GetLogger(), 0, ADVPCS_COMPOSE_ERROR_OPEN_EDI_FILE + outFilename + "]");
        return false;
    }

    BufferAlloc();
    GetTrailer().Reset();

    LOG_INFO(wxGetApp().GetLogger(), 0, ADVPCS_COMPOSE_BUILD_HEADER);
    bool hr = ComposeHeader(doc);

    LOG_INFO(wxGetApp().GetLogger(), 0, ADVPCS_COMPOSE_BUILD_DETAIL);
    bool dr = ComposeDetail(doc);

    LOG_INFO(wxGetApp().GetLogger(), 0, ADVPCS_COMPOSE_ADDING_TRAILER);
    ComposeTrailer(doc);

    BufferFree();
    LOG_INFO(wxGetApp().GetLogger(), 0, wxString::Format(ADVPCS_COMPOSE_OK, outFilename) );
    return hr & dr;
};
    
// private
bool EdiComposer::ComposeHeader(const Document& doc) {

    bool result = true;

    BufferClean();
    for (size_t j=0; j<doc.GetDescriptor().GetSize(); j++) {

        WriteData(doc.GetField(j));
        if ( 0 == doc.GetField(j).GetDescriptor().GetName().Cmp(CARRIER_NAME) ) {
            SetCarrier(doc.GetField(j).GetValue());
            GetTrailer().Carrier = GetCarrier();
        }
    }
    BufferFlush();
    return result;
};

bool EdiComposer::ComposeDetail(const Document& doc) {
    bool result = true;

    GetTrailer().TotalRecords = doc.GetNumberRows();
    bool fillTrailer = true;

    for (size_t i=0; i<doc.GetNumberRows(); i++) {

        if ( fillTrailer )  {
            UpdateTrailer(doc.GetValue(i,0));
        }
        BufferClean();
        for (size_t j=0; j<doc.GetColCount(); j++) {
            WriteData(doc.GetColumnDescriptor(j), doc.GetValue(i,j));
        }
        BufferFlush();
    }
    return result;
};

bool EdiComposer::ComposeTrailer()
{
    BufferClean();  

    BufferWriteRawData(GetTrailer().RecordType, 1);
    BufferWriteRawData(GetTrailer().Carrier, 9);
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

bool EdiComposer::ComposeTrailer(const Document& doc) {

    bool result = true;

    BufferClean();
    for (size_t k=0; k<doc.GetTrailerDescriptor().GetSize(); k++) {

        WriteData(doc.GetTrailerFieldDescriptor(k), doc.GetTrailerValue(k));

    }
    BufferFlush();
    return result;
};

bool EdiComposer::WriteData(const Field& field) {
        return WriteData(field.GetDescriptor(), field.GetValue());
};

bool EdiComposer::WriteData(const FieldDescriptor& desc, const wxString& value) {

    if ( desc.GetType().Cmp(NUMERIC_TYPE) == 0 
		|| desc.GetType().Cmp(ROW_COUNTER_TYPE) == 0
		|| desc.GetType().Cmp(FILE_COUNTER_TYPE) == 0) {
        wxString data = value;
        BufferWriteRawData(data.Pad(desc.GetLenght() - value.Len(), '0', FALSE),
                           desc.GetLenght(),
                           desc.GetAlign());
    } else if ( desc.GetType().Cmp(MONEY_TYPE) == 0 ) {
        wxString data = value;
        data = data.erase(value.Index('.'), 1);
        BufferWriteRawData(data.Pad(desc.GetLenght() - data.Len(), '0', FALSE),
                           desc.GetLenght(),
                           desc.GetAlign());
    } else if ( desc.GetType().Cmp(CHOICE_TYPE) == 0 ) {
        BufferWriteRawData(desc.PrepareValue(value), desc.GetLenght(), desc.GetAlign());
    } else {
        BufferWriteRawData(ConvertDataForType(value, desc.GetType()), 
                           desc.GetLenght(), desc.GetAlign());
    }
    return true;
};

wxString EdiComposer::ConvertDataForType(const wxString& data, const wxString& type) {
    if ( type.Cmp(TEXT_TYPE) == 0 || type.Cmp(REGEXP_TYPE) == 0 ) {
        return data;    
    } else if (type.Cmp(CARRIER_TYPE) == 0) {
        return GetCarrier(); 
    } else if (type.Cmp(CHOICE_TYPE) == 0) {
        return data;
    } else if (type.Cmp(NUMERIC_TYPE) == 0) {
        return data;
    } else if (type.Cmp(MONEY_TYPE) == 0) {
        return data;
    } else if (type.Cmp(ALPHA_NUMERIC_TYPE) == 0) {
        return data;
    } else if (type.Cmp(DATE_TYPE) == 0 ) {
        if ( data == "" ) {
            return "";
        }
        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }

        wxString str("");
        int c = date.GetYear()/100 - 19;
        str << ( c > 9 ? ( c - ( ( c/10 ) * 10) ) : c);
        str << date.Format("%y%m%d"); 
        return str; 

    } else if (type.Cmp(DATE_TYPE_0) == 0 ) {
        if ( data.Cmp("00/00/0000") == 0 ) {
            return "0000000";
        }
        if ( data == "" ) {
            return "";
        }
        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }

        wxString str("");
        int c = date.GetYear()/100 - 19;
        str << ( c > 9 ? ( c - ( ( c/10 ) * 10) ) : c);
        str << date.Format("%y%m%d"); 
        return str; 

    } else if (type.Cmp(DATE_TYPE_9) == 0 ) {
        if ( data.Cmp("99/99/9999") == 0 ) {
            return "9999999";
        }
 
        if ( data == "" ) {
            return "";
        }

        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }

        wxString str("");
        int c = date.GetYear()/100 - 19;
        str << ( c > 9 ? ( c - ( ( c/10 ) * 10) ) : c);
        str << date.Format("%y%m%d"); 
        return str; 

    } else if (type.Cmp(DATE_TYPE_09) == 0 ) {
        if ( data.Cmp("99/99/9999") == 0 ) {
            return "9999999";
        }
        if ( data.Cmp("00/00/0000") == 0 ) {
            return "0000000";
        }
        if ( data == "" ) {
            return "";
        }
        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }

        wxString str("");
        int c = date.GetYear()/100 - 19;
        str << ( c > 9 ? ( c - ( ( c/10 ) * 10) ) : c);
        str << date.Format("%y%m%d"); 
        return str; 

    } else if (type.Cmp(LONG_DATE_TYPE) == 0 ) {
        if ( data == "" ) {
            return "";
        }
        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }
        return date.Format("%Y%m%d"); 
    
    } else if (type.Cmp(LONG_DATE_TYPE_0) == 0 ) {
        if ( data.Cmp("00/00/0000") == 0 ) {
            return "00000000";
        }
        if ( data == "" ) {
            return "";
        }
        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }
        return date.Format("%Y%m%d"); 
    
    } else if (type.Cmp(LONG_DATE_TYPE_9) == 0 ) {
        if ( data.Cmp("99/99/9999") == 0 ) {
            return "99999999";
        }
        if ( data == "" ) {
            return "";
        }
        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }
        return date.Format("%Y%m%d"); 

    } else if (type.Cmp(LONG_DATE_TYPE_09) == 0 ) {
        if ( data.Cmp("99/99/9999") == 0 ) {
            return "99999999";
        }
        if ( data.Cmp("00/00/0000") == 0 ) {
            return "0000000";
        }
        if ( data == "" ) {
            return "";
        }
        wxDateTime date;
        if ( !date.ParseFormat(data, "%m/%d/%Y") ) {
            wxFAIL;
        }
        return date.Format("%Y%m%d"); 
    } else if (type.Cmp(FILE_COUNTER_TYPE) == 0) {
        return data;
    } else if (type.Cmp(ROW_COUNTER_TYPE) == 0) {
        return data;
    } else {
        THROW_ATF_EXCEPTION(0, wxString::Format("Unknown type '%s'", type));
    }
    return wxEmptyString;
};

wxString EdiComposer::PrepareString(const wxString& data, int len) {
    wxString result = data;
    result.Pad(len - data.Len(), '0', FALSE);
    return result;
};

void EdiComposer::BufferAlloc()
{
    m_buffer = malloc(m_bufferSize);
};

void EdiComposer::BufferFree()
{
    free(GetBuffer());
};

void* EdiComposer::GetBuffer()
{
    return m_buffer;
};

void EdiComposer::BufferClean()
{
    memset(GetBuffer(), FILLER_CHARACTER, m_bufferSize);
    m_bufferOffset = 0;
};


void EdiComposer::BufferWriteRawData(const wxString& rawData, size_t size, bool align)
{
    int realSize = rawData.Length();
    if ( m_bufferOffset + size <= m_bufferSize ) {
        int seek = (align == LEFT_ALIGN) ? 0 : size - realSize;
        memcpy(((unsigned char*)GetBuffer() + m_bufferOffset + seek), 
               (void*)rawData.c_str(), 
               realSize);
        m_bufferOffset += size;
    } else {
        LOG_ERROR(wxGetApp().GetLogger(), 0, "Buffer overflow!!!");
        THROW_ATF_EXCEPTION(0, "Buffer overflow!!!");
    }
};

void EdiComposer::BufferFlush()
{
    GetOutputFile().Write(GetBuffer(), m_bufferSize);
    GetOutputFile().Write((void*)"\r\n", 2);
};

void EdiComposer::BufferSkip(int count)
{
    m_bufferOffset += count;
};

void EdiComposer::UpdateTrailer(const wxString& recordType)
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
