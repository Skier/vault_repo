/**
 * $Id: VehicleHistoryTest.java 229 2007-05-31 09:10:58Z moritur $
 */
package com.affilia.cargo.test;

import java.sql.Timestamp;
import java.util.Date;

import util.sql.ConnectionManager;

import com.affilia.cargo.client.data.VehicleLogData;
import com.affilia.cargo.data.CargoDAOImpl;
import com.logicland.application.core.logger.LogHelper;

public class VehicleHistoryTest extends AbstractCargoTest  {

    private static final String VEHICLE_ITEM_ID = "new vehicle item id";
    private static final String VEHICLE_ITEM_OWNER = "new item owner";
    private static final String VEHICLE_NAME = "new vehicle name";
    
    public VehicleHistoryTest(String name)
        throws Exception  {
        super(name);
    }
    
    
    public void setUp() throws Exception {
        super.setUp();
        LogHelper.getLogger().debug("CargoDAOTest.setUp Called.");
        
        NetworkProcessor.getProcessor().fillup();
        
        VehicleProcessor.getProcessor().fillup();
        
        LogProcessor.getProcessor().networkId = 
            NetworkProcessor.getProcessor().getId();
        LogProcessor.getProcessor().vehicleId =
            VehicleProcessor.getProcessor().getId();
        LogProcessor.getProcessor().fillup();
        
        StatusLogProcessor.getProcessor().id = 
            LogProcessor.getProcessor().getId();
        StatusLogProcessor.getProcessor().fillup();
        
        UserProcessor.getProcessor().fillup();
    }

    private static final Date FROM_DATE = new Date(30000);
    private static final Date TO_DATE = new Date(50000);
    private static final long STEP = 10000;
    
    // * - point which exist in DB
    // . - poits which we wont see
    // | - point on  step
    

    //      TEST1:
    //
    //   ---*.--------------*-.---*------------.---   t (sec)
    //   fromDate                            toDate
    //     30                 40              50  

    private static final Date POINT1_TEST1 = new Date(29000);
    private static final Date POINT2_TEST1 = new Date(39000);
    private static final Date POINT3_TEST1 = new Date(43000);
    
    public void testVEhicalHistory1() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testVEhicalHistory1()");
        
        VehicleProcessor.getProcessor().makeVehicle(
                VehicleHistoryTest.VEHICLE_ITEM_ID,
                VehicleHistoryTest.VEHICLE_ITEM_OWNER,
                VehicleHistoryTest.VEHICLE_NAME);        
        
