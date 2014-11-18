package com.llsvc.server.coverage;

import java.util.Collection;
import java.util.List;
import java.util.Iterator;

import javax.persistence.Query;

import com.llsvc.server.framework.Logger;
import com.llsvc.server.framework.EntityManagerHolder;

import com.llsvc.server.entity.CoverageTractEntity;
import com.llsvc.server.entity.CoverageTractSetEntity;

// to do: Add coverage type handling
public class CoverageHelper
{
    private Integer type = null;

    public CoverageHelper(Integer type) {
        this.type = type;
    }

    public void clearCoverage() {
        Logger.getInstance().getLog().debug("CoverageHelper.clearCoverage: type=" + type);
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select c from CoverageTractEntity as c where type=:type");
        query.setParameter("type", type);
        List coverage = query.getResultList();
        if ( null != coverage ) {
            Iterator<CoverageTractEntity> ci = coverage.iterator();
            while ( ci.hasNext() ) {
                CoverageTractEntity c = ci.next();
                Logger.getInstance().getLog().debug("CoverageHelper.clearCoverage: c.id=" + c.id);
                EntityManagerHolder.getInstance().getEM().remove(c);
            }
        }
        Logger.getInstance().getLog().debug("CoverageHelper.clearCoverage: done.");
    }

    public void removeCoverage(Integer externalId) {
        Logger.getInstance().getLog().debug("CoverageHelper.removeCoverage: externalId=" + externalId);
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select cts from CoverageTractSetEntity as cts where cts.externalId=:externalId "
                        + " and cts.coverageTract.type=:type");
        query.setParameter("externalId", externalId);
        query.setParameter("type", type);
        List coverageSet = query.getResultList();
        if ( null != coverageSet ) {
            Iterator<CoverageTractSetEntity> ctsi = coverageSet.iterator();
            while ( ctsi.hasNext() ) {
                CoverageTractSetEntity cts = ctsi.next();
                Logger.getInstance().getLog().debug("CoverageHelper.removeCoverage: cts.id=" + cts.id);
                CoverageTractEntity coverageTract = cts.coverageTract;
                Collection<CoverageTractSetEntity> covSet = coverageTract.coverageSet;
                Logger.getInstance().getLog().debug("CoverageHelper.removeCoverage: coverageTract.id=" + coverageTract.id
                        + ", covSet.size=" + covSet.size() );
                EntityManagerHolder.getInstance().getEM().remove(cts);
                if ( 1 == covSet.size() ) {
                    EntityManagerHolder.getInstance().getEM().remove(coverageTract);
                }
            }
        }
    }

    public void generateCoverage(Integer externalId, Integer geometryId, CoverageTractEntity coverageData) {
        Logger.getInstance().getLog().debug("CoverageHelper.generageCoverage: (I) externalId=" + externalId);
        String sqlQuery = " area2d(transform(the_geom, 26714))*0.0002471 as ac, " + 
                " the_geom " +
                " from GEO_GEOMETRY where id = " + geometryId;
        CoverageTractEntity coverageTract = insertCoverage(sqlQuery);
        Logger.getInstance().getLog().debug("CoverageHelper.generageCoverage: coverageTract.id=" + coverageTract.id);
        boolean joinToExternalTract = true;

        if ( null != coverageData ) {
            coverageTract.name = coverageData.name;
            coverageTract.township = coverageData.township;
            coverageTract.tdir = coverageData.tdir;
            coverageTract.range = coverageData.range;
            coverageTract.rdir = coverageData.rdir;
            coverageTract.meridian = coverageData.meridian;
            coverageTract.section = coverageData.section;
            coverageTract.tractDescription = coverageData.tractDescription;
            EntityManagerHolder.getInstance().getEM().merge(coverageTract);
            EntityManagerHolder.getInstance().getEM().flush();
            EntityManagerHolder.getInstance().getEM().refresh(coverageTract);
        }

        // find coverage tracts that intersect our's
        String intersectQuery = "select b.id from geo_coverage_tract a "
                + " inner join geo_coverage_tract b on a.id != b.id "
                + " where intersects(a.the_geom, b.the_geom) "
                + "   and not isempty(buffer(intersection(a.the_geom, b.the_geom), 0.0)) "
                + "   and 0 != area(intersection(a.the_geom, b.the_geom)) "
                + "   and a.id=" + coverageTract.id;
        Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(intersectQuery);
        List intCoverages = query.getResultList();
        Iterator inti = intCoverages.iterator();
        while ( inti.hasNext() ) {
            Object intCoverage = inti.next();
            Logger.getInstance().getLog().debug("CoverageHelper.generageCoverage: intCoverage=" + intCoverage);
            CoverageTractEntity secondCoverageTract = EntityManagerHolder.getInstance().getEM().find(
                    CoverageTractEntity.class, new Integer(intCoverage.toString()));
            Logger.getInstance().getLog().debug("CoverageHelper.generageCoverage: secondCoverageTract.id=" + secondCoverageTract.id);
            
            // generage intersection coverage tract
            String insertIntersectionQuery = "area2d(transform(intersection(a.the_geom, b.the_geom),  26714))*0.0002471 as ac, "
                    + " multi(buffer(intersection(a.the_geom, b.the_geom),0)) "
                    + " from geo_coverage_tract a, geo_coverage_tract b "
                    + " where a.id=" + coverageTract.id
                    + " and b.id=" + intCoverage;
            CoverageTractEntity intCoverageTract = insertCoverage(insertIntersectionQuery);
            insertCoverageTractSet(externalId, intCoverageTract);
            Collection<CoverageTractSetEntity> secondCovSet = secondCoverageTract.coverageSet;
            if ( null != secondCovSet ) {
                Iterator<CoverageTractSetEntity> ctsi = secondCovSet.iterator();
                while ( ctsi.hasNext() ) {
                    CoverageTractSetEntity cts = ctsi.next();
                    if ( !cts.externalId.equals(externalId) ) {
                        insertCoverageTractSet(cts.externalId, intCoverageTract);
                    }
                }
            }            
           
            // exclude intersection from coverageTract and secondCoverageTract
            joinToExternalTract = joinToExternalTract && !excludeIntersection(coverageTract, intCoverageTract);
            excludeIntersection(secondCoverageTract, intCoverageTract);
            if ( !joinToExternalTract ) {
                Logger.getInstance().getLog().warn("CoverageHelper.generageCoverage: coverageTract should be removed!");
                // tract is lost, no more intersections
                break;
            }
        }

        if ( joinToExternalTract ) {
            insertCoverageTractSet(externalId, coverageTract);
        }

    }

    private CoverageTractEntity insertCoverage(String tailQuery) {
        Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(
            "select nextval('GEO_COVERAGE_TRACT_SQC')");
        java.math.BigInteger coverageTractId = (java.math.BigInteger) query.getSingleResult();
        Logger.getInstance().getLog().debug("CoverageHelper.insertCoverage: coverageTractId=" + coverageTractId);

        String sqlQuery = "insert into GEO_COVERAGE_TRACT (ID, TYPE, AC, THE_GEOM) " 
                + " select " + coverageTractId + " as id, "
                + type + " as type, " + tailQuery; 
        query = EntityManagerHolder.getInstance().getEM().createNativeQuery(sqlQuery);
        Logger.getInstance().getLog().debug("CoverageHelper.insertCoverage: sqlQuery=" + sqlQuery);
        int count = query.executeUpdate();
        Logger.getInstance().getLog().debug("CoverageHelper.insertCoverage: inserted count=" + count);

        CoverageTractEntity coverageTract = EntityManagerHolder.getInstance().getEM().find(
                CoverageTractEntity.class, new Integer(coverageTractId.intValue()));
        Logger.getInstance().getLog().debug("CoverageHelper.insertCoverage: coverageTract=" + coverageTract);

        return coverageTract;
    }

    private CoverageTractSetEntity insertCoverageTractSet(Integer externalId, CoverageTractEntity coverageTract) {
        Logger.getInstance().getLog().debug("CoverageHelper.insertCoverageTractSet:\n " 
                + " coverageTractId=" + coverageTract.id
                + " externalId=" + externalId
        );
        CoverageTractSetEntity cts = new CoverageTractSetEntity();
        cts.externalId = externalId;
        cts.coverageTract = coverageTract; 
        EntityManagerHolder.getInstance().getEM().persist(cts);
        return cts;
    }

    private boolean excludeIntersection(CoverageTractEntity coverageTract, CoverageTractEntity intTract) {
        String deleteQuery = "delete from GEO_COVERAGE_TRACT a "
                + " where ( isempty((select buffer(difference(a.the_geom, b.the_geom),0) "
                + " from GEO_COVERAGE_TRACT b where b.id=" + intTract.id + ")) "
                + "   or 0 = (select area(difference(a.the_geom, b.the_geom)) "
                + " from GEO_COVERAGE_TRACT b where b.id=" + intTract.id + ") ) "
                + " and a.id=" + coverageTract.id;
        Logger.getInstance().getLog().debug("CoverageHelper.excludeIntersection: deleteQuery=" + deleteQuery);
        Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(deleteQuery);
        if (0 == query.executeUpdate() ) {
            String updateQuery = "update GEO_COVERAGE_TRACT a set  "
                    + " the_geom=(select multi(buffer(difference(a.the_geom, b.the_geom), 0)) "
                    + " from geo_coverage_tract b where b.id=" + intTract.id + "), "
                    + " ac=a.ac-(select ac from geo_coverage_tract b where b.id=" + intTract.id + ") "
                    + " where a.id=" + coverageTract.id;
            Logger.getInstance().getLog().debug("CoverageHelper.excludeIntersection: updateQuery=" + updateQuery);
            query = EntityManagerHolder.getInstance().getEM().createNativeQuery(updateQuery);
            query.executeUpdate();
            return false;
        } else {
            return true;
        }
    }

/*
    private Collection<CoverageTractSetEntity> getCoverageSet(CoverageTractEntity ct) {
        Query query = EntityManagerHolder.getInstance().getEM().createQuery(
                "select ct from CoverageTractSetEntity as ct where ct.coverageTractId=:coverageTractId");
        query.setParameter("coverageTractId", ct.id);
        return query.getResultList();
    }
*/
}
