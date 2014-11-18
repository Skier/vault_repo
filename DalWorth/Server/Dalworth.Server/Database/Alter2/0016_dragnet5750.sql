DELIMITER $$

DROP PROCEDURE IF EXISTS `dalworth_server_dbo_production`.`ReportBooking` $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReportBooking`(IN startDate Date, in endDate Date)
BEGIN

  Declare dt date;

  DROP TABLE IF EXISTS TmpIncrementedDateList;

  CREATE TEMPORARY TABLE TmpIncrementedDateList (
  ID Date,
  RugQty int,
  FloodQty int,
  ConstructionQty int,
  ContentQty int,
  MiscQty int
  ) TYPE=HEAP;

  set dt = startDate;

  WHILE dt <= endDate DO
    INSERT INTO TmpIncrementedDateList (ID, RugQty, FloodQty, ConstructionQty, ContentQty, MiscQty)
      VALUES(dt, 0, 0, 0, 0, 0);
    SET dt = DATE_ADD(dt, INTERVAL 1 DAY);
  END WHILE;

  update TmpIncrementedDateList Idl
    join (
        select Date(CreateDate)  as bookedDate, count(ID) as qty
        from project
        where CreateDate between startDate and endDate
        and ProjectTypeId = 1
        group by Date(CreateDate)) pr on pr.bookedDate = idl.ID
  set idl.RugQty = pr.qty;

  update TmpIncrementedDateList Idl
    join (
        SELECT Date(CreateDate) as bookedDate, count(ID) as qty
        FROM Project
        where CreateDate between startDate and endDate
          AND ProjectTypeId = 2
        group by Date(CreateDate)) pr on Date(pr.bookedDate) = idl.ID
	set idl.FloodQty = pr.qty;

   update TmpIncrementedDateList Idl
    join (
        SELECT Date(CreateDate) as bookedDate, count(ID) as qty
        FROM Project
        where CreateDate between startDate and endDate
          AND ProjectTypeId = 3
        group by Date(CreateDate)) pr on Date(pr.bookedDate) = idl.ID
  set idl.MiscQty = pr.qty;

  update TmpIncrementedDateList Idl
    join (
        SELECT Date(CreateDate) as bookedDate, count(ID) as qty
        FROM Project
        where CreateDate between startDate and endDate
          AND ProjectTypeId = 4
        group by Date(CreateDate)) pr on Date(pr.bookedDate) = idl.ID
  set idl.ConstructionQty = pr.qty;

   update TmpIncrementedDateList Idl
    join (
        SELECT Date(CreateDate) as bookedDate, count(ID) as qty
        FROM Project
        where CreateDate between startDate and endDate
          AND ProjectTypeId = 5
        group by Date(CreateDate)) pr on Date(pr.bookedDate) = idl.ID
  set idl.ContentQty = pr.qty;

  select ID, RugQty, FloodQty, ConstructionQty, ContentQty, MiscQty from TmpIncrementedDateList;
END $$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `dalworth_server_dbo_production`.`ReportAdvertisingSourceByYear` $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReportAdvertisingSourceByYear`(IN yearCreated int)
BEGIN

  SELECT a.Name as AdvertisingSourceName,
    sum(if(Month(p.CreateDate) = 1 and p.ClosedAmount > 0 , 1, 0)) Month01Count,
    sum(if(Month(p.CreateDate) = 1 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month01Amount,
    sum(if(Month(p.CreateDate) = 2 and p.ClosedAmount > 0 , 1, 0))               Month02Count,
    sum(if(Month(p.CreateDate) = 2 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month02Amount,
    sum(if(Month(p.CreateDate) = 3 and p.ClosedAmount > 0 , 1, 0))               Month03Count,
    sum(if(Month(p.CreateDate) = 3 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month03Amount,
    sum(if(Month(p.CreateDate) = 4 and p.ClosedAmount > 0 , 1, 0))               Month04Count,
    sum(if(Month(p.CreateDate) = 4 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month04Amount,
    sum(if(Month(p.CreateDate) = 5 and p.ClosedAmount > 0 , 1, 0))               Month05Count,
    sum(if(Month(p.CreateDate) = 5 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month05Amount,
    sum(if(Month(p.CreateDate) = 6 and p.ClosedAmount > 0 , 1, 0))               Month06Count,
    sum(if(Month(p.CreateDate) = 6 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month06Amount,
    sum(if(Month(p.CreateDate) = 7 and p.ClosedAmount > 0 , 1, 0))               Month07Count,
    sum(if(Month(p.CreateDate) = 7 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month07Amount,
    sum(if(Month(p.CreateDate) = 8 and p.ClosedAmount > 0 , 1, 0))               Month08Count,
    sum(if(Month(p.CreateDate) = 8 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month08Amount,
    sum(if(Month(p.CreateDate) = 9 and p.ClosedAmount > 0 , 1, 0))               Month09Count,
    sum(if(Month(p.CreateDate) = 9 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month09Amount,
    sum(if(Month(p.CreateDate) = 10 and p.ClosedAmount > 0 , 1, 0))              Month10Count,
    sum(if(Month(p.CreateDate) = 10 and p.ClosedAmount > 0, p.ClosedAmount, 0))  Month10Amount,
    sum(if(Month(p.CreateDate) = 11 and p.ClosedAmount > 0 , 1, 0))              Month11Count,
    sum(if(Month(p.CreateDate) = 11 and p.ClosedAmount > 0, p.ClosedAmount, 0))  Month11Amount,
    sum(if(Month(p.CreateDate) = 12 and p.ClosedAmount > 0 , 1, 0))              Month12Count,
    sum(if(Month(p.CreateDate) = 12 and p.ClosedAmount > 0, p.ClosedAmount, 0))  Month12Amount,
    sum(p.ClosedAmount) total
  FROM project p
  inner join AdvertisingSource a on a.ID = p.AdvertisingSourceId
  where Year(p.CreateDate) = yearCreated
  group by a.Name
  order by total desc;


END $$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `dalworth_server_dbo_production`.`ReportAdvertisingSourceByYearProjectTypeId` $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReportAdvertisingSourceByYearProjectTypeId`(IN yearCreated int, IN projectTypeId int)
BEGIN

  SELECT a.Name as AdvertisingSourceName,
    sum(if(Month(p.CreateDate) = 1 and p.ClosedAmount > 0 , 1, 0)) Month01Count,
    sum(if(Month(p.CreateDate) = 1 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month01Amount,
    sum(if(Month(p.CreateDate) = 2 and p.ClosedAmount > 0 , 1, 0))               Month02Count,
    sum(if(Month(p.CreateDate) = 2 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month02Amount,
    sum(if(Month(p.CreateDate) = 3 and p.ClosedAmount > 0 , 1, 0))               Month03Count,
    sum(if(Month(p.CreateDate) = 3 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month03Amount,
    sum(if(Month(p.CreateDate) = 4 and p.ClosedAmount > 0 , 1, 0))               Month04Count,
    sum(if(Month(p.CreateDate) = 4 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month04Amount,
    sum(if(Month(p.CreateDate) = 5 and p.ClosedAmount > 0 , 1, 0))               Month05Count,
    sum(if(Month(p.CreateDate) = 5 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month05Amount,
    sum(if(Month(p.CreateDate) = 6 and p.ClosedAmount > 0 , 1, 0))               Month06Count,
    sum(if(Month(p.CreateDate) = 6 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month06Amount,
    sum(if(Month(p.CreateDate) = 7 and p.ClosedAmount > 0 , 1, 0))               Month07Count,
    sum(if(Month(p.CreateDate) = 7 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month07Amount,
    sum(if(Month(p.CreateDate) = 8 and p.ClosedAmount > 0 , 1, 0))               Month08Count,
    sum(if(Month(p.CreateDate) = 8 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month08Amount,
    sum(if(Month(p.CreateDate) = 9 and p.ClosedAmount > 0 , 1, 0))               Month09Count,
    sum(if(Month(p.CreateDate) = 9 and p.ClosedAmount > 0, p.ClosedAmount, 0))   Month09Amount,
    sum(if(Month(p.CreateDate) = 10 and p.ClosedAmount > 0 , 1, 0))              Month10Count,
    sum(if(Month(p.CreateDate) = 10 and p.ClosedAmount > 0, p.ClosedAmount, 0))  Month10Amount,
    sum(if(Month(p.CreateDate) = 11 and p.ClosedAmount > 0 , 1, 0))              Month11Count,
    sum(if(Month(p.CreateDate) = 11 and p.ClosedAmount > 0, p.ClosedAmount, 0))  Month11Amount,
    sum(if(Month(p.CreateDate) = 12 and p.ClosedAmount > 0 , 1, 0))              Month12Count,
    sum(if(Month(p.CreateDate) = 12 and p.ClosedAmount > 0, p.ClosedAmount, 0))  Month12Amount,
    sum(p.ClosedAmount) total
  FROM project p
  inner join AdvertisingSource a on a.ID = p.AdvertisingSourceId
  where Year(p.CreateDate) = yearCreated
    and p.ProjectTypeId = projectTypeId
  group by a.Name
  order by total desc;


END $$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `dalworth_server_dbo_production`.`ReportRevenue` $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReportRevenue`(IN startDate Date, in endDate Date)
BEGIN

  Declare dt date;

  DROP TABLE IF EXISTS TmpIncrementedDateList;

  CREATE TEMPORARY TABLE TmpIncrementedDateList (
  ID Date,
  RugAmount                    Decimal(19,4),
  FloodAmount                  Decimal(19,4),
  ConstructionAmount           Decimal(19,4),
  ContentAmount                Decimal(19,4),
  TotalAmount                Decimal(19,4)
  ) TYPE=HEAP;

  set dt = startDate;

  WHILE dt <= endDate DO
    INSERT INTO TmpIncrementedDateList (ID, RugAmount, FloodAmount,
      ConstructionAmount, ContentAmount, TotalAmount)
      VALUES(dt, 0, 0, 0, 0, 0);
    SET dt = DATE_ADD(dt, INTERVAL 1 DAY);
  END WHILE;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(w.StartDate) as bookedDate, sum(t.ClosedAmount) as RugAmount
        FROM WorkTransactionTask wtt
        inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
        inner join Work w on w.ID = wt.WorkId
        inner join Task t on t.ID = wtt.TaskId
        inner join Project p on p.ID = t.ProjectId
        where (p.ProjectTypeId = 1 or (p.ProjectTypeId = 3 and t.IsRugCleaningDepartment = 1))
        and WorkTransactionTaskActionId = 1 and
        w.StartDate between startDate and endDate
        group by Date(w.StartDate)) workData on workData.bookedDate = idl.ID
   set idl.RugAmount = workData.RugAmount;

  update TmpIncrementedDateList Idl
       join (
        SELECT Date(work.StartDate) as TotalDate, sum(ClosedDollarAmount) as TotalAmt
        FROM Work
        where Date(work.StartDate) between startDate and endDate
        group by Date(work.StartDate)) Total on Total.TotalDate = idl.ID
    set idl.TotalAmount   = Total.TotalAmt;

  update TmpIncrementedDateList Idl
    set FloodAmount = TotalAmount - RugAmount;


   update TmpIncrementedDateList Idl
    join (
      SELECT Date(b.IssueDate) as ConstructionBillDate, sum(b.Amount) as ConstructionBillAmt
        FROM ProjectConstructionBillPay b
          inner join Project p on p.ID = b.ProjectId
        where b.ProjectConstructionBillPayTypeId = 1
        and p.ProjectTypeId = 4
        and b.IsVoided = false
        and b.IssueDate between startDate and endDate
        group by Date(b.IssueDate)) ConstructionBill on ConstructionBill.ConstructionBillDate = idl.ID
    set idl.ConstructionAmount = ConstructionBill.ConstructionBillAmt;

    update TmpIncrementedDateList Idl
    join (
        SELECT Date(b.IssueDate) as ConstructionCreditDate, sum(b.Amount) as ConstructionCreditAmt
          FROM ProjectConstructionBillPay b
          inner join Project p on p.ID = b.ProjectId
          where b.ProjectConstructionBillPayTypeId = 3
          and p.ProjectTypeId = 4
          and b.IsVoided = false
          and Date(b.IssueDate) between startDate and endDate
          group by Date(b.IssueDate)) ConstructionCredit on ConstructionCredit.ConstructionCreditDate = idl.ID
    set idl.ConstructionAmount =  idl.ConstructionAmount - ConstructionCredit.ConstructionCreditAmt;

    update TmpIncrementedDateList Idl
      join (
       SELECT Date(b.IssueDate) as ContentBillDate, sum(b.Amount) as ContentBillAmt
         FROM ProjectConstructionBillPay b
         inner join Project p on p.ID = b.ProjectId
         where b.ProjectConstructionBillPayTypeId = 1
         and p.ProjectTypeId = 5
         and b.IsVoided = false
         and Date(b.IssueDate) between startDate and endDate
         group by Date(b.IssueDate)) ContentBill on ContentBill.ContentBillDate = idl.ID
    set idl.ContentAmount = ContentBill.ContentBillAmt;


    update TmpIncrementedDateList Idl
      join (
        SELECT Date(b.IssueDate) as ContentCreditDate, sum(b.Amount) as ContentCreditAmt
          FROM ProjectConstructionBillPay b
          inner join Project p on p.ID = b.ProjectId
        where b.ProjectConstructionBillPayTypeId = 3
        and p.ProjectTypeId = 5
        and b.IsVoided = false
        and Date(b.IssueDate) between startDate and endDate
        group by Date(b.IssueDate)) ContentCredit on ContentCredit.ContentCreditDate = idl.ID
    set idl.ContentAmount = idl.ContentAmount - ContentCredit.ContentCreditAmt;


  select ID, RugAmount, FloodAmount,  ConstructionAmount,
    ContentAmount, TotalAmount
  from TmpIncrementedDateList;


END $$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `dalworth_server_dbo_production`.`ReportConstructionSummary` $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReportConstructionSummary`(IN startDate Date, in endDate Date, in projectTypeId1 int)
BEGIN

  Declare dt date;

  DROP TABLE IF EXISTS TmpIncrementedDateList;

  CREATE TEMPORARY TABLE TmpIncrementedDateList (
  ID Date,
  LeadCount                    int,
  SingupCount                  int,
  ScopeAmount                  Decimal(19,4),
  BilledAmount                 Decimal(19,4),
  WorkInProgressAmount         Decimal(19,4)
  ) TYPE=HEAP;

  set dt = startDate;

  WHILE dt <= endDate DO
    INSERT INTO TmpIncrementedDateList (ID, LeadCount, SingupCount,
      ScopeAmount, BilledAmount, WorkInProgressAmount)
      VALUES(dt, 0, 0, 0, 0, 0);
    SET dt = DATE_ADD(dt, INTERVAL 1 DAY);
  END WHILE;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(p.CreateDate) as LeadDate, count(p.ID) as LeadCount
      FROM Project p
        inner join ProjectConstructionDetail d on d.ProjectId = p.ID
      where p.ProjectTypeId = projectTypeId1
        and  Date(p.CreateDate) between startDate and endDate
      group by Date(p.CreateDate)) lead on Lead.LeadDate = idl.ID
   set idl.LeadCount = lead.LeadCount;


  update TmpIncrementedDateList Idl
    join (
      SELECT Date(d.SignUpDate) as SignUpDate, count(p.ID) as SingupCount,
        sum(p.ClosedAmount) as ScopeAmount,  sum(d.BilledAmount) BilledAmount,
        sum(p.ClosedAmount - d.BilledAmount) as WorkInProgressAmount
      FROM Project p
        inner join ProjectConstructionDetail d on d.ProjectId = p.ID
      where p.ProjectTypeId = projectTypeId1
        and  Date(d.SignUpDate) between startDate and endDate
      group by Date(d.SignUpDate)) signup on signup.SignUpDate = idl.ID
    set idl.SingupCount = signup.SingupCount,
      idl.ScopeAmount = signup.ScopeAmount, idl.BilledAmount = signup.BilledAmount,
      idl.WorkInProgressAmount = signup.WorkInProgressAmount;

  select ID, LeadCount, SingupCount,  ScopeAmount,
    BilledAmount, WorkInProgressAmount
  from TmpIncrementedDateList;


END $$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `dalworth_server_dbo_production`.`ReportRugProduction` $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReportRugProduction`(IN startDate Date, in endDate Date)
BEGIN

  Declare dt date;

  DROP TABLE IF EXISTS TmpIncrementedDateList;

  CREATE TEMPORARY TABLE TmpIncrementedDateList (
  ID                             Date,
  PickupScheduledCount           int,
  PickupCompletedCount           int,
  PickupRugQuantity              int,
  EstimatedOrderAmount           Decimal(19,4),
  DeliveryCompletedCount         int,
  DeliveryRugQuantity            int,
  ClosedAmount                   Decimal(19,4)
  ) TYPE=HEAP;

  set dt = startDate;

  WHILE dt <= endDate DO
    INSERT INTO TmpIncrementedDateList (ID, PickupScheduledCount, PickupCompletedCount,
      PickupRugQuantity, EstimatedOrderAmount,
      DeliveryCompletedCount, DeliveryRugQuantity, ClosedAmount)
      VALUES(dt, 0, 0, 0, 0, 0, 0, 0);
    SET dt = DATE_ADD(dt, INTERVAL 1 DAY);
  END WHILE;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(wd.TimeBegin) as PickupScheduledDate, count(t.ID) as PickupScheduledCount
        FROM WorkDetail wd
        inner join VisitTask vt on vt.VisitId = wd.VisitId
        inner join Task t on t.ID = vt.TaskId
        inner join Project p on p.ID = t.ProjectId
        where Date(wd.TimeBegin) between startDate and endDate
        and t.TaskTypeId = 1 and p.ProjectTypeId = 1
      group by Date(wd.TimeBegin)) PuckupScheduled on PuckupScheduled.PickupScheduledDate = idl.ID
  set idl.PickupScheduledCount = PuckupScheduled.PickupScheduledCount;


  update TmpIncrementedDateList Idl
    join (
       SELECT Date(w.StartDate) as PickupCompletedDate, count(wtt.TaskId) as PickupCompletedCount,
        sum(t.EstimatedClosedAmount) as EstimatedOrderAmount
       FROM WorkTransactionTask wtt
           inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
           inner join Work w on w.ID = wt.WorkId
           inner join Task t on t.ID = wtt.TaskId
           inner join Project p on p.ID = t.ProjectId
       where Date(w.StartDate) between startDate and endDate
         and wtt.WorkTransactionTaskActionId = 1
         and t.TaskTypeId = 1 and p.ProjectTypeId = 1
       group by Date(w.StartDate)) PickupCompleted on PickupCompleted.PickupCompletedDate = idl.ID
   set idl.PickupCompletedCount = PickupCompleted.PickupCompletedCount,
       idl.EstimatedOrderAmount = PickupCompleted.EstimatedOrderAmount;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(w.StartDate) as PickupRugsDate, count(i.ID) as PickupRugsCount
      FROM WorkTransactionTask wtt
        inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
        inner join Work w on w.ID = wt.WorkId
        inner join Task t on t.ID = wtt.TaskId
        inner join Project p on p.ID = t.ProjectId
        inner join Item i on i.TaskId = t.ID
      where Date(w.StartDate) between startDate and endDate
        and WorkTransactionTaskActionId = 1
        and t.TaskTypeId = 1 and p.ProjectTypeId = 1
        group by Date(w.StartDate)) PickupRugs on PickupRugs.PickupRugsDate = idl.ID
  set idl.PickupRugQuantity =PickupRugs.PickupRugsCount;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(w.StartDate) as DeliveryCompletedDate, count(wtt.TaskId) as DeliveryCompletedCount
      FROM WorkTransactionTask wtt
        inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
        inner join Work w on w.ID = wt.WorkId
        inner join Task t on t.ID = wtt.TaskId
        inner join Project p on p.ID = t.ProjectId
        where Date(w.StartDate) between startDate and endDate
          and WorkTransactionTaskActionId = 1
          and t.TaskTypeId = 2 and p.ProjectTypeId = 1
      group by Date(w.StartDate)) DeliveryCompleted on DeliveryCompleted.DeliveryCompletedDate = idl.ID
  set idl.DeliveryCompletedCount =DeliveryCompleted.DeliveryCompletedCount;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(w.StartDate) as DeliveryRugsDate, count(i.ID) as DeliveryRugQuantity
      FROM WorkTransactionTask wtt
        inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
        inner join Work w on w.ID = wt.WorkId
        inner join Task t on t.ID = wtt.TaskId
        inner join Project p on p.ID = t.ProjectId
        inner join Item i on i.TaskId = t.ID
      where Date(w.StartDate) between startDate and endDate
        and WorkTransactionTaskActionId = 1
        and t.TaskTypeId = 2 and p.ProjectTypeId = 1
      group by Date(w.StartDate)) DeliveryRugs on DeliveryRugs.DeliveryRugsDate = idl.ID
  set idl.DeliveryRugQuantity =DeliveryRugs.DeliveryRugQuantity;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(w.StartDate) as DayClosedAmtDate, sum(t.ClosedAmount) as RugsClosedAmount
      FROM WorkTransactionTask wtt
        inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
        inner join Work w on w.ID = wt.WorkId
        inner join Task t on t.ID = wtt.TaskId
        inner join Project p on p.ID = t.ProjectId
      where Date(w.StartDate) between startDate and endDate
        and (p.ProjectTypeId = 1 or (p.ProjectTypeId = 3 and t.IsRugCleaningDepartment = 1))
        and WorkTransactionTaskActionId = 1
      group by Date(w.StartDate)) DayClosedAmount on DayClosedAmount.DayClosedAmtDate = idl.ID
  set idl.ClosedAmount =DayClosedAmount.RugsClosedAmount;


  select ID, PickupScheduledCount, PickupCompletedCount,  PickupRugQuantity,
    EstimatedOrderAmount, DeliveryCompletedCount, DeliveryRugQuantity, ClosedAmount
  from TmpIncrementedDateList;


END $$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `dalworth_server_dbo_production`.`ReportFloodProduction` $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ReportFloodProduction`(IN startDate Date, in endDate Date)
BEGIN

  Declare dt date;

  DROP TABLE IF EXISTS TmpIncrementedDateList;

  CREATE TEMPORARY TABLE TmpIncrementedDateList (
  ID                             Date,
  ScheduledCount                 int,
  SoldCount                      int,
  ClosedAmount                   Decimal(19,4)
  ) TYPE=HEAP;

  set dt = startDate;

  WHILE dt <= endDate DO
    INSERT INTO TmpIncrementedDateList (ID, ScheduledCount, SoldCount,
      ClosedAmount)
      VALUES(dt, 0, 0, 0);
    SET dt = DATE_ADD(dt, INTERVAL 1 DAY);
  END WHILE;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(wd.TimeBegin) as DefloodScheduledDate, count(t.ID) as DefloodScheduledCount
      FROM WorkDetail wd
        inner join VisitTask vt on vt.VisitId = wd.VisitId
        inner join Task t on t.ID = vt.TaskId
        inner join Project p on p.ID = t.ProjectId
      where Date(wd.TimeBegin) between startDate and endDate
        and t.TaskTypeId = 5 and p.ProjectTypeId = 2
        and (
          select count(*)
          from Task t2
          where t2.ParentTaskId = t.ParentTaskId
          and t2.DumpedTaskId is null
          and t2.ID < t.ID
          and TaskTypeId = 5) = 0
      group by Date(wd.TimeBegin)) DefloodScheduled on DefloodScheduled.DefloodScheduledDate = idl.ID
      set idl.ScheduledCount = DefloodScheduled.DefloodScheduledCount;

  update TmpIncrementedDateList Idl
    join (
      select Date(w.StartDate) as DefloodSoldDate, count(FirstProcessFlood.TaskId) as SoldCount
      from
        ( SELECT wtt.TaskId, min(wtt.WorkTransactionId) WorkTransactionId
          FROM WorkTransactionTask wtt
            inner join Task t on t.ID = wtt.TaskId
          where t.TaskTypeId = 4 and (wtt.WorkTransactionTaskActionId = 1 or wtt.WorkTransactionTaskActionId = 2)
          group by wtt.TaskId) FirstProcessFlood
        inner join WorkTransaction wt on wt.ID = FirstProcessFlood.WorkTransactionId
        inner join Work w on w.ID = wt.WorkId
      where Date(w.StartDate) between startDate and endDate
      group by Date(w.StartDate)) DefloodSold on DefloodSold.DefloodSoldDate = idl.ID
  set idl.SoldCount = DefloodSold.SoldCount;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(w.StartDate) as TotalDate, sum(w.ClosedDollarAmount) as TotalAmt
      FROM Work w
      where Date(w.StartDate) between startDate and endDate
      group by Date(w.StartDate)) Total on Total.TotalDate = idl.ID
  set idl.ClosedAmount = Total.TotalAmt;

  update TmpIncrementedDateList Idl
    join (
      SELECT Date(w.StartDate) as RugsDate, sum(t.ClosedAmount) as RugsAmt
      FROM WorkTransactionTask wtt
        inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
        inner join Work w on w.ID = wt.WorkId
        inner join Task t on t.ID = wtt.TaskId
        inner join Project p on p.ID = t.ProjectId
      where Date(w.StartDate) between startDate and endDate
        and (p.ProjectTypeId = 1 or (p.ProjectTypeId = 3 and t.IsRugCleaningDepartment = 1))
        and WorkTransactionTaskActionId = 1
      group by Date(w.StartDate)) Rugs on Rugs.RugsDate = idl.ID
  set idl.ClosedAmount =  idl.ClosedAmount - Rugs.RugsAmt;


  select ID, ScheduledCount, SoldCount,  ClosedAmount
  from TmpIncrementedDateList;


END $$

DELIMITER ;

INSERT INTO `sysversion` (`Version`) VALUES
(16);