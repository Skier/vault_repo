#pragma once
#include "Win32Error.h"

class QEncrypt
{
public:
	int Encrypt(TCHAR * sConnectionKey, TCHAR * sPassword, TCHAR * rvBuffer);
};