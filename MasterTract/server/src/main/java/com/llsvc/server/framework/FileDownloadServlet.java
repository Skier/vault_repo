package com.llsvc.server.framework;

import java.io.File;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileInputStream;
import java.io.DataInputStream;

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

import javax.persistence.Query;
import javax.persistence.NoResultException;

import com.llsvc.server.entity.FileEntity;
import com.llsvc.server.framework.EntityManagerHolder;

public class FileDownloadServlet 
    extends AbstractFileServlet 
{

    protected void doGet(HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException 
    {
        super.doGet(req, resp);

        ServletContext      context  = getServletConfig().getServletContext();

        String id = req.getParameter("id");
        System.out.println("id=" + id);

        FileEntity da = EntityManagerHolder.getInstance().getEM().find(FileEntity.class, new Integer(id));

        String filename = da.storageKey;
        String original_filename = da.origFilename;

        File                f        = new File(Configuration.getInstance().getAttachmentsStoragePrefix() + "/" + filename);
        int                 length   = 0;
        ServletOutputStream op       = resp.getOutputStream();
        String              mimetype = context.getMimeType( filename );

        resp.setContentType( (mimetype != null) ? mimetype : "application/pdf" );
        resp.setContentLength( (int)f.length() );
        resp.setHeader( "Content-Disposition", "attachment; filename=\"" + original_filename + "\"" );

        byte[] bbuf = new byte[10240];
        DataInputStream in = new DataInputStream(new FileInputStream(f));

        while ((in != null) && ((length = in.read(bbuf)) != -1))
        {
            op.write(bbuf,0,length);
        }

        in.close();
        op.flush();
        op.close();
    }

    protected void doPost(HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException 
    {
        doGet(req, resp);
    }

}
