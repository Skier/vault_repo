// MainDlg.cpp : implementation of the CMainDlg class
//
/////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "resource.h"

#include "aboutdlg.h"
#include "MainDlg.h"
#include "QEncrypt.h"
#include "RemoteSettings.h"

#define APP_TITLE _T("Q-Agent Connection Ticket Manager")

BOOL CMainDlg::PreTranslateMessage(MSG* pMsg)
{
	return CWindow::IsDialogMessage(pMsg);
}

//BOOL CMainDlg::OnIdle()
//{//
//	return FALSE;
//}

LRESULT CMainDlg::OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
	// center the dialog on the screen
	CenterWindow();

	// set icons
	HICON hIcon = (HICON)::LoadImage(_Module.GetResourceInstance(), MAKEINTRESOURCE(IDR_MAINFRAME), 
		IMAGE_ICON, ::GetSystemMetrics(SM_CXICON), ::GetSystemMetrics(SM_CYICON), LR_DEFAULTCOLOR);
	SetIcon(hIcon, TRUE);
	HICON hIconSmall = (HICON)::LoadImage(_Module.GetResourceInstance(), MAKEINTRESOURCE(IDR_MAINFRAME), 
		IMAGE_ICON, ::GetSystemMetrics(SM_CXSMICON), ::GetSystemMetrics(SM_CYSMICON), LR_DEFAULTCOLOR);
	SetIcon(hIconSmall, FALSE);

	m_link.SubclassWindow(GetDlgItem(IDC_LINK_CREATE_CONNECTION_KEY));
	m_link.SetHyperLink(_T("https://login.quickbooks.com/j/qbn/sdkapp/confirm?serviceid=2004&appid=96140499"));

	m_txtConnectionKey.Attach(GetDlgItem(IDC_EDIT_CONNECTION_KEY));
	m_txtPassword.Attach(GetDlgItem(IDC_EDIT_PASSWORD));
	m_txtPasswordConfirm.Attach(GetDlgItem(IDC_EDIT_PASSWORD_CONFIRM));

	// register object for message filtering and idle updates
	CMessageLoop* pLoop = _Module.GetMessageLoop();
	ATLASSERT(pLoop != NULL);
	pLoop->AddMessageFilter(this);
	//pLoop->AddIdleHandler(this);

	//UIAddChildWindowContainer(m_hWnd);

	/*try
	{
		RemoteSetting remoteSettings;
		remoteSettings.Load();
		
		m_txtConnectionKey.SetWindowTextW(remoteSettings.ConnectionTicket());

	}
	catch(CWin32Error e)
	{
		MessageBox(e.Description(),APP_TITLE,MB_ICONERROR);
	}*/

	return TRUE;
}

LRESULT CMainDlg::OnShowWindow(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
	//::SetFocus(GetDlgItem(IDC_EDIT_CONNECTION_KEY));
	m_txtConnectionKey.SetFocus();
	m_txtConnectionKey.SetSelAll();
	return FALSE;
}
LRESULT CMainDlg::OnAppAbout(WORD /*wNotifyCode*/, WORD /*wID*/, HWND /*hWndCtl*/, BOOL& /*bHandled*/)
{


	//CAboutDlg dlg;
	//dlg.DoModal();
	return 0;
}

void CMainDlg::CheckSql()
{
	if (wcscmp(CmdLineParams, _T("CheckSQL")) == 0)
	{
		try
		{

			::SetCursor(::LoadCursor(NULL, IDC_WAIT));
			if (m_remoteSettings.InitRapi() != S_OK){
				MessageBox(_T("Cannot establish connection with remote device. Make sure ActiveSync is connected and run installation again."),
					APP_TITLE, MB_ICONERROR);
				::SetCursor(::LoadCursor(NULL, IDC_ARROW));
				CloseDialog(0);
				return;
			}

			::SetCursor(::LoadCursor(NULL, IDC_ARROW));
			m_remoteSettings.ProcessSQLServerRegistryCorrections();
			m_remoteSettings.UnInitRapi();
		} catch(CWin32Error e)
		{
			MessageBox(e.Description(),APP_TITLE,MB_ICONERROR);	
			CloseDialog(0);
		}
	}

	return;
}

