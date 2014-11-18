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

InternalComposer::InternalComposer(size_t bufferSize,
                                   Header* header,
                                   Detail* detail,
                                   bool verifyOnly)
    : DefaultComposer(bufferSize,
                      header->GetDescriptor(),
                      detail->GetDescriptor(),
                      NULL, 
                      verifyOnly), m_header(header), m_detail(detail)
{
};

bool InternalComposer::ComposeHeader(Descriptor* descriptor, Matrix* matrix)
{
    bool result = true;
    BufferClean();
    for (int j=0; j<GetHeader()->GetSize(); j++) {
        bool dataCheck = CheckAndWriteData(
            GetHeader()->GetDescriptor()->GetElementDescriptor(j), 
            GetHeader()->GetField(j)->GetValue());
        if ( !dataCheck ) {
            LOG_MESSAGE(INFO_MESSAGE_LEVEL, wxString::Format(" Row is [%d].\n", 1));
        }
        if ( descriptor->GetElementDescriptor(j)->GetName().Cmp(CARRIER_NAME) == 0 ) {
            SetCarrier(GetHeader()->GetField(j)->GetValue());
            GetTrailer().Carrier = GetCarrier()->c_str();
        }
        result = result && dataCheck;
    }
    BufferFlush();
    return result;
};

bool InternalComposer::ComposeDetail(Descriptor* descriptor, Matrix* matrix)
{
    bool result = true;
    GetTrailer().TotalRecords = GetDetail()->GetNumberRows();
    size_t numberRows = GetDetail()->GetNumberRows();
    for (size_t i=0; i<numberRows; i++) {
        BufferClean();
        wxArrayString row;
        // prepare row
        for (size_t j=0, k=0; j<descriptor->GetSize(); j++) {
            XmlElementDescriptor* colDesc = (XmlElementDescriptor*)descriptor->GetElementDescriptor(j);
            if ( colDesc->IsEnabled() ) {
                row.Add(GetDetail()->GetValue(i, k++));
            } else {
                row.Add(colDesc->GetDefaultValue());
            }
        }
        // transformation for not enabled dependence
        for (size_t l=0; l<descriptor->GetSize(); l++) {
            XmlElementDescriptor* colDesc = (XmlElementDescriptor*)descriptor->GetElementDescriptor(l);
            ElementDependence* dep = colDesc->GetDependence();
            if ( NULL != dep ) {
                size_t index = dep->GetFieldIndex();
                if ( index >= 0 
                     && index < descriptor->GetSize()
                     && !descriptor->GetElementDescriptor(index)->IsEnabled() ) {
                    row[index] = colDesc->DependenceValue(row[l]);
                }
            }
        }
        // writing row to file
        wxString rowStr;
        for (size_t m=0; m<descriptor->GetSize(); m++) {
            ElementDescriptor* ed = GetDetail()->GetDescriptor()->GetElementDescriptor(m);
            bool dataCheck = CheckAndWriteData(ed, row[m]);
            if ( !dataCheck ) {
                LOG_MESSAGE(INFO_MESSAGE_LEVEL, wxString::Format(" Row is [%d].\n", i));
            }
            result = result && dataCheck;
        }
        UpdateTrailer(row[0]);
        BufferFlush();
    }
    return result;
};

