INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             1, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
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

-- ----------------------- 2 --------------------------------

INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             2, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             2, 
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
             3, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             3, 
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
             4, 
             1 
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             4, 
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

