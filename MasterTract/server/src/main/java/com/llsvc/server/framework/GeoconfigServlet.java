package com.llsvc.server.framework;

import java.io.File;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileOutputStream;
import java.io.DataInputStream;
import java.io.PrintWriter;
import java.io.*;

import java.util.Date;
import java.util.Map;
import java.util.HashMap;
import java.util.Enumeration;
import java.util.Collections;
import java.util.Iterator;
import java.util.zip.*;

import java.net.URL;
import java.net.URLConnection;
import java.net.MalformedURLException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.fileupload.FileItemIterator;
import org.apache.commons.fileupload.FileItemStream;
import org.apache.commons.fileupload.util.Streams;
import org.apache.commons.fileupload.servlet.ServletFileUpload;

import org.geotools.data.FileDataStoreFactorySpi;
import org.geotools.data.DataStore;
import org.geotools.data.DataStoreFinder;
import org.geotools.data.DataUtilities;
import org.geotools.data.DataStoreFactorySpi.Param;
import org.geotools.data.DefaultQuery;
import org.geotools.data.FeatureSource;
import org.geotools.data.FeatureStore;
import org.geotools.data.FeatureWriter;
import org.geotools.data.Transaction;
import org.geotools.data.DefaultTransaction;
//import org.geotools.geometry.Geometry;
import org.geotools.feature.Feature;
import org.geotools.feature.DefaultFeature;
import org.geotools.feature.FeatureType;
import org.geotools.feature.DefaultFeatureType;
import org.geotools.feature.FeatureIterator;
import org.geotools.feature.FeatureCollection;
import org.geotools.feature.AttributeType;
import org.geotools.data.shapefile.indexed.IndexedShapefileDataStoreFactory;
import org.geotools.data.postgis.PostgisDataStoreFactory;
import org.geotools.data.store.DataFeatureCollection;
import org.geotools.filter.text.cql2.CQL;
import org.geotools.filter.FilterFactoryFinder;
import org.geotools.filter.FidFilterImpl;
import org.opengis.filter.Filter;
import org.opengis.filter.FilterFactory;
import com.vividsolutions.jts.geom.Geometry;
import org.geotools.factory.CommonFactoryFinder;
import org.opengis.filter.FilterFactory2;

import javax.persistence.NoResultException;
import javax.persistence.EntityTransaction;

import com.llsvc.server.entity.UserEntity;
import com.llsvc.server.entity.FileEntity;
import com.llsvc.server.entity.LayerEntity;
import com.llsvc.server.entity.LeaseTractEntity;
import com.llsvc.server.entity.LeaseTractHistoryEntity;
import com.llsvc.server.framework.EntityManagerHolder;
import com.llsvc.server.framework.Logger;
import com.llsvc.server.geo.GeoService;

