DROP TABLE IF EXISTS `dalworth_server_dbo`.`workdetaillog`;
CREATE TABLE  `dalworth_server_dbo`.`workdetaillog` (
  `ID` int(10) NOT NULL auto_increment,
  `EmployeeId` int(10) NOT NULL,
  `DateCreated` datetime default NULL,
  `Trace` longtext,
  `WorkDetailId` int(10) default NULL,
  `WorkId` int(10) default NULL,
  `VisitId` int(10) default NULL,
  `TimeBegin` datetime default NULL,
  `TimeEnd` datetime default NULL,
  `Sequence` int(10) default NULL,
  `WorkDetailStatusId` int(10) default NULL,
  `TimeDispatch` datetime default NULL,
  `TimeArrive` datetime default NULL,
  `TimeComplete` datetime default NULL,
  `TimeBeginAssigned` datetime default NULL,
  `TimeEndAssigned` datetime default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `sysversion` (`Version`) VALUES (37);