/**
 * $Id: CargoDAOImpl.java 274 2007-06-16 13:08:32Z moritur $
 */
package com.affilia.cargo.data;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.Timestamp;
import java.util.Date;
import java.util.LinkedList;
import java.util.List;
import com.affilia.cargo.client.data.LogData;
import com.affilia.cargo.client.data.NetworkData;
import com.affilia.cargo.client.data.StatusLogData;
import com.affilia.cargo.client.data.UserData;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleJoinLogData;
import com.affilia.cargo.client.data.VehicleLogData;
import com.logicland.application.core.logger.LogHelper;
import com.logicland.application.core.util.DateHelper;
import com.logicland.gwt.util.client.system.ClientException;

public class CargoDAOImpl 
    implements CargoDAO {
    
    private final static String VEHICLES_DATA_SELECT = 
        " SELECT ve.id, " +
        "       ve.item_id, " +
        "       ve.item_owner, " +
        "       ve.name, " +
        "      (SELECT sl.latitude " + 
        "       FROM cargo_status_log sl " + 
        "           INNER JOIN cargo_log l ON sl.id = l.id " +  
        "       WHERE l.vehicle_id = ve.id " + 
        "       ORDER BY l.id DESC LIMIT 1 ) AS latitude, " + 
        "      (SELECT sl.LONGITUDE " +
        "       FROM cargo_status_log sl " +
        "           INNER JOIN cargo_log l ON sl.id = l.id " +
        "       WHERE l.vehicle_id = ve.id " +
        "       ORDER BY l.id DESC LIMIT 1 ) AS longitude, " +

        "      (SELECT sl.STATUS " +
        "       FROM cargo_status_log sl " +
        "           INNER JOIN cargo_log l ON sl.id = l.id " +
        "       WHERE l.vehicle_id = ve.id " +
        "       ORDER BY l.id DESC LIMIT 1 ) AS status, " +
        "      (SELECT sl.TEMPERATURE " +
        "       FROM cargo_status_log sl " +
        "           INNER JOIN cargo_log l ON sl.id = l.id " +
        "       WHERE l.vehicle_id = ve.id " +
        "       ORDER BY l.id DESC LIMIT 1 ) AS temperature, " +
        "      (SELECT sl.HUMIDITY " +
        "       FROM cargo_status_log sl " +
        "           INNER JOIN cargo_log l ON sl.id = l.id " +
        "       WHERE l.vehicle_id = ve.id " +
        "       ORDER BY l.id DESC LIMIT 1 ) AS humidity, " +
        "      (SELECT netveh.network_id " +
        "       FROM cargo_network_vehicle netveh " +
        "       WHERE netveh.vehicle_id = ve.id ) AS network_id " +
        " FROM cargo_vehicle ve "; 
    
    
    private final static String VEHICLES_LOG_DATA_SELECT =
        " SELECT cl.id, " +
        "   cl.log_time, " +
        "   cl.log_level, " +
        "   cl.log_text, " +
        "   csl.latitude, " +
        "   csl.longitude, " +
        "   csl.status, " +
        "   csl.temperature, " +
        "   csl.humidity " +
        " FROM cargo_log cl " +
        "   inner join cargo_status_log csl on cl.id = csl.id ";
        

    private Connection m_connection;
    
    public CargoDAOImpl(Connection connection) {
        m_connection = connection;
    }
    
    public NetworkData[] getAllNetworks() {
        LogHelper.getLogger().debug("CargoDAOImpl.getAllNetworks() call");

        List<NetworkData> result = new LinkedList<NetworkData>();
        try {
            ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                    "select ID, NAME " +
                    "  from CARGO_NETWORK " +
                    " order by NAME");
                rs = stmt.executeQuery();
                while ( rs.next() ) {
                    NetworkData data = new NetworkData();
                    data.id = new Integer(rs.getInt("ID"));
                    data.name = rs.getString("NAME");
                    result.add(data);
                }

            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) { 
            throw new RuntimeException(ex);
        }
    
        return result.toArray(new NetworkData[0]);
    }
    
    public StatusLogData getVehicleStatus(Integer vehicleId) {
        StatusLogData data = new StatusLogData();
        try {
        	ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                	"SELECT csl.id, " +
                	"   csl.latitude, " +
                	"   csl.longitude, " +
                	"   csl.status, " +
                	"   csl.temperature, " +
                	"   csl.humidity " +
                	"FROM cargo_status_log csl " + 
                	"INNER JOIN cargo_log cl ON csl.id=cl.id " + 
                	"INNER JOIN cargo_vehicle cv ON cl.vehicle_id=cv.id " + 
                	"WHERE cv.id=?");
                		
                stmt.setInt(1, vehicleId);
                rs = stmt.executeQuery();
                while ( rs.next() ) {
                	data.id = new Long(rs.getLong("ID"));
                	data.latitude = rs.getString("LATITUDE");
                	data.longtude = rs.getString("LONGITUDE");
                	data.status = rs.getString("STATUS");
                	data.temperature = rs.getString("TEMPERATURE");
                	data.humidity = rs.getString("HUMIDITY");
                }
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
        
        return data;
    }
    
    public LogData[] getLogInformation(Long logId) {
    	List<LogData> result = new LinkedList<LogData>();
        try {
        	ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                    " SELECT cl.id, " +
                        " cl.network_id, " +
                        " cv.name, " +
                        " cl.log_time, " +
                        " cl.log_level, " +
                        " cl.log_text, " +
                        " cnl.is_join, " +
                        " cnl.is_leave, " + 
                        " cnl.is_authorized " +
                    " FROM cargo_log cl " +
                    "     INNER JOIN cargo_vehicle cv ON cl.vehicle_id = cv.id " +
                    "     LEFT OUTER JOIN cargo_network_log cnl ON cl.id = cnl.id " +
                    " WHERE cl.id > ? " +
                    " ORDER BY cl.id");
                
                stmt.setLong(1, logId.longValue());
                
                rs = stmt.executeQuery();
                
                while ( rs.next() ) {
                	LogData data = new LogData();
                    data.id = new Long(rs.getLong("ID"));
                    data.networkId = rs.getInt("NETWORK_ID");
                    data.vehicleName = rs.getString("NAME");
                    data.time = DateHelper.dateTimeToString(rs.getTimestamp("LOG_TIME"));
                    data.level = rs.getInt("LOG_LEVEL");
                    data.text = rs.getString("LOG_TEXT");
                    data.is_join  = rs.getBoolean("IS_JOIN");
                    data.is_leave  = rs.getBoolean("IS_LEAVE");
                    data.is_authorized  = rs.getBoolean("IS_AUTHORIZED");
                    result.add(data);
                }
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
        
        return result.toArray(new LogData[0]);
    }
    
    public VehicleData[] getVehiclesByNetwork(Integer networkId) {
        List<VehicleData> result = new LinkedList<VehicleData>();
        try {
            ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                    CargoDAOImpl.VEHICLES_DATA_SELECT +
                    "INNER JOIN cargo_network_vehicle netve ON ve.id = netve.vehicle_id " + 
                	"WHERE netve.network_id = ? " + 
                	"ORDER BY name" );
                
                stmt.setLong(1, networkId.longValue());
                
                rs = stmt.executeQuery();
                while ( rs.next() ) {
                    VehicleData data = new VehicleData();
                    data.id = new Integer(rs.getInt("ID"));
                    data.itemId = rs.getString("ITEM_ID");
                    data.itemOwner = rs.getString("ITEM_OWNER");
                    data.name = rs.getString("NAME");
                    data.latitude = rs.getDouble("LATITUDE");
                    data.longitude = rs.getDouble("LONGITUDE");                    
                    data.status = rs.getString("STATUS");
                    data.temperature = rs.getString("TEMPERATURE");
                    data.humidity = rs.getString("HUMIDITY");
                    if ( null != networkId ) {
                        data.networkId = new Integer(networkId);
                    }
                    result.add(data);
                }

            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) { 
            throw new RuntimeException(ex);
        }
    
        return result.toArray(new VehicleData[result.size()]);
    }
    
    
    public Long getMaxLogId() {
        Long maxLogId = null;
        try {
        	ResultSet rs = null;
            try {       
            	PreparedStatement stmt = getConnection().prepareStatement(
            			"SELECT MAX(id) FROM cargo_log");
            	rs = stmt.executeQuery();
            	while ( rs.next() ) {
            		maxLogId = rs.getLong(1);
            	}
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
        
        return maxLogId;
    }
    
    public VehicleData getVehicle(String itemId) 
        throws VehicleNotFoundByItemException, ClientException {
        try {
            ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                    CargoDAOImpl.VEHICLES_DATA_SELECT +
                    " WHERE ve.item_id = ? " + 
                    " ORDER BY name" );
                
                stmt.setString(1, itemId);
                
                rs = stmt.executeQuery();
                if ( rs.next() ) {
                    VehicleData data = new VehicleData();
                    data.id = new Integer(rs.getInt("ID"));
                    data.itemId = rs.getString("ITEM_ID");
                    data.itemOwner = rs.getString("ITEM_OWNER");
                    data.name = rs.getString("NAME");
                    data.latitude = rs.getDouble("LATITUDE");
                    data.longitude = rs.getDouble("LONGITUDE");
                    data.status = rs.getString("STATUS");
                    data.temperature = rs.getString("TEMPERATURE");
                    data.humidity = rs.getString("HUMIDITY");
                    String networkId = rs.getString("NETWORK_ID");
                    if ( null != networkId ) {
                        data.networkId = new Integer(networkId);
                    }
                    return data;
                } else {
                    throw new VehicleNotFoundByItemException(itemId);
                }

            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) { 
            throw new RuntimeException(ex);
        }
    

    }
    
    public VehicleLogData[] getVehicleHistory(Integer vehicleId) {
        List<VehicleLogData> result = new LinkedList<VehicleLogData>();
        try {
            ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                        " SELECT cl.id, " +
                        "   cl.log_time, " +
                        "   cl.log_level, " +
                        "   cl.log_text, " +
                        "   csl.latitude, " +
                        "   csl.longitude, " +
                        "   csl.status, " +
                        "   csl.temperature, " +
                        "   csl.humidity " +
                        " FROM cargo_log cl " +
                        "   inner join cargo_status_log csl on cl.id = csl.id " +
                        " WHERE cl.vehicle_id = ? " +
                        " ORDER BY cl.id " );
                
                stmt.setInt(1, vehicleId.intValue());
                
                rs = stmt.executeQuery();
                while ( rs.next() ) {
                    VehicleLogData data = new VehicleLogData();
                    
                    data.id = new Long(rs.getLong("ID"));
                    
                    data.logTime = new Date(rs.getTimestamp("LOG_TIME").getTime());
                    data.logLevel = new Integer(rs.getInt("LOG_LEVEL"));
                    data.logText = rs.getString("LOG_TEXT");
                    
                    data.latitude = new Double(rs.getDouble("LATITUDE"));
                    data.longitude = new Double(rs.getDouble("LONGITUDE"));
                    data.status = rs.getString("STATUS");
                    data.temperature = rs.getString("TEMPERATURE");
                    data.humidity = rs.getString("HUMIDITY");
                    
                    result.add(data);
                }

            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) { 
            throw new RuntimeException(ex);
        }
    
        return result.toArray(new VehicleLogData[result.size()]);
    }    
    
    public VehicleLogData getVehicalLogDataBeforeTime(Integer vehicleId, Date timePoint) {
        LogHelper.getLogger().debug("CargoDAOImpl.getVehicalLogDataBeforeTime() call");
        VehicleLogData data = null;
        try {
            ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                        CargoDAOImpl.VEHICLES_LOG_DATA_SELECT +
                        " WHERE cl.vehicle_id = ? " +
                        "   and cl.log_time <= ? " +
                        " ORDER BY cl.id DESC LIMIT 1 " );

                stmt.setInt(1, vehicleId.intValue());
                stmt.setTimestamp(2, new Timestamp(timePoint.getTime()));
                
                rs = stmt.executeQuery();
                while ( rs.next() ) {
                    data = new VehicleLogData();
                    
                    data.id = new Long(rs.getLong("ID"));
                    
                    data.logTime = new Date(rs.getTimestamp("LOG_TIME").getTime());
                    data.logLevel = new Integer(rs.getInt("LOG_LEVEL"));
                    data.logText = rs.getString("LOG_TEXT");
                    
                    data.latitude = new Double(rs.getDouble("LATITUDE"));
                    data.longitude = new Double(rs.getDouble("LONGITUDE"));
                    data.status = rs.getString("STATUS");
                    data.temperature = rs.getString("TEMPERATURE");
                    data.humidity = rs.getString("HUMIDITY");
                }
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) { 
            LogHelper.getLogger().error("CargoDAOImpl.getVehicalLogDataBeforeTime() call",ex);
            throw new RuntimeException(ex);
        }
        LogHelper.getLogger().debug("CargoDAOImpl.getVehicalLogDataBeforeTime() finish");
        return data;
    }
    
    public VehicleLogData getVehicalLogDataAfterTime(Integer vehicleId, Date timePoint) {
        VehicleLogData data = null;
        try {
            ResultSet rs = null;
            try {       
                PreparedStatement stmt = getConnection().prepareStatement(
                        CargoDAOImpl.VEHICLES_LOG_DATA_SELECT +
                        " WHERE cl.vehicle_id = ? " +
                        "   and cl.log_time >= ? " +
                        " ORDER BY cl.id ASC LIMIT 1 " );

                stmt.setInt(1, vehicleId.intValue());
                stmt.setTimestamp(2, new Timestamp(timePoint.getTime()));
                
                rs = stmt.executeQuery();
                while ( rs.next() ) {
                    data = new VehicleLogData();
                    
                    data.id = new Long(rs.getLong("ID"));
                    
                    data.logTime = new Date(rs.getTimestamp("LOG_TIME").getTime());
                    data.logLevel = new Integer(rs.getInt("LOG_LEVEL"));
                    data.logText = rs.getString("LOG_TEXT");
                    
                    data.latitude = new Double(rs.getDouble("LATITUDE"));
                    data.longitude = new Double(rs.getDouble("LONGITUDE"));
                    data.status = rs.getString("STATUS");
                    data.temperature = rs.getString("TEMPERATURE");
                    data.humidity = rs.getString("HUMIDITY");
                }
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) { 
            throw new RuntimeException(ex);
        }
        return data;
    }
    
    public UserData[] getUsers() {
    	List<UserData> result = new LinkedList<UserData>();
        UserData data = null;
        try {
        	ResultSet rs = null;
            try {       
            	PreparedStatement stmt = getConnection().prepareStatement(
            			"SELECT * FROM cargo_user");
            	rs = stmt.executeQuery();
            	while ( rs.next() ) {
            		data = new UserData();
            		data.id = rs.getInt("ID");
            		data.userName = rs.getString("USER_NAME");
            		data.password = rs.getString("PASSWORD");
            		
            		result.add(data);
            	}
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
        
        return result.toArray(new UserData[result.size()]);
    }
    
    public UserData getUser(Integer id) {
        UserData data = null;
        try {
        	ResultSet rs = null;
            try {       
            	PreparedStatement stmt = getConnection().prepareStatement(
            			"SELECT id, user_name, password FROM cargo_user WHERE id = ?");
            	stmt.setInt(1, id.intValue());
            	rs = stmt.executeQuery();
            	while ( rs.next() ) {
            		data = new UserData();
            		data.id = rs.getInt("ID");
            		data.userName = rs.getString("USER_NAME");
            		data.password = rs.getString("PASSWORD");
            		
            	}
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
        
        return data;
    }
    
    public UserData getUser(String userName) {
        UserData data = null;
        try {
        	ResultSet rs = null;
            try {       
            	PreparedStatement stmt = getConnection().prepareStatement(
            			"SELECT id, user_name, password FROM cargo_user WHERE user_name = ?");
            	stmt.setString(1, userName);
            	rs = stmt.executeQuery();
            	while ( rs.next() ) {
            		data = new UserData();
            		data.id = rs.getInt("ID");
            		data.userName = rs.getString("USER_NAME");
            		data.password = rs.getString("PASSWORD");
            		
            	}
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        } catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
        
        return data;
    }

    
    public UserData storeUser(Integer id, String name, String password) 
    	throws DuplicateUserException 
    {
    	try {
    		ResultSet rs = null;
    		if (null == id) {
    			PreparedStatement stmt = getConnection().prepareStatement(
    					"SELECT NEXTVAL('cargo_user_sqc') AS user_id");
    			rs = stmt.executeQuery();
    			while ( rs.next() ) {
    				id = rs.getInt("USER_ID");
    			}
    			stmt.close();

    			if (null != getUser(name)) {
    				throw new DuplicateUserException(name);
    			} else {
    				stmt = getConnection().prepareStatement(
    						"INSERT INTO cargo_user(id, user_name, password) " +
    						"VALUES (?, ?, ?)"
    				);
    				stmt.setInt(1, id);
    				stmt.setString(2, name);
    				stmt.setString(3, password);
    				stmt.executeUpdate();
    				stmt.close();
    			}
    		} else {
    			UserData user = getUser(name);
    			if (null == user || 
    					((null != user) && (id.equals(user.id))) ) {
    				PreparedStatement stmt = getConnection().prepareStatement(
    						"UPDATE cargo_user " +
    						"SET user_name = ?, " +
    						"	 password = ? " +
    				"WHERE id = ?");
    				stmt.setInt(3, id);
    				stmt.setString(1, name);
    				stmt.setString(2, password);
    				stmt.executeUpdate();
    				stmt.close();
    			} else {
    				throw new DuplicateUserException(name);
    			}
    			
    		}
    	} catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
    	
    	return getUser(id);
    	
    }
    
    public void removeUser(Integer id) {
        try {
        	PreparedStatement stmt = getConnection().prepareStatement(
        			"DELETE FROM cargo_user WHERE id = ?");
        	stmt.setInt(1, id);
        	stmt.executeUpdate();
        } catch ( SQLException ex ) {
        	throw new RuntimeException(ex);
        }
    }
    


    
    public VehicleLogData[] getVehicleHistory(Integer vehicleId,
            Date fromDate, Date toDate, long timeStep ) {

        LogHelper.getLogger().debug("CargoDAOImpl.getVehicleHistory() call");
        LogHelper.getLogger().debug("  fromDate=" + fromDate.getTime());
        LogHelper.getLogger().debug("  toDate=" + toDate.getTime());
        LogHelper.getLogger().debug("  timeStep=" + timeStep);

        List<VehicleLogData> result = new LinkedList<VehicleLogData>();

        long datePoint = fromDate.getTime();
        long lastDatePoint = datePoint;

        while ( datePoint <= toDate.getTime() ) {

            LogHelper.getLogger().debug("  datePoint=" + datePoint);
            
            VehicleLogData vehicleLogData = 
                getNearestVehicleLogDataPoint(vehicleId, new Date(datePoint), timeStep);

            result.add(vehicleLogData);
            
            lastDatePoint = datePoint;
            
            datePoint += timeStep;
        }
        
        if ( lastDatePoint != toDate.getTime() ) {
            VehicleLogData vehicleLogData = 
                getNearestVehicleLogDataPoint(vehicleId, toDate, timeStep);
            result.add(vehicleLogData);
        }
        
        return result.toArray(new VehicleLogData[result.size()]);
        
    }
    
    
    private VehicleLogData getNearestVehicleLogDataPoint(Integer vehicleId, Date datePoint, long timeStep) {
        LogHelper.getLogger().debug("CargoDAOImpl.getNearestVehicleLogDataPoint() call");
        LogHelper.getLogger().debug("  datePoint="+datePoint.getTime());

        VehicleLogData beforeData =  
            getVehicalLogDataBeforeTime(vehicleId, datePoint);

        long beforeDelta;
        if ( null != beforeData ) {
            LogHelper.getLogger().debug("  beforeData.logTime="+beforeData.logTime.getTime());
            beforeDelta = Math.abs(
                    beforeData.logTime.getTime() - datePoint.getTime());
        } else {
            beforeDelta = timeStep;
            LogHelper.getLogger().debug("  beforeData="+beforeData);
        }
        LogHelper.getLogger().debug("  beforeDelta="+beforeDelta);

        
        VehicleLogData afterData =  
            getVehicalLogDataAfterTime(vehicleId, datePoint);
        long afterDelta;
        if ( null != afterData ) {
            LogHelper.getLogger().debug("  afterData.logTime="+afterData.logTime.getTime());
            afterDelta = Math.abs(
                    afterData.logTime.getTime() - datePoint.getTime());
        } else {
            LogHelper.getLogger().debug("  afterData="+afterData);
            afterDelta = timeStep;
        }
        LogHelper.getLogger().debug("  afterDelta="+afterDelta);
        
        
        if ( beforeDelta < afterDelta && beforeDelta < timeStep ) {
            return beforeData;
        } else  if ( beforeDelta > afterDelta && afterDelta < timeStep ) {
            return afterData;
        } else {
            return null;
        }
    }

    
    public Integer storeVehicleInfoLog(VehicleLogData vehicleLogData) {
        LogHelper.getLogger().debug("CargoDAOImpl.addVehicleLog() call");
                
        Integer logId = null;
        try {
        
            Statement statement = getConnection().createStatement();
            statement.execute("BEGIN TRANSACTION");
            statement.close();
            LogHelper.getLogger().debug("  begin transaction");

            try {
                
                logId = insertVehicleInfoLog(vehicleLogData);

                statement = getConnection().createStatement();
                statement.execute("COMMIT TRANSACTION");
                statement.close();
                LogHelper.getLogger().debug("  commit transaction");
            
            } catch (Throwable ex) {
                LogHelper.getLogger().error("  ERROR:",ex);
                statement = getConnection().createStatement();
                statement.execute("ROLLBACK TRANSACTION");
                statement.close();
                LogHelper.getLogger().debug("  rollback transaction");
                throw new RuntimeException(ex);

            }
        
        } catch ( SQLException ex ) {
            throw new RuntimeException(ex);
        }

        LogHelper.getLogger().debug("CargoDAOImpl.addVehicleLog() finish");
        return logId;
        
    }
    

    public Integer storeVehicleJoinToNetworkLog(VehicleJoinLogData vehicleJoinLogData) {
        LogHelper.getLogger().debug("CargoDAOImpl.storeVehicleJoinToNetworkLog() call");
        Integer logId = null;
        try {
        
            Statement statement = getConnection().createStatement();
            statement.execute("BEGIN TRANSACTION");
            statement.close();
            LogHelper.getLogger().debug("  begin transaction");

            try {
                
                if ( ! isVehicleInNetwork(vehicleJoinLogData.vehicleId, vehicleJoinLogData.networkId) ) {
                    insertCargoNetworkVehicle(
                                vehicleJoinLogData.vehicleId, 
                                vehicleJoinLogData.networkId);
                }
                
                logId = insertVehicleJoinLog(vehicleJoinLogData);
                
                statement = getConnection().createStatement();
                statement.execute("COMMIT TRANSACTION");
                statement.close();
                LogHelper.getLogger().debug("  commit transaction");
            
            } catch (Throwable ex) {
                LogHelper.getLogger().error("  ERROR:",ex);
                statement = getConnection().createStatement();
                statement.execute("ROLLBACK TRANSACTION");
                statement.close();
                LogHelper.getLogger().debug("  rollback transaction");
                throw new RuntimeException(ex);

            }        
        } catch ( SQLException ex ) {
            throw new RuntimeException(ex);
        }
        return logId;
    }
    
    public Integer storeVehicleNotRespondLog(VehicleJoinLogData logData) {
        LogHelper.getLogger().debug("CargoDAOImpl.storeVehicleNotRespondLog() call");
        Integer logId = null;
        try {
        
            Statement statement = getConnection().createStatement();
            statement.execute("BEGIN TRANSACTION");
            statement.close();
            LogHelper.getLogger().debug("  begin transaction");

            try {
                
                deleteCargoNetworkVehicle(logData.vehicleId, logData.networkId);
                
                logId = insertVehicleJoinLog(logData);
                
                statement = getConnection().createStatement();
                statement.execute("COMMIT TRANSACTION");
                statement.close();
                LogHelper.getLogger().debug("  commit transaction");
            
            } catch (Throwable ex) {
                LogHelper.getLogger().error("  ERROR:",ex);
                statement = getConnection().createStatement();
                statement.execute("ROLLBACK TRANSACTION");
                statement.close();
                LogHelper.getLogger().debug("  rollback transaction");
                throw new RuntimeException(ex);

            }
        
        } catch ( SQLException ex ) {
            throw new RuntimeException(ex);
        }
        return logId;
    }        
    
    public VehicleData[] getVehiclesWhichNotRespond(Timestamp fromTime) {
        List<VehicleData> result = new LinkedList<VehicleData>();
        try {
                    
            ResultSet rs = null;
            try {       
            
                PreparedStatement stmt = getConnection().prepareStatement(
                        " SELECT " +
                        "   v.id, " +
                        "   v.item_id, " +
                        "   v.item_owner, " +
                        "   v.name, " +
                        "   nv.network_id " +
                        " FROM " +
                        "   cargo_vehicle v " +
                        "   inner join cargo_network_vehicle nv on nv.vehicle_id = v.id " +
                        " WHERE " +
                        "   v.id not in ( SELECT l.vehicle_id " +
                        "                 FROM cargo_log l " +
                        "                 WHERE l.log_time >= ? ) ");
        
                stmt.setTimestamp(1, fromTime);
                
                rs = stmt.executeQuery();
                
                while ( rs.next() ) {
                    VehicleData data = new VehicleData();
                    data.id = new Integer(rs.getInt("ID"));
                    data.itemId = rs.getString("ITEM_ID");
                    data.itemOwner = rs.getString("ITEM_OWNER");
                    data.name = rs.getString("NAME");
                    data.networkId = rs.getInt("NETWORK_ID");
                    result.add(data);
                }
            } finally {
                if ( null != rs ) {
                    rs.close();
                }
            }
        
        } catch ( SQLException ex ) {
            throw new RuntimeException(ex);
        }
         
        return result.toArray(new VehicleData[result.size()]);
        
    }
    
    private boolean isVehicleInNetwork(Integer vehicleId, Integer networkId) 
        throws SQLException {
    
        PreparedStatement stmt = getConnection().prepareStatement(
                " SELECT nv.id " +
                " FROM cargo_network_vehicle nv" +
                " WHERE nv.vehicle_id = ?" +
                "   and nv.network_id = ? ");
        stmt.setInt(1, vehicleId);
        stmt.setInt(2, networkId);
        ResultSet rs = stmt.executeQuery();
        Integer networkVehicleId = null;
        while ( rs.next() ) {
            networkVehicleId = rs.getInt("id");
        }
        if ( null == networkVehicleId ) {
            return false;
        } else {
            return true;
        }
    }
    
    
    public NetworkData getVehicleNetwork(Integer vehicleId) 
        throws SQLException {
        NetworkData result = null;
        ResultSet rs = null;
        try {           
            PreparedStatement stmt = getConnection().prepareStatement(
                    " SELECT net.id, net.name " +
                    " FROM cargo_network_vehicle nv" +
                    "   inner join cargo_network net on net.id = nv.network_id" +
                    " WHERE nv.vehicle_id = ?" );
            stmt.setInt(1, vehicleId);
            rs = stmt.executeQuery();
            while ( rs.next() ) {
                result = new NetworkData();
                result.id = new Integer(rs.getInt("ID"));
                result.name = rs.getString("NAME");
            }
        } finally {
            if ( null != rs ) {
                rs.close();
            }
        }
        return result;
    }
    
    
    private Integer insertCargoNetworkVehicle(Integer networkId, Integer vehicleId) 
        throws SQLException {
     
        Integer networkVehicleId = null;
        ResultSet rs = null;
        try {
            Statement statement = getConnection().createStatement();
            rs = statement.executeQuery("SELECT NEXTVAL('cargo_network_vehicle_sqc') AS id");
            while ( rs.next() ) {
                networkVehicleId = rs.getInt("id");
            }
    
            PreparedStatement stmt = getConnection().prepareStatement(
                    " INSERT INTO cargo_network_vehicle(" +
                    "   id, vehicle_id, network_id ) " +
                    " VALUES (?, ?, ?)");
            stmt.setInt(1, networkVehicleId);
            stmt.setInt(2, networkId);
            stmt.setInt(3, vehicleId);
            stmt.executeUpdate();
            LogHelper.getLogger().debug(" insert cargo_network_vehicle");
        } finally {
            if ( null != rs ) {
                rs.close();
            }
        }
        return networkVehicleId;
    }
    
    
    private Integer insertVehicleJoinLog(VehicleJoinLogData vehicleJoinLogData) 
        throws SQLException {
        ResultSet rs = null;
        Integer logId = null;
        try {
            Statement statement = getConnection().createStatement();
            rs = statement.executeQuery("SELECT NEXTVAL('cargo_log_sqc') AS log_id");
            while ( rs.next() ) {
                logId = rs.getInt("log_id");
            }
            LogHelper.getLogger().debug(" log_id="+logId);
            
            PreparedStatement stmt = getConnection().prepareStatement(
                    " INSERT INTO cargo_log(" +
                    "   id, network_id, vehicle_id, log_time," +
                    "   log_level, log_text) " +
                    " VALUES (?, ?, ?, ?, ?, ?)");
            stmt.setInt(1, logId);
            stmt.setInt(2, vehicleJoinLogData.networkId);
            stmt.setInt(3, vehicleJoinLogData.vehicleId);
            stmt.setTimestamp(4, new Timestamp(vehicleJoinLogData.logTime.getTime()));
            stmt.setInt(5, vehicleJoinLogData.logLevel);
            stmt.setString(6, vehicleJoinLogData.logText);
            stmt.executeUpdate();
            LogHelper.getLogger().debug(" insert cargo_log");
            
            stmt = getConnection().prepareStatement(
                    " INSERT INTO cargo_network_log(" +
                    "   id, is_join, is_leave, is_authorized) " +
                    " VALUES (?, ?, ?, ?)");
            stmt.setInt(1, logId);
            stmt.setBoolean(2, vehicleJoinLogData.isJoin.booleanValue());
            stmt.setBoolean(3, vehicleJoinLogData.isLeave.booleanValue());
            stmt.setBoolean(4, vehicleJoinLogData.isAuthorized.booleanValue());
            stmt.executeUpdate();
            LogHelper.getLogger().debug(" insert cargo_network_log");
        } finally {
            if ( null != rs ) {
                rs.close();
            }
        }
        return logId;
    }
    
    private Integer insertVehicleInfoLog(VehicleLogData vehicleLogData) 
            throws SQLException {
        Integer logId = null;
        ResultSet rs = null;
        try {
            Statement statement = getConnection().createStatement();
            rs = statement.executeQuery("SELECT NEXTVAL('cargo_log_sqc') AS log_id");
            while ( rs.next() ) {
                logId = rs.getInt("log_id");
            }
            LogHelper.getLogger().debug(" log_id="+logId);
            
            PreparedStatement stmt = getConnection().prepareStatement(
                    " INSERT INTO cargo_log(" +
                    "   id, network_id, vehicle_id, log_time," +
                    "   log_level, log_text) " +
                    " VALUES (?, ?, ?, ?, ?, ?)");
            stmt.setInt(1, logId);
            stmt.setInt(2, vehicleLogData.networkId);
            stmt.setInt(3, vehicleLogData.vehicleId);
            stmt.setTimestamp(4, new Timestamp(vehicleLogData.logTime.getTime()));
            stmt.setInt(5, vehicleLogData.logLevel);
            stmt.setString(6, vehicleLogData.logText);
            stmt.executeUpdate();
            LogHelper.getLogger().debug(" insert cargo_log");
            
            stmt = getConnection().prepareStatement(
                    " INSERT INTO cargo_status_log(" +
                    "   id, latitude, longitude, " +
                    "   status, temperature, humidity) " +
                    " VALUES (?, ?, ?, ?, ?, ?)");
            stmt.setInt(1, logId);
            stmt.setString(2, vehicleLogData.latitude.toString());
            stmt.setString(3, vehicleLogData.longitude.toString());
            stmt.setString(4, vehicleLogData.status);
            stmt.setString(5, vehicleLogData.temperature);
            stmt.setString(6, vehicleLogData.humidity);
            stmt.executeUpdate();
            LogHelper.getLogger().debug(" insert cargo_status_log");
        } finally {
            if ( null != rs ) {
                rs.close();
            }
        }
        return logId;
    }
        
    private void deleteCargoNetworkVehicle(Integer vehicleId, Integer networkId) 
        throws SQLException {
        PreparedStatement stmt = getConnection().prepareStatement(
                " DELETE FROM cargo_network_vehicle " +
                " WHERE vehicle_id = ? and network_id = ? ");
        stmt.setInt(1, vehicleId);
        stmt.setInt(2, networkId);
        stmt.executeUpdate();
        LogHelper.getLogger().debug(" delete cargo_network_vehicle");
    }
        
    private Connection getConnection() {
        return m_connection;
    }

}
