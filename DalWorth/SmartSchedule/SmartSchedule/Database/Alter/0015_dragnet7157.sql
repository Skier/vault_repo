--
-- Table structure for table `userrole`
--

DROP TABLE IF EXISTS `useraction`;
DROP TABLE IF EXISTS `user`;
DROP TABLE IF EXISTS `userrole`;
DROP TABLE IF EXISTS `useractiontype`;

CREATE TABLE `userrole` (
  `ID` int(10) NOT NULL,
  `Role` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userrole`
--

/*!40000 ALTER TABLE `userrole` DISABLE KEYS */;
INSERT INTO `userrole` (`ID`, `Role`) VALUES (1,'Anonymous'),
(2,'Dispatrcher'),
(3,'Supervisor');
/*!40000 ALTER TABLE `userrole` ENABLE KEYS */;

--
-- Table structure for table `useractiontype`
--


CREATE TABLE `useractiontype` (
  `ID` int(10) NOT NULL,
  `ActionType` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `useractiontype`
--

/*!40000 ALTER TABLE `useractiontype` DISABLE KEYS */;
INSERT INTO `useractiontype` (`ID`, `ActionType`) VALUES (1,'BucketResolve'),
(2,'TechnicianSettingsEditDaily'),
(3,'TechnicianSettingsEditDefault'),
(4,'VisitModify'),
(5,'VisitUnschedule'),
(6,'BlockoutCreate'),
(7,'BlockoutModify'),
(8,'SuspendRecommendationsModify'),
(9,'Sync');
/*!40000 ALTER TABLE `useractiontype` ENABLE KEYS */;


--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `ID` int(10) NOT NULL AUTO_INCREMENT,
  `UserRoleId` int(10) NOT NULL,
  `Login` varchar(100) NOT NULL,
  `Password` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_User_UserRole` (`UserRoleId`),
  CONSTRAINT `FK_User_UserRole` FOREIGN KEY (`UserRoleId`) REFERENCES `userrole` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `user` (`ID`, `UserRoleId`, `Login`, `Password`, `IsActive`) VALUES 
(1, 1, 'Anonymous', '', 1),
(2, 3, 'Sync', '', 1),
(3, 3, 'Sergei', '8IfEtPLnj8kWnaEIVGb7+EhAUb1x/N1qXEJlJ9RAP/D66Q5LOtHYYCk02XOzgcJJFjBBwp+lp9UgjOfELBnW0w==', 1);

--
-- Dumping data for table `user`
--

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
-- TODO: put anonymous user here with ID 0
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

--
-- Table structure for table `useraction`
--

CREATE TABLE `useraction` (
  `ID` bigint(19) NOT NULL AUTO_INCREMENT,
  `UserId` int(10) NOT NULL,
  `UserActionTypeId` int(10) NOT NULL,
  `TechnicianDefaultId` int(10) DEFAULT NULL,
  `TicketNumber` varchar(50) NOT NULL,
  `DashboardDate` datetime DEFAULT NULL,
  `ActionDate` datetime NOT NULL,
  `Text` varchar(2000) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_UserAction_User` (`UserId`),
  KEY `FK_UserAction_TechnicianDefault` (`TechnicianDefaultId`),
  KEY `FK_UserAction_UserActionType` (`UserActionTypeId`),
  CONSTRAINT `FK_UserAction_User` FOREIGN KEY (`UserId`) REFERENCES `user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserAction_TechnicianDefault` FOREIGN KEY (`TechnicianDefaultId`) REFERENCES `techniciandefault` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_UserAction_UserActionType` FOREIGN KEY (`UserActionTypeId`) REFERENCES `useractiontype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `useraction`
--

ALTER TABLE `useraction` ADD INDEX `Ticket`(`TicketNumber`),
 ADD INDEX `DashboardDate`(`DashboardDate`),
 ADD INDEX `ActionDate`(`ActionDate`);

/*!40000 ALTER TABLE `useraction` DISABLE KEYS */;
/*!40000 ALTER TABLE `useraction` ENABLE KEYS */;

INSERT INTO `sysversion` (`Version`) VALUES 
(15);