
-- $Id: leave_vehicle.sql 213 2007-05-25 11:05:35Z hatu $

-- Vehicle leave network at time



INSERT INTO cargo_network_vehicle(id, vehicle_id, network_id, join_time, leave_time) 
    VALUES (NEXTVAL('cargo_network_vehicle_sqc'), 21, 2, '2007-01-02 23:01:13', '2007-01-04 15:01:13'); 

INSERT INTO cargo_log(id, network_id, vehicle_id, log_time, log_level, log_text) 
    VALUES (NEXTVAL('cargo_log_sqc'), 2, 21, '2007-01-04 15:01:13', 3, 'Vehicle 1 leave Maryland Network');

INSERT INTO cargo_network_log(id, network_vehicle_id, is_join, is_leave, is_authorized) 
    VALUES (CURRVAL('cargo_log_sqc'), CURRVAL('cargo_network_vehicle_sqc'), false, true, false);

delete from cargo_network_vehicle where id=5; 



