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

import javax.persistence.EntityTransaction;

import com.llsvc.server.entity.UserEntity;
import com.llsvc.server.entity.FileEntity;
import com.llsvc.server.entity.LeaseTractEntity;
import com.llsvc.server.entity.LeaseTractHistoryEntity;
import com.llsvc.server.entity.SurfaceTractEntity;
import com.llsvc.server.entity.SurfaceTractHistoryEntity;
import com.llsvc.server.doc.DocumentService;
import com.llsvc.server.coverage.CoverageHelper;
import com.llsvc.server.framework.EntityManagerHolder;
import com.llsvc.server.framework.Logger;

/* 
GEO_GEOMETRY
REF_ID -> DOC_SURFACE_TRACT.ID if STATUS=-2
REF_ID -> DOC_LEASE_TRACT.ID if STATUS=-1
REF_ID -> DOC_LEASE.ID if STATUS=1
REF_ID -> GEO_TRACT.ID if STATUS=0
*/
public class ShapefileTractUploadServlet 
    extends FileUploadServlet 
{
    public static final String TRACT_TYPE_KEY = "tractType";
    public static final String LEASE_TRACT_TYPE = "lt";
    public static final String SURFACE_TRACT_TYPE = "st";

    public static final String GEOMETRY_FEATURE_TYPE = "geo_geometry";
    public static final String DOC_LEASE_TRACT_VIEW_TYPE = "doc_lease_tract_view";

    protected void postUpload(HttpServletRequest req, HttpServletResponse rsp, FileEntity f, String filename, String originalFilename) {
        // check tract type
        String tractType = req.getParameter(ShapefileTractUploadServlet.TRACT_TYPE_KEY);
        if ( null == tractType ) {
            tractType = ShapefileTractUploadServlet.LEASE_TRACT_TYPE;
        }
        if ( !tractType.equals(ShapefileTractUploadServlet.LEASE_TRACT_TYPE)
                && !tractType.equals(ShapefileTractUploadServlet.SURFACE_TRACT_TYPE) ) {
            throw new RuntimeException("Not suported tract type: " + tractType);
        }

        String prefix = Configuration.getInstance().getTemporaryDirectory() + "/upload/";
        DataStore pgDataStore = null;
        Transaction t = new DefaultTransaction();
        EntityTransaction tr = null;
        try {
            File baseFile = new File(filename);
            String baseFilename = originalFilename;
            String baseDirName = baseFilename.substring(0, baseFilename.length() - 4);
            File baseDir = new File(prefix + baseDirName);
            baseDir.mkdir();

            unzip(prefix + baseDirName, filename);

            filename = prefix + baseDirName + File.separator + baseDirName + ".shp";
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: filename=" + filename);

            File file = new File(filename);
            if( !file.exists() ) {
                Logger.getInstance().getLog().error("ShapefileTractUploadServlet.postUpload: file not found " + filename);
                return;
            }

            Map connect = new HashMap();
            connect.put("url", file.toURL());

            DataStore dataStore = DataStoreFinder.getDataStore(connect);
            String[] typeNames = dataStore.getTypeNames();
            String typeName = typeNames[0];

            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: reading content "+ typeName);

            FeatureSource featureSource = dataStore.getFeatureSource(typeName);
            FeatureCollection collection = featureSource.getFeatures();

            pgDataStore = DataStoreFinder.getDataStore(getPostgisConfig());
            FeatureWriter fw = pgDataStore.getFeatureWriterAppend(GEOMETRY_FEATURE_TYPE, t);

            FeatureIterator iterator = collection.features();
            Map<Integer, Integer> geometryIdToTractId = new HashMap<Integer, Integer>();
            while( iterator.hasNext() ){
                Feature feature = iterator.next();
                Object tractId = feature.getAttribute(1);
                Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: tractId=" + tractId);
                
                if ( null == tractId ) {
                    throw new RuntimeException("tractId is null.");
                } else {
                    Integer status = null;
                    if ( tractType.equals(ShapefileTractUploadServlet.LEASE_TRACT_TYPE) ) {
                        status = new Integer(-1);
                    } else if ( tractType.equals(ShapefileTractUploadServlet.SURFACE_TRACT_TYPE) ) {
                        status = new Integer(-2);
                    }

                    Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: insert tract historical shape, tractType=" + tractType);
                    // insert new GEO_GEOMETRY, REF_ID=tractId
                    Feature df = fw.next();
                    df.setDefaultGeometry(feature.getDefaultGeometry());
                    df.setAttribute("status", status);
                    df.setAttribute("changed", new java.util.Date());
                    df.setAttribute("ref_id", tractId);
                    fw.write();
                    Integer fid = new Integer(df.getID().substring(df.getID().indexOf(".")+1));
                    Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: df.id=" + fid);

                    geometryIdToTractId.put(fid, new Integer(tractId.toString()));
                }
            }
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 0.");
            iterator.close();
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 1.");
            fw.close();
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 2.");
            t.commit();
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 3.");
            t.close();


            tr = EntityManagerHolder.getInstance().getEM().getTransaction();
            tr.begin();
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 3.1");
            Iterator<Integer> i = geometryIdToTractId.keySet().iterator();
            while ( i.hasNext() ) {
                Integer geometryId  = i.next();
                Integer tractId = geometryIdToTractId.get(geometryId);

                if ( tractType.equals(ShapefileTractUploadServlet.LEASE_TRACT_TYPE) ) {
                    // insert new DOC_LEASE_TRACT_HISTORY for tract with tractId
                    LeaseTractEntity tract = (LeaseTractEntity) EntityManagerHolder.getInstance().getEM().find(
                        LeaseTractEntity.class, tractId);
                    LeaseTractHistoryEntity hist = new LeaseTractHistoryEntity();
                    hist.tract = tract;
                    hist.geometryId = geometryId;
                    hist.userId = null;
                    hist.userName = null;
                    hist.created = new Date();
                    hist.note = "Tract updated from shapefile: " + filename;
                    EntityManagerHolder.getInstance().getEM().persist(hist);

                    // handle coverage layer
                    CoverageHelper coverageHelper = new CoverageHelper(DocumentService.LEASE_COVERAGE_TYPE);
                    coverageHelper.removeCoverage(tract.id);
                    coverageHelper.generateCoverage(tract.id, geometryId, null);

                } else if ( tractType.equals(ShapefileTractUploadServlet.SURFACE_TRACT_TYPE) ) {
                    // insert new DOC_LEASE_TRACT_HISTORY for tract with tractId
                    SurfaceTractEntity tract = (SurfaceTractEntity) EntityManagerHolder.getInstance().getEM().find(
                        SurfaceTractEntity.class, tractId);
                    SurfaceTractHistoryEntity hist = new SurfaceTractHistoryEntity();
                    hist.tract = tract;
                    hist.geometryId = geometryId;
                    hist.userId = null;
                    hist.userName = null;
                    hist.created = new Date();
                    hist.note = "Tract updated from shapefile: " + filename;
                    EntityManagerHolder.getInstance().getEM().persist(hist);
                }
            }        
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 3.5.");
            tr.commit();
            Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 4.");

        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("ShapefileTractUploadServlet.postUpload: exception caught.", ex);
            try {
                t.rollback();
                t.close();
                if ( null != tr ) {
                    tr.rollback();
                }
            } catch(IOException ex2) {
                throw new RuntimeException(ex2);
            }
            throw new RuntimeException(ex);
        } finally {
            try {
                Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 6.");
                if ( null != pgDataStore ) {
                    pgDataStore.dispose();
                }
                Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: checkpoint 7.");
            } catch (Throwable ex) {
                throw new RuntimeException(ex);
            }
        }
        Logger.getInstance().getLog().debug("ShapefileTractUploadServlet.postUpload: complete.");
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
