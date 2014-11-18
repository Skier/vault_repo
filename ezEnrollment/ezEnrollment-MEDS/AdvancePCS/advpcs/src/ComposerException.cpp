/*
 *  $RCSfile: ComposerException.cpp,v $
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

/* -------------------------- header place ---------------------------------- */
#include <advpcs/ComposerException.h>
/* -------------------------- implementation place -------------------------- */

const ATF_ERROR ADVPCS_COMPOSER_ERR  = ATF_USER_ERR + 2;


ComposerException::ComposerException(const char *fileName, int lineNo, const wxString& message)
    : CAtfException(fileName, lineNo, ADVPCS_COMPOSER_ERR, message.c_str())
{    
};
