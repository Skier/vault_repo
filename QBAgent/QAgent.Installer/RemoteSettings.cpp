#include "stdafx.h"
#include "RemoteSettings.h"
#include "rapi.h"
#include "Win32Error.h"
#include "AtlBase.h"

#pragma comment(lib,"rapi.lib")

#define REG_SUBKEY _T("Software\\Apps\\Affilia Software Q-Agent")
#define REG_VALUE_LENGTH 1000


//--------------------------------------------------------------------------

RemoteSetting::RemoteSetting()
{
	m_isSQLInstalledOnSDCard = false;
}

HRESULT RemoteSetting::TryRapiConnect(DWORD dwTimeOut)
{
    HRESULT            hr = E_FAIL;
    RAPIINIT           riCopy;
    bool          fInitialized = false;

    ZeroMemory(&riCopy, sizeof(riCopy));
    riCopy.cbSize = sizeof(riCopy);

    hr = CeRapiInitEx(&riCopy);
    if (SUCCEEDED(hr))
    {
        DWORD dwRapiInit = 0;
        fInitialized = true;

        dwRapiInit = WaitForSingleObject(
                    riCopy.heRapiInit,
                    dwTimeOut);
        if (WAIT_OBJECT_0 == dwRapiInit)
        {
            //  heRapiInit signaled:
            // set return error code to return value of RAPI Init function
            hr = riCopy.hrRapiInit;  
        }
        else if (WAIT_TIMEOUT == dwRapiInit)
        {
            // timed out: device is probably not connected
            // or not responding
            hr = HRESULT_FROM_WIN32(ERROR_TIMEOUT);
        }
        else
        {
            // WaitForSingleObject failed
            hr = HRESULT_FROM_WIN32(GetLastError());
        }
    }

   if (fInitialized && FAILED(hr))
   {
        CeRapiUninit();
   }
    return hr;
}


HRESULT RemoteSetting::InitRapi()
{
	//CeRapiInit();
	return TryRapiConnect(10000);
}

void RemoteSetting::UnInitRapi()
{
	CeRapiUninit();	
}


void RemoteSetting::ProcessSQLServerRegistryCorrections()
{	
	LPCE_FIND_DATA rootFoldersList = NULL;
	DWORD rootFoldersCount = 0;


	CeFindAllFiles(_T("\\*.*"), FAF_ATTRIB_CHILDREN | FAF_NAME | FAF_FOLDERS_ONLY | FAF_OID, 
		&rootFoldersCount, &rootFoldersList);

	//Now we are reading HKLM\Loader\SystemPath value
	//Start reading
	DWORD dWord;
	HKEY currentRegistryKey;
	TCHAR systemPathValue[REG_VALUE_LENGTH];

	if(CeRegCreateKeyEx(HKEY_LOCAL_MACHINE,
		_T("Loader"),
		0,
		_T(""),
		0,
		0,
		0,
		&currentRegistryKey,
		&dWord) != ERROR_SUCCESS)
		throw CWin32Error();

	DWORD dwType = 0;
	dWord = sizeof(systemPathValue);

	CeRegQueryValueEx(currentRegistryKey,
		_T("SystemPath"),
		NULL,
		&dwType,
		(BYTE*)systemPathValue,
		&dWord);

	CeRegCloseKey(m_hKey);
	//End reading reading

	//Preparing systemPathValue array to be null terminated string
	for (int i = 0; i <= REG_VALUE_LENGTH - 2; i++){
		if (systemPathValue[i] == 0 && systemPathValue[i+1] != 0){
			systemPathValue[i] = '\n';
		} else if (systemPathValue[i] == 0 && systemPathValue[i+1] == 0){
			break;
		}
	}


	bool isMadeChangesToRegistry = false;
	for( int i = 0; i < rootFoldersCount; i++ )
	{	
		//We shouldn't take care about defaulr install dir		
		if (_wcsicmp(rootFoldersList[i].cFileName, _T("Windows")) == 0){
			continue;
		}					

		//Now we are trying to find "Microsoft SQL Mobile 2005"	folder in each root folder
		wchar_t* folderToFind = wcscat(rootFoldersList[i].cFileName, _T("\\Microsoft SQL Mobile 2005"));
		CE_FIND_DATA findData;			
		HANDLE folderRetHdl = CeFindFirstFile(folderToFind, &findData);

		if (INVALID_HANDLE_VALUE != folderRetHdl){						

			//Checking if this folder already contains in registry			
			if (wcsstr(systemPathValue, folderToFind) == NULL){
				//Now we need to add this path to registry
				wcscat(systemPathValue, _T("\n\\"));
				wcscat(systemPathValue, folderToFind);
				wcscat(systemPathValue, _T("\\"));
				isMadeChangesToRegistry = true;
			}
		}
	}


	if (isMadeChangesToRegistry){
		m_isSQLInstalledOnSDCard = true;

		//Calculate new length of registry sting
		int newRegValueStringLenght = wcslen(systemPathValue) + 2;

		//Set temporary '\n' values back to '\0'
		for (int i = 0; i <= REG_VALUE_LENGTH - 2; i++){
			if (systemPathValue[i] == '\n'){
				systemPathValue[i] = 0;
			} else if (systemPathValue[i] == 0){
				break;
			}
		}

		//Writing back to registry
		CeRegCreateKeyEx(HKEY_LOCAL_MACHINE,
			_T("Loader"),
			0,
			_T(""),
			0,
			0,
			0,
			&currentRegistryKey,
			&dWord);

		 CeRegSetValueEx(currentRegistryKey,
			_T("SystemPath"),
			0,
			REG_MULTI_SZ,
			(LPBYTE)systemPathValue,
			newRegValueStringLenght * sizeof(wchar_t));

		CeRegCloseKey(currentRegistryKey);
		//End Writing back to registry
	}
}

//--------------------------------------------------------------------------

void RemoteSetting::Load()
{	
	DWORD dWord;

	if(CeRegCreateKeyEx(HKEY_LOCAL_MACHINE,
		REG_SUBKEY,
		0,
		_T(""),
		0,
		0,
		0,
		&m_hKey,
		&dWord) != ERROR_SUCCESS)
		throw CWin32Error();

	DWORD dwType = 0;

	dWord = sizeof(m_szConnectionKey);

	CeRegQueryValueEx(m_hKey,
		_T("ConnectionTicket"),
		NULL,
		&dwType,
		(BYTE*)m_szConnectionKey,
		&dWord);

	CeRegCloseKey(m_hKey);
	
}

//--------------------------------------------------------------------------

void RemoteSetting::Save()
{
	DWORD dWord;

	CeRegCreateKeyEx(HKEY_LOCAL_MACHINE,
		REG_SUBKEY,
		0,
		_T(""),
		0,
		0,
		0,
		&m_hKey,
		&dWord);

	 CeRegSetValueEx(m_hKey,
		_T("ConnectionTicket"),
		0,
		REG_SZ,
		(LPBYTE)m_szConnectionKey,
		wcsnlen(m_szConnectionKey,sizeof(m_szConnectionKey)) * sizeof(TCHAR));


	 CeRegSetValueEx(m_hKey,
		_T("UpdateSettings"),
		0,
		REG_SZ,
		(LPBYTE)_T("true"),
		10);

	CeRegCloseKey(m_hKey);
}

//--------------------------------------------------------------------------

RemoteSetting::~RemoteSetting()
{
	
}
