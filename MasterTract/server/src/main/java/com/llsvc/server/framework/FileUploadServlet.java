package com.llsvc.server.framework;

import java.io.File;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileOutputStream;
import java.io.DataInputStream;
import java.io.PrintWriter;

import java.util.Date;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.fileupload.FileItemIterator;
import org.apache.commons.fileupload.FileItemStream;
import org.apache.commons.fileupload.util.Streams;
import org.apache.commons.fileupload.servlet.ServletFileUpload;

import javax.persistence.EntityTransaction;

import com.llsvc.server.entity.UserEntity;
import com.llsvc.server.entity.FileEntity;
import com.llsvc.server.framework.EntityManagerHolder;

public class FileUploadServlet 
    extends AbstractFileServlet 
{

    protected void doGet(HttpServletRequest req, HttpServletResponse rsp)
        throws ServletException, IOException 
    {
//        super.doGet(req, rsp);
        try {
            boolean isMultipart = ServletFileUpload.isMultipartContent(req);
            if ( isMultipart ) {
                ServletFileUpload upload = new ServletFileUpload();

                FileItemIterator iter = upload.getItemIterator(req);
                while (iter.hasNext()) {
                    FileItemStream item = iter.next();
                    String name = item.getFieldName();
                    InputStream stream = item.openStream();
                    if (item.isFormField()) {
                        System.out.println("Form field " + name + " with value "
                            + Streams.asString(stream) + " detected.");
                    } else {
                        String userId = req.getParameter("userId");
                        String description = req.getParameter("description");

                        System.out.println("File field " + name + " with file name " + item.getName() + " detected.");
                        System.out.println("description=" + description + ", userId=" + userId);

                        int length = 0;
                        byte[] bbuf = new byte[10240];
                        DataInputStream in = new DataInputStream(stream);
                        File inputFile = new File(item.getName());
                        String storageKey = System.currentTimeMillis() + "_" + inputFile.getName();
                        String storedFilename = Configuration.getInstance().getAttachmentsStoragePrefix() + "/" + storageKey;
                        FileOutputStream op = new FileOutputStream(new File(storedFilename));
                        while ((in != null) && ((length = in.read(bbuf)) != -1))
                        {
                            op.write(bbuf,0,length);
                        }
                        op.close();
                        in.close();

                        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
                        FileEntity da = new FileEntity();
                        tr.begin();
                        try {
                            da.note = description;
                            da.origFilename = item.getName();
                            da.mimeType = "application/pdf";
                            da.storageKey = storageKey;
                            da.user = (UserEntity) EntityManagerHolder.getInstance().getEM().find(
                                    UserEntity.class, new Integer(userId));
                            da.changed = new Date();

                            EntityManagerHolder.getInstance().getEM().persist(da);
                        } catch (Exception ex) {
                            tr.rollback();
                            throw ex;
                        }
                        tr.commit();
                                      
                        try {
                            postUpload(req, rsp, da, storedFilename, item.getName());
                        } catch (Throwable ex) {
                            throw new RuntimeException(ex);
                        }

                        PrintWriter pw = new PrintWriter(rsp.getOutputStream());
                        pw.print(da.id.toString());
                        pw.close();
                        System.out.println("FileUploadServlet.doPost: done, da.id=" + da.id);
                    }
                }
            } else {
                System.out.println("FileUploadServlet.doPost: not multipart request.");
            }
        } catch (Exception ex) {
            throw new RuntimeException(ex);
        }
    }

    protected void postUpload(HttpServletRequest req, HttpServletResponse rsp, FileEntity file, String filename, String originalFilename) {
    }

}
