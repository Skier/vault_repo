/*
 *  $RCSfile: Composer.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_COMPOSER_H__
#define __ADVPCS_COMPOSER_H__

class Document;
class ProcessIndicator;

class Composer {
public:
    Composer(ProcessIndicator& indicator) 
        : m_indicator(indicator)
    {};
    virtual bool Write(const Document& doc, const wxString& filename) = 0;
    virtual void Read(Document& doc) = 0;

protected:
    ProcessIndicator& GetIndicator() { return m_indicator; };

private: 
    ProcessIndicator& m_indicator;
};

#endif /* __ADVPCS_COMPOSER_H__ */