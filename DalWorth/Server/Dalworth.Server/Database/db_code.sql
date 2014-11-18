-- Unique index for Login

ALTER TABLE Employee ADD UNIQUE INDEX `IX_EmployeeLogin` USING BTREE(`Login`);

-- Update Task to new Modified date

DELIMITER $$
CREATE PROCEDURE `MarkTaskModified`(IN taskId int)
BEGIN
    UPDATE Task SET Task.Modified = NOW() WHERE ID = taskId;
END$$
DELIMITER ;

-- Update Tasks to new Modified date by VisitId

DELIMITER $$
CREATE PROCEDURE `MarkTaskModifiedByVisit`(IN visitId int)
BEGIN
    UPDATE Task t, (SELECT TaskId FROM VisitTask WHERE VisitTask.VisitId = visitId) t2
	SET t.Modified = NOW()
	WHERE t.ID = t2.TaskId;
END$$
DELIMITER ;

-- Update Tasks to new Modified date by Work

DELIMITER $$
CREATE PROCEDURE `MarkTaskModifiedByWork`(IN workIdInput int)
BEGIN
	UPDATE Task t, (SELECT vt.TaskId FROM WorkDetail wd 
		inner join VisitTask vt on vt.VisitId = wd.VisitId    
		where wd.WorkId = workIdInput) t2
	SET t.Modified = NOW()
	WHERE t.ID = t2.TaskId;
END$$
DELIMITER ;

-- Trigger on Project Update 

DELIMITER $$                                      
CREATE TRIGGER MarkTaskModifiedProject BEFORE UPDATE ON Project
FOR EACH ROW
BEGIN

  IF NOT (NEW.InsuranceCompany <=> OLD.InsuranceCompany)
     OR NOT (NEW.InsuranceAgency <=> OLD.InsuranceAgency)
     OR NOT (NEW.InsuranceAgencyPhone <=> OLD.InsuranceAgencyPhone)
     OR NOT (NEW.InsuranceAgent <=> OLD.InsuranceAgent)
     OR NOT (NEW.InsuranceAdjustor <=> OLD.InsuranceAdjustor)
     OR NOT (NEW.InsuranceAdjustorPhone <=> OLD.InsuranceAdjustorPhone)
     OR NOT (NEW.AdvertisingSourceId <=> OLD.AdvertisingSourceId)
     OR NOT (NEW.AdvertisingTechnicianId <=> OLD.AdvertisingTechnicianId)
     OR NOT (NEW.ClosedAmount <=> OLD.ClosedAmount)
  THEN
    UPDATE Task SET Task.Modified = NOW() 
        WHERE Task.ProjectId = NEW.ID;
  END IF;

END$$
DELIMITER ;


-- Trigger on Task Insert

CREATE TRIGGER MarkTaskModifiedTaskInsert BEFORE INSERT ON Task FOR EACH ROW SET NEW.Modified = NOW();


-- Trigger on Task Update 

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedTask BEFORE UPDATE ON Task
FOR EACH ROW
BEGIN
  IF NOT (NEW.TaskStatusId <=> OLD.TaskStatusId)
     OR NOT (NEW.TaskFailTypeId <=> OLD.TaskFailTypeId)
     OR NOT (NEW.Message <=> OLD.Message)
     OR NOT (NEW.ClosedAmount <=> OLD.ClosedAmount)
     OR NOT (NEW.EstimatedClosedAmount <=> OLD.EstimatedClosedAmount)
  THEN
     SET NEW.Modified = NOW();
  END IF;
END$$
DELIMITER ;

-- Trigger on Visit Insert 

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedVisitTaskInsert BEFORE INSERT ON VisitTask
FOR EACH ROW
BEGIN
  call MarkTaskModified(NEW.TaskId);
END$$
DELIMITER ;

