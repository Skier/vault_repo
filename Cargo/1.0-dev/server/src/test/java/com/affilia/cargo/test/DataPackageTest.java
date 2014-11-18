/**
 * $Id: DataPackageTest.java 280 2007-06-20 09:06:07Z moritur $
 */
package com.affilia.cargo.test;

import com.affilia.cargo.server.DataPackage;
import com.logicland.application.core.logger.LogHelper;

public class DataPackageTest extends AbstractCargoTest {
    
    private static final double GEO_COORDINATE = 4830.150503;
    private static final double GOOGLE_COORDINATE = 48.50417;

    public DataPackageTest(String name)
        throws Exception  {
        super(name);
    }

    public void testConverGeograficalCoordinateToDecimal() {
        LogHelper.getLogger().debug("DataPackageTest.testConverGeograficalCoordinateToDecimal Called.");
        
        double decimalCoordinate = 
            DataPackage.converGeograficalCoordinateToDecimal(
                    DataPackageTest.GEO_COORDINATE);
        
        assertEquals( Math.round(DataPackageTest.GOOGLE_COORDINATE*100000),
                Math.round(decimalCoordinate*100000));
        
        
    }
    
    
    
}
