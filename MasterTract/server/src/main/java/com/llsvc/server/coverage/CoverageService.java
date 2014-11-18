package com.llsvc.server.coverage;

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
import com.llsvc.server.entity.CoverageTractEntity;

public class CoverageService
{

    public List<CoverageTractEntity> findCoverageTracts(CoverageTractEntity mask) {
        String ejbqlQuery = "select o from CoverageTractEntity as o where o.type=:type ";
        if ( 0 != mask.id ) {
            ejbqlQuery += " and o.id=:id ";
        } else {
            if ( null != mask.township ) {
                ejbqlQuery += " and o.township=:township ";
            }
            if ( null != mask.tdir ) {
                ejbqlQuery += " and o.tdir=:tdir ";
            }
            if ( null != mask.range ) {
                ejbqlQuery += " and o.range=:range ";
            }
            if ( null != mask.rdir ) {
                ejbqlQuery += " and o.rdir=:rdir ";
            }
            if ( 0 != mask.meridian ) {
                ejbqlQuery += " and o.meridian=:meridian ";
            }
            if ( null != mask.section ) {
                ejbqlQuery += " and o.section=:section ";
            }
            if ( null != mask.tractDescription ) {
                ejbqlQuery += " and o.tractDescription=:tractDescription ";
            }
            if ( null != mask.name ) {
                ejbqlQuery += " and o.name like :name ";
            }
        }
        Logger.getInstance().getLog().error("CoverageService.findCoverageTracts: ejbqlQuery=" + ejbqlQuery);
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(ejbqlQuery);
        query.setParameter("type", mask.type);
        if ( 0 != mask.id ) {
            query.setParameter("id", mask.id);
        } else {
            if ( null != mask.township ) {
                query.setParameter("township", mask.township);
            }
            if ( null != mask.tdir ) {
                query.setParameter("tdir", mask.tdir);
            }
            if ( null != mask.range ) {
                query.setParameter("range", mask.range);
            }
            if ( null != mask.rdir ) {
                query.setParameter("rdir", mask.rdir);
            }
            if ( 0 != mask.meridian ) {
                query.setParameter("meridian", mask.meridian);
            }
            if ( null != mask.section ) {
                query.setParameter("section", mask.section);
            }
            if ( null != mask.tractDescription ) {
                query.setParameter("tractDescription", mask.tractDescription);
            }
            if ( null != mask.name ) {
                query.setParameter("name", mask.name);
            }
        }
        
        return query.getResultList();
    }

    public CoverageTractEntity saveCoverageTract(CoverageTractEntity coverageTract) {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            if ( 0 == coverageTract.id ) {
                coverageTract.id = null;
                EntityManagerHolder.getInstance().getEM().persist(coverageTract);
            } else {
                EntityManagerHolder.getInstance().getEM().merge(coverageTract);
            }
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("CoverageService.saveCoverageTract: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("CoverageService.saveCoverageTract: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return (CoverageTractEntity) EntityManagerHolder.getInstance().getEM().find(CoverageTractEntity.class, coverageTract.id);        
    }

    public void removeCoverageTract(CoverageTractEntity coverageTract) {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            EntityManagerHolder.getInstance().getEM().remove(coverageTract);
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("CoverageService.removeCoverageTract: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("CoverageService.removeCoverageTract: commit failed ", ex);
            throw new RuntimeException(ex);
        }
    }

}