-- Trigger on Visit Update 

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedVisitUpdate BEFORE UPDATE ON Visit
FOR EACH ROW
BEGIN
  IF NOT (NEW.VisitStatusId <=> OLD.VisitStatusId)
     OR NOT (NEW.ServiceDate <=> OLD.ServiceDate)
     OR NOT (NEW.PreferedTimeFrom <=> OLD.PreferedTimeFrom)
     OR NOT (NEW.PreferedTimeTo <=> OLD.PreferedTimeTo)
     OR NOT (NEW.ServiceAddressId <=> OLD.ServiceAddressId)
     OR NOT (NEW.Notes <=> OLD.Notes)
     OR NOT (NEW.ClosedDollarAmount <=> OLD.ClosedDollarAmount)
  THEN
    call MarkTaskModifiedByVisit(NEW.ID);
  END IF;
END$$
DELIMITER ;

-- Trigger on WorkDetail Update

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedWorkDetailUpdate BEFORE UPDATE ON WorkDetail
FOR EACH ROW
BEGIN
  IF NOT (NEW.WorkId <=> OLD.WorkId)
     OR NOT (NEW.TimeBegin <=> OLD.TimeBegin)
     OR NOT (NEW.TimeEnd <=> OLD.TimeEnd)
     OR NOT (NEW.TimeDispatch <=> OLD.TimeDispatch)
     OR NOT (NEW.TimeArrive <=> OLD.TimeArrive)
     OR NOT (NEW.TimeComplete <=> OLD.TimeComplete)
  THEN
    call MarkTaskModifiedByVisit(OLD.VisitId);
    call MarkTaskModifiedByVisit(NEW.VisitId);
  END IF;
END$$
DELIMITER ;

-- Trigger on Work Update

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedWorkUpdate BEFORE UPDATE ON `Work`
FOR EACH ROW
BEGIN
  IF NOT (NEW.VanId <=> OLD.VanId)
  THEN
    call MarkTaskModifiedByWork(NEW.ID);
  END IF;
END$$
DELIMITER ;

-- Trigger on Item Update

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedItemUpdate BEFORE UPDATE ON Item
FOR EACH ROW
BEGIN
  DECLARE taskIdVar int;

  IF NOT (NEW.ItemShapeId <=> OLD.ItemShapeId)
     OR NOT (NEW.Width <=> OLD.Width)
     OR NOT (NEW.Height <=> OLD.Height)
     OR NOT (NEW.Diameter <=> OLD.Diameter)
     OR NOT (NEW.SubTotalCost <=> OLD.SubTotalCost)
  THEN
    SELECT TaskId INTO taskIdVar FROM Item WHERE Item.ID = NEW.ID;
    call MarkTaskModified(taskIdVar);
  END IF;
END$$
DELIMITER ;

-- Trigger on DefloodDetail Update

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedDefloodDetailUpdate BEFORE UPDATE ON DefloodDetail
FOR EACH ROW
BEGIN
  IF NOT (NEW.FloodDate <=> OLD.FloodDate)
  THEN
    call MarkTaskModified(NEW.DefloodTaskId);
  END IF;
END$$
DELIMITER ;

-- Trigger on Visit Insert 

DELIMITER $$
CREATE TRIGGER MarkTaskModifiedWorkTransactionEtcInsert BEFORE INSERT ON WorkTransactionEtc
FOR EACH ROW
BEGIN
	DECLARE visitIdVar int;
	SELECT VisitId INTO visitIdVar FROM WorkTransaction WHERE WorkTransaction.ID = NEW.WorkTransactionId;

	call MarkTaskModifiedByVisit(visitIdVar);
END$$
DELIMITER ;

-- Procedure shows list of incremental dates from start to end (used in reports)

DELIMITER $$
CREATE PROCEDURE `GetIncrementedDates`(IN startDate Date, in endDate Date)
BEGIN
  DROP TABLE IF EXISTS TmpIncrementedDateList;
  CREATE TEMPORARY TABLE TmpIncrementedDateList (ID Date) TYPE=HEAP;    

  WHILE startDate <= endDate DO
    INSERT INTO TmpIncrementedDateList VALUES(startDate);
    SET startDate = DATE_ADD(startDate, INTERVAL 1 DAY);
  END WHILE;

END$$
DELIMITER ;

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