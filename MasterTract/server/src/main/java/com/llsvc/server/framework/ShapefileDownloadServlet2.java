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

public class ShapefileDownloadServlet2
    extends ShapefileDownloadServlet
{

    protected String getDefaultLayer() {
        return "doc_lease_tract_view";
    }

}
