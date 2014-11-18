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

import com.llsvc.server.entity.DocumentEntity;
import com.llsvc.server.entity.SurfaceTractEntity;
import com.llsvc.server.entity.SurfaceOwnerEntity;
import com.llsvc.server.entity.SurfaceRunsheetEntity;

public class SurfaceDocumentService
    extends BaseDocumentService
{
    public List<SurfaceTractEntity> getSurfaceTracts() {
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select o from SurfaceTractEntity as o where o.document.user.client.id=:clientId order by o.no");
        query.setParameter("clientId", Service.getAuthorizedUser().client.id);
        return query.getResultList();
    }

    public SurfaceTractEntity getSurfaceTract(Integer surfaceTractId) {
        SurfaceTractEntity result = (SurfaceTractEntity) EntityManagerHolder.getInstance().getEM().find(
                SurfaceTractEntity.class, surfaceTractId);
        return result;
    }

    public SurfaceTractEntity createSurfaceTract(SurfaceTractEntity surfaceTract) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            surfaceTract.document.id = null;
            createDocument(surfaceTract.document);

            surfaceTract.id = surfaceTract.document.id;
            EntityManagerHolder.getInstance().getEM().persist(surfaceTract);

            saveDocumentDetails(surfaceTract.document, false);            
            saveSurfaceTractDetails(surfaceTract, false);
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.createSurfaceTract: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.createSurfaceTract: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getSurfaceTract(surfaceTract.id);        
    }

    public SurfaceTractEntity storeSurfaceTract(SurfaceTractEntity surfaceTract) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            surfaceTract.id = surfaceTract.document.id;
            saveDocumentDetails(surfaceTract.document, true);
            saveSurfaceTractDetails(surfaceTract, true);

            EntityManagerHolder.getInstance().getEM().merge(surfaceTract.document);
            EntityManagerHolder.getInstance().getEM().merge(surfaceTract);
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.storeSurfaceTract: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.storeSurfaceTract: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getSurfaceTract(surfaceTract.id);        
    }

    public void deleteSurfaceTract(Integer surfaceTractId) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            SurfaceTractEntity surfaceTract = getSurfaceTract(surfaceTractId);
            EntityManagerHolder.getInstance().getEM().remove(surfaceTract);
            EntityManagerHolder.getInstance().getEM().remove(surfaceTract.document);
        } catch (Exception ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.deleteSurfaceTract: ", ex);
            tr.rollback();
            throw ex;
        }

        try {        
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.deleteSurfaceTract: commit problem.", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
    }

    protected void saveSurfaceTractDetails(SurfaceTractEntity surfaceTract, boolean removeRequires) 
        throws Exception
    {
        if ( removeRequires ) {
            SurfaceTractEntity prevST = getSurfaceTract(surfaceTract.id);
            Iterator<SurfaceOwnerEntity> stoi = prevST.owners.iterator();
            while ( stoi.hasNext() ) {
                SurfaceOwnerEntity sto = stoi.next();
                EntityManagerHolder.getInstance().getEM().remove(sto);
            }
        }  // end of removeRequires

        Iterator<SurfaceOwnerEntity> stoi = surfaceTract.owners.iterator();
        while ( stoi.hasNext() ) {
            SurfaceOwnerEntity sto = stoi.next();
            EntityManagerHolder.getInstance().getEM().persist(sto);
        }
    }

    public SurfaceRunsheetEntity getSurfaceRunsheet(Integer surfaceRunsheetId) {
        SurfaceRunsheetEntity result = (SurfaceRunsheetEntity) EntityManagerHolder.getInstance().getEM().find(
                SurfaceRunsheetEntity.class, surfaceRunsheetId);
        return result;
    }

    public SurfaceRunsheetEntity createSurfaceRunsheet(SurfaceRunsheetEntity surfaceRunsheet) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            surfaceRunsheet.document.id = null;
            createDocument(surfaceRunsheet.document);

            surfaceRunsheet.id = surfaceRunsheet.document.id;
            EntityManagerHolder.getInstance().getEM().persist(surfaceRunsheet);

            saveDocumentDetails(surfaceRunsheet.document, false);            
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.createSurfaceRunsheet: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.createSurfaceRunsheet: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getSurfaceRunsheet(surfaceRunsheet.id);        
    }

    public SurfaceRunsheetEntity storeSurfaceRunsheet(SurfaceRunsheetEntity surfaceRunsheet) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            surfaceRunsheet.id = surfaceRunsheet.document.id;
            saveDocumentDetails(surfaceRunsheet.document, true);

            EntityManagerHolder.getInstance().getEM().merge(surfaceRunsheet.document);
            EntityManagerHolder.getInstance().getEM().merge(surfaceRunsheet);
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.storeSurfaceRunsheet: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.storeSurfaceRunsheet: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return getSurfaceRunsheet(surfaceRunsheet.id);        
    }

    public void deleteSurfaceRunsheet(Integer surfaceRunsheetId) 
        throws Exception
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            SurfaceRunsheetEntity surfaceRunsheet = getSurfaceRunsheet(surfaceRunsheetId);
            EntityManagerHolder.getInstance().getEM().remove(surfaceRunsheet);
            EntityManagerHolder.getInstance().getEM().remove(surfaceRunsheet.document);
        } catch (Exception ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.deleteSurfaceRunsheet: ", ex);
            tr.rollback();
            throw ex;
        }

        try {        
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("SurfaceDocumentService.deleteSurfaceRunsheet: commit problem.", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
    }

}
