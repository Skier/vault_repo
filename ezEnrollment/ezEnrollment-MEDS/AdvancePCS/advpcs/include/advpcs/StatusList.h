/*
 *  $RCSfile: StatusList.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_STATUS_LIST_H__
#define __ADVPCS_STATUS_LIST_H__

#pragma warning (disable : 4786)
/* -------------------------- header place ---------------------------------- */
#include <map>
#include <vector>
#include <wx/listctrl.h>
#include <atf/XmlNode.h>
/* -------------------------- implementation place -------------------------- */

class EdiStatus;


typedef std::vector<wxString> StringVector;

class StatusList : public wxListCtrl {
	friend int wxCALLBACK StatusComparator(long item1, long item2, long sortData);
public: 
    StatusList(wxWindow* parent, wxWindowID id,
               const wxPoint& pos = wxDefaultPosition,
               const wxSize& size = wxDefaultSize,
               long style = wxLC_REPORT | wxLC_SINGLE_SEL,
               const wxValidator& validator = wxDefaultValidator,
               const wxString& name = "StatusList");

    void AddStatus(const EdiStatus& status); 

    void OnColClick(wxListEvent& event);
    void OnDeleteItem(wxListEvent& event);
    void OnDeleteAllItems(wxListEvent& event);

	
	void Reorder();
private:
    DECLARE_EVENT_TABLE()
    long  m_idx;
	std::map<long, StringVector> m_data;
	long  m_orderCol;
	bool  m_backOrder;
	void OnSize();
};


#endif /* __ADVPCS_STATUS_LIST_H__ */
