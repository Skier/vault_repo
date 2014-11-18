DROP TABLE IF EXISTS `projectinsurance`;
CREATE TABLE  `projectinsurance` (
  `ProjectId` int(10) NOT NULL,
  `Company` varchar(50) default NULL,
  `Address1` varchar(256) default NULL,
  `Address2` varchar(256) default NULL,
  `Contact` varchar(50) default NULL,
  `Phone` varchar(20) default NULL,
  `Fax` varchar(20) default NULL,
  `ClaimNumber` varchar(20) default NULL,
  `DeductibleAmount` decimal(19,4) default NULL,
  PRIMARY KEY  (`ProjectId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `sysversion` (`Version`) VALUES (45);