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