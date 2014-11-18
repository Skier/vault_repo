-- Table structure for table `securitypermission`
--

DROP TABLE IF EXISTS `securitypermission`;
CREATE TABLE `securitypermission` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `securityrole`
--

DROP TABLE IF EXISTS `securityrole`;
CREATE TABLE `securityrole` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Table structure for table `securityrolepermission`
--

DROP TABLE IF EXISTS `securityrolepermission`;
CREATE TABLE `securityrolepermission` (
  `SecurityRoleId` int(10) NOT NULL,
  `SecurityPermissionId` int(10) NOT NULL,
  PRIMARY KEY  (`SecurityRoleId`,`SecurityPermissionId`),
  KEY `FK_SecurityRolePermission_SecurityPermission` (`SecurityPermissionId`),
  CONSTRAINT `FK_SecurityRolePermission_SecurityRole` FOREIGN KEY (`SecurityRoleId`) REFERENCES `securityrole` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_SecurityRolePermission_SecurityPermission` FOREIGN KEY (`SecurityPermissionId`) REFERENCES `securitypermission` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


--
-- Table structure for table `systemoperation`
--

DROP TABLE IF EXISTS `systemoperation`;
CREATE TABLE `systemoperation` (
  `ID` int(10) NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(200) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


-- Table structure for table `systemlog`
--

DROP TABLE IF EXISTS `systemlog`;
CREATE TABLE `systemlog` (
  `ID` bigint(19) NOT NULL auto_increment,
  `EmployeeId` int(10) default NULL,
  `DateCreated` datetime NOT NULL,
  `SystemOperationId` int(10) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `TimeTakenMiliseconds` int(10) default NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_SystemLog_SystemOperation` (`SystemOperationId`),
  CONSTRAINT `FK_SystemLog_SystemOperation` FOREIGN KEY (`SystemOperationId`) REFERENCES `systemoperation` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `employee` ADD COLUMN `SecurityRoleId` int(10) default NULL AFTER `DefaultVanId`,
  ADD KEY `FK_Employee_SecurityRole` (`SecurityRoleId`),
  ADD CONSTRAINT `FK_Employee_SecurityRole` FOREIGN KEY (`SecurityRoleId`)
	REFERENCES `securityrole` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

DROP TABLE IF EXISTS `dashboardstate`;
CREATE TABLE  `dashboardstate` (
  `ID` int(10) unsigned NOT NULL auto_increment,
  `EmployeeId` int(10) unsigned NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `PendingTaskGridState`;
CREATE TABLE  `PendingTaskGridState` (
  `ID` int(10) unsigned NOT NULL auto_increment,
  `EmployeeId` int(10) unsigned NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


INSERT INTO `securitypermission` (`ID`,`Name`,`Description`) VALUES (1,'ViewReports','View reports on Dashboard'), (2,'ViewDashboardTotalAmount','View total Amount shown on dashboard');

INSERT INTO `securityrole` (`ID`,`Name`,`Description`) VALUES (1,'Accounting','Accounting Dept');

INSERT INTO `securityrolepermission` (`SecurityRoleId`,`SecurityPermissionId`) VALUES (1,1), (1,2);

update employee set  SecurityRoleId = 1, password = 'dalworth2009'  where id = 78;


INSERT INTO `sysversion` (`Version`) VALUES
(28);