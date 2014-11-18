#include "stdafx.h"

#include "QEncrypt.h"
#include <Wincrypt.h>

#pragma comment(lib,"Crypt32.lib")


#define ENCRYPT_ALGORITHM CALG_RC2 //(PKCS_7_ASN_ENCODING | X509_ASN_ENCODING)
 
#define ENCRYPT_BLOCK_SIZE 8 
#define KEYLENGTH  0x00800000

int QEncrypt::Encrypt(TCHAR * szConnectionKey, TCHAR * szPassword, TCHAR * rvBuffer)
{
	HCRYPTPROV hCryptProv = NULL; 
	HCRYPTKEY hKey = NULL; 
	HCRYPTKEY hXchgKey = NULL; 
	HCRYPTHASH hHash = NULL; 

	PBYTE pbBuffer; 
	DWORD dwBlockLen; 
	DWORD dwBufferLen; 
	DWORD dwCount; 

	char cPassword[100];
	char cConnectionKey[100];

	wcstombs(cPassword,szPassword,sizeof(cPassword));
	wcstombs(cConnectionKey,szConnectionKey,sizeof(cConnectionKey));

	// Get the handle to the default provider. 
	if(!CryptAcquireContext(
		  &hCryptProv, 
		  NULL, 
		  MS_DEF_PROV, 
		  PROV_RSA_FULL, 
		  0))
	{
		
		if(GetLastError() == NTE_BAD_KEYSET)
		{
            // No default container was found. Attempt to create it.
            if(!CryptAcquireContext(
                &hCryptProv, 
                NULL, 
                MS_DEF_PROV, 
                PROV_RSA_FULL, 
                CRYPT_NEWKEYSET)) 
                throw CWin32Error();
		}
		else
		  throw CWin32Error();
	}

	if(!CryptCreateHash(
		   hCryptProv, 
		   CALG_MD5, 
		   0, 
		   0, 
		   &hHash))
		{
			throw CWin32Error();
		}
 
	//-------------------------------------------------------------------
	// Hash the password. 

	if(!CryptHashData(
		   hHash, 
		   (BYTE *)cPassword, 
		   strlen(cPassword), 
		   0))
	 {
	   throw CWin32Error();
	 }
	//-------------------------------------------------------------------
	// Derive a session key from the hash object. 

	if(!CryptDeriveKey(
		   hCryptProv, 
		   ENCRYPT_ALGORITHM, 
		   hHash, 
		   CRYPT_EXPORTABLE, 
		   &hKey))
	 {
	   throw CWin32Error();
	 }

	//BYTE pBypeIV[] = {1,2,3,4,5,6,7,8};
	//CryptSetKeyParam(hKey,KP_IV,pBypeIV,0);

	//-------------------------------------------------------------------
	// Destroy hash object. 

	CryptDestroyHash(hHash);

	
	dwCount = strlen(cConnectionKey);

	dwBlockLen = 1000 - 1000 % dwCount; 
	dwBufferLen = dwBlockLen + dwCount;

	pbBuffer = (BYTE *)malloc(dwBufferLen);

	memset(pbBuffer,'\0',dwBufferLen);
	memcpy(pbBuffer,cConnectionKey,dwCount);

	
	if(!CryptEncrypt(
		 hKey, 
		 0, 
		 TRUE, 
		 0, 
		 pbBuffer, 
		 &dwCount, 
		 dwBufferLen))
	{ 
	   throw CWin32Error();
	} 

	CryptBinaryToString(pbBuffer,
		dwCount,
		CRYPT_STRING_BASE64,
		rvBuffer,
		&dwBufferLen);

	//memcpy(rvBuffer,pbBuffer,dwCount + 1);
	
	if(pbBuffer) 
		 free(pbBuffer); 

	CryptDestroyKey(hKey);

	CryptReleaseContext(hCryptProv, 0);
	
	return STRLEN(rvBuffer);
}