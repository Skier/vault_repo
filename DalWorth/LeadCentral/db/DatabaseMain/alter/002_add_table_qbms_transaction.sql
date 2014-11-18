DROP TABLE IF EXISTS `qbmstransaction`;
CREATE TABLE `qbmstransaction` (
  `Id` int(10) NOT NULL auto_increment,
  `ServmanCustomerId` int(10) NOT NULL,
  `Ticket` varchar(150) NOT NULL,
  `OpId` varchar(50) NOT NULL,
  `Amount` decimal(19,4) NOT NULL,
  `OpType` varchar(50) default NULL,
  `Status` varchar(50) default NULL,
  `StatusCode` varchar(50) default NULL,
  `StatusMessage` varchar(250) default NULL,
  `TxnType` varchar(50) default NULL,
  `TxnTimestamp` varchar(50) default NULL,
  `MaskedCCN` varchar(50) default NULL,
  `AuthCode` varchar(50) default NULL,
  `TxnId` varchar(50) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_QbmsTransaction_ServmanCustomer` (`ServmanCustomerId`),
  CONSTRAINT `FK_QbmsTransaction_ServmanCustomer` FOREIGN KEY (`ServmanCustomerId`) REFERENCES `servmancustomer` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

