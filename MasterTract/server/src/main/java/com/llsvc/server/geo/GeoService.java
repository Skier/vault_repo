package com.llsvc.server.geo;

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

import com.llsvc.server.entity.UserEntity;
import com.llsvc.server.entity.LayerEntity;

public class GeoService
{

    public LayerEntity getLayerByName(String layerName) {
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select o from LayerEntity as o where o.name = :layerName");
        query.setParameter("layerName", layerName);
        return (LayerEntity) query.getSingleResult();
    }

    public List<SRS> getSRSes() {
        Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(
                "select srid, auth_name, auth_srid from spatial_ref_sys order by srid");
        Iterator i = query.getResultList().iterator();
        List<SRS> result = new ArrayList<SRS>();
        while ( i.hasNext() ) {
            Object[] row = (Object[]) i.next();
            SRS srs = new SRS();
            srs.srid = row[0].toString();
            srs.name = row[1].toString() + ":" + row[2].toString();;
            result.add(srs);
        }
        return result;
    }

    public List<LayerEntity> getLayers() {
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select o from LayerEntity as o order by o.name");
        return query.getResultList();
    }

    public LayerEntity saveLayer(LayerEntity layer) {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            if ( 0 == layer.id ) {
                layer.id = null;
                EntityManagerHolder.getInstance().getEM().persist(layer);
            } else {
                EntityManagerHolder.getInstance().getEM().merge(layer);
            }
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("GeoService.saveLayer: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("GeoService.saveLayer: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return (LayerEntity) EntityManagerHolder.getInstance().getEM().find(LayerEntity.class, layer.id);        
    }

    public void removeLayer(LayerEntity layer) {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            EntityManagerHolder.getInstance().getEM().remove(layer);
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("GeoService.removeLayer: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("GeoService.removeLayer: commit failed ", ex);
            throw new RuntimeException(ex);
        }
    }

}
