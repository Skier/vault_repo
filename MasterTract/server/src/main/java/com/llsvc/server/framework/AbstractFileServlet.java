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

import com.llsvc.server.entity.DocumentAttachmentEntity;
import com.llsvc.server.framework.EntityManagerHolder;

public abstract class AbstractFileServlet 
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
        HttpSession session = req.getSession();
        if ( null == session ) {
            throw new ServletException("not authorized");
        }
        String authorized = (String) session.getAttribute("authorized");
        if ( null == authorized || !"yes".equals(authorized) ) {
            throw new ServletException("not authorized");
        }
    }

}
