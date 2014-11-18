/*
 *  $RCSfile: EdiStatus.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_EDI_STATUS_H__
#define __ADVPCS_EDI_STATUS_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/datetime.h>
/* -------------------------- implementation place -------------------------- */


class EdiStatus {
public:
    EdiStatus(const wxString& userid, const wxString& fileName,
              wxString fileSize, const wxString& date,
              const wxString& tracking, wxString recordCount,
              const wxString& status)
       : m_userid(userid),m_fileName(fileName),m_fileSize(fileSize),
         m_date(date),m_tracking(tracking),m_recordCount(recordCount),
         m_status(status)
    {};

    wxString GetUserid() const { return m_userid; }; 
    wxString GetFileName() const { return m_fileName; };
    wxString GetFileSize() const { return m_fileSize; };
    wxString GetDate() const { return m_date; };
    wxString GetTracking() const { return m_tracking; };
    wxString GetRecordCount() const { return m_recordCount; };
    wxString GetStatus() const { return m_status; };

private:
    wxString   m_userid;
    wxString   m_fileName;
    wxString   m_fileSize;
    wxString   m_date;
    wxString   m_tracking;
    wxString   m_recordCount;
    wxString   m_status;

};

#endif /* __ADVPCS_EDI_STATUS_H__ */