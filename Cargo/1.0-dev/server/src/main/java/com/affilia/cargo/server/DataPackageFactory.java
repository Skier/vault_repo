/**
 * $Id: DataPackageFactory.java 246 2007-06-04 15:18:07Z moritur $
 */

package com.affilia.cargo.server;

import java.util.StringTokenizer;

import com.logicland.application.core.logger.LogHelper;

public class DataPackageFactory 
{
    private static final DataPackageFactory instance = new DataPackageFactory();
    
    public static DataPackageFactory getInstance() {
        return instance;
    }
    
    private DataPackageFactory() {
    }

    public DataPackage createPackage(Integer networkId, String data) {
        LogHelper.getLogger().debug("DataPackageFactory: data=" + data);
        StringTokenizer st = new StringTokenizer(data, ";");
        String packageType = st.nextToken();
        LogHelper.getLogger().debug("DataPackageFactory: packageType=" + packageType);

        DataPackage result = null;
        if ( "I".equals(packageType) ) {
            result = new InfoDataPackage(networkId, data);
        } else if ( "J".equals(packageType) ) {
            result = new JoinDataPackage(networkId, data);
        }
        

        if ( null != result ) {
            result.parse();
            return result;
        } else {
            throw new RuntimeException("Unsupported package type: " + packageType);
        }
    }

}
