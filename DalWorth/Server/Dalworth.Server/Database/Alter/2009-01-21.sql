ALTER TABLE EquipmentTransaction ADD INDEX `IX_SequnceDate` USING BTREE(`SequnceDate`);

DROP TABLE IF EXISTS `EquipmentTransactionDetailLink`;
CREATE TABLE `EquipmentTransactionDetailLink` (
  `EquipmentTransactionDetailId` int(10) NOT NULL,
  `PrevEquipmentTransactionDetailId` int(10) default NULL,
  PRIMARY KEY  (`EquipmentTransactionDetailId`),
  KEY `FK_EquipmentTransactionDetailLink_EquipmentTransactionDetailLink` (`PrevEquipmentTransactionDetailId`),
  CONSTRAINT `FK_EquipmentTransactionDetailLink_EquipmentTransactionDetailLink` FOREIGN KEY (`PrevEquipmentTransactionDetailId`) REFERENCES `equipmenttransactiondetaillink` (`EquipmentTransactionDetailId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_EquipmentTransactionDetailLink_EquipmentTransactionDetail` FOREIGN KEY (`EquipmentTransactionDetailId`) REFERENCES `equipmenttransactiondetail` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Function finds previous EquipmentTransactionDetail ID

DELIMITER $$
CREATE FUNCTION `FindPrevEquipmentTransactionDetailId`(equipmentIdVar int, equipmentTransactionDetailIdVar int) RETURNS int
BEGIN
  DECLARE currentSequenceDateVar DATETIME;
  DECLARE resultVar int;

	SELECT et.Sequncedate INTO currentSequenceDateVar FROM EquipmentTransactionDetail etd
		INNER JOIN EquipmentTransaction et ON etd.EquipmentTransactionId = et.ID
	WHERE etd.ID = equipmentTransactionDetailIdVar;

	SELECT etd.ID INTO resultVar FROM EquipmentTransactionDetail etd
	  INNER JOIN EquipmentTransaction et ON etd.EquipmentTransactionId = et.ID
	WHERE
	  EquipmentId = equipmentIdVar AND etd.ID <> equipmentTransactionDetailIdVar
	  AND et.SequnceDate = (
	      SELECT MAX(et.Sequncedate) FROM EquipmentTransactionDetail etd
        INNER JOIN EquipmentTransaction et ON etd.EquipmentTransactionId = et.ID
	      WHERE EquipmentId = equipmentIdVar
	      AND(
	        (et.Sequncedate < currentSequenceDateVar AND etd.ID <> equipmentTransactionDetailIdVar)
	        OR (et.Sequncedate = currentSequenceDateVar AND etd.ID < equipmentTransactionDetailIdVar)))
	ORDER BY etd.ID DESC LIMIT 1;

  RETURN resultVar;

END$$
DELIMITER ;

-- Function finds next EquipmentTransactionDetail ID

DELIMITER $$
CREATE FUNCTION `FindNextEquipmentTransactionDetailId`(equipmentIdVar int, equipmentTransactionDetailIdVar int) RETURNS int
BEGIN
  DECLARE currentSequenceDateVar DATETIME;
  DECLARE resultVar int;

	SELECT et.Sequncedate INTO currentSequenceDateVar FROM EquipmentTransactionDetail etd
		INNER JOIN EquipmentTransaction et ON etd.EquipmentTransactionId = et.ID
	WHERE etd.ID = equipmentTransactionDetailIdVar;

	SELECT etd.ID INTO resultVar FROM EquipmentTransactionDetail etd
	  INNER JOIN EquipmentTransaction et ON etd.EquipmentTransactionId = et.ID
	WHERE
	  EquipmentId = equipmentIdVar AND etd.ID <> equipmentTransactionDetailIdVar
	  AND et.SequnceDate = (
	      SELECT MIN(et.Sequncedate) FROM EquipmentTransactionDetail etd
        INNER JOIN EquipmentTransaction et ON etd.EquipmentTransactionId = et.ID
	      WHERE EquipmentId = equipmentIdVar
	      AND(
	        (et.Sequncedate > currentSequenceDateVar AND etd.ID <> equipmentTransactionDetailIdVar)
	        OR (et.Sequncedate = currentSequenceDateVar AND etd.ID > equipmentTransactionDetailIdVar)))
	ORDER BY etd.ID LIMIT 1;

  RETURN resultVar;

END$$
DELIMITER ;

-- Trigger to update Prev and Next state on EquipmentTransactionDetail Insert

DELIMITER $$
CREATE TRIGGER SetEquipmentTransactionPrevNextStateInsert AFTER INSERT ON EquipmentTransactionDetail
FOR EACH ROW
BEGIN
	DECLARE nextTransactionIdVar int;

	INSERT INTO EquipmentTransactionDetailLink
		SELECT NEW.ID, FindPrevEquipmentTransactionDetailId(NEW.EquipmentId, NEW.ID);

	SELECT FindNextEquipmentTransactionDetailId(NEW.EquipmentId, NEW.ID) INTO nextTransactionIdVar;
	
	IF (nextTransactionIdVar is not null)
	THEN		
		UPDATE EquipmentTransactionDetailLink SET PrevEquipmentTransactionDetailId = NEW.ID
			WHERE EquipmentTransactionDetailId = nextTransactionIdVar;
	END IF;	
  	
END$$
DELIMITER ;

-- Trigger to update Prev and Next state on EquipmentTransactionDetail Delete

DELIMITER $$
CREATE TRIGGER SetEquipmentTransactionPrevNextStateDelete BEFORE DELETE ON EquipmentTransactionDetail
FOR EACH ROW
BEGIN

	DECLARE nextTransactionIdVar int;
	DECLARE prevTransactionIdForCurrentVar int;

	SELECT PrevEquipmentTransactionDetailId INTO prevTransactionIdForCurrentVar FROM EquipmentTransactionDetailLink
		WHERE EquipmentTransactionDetailId = OLD.ID;

	UPDATE EquipmentTransactionDetailLink SET PrevEquipmentTransactionDetailId = prevTransactionIdForCurrentVar
		WHERE PrevEquipmentTransactionDetailId = OLD.ID;


	DELETE FROM EquipmentTransactionDetailLink
		WHERE EquipmentTransactionDetailId = OLD.ID;
  	
END$$
DELIMITER ;