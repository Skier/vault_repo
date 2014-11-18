package com.llsvc.server.doc;

import java.math.BigDecimal;

import java.util.Date;
import java.util.List;
import java.util.ArrayList;
import java.util.Map;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Collection;

import javax.persistence.Query;
import javax.persistence.NoResultException;
import javax.persistence.EntityTransaction;

import com.llsvc.server.framework.Logger;
import com.llsvc.server.framework.Service;
import com.llsvc.server.framework.EntityManagerHolder;

import com.llsvc.server.coverage.CoverageHelper;

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
import com.llsvc.server.entity.CoverageTractEntity;
import com.llsvc.server.entity.CoverageTractSetEntity;

public class DocumentService
{
    public static final Integer LEASE_COVERAGE_TYPE = new Integer(0);

    public DocumentPackage getPackage(UserEntity user) {
        Logger.getInstance().getLog().debug("DocumentService.getPackage: user=" + user.login);
        Logger.getInstance().getLog().debug("DocumentService.getPackage: authorized user=" + Service.getAuthorizedUser().login);

        DocumentPackage pkg = new DocumentPackage();
        pkg.stateList = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                StateEntity.FIND_ALL).getResultList();
        pkg.documentTypeList = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                DocumentTypeEntity.FIND_ALL).getResultList();
        pkg.documentStatusList = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                DocumentStatusEntity.FIND_ALL).getResultList();

        Query projectQuery = EntityManagerHolder.getInstance().getEM().createQuery(
                "select o from ProjectEntity as o where o.client.id=:clientId order by o.name");
        projectQuery.setParameter("clientId", Service.getAuthorizedUser().client.id);
        pkg.projectList = projectQuery.getResultList();

        return pkg;
    }

    public List<ProjectEntity> getProjects() {
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select o from ProjectEntity as o where o.client.id=:clientId order by o.name");
        query.setParameter("clientId", Service.getAuthorizedUser().client.id);
        return query.getResultList();
    }

    public ProjectEntity saveProject(ProjectEntity project) {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            if ( 0 == project.id ) {
                project.id = null;
                project.client = Service.getAuthorizedUser().client;
                EntityManagerHolder.getInstance().getEM().persist(project);
            } else {
                EntityManagerHolder.getInstance().getEM().merge(project);
            }
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.saveProject: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.saveProject: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return (ProjectEntity) EntityManagerHolder.getInstance().getEM().find(ProjectEntity.class, project.id);        
    }

    public Collection getDocumentReferences(Integer documentId) {
        List result = new ArrayList();
        DocumentEntity doc = getDocument(documentId);
        if ( null != doc.references ) {
            Iterator<DocumentReferenceEntity> dri = doc.references.iterator();
            while ( dri.hasNext() ) {
                DocumentReferenceEntity dr = dri.next();
                DocumentEntity rdoc = getDocument(dr.refereeId);
                if ( DocumentTypeEntity.KIND_LEASE.equals(rdoc.documentType.kind) ) {
                    result.add(getLease(rdoc.id));
/*
                } else if ( DocumentTypeEntity.KIND_ASSIGNMENT.equals(rdoc.documentType.kind) ) {
                    result.add(getLeaseAssignment(rdoc.id));
                } else if ( DocumentTypeEntity.KIND_RECORD.equals(rdoc.documentType.kind) ) {
                    result.add(getRecord(rdoc.id));
*/
                } else {
                    throw new RuntimeException("unsupported document kind " + rdoc.documentType.kind);
                } 
            }
        }
        return result;
    }

/*
    public Collection<RecordEntity> getDocumentRecords(Integer documentId) {
        Collection<RecordEntity> result = new ArrayList<RecordEntity>();
        Iterator dri = getDocumentReferences(documentId).iterator();
        while ( dri.hasNext() ) {
            Object doc = dri.next();
            if ( doc instanceof RecordEntity ) {
                result.add((RecordEntity) doc);
            }
        }
        return result;
    }

    public void createTract(TractEntity tract) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            tract.id = null;
            EntityManagerHolder.getInstance().getEM().persist(tract);
        } catch (Exception ex) {
            tr.rollback();
            throw ex;
        }
        
        tr.commit();
    }
*/
    public Collection<TractEntity> findTracts(TractEntity mask) {
        Logger.getInstance().getLog().debug("DocumentService.findTracts: request township=" 
                + mask.township
                + ", range=" + mask.range
                + ", section=" + mask.section
                + ", qq=" + mask.qq);
        Query query  = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                TractEntity.FIND_BY_MASK);
        query.setParameter("township", mask.township);
        query.setParameter("range", mask.range);
        query.setParameter("section", mask.section);
        query.setParameter("qq", (null != mask.qq ? mask.qq : "!"));

        Collection<TractEntity> result = query.getResultList();
        if ( null != result ) {
            Logger.getInstance().getLog().debug("DocumentService.findTracts: result.size=" + result.size());
        } else {
            Logger.getInstance().getLog().debug("DocumentService.findTracts: nothing found.");
        }
        return result;
    }

    public Collection<String> getLeaseNames(LeaseSearchCriterias criterias) {
        try {
/*
            Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                    "select o.leaseName from LeaseEntity as o where o.document.user.client.id=:clientId order by o.leaseName");
            query.setParameter("clientId", Service.getAuthorizedUser().client.id);
            return query.getResultList();
*/
            Query query = createSearchQuery(criterias, true);
            return query.getResultList();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.getLeaseNames: unexpected exception, ", ex);
            throw new RuntimeException(ex);
        }
    }

    public Integer getLeasesCount(LeaseSearchCriterias criterias) {
/*
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select count(o) from LeaseEntity as o where o.document.user.client.id=:clientId");
        query.setParameter("clientId", Service.getAuthorizedUser().client.id);
*/
/*
        Query query = createSearchQuery(criterias, true);
        Iterator i = query.getResultList().iterator();
        if ( i.hasNext() ) {
            return ((Long)i.next()).intValue();
        } else {
            return 0;
        }
*/
        throw new RuntimeException("not implemented.");
    }

