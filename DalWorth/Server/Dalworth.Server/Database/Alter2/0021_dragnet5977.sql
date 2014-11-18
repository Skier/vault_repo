--
-- Table structure for table `projectfeedbackrate`
--

DROP TABLE IF EXISTS `projectfeedbackrate`;
CREATE TABLE `projectfeedbackrate` (
  `ID` int(10) NOT NULL,
  `Rate` varchar(50) NOT NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


DROP TABLE IF EXISTS `projectfeedback`;
CREATE TABLE `projectfeedback` (
  `ID` int(10) NOT NULL auto_increment,
  `ProjectId` int(10) NOT NULL,
  `CallbackInquireTransactionId` int(10) NOT NULL,
  `CustomerNote` varchar(2000) NOT NULL,
  `EditedCustomerNote` varchar(2000) NOT NULL,
  `DispatcherNote` varchar(2000) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateReviewed` datetime default NULL,
  `ReviewedByEmployeeId` int(10) default NULL,
  `CanBePostedOnWebSite` tinyint(1) NOT NULL,
  `CanBeUsedAsReferral` tinyint(1) NOT NULL,
  `IsSubscribeToMailingList` tinyint(1) NOT NULL,
  `RateId` int(10) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_ProjectFeedback_CallbackInquireTransaction` (`CallbackInquireTransactionId`),
  KEY `FK_ProjectFeedback_Employee` (`ReviewedByEmployeeId`),
  KEY `FK_ProjectFeedback_ProjectFeedbackRate` (`RateId`),
  KEY `FK_ProjectFeedback_Project` (`ProjectId`),
  CONSTRAINT `FK_ProjectFeedback_CallbackInquireTransaction` FOREIGN KEY (`CallbackInquireTransactionId`) REFERENCES `callbackinquiretransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectFeedback_Employee` FOREIGN KEY (`ReviewedByEmployeeId`) REFERENCES `employee` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectFeedback_ProjectFeedbackRate` FOREIGN KEY (`RateId`) REFERENCES `projectfeedbackrate` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProjectFeedback_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



INSERT INTO `projectfeedbackrate` (`ID`, `Rate`) VALUES 
(0,'Not Rated'),
(1,'Excellent'),
(2,'Good'),
(3,'Acceptable'),
(4,'Needs improvement'),
(5,'Not satisfied');


INSERT INTO `sysversion` (`Version`) VALUES
(21);