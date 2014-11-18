/**
 * $Id: CargoDAOTest.java 274 2007-06-16 13:08:32Z moritur $
 */
package com.affilia.cargo.test;

import java.sql.Timestamp;

import util.sql.ConnectionManager;

import com.affilia.cargo.client.data.LogData;
import com.affilia.cargo.client.data.NetworkData;
import com.affilia.cargo.client.data.StatusLogData;
import com.affilia.cargo.client.data.UserData;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleJoinLogData;
import com.affilia.cargo.client.data.VehicleLogData;
import com.affilia.cargo.data.CargoDAO;
import com.affilia.cargo.data.CargoDAOImpl;
import com.logicland.application.core.logger.LogHelper;
import com.logicland.application.core.util.DateHelper;

public class CargoDAOTest    
    extends AbstractCargoTest {
    
    public static final String NEW_LATITUDE = "-20.36";
    public static final String NEW_LONGITUDE = "42.89";
    public static final String NEW_STATUS = "unknown";
    public static final String NEW_TEMPERATURE = "23C";
    public static final String NEW_HUMIDITY = "65%";
    public static final String NEW_USER_NAME = "cargo";
    public static final String NEW_PASSWORD = "password";
    
    public static final Timestamp NEW_LOG_TIME = new Timestamp(DateHelper.getDate(2008,8,9).getTime()); 
    public static final Integer NEW_LOG_LEVEL = new Integer(2);  
    public static final String NEW_LOG_TEXT = "CARGO MOVE TO POINT C";
    
    public static final Boolean NEW_IS_JOIN = Boolean.TRUE;
    public static final Boolean NEW_IS_LEAVE = Boolean.TRUE;
    public static final Boolean NEW_IS_AUTHORIZED = Boolean.TRUE;
    
    private static final String NEW_NETWORK_NAME = "new network name";
    
    private static final String VEHICLE_ITEM_ID = "new vehicle item id";
    private static final String VEHICLE_ITEM_OWNER = "new item owner";
    private static final String VEHICLE_NAME = "new vehicle name";
    
    public CargoDAOTest(String name)
        throws Exception  {
        super(name);
    }
    
    public void setUp() throws Exception {
        super.setUp();
        LogHelper.getLogger().debug("CargoDAOTest.setUp Called.");
        
        NetworkProcessor.getProcessor().fillup();
        
        VehicleProcessor.getProcessor().fillup();
        
        NetworkVehicleProcessor.getProcessor().networkId = 
            NetworkProcessor.getProcessor().getId();
        NetworkVehicleProcessor.getProcessor().vehicleId = 
            VehicleProcessor.getProcessor().getId();
        NetworkVehicleProcessor.getProcessor().fillup(); 
        
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

    public void testGetVehiclesByNetwork() throws Exception {
        LogHelper.getLogger().debug("CargoDAOTest.testGetVehiclesByNetwork");
        
        Integer networkId = NetworkProcessor.getProcessor().getId();
        
        NetworkProcessor.getProcessor().makeNetwork(CargoDAOTest.NEW_NETWORK_NAME);
        VehicleProcessor.getProcessor().makeVehicle(
                CargoDAOTest.VEHICLE_ITEM_ID, 
                CargoDAOTest.VEHICLE_ITEM_OWNER,
                CargoDAOTest.VEHICLE_NAME);

        VehicleData[] vehiclesData = createDAO().getVehiclesByNetwork(networkId);
        
        assertEquals(1, vehiclesData.length);

    
    }
    
    
    public void testGetAllNetworks() throws Exception {
        LogHelper.getLogger().debug("CargoDAOTest.testStore");
        
        NetworkProcessor.getProcessor().makeNetwork(CargoDAOTest.NEW_NETWORK_NAME);
        
        NetworkMokcObject[] networks = 
            (NetworkMokcObject[]) NetworkProcessor.getProcessor().getAll();
                
        NetworkData[] networksData = createDAO().getAllNetworks();
        
        LogHelper.getLogger().debug("   networksData.length="+networksData.length);
        LogHelper.getLogger().debug("   networks.length="+networks.length);
        
        assertEquals(networks.length, networksData.length);
        
    }
    
    public void testGetVehicleStatus() throws Exception {
        
        Integer vehicleId = VehicleProcessor.getProcessor().makeVehicle(
                CargoDAOTest.VEHICLE_ITEM_ID, 
                CargoDAOTest.VEHICLE_ITEM_OWNER, 
                CargoDAOTest.VEHICLE_NAME);
        
        Integer logId = LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(), 
                vehicleId, 
                CargoDAOTest.NEW_LOG_TIME, 
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);
        
        StatusLogProcessor.getProcessor().makeStatusLog(
                logId,
                CargoDAOTest.NEW_LATITUDE, 
                CargoDAOTest.NEW_LONGITUDE, 
                CargoDAOTest.NEW_STATUS, 
                CargoDAOTest.NEW_TEMPERATURE, 
                CargoDAOTest.NEW_HUMIDITY);
        
        
        LogHelper.getLogger().debug(
                "CargoDAOTest.testGetUnitStatus(): vehicleId = " + vehicleId);

        StatusLogData vehicleStatus = createDAO().getVehicleStatus(vehicleId);
        
        assertEquals(CargoDAOTest.NEW_LATITUDE, vehicleStatus.latitude);
        assertEquals(CargoDAOTest.NEW_LONGITUDE, vehicleStatus.longtude);
        assertEquals(CargoDAOTest.NEW_STATUS, vehicleStatus.status);
        assertEquals(CargoDAOTest.NEW_TEMPERATURE, vehicleStatus.temperature);
        assertEquals(CargoDAOTest.NEW_HUMIDITY, vehicleStatus.humidity);
        
    }
    
    public void testGetLogInformation() throws Exception {
        
        int iteration = 2;
        
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetLogInformation()");
        
        Integer logId = LogProcessor.getProcessor().getId();
        
        for (int i = 0; i < iteration; i++) {
            LogHelper.getLogger().debug("*");
            LogProcessor.getProcessor().makeLog(
                    NetworkProcessor.getProcessor().getId(),
                    VehicleProcessor.getProcessor().getId(), 
                    CargoDAOTest.NEW_LOG_TIME, 
                    CargoDAOTest.NEW_LOG_LEVEL, 
                    CargoDAOTest.NEW_LOG_TEXT);
        }

        LogData[] logInformation = createDAO().getLogInformation(new Long(logId));
        
        assertEquals(logInformation.length, iteration);
    }
    
    public void testGetMaxLogId() throws Exception {

        LogHelper.getLogger().debug("Called CargoDAOTest.testGetMaxLogId()");

        Integer logId = LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(),
                VehicleProcessor.getProcessor().getId(), 
                CargoDAOTest.NEW_LOG_TIME, 
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);

        Long maxLogId = createDAO().getMaxLogId();

        assertEquals(logId, new Integer(maxLogId.intValue()));
    }
    
    public void testGetVehicle() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetMaxLogId()");

        Integer logId = LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(), 
                VehicleProcessor.getProcessor().getId(), 
                CargoDAOTest.NEW_LOG_TIME, 
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);
        StatusLogProcessor.getProcessor().makeStatusLog(
                logId,
                CargoDAOTest.NEW_LATITUDE, 
                CargoDAOTest.NEW_LONGITUDE, 
                CargoDAOTest.NEW_STATUS, 
                CargoDAOTest.NEW_TEMPERATURE, 
                CargoDAOTest.NEW_HUMIDITY);
        VehicleMockObject vehicle = (VehicleMockObject) VehicleProcessor.getProcessor().getOne(
                VehicleProcessor.getProcessor().getId());

        VehicleData vehicleData = createDAO().getVehicle(vehicle.itemId);
        
        assertEquals(vehicle.id, vehicleData.id);
        assertEquals(vehicle.itemId, vehicleData.itemId);
        assertEquals(vehicle.itemOwner, vehicleData.itemOwner);
        assertEquals(vehicle.name, vehicleData.name);
        assertEquals(CargoDAOTest.NEW_LATITUDE, new Double(vehicleData.latitude).toString());
        assertEquals(CargoDAOTest.NEW_LONGITUDE, new Double(vehicleData.longitude).toString());
        assertEquals(CargoDAOTest.NEW_STATUS, vehicleData.status);
        assertEquals(CargoDAOTest.NEW_TEMPERATURE, vehicleData.temperature);
        assertEquals(CargoDAOTest.NEW_HUMIDITY, vehicleData.humidity);
        assertEquals(NetworkProcessor.getProcessor().getId(), vehicleData.networkId);
        
    }

    public void testGetVehicleHistory() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetVehicleHistory()");

        Integer logId = LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(), 
                VehicleProcessor.getProcessor().getId(), 
                CargoDAOTest.NEW_LOG_TIME, 
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);
        StatusLogProcessor.getProcessor().makeStatusLog(
                logId,
                CargoDAOTest.NEW_LATITUDE, 
                CargoDAOTest.NEW_LONGITUDE, 
                CargoDAOTest.NEW_STATUS, 
                CargoDAOTest.NEW_TEMPERATURE, 
                CargoDAOTest.NEW_HUMIDITY);
        
        VehicleLogData[] vehicleLogDatas = createDAO().getVehicleHistory(
                VehicleProcessor.getProcessor().getId());
        assertNotNull(vehicleLogDatas);
        assertEquals(2, vehicleLogDatas.length);
        
    }

    public void testGetVehicalLogDataAfterTime() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetVehicalLogDataAfterTime()");

        Integer logId = LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(), 
                VehicleProcessor.getProcessor().getId(), 
                CargoDAOTest.NEW_LOG_TIME, 
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);
        StatusLogProcessor.getProcessor().makeStatusLog(
                logId,
                CargoDAOTest.NEW_LATITUDE, 
                CargoDAOTest.NEW_LONGITUDE, 
                CargoDAOTest.NEW_STATUS, 
                CargoDAOTest.NEW_TEMPERATURE, 
                CargoDAOTest.NEW_HUMIDITY);

        VehicleLogData vehicleLogDatas = createDAO().getVehicalLogDataAfterTime(
                VehicleProcessor.getProcessor().getId(),
                CargoDAOTest.NEW_LOG_TIME);
        
        assertNotNull(vehicleLogDatas);
        assertEquals(logId.intValue(), vehicleLogDatas.id.intValue());
        
    }

    public void testGetVehicalLogDataBeforeTime() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetVehicalLogDataBeforeTime()");

        Integer logId = LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(), 
                VehicleProcessor.getProcessor().getId(), 
                CargoDAOTest.NEW_LOG_TIME, 
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);
        StatusLogProcessor.getProcessor().makeStatusLog(
                logId,
                CargoDAOTest.NEW_LATITUDE, 
                CargoDAOTest.NEW_LONGITUDE, 
                CargoDAOTest.NEW_STATUS, 
                CargoDAOTest.NEW_TEMPERATURE, 
                CargoDAOTest.NEW_HUMIDITY);
        
        VehicleLogData vehicleLogDatas = createDAO().getVehicalLogDataBeforeTime(
                VehicleProcessor.getProcessor().getId(),
                CargoDAOTest.NEW_LOG_TIME);
        
        assertNotNull(vehicleLogDatas);
        assertEquals(logId.intValue(), vehicleLogDatas.id.intValue());
        assertEquals(CargoDAOTest.NEW_LOG_TIME.getTime(), CargoDAOTest.NEW_LOG_TIME.getTime());
        
    }
    
    public void testGetUsers() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetUsers()");
        UserProcessor.getProcessor().makeUser(CargoDAOTest.NEW_USER_NAME, 
                CargoDAOTest.NEW_PASSWORD);
        
        UserData[] users = createDAO().getUsers();
        
        assertNotNull(users);
        assertEquals(2, users.length);
        
    }
    
    public void testGetUser() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetUser()");
        Integer id = UserProcessor.getProcessor().getId();
        
        UserData user = createDAO().getUser(id);
        
        assertNotNull(user);
        assertEquals(UserProcessor.USER_NAME, user.userName);
        assertEquals(UserProcessor.PASSWORD, user.password);
    }
    
    public void testGetUserByName() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetUserByName()");
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());
        
        UserData user = cargoDAO.getUser(UserProcessor.USER_NAME);
        
        assertNotNull(user);
        assertEquals(UserProcessor.USER_NAME, user.userName);
        assertEquals(UserProcessor.PASSWORD, user.password);
    }

    
    public void testCreateUser() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testCreateUser()");
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());
        
        cargoDAO.storeUser(null, CargoDAOTest.NEW_USER_NAME, 
                CargoDAOTest.NEW_PASSWORD);
        
        UserData user = cargoDAO.getUser(CargoDAOTest.NEW_USER_NAME);
        
        assertNotNull(user);
        assertEquals(CargoDAOTest.NEW_USER_NAME, user.userName);
        assertEquals(CargoDAOTest.NEW_PASSWORD, user.password);
    }

    
    public void testStoreUser() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testStoreUser()");
        
        Integer id = UserProcessor.getProcessor().getId();
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());
        
        cargoDAO.storeUser(id, CargoDAOTest.NEW_USER_NAME, 
                CargoDAOTest.NEW_PASSWORD);
        
        UserData user = cargoDAO.getUser(id);
        
        assertNotNull(user);
        assertEquals(CargoDAOTest.NEW_USER_NAME, user.userName);
        assertEquals(CargoDAOTest.NEW_PASSWORD, user.password);
    }
    
    public void testRemoveUser() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testRemoveUser()");
        
        Integer id = UserProcessor.getProcessor().getId();
        
        CargoDAOImpl cargoDAO = 
            new CargoDAOImpl(ConnectionManager.getConnection());
        
        cargoDAO.removeUser(id);
        
        Object[] user = UserProcessor.getProcessor().getAll();
        
        assertEquals(0, user.length);
    }



    public void testStoreVehicleInfoLog() {
        LogHelper.getLogger().debug("Called CargoDAOTest.testStoreVehicleInfoLog()");

        VehicleLogData vehicleLogData = new VehicleLogData();        
        vehicleLogData.networkId = NetworkProcessor.getProcessor().getId();
        vehicleLogData.vehicleId = VehicleProcessor.getProcessor().getId();
        vehicleLogData.logTime = CargoDAOTest.NEW_LOG_TIME;
        vehicleLogData.logLevel = CargoDAOTest.NEW_LOG_LEVEL;
        vehicleLogData.logText = CargoDAOTest.NEW_LOG_TEXT;
        vehicleLogData.latitude = new Double(CargoDAOTest.NEW_LATITUDE);
        vehicleLogData.longitude = new Double(CargoDAOTest.NEW_LONGITUDE);
        vehicleLogData.status = CargoDAOTest.NEW_STATUS;
        vehicleLogData.temperature = CargoDAOTest.NEW_TEMPERATURE;
        vehicleLogData.humidity = CargoDAOTest.NEW_HUMIDITY;
        
        Integer logId = createDAO().storeVehicleInfoLog(vehicleLogData);
        
        LogMockObject log = (LogMockObject) LogProcessor.getProcessor().getOne(logId);
        StatusLogMockObject statusLog = 
            (StatusLogMockObject) StatusLogProcessor.getProcessor().getOne(logId);

        assertEquals(NetworkProcessor.getProcessor().getId(), log.networkId);
        assertEquals(VehicleProcessor.getProcessor().getId(), log.vehicleId);
        assertEquals(CargoDAOTest.NEW_LOG_TIME, log.logTime);
        assertEquals(CargoDAOTest.NEW_LOG_LEVEL, log.logLevel);
        assertEquals(CargoDAOTest.NEW_LOG_TEXT, log.logText);
        assertEquals(CargoDAOTest.NEW_LATITUDE, statusLog.latitude);
        assertEquals(CargoDAOTest.NEW_LONGITUDE, statusLog.longitude);
        assertEquals(CargoDAOTest.NEW_STATUS, statusLog.status);
        assertEquals(CargoDAOTest.NEW_TEMPERATURE, statusLog.temperature);
        assertEquals(CargoDAOTest.NEW_HUMIDITY, statusLog.humidity);
    }

    
    public void testStoreVehicleJoinToNetworkLog() {
        LogHelper.getLogger().debug("Called CargoDAOTest.testStoreVehicleJoinToNetworkLog()");

        Integer networkId = NetworkProcessor.getProcessor().makeNetwork(
                CargoDAOTest.NEW_NETWORK_NAME);
        Integer vehicleId = VehicleProcessor.getProcessor().makeVehicle(
                CargoDAOTest.VEHICLE_ITEM_ID,
                CargoDAOTest.VEHICLE_ITEM_OWNER,
                CargoDAOTest.VEHICLE_NAME);
        
        VehicleJoinLogData logData = new VehicleJoinLogData();        
        logData.networkId = networkId;
        logData.vehicleId = vehicleId;
        logData.logTime = CargoDAOTest.NEW_LOG_TIME;
        logData.logLevel = CargoDAOTest.NEW_LOG_LEVEL;
        logData.logText = CargoDAOTest.NEW_LOG_TEXT;
        logData.isJoin = CargoDAOTest.NEW_IS_JOIN;
        logData.isLeave = CargoDAOTest.NEW_IS_LEAVE;
        logData.isAuthorized = CargoDAOTest.NEW_IS_AUTHORIZED;
        
        Integer logId = createDAO().storeVehicleJoinToNetworkLog(logData);
        
        NetworkVehicleMockObject[] networkVehicles = 
            (NetworkVehicleMockObject[]) NetworkVehicleProcessor.getProcessor().getAll();
        assertEquals(2, networkVehicles.length);
        
        LogMockObject log = (LogMockObject) LogProcessor.getProcessor().getOne(logId);
        
        assertEquals(NetworkProcessor.getProcessor().getId(), log.networkId);
        assertEquals(VehicleProcessor.getProcessor().getId(), log.vehicleId);
        assertEquals(CargoDAOTest.NEW_LOG_TIME, log.logTime);
        assertEquals(CargoDAOTest.NEW_LOG_LEVEL, log.logLevel);
        assertEquals(CargoDAOTest.NEW_LOG_TEXT, log.logText);

        NetworkLogMockObject networkLog = 
            (NetworkLogMockObject) NetworkLogProcessor.getProcessor().getOne(logId);
        
        assertEquals(CargoDAOTest.NEW_IS_JOIN, networkLog.isJoin);
        assertEquals(CargoDAOTest.NEW_IS_LEAVE, networkLog.isLeave);
        assertEquals(CargoDAOTest.NEW_IS_AUTHORIZED, networkLog.isAuthorized);

    }
    
    
    
    private final static Timestamp FROM_TIME = new Timestamp(
            CargoDAOTest.NEW_LOG_TIME.getTime() + 20000);
    
    public void testGetVehiclesWhichNotRespond() {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetVehiclesWhichNotRespond()");
        
        // set start up data
        VehicleMockObject oldVehicle = (VehicleMockObject) VehicleProcessor.getProcessor().getOne(
                VehicleProcessor.getProcessor().getId());
        
        Integer newVehicleId = VehicleProcessor.getProcessor().makeVehicle(
                CargoDAOTest.VEHICLE_ITEM_ID, 
                CargoDAOTest.VEHICLE_ITEM_OWNER,
                CargoDAOTest.VEHICLE_NAME);
        
        NetworkVehicleProcessor.getProcessor().networkId =
            NetworkProcessor.getProcessor().getId();
        NetworkVehicleProcessor.getProcessor().vehicleId = newVehicleId;
        NetworkVehicleProcessor.getProcessor().makeNetworkVehicle();
        
        LogProcessor.getProcessor().makeLog(
                NetworkProcessor.getProcessor().getId(), 
                newVehicleId, 
                new Timestamp(
                        CargoDAOTest.FROM_TIME.getTime() + 1000),
                CargoDAOTest.NEW_LOG_LEVEL, 
                CargoDAOTest.NEW_LOG_TEXT);
        
        // use testing function
        
        VehicleData[] vehicles =
            createDAO().getVehiclesWhichNotRespond(CargoDAOTest.FROM_TIME);
        
        // compare results:
        
        assertEquals(1, vehicles.length);
        assertEquals(oldVehicle.id, vehicles[0].id);
        assertEquals(NetworkProcessor.getProcessor().getId(), vehicles[0].networkId);
        assertEquals(oldVehicle.itemId, vehicles[0].itemId);
        assertEquals(oldVehicle.itemOwner, vehicles[0].itemOwner);
        assertEquals(oldVehicle.name, vehicles[0].name);

    }
    
    public void testStoreVehicleNotRespondLog() {
        LogHelper.getLogger().debug("Called CargoDAOTest.testStoreVehicleNotRespondLog()");
        
        
        VehicleJoinLogData logData = new VehicleJoinLogData();
        logData.networkId = NetworkProcessor.getProcessor().getId();
        logData.vehicleId = VehicleProcessor.getProcessor().getId();
        logData.logTime = CargoDAOTest.NEW_LOG_TIME;
        logData.logLevel = CargoDAOTest.NEW_LOG_LEVEL;
        logData.logText = CargoDAOTest.NEW_LOG_TEXT;
        logData.isJoin = CargoDAOTest.NEW_IS_JOIN;
        logData.isLeave = CargoDAOTest.NEW_IS_LEAVE;
        logData.isAuthorized = CargoDAOTest.NEW_IS_AUTHORIZED;
        
        Integer lodId = createDAO().storeVehicleNotRespondLog(logData);
        
        LogMockObject log = (LogMockObject) LogProcessor.getProcessor().getOne(lodId);
        
        assertEquals(NetworkProcessor.getProcessor().getId(), log.networkId);
        assertEquals(VehicleProcessor.getProcessor().getId(), log.vehicleId);
        assertEquals(CargoDAOTest.NEW_LOG_TIME, log.logTime);
        assertEquals(CargoDAOTest.NEW_LOG_LEVEL, log.logLevel);
        assertEquals(CargoDAOTest.NEW_LOG_TEXT, log.logText);

        NetworkLogMockObject networkLog = 
            (NetworkLogMockObject) NetworkLogProcessor.getProcessor().getOne(lodId);
        
        assertEquals(CargoDAOTest.NEW_IS_JOIN, networkLog.isJoin);
        assertEquals(CargoDAOTest.NEW_IS_LEAVE, networkLog.isLeave);
        assertEquals(CargoDAOTest.NEW_IS_AUTHORIZED, networkLog.isAuthorized);
        
        NetworkVehicleMockObject[] netVeh = 
            (NetworkVehicleMockObject[]) NetworkVehicleProcessor.getProcessor().getAll();
        assertEquals(0, netVeh.length);

    }
    
    public void testGetVehicleNetworke() throws Exception {
        LogHelper.getLogger().debug("Called CargoDAOTest.testGetVehicleNetworke()");
        
        NetworkData networkData = createDAO().getVehicleNetwork(
                VehicleProcessor.getProcessor().getId());
        
        assertNotNull(networkData);
        assertEquals(NetworkProcessor.getProcessor().getId(), networkData.id);
        assertEquals(NetworkProcessor.getProcessor().name, networkData.name);
    }
    
    protected CargoDAO createDAO() {
        return new CargoDAOImpl(ConnectionManager.getConnection());
    }

}