/*
    public Collection<LeaseEntity> findLeases(LeaseSearchCriterias criterias, int start, int limit) {
        try {
            Query query = createSearchQuery(criterias, false);
            query.setFirstResult(start);
            query.setMaxResults(limit);

            return query.getResultList();
        } catch (Throwable ex) {
            ex.printStackTrace();
            throw new RuntimeException(ex);
        }
    }
*/
    public Collection<LeaseEntity> getLeasesRange(Integer start, Integer limit, LeaseSearchCriterias criterias) {
/*
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select o from LeaseEntity as o where o.document.user.client.id=:clientId order by o.leaseName");
        query.setParameter("clientId", Service.getAuthorizedUser().client.id);
        query.setFirstResult(start);
        query.setMaxResults(limit);
        return query.getResultList();
*/
/*
        LeaseSummary ls = getLeaseSummary(criterias);
        System.out.println("summary=" + ls);
*/
        try {
            Query query = createSearchQuery(criterias, false);
            query.setFirstResult(start);
            query.setMaxResults(limit);

            return query.getResultList();
        } catch (Throwable ex) {
            ex.printStackTrace();
            throw new RuntimeException(ex);
        }
    }

    public LeaseSummary getLeaseSummary(LeaseSearchCriterias criterias) {
        LeaseSummary result = new LeaseSummary();
        try {
            Query query = createSummarySearchQuery(criterias);
            Object[] data = (Object[]) query.getSingleResult();
            result.grossAc = (BigDecimal) data[0];
            result.netAc = (BigDecimal) data[1];
            result.interest = (BigDecimal) data[2];
            result.leaseBurden = (BigDecimal) data[3];
            result.leaseNri = (BigDecimal) data[4];
            result.wi = (BigDecimal) data[5];
            result.additionalBurden = (BigDecimal) data[6];
            result.nri = (BigDecimal) data[7];
            result.net = (BigDecimal) data[8];
        } catch (Throwable ex) {
            ex.printStackTrace();
            throw new RuntimeException(ex);
        }
        result.leaseNames = getLeaseNames(criterias);
        return result;
    }

    public Collection<LeaseEntity> getLeasesByProject(Integer projectId) {
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select o from LeaseEntity as o join o.document as d join d.projects as dp join dp.project as p where p.id=:projectId order by o.leaseName");
        query.setParameter("projectId", projectId);
        return query.getResultList();
    }

    public LeaseEntity getLease(Integer leaseId) {
        LeaseEntity result = (LeaseEntity) EntityManagerHolder.getInstance().getEM().find(
                LeaseEntity.class, leaseId);
        return result;
    }

    public LeaseEntity createLease(LeaseEntity lease) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            if ( null == lease.document ) {
                throw new Exception("document is not present.");
            }

            lease.document.id = null;
            lease.document.documentType = getLeaseDocumentType();
            lease.document.documentStatus = getUnrecordedDocumentStatus();
            EntityManagerHolder.getInstance().getEM().persist(lease.document);

            lease.id = lease.document.id;
            EntityManagerHolder.getInstance().getEM().persist(lease);

/*
            // extent
            if ( null != lease.extention ) {
                lease.extention.id = null;
                lease.extention.lease = lease;
                EntityManagerHolder.getInstance().getEM().persist(lease.extention);
            }            
*/
/*
            lease.clause.id = lease.id;
            lease.clause.extent.id = lease.clause.extent.id;
            EntityManagerHolder.getInstance().getEM().persist(lease.clause);
*/
            saveLeaseDetails(lease, false);            
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.createLease: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.createLease: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getLease(lease.id);        
    }

    public LeaseEntity storeLease(LeaseEntity lease) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            lease.id = lease.document.id;
            saveLeaseDetails(lease, true);

            EntityManagerHolder.getInstance().getEM().merge(lease.document);
/*
            if ( null != lease.extention ) {
                EntityManagerHolder.getInstance().getEM().merge(lease.extention);
            }            
*/
/*
            lease.clause.id = lease.document.id;
            EntityManagerHolder.getInstance().getEM().merge(lease.clause);
*/
            EntityManagerHolder.getInstance().getEM().merge(lease);
            
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.storeLease: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.storeLease: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getLease(lease.id);        
    }

    public void deleteLease(Integer leaseId) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            LeaseEntity lease = getLease(leaseId);
            EntityManagerHolder.getInstance().getEM().remove(lease);
            EntityManagerHolder.getInstance().getEM().remove(lease.document);
        } catch (Exception ex) {
            Logger.getInstance().getLog().error("DocumentService.deleteLease: ", ex);
            tr.rollback();
            throw ex;
        }

        try {        
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.deleteLease: commit problem.", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
    }
    
/* to do: fix or remove
    public AssignmentEntity getAssignmentByLease(Integer leaseId) {
        Query query  = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                    AssignmentEntity.FIND_BY_LEASE_ID);
        query.setParameter("leaseId", leaseId);
        try {
            return (AssignmentEntity) query.getSingleResult();
        } catch (NoResultException ex) {
            return null;
        }
    }
*/
    public Collection<LeaseTractEntity> findLeaseTracts(TractEntity mask) {
        Query query  = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                LeaseTractEntity.FIND_BY_TRACT_MASK);
        query.setParameter("township", mask.township);
        query.setParameter("range", mask.range);
        query.setParameter("section", mask.section);
        query.setParameter("qq", (null != mask.qq ? mask.qq : "!"));

        return query.getResultList();
    }

/*
    public Collection<AssignmentEntity> findLeaseAssignments(DocumentRecordEntity mask) {
        Query query  = null;
        if ( null != mask.docNo ) {
            query = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                    AssignmentEntity.FIND_BY_DOC_NO);
            query.setParameter("stateId", mask.state.id);
            query.setParameter("countyId", mask.county.id);
            query.setParameter("docNo", mask.docNo);
            query.setParameter("volume", (null != mask.volume ? mask.volume : "!"));
            query.setParameter("page", (null != mask.page ? mask.page : "!"));
        } else if ( null != mask.volume && null != mask.page ) {
            query = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                    AssignmentEntity.FIND_BY_VOLUME_AND_PAGE);
            query.setParameter("stateId", mask.state.id);
            query.setParameter("countyId", mask.county.id);
            query.setParameter("volume", mask.volume);
            query.setParameter("page", mask.page);
        } else {
            query = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                    AssignmentEntity.FIND_BY_STATE_AND_COUNTY);
            query.setParameter("stateId", mask.state.id);
            query.setParameter("countyId", mask.county.id);
        }

        return query.getResultList();
    }
*/

/* to do: fix or remove
    public Collection<AssignmentEntity> getLeaseAssignments() {
        return EntityManagerHolder.getInstance().getEM().createNamedQuery(
                AssignmentEntity.FIND_ALL).getResultList();
    }
*/