LRESULT CMainDlg::OnOK(WORD /*wNotifyCode*/, WORD wID, HWND /*hWndCtl*/, BOOL& /*bHandled*/)
{
	TCHAR sConnectionKey[100];

	if(m_txtConnectionKey.GetWindowText(sConnectionKey,
		sizeof(sConnectionKey)) == 0)
	{
		MessageBox(_T("Please enter connection key"),
			APP_TITLE,
			MB_OK | MB_ICONINFORMATION);

		m_txtConnectionKey.SetFocus();

		return 0;
	}
	
	TCHAR sPassword[100],sPasswordConfirm[100];
	if(m_txtPassword.GetWindowText(sPassword,
		sizeof(sPassword)) == 0)
	{
		MessageBox(_T("Please enter password"),
			APP_TITLE,
			MB_OK | MB_ICONINFORMATION);

		m_txtPassword.SetFocus();

		return 0;
	}


	if(m_txtPasswordConfirm.GetWindowText(sPasswordConfirm,
		sizeof(sPasswordConfirm)) == 0)
	{
		MessageBox(_T("Please confirm password"),
			APP_TITLE,
			MB_OK | MB_ICONINFORMATION);

		m_txtPasswordConfirm.SetFocus();

		return 0;
	}
	
	if(StrCmp(sPassword,sPasswordConfirm) != 0)
	{
		MessageBox(_T("Please enter match passwords"),
			APP_TITLE,
			MB_OK | MB_ICONINFORMATION);

		m_txtPasswordConfirm.SetSelAll();
		m_txtPasswordConfirm.SetFocus();

		return 0;
	}

	QEncrypt encrypt;

	try
	{

		::EnableWindow(GetDlgItem(IDOK),FALSE);
		::EnableWindow(GetDlgItem(IDCANCEL),FALSE);
		::SetCursor(::LoadCursor(NULL, IDC_WAIT));

		TCHAR sEncryptedKey[100];

		memset(sEncryptedKey,'\0',sizeof(sEncryptedKey));

		encrypt.Encrypt(sConnectionKey,sPassword,sEncryptedKey);

		//MessageBox(sEncryptedKey);
		
		if (m_remoteSettings.InitRapi() != S_OK){
			MessageBox(_T("Cannot establish connection with remote device. Make sure ActiveSync is connected and try again."),
				APP_TITLE, MB_ICONERROR);

			::EnableWindow(GetDlgItem(IDOK),TRUE);
			::EnableWindow(GetDlgItem(IDCANCEL),TRUE);
			::SetCursor(::LoadCursor(NULL, IDC_ARROW));

			return 0;
		}
		m_remoteSettings.SetConnectionTicket(sEncryptedKey);
		m_remoteSettings.Save();
		m_remoteSettings.UnInitRapi();

		MessageBox(_T("Settings were successfully updated."),APP_TITLE,
			MB_ICONINFORMATION);
	}
	catch(CWin32Error e)
	{
		MessageBox(e.Description(),APP_TITLE,MB_ICONERROR);

		::EnableWindow(GetDlgItem(IDOK),TRUE);
		::EnableWindow(GetDlgItem(IDCANCEL),TRUE);
		::SetCursor(::LoadCursor(NULL, IDC_ARROW));

		return 0;

	}

	// TODO: Add validation code 
	CloseDialog(wID);
	return 0;
}

LRESULT CMainDlg::OnCancel(WORD /*wNotifyCode*/, WORD wID, HWND /*hWndCtl*/, BOOL& /*bHandled*/)
{
	CloseDialog(wID);
	return 0;
}

void CMainDlg::CloseDialog(int nVal)
{
	if (m_remoteSettings.IsSQLInstalledOnSDCard()){
		MessageBox(_T("You have installed Microsoft SQL Mobile on the storage card. You have to soft reset your device."), 
			APP_TITLE, MB_ICONINFORMATION);
	}
	
	DestroyWindow();
	::PostQuitMessage(nVal);
}

