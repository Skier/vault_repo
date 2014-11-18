package com.llsvc.server.framework;

import java.io.File;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileInputStream;
import java.io.DataInputStream;
import java.io.PrintWriter;

import java.util.List;
import java.util.Iterator;

import javax.servlet.ServletException;
import javax.servlet.ServletContext;
import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpSession;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.fileupload.FileItemIterator;
import org.apache.commons.fileupload.FileItemStream;
import org.apache.commons.fileupload.util.Streams;
import org.apache.commons.fileupload.servlet.ServletFileUpload;

import javax.persistence.Query;
import javax.persistence.NoResultException;
import javax.persistence.EntityTransaction;

import com.llsvc.server.framework.Logger;
import com.llsvc.server.framework.EntityManagerHolder;
import com.llsvc.server.entity.LeaseTractEntity;
import com.llsvc.server.entity.CoverageTractEntity;
import com.llsvc.server.coverage.CoverageHelper;
import com.llsvc.server.doc.DocumentService;

public class CoverageServlet 
    extends HttpServlet 
{

    protected void doPost(HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException 
    {
        doGet(req, resp);
    }

    protected void doGet(HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException 
    {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();
        try {
            CoverageHelper coverageHelper = new CoverageHelper(DocumentService.LEASE_COVERAGE_TYPE);
            coverageHelper.clearCoverage();
            EntityManagerHolder.getInstance().getEM().flush();

            String sqlQuery = "select lt.id as ltId, g.id as gid from doc_lease_tract lt "
                    + " inner join (select lth.tract_id as tract_id, max(lth.id) as id "
                    + "         from doc_lease_tract_history lth "
                    + "         group by lth.tract_id) lh on lh.tract_id=lt.id "
                    + " inner join doc_lease_tract_history hist on hist.id=lh.id "
                    + " inner join geo_geometry g on g.id=hist.geometry_id order by lt.id ";
            Logger.getInstance().getLog().debug("CoverageServlet: sqlQuery=" + sqlQuery);
            Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(sqlQuery);
            List pairs = query.getResultList();
            Iterator iter = pairs.iterator();
            while ( iter.hasNext() ) {
                Object[] pair = (Object[]) iter.next();
                Integer ltId = (Integer) pair[0];
                Integer geometryId = (Integer) pair[1];
                Logger.getInstance().getLog().debug("CoverageServlet: (G) leaseTractId=" + ltId
                        + ", geometryId=" + geometryId);

                LeaseTractEntity lt = EntityManagerHolder.getInstance().getEM().find(
                        LeaseTractEntity.class, ltId);
                CoverageTractEntity coverageData = new CoverageTractEntity();
                coverageData.name = "Tract #" + ltId;
                coverageData.township = lt.township;
                coverageData.range = lt.range;
                coverageData.section = lt.section;
                coverageData.meridian = 6;
                coverageData.tractDescription = lt.tract;
                coverageHelper.generateCoverage(ltId, geometryId, coverageData);
                EntityManagerHolder.getInstance().getEM().flush();
            }
            
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("CoverageServlet.doGet: exception caught.", ex);
            try {
                tr.rollback();
            } catch(Exception ex2) {
                throw new RuntimeException(ex2);
            }
            throw new RuntimeException(ex);
        }
    }

}