        Integer point0Id = makeVehicleLofForDatePoint(POINT1_TEST1);
        Integer point1Id = makeVehicleLofForDatePoint(POINT2_TEST1);
        Integer point2Id = makeVehicleLofForDatePoint(POINT3_TEST1);
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());

        VehicleLogData[] logDatas = 
            cargoDAO.getVehicleHistory(
                    VehicleProcessor.getProcessor().getId(), 
                    VehicleHistoryTest.FROM_DATE,
                    VehicleHistoryTest.TO_DATE,
                    VehicleHistoryTest.STEP);

        LogHelper.getLogger().debug("  logDatas.legth=" + logDatas.length);
        
        assertEquals(3, logDatas.length);
        assertEquals(point0Id.intValue(), logDatas[0].id.intValue());
        assertEquals(point1Id.intValue(), logDatas[1].id.intValue());
        assertEquals(point2Id.intValue(), logDatas[2].id.intValue());

        
    }

    //      TEST2:
    //
    //   ---*.--------------*-.----------------.-*-   t (sec)
    //   fromDate                            toDate
    //     30                 40              50  

    private static final Date POINT1_TEST2 = new Date(29000);
    private static final Date POINT2_TEST2 = new Date(39000);
    private static final Date POINT3_TEST2 = new Date(53000);
    
    public void testVEhicalHistory2() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testVEhicalHistory2()");
        
        VehicleProcessor.getProcessor().makeVehicle(
                VehicleHistoryTest.VEHICLE_ITEM_ID,
                VehicleHistoryTest.VEHICLE_ITEM_OWNER,
                VehicleHistoryTest.VEHICLE_NAME);        
        
        Integer point0Id = makeVehicleLofForDatePoint(POINT1_TEST2);
        Integer point1Id = makeVehicleLofForDatePoint(POINT2_TEST2);
        Integer point2Id = makeVehicleLofForDatePoint(POINT3_TEST2);
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());

        VehicleLogData[] logDatas = 
            cargoDAO.getVehicleHistory(
                    VehicleProcessor.getProcessor().getId(), 
                    VehicleHistoryTest.FROM_DATE,
                    VehicleHistoryTest.TO_DATE,
                    VehicleHistoryTest.STEP);

        LogHelper.getLogger().debug("  logDatas.legth=" + logDatas.length);
        
        assertEquals(3, logDatas.length);
        assertEquals(point0Id.intValue(), logDatas[0].id.intValue());
        assertEquals(point1Id.intValue(), logDatas[1].id.intValue());
        assertEquals(point2Id.intValue(), logDatas[2].id.intValue());

    }

    
    //      TEST3:
    //
    //   ---*.--------------*-.----------------.---   t (sec)
    //   fromDate                            toDate
    //     30                 40              50  

    private static final Date POINT1_TEST3 = new Date(29000);
    private static final Date POINT2_TEST3 = new Date(39000);
    
    public void testVEhicalHistory3() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testVEhicalHistory2()");
        
        VehicleProcessor.getProcessor().makeVehicle(
                VehicleHistoryTest.VEHICLE_ITEM_ID,
                VehicleHistoryTest.VEHICLE_ITEM_OWNER,
                VehicleHistoryTest.VEHICLE_NAME);        
        
        Integer point0Id = makeVehicleLofForDatePoint(POINT1_TEST3);
        Integer point1Id = makeVehicleLofForDatePoint(POINT2_TEST3);
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());

        VehicleLogData[] logDatas = 
            cargoDAO.getVehicleHistory(
                    VehicleProcessor.getProcessor().getId(), 
                    VehicleHistoryTest.FROM_DATE,
                    VehicleHistoryTest.TO_DATE,
                    VehicleHistoryTest.STEP);

        LogHelper.getLogger().debug("  logDatas.legth=" + logDatas.length);
        
        assertEquals(3, logDatas.length);
        assertEquals(point0Id.intValue(), logDatas[0].id.intValue());
        assertEquals(point1Id.intValue(), logDatas[1].id.intValue());
        assertNull(logDatas[2]);
    }

    private static final long LONG_STEP = 15000;
    
    //      TEST4:
    //       |                         |
    //   ---*.----------------------*---------.-*-   t (sec)
    //   fromDate                            toDate
    //     30                 40              50  

    private static final Date POINT1_TEST4 = new Date(29000);
    private static final Date POINT2_TEST4 = new Date(44000);
    private static final Date POINT3_TEST4 = new Date(53000);
    
    public void testVEhicalHistory4() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testVEhicalHistory2()");
        
        VehicleProcessor.getProcessor().makeVehicle(
                VehicleHistoryTest.VEHICLE_ITEM_ID,
                VehicleHistoryTest.VEHICLE_ITEM_OWNER,
                VehicleHistoryTest.VEHICLE_NAME);        
        
        Integer point0Id = makeVehicleLofForDatePoint(POINT1_TEST4);
        Integer point1Id = makeVehicleLofForDatePoint(POINT2_TEST4);
        Integer point2Id = makeVehicleLofForDatePoint(POINT3_TEST4);
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());

        VehicleLogData[] logDatas = 
            cargoDAO.getVehicleHistory(
                    VehicleProcessor.getProcessor().getId(), 
                    VehicleHistoryTest.FROM_DATE,
                    VehicleHistoryTest.TO_DATE,
                    VehicleHistoryTest.LONG_STEP);

        LogHelper.getLogger().debug("  logDatas.legth=" + logDatas.length);
        
        assertEquals(3, logDatas.length);
        assertEquals(point0Id.intValue(), logDatas[0].id.intValue());
        assertEquals(point1Id.intValue(), logDatas[1].id.intValue());
        assertEquals(point2Id.intValue(), logDatas[2].id.intValue());

    }

    //      TEST5:
    //       |                         |
    //   ---*.--------------------------------.-*-   t (sec)
    //   fromDate                            toDate
    //     30                 40              50  

    private static final Date POINT1_TEST5 = new Date(29000);
    private static final Date POINT2_TEST5 = new Date(53000);
    
    public void testVEhicalHistory5() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testVEhicalHistory2()");
        
        VehicleProcessor.getProcessor().makeVehicle(
                VehicleHistoryTest.VEHICLE_ITEM_ID,
                VehicleHistoryTest.VEHICLE_ITEM_OWNER,
                VehicleHistoryTest.VEHICLE_NAME);        
        
        Integer point0Id = makeVehicleLofForDatePoint(POINT1_TEST5);
        Integer point1Id = makeVehicleLofForDatePoint(POINT2_TEST5);
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());

        VehicleLogData[] logDatas = 
            cargoDAO.getVehicleHistory(
                    VehicleProcessor.getProcessor().getId(), 
                    VehicleHistoryTest.FROM_DATE,
                    VehicleHistoryTest.TO_DATE,
                    VehicleHistoryTest.LONG_STEP);

        LogHelper.getLogger().debug("  logDatas.legth=" + logDatas.length);
        
        assertEquals(3, logDatas.length);
        assertEquals(point0Id.intValue(), logDatas[0].id.intValue());
        assertEquals(point1Id.intValue(), logDatas[1].id.intValue());
        assertEquals(point1Id.intValue(), logDatas[2].id.intValue());

    }
    
    private Integer makeVehicleLofForDatePoint(Date datePoint) {
        Integer logId = LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(), 
                VehicleProcessor.getProcessor().getId(), 
                new Timestamp(datePoint.getTime()), 
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);
        StatusLogProcessor.getProcessor().makeStatusLog(
                logId,
                CargoDAOTest.NEW_LATITUDE, 
                CargoDAOTest.NEW_LONGITUDE, 
                CargoDAOTest.NEW_STATUS, 
                CargoDAOTest.NEW_TEMPERATURE, 
                CargoDAOTest.NEW_HUMIDITY);

        return logId;
    }

    
}
