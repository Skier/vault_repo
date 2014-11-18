package com.llsvc.server.framework;

import java.io.File;
import java.io.FileWriter;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.DataInputStream;

import java.util.Map;
import java.util.HashMap;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.ArrayList;
import java.util.zip.*;

import javax.servlet.ServletException;
import javax.servlet.ServletContext;
import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.fileupload.FileItemIterator;
import org.apache.commons.fileupload.FileItemStream;
import org.apache.commons.fileupload.util.Streams;
import org.apache.commons.fileupload.servlet.ServletFileUpload;

import net.sf.jxls.exception.ParsePropertyException;
import net.sf.jxls.transformer.XLSTransformer;

import javax.persistence.Query;
import javax.persistence.NoResultException;

import com.llsvc.server.entity.LeaseEntity;
import com.llsvc.server.entity.LeaseTractEntity;
import com.llsvc.server.entity.LeaseTractQQEntity;
import com.llsvc.server.entity.TractEntity;
import com.llsvc.server.framework.EntityManagerHolder;
import com.llsvc.server.doc.DocumentService;

public class XLSFileDownloadServlet 
    extends AbstractFileServlet 
{
    private static String LEASES_TEMPLATE_FILENAME = "leases_template.xls";
    private static String LEASES_DEST_FILENAME = "leases.xls";
    private static String TRACTS_TEMPLATE_FILENAME = "tracts_template.xls";
    private static String TRACTS_DEST_FILENAME = "tracts.xls";

    protected void doGet(HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException 
    {
        super.doGet(req, resp);

        Map beans = new HashMap();
        ServletContext context = getServletConfig().getServletContext();
        String prefix = context.getRealPath("/");
        String leaseId = req.getParameter("leaseId");
        String templateFileName = null;
        String destFileName = null;

        if ( null == leaseId ) {
            templateFileName = XLSFileDownloadServlet.LEASES_TEMPLATE_FILENAME;
            destFileName = XLSFileDownloadServlet.LEASES_DEST_FILENAME;

            String sqlQuery = "select l.lease_num, l.lease_name, " 
                    + " coalesce((select rc.name || ' ' || r.doc_no || ':' || r.volume || '/' || r.page "
                    + "     from doc_record r inner join geo_county rc on rc.id=r.county_id "
                    + "     where r.doc_id=l.id limit 1), 'NOT RECORDED') as records, "
                    + " to_char(l.effect_date + cast(l.term || ' months' as interval), 'MM/DD/YYYY') as exp_date, "
                    + " (select sum(lt.gross_acres) from doc_lease_tract lt where lt.lease_id=l.id) as gross_acres, "
                    + " (select sum(lt.net_acres) from doc_lease_tract lt where lt.lease_id=l.id) as net_acres, "
                    + " (select sum(lt.lease_interest)*100/count(*) from doc_lease_tract lt where lt.lease_id=l.id) as interest, "
                    + " (select sum(lt.lease_burden)*100/count(*) from doc_lease_tract lt where lt.lease_id=l.id) as l_burden, "
                    + " (select sum(lt.nri)*100/count(*) from doc_lease_tract lt where lt.lease_id=l.id) as l_nri, "
                    + " (select sum(lt.cwi)*100/count(*) from doc_lease_tract lt where lt.lease_id=l.id) as wi, "
                    + " (select sum(lt.burden)*100/count(*) from doc_lease_tract lt where lt.lease_id=l.id) as add_burden, "
                    + " (select sum(lt.cnri)*100/count(*) from doc_lease_tract lt where lt.lease_id=l.id) as nri, "
                    + " (select sum(lt.c_net_acres)*100/count(*) from doc_lease_tract lt where lt.lease_id=l.id) as net "
                    + " from doc_lease l "
                    + " order by l.lease_name ";

            Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(
                    sqlQuery);
            beans.put("leases", query.getResultList());
        } else {
            templateFileName = XLSFileDownloadServlet.TRACTS_TEMPLATE_FILENAME;
            destFileName = XLSFileDownloadServlet.TRACTS_DEST_FILENAME;

            String sqlQuery = "select lt.id, " 
                    + " cast('PLSS' as text) as type, "
                    + " coalesce((select s.name from geo_state s where s.id=lt.state_id), '') as state, "
                    + " coalesce((select c.name from geo_county c where c.id=lt.county_id), '') as county, "
                    + " lt.township || ' ' || lt.range as tr, "
                    + " lt.section as sec, "
                    + " lt.tract as tract, "
                    + " lt.gross_acres as gross_acres, "
                    + " lt.net_acres as net_acres, "
                    + " lt.lease_interest*100 as interest, "
                    + " lt.lease_burden*100 as l_burden, "
                    + " lt.nri*100 as l_nri, "
                    + " lt.cwi*100 as wi, "
                    + " lt.burden*100 as add_burden, "
                    + " lt.cnri*100 as nri, "
                    + " lt.c_net_acres*100 as net "
                    + " from doc_lease_tract lt "
                    + " where lt.lease_id = " + leaseId
                    + " order by lt.id ";

            Query query = EntityManagerHolder.getInstance().getEM().createNativeQuery(
                    sqlQuery);
            beans.put("tracts", query.getResultList());
        }


        XLSTransformer transformer = new XLSTransformer();
        transformer.transformXLS(prefix + "/WEB-INF/" + templateFileName, beans, destFileName);
        File                f        = new File(destFileName);
        int                 length   = 0;
        ServletOutputStream op       = resp.getOutputStream();
        String              mimetype = "application/xls";

        resp.setContentType( (mimetype != null) ? mimetype : "application/pdf" );
        resp.setContentLength( (int)f.length() );
        resp.setHeader( "Content-Disposition", "attachment; filename=\"" + destFileName + "\"" );

        byte[] bbuf = new byte[10240];
        DataInputStream in = new DataInputStream(new FileInputStream(f));
        while ((in != null) && ((length = in.read(bbuf)) != -1)) {
            op.write(bbuf,0,length);
        }

        in.close();
        op.flush();
        op.close();
    }

}
