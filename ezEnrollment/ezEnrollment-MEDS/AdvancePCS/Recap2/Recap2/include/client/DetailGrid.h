#ifndef ___NEW_GRID___
#define ___NEW_GRID___

#include "wx/wx.h"
#include "wx/grid.h"
#include "wx/generic/gridctrl.h"

class DetailGrid : public wxGrid {
public:
    DetailGrid(wxWindow* parent, 
    		wxWindowID id, 
    		const wxPoint& pos = wxDefaultPosition, 
    		const wxSize& size = wxDefaultSize, 
    		long style = wxWANTS_CHARS, 
    		const wxString& name = wxPanelNameStr);
    ~DetailGrid();

    void PasteFromClipboard();
    void CopyToClipboard();
    void CutToClipboard();

private:

};

#endif // ___NEW_GRID___
