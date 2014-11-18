/*
 *  $RCSfile: ComposerException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_COMPOSER_EXCEPTION_H__
#define __ADVPCS_COMPOSER_EXCEPTION_H__

/* -------------------------- header place ---------------------------------- */
#include <atf/Exception.h>
/* -------------------------- implementation place -------------------------- */

extern const ATF_ERROR ADVPCS_COMPOSER_ERR; //  = ATF_USER_ERR + 2;

class ComposerException : public CAtfException {
public:
    ComposerException(const char *fileName, int lineNo, const wxString& message);
};

#define THROW_COMPOSER_EXCEPTION(message) \
    throw ComposerException(__FILE__, __LINE__, message);

#endif /* __ADVPCS_COMPOSER_EXCEPTION_H__ */