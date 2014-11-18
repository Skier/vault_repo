/* Set these variables */
set @existingTechnicianName := 'Technician name to copy';
set @newTechnicianName := 'New technician Name';
set @newTechnicianServmanId := 'New technician ServmanId';
set @newTechnicianServmanCompanyId := 0;
/* Set these variables */

set @existingTechnicianId = 0;
select ID into @existingTechnicianId from TechnicianDefault where Name = @existingTechnicianName;

insert into TechnicianDefault (`ServmanId`, `Name`, `HourlyRate`, `HourlyRate150to300`, `HourlyRateMore300`, `DisplaySequence`, `CompanyId`, `DepotAddress`, `DepotLatitude`, `DepotLongitude`, `DriveTimeMinutes`, `IsContractor`, `MaxVisitsCount`, `MaxNonExclusiveVisitsCount`)
  (select @newTechnicianServmanId, @newTechnicianName, `HourlyRate`, `HourlyRate150to300`, `HourlyRateMore300`, `DisplaySequence`, @newTechnicianServmanCompanyId, `DepotAddress`, `DepotLatitude`, `DepotLongitude`, `DriveTimeMinutes`, `IsContractor`, `MaxVisitsCount`, `MaxNonExclusiveVisitsCount`
  from TechnicianDefault where ID = @existingTechnicianId);


insert into TechnicianServiceDenyDefault
  select LAST_INSERT_ID(), ServiceId, IsForNonExclusive from TechnicianServiceDenyDefault where TechnicianId = @existingTechnicianId;


insert into TechnicianWorkTimeDefault
  select LAST_INSERT_ID(), TimeStart, TimeEnd from TechnicianWorkTimeDefault where TechnicianId = @existingTechnicianId;


insert into TechnicianWorkTimeDefaultPreset
  select LAST_INSERT_ID(), PresetNumber, TimeStart, TimeEnd from TechnicianWorkTimeDefaultPreset where TechnicianId = @existingTechnicianId;
