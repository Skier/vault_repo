CREATE TABLE `EquipmentTransactionDetailTemp` (
  `ID` int(10) NOT NULL auto_increment,
  `EquipmentTransactionId` int(10) NOT NULL,
  `EquipmentTypeId` int(10) NOT NULL,
  `VanId` int(10) default NULL,
  `AddressId` int(10) default NULL,
  `Quantity` int(10) NOT NULL,
  `QuantityChange` int(10) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_EquipmentTransactionDetailTemp_Address` (`AddressId`),
  KEY `FK_EquipmentTransactionDetailTemp_EquipmentTransaction` (`EquipmentTransactionId`),
  KEY `FK_EquipmentTransactionDetailTemp_Van` (`VanId`),
  KEY `FK_EquipmentTransactionDetailTemp_EquipmentType` (`EquipmentTypeId`),
  CONSTRAINT `FK_EquipmentTransactionDetailTemp_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetailTemp_EquipmentTransaction` FOREIGN KEY (`EquipmentTransactionId`) REFERENCES `equipmenttransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetailTemp_Van` FOREIGN KEY (`VanId`) REFERENCES `van` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetailTemp_EquipmentType` FOREIGN KEY (`EquipmentTypeId`) REFERENCES `equipmenttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2938 DEFAULT CHARSET=latin1;


INSERT INTO `sysversion` (`Version`) VALUES
(32);