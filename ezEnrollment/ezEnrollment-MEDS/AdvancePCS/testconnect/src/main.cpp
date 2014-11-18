#include "wx/defs.h"

#if wxUSE_GUI
    #error "This sample can't be compiled in GUI mode."
#endif // wxUSE_GUI

#include <stdio.h>

#include "wx/string.h"
#include "wx/file.h"
#include "wx/app.h"

// without this pragma, the stupid compiler precompiles #defines below so that
// changing them doesn't "take place" later!
#ifdef __VISUALC__
    #pragma hdrstop
#endif

#include <memory>
#include <ostream>
#include <atf/Cfg.h>
#include <atf/CfgCmdLine.h>
#include <atf/Exception.h>
#include <atf/StreamLogger.h>
#include <atf/XmlLoadException.h>
#include <advpcs/HttpAgent.h>
#include <advpcs/EdiStatus.h>
#include <advpcs/EdiResponse.h>
#include <advpcs/AgentException.h>
#include <advpcs/ProcessIndicator.h>

using namespace std;

void OutResponse(EdiResponse* resp) {
    cout << "code:";
	cout << resp->GetCode();
	cout << " msg:";
	cout << resp->GetMessage().c_str();
	cout << endl;
    for (EdiStatus* s = resp->GetNext(); NULL != s; s = resp->GetNext() ) {
        cout << endl;
        cout <<" userid:\t"    << s->GetUserid().c_str()     << endl;
        cout <<" file name:\t" << s->GetFileName().c_str()   << endl;
        cout <<" file size:\t" << s->GetFileSize()   << endl;
        cout <<" datetime:\t"  << s->GetDate().Format("%Y.%m.%d %H:%M:%S").c_str() << endl;
        cout <<" tracking:\t"  << s->GetTracking().c_str()  << endl;
        cout <<" rec count:\t" << s->GetRecordCount() << endl; 
        cout <<" status:\t"    << s->GetStatus().c_str()    << endl;
        cout << endl;
    }
};

class DummyProcessIndicator : public ProcessIndicator {
public:
    virtual void StartProcess(long maxState = 100, const wxString& processName = wxEmptyString) {
    };
    virtual void SetState(long state, const wxString& description = wxEmptyString) {
    };
    virtual void FinishProcess(const wxString& description = wxEmptyString) {
    };
};

int main(int argc, char ** argv) {

    wxApp::CheckBuildOptions(wxBuildOptions());

    wxInitializer initializer;
    if ( !initializer )
    {
        fprintf(stderr, "Failed to initialize the wxWindows library, aborting.");

        return -1;
    }

    try {
        CCfgCmdLine cfg(argc, argv);
        CStreamLogger log(cerr);
        DummyProcessIndicator indicator;
        HttpAgent agent(cfg, log, indicator);

		if (0 == cfg.GetParam("mode").CompareNoCase("password")) {
			auto_ptr<EdiResponse> r_pswd(agent.ChangePassword((const char*)cfg.GetParam("userid"), 
				                 (const char*)cfg.GetParam("password"),
								 (const char*)cfg.GetParam("newpassword")));
			OutResponse(r_pswd.operator->());
			return 0;
		}

        auto_ptr<EdiResponse> r_login( agent.Login((const char*)cfg.GetParam("userid"), (const char*)cfg.GetParam("password")));

        OutResponse(r_login.operator->());
        if ( r_login->GetCode() != ADVPCS_LOGIN_OK - ADVPCS_BASE ) {
            return -2;
        }

        EdiResponse* r_rqst = NULL;
        if ( 0 == cfg.GetParam("mode").CompareNoCase("upload") ) {
            r_rqst = agent.PostFile((const char*)cfg.GetParam("file")); 
        } else if (0 == cfg.GetParam("mode").CompareNoCase("status") ) {
            r_rqst = agent.GetStatus();
        } else {
            cout << "mode must be 'upload' or 'status' " << endl;
            return -3;
        }
        OutResponse(r_rqst);
    } catch (CAtfException &  ex) {
        cout << "Exception: " << (const char*)ex.GetText()<< endl << (const char*)ex.GetFileName()<<"["<<ex.GetLine()<<"]";
    } catch (CXmlLoadException& ex) {
		cout << " XML exception: " << ex.GetErrorCode() << endl << ex.GetErrorString() << endl << ex.GetLineNumber()<< endl;
    } catch (...) {
		cout << "Unknown exception";
	}
    return 0;
};

