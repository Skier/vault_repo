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
import com.llsvc.server.framework.EntityManagerHolder;
import com.llsvc.server.framework.Logger;

/* 
GEO_GEOMETRY
REF_ID -> DOC_LEASE_TRACT.ID if STATUS=-1
REF_ID -> DOC_LEASE.ID if STATUS=1
REF_ID -> GEO_TRACT.ID if STATUS=0
*/
public class ShapefileUploadServlet 
    extends FileUploadServlet 
{
    public static final String GEOMETRY_FEATURE_TYPE = "geo_geometry";
    public static final String DOC_LEASE_TRACT_VIEW_TYPE = "doc_lease_tract_view";

    protected void postUpload(HttpServletRequest req, HttpServletResponse rsp, FileEntity f, String filename, String originalFilename) {
        String prefix = Configuration.getInstance().getTemporaryDirectory() + "/upload/";
        DataStore pgDataStore = null;
        Transaction t = new DefaultTransaction();
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();
        try {
            File baseFile = new File(filename);
            String baseFilename = originalFilename;
            String baseDirName = baseFilename.substring(0, baseFilename.length() - 4);
            File baseDir = new File(prefix + baseDirName);
            baseDir.mkdir();

            unzip(prefix + baseDirName, filename);

            filename = prefix + baseDirName + File.separator + baseDirName + ".shp";
            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: filename=" + filename);

            File file = new File(filename);
            if( !file.exists() ) {
                Logger.getInstance().getLog().error("ShapefileUploadServlet.postUpload: file not found " + filename);
                return;
            }

            Map connect = new HashMap();
            connect.put("url", file.toURL());

            DataStore dataStore = DataStoreFinder.getDataStore(connect);
            String[] typeNames = dataStore.getTypeNames();
            String typeName = typeNames[0];

            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: reading content "+ typeName);

//            FilterFactory filterFactory = FilterFactoryFinder.createFilterFactory();

            FeatureSource featureSource = dataStore.getFeatureSource(typeName);
            FeatureCollection collection = featureSource.getFeatures();

            pgDataStore = DataStoreFinder.getDataStore(getPostgisConfig());
//            FeatureStore pgFeatureStore = (FeatureStore) pgDataStore.getFeatureSource(GEOMETRY_FEATURE_TYPE);
            FeatureSource pgFeatureSource = pgDataStore.getFeatureSource(DOC_LEASE_TRACT_VIEW_TYPE);
//            pgFeatureStore.addFeatures(collection);
            FeatureWriter fw = pgDataStore.getFeatureWriterAppend(GEOMETRY_FEATURE_TYPE, t);

            FeatureIterator iterator = collection.features();

            while( iterator.hasNext() ){
                Feature feature = iterator.next();
                Object leaseId = feature.getAttribute(1);
                Object tractId = feature.getAttribute(2);
                Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: leaseId=" + leaseId);
                Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: tractId=" + tractId);
                
                if ( null == tractId /*|| true*/ ) {
                    Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: insert orphan shape.");
                    // insert new GEO_GEOMETRY, REF_ID=leaseId, STATUS=1
                    Feature df = fw.next();
                    df.setDefaultGeometry(feature.getDefaultGeometry());
                    df.setAttribute("status", new Integer(1));
                    df.setAttribute("changed", new java.util.Date());
                    df.setAttribute("ref_id", leaseId);
                    fw.write();
                } else {
                    FilterFactory2 ff = CommonFactoryFinder.getFilterFactory2(null);
                    Filter filter = CQL.toFilter("tract_id="+tractId);
                            // ff.id(Collections.singleton(ff.featureId(tractId.toString())));

                    DefaultQuery query = new DefaultQuery();
                    query.setTypeName(DOC_LEASE_TRACT_VIEW_TYPE);
                    query.setFilter(filter);
                    FeatureCollection result = pgFeatureSource.getFeatures(query);
                    if ( null != result && 1 == result.size() ) {
                        // tract found, check geometry
                        Feature foundFeature = null;
                        Iterator i2 = null;
                        try {
                            for( i2=result.iterator(); i2.hasNext(); ) {
                                foundFeature = (Feature) i2.next();
                                break;
                            }
                        } finally {
                            result.close(i2);
                        }
                        if ( null != foundFeature && !foundFeature.getDefaultGeometry().equals(feature.getDefaultGeometry()) ) {
                            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: insert tract historical shape.");
                            // insert new GEO_GEOMETRY, REF_ID=tractId, STATUS=-1
                            Feature df = fw.next();
                            df.setDefaultGeometry(feature.getDefaultGeometry());
                            df.setAttribute("status", new Integer(-1));
                            df.setAttribute("changed", new java.util.Date());
                            df.setAttribute("ref_id", tractId);
                            fw.write();
                            Integer fid = new Integer(df.getID().substring(df.getID().indexOf(".")+1));
                            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: df.id=" + fid);

                            // insert new DOC_LEASE_TRACT_HISTORY for tract with tractId
                            LeaseTractEntity tract = (LeaseTractEntity) EntityManagerHolder.getInstance().getEM().find(
                                LeaseTractEntity.class, new Integer(tractId.toString()));
                            LeaseTractHistoryEntity hist = new LeaseTractHistoryEntity();
                            hist.tract = tract;
                            hist.geometryId = fid;
                            hist.userId = null;
                            hist.userName = null;
                            hist.created = new Date();
                            hist.note = "Updated from shapefile: " + filename;
                            EntityManagerHolder.getInstance().getEM().persist(hist);
                        } else {
                            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: new geometry is equal to lms, skipped.");
                        }
                    } else {
                        Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: insert orphan shape 2.");
                        // tract is not found, create orphan
                        // insert new GEO_GEOMETRY, REF_ID=leaseId, STATUS=1
                        Feature df = fw.next();
                        df.setDefaultGeometry(feature.getDefaultGeometry());
                        df.setAttribute("status", new Integer(1));
                        df.setAttribute("changed", new java.util.Date());
                        df.setAttribute("ref_id", leaseId);
                        fw.write();
                    }
                }

            }
            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 0.");
            iterator.close();
            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 1.");
            fw.close();
            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 2.");
            t.commit();
            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 3.");
            tr.commit();
            Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 4.");
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("ShapefileUploadServlet.postUpload: exception caught.", ex);
            try {
                t.rollback();
                tr.rollback();
            } catch(IOException ex2) {
                throw new RuntimeException(ex2);
            }
            throw new RuntimeException(ex);
        } finally {
            try {
                Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 5.");
                t.close();
                Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 6.");
                if ( null != pgDataStore ) {
                    pgDataStore.dispose();
                }
                Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: checkpoint 7.");
            } catch (Throwable ex) {
                throw new RuntimeException(ex);
            }
        }
        Logger.getInstance().getLog().debug("ShapefileUploadServlet.postUpload: complete.");
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
