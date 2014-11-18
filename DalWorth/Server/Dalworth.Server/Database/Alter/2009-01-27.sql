DROP TABLE IF EXISTS `CallbackProcessTransactionStatus`;
CREATE TABLE `CallbackProcessTransactionStatus` (
  `ID` int(10) NOT NULL,
  `Status` varchar(50) character set utf8 NOT NULL,
  `Description` varchar(200) character set utf8 default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `CallbackProcessTransactionStatus` (`ID`, `Status`, `Description`) VALUES 
(1,'VisitCreated',NULL),
(2,'NotIntrested',NULL),
(3,'DoNotCall',NULL),
(4,'LeftMessage',NULL),
(5,'Busy',NULL),
(6,'CallReschedule',NULL);

DROP TABLE IF EXISTS `CallbackProcessTransaction`;
CREATE TABLE `CallbackProcessTransaction` (
  `CustomerId` int(10) NOT NULL,
  `TransactionDate` datetime NOT NULL,
  `CallbackProcessTransactionStatusId` int(10) NOT NULL,
  PRIMARY KEY  (`CustomerId`,`TransactionDate`),
  KEY `FK_CallbackProcessTransaction_CallbackProcessTransactionStatus` (`CallbackProcessTransactionStatusId`),
  CONSTRAINT `FK_CallbackProcessTransaction_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_CallbackProcessTransaction_CallbackProcessTransactionStatus` FOREIGN KEY (`CallbackProcessTransactionStatusId`) REFERENCES `callbackprocesstransactionstatus` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

ALTER TABLE `Customer` ADD COLUMN `CallbackLeftMessageCount` INT(10) NOT NULL AFTER `CallbackDoNotCall`;
ALTER TABLE `Customer` ADD COLUMN `CallbackBusyCount` INT(10) NOT NULL AFTER `CallbackLeftMessageCount`;