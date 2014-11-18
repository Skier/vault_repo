package com.llsvc.server.framework;

import java.io.File;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileInputStream;
import java.io.DataInputStream;
import java.io.PrintWriter;

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

import com.llsvc.server.entity.UserOptionEntity;
import com.llsvc.server.framework.EntityManagerHolder;

public class UserOptionServlet 
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
        String id = req.getParameter("id");
        System.out.println("UserOptionServlet, id=" + id);

        UserOptionEntity option = (UserOptionEntity) EntityManagerHolder.getInstance().getEM().find(
                UserOptionEntity.class, new Integer(id));    
        if ( null != option ) {
            ServletOutputStream op = resp.getOutputStream();
            PrintWriter writer = new PrintWriter(op);            
            writer.write(option.optionValue);
            writer.close();
        } else {
            throw new RuntimeException("User option is not found for id " + id);
        }
    }

}
