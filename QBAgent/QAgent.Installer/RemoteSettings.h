#pragma once

class RemoteSetting
{

   TCHAR m_szConnectionKey[100];  
   HKEY m_hKey;
   bool m_isSQLInstalledOnSDCard;
public:

	 RemoteSetting();

	 void Load();

	 void Save();

	 HRESULT InitRapi();

	 HRESULT TryRapiConnect(DWORD dwTimeOut);

	 void UnInitRapi();

	 void ProcessSQLServerRegistryCorrections();

	 ~RemoteSetting();

    const TCHAR* GetConnectionTicket() const
    {
        return m_szConnectionKey;
    }

	void SetConnectionTicket(TCHAR* szConnectionTicket)
	{
		wcscpy(m_szConnectionKey,szConnectionTicket);
	}

	bool IsSQLInstalledOnSDCard (){
		return m_isSQLInstalledOnSDCard;
	}
};