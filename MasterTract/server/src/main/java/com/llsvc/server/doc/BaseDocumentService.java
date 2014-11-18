package com.llsvc.server.doc;

import java.math.BigDecimal;

import java.util.Date;
import java.util.List;
import java.util.ArrayList;
import java.util.Map;
import java.util.Iterator;
import java.util.Collection;

import javax.persistence.Query;
import javax.persistence.NoResultException;
import javax.persistence.EntityTransaction;

import com.llsvc.server.framework.Logger;
import com.llsvc.server.framework.Service;
import com.llsvc.server.framework.EntityManagerHolder;

import com.llsvc.server.entity.AddressEntity;
import com.llsvc.server.entity.FileEntity;
import com.llsvc.server.entity.UserEntity;
import com.llsvc.server.entity.StateEntity;
import com.llsvc.server.entity.TractEntity;
import com.llsvc.server.entity.ProjectEntity;
import com.llsvc.server.entity.LeaseEntity;
import com.llsvc.server.entity.LeaseClauseEntity;
import com.llsvc.server.entity.LeaseAlarmEntity;
import com.llsvc.server.entity.LeaseTractEntity;
import com.llsvc.server.entity.LeaseTractQQEntity;
import com.llsvc.server.entity.LeaseTractHistoryEntity;
import com.llsvc.server.entity.LeaseBreakdownEntity;
import com.llsvc.server.entity.AssignmentEntity;
import com.llsvc.server.entity.AssignmentTractEntity;
import com.llsvc.server.entity.DocumentEntity;
import com.llsvc.server.entity.DocumentTypeEntity;
import com.llsvc.server.entity.DocumentStatusEntity;
import com.llsvc.server.entity.DocumentActorEntity;
import com.llsvc.server.entity.DocumentActorPhoneEntity;
import com.llsvc.server.entity.DocumentAttachmentEntity;
import com.llsvc.server.entity.DocumentReferenceEntity;
import com.llsvc.server.entity.DocumentProjectEntity;
import com.llsvc.server.entity.DocumentRecordEntity;
import com.llsvc.server.entity.SurfaceTractEntity;
import com.llsvc.server.entity.SurfaceRunsheetEntity;

public abstract class BaseDocumentService
{
    public Collection getDocumentReferences(Integer documentId) {
        List result = new ArrayList();
        DocumentEntity doc = getDocument(documentId);
        if ( null != doc.references ) {
            Iterator<DocumentReferenceEntity> dri = doc.references.iterator();
            while ( dri.hasNext() ) {
                DocumentReferenceEntity dr = dri.next();
                DocumentEntity rdoc = getDocument(dr.refereeId);
                if ( DocumentTypeEntity.KIND_LEASE.equals(rdoc.documentType.kind) ) {
                    result.add(EntityManagerHolder.getInstance().getEM().find(
                            LeaseEntity.class, rdoc.id));
                } else if ( DocumentTypeEntity.KIND_SURFACE_TRACT.equals(rdoc.documentType.kind) ) {
                    result.add(EntityManagerHolder.getInstance().getEM().find(
                            SurfaceTractEntity.class, rdoc.id));
                } else if ( DocumentTypeEntity.KIND_SURFACE_RUNSHEET.equals(rdoc.documentType.kind) ) {
                    result.add(EntityManagerHolder.getInstance().getEM().find(
                            SurfaceRunsheetEntity.class, rdoc.id));
                } else {
                    throw new RuntimeException("unsupported document kind " + rdoc.documentType.kind);
                } 
            }
        }
        return result;
    }

    protected DocumentEntity createDocument(DocumentEntity document) 
        throws Exception
    {
        if ( null == document ) {
            throw new Exception("document is required.");
        }
        if ( null == document.documentType ) {
            throw new Exception("documentType is required.");
        }
        if ( null == document.documentStatus ) {
            throw new Exception("documentStatus is required.");
        }

        document.id = null;
        EntityManagerHolder.getInstance().getEM().persist(document);

        return document;
    }

    protected DocumentEntity getDocument(Integer documentId) {
        DocumentEntity result = (DocumentEntity) EntityManagerHolder.getInstance().getEM().find(
                DocumentEntity.class, documentId);
        return result;
    }

    protected void saveDocumentDetails(DocumentEntity document, boolean removeRequires) 
        throws Exception
    {
        if ( removeRequires ) {
            DocumentEntity prevDoc = getDocument(document.id);

            // attachments
            Iterator<DocumentAttachmentEntity> dai = prevDoc.attachments.iterator();
            while ( dai.hasNext() ) {
                DocumentAttachmentEntity da = dai.next();
                EntityManagerHolder.getInstance().getEM().remove(da);
            }

            // references
            Iterator<DocumentReferenceEntity> dri = prevDoc.references.iterator();
            while ( dri.hasNext() ) {
                DocumentReferenceEntity dr = dri.next();
                EntityManagerHolder.getInstance().getEM().remove(dr);
            }

            // actors
            Iterator<DocumentActorEntity> a = prevDoc.actors.iterator();
            while ( a.hasNext() ) {
                DocumentActorEntity aa = a.next();
                Iterator<DocumentActorPhoneEntity> pi = aa.phones.iterator();
                while ( pi.hasNext() ) {
                    DocumentActorPhoneEntity p = pi.next();
                    EntityManagerHolder.getInstance().getEM().remove(p);
                }
                EntityManagerHolder.getInstance().getEM().remove(aa);
                EntityManagerHolder.getInstance().getEM().remove(aa.address);
            }

        }  // end of removeRequires

        // references
        if ( null != document.references ) {
            Iterator<DocumentReferenceEntity> dri = document.references.iterator();
            while ( dri.hasNext() ) {
                DocumentReferenceEntity dr = dri.next();
                dr.id = null;
                dr.referrer = document;
                EntityManagerHolder.getInstance().getEM().persist(dr);
            }
        }

        // actors
        if ( null != document.actors ) {
            Iterator<DocumentActorEntity> a = document.actors.iterator();
            while ( a.hasNext() ) {
                DocumentActorEntity aa = a.next();
                AddressEntity address = aa.address;
                address.id = null;
                EntityManagerHolder.getInstance().getEM().persist(address);

                aa.id = null;
                aa.document = document;    
                EntityManagerHolder.getInstance().getEM().persist(aa);

                Iterator<DocumentActorPhoneEntity> pi = aa.phones.iterator();
                while ( pi.hasNext() ) {
                    DocumentActorPhoneEntity p = pi.next();
                    p.id = null;
                    p.actor = aa;
                    EntityManagerHolder.getInstance().getEM().persist(p);
                }
            }
        }
        
        // attachments
        if ( null != document.attachments ) {
            Logger.getInstance().getLog().debug("BaseDocumentService.saveDocumentDetails: attachments.count=" + document.attachments.size() + " obtained.");
            Iterator<DocumentAttachmentEntity> a = document.attachments.iterator();
            while ( a.hasNext() ) {
                DocumentAttachmentEntity aa = a.next();
                aa.id = null;
                aa.document = document;
                Logger.getInstance().getLog().debug("BaseDocumentService.saveDocumentDetails: attachments.file.id=" + aa.file.id + " obtained.");
                aa.file = EntityManagerHolder.getInstance().getEM().find(FileEntity.class, aa.file.id);
                Logger.getInstance().getLog().debug("BaseDocumentService.saveDocumentDetails: aa.file=" + aa.file);
                EntityManagerHolder.getInstance().getEM().persist(aa);
            }
        }
    }

}
