package com.llsvc.server.framework;

import java.io.File;
import java.io.FileWriter;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.DataInputStream;
import java.io.ByteArrayInputStream;

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

public class PseudoFileDownloadServlet 
    extends AbstractFileServlet 
{
    protected void doGet(HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException 
    {
        super.doGet(req, resp);
        
        String data = req.getParameter("data");
        String filename = req.getParameter("filename");
        byte[] bytes = data.getBytes();

        int length = 0;
        ServletOutputStream op = resp.getOutputStream();

        resp.setContentType( "application/xls" );
        resp.setContentLength( bytes.length );
        resp.setHeader( "Content-Disposition", "attachment; filename=\"" + filename + "\"" );

        byte[] bbuf = new byte[10240];
        DataInputStream in = new DataInputStream(new ByteArrayInputStream(bytes));
        while ((in != null) && ((length = in.read(bbuf)) != -1)) {
            op.write(bbuf,0,length);
        }

        in.close();
        op.flush();
        op.close();
    }
}
