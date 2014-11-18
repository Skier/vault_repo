
-- $Id: motion_vehicle.sql 213 2007-05-25 11:05:35Z hatu $

-- Move vehicle --

\set NETWORK_ID 2
\set VEHICLE_ID 21
\set VEHICLE_LATITUDE '36.64667'
\set VEHICLE_LONGITUDE '-96.72284'


INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES (NEXTVAL('cargo_log_sqc'), :NETWORK_ID, :VEHICLE_ID, now(), 3, 'Vehicle moved'); 
INSERT INTO cargo_status_log(id, latitude, longitude, status, temperature, humidity) 
    VALUES (CURRVAL('cargo_log_sqc'), :VEHICLE_LATITUDE, :VEHICLE_LONGITUDE, 'ACTIVE', '79', '83'); 



