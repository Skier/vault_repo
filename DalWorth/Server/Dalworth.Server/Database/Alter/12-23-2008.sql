update Item i
inner join Task t on t.ID = i.TaskId
inner join Project p on p.ID = t.ProjectId
set CleaningRate = 3
where p.ProjectTypeId = 1;

update Item i
inner join Task t on t.ID = i.TaskId
inner join Project p on p.ID = t.ProjectId
set CleaningRate = 4.5
where p.ProjectTypeId = 2;