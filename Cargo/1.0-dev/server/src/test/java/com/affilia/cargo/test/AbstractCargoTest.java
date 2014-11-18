/**
 * $Id: AbstractCargoTest.java 251 2007-06-05 13:40:29Z moritur $
 */
package com.affilia.cargo.test;

import com.logicland.application.core.logger.LogHelper;
import util.testhelper.TestHelper;
import junit.framework.TestCase;

public class AbstractCargoTest 
    extends TestCase {
    
    private static final String CONF_FILE = "cargo-test.properties";

    public AbstractCargoTest(String name) 
        throws Exception {
        super(name);
        TestHelper.initTestSuite(
            com.affilia.cargo.test.Module.getInstance().getName(),
            AbstractCargoTest.CONF_FILE);
    }

    public void setUp() 
        throws Exception {
        super.setUp();
        LogHelper.getLogger().debug("AbstractCargoTest.setUp: Settting up...");
        cleanDatabase();
        LogHelper.getLogger().debug("AbstractCargoTest.setUp: Setted up.");
    }

    public void tearDown() 
        throws Exception {
        LogHelper.getLogger().debug("AbstractCargoTest.tearDown: Tearring down...");
//        cleanDatabase();
        LogHelper.getLogger().debug("AbstractCargoTest.tearDown: Tearred down.");
    }

    protected void cleanDatabase() 
    throws Exception {
        LogHelper.getLogger().debug("AbstractCargoTest.cleanDatabase: Cleaning database...");
        

        TestHelper.finalize(NetworkVehicleProcessor.getProcessor());

        TestHelper.finalize(NetworkLogProcessor.getProcessor());
                
        TestHelper.finalize(StatusLogProcessor.getProcessor());
        //TestHelper.finalize(TripLogProcessor.getProcessor());
        TestHelper.finalize(LogProcessor.getProcessor());

        TestHelper.finalize(NetworkProcessor.getProcessor());
        TestHelper.finalize(VehicleProcessor.getProcessor());
        
        TestHelper.finalize(UserProcessor.getProcessor());
        
    }
    
}