/* to do: fix or remove
    public void createLeaseAssignment(AssignmentEntity assignment) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            if ( null == assignment.document ) {
                throw new Exception("document is not supplied.");
            }

            assignment.document.id = null;
            assignment.document.documentType = getLeaseAssignmentDocumentType();
            assignment.document.documentStatus = getUnrecordedDocumentStatus();
            EntityManagerHolder.getInstance().getEM().persist(assignment.document);

            assignment.id = assignment.document.id;
            EntityManagerHolder.getInstance().getEM().persist(assignment);

            if ( null != assignment.document.actors ) {
                Iterator<DocumentActorEntity> a = assignment.document.actors.iterator();
                while ( a.hasNext() ) {
                    DocumentActorEntity aa = a.next();
                    AddressEntity address = aa.address;
                    address.id = null;
                    EntityManagerHolder.getInstance().getEM().persist(address);

                    aa.id = null;
                    aa.document = assignment.document;    
                    EntityManagerHolder.getInstance().getEM().persist(aa);
                }
            }

            if ( null != assignment.leaseSet ) {
                Iterator<AssignmentTractEntity> i = assignment.leaseSet.iterator();
                while ( i.hasNext() ) {
                    AssignmentTractEntity b = i.next();
                    b.id = null;
                    b.assignment = assignment;    
                    EntityManagerHolder.getInstance().getEM().persist(b);
                }
            }
            
        } catch (Exception ex) {
            tr.rollback();
            throw ex;
        }
        
        tr.commit();
        
    }
*/
    private DocumentEntity createDocument(DocumentEntity document) 
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

