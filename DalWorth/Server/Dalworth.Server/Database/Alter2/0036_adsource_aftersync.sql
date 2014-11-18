 

update projecttype set QbClassListId = '80000011-1273522722' where id = 1;
update projecttype set QbClassListId = '8000000F-1273522694' where id = 2;
update projecttype set QbClassListId = '80000008-1273522561' where id = 4;
update projecttype set QbClassListId = '8000000C-1273522596' where id = 5;
update projecttype set QbClassListId = '80000008-1273522561' where id = 6;

-- add all business partner codes
update businesspartner set qbcustomertypelistid = '8000006B-1299278845' where id = 2
update businesspartner set qbcustomertypelistid = '80000069-1299278822' where id = 1
update businesspartner set qbcustomertypelistid = '8000006A-1299278834' where id = 4

-- Terry Hedge and other dalworth clean techs
--update qbsalesRep set qbcustomerTypelistid = '80000002-1272662058', employeeid = 826 where listid = '8000001B-1293313221';

-- Dalworth Techs
update qbsalesRep set  employeeid = 780 where listid = '80000009-1274738999';

INSERT INTO `sysversion` (`Version`) VALUES (36);
