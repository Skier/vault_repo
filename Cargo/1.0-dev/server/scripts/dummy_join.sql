INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             5, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             5, 
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

-- ----------------------- 2 --------------------------------

INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             6, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             6, 
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

-- ----------------------- 3 --------------------------------

INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             7, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             7, 
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

-- ----------------------- 4 --------------------------------

INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             8, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             8, 
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

