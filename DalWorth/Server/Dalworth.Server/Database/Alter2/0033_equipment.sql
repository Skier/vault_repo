ALTER TABLE `equipmenttransaction` CHANGE COLUMN `SequnceDate` `SequenceDate` DATETIME NOT NULL,
 ADD INDEX `IX_SequenceDate` USING BTREE(`SequenceDate`);

DROP TABLE `taskequipmentcapture`;
DROP TABLE `taskequipmentrequirement`;
DROP TABLE `equipmenttransactiondetaillink`;
DROP TABLE `equipmenttransactiondetail`;
DROP TABLE `equipment`;
DROP TABLE `equipmentstatus`;
DROP TABLE `inventoryroom`;
drop FUNCTION IF EXISTS `FindPrevEquipmentTransactionDetailId`;
drop FUNCTION IF EXISTS `FindNextEquipmentTransactionDetailId`;
drop TRIGGER IF EXISTS SetEquipmentTransactionPrevNextStateInsert;
drop TRIGGER IF EXISTS SetEquipmentTransactionPrevNextStateDelete;

CREATE TABLE `equipmenttransactiondetail` (
  `ID` int(10) NOT NULL auto_increment,
  `EquipmentTransactionId` int(10) NOT NULL,
  `EquipmentTypeId` int(10) NOT NULL,
  `VanId` int(10) default NULL,
  `AddressId` int(10) default NULL,
  `Quantity` int(10) NOT NULL,
  `QuantityChange` int(10) NOT NULL,
  PRIMARY KEY  (`ID`),
  KEY `FK_EquipmentTransactionDetail_Address` (`AddressId`),
  KEY `FK_EquipmentTransactionDetail_EquipmentTransaction` (`EquipmentTransactionId`),
  KEY `FK_EquipmentTransactionDetail_Van` (`VanId`),
  KEY `FK_EquipmentTransactionDetail_EquipmentType` (`EquipmentTypeId`),
  CONSTRAINT `FK_EquipmentTransactionDetail_Address` FOREIGN KEY (`AddressId`) REFERENCES `address` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetail_EquipmentTransaction` FOREIGN KEY (`EquipmentTransactionId`) REFERENCES `equipmenttransaction` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetail_Van` FOREIGN KEY (`VanId`) REFERENCES `van` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetail_EquipmentType` FOREIGN KEY (`EquipmentTypeId`) REFERENCES `equipmenttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

insert equipmenttransactiondetail
select * from equipmenttransactiondetailtemp
order by EquipmentTransactionId, VanId desc, EquipmentTypeId;

-- drop table equipmenttransactiondetailtemp;

INSERT INTO `sysversion` (`Version`) VALUES
(33);