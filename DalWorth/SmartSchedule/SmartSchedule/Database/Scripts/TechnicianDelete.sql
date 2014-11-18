/* Set these variables */
set @technicianId := 0;
/* Set these variables */

delete from TechnicianServiceDenyDefault where TechnicianId = @technicianId;
delete from TechnicianWorkTimeDefault where TechnicianId = @technicianId;
delete from TechnicianWorkTimeDefaultPreset where TechnicianId = @technicianId;
delete from TechnicianZipDefault where TechnicianId = @technicianId;
delete from TechnicianDefault where ID = @technicianId;