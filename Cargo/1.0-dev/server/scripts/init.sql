
-- $Id: create_all_networks.sql 213 2007-05-25 11:05:35Z hatu $

-- Initialize data --


INSERT INTO cargo_network(id, name) VALUES (1, 'Maryland Network');   -- 39.51639    -76.61639 (Phoenix)


INSERT INTO cargo_vehicle(id, item_id, item_owner, name) 
            VALUES (11, 'M01', 'USA Transport Stream', 'MAR-101'); 


INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id, join_time, leave_time) 
    VALUES ( NEXTVAL('cargo_network_vehicle_sqc'), 
             11, 
             1, 
             '2007-01-02 23:01:13', 
             'infinity'
           ); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES ( NEXTVAL('cargo_log_sqc'), 
             1, 
             11, 
             '2007-01-02 23:01:13', 
             3, 
             'Vehicle join to Meryland Network'
           );

INSERT INTO cargo_status_log(id, latitude, longitude, status, temperature, humidity) 
    VALUES ( CURRVAL('cargo_log_sqc'), 
             '39.51639', 
             '-76.61639', 
             'JOIN', 
             '77', 
             '75'
           );


INSERT INTO cargo_network_log(id, network_vehicle_id, is_join, is_leave, is_authorized) 
    VALUES (CURRVAL('cargo_log_sqc'), CURRVAL('cargo_network_vehicle_sqc'), true, false, false);









                                                                                                 
