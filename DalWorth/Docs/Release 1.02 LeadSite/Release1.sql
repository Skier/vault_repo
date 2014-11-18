-- create leasd without signuip
insert into lead (
  `ProjectTypeId`
  ,`BusinessPartnerId`
  ,`CustomerWebAccountId`
  ,`Company`
  ,`FirstName`
  ,`Last Name`
  ,`Address1`
  ,`Address2`
  ,`City`
  ,`State`
  ,  `Zip`
  ,`Phone`
  ,`Email`
  ,`Description`
  ,`DesiredServiceDate`
  ,`DesiredTimeFrame`
  ,`LeadStatusId`
  ,`DateCreated`
)
values (1, 1, null, null, "Boris", "Furman", "7016 Randall Way", null, "Plano",
  "TX", "75025", "214 335 4143", "bifurman@yahoo.com", "need to clean two rugs 8X10 and 10X11", null, null, 1, now())
  
  
-- singup 

insert into `customerwebaccount` (

  `BusinessPartnerId`
  ,`Company`
  ,`FirstName`
  ,`LastName`
  ,`Address1`
  ,`Address2`
  ,`City`
  ,`State`
  ,`Zip`
  ,`Password`
  ,`LastModifiedDate`
)
values (1, null, "Boris", "Furman", "7016 Randall Way", null, "Plano", "TX", "75025", "boris1o", now())


-- todo Customer history

-- message on dashboard showing leads 
select leadstatusid, count(*)
from lead where leadstatusid in (1, 2) group by leadstatusid







