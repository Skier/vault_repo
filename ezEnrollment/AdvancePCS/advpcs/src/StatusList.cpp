/*
 *  $RCSfile: StatusList.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

#pragma warning (disable : 4786)

/* -------------------------- header place ---------------------------------- */
#include <advpcs/StatusList.h>
#include <advpcs/Resources.h>
#include <advpcs/EdiStatus.h>
/* -------------------------- implementation place -------------------------- */

#define USERID_COL        0
#define FILENAME_COL      1
#define FILESIZE_COL      2
#define DATETIME_COL      3
#define TRACKING_COL      4
#define RECORDCOUNT_COL   5
#define STATUS_COL        6

BEGIN_EVENT_TABLE(StatusList, wxListCtrl)
  EVT_SIZE    (StatusList::OnSize)
END_EVENT_TABLE()


StatusList::StatusList(wxWindow* parent, wxWindowID id, const wxPoint& pos,
                      const wxSize& size, long style, const wxValidator& validator,
                      const wxString& name)
  : wxListCtrl(parent, id, pos, size, style, validator, name), m_data(), m_idx(0),
    m_orderCol(-1), m_backOrder(false)
{

    InsertColumn(USERID_COL,      ADVPCS_STATUS_COL_USERID_TITLE, wxLIST_FORMAT_LEFT, 100);
    InsertColumn(FILENAME_COL,    ADVPCS_STATUS_COL_FILENAME_TITLE, wxLIST_FORMAT_LEFT, 150);
    InsertColumn(FILESIZE_COL,    ADVPCS_STATUS_COL_FILESIZE_TITLE, wxLIST_FORMAT_LEFT, 130);
    InsertColumn(DATETIME_COL,    ADVPCS_STATUS_COL_DATETIME_TITLE, wxLIST_FORMAT_LEFT, 190);
    InsertColumn(TRACKING_COL,    ADVPCS_STATUS_COL_TRACKINGREF_TITLE, wxLIST_FORMAT_LEFT, 100);
    InsertColumn(RECORDCOUNT_COL, ADVPCS_STATUS_COL_RECORDCOUNT_TITLE, wxLIST_FORMAT_LEFT, 110);
    InsertColumn(STATUS_COL,      ADVPCS_STATUS_COL_STATUS_TITLE, wxLIST_FORMAT_LEFT, -1);

    Connect(-1, wxEVT_COMMAND_LIST_COL_CLICK, (wxObjectEventFunction) StatusList::OnColClick);
    Connect(-1, wxEVT_COMMAND_LIST_DELETE_ITEM, (wxObjectEventFunction) StatusList::OnDeleteItem);
    Connect(-1, wxEVT_COMMAND_LIST_DELETE_ALL_ITEMS, (wxObjectEventFunction) StatusList::OnDeleteAllItems);

};

void StatusList::OnSize() {
	int min_size = 50;
	int fix_size = GetColumnWidth(0) + 
					GetColumnWidth(1) +
					GetColumnWidth(2) +
					GetColumnWidth(3) +
					GetColumnWidth(4) +
					GetColumnWidth(5) + 6;
	SetColumnWidth( 6, GetSize().GetWidth()-fix_size < min_size ? min_size : GetSize().GetWidth()-fix_size );
};

void StatusList::AddStatus(const EdiStatus& status) {

    long idx = GetItemCount();
    
	m_data[++m_idx] = StringVector();

    long item = InsertItem(idx, status.GetUserid());
	SetItemData(item, m_idx);
    m_data[m_idx].push_back(status.GetUserid());

    item = SetItem(idx, FILENAME_COL, status.GetFileName());
    m_data[m_idx].push_back(status.GetFileName());

    item = SetItem(idx, FILESIZE_COL, status.GetFileSize());
    m_data[m_idx].push_back(status.GetFileSize());

    item = SetItem(idx, DATETIME_COL, status.GetDate());
    m_data[m_idx].push_back(status.GetDate());

    item = SetItem(idx, TRACKING_COL, status.GetTracking());
    m_data[m_idx].push_back(status.GetTracking());

    item = SetItem(idx, RECORDCOUNT_COL, status.GetRecordCount());
    m_data[m_idx].push_back(status.GetRecordCount());

    item = SetItem(idx, STATUS_COL, status.GetStatus());
    m_data[m_idx].push_back(status.GetStatus());

}; 


enum CompareMode { AS_STRING, AS_DATETIME, AS_INTEGER };

static int CompareStrings(const wxString& str1, const wxString& str2, CompareMode mode) {
    if ( AS_DATETIME == mode) {
        wxDateTime d1;
        wxDateTime d2;

        if ( NULL != d1.ParseFormat(str1, "%m/%d/%Y %I:%M:%S %p") 
          && NULL != d2.ParseFormat(str2, "%m/%d/%Y %I:%M:%S %p") )
        {
            if ( d1.IsLaterThan(d2) ) {
                return 1;
            } else if (d2.IsLaterThan(d1) ) {
                return -1;
            } else {
                return 0;
            }
        }
    }
            
    if ( AS_INTEGER == mode ) {
        if ( str1.IsNumber() && str2.IsNumber() ) {
            long l1;
            long l2;
            str1.ToLong(&l1);
            str2.ToLong(&l2);
            return l1 - l2;
        }
    }
    return str1.Cmp(str2);
};

int wxCALLBACK StatusComparator(long item1, long item2, long sortData) {
	StatusList* self = (StatusList*) sortData;
    CompareMode mode = AS_STRING;

    if ( FILESIZE_COL == self->m_orderCol ) {
        mode = AS_INTEGER;
    } else if ( DATETIME_COL == self->m_orderCol ) {
        mode = AS_DATETIME;
    }

	if ( self->m_backOrder ) {
        return CompareStrings(self->m_data[item1][self->m_orderCol],
                              self->m_data[item2][self->m_orderCol], 
                              mode);
	} else {
        return CompareStrings(self->m_data[item2][self->m_orderCol],
                              self->m_data[item1][self->m_orderCol], 
                              mode);
	}
}; 


void StatusList::OnColClick(wxListEvent& event) {
	m_backOrder = (m_orderCol == event.GetColumn()) ? !m_backOrder : false;
	m_orderCol = event.GetColumn();
    Reorder();
};

void StatusList::Reorder() {
    SortItems(StatusComparator, (long) this);
};

void StatusList::OnDeleteItem(wxListEvent& event) {
	m_data.erase(m_data.find(event.GetItem()));
}


void StatusList::OnDeleteAllItems(wxListEvent& event) {
	m_data.clear();
}