public class GeoconfigServlet 
    extends FileUploadServlet 
{
    private static String cookieString = null;

    public static String login() throws MalformedURLException, IOException {
        String responseString = "";
        InputStream inStream;
        BufferedReader in;
        URLConnection conn;

        URL url = new URL(Configuration.getInstance().getGeoserverUrl() + "admin/login.do");

        conn = url.openConnection();
        inStream = conn.getInputStream();
        responseString = new String();
        in = new BufferedReader(new InputStreamReader(inStream));

        while (in.ready()) {
            responseString += in.readLine();
        }

        String cookie = conn.getHeaderField("Set-Cookie");
        cookie = cookie.substring(0, cookie.indexOf(";"));
        String cookieName = cookie.substring(0, cookie.indexOf("="));
        String cookieValue = cookie.substring(cookie.indexOf("=") + 1, cookie.length());
        cookieString = cookieName + "=" + cookieValue;

        return responseString;
    }
    
    public static String geoserverAction(String address) 
                    throws MalformedURLException, IOException {
        URLConnection conn;
        String responseString = "";
        InputStream inStream;
        BufferedReader in;
        
        URL url = new URL(Configuration.getInstance().getGeoserverUrl() + address);

        conn = url.openConnection();
        conn.setRequestProperty("Cookie", cookieString);
        conn.connect();

        inStream = conn.getInputStream();
        in = new BufferedReader(new InputStreamReader(inStream));
        responseString = new String();
        while (in.ready()) {
            responseString += in.readLine();
        }
        
        System.out.println(responseString);
        return responseString;
    }

    public static String coverageSubmitBuilder(String newlayername) throws MalformedURLException, IOException {
        String styl = "styleId=polygon";
        String srsn = "&SRS=4267";
        String minx = "&minX=-180";
        String miny = "&minY=-90";
        String maxx = "&maxX=180";
        String maxy = "&maxY=90";
        String keyw = "&keywords=" + newlayername;
        String title = "&title=llsvc:" + newlayername;
        String abst = "&abstract=llsvc:" + newlayername;
        String schema = "&schemaBase=--";
        String auto = "&autoGenerateExtent=true";
        String acti = "&action=Submit";
        
        String urls = styl + srsn + title + minx + miny + maxx + maxy + keyw + abst + schema + auto + acti;
        return urls;
    }

    private static void addLayer(String newlayername) throws MalformedURLException, IOException {
//        login();

        geoserverAction("j_acegi_security_check?username=" + Configuration.getInstance().getGeoserverUsername() 
                + "&password=" + Configuration.getInstance().getGeoserverPassword() + "&submit=Submit");

        geoserverAction("config/data/typeNewSubmit.do?selectedNewFeatureType=llsvc:::" + newlayername + "&submit=New");

        String urls = coverageSubmitBuilder(newlayername);

        geoserverAction("config/data/typeEditorSubmit.do?" + urls);

        geoserverAction("admin/saveToGeoServer.do");

        geoserverAction("admin/saveToXML.do");

//        geoserverAction("admin/logout.do");
    }

    protected void postUpload(HttpServletRequest req, HttpServletResponse rsp, FileEntity f, String filename, String originalFilename) {
        try {
            String prefix = Configuration.getInstance().getTemporaryDirectory() + "/upload/";
            File baseFile = new File(filename);
            String baseFilename = originalFilename;
            String baseDirName = baseFilename.substring(0, baseFilename.length() - 4);
            File baseDir = new File(prefix + baseDirName);
            baseDir.mkdir();

            unzip(prefix + baseDirName, filename);

            filename = prefix + baseDirName + File.separator + baseDirName + ".shp";
            Logger.getInstance().getLog().debug("GeoconfigServlet.postUpload: filename=" + filename);

            File file = new File(filename);
            if( !file.exists() ) {
                Logger.getInstance().getLog().error("GeoconfigServlet.postUpload: file not found " + filename);
                return;
            }

            String layerName = "l_" + baseDirName.toLowerCase();

            GeoService geoService = new GeoService();
            LayerEntity layer = null;
            try { 
                layer = geoService.getLayerByName(layerName);
            } catch (NoResultException nrex) {
                layer = new LayerEntity();
                layer.id = 0;
            }
            layer.description = f.note;
            layer.name = layerName;
            layer.isActive = Boolean.TRUE;
            layer.isPublic = Boolean.TRUE;
            geoService.saveLayer(layer);
            
            String srs = req.getParameter("srs");
            Logger.getInstance().getLog().debug("GeoconfigServlet.postUpload: srs=" + srs);

            String shp2pgsql = "shp2pgsql.exe -d " + (null != srs ? ("-s " + srs) : ("")) + " " + filename + " " + layerName;
            String psql = "psql.exe -U llsvc -d llsvc";

            Process p = Runtime.getRuntime().exec(shp2pgsql);
            Process p2 = Runtime.getRuntime().exec(psql);
            try {
                InputStream pin = p.getInputStream();
                InputStreamReader inputstreamreader =
                        new InputStreamReader(pin);
                BufferedReader bufferedreader =
                        new BufferedReader(inputstreamreader);
    
                OutputStream pout = p2.getOutputStream();
                PrintWriter writer = new PrintWriter(pout);

                String line;
                while ((line = bufferedreader.readLine()) 
                          != null) {
                    writer.print(line);
                }

                bufferedreader.close();
                writer.close();

                p.waitFor();
                Logger.getInstance().getLog().debug("GeoconfigServlet.postUpload: p.exitCode=" + p.exitValue());

                InputStream pin2 = p2.getInputStream();
                InputStreamReader inputstreamreader2 =
                        new InputStreamReader(pin2);
                BufferedReader bufferedreader2 =
                        new BufferedReader(inputstreamreader2);
    
                while ((line = bufferedreader2.readLine()) 
                          != null) {
                    Logger.getInstance().getLog().debug("GeoconfigServlet.postUpload: p2.line=" + line);
                }

                p2.waitFor();
                Logger.getInstance().getLog().debug("GeoconfigServlet.postUpload: p2.exitCode=" + p2.exitValue());

                String response = GeoconfigServlet.login();
                Logger.getInstance().getLog().debug("GeoconfigServlet.postUpload: cookieString=" + cookieString);
                GeoconfigServlet.addLayer(layerName);
                Logger.getInstance().getLog().debug("GeoconfigServlet.postUpload: layer is created.");
            } catch (InterruptedException ex) {
                throw new RuntimeException(ex);
            }
        } catch (Exception ex2) {
            throw new RuntimeException(ex2);
        }
    }

    protected void unzip(String directory, String filename) {
      try {
         BufferedOutputStream dest = null;
         BufferedInputStream is = null;
         ZipEntry entry;
         ZipFile zipfile = new ZipFile(filename);
         Enumeration e = zipfile.entries();
         while(e.hasMoreElements()) {
            entry = (ZipEntry) e.nextElement();
            System.out.println("Extracting: " +entry);
            is = new BufferedInputStream
              (zipfile.getInputStream(entry));
            int count;
            byte data[] = new byte[1024];
            FileOutputStream fos = new 
              FileOutputStream(directory + "/" + entry.getName());
            dest = new 
              BufferedOutputStream(fos, 1024);
            while ((count = is.read(data, 0, 1024)) 
              != -1) {
               dest.write(data, 0, count);
            }
            dest.flush();
            dest.close();
            is.close();
         }
      } catch(Exception e) {
         e.printStackTrace();
      }
   }
    
}
