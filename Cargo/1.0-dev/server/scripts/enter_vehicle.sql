
-- $Id: enter_vehicle.sql 213 2007-05-25 11:05:35Z hatu $

-- Vehicle 3 join to Oklahoma Network at time --


INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             1, 
             2 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             2, 
             1, 
             '2007-01-02 23:01:13', 
             3, 
             'Vehicle join to Oklahoma Network'
           );

INSERT INTO cargo_status_log(id, latitude, longitude, status, temperature, humidity) 
    VALUES ( CURRVAL('cargo_log_sqc'), 
             '36.64444', 
             '-96.72083',
             'JOIN', 
             '77', 
             '75'
           );


INSERT INTO cargo_network_log(id, is_join, is_leave, is_authorized) 
    VALUES (CURRVAL('cargo_log_sqc'), true, false, false);