/*
    private RecordEntity getRecord(Integer recordId) {
        RecordEntity result = (RecordEntity) EntityManagerHolder.getInstance().getEM().find(
                RecordEntity.class, recordId);
        return result;
    }

    public RecordEntity createRecord(RecordEntity record) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            createDocument(record.document);
            record.id = record.document.id;
            EntityManagerHolder.getInstance().getEM().persist(record);

            saveDocumentDetails(record.document, false);            
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.createLease: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.createLease: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getRecord(record.id);        
    }

    public RecordEntity storeRecord(RecordEntity record) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            record.id = record.document.id;
            saveDocumentDetails(record.document, true);

            EntityManagerHolder.getInstance().getEM().merge(record.document);
            EntityManagerHolder.getInstance().getEM().merge(record);
            
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.storeRecord: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.storeRecord: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getRecord(record.id);        
    }

    public void deleteRecord(Integer recordId) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            RecordEntity record = getRecord(recordId);
            EntityManagerHolder.getInstance().getEM().remove(record);
            EntityManagerHolder.getInstance().getEM().remove(record.document);
        } catch (Exception ex) {
            Logger.getInstance().getLog().error("DocumentService.deleteRecord: ", ex);
            tr.rollback();
            throw ex;
        }

        try {        
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("DocumentService.deleteRecord: commit problems.", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
    }
*/    

    public Collection<CoverageTractSetEntity> getLeaseTractCoverageSet(Integer leaseTractId) {
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select c from CoverageTractSetEntity as c where c.coverageTract.type=:type and c.externalId=:externalId");
        query.setParameter("type", DocumentService.LEASE_COVERAGE_TYPE);
        query.setParameter("externalId", leaseTractId);
        return query.getResultList();
    }

    public Collection<CoverageTractEntity> getTotalLeaseTractCoverage() {
/*
        Collection<CoverageTractSetEntity> coverageSet = EntityManagerHolder.getInstance().getEM().createQuery(
                "select c from CoverageTractSetEntity as c").getResultList();
        Iterator<CoverageTractSetEntity> icoverageSet = coverageSet.iterator();
        Map<Integer,Collection> coverageSetMap = new HashMap<Integer,Collection>();
        while ( icoverageSet.hasNext() ) {
            CoverageTractSetEntity cts = icoverageSet.next();
            Collection cset = coverageSetMap.get(cts.coverageTractId);
            if ( null == cset ) {
                cset = new ArrayList();
                coverageSetMap.put(cts.coverageTractId, cset);
            }
            cset.add(cts);
        }
*/
        Collection<CoverageTractEntity> coverage = EntityManagerHolder.getInstance().getEM().createQuery(
                "select c from CoverageTractEntity as c where c.type=0").getResultList();
/*
        Iterator<CoverageTractEntity> icoverage = coverage.iterator();
        while ( icoverage.hasNext() ) {
            CoverageTractEntity cov = icoverage.next();
            cov.coverageSet = coverageSetMap.get(cov.id);
        }
*/
        return coverage;
    }

    private DocumentEntity getDocument(Integer documentId) {
        DocumentEntity result = (DocumentEntity) EntityManagerHolder.getInstance().getEM().find(
                DocumentEntity.class, documentId);
        return result;
    }

    private void saveDocumentDetails(DocumentEntity document, boolean removeRequires) 
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
            Logger.getInstance().getLog().debug("DocumentService.saveDocumentDetails: attachments.count=" + document.attachments.size() + " obtained.");
            Iterator<DocumentAttachmentEntity> a = document.attachments.iterator();
            while ( a.hasNext() ) {
                DocumentAttachmentEntity aa = a.next();
                aa.id = null;
                aa.document = document;
                Logger.getInstance().getLog().debug("DocumentService.saveDocumentDetails: attachments.file.id=" + aa.file.id + " obtained.");
                aa.file = EntityManagerHolder.getInstance().getEM().find(FileEntity.class, aa.file.id);
                Logger.getInstance().getLog().debug("DocumentService.saveDocumentDetails: aa.file=" + aa.file);
                EntityManagerHolder.getInstance().getEM().persist(aa);
            }
        }
    }

    private void saveLeaseDetails(LeaseEntity lease, boolean removeRequires) 
        throws Exception
    {
        CoverageHelper coverageHelper = new CoverageHelper(DocumentService.LEASE_COVERAGE_TYPE);
        if ( removeRequires ) {
            LeaseEntity prevLease = getLease(lease.id);

            // alarms
            if ( null != prevLease.alarms ) {
                Iterator<LeaseAlarmEntity> ali = prevLease.alarms.iterator();
                while ( ali.hasNext() ) {
                    LeaseAlarmEntity ala = ali.next();
                    EntityManagerHolder.getInstance().getEM().remove(ala);
                }
            }

            // clauses
            Iterator<LeaseClauseEntity> cli = prevLease.clauses.iterator();
            while ( cli.hasNext() ) {
                LeaseClauseEntity cla = cli.next();
                EntityManagerHolder.getInstance().getEM().remove(cla);
            }

            // attachments
            Iterator<DocumentAttachmentEntity> dai = prevLease.document.attachments.iterator();
            while ( dai.hasNext() ) {
                DocumentAttachmentEntity da = dai.next();
                EntityManagerHolder.getInstance().getEM().remove(da);
            }

            // records
            Iterator<DocumentRecordEntity> drei = prevLease.document.records.iterator();
            while ( drei.hasNext() ) {
                DocumentRecordEntity record = drei.next();
                EntityManagerHolder.getInstance().getEM().remove(record);
            }

            // references
            Iterator<DocumentReferenceEntity> dri = prevLease.document.references.iterator();
            while ( dri.hasNext() ) {
                DocumentReferenceEntity dr = dri.next();
                EntityManagerHolder.getInstance().getEM().remove(dr);
            }

            // projects
            if ( null != prevLease.document.projects ) {
                Iterator<DocumentProjectEntity> dpi = prevLease.document.projects.iterator();
                while ( dpi.hasNext() ) {
                    DocumentProjectEntity dp = dpi.next();
                    EntityManagerHolder.getInstance().getEM().remove(dp);
                }
            }

            // tracts
            Iterator<LeaseTractEntity> i = prevLease.tracts.iterator();
            while ( i.hasNext() ) {
                LeaseTractEntity b = i.next();

                Iterator<LeaseBreakdownEntity> bi = b.breakdown.iterator();
                while ( bi.hasNext() ) {
                    LeaseBreakdownEntity lb = bi.next();
                    EntityManagerHolder.getInstance().getEM().remove(lb);
                }

                Iterator<LeaseTractQQEntity> ltqi = b.qqs.iterator();
                while ( ltqi.hasNext() ) {
                    LeaseTractQQEntity ltq = ltqi.next();
                    EntityManagerHolder.getInstance().getEM().remove(ltq);
                }

                boolean removeTract = true;
                Iterator<LeaseTractEntity> ni = lease.tracts.iterator();
                while ( ni.hasNext() ) {
                    LeaseTractEntity nb = ni.next();
                    if ( nb.id.equals(b.id) ) {
                        removeTract = false;
                        break;        
                    }
                }
                if ( removeTract ) {
                    coverageHelper.removeCoverage(b.id);
                    EntityManagerHolder.getInstance().getEM().flush();
                    EntityManagerHolder.getInstance().getEM().refresh(b);
                    EntityManagerHolder.getInstance().getEM().remove(b);
                } else {
                    EntityManagerHolder.getInstance().getEM().flush();
                    EntityManagerHolder.getInstance().getEM().refresh(b);
                }
            }

            // actors
            Iterator<DocumentActorEntity> a = prevLease.document.actors.iterator();
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

        }

        // clauses
        Iterator<LeaseClauseEntity> cli = lease.clauses.iterator();
        while ( cli.hasNext() ) {
            LeaseClauseEntity cla = cli.next();
            cla.id = null;
            cla.lease = lease;
            EntityManagerHolder.getInstance().getEM().persist(cla);
        }

        // alarms
        if ( null != lease.alarms ) {
            Iterator<LeaseAlarmEntity> ali = lease.alarms.iterator();
            while ( ali.hasNext() ) {
                LeaseAlarmEntity ala = ali.next();
                ala.id = null;
                ala.lease = lease;
                EntityManagerHolder.getInstance().getEM().persist(ala);
            }
        }

        // projects
        if ( null != lease.document.projects ) {
            Iterator<DocumentProjectEntity> dpi = lease.document.projects.iterator();
            while ( dpi.hasNext() ) {
                DocumentProjectEntity dp = dpi.next();
                dp.id = null;
                EntityManagerHolder.getInstance().getEM().persist(dp);
            }
        }

        // references
        if ( null != lease.document.references ) {
            Iterator<DocumentReferenceEntity> dri = lease.document.references.iterator();
            while ( dri.hasNext() ) {
                DocumentReferenceEntity dr = dri.next();
                dr.id = null;
                dr.referrer = lease.document;
                EntityManagerHolder.getInstance().getEM().persist(dr);
/*
               if ( null != dr.attachments ) {
                   Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: record.attachments.count=" + dr.attachments.size() + " obtained.");
                   Iterator<DocumentRecordAttachmentEntity> drai = dr.attachments.iterator();
                   while ( drai.hasNext() ) {
                       DocumentRecordAttachmentEntity dra = drai.next();
                       dra.id = null;
                       dra.record = dr;
                       Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: record.attachment.file.id=" + dra.file.id + " obtained.");
                       dra.file = EntityManagerHolder.getInstance().getEM().find(FileEntity.class, dra.file.id);
                       Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: record.attahcment dra.file=" + dra.file);
                       EntityManagerHolder.getInstance().getEM().persist(dra);
                   }
               }
*/
            }
        }

        // actors
        if ( null != lease.document.actors ) {
            Iterator<DocumentActorEntity> a = lease.document.actors.iterator();
            while ( a.hasNext() ) {
                DocumentActorEntity aa = a.next();
                AddressEntity address = aa.address;
                address.id = null;
                EntityManagerHolder.getInstance().getEM().persist(address);

                aa.id = null;
                aa.document = lease.document;    
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
        
        // tracts
        if ( null != lease.tracts ) {
            Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: " + lease.tracts.size() + " obtained.");
            Iterator<LeaseTractEntity> i = lease.tracts.iterator();
            while ( i.hasNext() ) {
                LeaseTractEntity b = i.next();
                b.lease = lease;

                boolean isMerge = false;
                if ( 0 == b.id ) {
                    b.id = null;
                    EntityManagerHolder.getInstance().getEM().persist(b);
                } else {
                    isMerge = true;
                }

                Iterator<LeaseBreakdownEntity> bi = b.breakdown.iterator();
                while ( bi.hasNext() ) {
                    LeaseBreakdownEntity lb = bi.next();
                    lb.id = null;
                    lb.tract = b;
                    EntityManagerHolder.getInstance().getEM().persist(lb);
                }

                Iterator<LeaseTractQQEntity> ltqi = b.qqs.iterator();
                String tractIdList = "";
                while ( ltqi.hasNext() ) {
                    LeaseTractQQEntity ltq = ltqi.next();
                    ltq.id = null;
                    ltq.leaseTract = b;

                    tractIdList += ltq.tract.id.toString();
                    if ( ltqi.hasNext() ) {
                        tractIdList += ",";
                    }

                    EntityManagerHolder.getInstance().getEM().persist(ltq);
                }

                if ( isMerge ) {
                    EntityManagerHolder.getInstance().getEM().merge(b);
                }

                if ( b.tractDescriptionChanged.booleanValue() || true ) {
                    // insert new shape into geo_geometry
                    Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(
                        "select nextval('GEO_GEOMETRY_SQC')");
                    java.math.BigInteger geometryId = (java.math.BigInteger) query.getSingleResult();
                    Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: geometryId=" + geometryId);

                    String sqlQuery = null;
                    if ( null != b.qqs && 0 != b.qqs.size() ) {
                        sqlQuery = "insert into GEO_GEOMETRY (id, ref_id, status, the_geom) " +
                            " select " + geometryId + " as id, " + b.id + " as ref_id, -1 as status, multi(geomunion(a.the_geom)) " +
                            " from ( select (dump(g.THE_GEOM)).geom as the_geom " +
                            "        from GEO_TRACT gt " +
                            "           inner join GEO_GEOMETRY g on gt.geometry_id=g.id " +
                            "        where gt.id in (" + tractIdList + ") " +
                            " ) a group by id, ref_id, status ";
                    } else {
                        sqlQuery = "insert into GEO_GEOMETRY (id, ref_id, status, the_geom) " +
                            " values ( " + geometryId + ", " + b.id + ", -1, null) ";
                    }
                    
                    query = EntityManagerHolder.getInstance().getEM().createNativeQuery(
                        sqlQuery);
                    Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: sqlQuery=" + sqlQuery);
                    query.executeUpdate();

                    LeaseTractHistoryEntity hist = new LeaseTractHistoryEntity();
                    hist.tract = b;
                    hist.geometryId = new Integer(geometryId.intValue());
                    hist.userId = lease.document.user.id;
                    hist.userName = lease.document.user.login;
                    hist.created = new Date();
                    hist.note = "Generated from geo_tracts: " + tractIdList;
                    EntityManagerHolder.getInstance().getEM().persist(hist);

                    // handle coverage layer
                    EntityManagerHolder.getInstance().getEM().flush();
                    coverageHelper.removeCoverage(b.id);
                    coverageHelper.generateCoverage(b.id, hist.geometryId, null);
                }
            }
        }

        // records
        if ( null != lease.document.records ) {
            Iterator<DocumentRecordEntity> drei = lease.document.records.iterator();
            while ( drei.hasNext() ) {
                DocumentRecordEntity record = drei.next();
                record.id = null;
                record.document = lease.document;
                EntityManagerHolder.getInstance().getEM().persist(record);
                Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: record.id=" + record.id);
            }
        }

        // attachments
        if ( null != lease.document.attachments ) {
            Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: attachments.count=" + lease.document.attachments.size() + " obtained.");
            Iterator<DocumentAttachmentEntity> a = lease.document.attachments.iterator();
            while ( a.hasNext() ) {
                DocumentAttachmentEntity aa = a.next();
                aa.id = null;
                aa.document = lease.document;
                if ( null != aa.record ) {
                    Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: aa.record.id=" + aa.record.id);
                } else {
                    Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: aa.record is null.");
                }   
                if ( null != aa.file ) {
                    Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: attachments.file.id=" + aa.file.id + " obtained.");
                    aa.file = EntityManagerHolder.getInstance().getEM().find(FileEntity.class, aa.file.id);
                    Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: aa.file=" + aa.file);
                } else {
                    Logger.getInstance().getLog().debug("DocumentService.saveLeaseDetails: attachment without file is received.");
                }
                EntityManagerHolder.getInstance().getEM().persist(aa);
            }
        }
    }

    private DocumentTypeEntity getLeaseDocumentType() {
        return (DocumentTypeEntity) EntityManagerHolder.getInstance().getEM().find(
                DocumentTypeEntity.class, new Integer(1));    
    }

/* to do: fix or remove
    private DocumentTypeEntity getLeaseAssignmentDocumentType() {
        return (DocumentTypeEntity) EntityManagerHolder.getInstance().getEM().find(
                DocumentTypeEntity.class, new Integer(2));    
    }
*/
    private DocumentStatusEntity getUnrecordedDocumentStatus() {
        return (DocumentStatusEntity) EntityManagerHolder.getInstance().getEM().find(
                DocumentStatusEntity.class, new Integer(2));    
    }

    private Query createSearchQuery(LeaseSearchCriterias criterias, boolean isNames) {
        String columns = null;
        if ( isNames ) {
            columns = "o.leaseName";
        } else {
            columns = "o";
        }

        String sqlQuery = "select " + columns + " from LeaseEntity as o ";
        String whereClause = "where o.document.user.client.id=:clientId";

        if ( null != criterias ) {
            if ( null != criterias.leaseNo ) {
                whereClause += " and o.leaseNum like :leaseNo ";
            }
            if ( null != criterias.projectId ) {
                whereClause += " and exists (select dp from DocumentProjectEntity as dp where dp.document.id=o.document.id and dp.project.id = :projectId) ";
            }
            if ( null != criterias.leaseName ) {
                whereClause += " and o.leaseName like :leaseName ";
            }
            if ( null != criterias.recordInfo ) {
                whereClause += " and exists (select r from RecordEntity as r where r.document=o.document and (r.docNo like :recordInfo or r.volume like :recordInfo or r.page like :recordInfo or r.county.name like :recordInfo)) ";
            }
            if ( null != criterias.expDate ) {
                if ( null != criterias.expDate.dateFrom ) {
                    whereClause += " and DOC_LEASE_EXP_DATE(o.effectiveDate, o.term) >= :expDateMin ";
                }
                if ( null != criterias.expDate.dateTo ) {
                    whereClause += " and DOC_LEASE_EXP_DATE(o.effectiveDate, o.term) <= :expDateMax ";
                }
            }
            if ( null != criterias.grossAc ) {
                if ( null != criterias.grossAc.min ) {
                    whereClause += " and (select sum(lt.grossAcres) from LeaseTractEntity as lt where lt.lease=o) >= :grossAcMin ";
                }
                if ( null != criterias.grossAc.max ) {
                    whereClause += " and (select sum(lt.grossAcres) from LeaseTractEntity as lt where lt.lease=o) <= :grossAcMax ";
                }
            }
            if ( null != criterias.netAc ) {
                if ( null != criterias.netAc.min ) {
                    whereClause += " and (select sum(lt.netAcres) from LeaseTractEntity as lt where lt.lease=o) >= :netAcMin ";
                }
                if ( null != criterias.netAc.max ) {
                    whereClause += " and (select sum(lt.netAcres) from LeaseTractEntity as lt where lt.lease=o) <= :netAcMax ";
                }
            }
            if ( null != criterias.interest ) {
                if ( null != criterias.interest.min ) {
                    whereClause += " and (select case when sum(lt.grossAcres) != 0 then (sum(lt.netAcres)*100/sum(lt.grossAcres)) else 0.00 end from LeaseTractEntity as lt where lt.lease=o) >= :interestMin ";
                }
                if ( null != criterias.interest.max ) {
                    whereClause += " and (select case when sum(lt.grossAcres) != 0 then (sum(lt.netAcres)*100/sum(lt.grossAcres)) else 0.00 end from LeaseTractEntity as lt where lt.lease=o) <= :interestMax ";
                }
            }
            if ( null != criterias.leaseBurden ) {
                if ( null != criterias.leaseBurden.min ) {
                    whereClause += " and (select (sum(lt.leaseBurden)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) >= :leaseBurdenMin ";
                }
                if ( null != criterias.leaseBurden.max ) {
                    whereClause += " and (select (sum(lt.leaseBurden)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) <= :leaseBurdenMax ";
                }
            }
            if ( null != criterias.leaseNri ) {
                if ( null != criterias.leaseNri.min ) {
                    whereClause += " and (select (sum(lt.nri)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) >= :leaseNriMin ";
                }
                if ( null != criterias.leaseNri.max ) {
                    whereClause += " and (select (sum(lt.nri)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) <= :leaseNriMax ";
                }
            }
            if ( null != criterias.wi ) {
                if ( null != criterias.wi.min ) {
                    whereClause += " and (select (sum(lt.cwi)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) >= :wiMin ";
                }
                if ( null != criterias.wi.max ) {
                    whereClause += " and (select (sum(lt.cwi)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) <= :wiMax ";
                }
            }
            if ( null != criterias.additionalBurden ) {
                if ( null != criterias.additionalBurden.min ) {
                    whereClause += " and (select (sum(lt.burden)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) >= :additionalBurdenMin ";
                }
                if ( null != criterias.additionalBurden.max ) {
                    whereClause += " and (select (sum(lt.burden)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) <= :additionalBurdenMax ";
                }
            }
            if ( null != criterias.nri ) {
                if ( null != criterias.nri.min ) {
                    whereClause += " and (select (sum(lt.cnri)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) >= :nriMin ";
                }
                if ( null != criterias.nri.max ) {
                    whereClause += " and (select (sum(lt.cnri)*100/count(*)) from LeaseTractEntity as lt where lt.lease=o) <= :nriMax ";
                }
            }
            if ( null != criterias.net ) {
                if ( null != criterias.net.min ) {
                    whereClause += " and (select sum(lt.cNetAcres) from LeaseTractEntity as lt where lt.lease=o) >= :netMin ";
                }
                if ( null != criterias.net.max ) {
                    whereClause += " and (select sum(lt.cNetAcres) from LeaseTractEntity as lt where lt.lease=o) <= :netMax ";
                }
            }
            if ( null != criterias.tracts ) {
                Iterator<TractSearchCriteria> i = criterias.tracts.iterator();
                int index = 0;
                while ( i.hasNext() ) {
                    TractSearchCriteria tsc = i.next();
                    if ( 0 != index ) {
                        whereClause += " or ";
                    } else {
                        whereClause += " and ( ";
                    }
                    
                    whereClause += " exists (select lt from LeaseTractEntity as lt left outer join lt.state as st left outer join lt.county as ct where lt.lease=o ";
                    if ( null != tsc.stateId ) {
                        whereClause += " and st.id = :state" + index + " ";
                    }
                    if ( null != tsc.countyId ) {
                        whereClause += " and ct.id = :county" + index + " ";
                    }
                    if ( null != tsc.township ) {
                        whereClause += " and lt.township like :township" + index + " ";
                    }
                    if ( null != tsc.range ) {
                        whereClause += " and lt.range like :range" + index + " ";
                    }
                    if ( null != tsc.section ) {
                        whereClause += " and lt.section like :section" + index + " ";
                    }
                    whereClause += " ) ";

                    if ( !i.hasNext() ){
                        whereClause += " ) ";
                    }
                    index ++;
                }
            }
        }            
        sqlQuery += whereClause + " order by o.leaseName";

        System.out.println("sqlQuery=" + sqlQuery);
        Query query  = EntityManagerHolder.getInstance().getEM().createQuery(sqlQuery);
        query.setParameter("clientId", Service.getAuthorizedUser().client.id);

        if ( null != criterias ) {
            if ( null != criterias.leaseNo ) {
                query.setParameter("leaseNo", criterias.leaseNo);
            }
            if ( null != criterias.projectId ) {
                query.setParameter("projectId", criterias.projectId);
            }
            if ( null != criterias.leaseName ) {
                query.setParameter("leaseName", criterias.leaseName);
            }
            if ( null != criterias.recordInfo ) {
                query.setParameter("recordInfo", criterias.recordInfo);
            }
            if ( null != criterias.expDate ) {
                if ( null != criterias.expDate.dateFrom ) {
                    query.setParameter("expDateMin", criterias.expDate.dateFrom);
                }
                if ( null != criterias.expDate.dateTo ) {
                    query.setParameter("expDateMax", criterias.expDate.dateTo);
                }
            }
            if ( null != criterias.grossAc ) {
                if ( null != criterias.grossAc.min ) {
                    query.setParameter("grossAcMin", criterias.grossAc.min);
                }
                if ( null != criterias.grossAc.max ) {
                    query.setParameter("grossAcMax", criterias.grossAc.max);
                }
            }
            if ( null != criterias.netAc ) {
                if ( null != criterias.netAc.min ) {
                    query.setParameter("netAcMin", criterias.netAc.min);
                }
                if ( null != criterias.netAc.max ) {
                    query.setParameter("netAcMax", criterias.netAc.max);
                }
            }
            if ( null != criterias.interest ) {
                if ( null != criterias.interest.min ) {
                    query.setParameter("interestMin", criterias.interest.min);
                }
                if ( null != criterias.interest.max ) {
                    query.setParameter("interestMax", criterias.interest.max);
                }
            }
            if ( null != criterias.leaseBurden ) {
                if ( null != criterias.leaseBurden.min ) {
                    query.setParameter("leaseBurdenMin", criterias.leaseBurden.min);
                }
                if ( null != criterias.leaseBurden.max ) {
                    query.setParameter("leaseBurdenMax", criterias.leaseBurden.max);
                }
            }
            if ( null != criterias.leaseNri ) {
                if ( null != criterias.leaseNri.min ) {
                    query.setParameter("leaseNriMin", criterias.leaseNri.min);
                }
                if ( null != criterias.leaseNri.max ) {
                    query.setParameter("leaseNriMax", criterias.leaseNri.max);
                }
            }
            if ( null != criterias.wi ) {
                if ( null != criterias.wi.min ) {
                    query.setParameter("wiMin", criterias.wi.min);
                }
                if ( null != criterias.wi.max ) {
                    query.setParameter("wiMax", criterias.wi.max);
                }
            }
            if ( null != criterias.additionalBurden ) {
                if ( null != criterias.additionalBurden.min ) {
                    query.setParameter("additionalBurdenMin", criterias.additionalBurden.min);
                }
                if ( null != criterias.additionalBurden.max ) {
                    query.setParameter("additionalBurdenMax", criterias.additionalBurden.max);
                }
            }
            if ( null != criterias.nri ) {
                if ( null != criterias.nri.min ) {
                    query.setParameter("nriMin", criterias.nri.min);
                }
                if ( null != criterias.nri.max ) {
                    query.setParameter("nriMax", criterias.nri.max);
                }
            }
            if ( null != criterias.net ) {
                if ( null != criterias.net.min ) {
                    query.setParameter("netMin", criterias.net.min);
                }
                if ( null != criterias.net.max ) {
                    query.setParameter("netMax", criterias.net.max);
                }
            }
            if ( null != criterias.tracts ) {
                Iterator<TractSearchCriteria> i = criterias.tracts.iterator();
                int index = 0;
                while ( i.hasNext() ) {
                    TractSearchCriteria tsc = i.next();
                    if ( null != tsc.stateId ) {
                        query.setParameter("state"+index, tsc.stateId);
                    }
                    if ( null != tsc.countyId ) {
                        query.setParameter("county"+index, tsc.countyId);
                    }
                    if ( null != tsc.township ) {
                        query.setParameter("township"+index, tsc.township);
                    }
                    if ( null != tsc.range ) {
                        query.setParameter("range"+index, tsc.range);
                    }
                    if ( null != tsc.section ) {
                        query.setParameter("section"+index, tsc.section);
                    }
                    index ++;
                }
            }
        }

        return query;
    }

    private Query createSummarySearchQuery(LeaseSearchCriterias criterias) {
        String columns = "sum((select sum(lt.GROSS_ACRES) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID)) as grossAc, ";
        columns += "sum((select sum(lt.NET_ACRES) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID)) as netAc, ";
        columns += "case when sum((select sum(lt.GROSS_ACRES) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID)) != 0 then ";
        columns += " sum((select sum(lt.NET_ACRES) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID))*100/";
        columns += " sum((select sum(lt.GROSS_ACRES) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID)) ";
        columns += "else 0.00 end as interest, ";
        columns += "sum((select sum(lt.LEASE_BURDEN)*100/count(*) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID))/count(*) as leaseBurden, ";
        columns += "sum((select sum(lt.NRI)*100/count(*) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID))/count(*) as leaseNri, ";
        columns += "sum((select sum(lt.CWI)*100/count(*) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID))/count(*) as wi, ";
        columns += "sum((select sum(lt.BURDEN)*100/count(*) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID))/count(*) as additionalBurden, ";
        columns += "sum((select sum(lt.CNRI)*100/count(*) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID))/count(*) as nri, ";
        columns += "sum((select sum(lt.C_NET_ACRES) from DOC_LEASE_TRACT lt where lt.LEASE_ID=l.ID)) as net ";

        String sqlQuery = "select " + columns + " from DOC_LEASE l ";
        sqlQuery += " inner join DOC_DOCUMENT d on l.ID=d.ID ";
        sqlQuery += " inner join SYS_USER u on d.USER_ID=u.ID ";

        String whereClause = "where u.CLIENT_ID=:clientId";
        if ( null != criterias ) {
            if ( null != criterias.leaseNo ) {
                whereClause += " and l.LEASE_NUM like :leaseNo ";
            }
            if ( null != criterias.projectId ) {
                whereClause += " and exists (select dp.ID from DOC_PROJECT as dp where dp.DOC_ID=l.ID and dp.PROJECT_ID = :projectId) ";
            }
            if ( null != criterias.leaseName ) {
                whereClause += " and l.LEASE_NAME like :leaseName ";
            }
            if ( null != criterias.recordInfo ) {
                whereClause += " and exists (select r.ID from DOC_RECORD as r inner join GEO_COUNTY gc on r.COUNTY_ID=gc.ID where r.DOC_ID=l.ID and (r.DOC_NO like :recordInfo or r.VOLUME like :recordInfo or r.PAGE like :recordInfo or gc.NAME like :recordInfo)) ";
            }
            if ( null != criterias.expDate ) {
                if ( null != criterias.expDate.dateFrom ) {
                    whereClause += " and DOC_LEASE_EXP_DATE(l.effectiveDate, l.term) >= :expDateMin ";
                }
                if ( null != criterias.expDate.dateTo ) {
                    whereClause += " and DOC_LEASE_EXP_DATE(l.effectiveDate, l.term) <= :expDateMax ";
                }
            }
            if ( null != criterias.grossAc ) {
                if ( null != criterias.grossAc.min ) {
                    whereClause += " and (select sum(lt.GROSS_ACRES) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) >= :grossAcMin ";
                }
                if ( null != criterias.grossAc.max ) {
                    whereClause += " and (select sum(lt.GROSS_ACRES) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) <= :grossAcMax ";
                }
            }
            if ( null != criterias.netAc ) {
                if ( null != criterias.netAc.min ) {
                    whereClause += " and (select sum(lt.NET_ACRES) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) >= :netAcMin ";
                }
                if ( null != criterias.netAc.max ) {
                    whereClause += " and (select sum(lt.NET_ACRES) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) <= :netAcMax ";
                }
            }
            if ( null != criterias.interest ) {
                if ( null != criterias.interest.min ) {
                    whereClause += " and (select case when sum(GROSS_ACRES)!=0 then sum(lt.NET_ACRES)*100/sum(GROSS_ACRES) else 0.00 end from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) >= :interestMin ";
                }
                if ( null != criterias.interest.max ) {
                    whereClause += " and (select case when sum(GROSS_ACRES)!=0 then sum(lt.NET_ACRES)*100/sum(GROSS_ACRES) else 0.00 end from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) <= :interestMax ";
                }
            }
            if ( null != criterias.leaseBurden ) {
                if ( null != criterias.leaseBurden.min ) {
                    whereClause += " and (select sum(lt.LEASE_BURDEN)*100/count(*) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) >= :leaseBurdenMin ";
                }
                if ( null != criterias.leaseBurden.max ) {
                    whereClause += " and (select sum(lt.LEASE_BURDEN)*100/count(*) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID) <= :leaseBurdenMax ";
                }
            }
            if ( null != criterias.leaseNri ) {
                if ( null != criterias.leaseNri.min ) {
                    whereClause += " and (select (sum(lt.NRI)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) >= :leaseNriMin ";
                }
                if ( null != criterias.leaseNri.max ) {
                    whereClause += " and (select (sum(lt.NRI)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) <= :leaseNriMax ";
                }
            }
            if ( null != criterias.wi ) {
                if ( null != criterias.wi.min ) {
                    whereClause += " and (select (sum(lt.CWI)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) >= :wiMin ";
                }
                if ( null != criterias.wi.max ) {
                    whereClause += " and (select (sum(lt.CWI)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) <= :wiMax ";
                }
            }
            if ( null != criterias.additionalBurden ) {
                if ( null != criterias.additionalBurden.min ) {
                    whereClause += " and (select (sum(lt.BURDEN)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) >= :additionalBurdenMin ";
                }
                if ( null != criterias.additionalBurden.max ) {
                    whereClause += " and (select (sum(lt.BURDEN)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) <= :additionalBurdenMax ";
                }
            }
            if ( null != criterias.nri ) {
                if ( null != criterias.nri.min ) {
                    whereClause += " and (select (sum(lt.CNRI)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) >= :nriMin ";
                }
                if ( null != criterias.nri.max ) {
                    whereClause += " and (select (sum(lt.CNRI)*100/count(*)) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) <= :nriMax ";
                }
            }
            if ( null != criterias.net ) {
                if ( null != criterias.net.min ) {
                    whereClause += " and (select sum(lt.C_NET_ACRES) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) >= :netMin ";
                }
                if ( null != criterias.net.max ) {
                    whereClause += " and (select sum(lt.C_NET_ACRES) from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.id) <= :netMax ";
                }
            }
            if ( null != criterias.tracts ) {
                Iterator<TractSearchCriteria> i = criterias.tracts.iterator();
                int index = 0;
                while ( i.hasNext() ) {
                    TractSearchCriteria tsc = i.next();
                    if ( 0 != index ) {
                        whereClause += " or ";
                    } else {
                        whereClause += " and ( ";
                    }
                    
                    whereClause += " exists (select lt.ID from DOC_LEASE_TRACT as lt where lt.LEASE_ID=l.ID ";
                    if ( null != tsc.stateId ) {
                        whereClause += " and lt.STATE_ID is not null and lt.STATE_ID = :state" + index + " ";
                    }
                    if ( null != tsc.countyId ) {
                        whereClause += " and lt.COUNTY_ID is not null and lt.COUNTY_ID = :county" + index + " ";
                    }
                    if ( null != tsc.township ) {
                        whereClause += " and lt.TOWNSHIP like :township" + index + " ";
                    }
                    if ( null != tsc.range ) {
                        whereClause += " and lt.RANGE like :range" + index + " ";
                    }
                    if ( null != tsc.section ) {
                        whereClause += " and lt.SECTION like :section" + index + " ";
                    }
                    whereClause += " ) ";

                    if ( !i.hasNext() ){
                        whereClause += " ) ";
                    }
                    index ++;
                }
            }
        }            

        sqlQuery += whereClause;
        System.out.println("sqlQuery=" + sqlQuery);
        Query query  = EntityManagerHolder.getInstance().getEM().createNativeQuery(sqlQuery);
        query.setParameter("clientId", Service.getAuthorizedUser().client.id);

        if ( null != criterias ) {
            if ( null != criterias.leaseNo ) {
                query.setParameter("leaseNo", criterias.leaseNo);
            }
            if ( null != criterias.projectId ) {
                query.setParameter("projectId", criterias.projectId);
            }
            if ( null != criterias.leaseName ) {
                query.setParameter("leaseName", criterias.leaseName);
            }
            if ( null != criterias.recordInfo ) {
                query.setParameter("recordInfo", criterias.recordInfo);
            }
            if ( null != criterias.expDate ) {
                if ( null != criterias.expDate.dateFrom ) {
                    query.setParameter("expDateMin", criterias.expDate.dateFrom);
                }
                if ( null != criterias.expDate.dateTo ) {
                    query.setParameter("expDateMax", criterias.expDate.dateTo);
                }
            }
            if ( null != criterias.grossAc ) {
                if ( null != criterias.grossAc.min ) {
                    query.setParameter("grossAcMin", criterias.grossAc.min);
                }
                if ( null != criterias.grossAc.max ) {
                    query.setParameter("grossAcMax", criterias.grossAc.max);
                }
            }
            if ( null != criterias.netAc ) {
                if ( null != criterias.netAc.min ) {
                    query.setParameter("netAcMin", criterias.netAc.min);
                }
                if ( null != criterias.netAc.max ) {
                    query.setParameter("netAcMax", criterias.netAc.max);
                }
            }
            if ( null != criterias.interest ) {
                if ( null != criterias.interest.min ) {
                    query.setParameter("interestMin", criterias.interest.min);
                }
                if ( null != criterias.interest.max ) {
                    query.setParameter("interestMax", criterias.interest.max);
                }
            }
            if ( null != criterias.leaseBurden ) {
                if ( null != criterias.leaseBurden.min ) {
                    query.setParameter("leaseBurdenMin", criterias.leaseBurden.min);
                }
                if ( null != criterias.leaseBurden.max ) {
                    query.setParameter("leaseBurdenMax", criterias.leaseBurden.max);
                }
            }
            if ( null != criterias.leaseNri ) {
                if ( null != criterias.leaseNri.min ) {
                    query.setParameter("leaseNriMin", criterias.leaseNri.min);
                }
                if ( null != criterias.leaseNri.max ) {
                    query.setParameter("leaseNriMax", criterias.leaseNri.max);
                }
            }
            if ( null != criterias.wi ) {
                if ( null != criterias.wi.min ) {
                    query.setParameter("wiMin", criterias.wi.min);
                }
                if ( null != criterias.wi.max ) {
                    query.setParameter("wiMax", criterias.wi.max);
                }
            }
            if ( null != criterias.additionalBurden ) {
                if ( null != criterias.additionalBurden.min ) {
                    query.setParameter("additionalBurdenMin", criterias.additionalBurden.min);
                }
                if ( null != criterias.additionalBurden.max ) {
                    query.setParameter("additionalBurdenMax", criterias.additionalBurden.max);
                }
            }
            if ( null != criterias.nri ) {
                if ( null != criterias.nri.min ) {
                    query.setParameter("nriMin", criterias.nri.min);
                }
                if ( null != criterias.nri.max ) {
                    query.setParameter("nriMax", criterias.nri.max);
                }
            }
            if ( null != criterias.net ) {
                if ( null != criterias.net.min ) {
                    query.setParameter("netMin", criterias.net.min);
                }
                if ( null != criterias.net.max ) {
                    query.setParameter("netMax", criterias.net.max);
                }
            }
            if ( null != criterias.tracts ) {
                Iterator<TractSearchCriteria> i = criterias.tracts.iterator();
                int index = 0;
                while ( i.hasNext() ) {
                    TractSearchCriteria tsc = i.next();
                    if ( null != tsc.stateId ) {
                        query.setParameter("state"+index, tsc.stateId);
                    }
                    if ( null != tsc.countyId ) {
                        query.setParameter("county"+index, tsc.countyId);
                    }
                    if ( null != tsc.township ) {
                        query.setParameter("township"+index, tsc.township);
                    }
                    if ( null != tsc.range ) {
                        query.setParameter("range"+index, tsc.range);
                    }
                    if ( null != tsc.section ) {
                        query.setParameter("section"+index, tsc.section);
                    }
                    index ++;
                }
            }
        }
        return query;
    }

}
