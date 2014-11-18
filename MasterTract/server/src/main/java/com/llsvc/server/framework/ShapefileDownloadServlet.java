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

import org.geotools.data.FileDataStoreFactorySpi;
import org.geotools.data.DataStore;
import org.geotools.data.DataStoreFinder;
import org.geotools.data.DataUtilities;
import org.geotools.data.DataStoreFactorySpi.Param;
import org.geotools.data.DefaultQuery;
import org.geotools.data.FeatureSource;
import org.geotools.data.FeatureStore;
import org.geotools.data.Transaction;
import org.geotools.data.DefaultTransaction;
import org.geotools.feature.FeatureType;
import org.geotools.feature.FeatureCollection;
import org.geotools.data.shapefile.indexed.IndexedShapefileDataStoreFactory;
import org.geotools.data.postgis.PostgisDataStoreFactory;
import org.geotools.filter.text.cql2.CQL;
import org.opengis.filter.Filter;

import javax.persistence.Query;
import javax.persistence.NoResultException;

import com.llsvc.server.entity.LeaseEntity;
import com.llsvc.server.entity.LeaseTractEntity;
import com.llsvc.server.entity.LeaseTractQQEntity;
import com.llsvc.server.entity.TractEntity;
import com.llsvc.server.framework.EntityManagerHolder;

