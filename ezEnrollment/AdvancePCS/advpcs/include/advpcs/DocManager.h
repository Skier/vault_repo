/*
 *  $RCSfile: DocManager.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_DOC_MANAGER_H__
#define __ADVPCS_DOC_MANAGER_H__

/* -------------------------- header place ---------------------------------- */
#pragma warning( disable : 4786 ) 
#include <map>
/* -------------------------- implementation place -------------------------- */
class Document;
class Composer;
class CXmlNode;
class XmlDescriptor;
class ProcessIndicator;

class DocManager {
public:
    DocManager(const CXmlNode& cfg, ProcessIndicator& indicator);
    ~DocManager();
    Document* Create() const;
    void SaveAs(const Document& doc, const wxString& fileName) const; 
    void Save(const Document& doc) const;
    Document* Open(const wxString& fileName) const;

private:
    void BuildDescriptors(const CXmlNode& cfg);
    void BuildComposers(ProcessIndicator& indicator);
    
    Composer& FindComposerForFile(const wxString& fileName) const;
private:
    XmlDescriptor *m_headerDesc;
    XmlDescriptor *m_detailDesc;
    std::map<wxString, Composer*> m_composers;
};

#endif /* __ADVPCS_DOC_MANAGER_H__ */
