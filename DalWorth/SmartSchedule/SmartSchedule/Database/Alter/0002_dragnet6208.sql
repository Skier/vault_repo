CREATE TABLE `technicianworktimedefaultpreset` (
  `TechnicianId` int(10) NOT NULL,
  `PresetNumber` int(10) NOT NULL,
  `TimeStart` datetime default NULL,
  `TimeEnd` datetime default NULL,
  PRIMARY KEY  (`TechnicianId`,`PresetNumber`),
  CONSTRAINT `FK_TechnicianWorkTimeDefaultPreset_TechnicianDefault` FOREIGN KEY (`TechnicianId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


INSERT INTO `sysversion` (`Version`) VALUES 
(2);