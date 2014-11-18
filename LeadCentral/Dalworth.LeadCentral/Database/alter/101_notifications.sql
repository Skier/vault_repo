DROP TABLE IF EXISTS `notificationtype`;
CREATE TABLE  `notificationtype` (
  `Id` int(10) NOT NULL auto_increment,
  `TypeName` varchar(50) NOT NULL,
  `SendToAdmin` tinyint(4) NOT NULL,
  `SendToPartner` tinyint(4) NOT NULL,
  `SendToPartnerUsers` tinyint(4) NOT NULL,
  `SendToStaff` tinyint(4) NOT NULL,
  `SendToSalesRep` tinyint(4) NOT NULL,
  `SendToAccountant` tinyint(4) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


DROP TABLE IF EXISTS `notification`;
CREATE TABLE  `notification` (
  `Id` int(10) NOT NULL auto_increment,
  `NotificationTypeId` int(10) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateProcessed` datetime default NULL,
  `IsProcessed` tinyint(4) NOT NULL,
  `FromEmail` varchar(150) NOT NULL,
  `ToEmail` varchar(150) NOT NULL,
  `Message` varchar(5000) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_Notification_NotificationType` (`NotificationTypeId`),
  CONSTRAINT `FK_Notification_NotificationType` FOREIGN KEY (`NotificationTypeId`) REFERENCES `notificationtype` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

INSERT INTO `notificationtype` (`Id`,`TypeName`,`SendToAdmin`,`SendToPartner`,`SendToPartnerUsers`,`SendToStaff`,`SendToSalesRep`,`SendToAccountant`) VALUES
(1,'Create Lead',1,1,1,1,1,0),
(2,'Cancel Lead',1,1,0,0,1,0),
(3,'Set Lead Pending',1,1,0,1,1,0),
(4,'Complete Lead',1,1,1,0,1,0),
(5,'Match Lead to QB Invoice',1,1,0,0,1,1),
(6,'Low Balance',1,0,0,0,0,0),
(7,'Empty Balance',1,0,0,0,0,0),
(8,'Reject Call',1,0,0,1,1,0),
(9,'Purchase Phone Number',1,0,0,0,0,1),
(10,'Weekly Summary',1,0,0,0,0,1);
