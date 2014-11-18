
-- $Id: clear_db.sql 213 2007-05-25 11:05:35Z hatu $

-- Clear database --

DELETE FROM cargo_status_log;

DELETE FROM cargo_network_log;

DELETE FROM cargo_log;

DELETE FROM cargo_network_vehicle;

DELETE FROM cargo_vehicle;

DELETE FROM cargo_network;