public class ShapefileDownloadServlet 
    extends AbstractFileServlet 
{

    protected String getDefaultLayer() {
        return "doc_lease_tract_qq_view";
    }

    protected void doGet(HttpServletRequest req, HttpServletResponse resp)
        throws ServletException, IOException 
    {
        super.doGet(req, resp);

        String leaseId = req.getParameter("leaseId");
        System.out.println("leaseId=" + leaseId);
        String tracts = req.getParameter("tracts");
        System.out.println("tracts=" + tracts);

        if ( null != leaseId ) {
            generateShapefile(req, resp, new Integer(leaseId));
        } else if ( null != tracts ) {
            generateShapefile(req, resp, tracts);
        } else {
            generateShapefile(req, resp, null);
        }
    }

    protected void generateShapefile(HttpServletRequest req, HttpServletResponse resp, Object param)
        throws ServletException, IOException 
    {
        super.doGet(req, resp);

        ServletContext      context  = getServletConfig().getServletContext();

        Integer leaseId = null;
        String tracts = null;
        if ( null != param ) {
            if ( param instanceof Integer ) {
                leaseId = (Integer) param;
            } else {
                tracts = (String) param;
            }
        }

        String layer = getDefaultLayer();
        String name = null;
        if ( null != leaseId ) {
            name = leaseId.toString();
        } else if ( null != tracts ) {
            name = "tracts";
        } else {
            String l = req.getParameter("layer");
            if ( null != l ) {
                layer = l;
                name = l;
            } else {
                name = "alltracts";
            }
        }

        String prefix = Configuration.getInstance().getTemporaryDirectory() + "/download/";
        File dir = new File(prefix + name);
        dir.mkdir();

        File file = new File(prefix + name + "/" + name + ".shp");
        DataStore pgDataStore = null;
        DataStore myData = null;
        Transaction t = new DefaultTransaction();
        try {
            pgDataStore = DataStoreFinder.getDataStore(getPostgisConfig());

            FileDataStoreFactorySpi factory = new IndexedShapefileDataStoreFactory();

            Map map = Collections.singletonMap("url", file.toURL());

            myData = factory.createNewDataStore(map);
            FeatureType featureType = pgDataStore.getSchema(layer); 
            //DataUtilities.createType( leaseId.toString(), "geom:Polygon,township:String,range:String,section:String");
            myData.createSchema( featureType );
            FeatureStore featureStore = (FeatureStore) myData.getFeatureSource(layer); 
            //leaseId.toString());

            String query = null;
            if ( null != leaseId ) {
                query = "lease_id=" + leaseId;
            } else if ( null != tracts ) {
                String[] cases = tracts.split(",");
                query = "";
                for (int i=0; i<cases.length; i++) {
                    query +=  ((0 != i) ? " or " : " ") + " tract_id=" + cases[i];
                }
            }

            FeatureCollection pgFeatures = filter(pgDataStore, layer, query);
            featureStore.addFeatures(pgFeatures);            
            t.commit();
        } catch (Exception ex) {
            try {
                t.rollback();
            } catch(IOException ex2) {
                throw new RuntimeException(ex2);
            }
            throw new RuntimeException(ex);
        } finally {
            t.close();
            if ( null != pgDataStore ) {
                pgDataStore.dispose();
            }
            if ( null != myData ) {
                myData.dispose();
            }
        } 

        // create new projection file
        File projFile = new File(prefix + name + "/" + name + ".prj");
        FileWriter projFileWriter = new FileWriter(projFile);
//        projFileWriter.write("GEOGCS[\"GCS_North_American_1983\",DATUM[\"D_North_American_1983\",SPHEROID[\"GRS_1980\",6378137.0,298.257222101]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]]");
        projFileWriter.write("GEOGCS[\"GCS_North_American_1927\",DATUM[\"D_North_American_1927\",SPHEROID[\"Clarke_1866\",6378206.4,294.9786982]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]]");
        projFileWriter.close();

        String filename = prefix + name + ".zip";

        ZipOutputStream zos = new 
           ZipOutputStream(new FileOutputStream(filename)); 
        zipDir(prefix + name, zos); 
        zos.close(); 

        String original_filename = name + ".zip";

        File                f        = new File(filename);
        int                 length   = 0;
        ServletOutputStream op       = resp.getOutputStream();
        String              mimetype = null; //context.getMimeType( filename );

        resp.setContentType( (mimetype != null) ? mimetype : "application/octet-stream" );
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

    public FeatureCollection filter(DataStore dataStore, String typeName, String text) throws Exception {
        FeatureSource table = dataStore.getFeatureSource(typeName);
        if ( null != text ) {
            Filter filter = CQL.toFilter(text);
            DefaultQuery query = new DefaultQuery();
            query.setTypeName(typeName);
            query.setFilter(filter);
            return table.getFeatures(query);
        } else {
            return table.getFeatures();
        }
    }

    public void zipDir(String dir2zip, ZipOutputStream zos) 
    { 
        try { 
            //create a new File object based on the directory we have to zip File    
            File zipDir = new File(dir2zip); 
            //get a listing of the directory content 
            String[] dirList = zipDir.list(); 
            byte[] readBuffer = new byte[2156]; 
            int bytesIn = 0; 
            //loop through dirList, and zip the files 
            for(int i=0; i<dirList.length; i++) 
            { 
                File f = new File(zipDir, dirList[i]); 
                if(f.isDirectory()) 
                { 
                        //if the File object is a directory, call this 
                        //function again to add its content recursively 
                    String filePath = f.getPath(); 
                    zipDir(filePath, zos); 
                        //loop again 
                    continue; 
                } 
                //if we reached here, the File object f was not                 a directory 
                //create a FileInputStream on top of f 
                FileInputStream fis = new FileInputStream(f); 
                // create a new zip entry 
                ZipEntry anEntry = new ZipEntry(f.getName()); 
                //place the zip entry in the ZipOutputStream object 
                zos.putNextEntry(anEntry); 
                //now write the content of the file to the ZipOutputStream 
                while((bytesIn = fis.read(readBuffer)) != -1) 
                { 
                    zos.write(readBuffer, 0, bytesIn); 
                } 
               //close the Stream 
               fis.close(); 
            } 
        } catch(Exception e) { 
            throw new RuntimeException(e);
        }
    } 

    protected Map getPostgisConfig() {
        Map config = new HashMap();
        config.put( PostgisDataStoreFactory.DBTYPE.key, "postgis");
        config.put( PostgisDataStoreFactory.HOST.key, Configuration.getInstance().getDatabaseHost());
        config.put( PostgisDataStoreFactory.PORT.key, Configuration.getInstance().getDatabasePort());
        config.put( PostgisDataStoreFactory.SCHEMA.key, Configuration.getInstance().getDatabaseSchema());
        config.put( PostgisDataStoreFactory.DATABASE.key, Configuration.getInstance().getDatabaseName());
        config.put( PostgisDataStoreFactory.USER.key, Configuration.getInstance().getDatabaseUsername());
        config.put( PostgisDataStoreFactory.PASSWD.key, Configuration.getInstance().getDatabasePassword());           
        return config;
    }

}
