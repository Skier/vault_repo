ALTER TABLE `dalworth_server_dbo_production`.`lead`  CHANGE `Address1` `Address1` varchar(60);
ALTER TABLE `dalworth_server_dbo_production`.`lead`  CHANGE `Address2` `Address2` varchar(40);
ALTER TABLE `dalworth_server_dbo_production`.`lead`  CHANGE `City` `City` varchar(24);
ALTER TABLE `dalworth_server_dbo_production`.`lead`  CHANGE `State` `State` varchar(2);
ALTER TABLE `dalworth_server_dbo_production`.`lead`  CHANGE `Phone2` `Phone2` varchar(14);
ALTER TABLE `dalworth_server_dbo_production`.`lead`  CHANGE `CustomerNotes` `CustomerNotes` varchar(500);
ALTER TABLE `dalworth_server_dbo_production`.`lead`  CHANGE `PreferredTime` `PreferredTime` varchar(30);

insert into backgroundjobtype (`id`,`Type`, `Description`) values (3, 'NotifiyOnCallNewLead', 'Notify on call restoration technician about new lead');

insert into backgroundjobtype (`id`,`Type`, `Description`) values (4, 'ProjectCompletedEmail', 'Email Customer that project is completed');

insert into businesspartner (name) values ('dalworthrestoration.com');

INSERT INTO `sysversion` (`Version`) VALUES
(19);